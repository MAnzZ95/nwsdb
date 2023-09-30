﻿//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Xeptions;

namespace Nwsdb.Web.Api.Models.UserTypes.Exceptions
{
    public class AlreadyExistsUserTypeException : Xeption
    {
        public AlreadyExistsUserTypeException()
            : base(message: "User type with the same id already exists.") { }
    }
}