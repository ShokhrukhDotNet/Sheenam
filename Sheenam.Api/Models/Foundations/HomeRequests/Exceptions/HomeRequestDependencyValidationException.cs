//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.HomeRequests.Exceptions
{
    public class HomeRequestDependencyValidationException : Xeption
    {
        public HomeRequestDependencyValidationException(Xeption innerException)
            : base(message: "HomeRequest dependency validation error occurred, fix the errors and try again",
                   innerException)
        { }
    }
}
