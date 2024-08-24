using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Controllers;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Otus.Teaching.PromoCodeFactory.UnitTests.WebHost.Controllers.Partners.Builders
{
    internal class PartnerControllerTestBuilder
    {
        public Partner CreateCurrentPartner()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var partner = fixture.Build<Partner>().With(x => x.IsActive, true)
                .With(l => l.PartnerLimits).Create();
            partner.PartnerLimits.First().CancelDate = null;

            return partner;
        }
        public SetPartnerPromoCodeLimitRequest CreateRequest()
        {
            var fixture = new Fixture();
            return fixture.Build<SetPartnerPromoCodeLimitRequest>()
                .With(r => r.Limit, 10)
                .With(d => d.EndDate, DateTime.Now.AddDays(30))
                .Create();
        }
        public ICollection<PartnerPromoCodeLimit> CreateLimitsPartner(Partner partner)
        {
            var fixture = new Fixture();
            return fixture.Build<PartnerPromoCodeLimit>()
                .With(x => x.CancelDate, null as DateTime?)
                .With(x => x.PartnerId, partner.Id)
                .With(x => x.Partner, partner)
                .CreateMany(4)
                .ToList();
        }


    }
}
