//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.HomeRequests.Exceptions
{
    public class NotFoundHomeRequestException : Xeption
    {
        public NotFoundHomeRequestException(Guid homeRequestId)
            : base(message: $"Couldn't find homeRequest with id {homeRequestId}")
        { }
    }
}
