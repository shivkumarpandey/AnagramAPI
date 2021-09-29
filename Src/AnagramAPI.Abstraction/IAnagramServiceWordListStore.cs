using AnagramAPI.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramAPI.Abstraction
{
    public interface IRepository
    {
        Dictionary<string,string> LoadWordListFromFile(Request request);
        void AddWordToDictionary(string word);
    }
}
