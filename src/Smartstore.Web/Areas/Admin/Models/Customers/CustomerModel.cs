﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Smartstore.Collections;
using Smartstore.Core.Security;
using Smartstore.Web.Modelling;
using Smartstore.Web.Models.Common;

namespace Smartstore.Admin.Models.Customers
{
    [LocalizedDisplay("Admin.Customers.Customers.Fields.")]
    public class CustomerModel : TabbableModel
    {
        public bool AllowUsersToChangeUsernames { get; set; }
        public bool UsernamesEnabled { get; set; }

        [LocalizedDisplay("*Username")]
        public string Username { get; set; }

        [LocalizedDisplay("*Email")]
        public string Email { get; set; }

        [LocalizedDisplay("*Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [LocalizedDisplay("*Title")]
        public string Title { get; set; }
        public bool TitleEnabled { get; set; }

        public bool GenderEnabled { get; set; }
        [LocalizedDisplay("*Gender")]
        public string Gender { get; set; }

        [LocalizedDisplay("*FirstName")]
        public string FirstName { get; set; }

        [LocalizedDisplay("*LastName")]
        public string LastName { get; set; }

        [LocalizedDisplay("*FullName")]
        public string FullName { get; set; }

        public bool DateOfBirthEnabled { get; set; }
        [LocalizedDisplay("*DateOfBirth")]
        public DateTime? DateOfBirth { get; set; }

        public bool CompanyEnabled { get; set; }

        [LocalizedDisplay("*Company")]
        public string Company { get; set; }

        public bool CustomerNumberEnabled { get; set; }

        [LocalizedDisplay("Account.Fields.CustomerNumber")]
        public string CustomerNumber { get; set; }

        public bool StreetAddressEnabled { get; set; }

        [LocalizedDisplay("*StreetAddress")]
        public string StreetAddress { get; set; }

        public bool StreetAddress2Enabled { get; set; }

        [LocalizedDisplay("*StreetAddress2")]
        public string StreetAddress2 { get; set; }

        public bool ZipPostalCodeEnabled { get; set; }

        [LocalizedDisplay("*ZipPostalCode")]
        public string ZipPostalCode { get; set; }

        public bool CityEnabled { get; set; }

        [LocalizedDisplay("*City")]
        public string City { get; set; }

        public bool CountryEnabled { get; set; }

        [LocalizedDisplay("*Country")]
        public int CountryId { get; set; }

        public bool StateProvinceEnabled { get; set; }

        [LocalizedDisplay("*StateProvince")]
        public int StateProvinceId { get; set; }

        public bool PhoneEnabled { get; set; }

        [LocalizedDisplay("*Phone")]
        public string Phone { get; set; }

        public bool FaxEnabled { get; set; }

        [LocalizedDisplay("*Fax")]
        public string Fax { get; set; }

        [LocalizedDisplay("*AdminComment")]
        public string AdminComment { get; set; }

        [LocalizedDisplay("*IsTaxExempt")]
        public bool IsTaxExempt { get; set; }

        [LocalizedDisplay("*Active")]
        public bool Active { get; set; }

        [LocalizedDisplay("*Affiliate")]
        public int AffiliateId { get; set; }
        public string AffiliateFullName { get; set; }

        [LocalizedDisplay("*TimeZoneId")]
        public string TimeZoneId { get; set; }

        public bool AllowCustomersToSetTimeZone { get; set; }

        [LocalizedDisplay("*VatNumber")]
        public string VatNumber { get; set; }

        public string VatNumberStatusNote { get; set; }

        public bool DisplayVatNumber { get; set; }

        [LocalizedDisplay("Common.CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [LocalizedDisplay("*LastActivityDate")]
        public DateTime LastActivityDate { get; set; }

        [LocalizedDisplay("*IPAddress")]
        public string LastIpAddress { get; set; }

        [LocalizedDisplay("*LastVisitedPage")]
        public string LastVisitedPage { get; set; }

        [LocalizedDisplay("*CustomerRoles")]
        public string CustomerRoleNames { get; set; }

        [UIHint("CustomerRoles")]
        [AdditionalMetadata("multiple", true)]
        [LocalizedDisplay("Admin.Customers.CustomerRoles")]
        public int[] SelectedCustomerRoleIds { get; set; }

        public bool AllowManagingCustomerRoles { get; set; }

        public bool DisplayRewardPointsHistory { get; set; }

        [LocalizedDisplay("Admin.Customers.Customers.RewardPoints.Fields.AddRewardPointsValue")]
        public int AddRewardPointsValue { get; set; }

        [LocalizedDisplay("Admin.Customers.Customers.RewardPoints.Fields.AddRewardPointsMessage")]
        public string AddRewardPointsMessage { get; set; }

        public SendEmailModel SendEmail { get; set; }
        public SendPmModel SendPm { get; set; }

        [LocalizedDisplay("Admin.Customers.Customers.AssociatedExternalAuth")]
        public IList<AssociatedExternalAuthModel> AssociatedExternalAuthRecords { get; set; }

        public bool Deleted { get; set; }
        public string EditUrl { get; set; }

        public TreeNode<IPermissionNode> PermissionTree { get; set; }
        public List<AddressModel> Addresses { get; set; } = new();


        #region Nested classes

        [LocalizedDisplay("Admin.Customers.Customers.AssociatedExternalAuth.Fields.")]
        public class AssociatedExternalAuthModel : EntityModelBase
        {
            [LocalizedDisplay("*Email")]
            public string Email { get; set; }

            [LocalizedDisplay("*ExternalIdentifier")]
            public string ExternalIdentifier { get; set; }

            [LocalizedDisplay("*AuthMethodName")]
            public string AuthMethodName { get; set; }
        }

        [LocalizedDisplay("Admin.Customers.Customers.RewardPoints.Fields.")]
        public class RewardPointsHistoryModel : EntityModelBase
        {
            [LocalizedDisplay("*Points")]
            public int Points { get; set; }

            [LocalizedDisplay("*PointsBalance")]
            public int PointsBalance { get; set; }

            [LocalizedDisplay("*Message")]
            public string Message { get; set; }

            [LocalizedDisplay("Common.CreatedOn")]
            public DateTime CreatedOn { get; set; }
        }

        [LocalizedDisplay("Admin.Customers.Customers.SendEmail.")]
        public class SendEmailModel : ModelBase
        {
            [Required]
            [LocalizedDisplay("*Subject")]
            public string Subject { get; set; }

            [Required]
            [LocalizedDisplay("*Body")]
            public string Body { get; set; }
        }

        [LocalizedDisplay("Admin.Customers.Customers.SendPM.")]
        public class SendPmModel : ModelBase
        {
            [Required]
            [LocalizedDisplay("*Subject")]
            public string Subject { get; set; }

            [Required]
            [LocalizedDisplay("*Message")]
            public string Message { get; set; }
        }

        [LocalizedDisplay("Admin.Customers.Customers.Orders.")]
        public class OrderModel : EntityModelBase
        {
            [LocalizedDisplay("*ID")]
            public override int Id { get; set; }

            [LocalizedDisplay("*OrderStatus")]
            public string OrderStatus { get; set; }

            [LocalizedDisplay("*PaymentStatus")]
            public string PaymentStatus { get; set; }

            [LocalizedDisplay("*ShippingStatus")]
            public string ShippingStatus { get; set; }

            [LocalizedDisplay("*OrderTotal")]
            public string OrderTotal { get; set; }

            [LocalizedDisplay("*Store")]
            public string StoreName { get; set; }

            [LocalizedDisplay("Common.CreatedOn")]
            public DateTime CreatedOn { get; set; }
        }

        [LocalizedDisplay("Admin.Customers.Customers.ActivityLog.")]
        public partial class ActivityLogModel : EntityModelBase
        {
            [LocalizedDisplay("*ActivityLogType")]
            public string ActivityLogTypeName { get; set; }

            [LocalizedDisplay("*Comment")]
            public string Comment { get; set; }

            [LocalizedDisplay("Common.CreatedOn")]
            public DateTime CreatedOn { get; set; }
        }

        #endregion
    }
}
