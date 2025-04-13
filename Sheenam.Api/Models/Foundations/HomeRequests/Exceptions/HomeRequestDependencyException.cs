//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.HomeRequests.Exceptions
{
    public class HomeRequestDependencyException : Xeption
    {
        public HomeRequestDependencyException(Xeption innerException)
            : base(message: "HomeRequest dependency error occurred, contact support",
                  innerException)
        { }
    }
}
