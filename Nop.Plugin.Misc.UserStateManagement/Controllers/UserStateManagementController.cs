using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Admin.Models.Customers;
using Nop.Core;
using Nop.Admin.Controllers;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Forums;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Tax;
using Nop.Plugin.Misc.UserStateManagement.Domain;
using Nop.Plugin.Misc.UserStateManagement.Model;
using Nop.Plugin.Misc.UserStateManagement.Services;
using Nop.Services.Affiliates;
using Nop.Services.Authentication.External;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.ExportImport;
using Nop.Services.Forums;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Services.Vendors;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Misc.UserStateManagement.Controllers
{
    public class UserStateManagementController : CustomerController
    {
        #region Fields
       
        private readonly ICustomerService _customerService;
        private readonly IUserStateManagementService _userStateManagementService;
        
        private readonly INewsLetterSubscriptionService _newsLetterSubscriptionService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly ICustomerReportService _customerReportService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly ILocalizationService _localizationService;
        private readonly DateTimeSettings _dateTimeSettings;
        private readonly TaxSettings _taxSettings;
        private readonly RewardPointsSettings _rewardPointsSettings;
        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IAddressService _addressService;
        private readonly CustomerSettings _customerSettings;
        private readonly ITaxService _taxService;
        private readonly IWorkContext _workContext;
        private readonly IVendorService _vendorService;
        private readonly IStoreContext _storeContext;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IOrderService _orderService;
        private readonly IExportManager _exportManager;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IPriceCalculationService _priceCalculationService;
        private readonly IProductAttributeFormatter _productAttributeFormatter;
        private readonly IPermissionService _permissionService;
        private readonly IQueuedEmailService _queuedEmailService;
        private readonly EmailAccountSettings _emailAccountSettings;
        private readonly IEmailAccountService _emailAccountService;
        private readonly ForumSettings _forumSettings;
        private readonly IForumService _forumService;
        private readonly IOpenAuthenticationService _openAuthenticationService;
        private readonly AddressSettings _addressSettings;
        private readonly IStoreService _storeService;
        private readonly ICustomerAttributeParser _customerAttributeParser;
        private readonly ICustomerAttributeService _customerAttributeService;
        private readonly IAddressAttributeParser _addressAttributeParser;
        private readonly IAddressAttributeService _addressAttributeService;
        private readonly IAddressAttributeFormatter _addressAttributeFormatter;
        private readonly IAffiliateService _affiliateService;
        private readonly IWorkflowMessageService _workflowMessageService;

        private readonly CurrencySettings _currencySettings;
        private readonly AdminAreaSettings _adminAreaSettings;

        #endregion
        
        #region Ctor

        public UserStateManagementController(
            IUserStateManagementService userStateManagement,
            ICustomerService customerService,
            INewsLetterSubscriptionService newsLetterSubscriptionService,
            IGenericAttributeService genericAttributeService,
            ICustomerRegistrationService customerRegistrationService,
            ICustomerReportService customerReportService,
            IDateTimeHelper dateTimeHelper,
            ILocalizationService localizationService,
            DateTimeSettings dateTimeSettings,
            TaxSettings taxSettings,
            RewardPointsSettings rewardPointsSettings,
            ICountryService countryService,
            IStateProvinceService stateProvinceService,
            IAddressService addressService,
            CustomerSettings customerSettings,
            ITaxService taxService,
            IWorkContext workContext,
            IVendorService vendorService,
            IStoreContext storeContext,
            IPriceFormatter priceFormatter,
            IOrderService orderService,
            IExportManager exportManager,
            ICustomerActivityService customerActivityService,
            IPriceCalculationService priceCalculationService,
            IProductAttributeFormatter productAttributeFormatter,
            IPermissionService permissionService,
            IQueuedEmailService queuedEmailService,
            EmailAccountSettings emailAccountSettings,
            IEmailAccountService emailAccountService,
            ForumSettings forumSettings,
            IForumService forumService,
            IOpenAuthenticationService openAuthenticationService,
            AddressSettings addressSettings,
            IStoreService storeService,
            ICustomerAttributeParser customerAttributeParser,
            ICustomerAttributeService customerAttributeService,
            IAddressAttributeParser addressAttributeParser,
            IAddressAttributeService addressAttributeService,
            IAddressAttributeFormatter addressAttributeFormatter,
            IAffiliateService affiliateService,
            IWorkflowMessageService workflowMessageService)
            : base(customerService, newsLetterSubscriptionService, genericAttributeService,
                customerRegistrationService, customerReportService, dateTimeHelper,
                localizationService, dateTimeSettings, taxSettings, rewardPointsSettings,
                countryService, stateProvinceService, addressService, customerSettings,
                taxService, workContext, vendorService, storeContext, priceFormatter,
                orderService, exportManager, customerActivityService, priceCalculationService,
                productAttributeFormatter, permissionService, queuedEmailService,
                emailAccountSettings, emailAccountService, forumSettings,
                forumService, openAuthenticationService, addressSettings,
                storeService, customerAttributeParser, customerAttributeService,
                addressAttributeParser, addressAttributeService, addressAttributeFormatter,
                affiliateService, workflowMessageService)
        {
            this._customerService = customerService;
            this._newsLetterSubscriptionService = newsLetterSubscriptionService;
            this._genericAttributeService = genericAttributeService;
            this._customerRegistrationService = customerRegistrationService;
            this._customerReportService = customerReportService;
            this._dateTimeHelper = dateTimeHelper;
            this._localizationService = localizationService;
            this._dateTimeSettings = dateTimeSettings;
            this._taxSettings = taxSettings;
            this._rewardPointsSettings = rewardPointsSettings;
            this._countryService = countryService;
            this._stateProvinceService = stateProvinceService;
            this._addressService = addressService;
            this._customerSettings = customerSettings;
            this._taxService = taxService;
            this._workContext = workContext;
            this._vendorService = vendorService;
            this._storeContext = storeContext;
            this._priceFormatter = priceFormatter;
            this._orderService = orderService;
            this._exportManager = exportManager;
            this._customerActivityService = customerActivityService;
            this._priceCalculationService = priceCalculationService;
            this._productAttributeFormatter = productAttributeFormatter;
            this._permissionService = permissionService;
            this._queuedEmailService = queuedEmailService;
            this._emailAccountSettings = emailAccountSettings;
            this._emailAccountService = emailAccountService;
            this._forumSettings = forumSettings;
            this._forumService = forumService;
            this._openAuthenticationService = openAuthenticationService;
            this._addressSettings = addressSettings;
            this._storeService = storeService;
            this._customerAttributeParser = customerAttributeParser;
            this._customerAttributeService = customerAttributeService;
            this._addressAttributeParser = addressAttributeParser;
            this._addressAttributeService = addressAttributeService;
            this._addressAttributeFormatter = addressAttributeFormatter;
            this._affiliateService = affiliateService;
            this._workflowMessageService = workflowMessageService;
            this._userStateManagementService = userStateManagement;
        }
        
        #endregion

        #region Utilities

        [NonAction]
        protected virtual void PrepareCustomerStateModel(CustomerStateModel model)
        {
            var stateId = _userStateManagementService.GetStateId(model.Id);
            if(stateId == -1)
                stateId = _userStateManagementService.AddOrUpdateEventStatus(model.Id, UserStatusEnum.Create);
            model.State.StateId = stateId;
            model.State.UserState = _userStateManagementService.GetUserStatus(stateId).ToString();
            model.State.State = _userStateManagementService.GetInnerStatus(stateId).ToString();
        }

        #endregion

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        [ValidateInput(false)]
        public ActionResult OverrideCustomerEdit(CustomerModel model, bool continueEditing, FormCollection form)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();

            var customer = _customerService.GetCustomerById(model.Id);
            if (customer == null || customer.Deleted)
                //No customer found with the specified id
                return Redirect("/Admin/Customer/List");

            //validate customer roles
            var allCustomerRoles = _customerService.GetAllCustomerRoles(true);
            var newCustomerRoles = new List<CustomerRole>();
            foreach (var customerRole in allCustomerRoles)
                if (model.SelectedCustomerRoleIds != null && model.SelectedCustomerRoleIds.Contains(customerRole.Id))
                    newCustomerRoles.Add(customerRole);
            var customerRolesError = ValidateCustomerRoles(newCustomerRoles);
            if (!String.IsNullOrEmpty(customerRolesError))
            {
                ModelState.AddModelError("", customerRolesError);
                ErrorNotification(customerRolesError, false);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    customer.AdminComment = model.AdminComment;
                    customer.IsTaxExempt = model.IsTaxExempt;
                    customer.Active = model.Active;
                    //email
                    if (!String.IsNullOrWhiteSpace(model.Email))
                    {
                        _customerRegistrationService.SetEmail(customer, model.Email);
                    }
                    else
                    {
                        customer.Email = model.Email;
                    }

                    //username
                    if (_customerSettings.UsernamesEnabled && _customerSettings.AllowUsersToChangeUsernames)
                    {
                        if (!String.IsNullOrWhiteSpace(model.Username))
                        {
                            _customerRegistrationService.SetUsername(customer, model.Username);
                        }
                        else
                        {
                            customer.Username = model.Username;
                        }
                    }

                    //VAT number
                    if (_taxSettings.EuVatEnabled)
                    {
                        var prevVatNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.VatNumber);

                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.VatNumber, model.VatNumber);
                        //set VAT number status
                        if (!String.IsNullOrEmpty(model.VatNumber))
                        {
                            if (!model.VatNumber.Equals(prevVatNumber, StringComparison.InvariantCultureIgnoreCase))
                            {
                                _genericAttributeService.SaveAttribute(customer,
                                    SystemCustomerAttributeNames.VatNumberStatusId,
                                    (int)_taxService.GetVatNumberStatus(model.VatNumber));
                            }
                        }
                        else
                        {
                            _genericAttributeService.SaveAttribute(customer,
                                SystemCustomerAttributeNames.VatNumberStatusId,
                                (int)VatNumberStatus.Empty);
                        }
                    }

                    //vendor
                    customer.VendorId = model.VendorId;

                    //form fields
                    if (_dateTimeSettings.AllowCustomersToSetTimeZone)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.TimeZoneId, model.TimeZoneId);
                    if (_customerSettings.GenderEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Gender, model.Gender);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.FirstName, model.FirstName);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.LastName, model.LastName);
                    if (_customerSettings.DateOfBirthEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.DateOfBirth, model.DateOfBirth);
                    if (_customerSettings.CompanyEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Company, model.Company);
                    if (_customerSettings.StreetAddressEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StreetAddress, model.StreetAddress);
                    if (_customerSettings.StreetAddress2Enabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StreetAddress2, model.StreetAddress2);
                    if (_customerSettings.ZipPostalCodeEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.ZipPostalCode, model.ZipPostalCode);
                    if (_customerSettings.CityEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.City, model.City);
                    if (_customerSettings.CountryEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.CountryId, model.CountryId);
                    if (_customerSettings.CountryEnabled && _customerSettings.StateProvinceEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StateProvinceId, model.StateProvinceId);
                    if (_customerSettings.PhoneEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Phone, model.Phone);
                    if (_customerSettings.FaxEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Fax, model.Fax);

                    //custom customer attributes
                    var customerAttributes = ParseCustomCustomerAttributes(customer, form);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.CustomCustomerAttributes, customerAttributes);


                    //customer roles
                    foreach (var customerRole in allCustomerRoles)
                    {
                        //ensure that the current customer cannot add/remove to/from "Administrators" system role
                        //if he's not an admin himself
                        if (customerRole.SystemName == SystemCustomerRoleNames.Administrators &&
                            !_workContext.CurrentCustomer.IsAdmin())
                            continue;

                        if (model.SelectedCustomerRoleIds != null &&
                            model.SelectedCustomerRoleIds.Contains(customerRole.Id))
                        {
                            //new role
                            if (customer.CustomerRoles.Count(cr => cr.Id == customerRole.Id) == 0)
                                customer.CustomerRoles.Add(customerRole);
                        }
                        else
                        {
                            //remove role
                            if (customer.CustomerRoles.Count(cr => cr.Id == customerRole.Id) > 0)
                                customer.CustomerRoles.Remove(customerRole);
                        }
                    }
                    _customerService.UpdateCustomer(customer);


                    //ensure that a customer with a vendor associated is not in "Administrators" role
                    //otherwise, he won't have access to the other functionality in admin area
                    if (customer.IsAdmin() && customer.VendorId > 0)
                    {
                        customer.VendorId = 0;
                        _customerService.UpdateCustomer(customer);
                        ErrorNotification(_localizationService.GetResource("Admin.Customers.Customers.AdminCouldNotbeVendor"));
                    }

                    //ensure that a customer in the Vendors role has a vendor account associated.
                    //otherwise, he will have access to ALL products
                    if (customer.IsVendor() && customer.VendorId == 0)
                    {
                        var vendorRole = customer
                            .CustomerRoles
                            .FirstOrDefault(x => x.SystemName == SystemCustomerRoleNames.Vendors);
                        customer.CustomerRoles.Remove(vendorRole);
                        _customerService.UpdateCustomer(customer);
                        ErrorNotification(_localizationService.GetResource("Admin.Customers.Customers.CannotBeInVendoRoleWithoutVendorAssociated"));
                    }


                    //activity log
                    _customerActivityService.InsertActivity("EditCustomer", _localizationService.GetResource("ActivityLog.EditCustomer"), customer.Id);

                    SuccessNotification(_localizationService.GetResource("Admin.Customers.Customers.Updated"));
                    if (continueEditing)
                    {
                        //selected tab
                        SaveSelectedTabIndex();

                        return RedirectToAction("OverrideCustomerEdit", new { id = customer.Id });
                    }
                    return Redirect("/Admin/Customer/List");
                }
                catch (Exception exc)
                {
                    ErrorNotification(exc.Message, false);
                }
            }


            //If we got this far, something failed, redisplay form
            PrepareCustomerModel(model, customer, true);
            //return View("", model);
            var customerStateModel = (CustomerStateModel)model;
            PrepareCustomerStateModel(customerStateModel);
            return View("~/Plugins/Misc.UserStateManagement/Views/UserStateManagement/Edit.cshtml", customerStateModel);
        }

        public ActionResult OverrideCustomerEdit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();

            var customer = _customerService.GetCustomerById(id);
            if (customer == null || customer.Deleted)
                //No customer found with the specified id
                return Redirect("/Admin/Customer/List");

            var model = new CustomerStateModel();
            PrepareCustomerModel(model, customer, false);
            PrepareCustomerStateModel(model);
            return View("~/Plugins/Misc.UserStateManagement/Views/UserStateManagement/Edit.cshtml", model);
        }

        [HttpPost]
        public ActionResult Previous(int customerId, int stateId)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();

            var customer = _customerService.GetCustomerById(customerId);
            if (customer == null || customer.Deleted)
                //No customer found with the specified id
                return Redirect("/Admin/Customer/List");

            var nextStateId = _userStateManagementService.Previous(customerId, stateId);
            
            var model = new CustomerStateChanged
            {
                LastStateId = stateId,
                CurrentStateId = nextStateId,
                CurrentState = _userStateManagementService.GetUserStatus(nextStateId).ToString(),
                InnerState = _userStateManagementService.GetInnerStatus(nextStateId).ToString(),
            };
            return Json(model);
        }

        [HttpPost]
        public ActionResult Next(int customerId, int stateId)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();

            var customer = _customerService.GetCustomerById(customerId);
            if (customer == null || customer.Deleted)
                //No customer found with the specified id
                return Redirect("/Admin/Customer/List");

            var nextStateId = _userStateManagementService.Next(customerId, stateId);

            var model = new CustomerStateChanged
            {
                LastStateId = stateId,
                CurrentStateId = nextStateId,
                CurrentState = _userStateManagementService.GetUserStatus(nextStateId).ToString(),
                InnerState = _userStateManagementService.GetInnerStatus(nextStateId).ToString(),
            };
            return Json(model);
        }
    }
}   