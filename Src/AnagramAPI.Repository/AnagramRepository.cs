using AnagramAPI.Abstraction;
using AnagramAPI.Contract;
using AnagramAPI.Utility;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace AnagramAPI.Repository
{
    public  class AnagramRepository : IRepository
    {
        private readonly ILogger<AnagramRepository> _logger;

        public Dictionary<string, string> wordList;

        public AnagramRepository(ILogger<AnagramRepository> logger)
        {
            wordList = new Dictionary<string, string>();
            _logger = logger;
        }

        /// <summary>
        /// Load anagram words from text file
        /// </summary>
        public Dictionary<string, string> LoadWordListFromFile(Request request)
        {
            _logger.LogInformation("ReadWordListFile method started.");

            //get filepath from appsetting.json file
            string filepath = request.FilePath; //_config.GetValue<string>("FilePath");

            try
            {
                using (StreamReader streamReader = new StreamReader(filepath))
                {
                    string row;
                    while ((row = streamReader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(row))
                        {
                            AddWordToDictionary(row.Trim());
                        }
                    }
                }

                return wordList;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured in LoadWordListFromFile method : {0}", Utility.Helper.GetExceptionMessage(ex));
                throw;
            }
        }

        /// <summary>
        /// This function is to adding word into anagram dictionary
        /// </summary>
        /// <param name="word">Anagram word from text file</param>
        public void AddWordToDictionary(string word)
        {
            _logger.LogInformation("AddWordToDictionary method started.");

            try
            {
                //get arranged word in alphabetical order
                string alphabetWord = Helper.GetAlphabeticalOrder(word);
                string existingWords;

                //if alphabetWord value is already exist in dictionary then append word with comma
                if (wordList.Count > 0 && wordList.TryGetValue(alphabetWord, out existingWords))
                {
                     wordList[alphabetWord] = existingWords + "," + word;
                    _logger.LogInformation("Anagram dictionay updated for key : {0} and value {1}", alphabetWord, existingWords + "," + word);
                }
                else
                {
                     wordList.Add(alphabetWord, word);
                    _logger.LogInformation("Anagram dictionay added with key : {0} and value {1}", alphabetWord, word);
                }

            }

            catch (Exception ex)
            {
                _logger.LogError("Exception occured in AddWord method: {0}", Utility.Helper.GetExceptionMessage(ex));
                throw;
            }

            _logger.LogInformation("AddWordToDictionary method ended.");
        }

  
    }
}
