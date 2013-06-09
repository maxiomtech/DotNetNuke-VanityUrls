// ***********************************************************************
// Assembly         : VanityURLs
// Author           : Jonathan Sheely
// License          : New BSD License (BSD)
// Created          : 01-17-2013
//
// Last Modified By : Jonathan Sheely
// Last Modified On : 02-06-2013
// ***********************************************************************
// <copyright file="UrlModule.cs" company="InspectorIT Inc">
//     Copyright (c) InspectorIT Inc. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Web;
using DotNetNuke.Entities.Portals;
using InspectorIT.VanityURLs.Components.Controllers;
using InspectorIT.VanityURLs.Components.Entities;

namespace InspectorIT.VanityURLs.Components.Modules
{
    /// <summary>
    /// Class UrlModule
    /// </summary>
    public class UrlModule : IHttpModule
    {

        #region IHttpModule Members

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule" />.
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication" /> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnBeginRequest;
        }

        #endregion

        /// <summary>
        /// Called when [begin request].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        public void OnBeginRequest(Object source, EventArgs e)
        {
            try
            {
                var app = (HttpApplication)source;
                var context = app.Context;
                var myAlias = DotNetNuke.Common.Globals.GetDomainName(app.Request, true);
                var slashIndex = myAlias.LastIndexOf('/');
                myAlias = slashIndex > 1 ? myAlias.Substring(0, slashIndex) : "";

                bool doRedirect = false;
                string redirectLocation = "";//set blank location
                string requestUri = context.Request.Url.AbsoluteUri;
                string scheme = context.Request.Url.Scheme;

                string urlWithoutAlias = requestUri.Replace(myAlias, "").Replace(scheme + "://", "");
                if (urlWithoutAlias.StartsWith("/"))
                {
                    urlWithoutAlias = urlWithoutAlias.Substring(1);
                }

                //Check Database / Cache for the key.
                //Redirect if key found.
                int portalId = PortalAliasController.GetPortalAliasInfo(myAlias).PortalID;

                foreach (VanityUrlInfo urlInfo in VanityUrlController.GetVanityURLs(portalId))
                {
                    if (urlInfo.VanityUrl.ToLower() == urlWithoutAlias.ToLower())
                    {
                        if ((urlInfo.ActiveStartDate == null || urlInfo.ActiveStartDate < DateTime.Now) &&
                            (urlInfo.ActiveEndDate == null || urlInfo.ActiveEndDate > DateTime.Now))
                        {


                            doRedirect = true;
                            redirectLocation = scheme + "://" + myAlias + urlInfo.RedirectUrl;
                            //TODO: Test this. Not sure it actaully works
                            VanityUrlController.UpdateLastAccessedDate(urlInfo, portalId);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (doRedirect)
                {
                    context.Response.Redirect(redirectLocation);
                }

                
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
            }
        }

    }
}