//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Models.RMOs;

namespace Nwsdb.Web.Api.Models.WSSs
{
    public class Wss : IAuditable
    {
        public Guid Id { get; set; }

        public Guid RmoId { get; set; }
        public string? WSSNumber { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Mobile { get; set; }
        public WssStatus Status { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }

        public virtual Rmo Rmo { get; set; }
    }
}
