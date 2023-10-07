//-------------------------------------------------------------------------------------
// Copyright (C) 2023, by National Water Supply and Drain Board. All right reserved.
// The information and source code contained herein is the exclusive property of 
// NWSDB and may not be disclosed, examined or reproduced in whole or in part without
// explicit written authorization from NWSDB ------------------------------------------
//-------------------------------------------------------------------------------------

using Nwsdb.Web.Api.Models.Lands.Exceptions;
using Nwsdb.Web.Api.Models.Lands;
using Nwsdb.Web.Api.Models.LandHistories;
using Nwsdb.Web.Api.Models.LandHistories.Exceptions;

namespace Nwsdb.Web.Api.Services.Foundations.LandHistories
{
    public partial class LandHistoryService
    {
        private void ValidateLandHistoryOnAdd(LandHistory landHistory)
        {
            ValidatLandHistoryIsNotNull(landHistory);
            Validate(
                (Rule: IsInvalid(landHistory.Id), Parameter: nameof(LandHistory.Id)),
                (Rule: IsInvalid(landHistory.Address), Parameter: nameof(LandHistory.Address)),
                (Rule: IsInvalid(landHistory.LandName), Parameter: nameof(LandHistory.LandName)),
                (Rule: IsInvalid(landHistory.DistricId), Parameter: nameof(LandHistory.DistricId)),
                (Rule: IsInvalid(landHistory.SubCategoryId), Parameter: nameof(LandHistory.SubCategoryId))
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

        private static void ValidateLandHistoryId(Guid id) =>
            Validate((Rule: IsInvalid(id), Parameter: nameof(LandHistory.Id)));

        private static void ValidatLandHistoryIsNotNull(LandHistory land)
        {
            if (land is null)
            {
                throw new NullLandHistoryException();
            }
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidLandHistoryException = new InvalidLandHistoryException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidLandHistoryException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidLandHistoryException.ThrowIfContainsErrors();
        }
    }
}
