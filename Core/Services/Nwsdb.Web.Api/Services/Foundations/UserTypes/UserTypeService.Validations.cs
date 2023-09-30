//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Models.Users.Exceptions;
using Nwsdb.Web.Api.Models.Users;
using Nwsdb.Web.Api.Models.UserTypes;
using Nwsdb.Web.Api.Models.UserTypes.Exceptions;

namespace Nwsdb.Web.Api.Services.Foundations.UserTypes
{
    public partial class UserTypeService
    {
        private void ValidateUserTypeOnAdd(UserType userType)
        {
            ValidateUserTypeIsNotNull(userType);
            Validate(
                (Rule: IsInvalid(userType.Id), Parameter: nameof(User.Id)),
                (Rule: IsInvalid(userType.Name), Parameter: nameof(User.Name))
                );
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static void ValidateUserTypeId(Guid id) =>
            Validate((Rule: IsInvalid(id), Parameter: nameof(UserType.Id)));

        private static void ValidateUserTypeIsNotNull(UserType userType)
        {
            if (userType is null)
            {
                throw new NullUserTypeException();
            }
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidUserTypeException = new InvalidUserTypeException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidUserTypeException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidUserTypeException.ThrowIfContainsErrors();
        }
    }
}
