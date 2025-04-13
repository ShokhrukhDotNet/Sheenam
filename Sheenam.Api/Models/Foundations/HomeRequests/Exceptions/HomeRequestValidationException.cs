//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.HomeRequests.Exceptions
{
    public class HomeRequestValidationException : Xeption
    {
        public HomeRequestValidationException(Xeption innerException)
            : base(message: "HomeRequest validation error occured, fix the errors and try again",
                innerException)
        { }
    }
}
