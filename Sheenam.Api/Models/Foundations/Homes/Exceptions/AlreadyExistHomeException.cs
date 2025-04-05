//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Homes.Exceptions
{
    public class AlreadyExistHomeException : Xeption
    {
        public AlreadyExistHomeException(Exception innerException)
            : base(message: "Home already exists", innerException)
        { }
    }
}
