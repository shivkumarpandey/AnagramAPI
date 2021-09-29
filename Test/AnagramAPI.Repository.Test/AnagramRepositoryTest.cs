using AnagramAPI.Abstraction;
using AnagramAPI.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace AnagramAPI.Repository.Test
{
    public class AnagramRepositoryTest
    {
        private IServiceCollection _serviceCollection;
        private IServiceProvider _serviceProvider;
        private IRepository _repository;
        private readonly IConfiguration _config;

        public AnagramRepositoryTest()
        {
            _serviceCollection = new ServiceCollection().AddLogging()
              .AddSingleton<IConfiguration>(InitConfiguration())
              .AddSingleton<IRepository, AnagramRepository>();

            _serviceProvider = _serviceCollection.BuildServiceProvider();
            _repository = _serviceProvider.GetRequiredService<IRepository>();
            _config = _serviceProvider.GetRequiredService<IConfiguration>();
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        public void Success()
        {
            Request request = new Request()
            {
                FilePath = _config.GetValue<string>("FilePath"),
                RequestGuid = Guid.NewGuid().ToString(),
                WordToFind = "Arc"
            };
           Dictionary<string,string> wordData= _repository.LoadWordListFromFile(request);
            Assert.NotNull(wordData);
            Assert.True(wordData.Count > 0);
        }

        private static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            return config;
        }
    }
}
