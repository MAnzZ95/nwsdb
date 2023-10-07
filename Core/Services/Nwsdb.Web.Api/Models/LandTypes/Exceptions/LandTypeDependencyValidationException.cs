//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Xeptions;

namespace Nwsdb.Web.Api.Models.LandTypes.Exceptions
{
    public class LandTypeDependencyValidationException : Xeption
    {
        public LandTypeDependencyValidationException(Xeption innerException)
            : base(message: "Land type validation error occured, Please try again.", innerException) { }
    }
}
