using Nwsdb.Web.Api.Models.Users.Exceptions;
using Nwsdb.Web.Api.Models.Users;
using Nwsdb.Web.Api.Models.LandTypes;
using Nwsdb.Web.Api.Models.LandTypes.Exceptions;

namespace Nwsdb.Web.Api.Services.Foundations.LandTypes
{
    public partial class LandTypeService
    {
        //private void ValidateLandTypeOnAdd(LandType landType)
        //{
        //    ValidateUserIsNotNull(user);
        //    Validate(
        //        (Rule: IsInvalid(user.Id), Parameter: nameof(User.Id)),
        //        (Rule: IsInvalid(user.Name), Parameter: nameof(User.Name)),
        //        (Rule: IsInvalid(user.Email), Parameter: nameof(User.Email)),
        //        (Rule: IsInvalid(user.Mobile), Parameter: nameof(User.Mobile)),
        //        (Rule: IsInvalid(user.Password), Parameter: nameof(User.Password))
        //        );
        //}

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

        //private static void ValidateLandTypeId(Guid id) =>
        //    Validate((Rule: IsInvalid(id), Parameter: nameof(LandType.)));

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
