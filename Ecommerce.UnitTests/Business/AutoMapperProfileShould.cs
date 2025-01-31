﻿using AutoMapper;
using FluentAssertions;
using Rookie.Ecom.Business;
using System;
using Xunit;

namespace Ecommerce.UnitTests.Business
{
    public class AutoMapperProfileShould
    {
        [Fact]
        public void BeValid()
        {
            // Arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            IMapper mapper = config.CreateMapper();

            // Act
            Action act = () => mapper.ConfigurationProvider.AssertConfigurationIsValid();

            // Assert
            act.Should().NotThrow();
        }
    }
}
