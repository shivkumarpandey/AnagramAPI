using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramAPI.Contract
{
   public class Request
    {
        public string RequestGuid { get; set; }

        public string FilePath { get; set; }

        public string WordToFind { get; set; }
    }
}
