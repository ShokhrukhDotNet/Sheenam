//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using Sheenam.Api.Models.Foundations.Guests;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Sheenam.Api.Services.Processings.Guests
{
    public interface IGuestProcessingService
    {
        ValueTask<Guest> RegisterAndSaveGuestAsync(Guest guest);
        IQueryable<Guest> RetrieveAllGuests();
        ValueTask<Guest> RetrieveGuestByIdAsync(Guid guestId);
        ValueTask<Guest> ModifyGuestAsync(Guest guest);
        ValueTask<Guest> RemoveGuestByIdAsync(Guid guestId);
    }
}