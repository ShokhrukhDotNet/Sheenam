//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System.Text.Json.Serialization;

namespace Sheenam.Api.Models.Foundations.Guests
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GuestGenderType
    {
        Male,
        Female,
        Other
    }
}
