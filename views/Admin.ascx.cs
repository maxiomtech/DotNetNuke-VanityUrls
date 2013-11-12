using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Application;
using DotNetNuke.Common;
using DotNetNuke.Framework;
using DotNetNuke.Web.Client;
using DotNetNuke.Web.Client.ClientResourceManagement;
using InspectorIT.VanityURLs.Components.Common;

namespace InspectorIT.VanityURLs.views
{
    public partial class Admin : CustomModuleBase
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            jQuery.RegisterJQuery(this.Page);
            jQuery.RegisterJQueryUI(this.Page);

            //if (DotNetNukeContext.Current.Application.Version.Major >= 7)
            //{
            //    ClientResourceManager.RegisterScript(Page, ControlPath + "../js/jquery.autocomplete.js", FileOrder.Js.DefaultPriority + 1);
            //}
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserId != -1)
            {
                phCanEdit.Visible = true;
            }
        }

        public string servicePath
        {
            get { return VirtualPathUtility.ToAbsolute(ControlPath.Replace("/views","") + "VanityUrlsWS.asmx"); }
        }
    }
}