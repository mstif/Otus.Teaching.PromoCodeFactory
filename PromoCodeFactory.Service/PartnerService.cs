using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeFactory.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly IRepository<Partner> _partnersRepository;

        public PartnerService(IRepository<Partner> partnersRepository)
        {
            _partnersRepository = partnersRepository;
        }

        public async Task<Partner> GetPartnerByIdAsync(Guid partnerId)
        {
            return await _partnersRepository.GetByIdAsync(partnerId);
        }

        public async void SetPartnerNewLimit(Guid partnerId, PartnerPromoCodeLimit limit)
        {
            var partner = await _partnersRepository.GetByIdAsync(partnerId);
            //Установка лимита партнеру
            var activeLimit = partner.PartnerLimits.FirstOrDefault(x =>
                !x.CancelDate.HasValue);

            if (activeLimit != null)
            {
                //Если партнеру выставляется лимит, то мы 
                //должны обнулить количество промокодов, которые партнер выдал, если лимит закончился, 
                //то количество не обнуляется
                partner.NumberIssuedPromoCodes = 0;

                //При установке лимита нужно отключить предыдущий лимит
                activeLimit.CancelDate = DateTime.Now; 
            }

            partner.PartnerLimits.Add(limit);
            await _partnersRepository.UpdateAsync(partner);
        }
    }
}
