//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using Sheenam.Api.Models.Foundations.Homes;
using System.Collections.Generic;

namespace Sheenam.Api.Models.Foundations.Hosts
{
    public class Host
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public HostGenderType Gender { get; set; }
        public List<Home> Homes { get; set; }
    }
}
