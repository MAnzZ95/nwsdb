//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Models.RSCs;
using Nwsdb.Web.Api.Models.WSSs;

namespace Nwsdb.Web.Api.Models.RMOs
{
    public class Rmo : IAuditable
    {
        public Guid Id { get; set; }
        public string? RMONumber { get; set; }
        public Guid RscId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Mobile { get; set; }
        public RmoStatus Status { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }

        public virtual Rsc Rsc { get; set; }
        public ICollection<Wss> Wsses { get; set; }
    }
}
