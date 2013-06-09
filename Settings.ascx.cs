// ***********************************************************************
// Assembly         : VanityURLs
// Author           : Jonathan Sheely
// Created          : 01-17-2013
//
// Last Modified By : Jonathan Sheely
// Last Modified On : 02-05-2013
// ***********************************************************************
// <copyright file="Settings.ascx.cs" company="InspectorIT">
//     Copyright (c) InspectorIT. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using InspectorIT.VanityURLs.Components.Common;

namespace InspectorIT.VanityURLs
{
    /// <summary>
    /// Class Settings
    /// </summary>
    public partial class Settings : DotNetNuke.Entities.Modules.ModuleSettingsBase
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            BindButtons();
        }

        /// <summary>
        /// Binds the buttons.
        /// </summary>
        private void BindButtons()
        {
            if (Utils.IsIfinityUrlModuleInstalled())
            {
                btnToggleIfinityProvider.Text = "Disable iFinity Provider";
            }
            else
            {
                btnToggleIfinityProvider.Text = "Enable iFinity Provider";
            }

            if (Utils.IsStandardUrlModuleInstalled())
            {
                btnToggleStandardProvider.Text = "Disable Standard Provider";
            }
            else
            {
                btnToggleStandardProvider.Text = "Enable Standard Provider";
            }
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        public override void LoadSettings()
        {
            base.LoadSettings();
        }

        /// <summary>
        /// Updates the settings.
        /// </summary>
        public override void UpdateSettings()
        {
            base.UpdateSettings();
        }


        /// <summary>
        /// Handles the Click event of the btnToggleIfinityProvider control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnToggleIfinityProvider_Click(object sender, EventArgs e)
        {
            if (Utils.IsIfinityUrlModuleInstalled())
            {
                Utils.RemoveiFinityModule();
            }
            else
            {
                Utils.AddIfinityUrlConfiguration();
            }
            BindButtons();
            
        }

        /// <summary>
        /// Handles the Click event of the btnToggleStandardProvider control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnToggleStandardProvider_Click(object sender, EventArgs e)
        {
            if (Utils.IsStandardUrlModuleInstalled())
            {
                Utils.RemoveStandardUrlModule();
            }
            else
            {
                Utils.AddStandardUrlConfiguration();
            }
            BindButtons();
        }
    }
}