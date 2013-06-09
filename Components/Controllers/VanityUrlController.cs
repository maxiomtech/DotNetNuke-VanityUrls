// ***********************************************************************
// Assembly         : VanityURLs
// Author           : Jonathan Sheely
// License          : New BSD License (BSD)
// Created          : 12-10-2012
//
// Last Modified By : Jonathan Sheely
// Last Modified On : 02-06-2013
// ***********************************************************************
// <copyright file="VanityUrlController.cs" company="InspectorIT Inc">
//     Copyright (c) InspectorIT Inc. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


using System;
using System.Collections.Generic;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Data;
using DotNetNuke.Entities.Host;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using InspectorIT.VanityURLs.Components.Common;
using InspectorIT.VanityURLs.Components.Entities;
using System.Linq;
namespace InspectorIT.VanityURLs.Components.Controllers
{
    /// <summary>
    /// Class VanityUrlController
    /// </summary>
    public static class VanityUrlController
    {
        /// <summary>
        /// Gets the vanity UR ls.
        /// </summary>
        /// <returns>List{VanityUrlInfo}.</returns>
        public static List<VanityUrlInfo> GetVanityURLs()
        {
            var cache = DataCache.GetCache(Constants.ModuleCacheKey + string.Format(Constants.LinksCacheKey,PortalSettings.Current.PortalId)) as List<VanityUrlInfo>;
            if(cache==null)
            {
                var timeOut = Convert.ToInt32(Host.PerformanceSetting);
                cache = CBO.FillCollection<VanityUrlInfo>(DataProvider.Instance().ExecuteReader(Constants.DbPrefix + "Get_Urls", PortalSettings.Current.PortalId));
                if (timeOut > 0 & cache != null)
                {
                    DataCache.SetCache(Constants.ModuleCacheKey + string.Format(Constants.LinksCacheKey, PortalSettings.Current.PortalId), cache, TimeSpan.FromMinutes(timeOut));
                }
            }
            return cache;
        }

        /// <summary>
        /// Gets the vanity UR ls.
        /// </summary>
        /// <param name="portalid">The portalid.</param>
        /// <returns>List{VanityUrlInfo}.</returns>
        public static List<VanityUrlInfo> GetVanityURLs(int portalid)
        {
            var cache = DataCache.GetCache(Constants.ModuleCacheKey + string.Format(Constants.LinksCacheKey, portalid)) as List<VanityUrlInfo>;
            if (cache == null)
            {
                var timeOut = Convert.ToInt32(Host.PerformanceSetting);
                cache = CBO.FillCollection<VanityUrlInfo>(DataProvider.Instance().ExecuteReader(Constants.DbPrefix + "Get_Urls", portalid));
                if (timeOut > 0 & cache != null)
                {
                    DataCache.SetCache(Constants.ModuleCacheKey + string.Format(Constants.LinksCacheKey, portalid), cache, TimeSpan.FromMinutes(timeOut));
                }
            }
            return cache;
        }

        /// <summary>
        /// Saves the URL.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <returns>VanityUrlInfo.</returns>
        public static VanityUrlInfo SaveUrl(VanityUrlInfo v)
        {
            v.ModifiedOnDate = DateTime.Now;
            v.ModifiedByUserId = UserController.GetCurrentUserInfo().UserID;
            v = CBO.FillObject<VanityUrlInfo>(
                        DataProvider.Instance()
                                    .ExecuteReader(Constants.DbPrefix + "Update_Url", v.ID, PortalSettings.Current.PortalId, v.VanityUrl, v.RedirectUrl,
                                                   v.Description, v.ActiveStartDate, v.ActiveEndDate, v.ModifiedByUserId,
                                                   v.ModifiedOnDate));
            UpdateCache(v,PortalSettings.Current.PortalId, true);
            return v;

        }

        /// <summary>
        /// Deletes the URL.
        /// </summary>
        /// <param name="v">The v.</param>
        public static void DeleteUrl(VanityUrlInfo v)
        {
            DataProvider.Instance().ExecuteNonQuery(Constants.DbPrefix + "Delete_Url", v.ID);
            UpdateCache(v,PortalSettings.Current.PortalId);
        }

        /// <summary>
        /// Updates the last accessed date.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <param name="PortalId">The portal id.</param>
        public static void UpdateLastAccessedDate(VanityUrlInfo v, int PortalId)
        {
            DataProvider.Instance().ExecuteNonQuery(Constants.DbPrefix + "Update_LastAccessed", v.ID);
            v.LastAccessedDate = DateTime.Now;
            UpdateCache(v, PortalId, true);

          
        }


        /// <summary>
        /// Gets the google tracking types.
        /// </summary>
        /// <returns>List{GoogleTrackInfo}.</returns>
        public static List<GoogleTrackInfo> GetGoogleTrackingTypes()
        {
            List<GoogleTrackInfo> cache = DataCache.GetCache(Constants.ModuleCacheKey + string.Format(Constants.TrackingInfoCacheKey, PortalSettings.Current.PortalId)) as List<GoogleTrackInfo>;
            if (cache == null)
            {
                var timeOut = Convert.ToInt32(Host.PerformanceSetting);
                cache = CBO.FillCollection<GoogleTrackInfo>(DataProvider.Instance().ExecuteReader(Constants.DbPrefix + "Get_TrackingInfo", PortalSettings.Current.PortalId));
                cache = cache.OrderBy(x => x.UTM_Type).ToList();
                if (timeOut > 0 & cache != null)
                {
                    DataCache.SetCache(Constants.ModuleCacheKey + string.Format(Constants.TrackingInfoCacheKey, PortalSettings.Current.PortalId), cache, TimeSpan.FromMinutes(timeOut));
                }
            }
            return cache;
        }

        /// <summary>
        /// Removes the type of the google tracking.
        /// </summary>
        /// <param name="googletrackInfo">The googletrack info.</param>
        public static void RemoveGoogleTrackingType(GoogleTrackInfo googletrackInfo)
        {
            List<GoogleTrackInfo> cache = DataCache.GetCache(Constants.ModuleCacheKey + string.Format(Constants.TrackingInfoCacheKey, PortalSettings.Current.PortalId)) as List<GoogleTrackInfo>;
            if (cache != null)
            {
                cache.RemoveAll(x => x.UTM_Type == googletrackInfo.UTM_Type && x.Value == googletrackInfo.Value);
                var timeOut = Convert.ToInt32(Host.PerformanceSetting);
                DataCache.SetCache(Constants.ModuleCacheKey + string.Format(Constants.TrackingInfoCacheKey, PortalSettings.Current.PortalId), cache, TimeSpan.FromMinutes(timeOut));
            }
            DataProvider.Instance().ExecuteNonQuery(Constants.DbPrefix + "Delete_TrackingInfo", googletrackInfo.UTM_Type, googletrackInfo.Value, PortalSettings.Current.PortalId);

        }

        /// <summary>
        /// Updates the type of the google tracking.
        /// </summary>
        /// <param name="googletrackInfo">The googletrack info.</param>
        public static void UpdateGoogleTrackingType(GoogleTrackInfo googletrackInfo)
        {
            List<GoogleTrackInfo> cache = DataCache.GetCache(Constants.ModuleCacheKey + string.Format(Constants.TrackingInfoCacheKey, PortalSettings.Current.PortalId)) as List<GoogleTrackInfo>;
            if (cache != null)
            {
                cache.Add(googletrackInfo);
                var timeOut = Convert.ToInt32(Host.PerformanceSetting);
                DataCache.SetCache(Constants.ModuleCacheKey + string.Format(Constants.TrackingInfoCacheKey, PortalSettings.Current.PortalId), cache, TimeSpan.FromMinutes(timeOut));
            }
            DataProvider.Instance().ExecuteNonQuery(Constants.DbPrefix + "Update_TrackingInfo", googletrackInfo.UTM_Type, googletrackInfo.Value, PortalSettings.Current.PortalId);
        }

        /// <summary>
        /// Updates the cache.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <param name="PortalId">The portal id.</param>
        /// <param name="add">if set to <c>true</c> [add].</param>
        private static void UpdateCache(VanityUrlInfo v, int PortalId, bool add = false)
        {
            List<VanityUrlInfo> cache = DataCache.GetCache(Constants.ModuleCacheKey + string.Format(Constants.LinksCacheKey, PortalId)) as List<VanityUrlInfo>;
            if (cache != null)
            {
                cache.RemoveAll(x => x.ID == v.ID);
                if(add)
                    cache.Add(v);
                var timeOut = Convert.ToInt32(Host.PerformanceSetting);
                DataCache.SetCache(Constants.ModuleCacheKey + string.Format(Constants.LinksCacheKey, PortalId), cache, TimeSpan.FromMinutes(timeOut));
            }
        }

    }
}
