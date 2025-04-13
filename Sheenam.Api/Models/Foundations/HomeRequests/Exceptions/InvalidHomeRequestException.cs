//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.HomeRequests.Exceptions
{
    public class InvalidHomeRequestException : Xeption
    {
        public InvalidHomeRequestException()
            : base(message: "HomeRequest is invalid")
        { }
    }
}
