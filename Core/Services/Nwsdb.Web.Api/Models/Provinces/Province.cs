﻿//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Models.Districts;

namespace Nwsdb.Web.Api.Models.Provinces
{
    public class Province
    {
        public Guid Id { get; set; }
        public int PCode { get; set; }
        public string? Name { get; set; }

        public ICollection<District> Districts { get; set; }
    }
}
