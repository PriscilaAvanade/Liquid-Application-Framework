// Copyright (c) Avanade Inc. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentValidation;
using Liquid;
using Liquid.OnWindowsClient;
using Liquid.Repository;
using Microsoft.Extensions.Caching.Memory;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using MemoryCache = Liquid.OnWindowsClient.MemoryCache;

namespace OnPre.IntegrationTests
{
    public class MemoryCacheIntegrationTests 
    {        
        private static readonly IFixture _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
        private readonly ITestOutputHelper _output;
        private EntityUnderTesting _lightModel;

        private readonly MemoryCache _sut;
        public MemoryCacheIntegrationTests(ITestOutputHelper output)
        {
            _output = output;
            Workbench.Instance.Reset();
            _sut = new MemoryCache(new MemoryCacheConfiguration
            {               
                SlidingExpirationSeconds = 120,
                AbsoluteExpirationRelativeToNowSeconds = 360,
            });
            _lightModel = new EntityUnderTesting();
        }

        [Fact]
        public void ShouldThrowExceptionWhenConfigurationIsNull()
        {
            Assert.ThrowsAny<ArgumentNullException>(() => new MemoryCache(null));
            
        }

        [Fact]
        public Task ShouldThrowNullExceptionWhenTrySetNullEntity()
        {
            _lightModel = null;
            string str = null;

            return Assert.ThrowsAnyAsync<ArgumentNullException>(() => _sut.SetAsync<EntityUnderTesting>(str, _lightModel));
        }

    } 
    public class EntityUnderTesting : LightModel<EntityUnderTesting>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public override void Validate()
        {
            RuleFor(i => i.Description).NotEmpty().WithErrorCode("DESCRIPTION_MUSTNOT_BE_EMPTY");
        }
    }
}
