// ***********************************************************************
// Assembly         : VanityURLs
// Author           : Jonathan Sheely
// Created          : 01-10-2013
//
// Last Modified By : Jonathan Sheely
// Last Modified On : 02-06-2013
// ***********************************************************************
// <copyright file="VanityUrlsWS.asmx.cs" company="InspectorIT">
//     Copyright (c) InspectorIT. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
        /// <summary>
        /// Gets the vanity urls.
        /// </summary>
        /// <returns>List{VanityUrlInfo}.</returns>
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

        /// <summary>
        /// Gets the vanity urls XML.
        /// </summary>
        /// <returns>List{VanityUrlInfo}.</returns>
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

        /// <summary>
        /// Saves the URL.
        /// </summary>
        /// <param name="VanityUrl">The vanity URL.</param>
        /// <returns>VanityUrlInfo.</returns>
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

        /// <summary>
        /// Deletes the URL.
        /// </summary>
        /// <param name="VanityUrl">The vanity URL.</param>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DeleteUrl(VanityUrlInfo VanityUrl)
        {
            if (ValidateAuthentication())
            {
                VanityUrlController.DeleteUrl(VanityUrl);    
            }
            
        }

        /// <summary>
        /// Gets the google tracking types.
        /// </summary>
        /// <returns>List{GoogleTrackInfo}.</returns>
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

        /// <summary>
        /// Updates the type of the google tracking.
        /// </summary>
        /// <param name="googleTrackInfo">The google track info.</param>
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

        /// <summary>
        /// Removes the type of the google tracking.
        /// </summary>
        /// <param name="googleTrackInfo">The google track info.</param>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RemoveGoogleTrackingType(GoogleTrackInfo googleTrackInfo)
        {
            if (ValidateAuthentication())
            {
                VanityUrlController.RemoveGoogleTrackingType(googleTrackInfo);
            }
        }



        /// <summary>
        /// Validates the authentication.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
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

        /// <summary>
        /// The _user info
        /// </summary>
        private UserInfo _userInfo;
        /// <summary>
        /// Gets or sets the user info.
        /// </summary>
        /// <value>The user info.</value>
        private UserInfo UserInfo
        {
            get { return _userInfo; }
            set { _userInfo = value; }
        }
    }
}
