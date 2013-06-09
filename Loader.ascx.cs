// ***********************************************************************
// Assembly         : VanityURLs
// Author           : Jonathan Sheely
// Created          : 12-10-2012
//
// Last Modified By : Jonathan Sheely
// Last Modified On : 01-10-2013
// ***********************************************************************
// <copyright file="Loader.ascx.cs" company="InspectorIT">
//     Copyright (c) InspectorIT. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using InspectorIT.VanityURLs.Components.Common;

namespace InspectorIT.VanityURLs
{
    /// <summary>
    /// Class Loader
    /// </summary>
    public partial class Loader : CustomModuleBase
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            var adminControl = LoadControl("~/DesktopModules/InspectorIT/VanityUrls/views/Admin.ascx") as views.Admin;
            adminControl.ModuleConfiguration = this.ModuleConfiguration;
            phOutput.Controls.Add(adminControl);
        }
    }
}