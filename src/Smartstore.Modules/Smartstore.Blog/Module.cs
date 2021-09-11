﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Smartstore.Blog.Migrations;
using Smartstore.Core.Widgets;
using Smartstore.Engine.Modularity;
using Smartstore.Http;

namespace Smartstore.Blog
{
    internal class Module : ModuleBase, IConfigurable, IWidget
    {
        public ILogger Logger { get; set; } = NullLogger.Instance;

        public RouteInfo GetConfigurationRoute()
            => new("Settings", "Blog", new { area = "Admin" });

        // TODO: (mh) (core) Won't be called. Why???
        public WidgetInvoker GetDisplayWidget(string widgetZone, object model, int storeId)
        {
            //return new HtmlWidgetInvoker(new HtmlString("<a class='menubar-link' href='@Url.RouteUrl('Blog')'>@T('Blog')</a>"));

            return new ComponentWidgetInvoker("MenuItem", new { widgetZone, model, storeId }) { };
        }

        public string[] GetWidgetZones()
        {
            return new string[] { "header_menu_special" };
        }

        public override async Task InstallAsync(ModuleInstallationContext context)
        {
            await TrySaveSettingsAsync<BlogSettings>();
            await ImportLanguageResources();
            await TrySeedData(context);

            await base.InstallAsync(context);
        }

        private async Task TrySeedData(ModuleInstallationContext context)
        {
            if (context.SeedSampleData == null || context.SeedSampleData == true)
            {
                try
                {
                    var seeder = new BlogSampleDataSeeder(context);
                    await seeder.SeedAsync(Services.DbContext);
                }
                catch (Exception ex)
                {
                    context.Logger.Error(ex, "BlogSampleDataSeeder failed.");
                }
            }
        }

        public override async Task UninstallAsync()
        {
            await DeleteSettingsAsync<BlogSettings>();
            await DeleteLanguageResources();

            await base.UninstallAsync();
        }
    }
}
