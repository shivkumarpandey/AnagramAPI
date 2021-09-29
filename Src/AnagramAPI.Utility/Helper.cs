using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramAPI.Utility
{
   public class Helper
    {
        /// <summary>
        /// Get arranged word in alphabetical order.
        /// </summary>
        /// <param name="word">Anagram word from text file</param>
        /// <returns></returns>
        public static string GetAlphabeticalOrder(string word)
        {
            // Convert to char array
            char[] sortValue = word.ToLower().ToCharArray();

            // Sort the characters alphabetically 
            Array.Sort(sortValue);

            return new string(sortValue);
        }

        /// <summary>
        /// Get exception message
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetExceptionMessage(Exception ex)
        {
            string message = null;
            if (ex != null && ex.InnerException == null)
            {
                message = $"ExceptionMessage:{ex.Message} \nStackTrace: {ex.StackTrace} \nErrorSource:{ex.Source}";
            }
            else
            {
                message = $"ExceptionMessage:{ex.Message} \nStackTrace: {ex.StackTrace} \nErrorSource:{ex.Source} " +
                    $"\n InnerExceptionMessage :{ex.InnerException}";
            }
            return message;
        }
    }
}
