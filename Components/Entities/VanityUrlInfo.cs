// ***********************************************************************
// Assembly         : VanityURLs
// Author           : Jonathan Sheely
// License          : New BSD License (BSD)
// Created          : 12-10-2012
//
// Last Modified By : Jonathan Sheely
// Last Modified On : 02-06-2013
// ***********************************************************************
// <copyright file="VanityUrlInfo.cs" company="InspectorIT Inc">
//     Copyright (c) InspectorIT Inc. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Users;
using DotNetNuke.Common.Utilities;
namespace InspectorIT.VanityURLs.Components.Entities
{
    /// <summary>
    /// Class VanityUrlInfo
    /// </summary>
    public class VanityUrlInfo : IHydratable
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the vanity URL.
        /// </summary>
        /// <value>The vanity URL.</value>
        public string VanityUrl { get; set; }
        /// <summary>
        /// Gets or sets the redirect URL.
        /// </summary>
        /// <value>The redirect URL.</value>
        public string RedirectUrl { get; set; }
        /// <summary>
        /// Gets or sets the active start date.
        /// </summary>
        /// <value>The active start date.</value>
        public DateTime? ActiveStartDate { get; set; }
        /// <summary>
        /// Gets or sets the active end date.
        /// </summary>
        /// <value>The active end date.</value>
        public DateTime? ActiveEndDate { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the created on date.
        /// </summary>
        /// <value>The created on date.</value>
        public DateTime? CreatedOnDate { get; set; }
        /// <summary>
        /// Gets or sets the created by user id.
        /// </summary>
        /// <value>The created by user id.</value>
        public int CreatedByUserId { get; set; }
        /// <summary>
        /// Gets or sets the modified on date.
        /// </summary>
        /// <value>The modified on date.</value>
        public DateTime? ModifiedOnDate { get; set; }
        /// <summary>
        /// Gets or sets the modified by user id.
        /// </summary>
        /// <value>The modified by user id.</value>
        public int ModifiedByUserId { get; set; }
        /// <summary>
        /// Gets or sets the last accessed date.
        /// </summary>
        /// <value>The last accessed date.</value>
        public DateTime? LastAccessedDate { get; set; }

        /// <summary>
        /// Gets the name of the modified by user.
        /// </summary>
        /// <value>The name of the modified by user.</value>
        public string ModifiedByUserName
        {
            get { return UserController.GetUserById(Globals.GetPortalSettings().PortalId, ModifiedByUserId).DisplayName; }
        }
        /// <summary>
        /// Gets the name of the created by user.
        /// </summary>
        /// <value>The name of the created by user.</value>
        public string CreatedByUserName
        {
            get { return UserController.GetUserById(Globals.GetPortalSettings().PortalId, CreatedByUserId).DisplayName; }
        }


        /// <summary>
        /// Fills the specified dr.
        /// </summary>
        /// <param name="dr">The dr.</param>
        public void Fill(System.Data.IDataReader dr)
        {
            ID = Null.SetNullInteger(dr["ID"]);
            VanityUrl = Null.SetNullString(dr["VanityUrl"]);
            RedirectUrl = Null.SetNullString(dr["RedirectUrl"]);
            ActiveStartDate = dr["ActiveStartDate"] == null ? null : dr["ActiveStartDate"] as DateTime?; //Null.SetNullDateTime(dr["ActiveStartDate"]);
            ActiveEndDate = dr["ActiveEndDate"] == null ? null : dr["ActiveEndDate"] as DateTime?; //Null.SetNullDateTime(dr["ActiveEndDate"]);
            Description = Null.SetNullString(dr["Description"]);
            CreatedOnDate = dr["CreatedOnDate"] == null ? null : dr["CreatedOnDate"] as DateTime?; //Null.SetNullDateTime(dr["CreatedOnDate"]);
            CreatedByUserId = Null.SetNullInteger(dr["CreatedByUserId"]);
            ModifiedOnDate = dr["ModifiedOnDate"] == null ? null : dr["ModifiedOnDate"] as DateTime?; //Null.SetNullDateTime(dr["ModifiedOnDate"]);
            ModifiedByUserId = Null.SetNullInteger(dr["ModifiedByUserId"]);
            LastAccessedDate = dr["LastAccessedDate"] == null ? null : dr["LastAccessedDate"] as DateTime?; //Null.SetNullDateTime(dr["LastAccessedDate"]);
        }

        /// <summary>
        /// Gets or sets the key ID.
        /// </summary>
        /// <value>The key ID.</value>
        public int KeyID
        {
            get { return ID; }
            set { ID = value; }
        }
    }
}
