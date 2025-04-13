//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.HomeRequests.Exceptions
{
    public class AlreadyExistHomeRequestException : Xeption
    {
        public AlreadyExistHomeRequestException(Exception innerException)
            : base(message: "HomeRequest already exists", innerException)
        { }
    }
}
