//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System.Text.Json.Serialization;

namespace Sheenam.Api.Models.Foundations.Homes
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum HouseType
    {
        Flat,
        Bungalow,
        Duplex,
        Other
    }
}