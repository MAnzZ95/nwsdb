﻿//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Models.RMOs;

namespace Nwsdb.Web.Api.Models.RSCs
{
    public class Rsc : IAuditable
    {
        public Guid Id { get; set; }
        public string? RMONumber { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Mobile { get; set; }
        public RscStatus Status { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public ICollection<Rmo> Rmos { get; set; }
    }
}
