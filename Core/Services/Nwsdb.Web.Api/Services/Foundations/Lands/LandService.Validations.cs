//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Models.Lands;
using Nwsdb.Web.Api.Models.Lands.Exceptions;

namespace Nwsdb.Web.Api.Services.Foundations.Lands
{
    public partial class LandService
    {
        private void ValidateLandOnAdd(Land land)
        {
            ValidateLandIsNotNull(land);
            Validate(
                (Rule: IsInvalid(land.Id), Parameter: nameof(Land.Id)),
                (Rule: IsInvalid(land.Address), Parameter: nameof(Land.Address)),
                (Rule: IsInvalid(land.LandName), Parameter: nameof(Land.LandName)),
                (Rule: IsInvalid(land.DistricId), Parameter: nameof(Land.DistricId)),
                (Rule: IsInvalid(land.SubCategoryId), Parameter: nameof(Land.SubCategoryId))
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
            Validate((Rule: IsInvalid(id), Parameter: nameof(Land.Id)));

        private static void ValidateLandIsNotNull(Land land)
        {
            if (land is null)
            {
                throw new NullLandException();
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
