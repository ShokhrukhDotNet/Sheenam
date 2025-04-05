//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Models.Foundations.Homes.Exceptions;

namespace Sheenam.Api.Services.Foundations.Homes
{
    public partial class HomeService
    {
        private void ValidateHomeOnAdd(Home home)
        {
            ValidateHomeNotNull(home);

            Validate(
                (Rule: IsInvalid(home.HomeId), Parameter: nameof(Home.HomeId)),
                (Rule: IsInvalid(home.Address), Parameter: nameof(Home.Address)),
                (Rule: IsInvalid(home.AdditionalInfo), Parameter: nameof(Home.AdditionalInfo)),
                (Rule: IsInvalid(home.NumberOfBedrooms), Parameter: nameof(Home.NumberOfBedrooms)),
                (Rule: IsInvalid(home.NumberOfBathrooms), Parameter: nameof(Home.NumberOfBathrooms)),
                (Rule: IsInvalid(home.Area), Parameter: nameof(Home.Area)),
                (Rule: IsInvalid(home.Price), Parameter: nameof(Home.Price)));
        }

        private static void ValidateHomeNotNull(Home home)
        {
            if (home is null)
            {
                throw new NullHomeException();
            }
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

        private static dynamic IsInvalid(int number) => new
        {
            Condition = number <= 0,
            Message = "Value must be greater than 0"
        };

        private static dynamic IsInvalid(double number) => new
        {
            Condition = number <= 0,
            Message = "Value must be greater than 0"
        };

        private static dynamic IsInvalid(decimal number) => new
        {
            Condition = number <= 0,
            Message = "Value must be greater than 0"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidHomeException = new InvalidHomeException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidHomeException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidHomeException.ThrowIfContainsErrors();
        }
    }
}