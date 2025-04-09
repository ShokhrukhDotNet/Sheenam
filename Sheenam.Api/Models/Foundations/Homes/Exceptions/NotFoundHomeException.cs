//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Homes.Exceptions
{
    public class NotFoundHomeException : Xeption
    {
        public NotFoundHomeException(Guid homeId)
            : base(message: $"Couldn't find home with id {homeId}")
        { }
    }
}
