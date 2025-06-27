//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System.Text.Json.Serialization;

namespace Sheenam.Api.Models.Foundations.Hosts
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum HostGenderType
    {
        Male,
        Female,
        Other
    }
}
