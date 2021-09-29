using AnagramAPI.Abstraction;
using AnagramAPI.Contract;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using AnagramAPI.Utility;

namespace AnagramAPI.Core
{
    public class Processor : IProcessor
    {
        private readonly ILogger<Processor> _logger;
        private IRepository _repository;
        public Processor(ILogger<Processor> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Find anagram word found  
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string FindAnagrams(Request request)
        {
            _logger.LogInformation("FindAnagrams method started.");

            try
            {
                string anagramWords = string.Empty;

               Dictionary<string,string> wordList =  _repository.LoadWordListFromFile(request);
               wordList.TryGetValue(Utility.Helper.GetAlphabeticalOrder(request.WordToFind), out anagramWords);

                _logger.LogInformation("Anagram Words found for {0} : {1}", request.WordToFind, anagramWords);
                _logger.LogInformation("FindAnagrams method ended.");

                return anagramWords;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured in FindAnagrams : {0}", Utility.Helper.GetExceptionMessage(ex));
                throw;
            }
        }
    }
}
