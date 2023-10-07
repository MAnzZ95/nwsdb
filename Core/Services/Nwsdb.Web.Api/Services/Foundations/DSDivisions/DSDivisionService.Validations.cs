//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Models.Lands.Exceptions;
using Nwsdb.Web.Api.Models.DSDevisions;
using Nwsdb.Web.Api.Models.GSDivisions.Exceptions;

namespace Nwsdb.Web.Api.Services.Foundations.DSDivisions
{
    public partial class DSDivisionService
    {
        private void ValidateLandOnAdd(DSDivision dSDivision)
        {
            ValidateDSDivisionIsNotNull(dSDivision);
            Validate(
                (Rule: IsInvalid(dSDivision.Id), Parameter: nameof(DSDivision.Id)),
                (Rule: IsInvalid(dSDivision.Name), Parameter: nameof(DSDivision.Name))
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

        private static void ValidateLandId(Guid id) =>
            Validate((Rule: IsInvalid(id), Parameter: nameof(DSDivision.Id)));

        private static void ValidateDSDivisionIsNotNull(DSDivision dSDivision)
        {
            if (dSDivision is null)
            {
                throw new NullGSDivisionException();
            }
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidLandException = new InvalidLandException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidLandException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidLandException.ThrowIfContainsErrors();
        }
    }
}
