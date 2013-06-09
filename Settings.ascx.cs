using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InspectorIT.VanityURLs.Components.Common;

namespace InspectorIT.VanityURLs
{
    public partial class Settings : DotNetNuke.Entities.Modules.ModuleSettingsBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindButtons();
        }

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

        public override void LoadSettings()
        {
            base.LoadSettings();
        }

        public override void UpdateSettings()
        {
            base.UpdateSettings();
        }


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