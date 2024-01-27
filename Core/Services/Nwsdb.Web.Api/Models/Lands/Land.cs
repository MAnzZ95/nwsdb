//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Models.RMOs;
using Nwsdb.Web.Api.Models.RSCs;
using Nwsdb.Web.Api.Models.WSSs;

namespace Nwsdb.Web.Api.Models.Lands
{
    public class Land : IAuditable
    {
        public Guid Id { get; set; }
        public Guid LandTypeId { get; set; }
        public Guid OwnerShipId { get; set; }
        public Guid ProvinceId { get; set; }
        public Guid DistricId { get; set; }
        public Guid DSDivisionId { get; set; }
        public Guid GSDivisionId { get; set; }
        public Guid SubCategoryId {get; set;}
        public Guid WssId { get; set; }
        public Guid RscId { get; set; }
        public Guid RmoId { get; set; }
        public string? LandName { get; set; }
        public string? Phone { get; set; }
        public string? LandArea { get; set; }
        public string? Address { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public bool IsLegal { get; set; }
        public string? Remark { get; set; }
        public LandStatus? LandStatus { get; set; }
        public LandAprovalStatus? LandAprovalStatus { get;set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get;set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set;}

        public virtual Rmo? Rmo { get; set; }
        public virtual Wss? WSS { get; set; }
        public virtual Rsc? RSC { get; set; }
    }
}
