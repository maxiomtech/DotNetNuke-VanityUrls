// ***********************************************************************
// Assembly         : VanityURLs
// Author           : Jonathan Sheely
// License          : New BSD License (BSD)
// Created          : 01-17-2013
//
// Last Modified By : Jonathan Sheely
// Last Modified On : 02-06-2013
// ***********************************************************************
// <copyright file="Utils.cs" company="InspectorIT Inc">
//     Copyright (c) InspectorIT Inc. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.IO;
using System.Web;
using System.Xml;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Services.Installer;

namespace InspectorIT.VanityURLs.Components.Common
{
    /// <summary>
    /// Class Utils
    /// </summary>
    public class Utils
    {

        /// <summary>
        /// Removeis the finity module.
        /// </summary>
        public static void RemoveiFinityModule()
        {
            try
            {
                string configString = "";
                using (var stream = new StreamReader(Path.Combine(HttpContext.Current.Server.MapPath("~"),
                                                     "DesktopModules/InspectorIT/VanityUrls/Includes/iFinityProvider.remove.config"))
                    )
                {
                    configString = stream.ReadToEnd();
                }

                var targetConfig = new XmlDocument();
                targetConfig.Load(Path.Combine(HttpContext.Current.Server.MapPath("~"), "web.config"));

                var merge = new XmlMerge(new StringReader(configString), "", "");
                merge.UpdateConfig(targetConfig);
                Config.Save(targetConfig, "web.config");

            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
            }
        }

        /// <summary>
        /// Removes the standard URL module.
        /// </summary>
        public static void RemoveStandardUrlModule()
        {
            try
            {
                string configString = "";
                using (var stream = new StreamReader(Path.Combine(HttpContext.Current.Server.MapPath("~"),
                                                     "DesktopModules/InspectorIT/VanityUrls/Includes/StandardProvider.remove.config"))
                    )
                {
                    configString = stream.ReadToEnd();
                }

                var targetConfig = new XmlDocument();
                targetConfig.Load(Path.Combine(HttpContext.Current.Server.MapPath("~"), "web.config"));

                var merge = new XmlMerge(new StringReader(configString), "", "");
                merge.UpdateConfig(targetConfig);
                Config.Save(targetConfig, "web.config");

            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
            }
        }


        /// <summary>
        /// Adds the standard URL configuration.
        /// </summary>
        public static void AddStandardUrlConfiguration()
        {
            try
            {
                string configString = "";
                using (var stream = new StreamReader(Path.Combine(HttpContext.Current.Server.MapPath("~"),
                                                     "DesktopModules/InspectorIT/VanityUrls/Includes/StandardProvider.add.config"))
                    )
                {
                    configString = stream.ReadToEnd();
                }

                var targetConfig = new XmlDocument();
                targetConfig.Load(Path.Combine(HttpContext.Current.Server.MapPath("~"), "web.config"));

                var merge = new XmlMerge(new StringReader(configString), "", "");
                merge.UpdateConfig(targetConfig);
                Config.Save(targetConfig, "web.config");

            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
            }
        }

        /// <summary>
        /// Adds the ifinity URL configuration.
        /// </summary>
        public static void AddIfinityUrlConfiguration()
        {
            try
            {

                var targetConfig = new XmlDocument();
                targetConfig.Load(Path.Combine(HttpContext.Current.Server.MapPath("~"), "web.config"));

                string configString = "";

                using (var stream = new StreamReader(Path.Combine(HttpContext.Current.Server.MapPath("~"),
                                                     "DesktopModules/InspectorIT/VanityUrls/Includes/iFinityProvider.add.config"))
                    )
                {
                    configString = stream.ReadToEnd();
                }


                var merge = new XmlMerge(new StringReader(configString), "", "");
                merge.UpdateConfig(targetConfig);
                Config.Save(targetConfig, "web.config");


            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
            }
        }

        /// <summary>
        /// Determines whether [is ifinity URL module installed].
        /// </summary>
        /// <returns><c>true</c> if [is ifinity URL module installed]; otherwise, <c>false</c>.</returns>
        public static bool IsIfinityUrlModuleInstalled()
        {
            
            var targetConfig = new XmlDocument();
            targetConfig.Load(Path.Combine(HttpContext.Current.Server.MapPath("~"), "web.config"));

            bool isInstalled = targetConfig.SelectSingleNode("/configuration/dotnetnuke/moduleFriendlyUrl/providers/add[@name='InspectorIT.VanityURLs']") != null;

            return Convert.ToBoolean(isInstalled);
        }



        /// <summary>
        /// Determines whether [is standard URL module installed].
        /// </summary>
        /// <returns><c>true</c> if [is standard URL module installed]; otherwise, <c>false</c>.</returns>
        public static bool IsStandardUrlModuleInstalled()
        {
            var targetConfig = new XmlDocument();
            targetConfig.Load(Path.Combine(HttpContext.Current.Server.MapPath("~"), "web.config"));

            bool isInstalled = targetConfig.SelectSingleNode("/configuration/system.webServer/modules/add[@name='VanityURLs']") != null;

            return Convert.ToBoolean(isInstalled);
        }
    }
}