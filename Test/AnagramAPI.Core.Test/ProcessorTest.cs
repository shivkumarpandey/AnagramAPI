using AnagramAPI.Abstraction;
using AnagramAPI.Contract;
using AnagramAPI.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace AnagramAPI.Core.Test
{
    public class ProcessorTest
    {
        private IServiceCollection _serviceCollection;
        private IServiceProvider _serviceProvider;
        private IProcessor _processor;
        private IRepository _repository;
        private readonly IConfiguration _config;

        public ProcessorTest()
        {
            _serviceCollection = new ServiceCollection().AddLogging()
              .AddSingleton<IConfiguration>(InitConfiguration())
              .AddSingleton<IProcessor, Processor>()
              .AddSingleton<IRepository, AnagramRepository>();

            _serviceProvider = _serviceCollection.BuildServiceProvider();
            _processor = _serviceProvider.GetRequiredService<IProcessor>();
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
           string result= _processor.FindAnagrams(request);
            Assert.Equal("car",result);
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
