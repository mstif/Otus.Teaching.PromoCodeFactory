using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.UnitTests.WebHost.Controllers.Partners.Builders;
using Otus.Teaching.PromoCodeFactory.WebHost.Controllers;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using PromoCodeFactory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Otus.Teaching.PromoCodeFactory.UnitTests.WebHost.Controllers.Partners
{
    public class SetPartnerPromoCodeLimitAsyncTests
    {
        private readonly Mock<IRepository<Partner>> _mockPartnerRepository;
        private readonly PartnerService _partnerService;
        private readonly PartnersController _partnerController;
        private readonly PartnerControllerTestBuilder _testBuilder;

        public SetPartnerPromoCodeLimitAsyncTests()
        {
            _mockPartnerRepository = new Mock<IRepository<Partner>>();
            _partnerService = new PartnerService(_mockPartnerRepository.Object);
            _partnerController = new PartnersController(
                _mockPartnerRepository.Object,
                _partnerService);
            _testBuilder = new PartnerControllerTestBuilder();
        }

        /// <summary>
        /// Если партнер не найден, то нужно выдать ошибку 404
        /// </summary>
        [Fact]
        public async Task SetPartnerPromoCodeLimit_NotExistingIdPartner_ReturnsNotFoundResult()
        {
            //Arrange
            var id = Guid.NewGuid();
            var partner = _testBuilder.CreateCurrentPartner();
            _mockPartnerRepository.Setup(repo => repo.GetByIdAsync(partner.Id)).ReturnsAsync(partner);

            var request = _testBuilder.CreateRequest();

            //Act
            var result = await _partnerController.SetPartnerPromoCodeLimitAsync(id, request);

            //Assert
            result.Should().BeAssignableTo<NotFoundResult>();
        }

        /// <summary>
        /// Если партнер заблокирован, то есть поле IsActive=false в классе Partner,
        /// то также нужно выдать ошибку 400
        /// </summary>
        [Fact]
        public async Task SetPartnerPromoCodeLimit_PartnerIsActiveFalse_Returns400()
        {
            //Arrange
            var partner = _testBuilder.CreateCurrentPartner();
            partner.IsActive = false;
            _mockPartnerRepository.Setup(repo => repo.GetByIdAsync(partner.Id)).ReturnsAsync(partner);

            var request = _testBuilder.CreateRequest();

            //Act
            var result = await _partnerController.SetPartnerPromoCodeLimitAsync(partner.Id, request);

            //Assert
            result.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        /// <summary>
        /// Если партнеру выставляется лимит, то мы должны обнулить количество промокодов,
        /// которые партнер выдал NumberIssuedPromoCodes
        /// </summary>
        [Fact]
        public async Task SetPartnerPromoCodeLimit_LimitIsNotEmpty_NumberIssuedPromoCodesZerro()
        {
            //Arrange
            var partner = _testBuilder.CreateCurrentPartner();
            _mockPartnerRepository.Setup(repo => repo.GetByIdAsync(partner.Id)).ReturnsAsync(partner);
      
            var request = _testBuilder.CreateRequest();

            //Act
            var result = await _partnerController.SetPartnerPromoCodeLimitAsync(partner.Id, request);

            //Assert
            partner.NumberIssuedPromoCodes.Should().Be(0);
        }

        /// <summary>
        /// При установке лимита нужно отключить предыдущий лимит
        /// </summary>
        [Fact]
        public async Task SetPartnerPromoCodeLimit_LimitIsNotEmpty_PreviosLimitCancel()
        {
            //Arrange
            var partner = _testBuilder.CreateCurrentPartner();
            _mockPartnerRepository.Setup(repo => repo.GetByIdAsync(partner.Id)).ReturnsAsync(partner);
            var request = _testBuilder.CreateRequest();

            //Act
            var result = await _partnerController.SetPartnerPromoCodeLimitAsync(partner.Id, request);

            //Assert
            var activeLimit = partner.PartnerLimits.First();
            activeLimit.CancelDate.Should().HaveValue();
        }

        /// <summary>
        /// если лимит закончился,
        /// то количество не обнуляется
        /// </summary>
        [Fact]
        public async Task SetPartnerPromoCodeLimit_LimitsCancelled_NumberIssuedPromoCodesNotZerro()
        {
            //Arrange
            var partner = _testBuilder.CreateCurrentPartner();
            partner.PartnerLimits.First().CancelDate = DateTime.Now;

            var NumberIssuedPromoCodesOld = partner.NumberIssuedPromoCodes;
            _mockPartnerRepository.Setup(repo => repo.GetByIdAsync(partner.Id)).ReturnsAsync(partner);
            var request = _testBuilder.CreateRequest();

            //Act
            var result = await _partnerController.SetPartnerPromoCodeLimitAsync(partner.Id, request);

            //Assert
            partner.NumberIssuedPromoCodes.Should().Be(NumberIssuedPromoCodesOld);
        }

        /// <summary>
        /// Лимит должен быть больше 0
        /// </summary>
        [Fact]
        public async Task SetPartnerPromoCodeLimit_LimitNegative_ReturnsBadRequest()
        {
            //Arrange
            var partner = _testBuilder.CreateCurrentPartner();
            _mockPartnerRepository.Setup(repo => repo.GetByIdAsync(partner.Id)).ReturnsAsync(partner);

            var request = _testBuilder.CreateRequest();
            request.Limit = -10;

            //Act
            var result = await _partnerController.SetPartnerPromoCodeLimitAsync(partner.Id, request);

            //Assert
            result.Should().BeAssignableTo<BadRequestObjectResult>();
        }
        /// <summary>
        /// Нужно убедиться, что сохранили новый лимит в базу данных
        /// </summary>
        [Fact]
        public async Task SetPartnerPromoCodeLimit_SetNewLimit_SaveNewLimit()
        {
            //Arrange
            var partner = _testBuilder.CreateCurrentPartner();
            var endDate = DateTime.Now.AddDays(30);
            var limit = 10;

            _mockPartnerRepository.Setup(repo => repo.GetByIdAsync(partner.Id)).ReturnsAsync(partner);

            var request = _testBuilder.CreateRequest();

            request.EndDate = endDate;
            request.Limit = limit;

            var oldCountLimits = partner.PartnerLimits.Count;

            //Act
            var result = await _partnerController.SetPartnerPromoCodeLimitAsync(partner.Id, request);

            //Assert
            _mockPartnerRepository.Verify(repo => repo.UpdateAsync(partner), Times.Once());

            partner.PartnerLimits.Count.Should().Be(oldCountLimits + 1);
            partner.PartnerLimits.ElementAt(oldCountLimits).Limit.Should().Be(limit);
            partner.PartnerLimits.ElementAt(oldCountLimits).EndDate.Should().Be(endDate);
        }
    }
}