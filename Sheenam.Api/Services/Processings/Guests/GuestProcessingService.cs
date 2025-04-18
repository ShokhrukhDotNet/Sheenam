﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Services.Foundations.Guests;

namespace Sheenam.Api.Services.Processings.Guests
{
    public class GuestProcessingService : IGuestProcessingService
    {
        private readonly IGuestService guestService;

        public GuestProcessingService(IGuestService guestService) =>
            this.guestService = guestService;

        public async ValueTask<Guest> RegisterAndSaveGuestAsync(Guest guest)
        {
            guest.Id = Guid.NewGuid();
            guest.HomeRequests = new();

            return await this.guestService.AddGuestAsync(guest);
        }

        public IQueryable<Guest> RetrieveAllGuests() =>
            this.guestService.RetrieveAllGuests();

        public async ValueTask<Guest> RetrieveGuestByIdAsync(Guid guestId) =>
            await this.guestService.RetrieveGuestByIdAsync(guestId);

        public async ValueTask<Guest> ModifyGuestAsync(Guest guest) =>
            await this.guestService.ModifyGuestAsync(guest);

        public async ValueTask<Guest> RemoveGuestByIdAsync(Guid guestId) =>
            await this.guestService.RemoveGuestByIdAsync(guestId);
    }
}
