﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using Sheenam.Api.Models.Foundations.Hosts;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;

namespace Sheenam.Api.Services.Foundations.Hosts
{
    public partial class HostService
    {
        private void ValidateHostOnAdd(Host host)
        {
            ValidateHostNotNull(host);

            Validate(
                (Rule: IsInvalid(host.Id), Parameter: nameof(Host.Id)),
                (Rule: IsInvalid(host.FirstName), Parameter: nameof(Host.FirstName)),
                (Rule: IsInvalid(host.LastName), Parameter: nameof(Host.LastName)),
                (Rule: IsInvalid(host.DateOfBirth), Parameter: nameof(Host.DateOfBirth)),
                (Rule: IsInvalid(host.Email), Parameter: nameof(Host.Email)),
                (Rule: IsInvalid(host.PhoneNumber), Parameter: nameof(Host.PhoneNumber)),
                (Rule: IsInvalid(host.Address), Parameter: nameof(Host.Address)),
                (Rule: IsInvalid(host.Gender), Parameter: nameof(Host.Gender)));
        }

        private static void ValidateHostNotNull(Host host)
        {
            if (host is null)
            {
                throw new NullHostException();
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

        private static dynamic IsInvalid(HostGenderType gender) => new
        {
            Condition = Enum.IsDefined(gender) is false,
            Message = "Value is invalid"
        };

        private static void ValidateHostId(Guid hostId) =>
            Validate((Rule: IsInvalid(hostId), Parameter: nameof(Host.Id)));

        private static void ValidateStorageHost(Host maybeHost, Guid hostId)
        {
            if (maybeHost is null)
            {
                throw new NotFoundHostException(hostId);
            }
        }

        private static void ValidateHostOnModify(Host host)
        {
            ValidateHostNotNull(host);

            Validate(
                (Rule: IsInvalid(host.Id), Parameter: nameof(Host.Id)),
                (Rule: IsInvalid(host.FirstName), Parameter: nameof(Host.FirstName)),
                (Rule: IsInvalid(host.LastName), Parameter: nameof(Host.LastName)),
                (Rule: IsInvalid(host.DateOfBirth), Parameter: nameof(Host.DateOfBirth)),
                (Rule: IsInvalid(host.Email), Parameter: nameof(Host.Email)),
                (Rule: IsInvalid(host.PhoneNumber), Parameter: nameof(Host.PhoneNumber)),
                (Rule: IsInvalid(host.Address), Parameter: nameof(Host.Address)),
                (Rule: IsInvalid(host.Gender), Parameter: nameof(Host.Gender)));
        }

        private static void ValidateAgainstStorageHostOnModify(Host host, Host storageHost)
        {
            ValidateStorageHost(storageHost, host.Id);

            Validate(
                (Rule: IsInvalid(host.Id), Parameter: nameof(Host.Id)),
                (Rule: IsInvalid(host.FirstName), Parameter: nameof(Host.FirstName)),
                (Rule: IsInvalid(host.LastName), Parameter: nameof(Host.LastName)),
                (Rule: IsInvalid(host.DateOfBirth), Parameter: nameof(Host.DateOfBirth)),
                (Rule: IsInvalid(host.Email), Parameter: nameof(Host.Email)),
                (Rule: IsInvalid(host.PhoneNumber), Parameter: nameof(Host.PhoneNumber)),
                (Rule: IsInvalid(host.Address), Parameter: nameof(Host.Address)),
                (Rule: IsInvalid(host.Gender), Parameter: nameof(Host.Gender)));
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidHostException = new InvalidHostException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidHostException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidHostException.ThrowIfContainsErrors();
        }
    }
}