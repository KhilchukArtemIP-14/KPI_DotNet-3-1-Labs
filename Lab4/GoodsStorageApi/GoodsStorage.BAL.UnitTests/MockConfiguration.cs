using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.BAL.UnitTests
{
    public static class MockConfiguration
    {
        public static IConfiguration CreateMockConfiguration()
        {
            //Arrange
            var inMemorySettings = new Dictionary<string, string> {
            {"Jwt:Key", "ci7MpCTqV29znO9uazBsHBcv2ntiWz7BNRyjeLf07khNZoelgEL98Phut8dJlE6"},
            {"Jwt:Issuer", "https://localhost:7253/"},
            {"Jwt:Audience", "https://localhost:7253/"},
            };

            var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

            return configuration;
        }
    }
}
