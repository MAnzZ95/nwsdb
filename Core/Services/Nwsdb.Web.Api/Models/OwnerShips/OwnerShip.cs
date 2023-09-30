//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

namespace Nwsdb.Web.Api.Models.OwnerShips
{
    public class OwnerShip : IAuditable
    {
        public Guid Id { get; set; }
        public string? Owner { get; set; }
        public string? Deed { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get;set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set;}
    }
}
