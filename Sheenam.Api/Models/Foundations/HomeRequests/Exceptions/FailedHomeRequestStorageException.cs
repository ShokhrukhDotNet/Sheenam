//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.HomeRequests.Exceptions
{
    public class FailedHomeRequestStorageException : Xeption
    {
        public FailedHomeRequestStorageException(Exception innerException)
            : base(message: "Failed homeRequest storage error occurred, contact support",
                  innerException)
        { }
    }
}
