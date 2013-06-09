using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using InspectorIT.VanityURLs.Components.Entities;
using InspectorIT.VanityURLs.Components.Controllers;

namespace InspectorIT.VanityURLs
{
    /// <summary>
    /// Summary description for VanityUrlsWS
    /// </summary>
    [WebService(Namespace = "http://inspectorit.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class VanityUrlsWS : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<VanityUrlInfo> GetVanityUrls()
        {
            if (ValidateAuthentication())
            {
                return VanityUrlController.GetVanityURLs();    
            }
            return null;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml, UseHttpGet = true)]
        public List<VanityUrlInfo> GetVanityUrlsXml()
        {
            if (ValidateAuthentication())
            {
                return VanityUrlController.GetVanityURLs();
            }
            return null;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public VanityUrlInfo SaveUrl(VanityUrlInfo VanityUrl)
        {
            if (ValidateAuthentication())
            {
                return VanityUrlController.SaveUrl(VanityUrl);    
            }
            return null;

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DeleteUrl(VanityUrlInfo VanityUrl)
        {
            if (ValidateAuthentication())
            {
                VanityUrlController.DeleteUrl(VanityUrl);    
            }
            
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<GoogleTrackInfo> GetGoogleTrackingTypes()
        {
            if (ValidateAuthentication())
            {
                return VanityUrlController.GetGoogleTrackingTypes();    
            }
            return null;

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void UpdateGoogleTrackingType(GoogleTrackInfo googleTrackInfo)
        {
            if (ValidateAuthentication())
            {
                if (googleTrackInfo.Value != "")
                    VanityUrlController.UpdateGoogleTrackingType(googleTrackInfo);
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RemoveGoogleTrackingType(GoogleTrackInfo googleTrackInfo)
        {
            if (ValidateAuthentication())
            {
                VanityUrlController.RemoveGoogleTrackingType(googleTrackInfo);
            }
        }

        

        private bool ValidateAuthentication()
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                UserInfo user = UserController.GetUserByName(PortalSettings.Current.PortalId,
                                                             HttpContext.Current.User.Identity.Name);
                HttpContext.Current.Items["UserInfo"] = user;
                if (user != null && user.UserID!=-1)
                {
                    if (user.IsInRole("Administrators") || user.IsSuperUser)
                        UserInfo = user;
                    else return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;

        }

        private UserInfo _userInfo;
        private UserInfo UserInfo
        {
            get { return _userInfo; }
            set { _userInfo = value; }
        }
    }
}
