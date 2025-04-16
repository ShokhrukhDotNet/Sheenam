﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using Sheenam.Api.Models.Foundations.HomeRequests.Exceptions;
using Sheenam.Api.Models.Foundations.HomeRequests;
using System;

namespace Sheenam.Api.Services.Foundations.HomeRequests
{
    public partial class HomeRequestService
    {
        private void ValidateHomeRequestOnAdd(HomeRequest homeRequest)
        {
            ValidateHomeRequestNotNull(homeRequest);

            Validate(
                (Rule: IsInvalid(homeRequest.Id), Parameter: nameof(HomeRequest.Id)),
                (Rule: IsInvalid(homeRequest.GuestId), Parameter: nameof(HomeRequest.GuestId)),
                (Rule: IsInvalid(homeRequest.HomeId), Parameter: nameof(HomeRequest.HomeId)),
                (Rule: IsInvalid(homeRequest.Message), Parameter: nameof(HomeRequest.Message)),
                (Rule: IsInvalid(homeRequest.StartDate), Parameter: nameof(HomeRequest.StartDate)),
                (Rule: IsInvalid(homeRequest.EndDate), Parameter: nameof(HomeRequest.EndDate)),
                (Rule: IsInvalid(homeRequest.CreatedDate), Parameter: nameof(HomeRequest.CreatedDate)),
                (Rule: IsInvalid(homeRequest.UpdatedDate), Parameter: nameof(HomeRequest.UpdatedDate)));
        }

        private static void ValidateHomeRequestNotNull(HomeRequest homeRequest)
        {
            if (homeRequest is null)
            {
                throw new NullHomeRequestException();
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

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static void ValidateHomeRequestId(Guid homeRequestId) =>
            Validate((Rule: IsInvalid(homeRequestId), Parameter: nameof(HomeRequest.Id)));

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidHomeRequestException = new InvalidHomeRequestException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidHomeRequestException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidHomeRequestException.ThrowIfContainsErrors();
        }
    }
}
