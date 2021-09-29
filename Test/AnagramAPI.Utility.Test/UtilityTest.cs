using System;
using Xunit;

namespace AnagramAPI.Utility.Test
{
    public class UtilityTest
    {
        [Fact]
        [Trait("Category", "UnitTest")]
        public void GetExceptionMessage_Should_Return_ExceptionMessage()
        {
            //Arrange
            Exception exception = new NullReferenceException("Object Reference Error.");

            //Act
            string message = Utility.Helper.GetExceptionMessage(exception);

            //Assert
            Assert.True(!string.IsNullOrEmpty(message));
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        public void GetAlphabeticalOrderWord_Success()
        {
            //Arrange
            string inputWord = "test";

            //Act
            string anagarmResult = Utility.Helper.GetAlphabeticalOrder(inputWord);

            //Assert
            Assert.Equal("estt", anagarmResult);
        }
    }
}
