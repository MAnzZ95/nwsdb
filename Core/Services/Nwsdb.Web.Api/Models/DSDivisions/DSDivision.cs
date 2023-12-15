//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Nwsdb.Web.Api.Models.Districts;
using Nwsdb.Web.Api.Models.GSDivisions;

namespace Nwsdb.Web.Api.Models.DSDevisions
{
    public class DSDivision
    {
        public Guid Id { get; set; }
        public Guid DistrictId { get; set; }
        public int DSDCode { get; set; }
        
        public string? Name { get; set; }
        public virtual District District { get; set; }
        public ICollection<GSDivision> GSDivisions { get; set; }
    }
}
