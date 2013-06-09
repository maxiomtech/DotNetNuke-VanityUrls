// ***********************************************************************
// Assembly         : VanityURLs
// Author           : Jonathan Sheely
// License          : New BSD License (BSD)
// Created          : 12-10-2012
//
// Last Modified By : Jonathan Sheely
// Last Modified On : 02-06-2013
// ***********************************************************************
// <copyright file="Constants.cs" company="InspectorIT Inc">
//     Copyright (c) InspectorIT Inc. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace InspectorIT.VanityURLs.Components.Common
{
    /// <summary>
    /// Class Constants
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// The module cache key
        /// </summary>
        internal const string ModuleCacheKey = "InspectorIT.VanityURLs.";
        /// <summary>
        /// The links cache key
        /// </summary>
        internal const string LinksCacheKey = "{0}_Links";
        /// <summary>
        /// The tracking info cache key
        /// </summary>
        internal const string TrackingInfoCacheKey = "{0}_TrackingInfo";
        /// <summary>
        /// The db prefix
        /// </summary>
        internal const string DbPrefix = "InspectorIT_VanityURLs_";
        /// <summary>
        /// The enable caching
        /// </summary>
        public const bool EnableCaching = true;
    }
}