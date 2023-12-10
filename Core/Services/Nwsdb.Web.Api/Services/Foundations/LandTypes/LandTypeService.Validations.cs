using Nwsdb.Web.Api.Models.Users.Exceptions;
using Nwsdb.Web.Api.Models.Users;
using Nwsdb.Web.Api.Models.LandTypes;
using Nwsdb.Web.Api.Models.LandTypes.Exceptions;

namespace Nwsdb.Web.Api.Services.Foundations.LandTypes
{
    public partial class LandTypeService
    {
        private void ValidateLandTypeOnAdd(LandType landType)
        {
            ValidateLandTypeIsNotNull(landType);
            Validate(
                (Rule: IsInvalid(landType.Id), Parameter: nameof(LandType.Id)),
                (Rule: IsInvalid(landType.Name), Parameter: nameof(LandType.Name))
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

        private static void ValidateLandTypeId(Guid id) =>
            Validate((Rule: IsInvalid(id), Parameter: nameof(LandType.Id)));

        private static void ValidateLandTypeIsNotNull(LandType landType)
        {
            if (landType is null)
            {
                throw new NullLandTypeException();
            }
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidLandTypeException = new InvalidLandTypeException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidLandTypeException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidLandTypeException.ThrowIfContainsErrors();
        }
    }
}
