﻿using System.Diagnostics.CodeAnalysis;
using Autofac;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Smartstore.Core.Installation;
using Smartstore.Data;
using Smartstore.Data.Hooks;
using Smartstore.Data.Migrations;
using Smartstore.Data.Providers;
using Smartstore.Threading;

namespace Smartstore.Core.Data
{
    public abstract class AsyncDbSaveHook<TEntity> : AsyncDbSaveHook<SmartDbContext, TEntity>
        where TEntity : class
    {
    }

    public abstract class DbSaveHook<TEntity> : DbSaveHook<SmartDbContext, TEntity>
        where TEntity : class
    {
    }

    [CheckTables("Customer", "Discount", "Order", "Product", "ShoppingCartItem", "QueuedEmailAttachment", "ExportProfile")]
    public partial class SmartDbContext : HookingDbContext
    {
        public SmartDbContext(DbContextOptions<SmartDbContext> options)
            : base(options)
        {
        }

        protected SmartDbContext(DbContextOptions options)
            : base(options)
        {
        }

        [SuppressMessage("Performance", "CA1822:Member can be static", Justification = "Seriously?")]
        public DbQuerySettings QuerySettings
        {
            get => EngineContext.Current.Scope.ResolveOptional<DbQuerySettings>() ?? DbQuerySettings.Default;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            // For installation only:
            // The connection string may change during installation attempts. 
            // Refresh the connection string in the underlying factory in that case.

            if (!builder.IsConfigured || DataSettings.DatabaseIsInstalled())
            {
                return;
            }

            var attemptedConString = DataSettings.Instance.ConnectionString;
            if (attemptedConString.IsEmpty())
            {
                return;
            }

            var extension = builder.Options.FindExtension<DbFactoryOptionsExtension>();
            if (extension == null)
            {
                return;
            }

            var currentConString = extension.ConnectionString;
            if (currentConString == null)
            {
                // No database creation attempt yet
                var asyncState = EngineContext.Current.Application.Services.ResolveOptional<IAsyncState>();

                UpdateExtension(attemptedConString, asyncState?.Get<InstallationResult>()?.Model);
           }
            else
            {
                // At least one database creation attempt
                if (attemptedConString != currentConString)
                {
                    // ConString changed. Refresh!
                    UpdateExtension(attemptedConString, null);
                }

                DataSettings.Instance.DbFactory?.ConfigureDbContext(builder, attemptedConString);
            }

            void UpdateExtension(string conString, InstallationModel model)
            {
                extension.ConnectionString = conString;

                if (model != null && model.UseCustomCollation)
                {
                    extension.Collation = model.Collation;
                }
                
                ((IDbContextOptionsBuilderInfrastructure)builder).AddOrUpdateExtension(extension);
            }
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            DataSettings.Instance.DbFactory?.ConfigureModelConventions(configurationBuilder);
            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DataSettings.Instance.DbFactory?.CreateModel(modelBuilder);

            var options = this.Options.FindExtension<DbFactoryOptionsExtension>();
            
            if (options.DefaultSchema.HasValue())
            {
                modelBuilder.HasDefaultSchema(options.DefaultSchema);
            }

            if (options.Collation.HasValue())
            {
                modelBuilder.UseCollation(options.Collation);
            }

            CreateModel(modelBuilder, options.ModelAssemblies);

            base.OnModelCreating(modelBuilder);
        }
    }
}
