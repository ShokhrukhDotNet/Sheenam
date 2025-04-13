//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.HomeRequests.Exceptions
{
    public class FailedHomeRequestServiceException : Xeption
    {
        public FailedHomeRequestServiceException(Exception innerException)
            : base(message: "Failed homeRequest service error occurred, contact support",
                  innerException)
        { }
    }
}
