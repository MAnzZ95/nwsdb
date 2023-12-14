//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Models.DSDevisions;

namespace Nwsdb.Web.Api.Models.GSDivisions
{
    public class GSDivision
    {
        public Guid Id { get; set; }
        public Guid DSDivisionId { get; set; }
        public int GSDCode { get; set; }
        public string? Name { get; set; }
        public virtual DSDivision DSDivision { get; set; }
    }
}
