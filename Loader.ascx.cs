using System;
using DotNetNuke.Entities.Modules;
using InspectorIT.VanityURLs.Components.Common;

namespace InspectorIT.VanityURLs
{
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