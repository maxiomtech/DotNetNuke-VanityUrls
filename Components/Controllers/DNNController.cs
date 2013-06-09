// ***********************************************************************
// Assembly         : VanityURLs
// Author           : Jonathan Sheely
// License          : New BSD License (BSD)
// Created          : 02-04-2013
//
// Last Modified By : Jonathan Sheely
// Last Modified On : 02-06-2013
// ***********************************************************************
// <copyright file="DNNController.cs" company="InspectorIT Inc">
//     Copyright (c) InspectorIT Inc. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Definitions;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Security.Permissions;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Log.EventLog;

namespace InspectorIT.VanityURLs.Components.Controllers
{
    /// <summary>
    /// Class DNNController
    /// </summary>
    public class DNNController : IUpgradeable
    {

        /// <summary>
        /// Upgrades the module.
        /// </summary>
        /// <param name="Version">The version.</param>
        /// <returns>System.String.</returns>
        public string UpgradeModule(string Version)
        {
            TabController tabs = new TabController();
            foreach (PortalInfo p in new PortalController().GetPortals())
            {
                TabInfo tabInfo = tabs.GetTabByName("Vanity Urls", p.PortalID);
                if (tabInfo == null)
                {

                    tabInfo = new TabInfo();
                    tabInfo.TabID = -1;
                    tabInfo.ParentId = tabs.GetTabByName("Admin", p.PortalID).TabID;
                    tabInfo.PortalID = p.PortalID;
                    tabInfo.TabName = "Vanity Urls";
                    try
                    {
                        int tabId = tabs.AddTab(tabInfo);
                        AddModuleToPage(p.PortalID, tabId); 
                        return "Vanity Urls page added";
                    }
                    catch (Exception ex)
                    {
                        DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
                    }
                }
            }

            return "";
        }
        /// <summary>
        /// Adds the module to page.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <param name="tabId">The tab id.</param>
        /// <exception cref="System.ArgumentException">desktopModuleId</exception>
        private void AddModuleToPage(int portalId, int tabId)
        {

            TabPermissionCollection objTabPermissions = new TabController().GetTab(tabId, portalId, false).TabPermissions;
            var objPermissionController = new PermissionController();
            var objModules = new ModuleController();
            new EventLogController();

            int desktopModuleId =
                DesktopModuleController.GetDesktopModuleByFriendlyName("VanityURLs").DesktopModuleID;

            try
            {
                DesktopModuleInfo desktopModule;
                if (!DesktopModuleController.GetDesktopModules(portalId).TryGetValue(desktopModuleId, out desktopModule))
                {
                    throw new ArgumentException("desktopModuleId");
                }
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }

            foreach (ModuleDefinitionInfo objModuleDefinition in
                ModuleDefinitionController.GetModuleDefinitionsByDesktopModuleID(desktopModuleId).Values)
            {
                var objModule = new ModuleInfo();
                objModule.PortalID = portalId;
                objModule.TabID = tabId;
                objModule.ModuleOrder = 0;
                objModule.ModuleTitle = objModuleDefinition.FriendlyName;
                objModule.PaneName = "ContentPane";
                objModule.ModuleDefID = objModuleDefinition.ModuleDefID;
                objModule.InheritViewPermissions = true;               
                objModule.CultureCode = Null.NullString;
                objModule.AllTabs = false;
                objModules.AddModule(objModule);
            }
        }
    }

    
}