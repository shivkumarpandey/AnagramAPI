using AnagramAPI.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramAPI.Abstraction
{
    public interface IProcessor
    {
        string FindAnagrams(Request request);
      
    }
}
