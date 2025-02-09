//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Guests.Exceptions
{
    public class FailedGuestServiceException : Xeption
    {
        public FailedGuestServiceException(Exception innerException)
            : base(message: "Failed guest service error occurred, contact support",
                  innerException)
        { }
    }
}
