using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Threading.Tasks;

namespace PromoCodeFactory.Services
{
    public interface IPartnerService
    {
        Task<Partner> GetPartnerByIdAsync(Guid partnerId);
        void SetPartnerNewLimit(Guid partnerId, PartnerPromoCodeLimit limit);
    }
}
