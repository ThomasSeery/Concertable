using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IRegisterRepository registerRepository;

        public RegisterService(IRegisterRepository registerRepository)
        {
            this.registerRepository = registerRepository;
        }

        public async Task<IEnumerable<Register>> GetRegistrationsForListingIdAsync(int listingId)
        {
            return await registerRepository.GetAllForListingIdAsync(listingId);
        }
    }
}
