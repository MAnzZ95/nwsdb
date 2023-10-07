//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Xeptions;

namespace Nwsdb.Web.Api.Models.Provinces.Exceptions
{
    public class ProvinceServiceException : Xeption
    {
        public ProvinceServiceException(Exception innerException)
            : base(message: "Province service error occured, contact support.", innerException) { }
    }
}
