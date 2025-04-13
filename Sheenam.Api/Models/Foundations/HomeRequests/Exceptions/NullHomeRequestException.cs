//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.HomeRequests.Exceptions
{
    public class NullHomeRequestException : Xeption
    {
        public NullHomeRequestException()
            : base(message: "HomeRequest is null")
        { }
    }
}
