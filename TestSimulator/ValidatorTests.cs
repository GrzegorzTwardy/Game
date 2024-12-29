using Simulator;

namespace TestSimulator;

public class ValidatorTests
{
    [Theory]
    [InlineData(5, 0, 10, 5)]
    [InlineData(11, 0, 10, 10)]
    [InlineData(-2, -1, 10, -1)]
    [InlineData(-2, 0, 0, 0)]
    public void Limiter_ShouldReturnCorrectValue(int value, int min, int max, int expected)
    {
        int testValue = Validator.Limiter(value, min, max);
        Assert.Equal(expected, testValue);
    }

    [Theory]
    [InlineData("Nazwa", 2, 10, '#', "Nazwa")]
    [InlineData("nazwa", 2, 4, '#', "Nazw")]
    [InlineData("   ab ", 3, 10, '#', "Ab#")]
    [InlineData("12345678910   ", 2, 8, '#', "12345678")]
    [InlineData(" abcdefhg   ", 2, 4, '#', "Abcd")]
    [InlineData(" rs  ", 5, 8, 'x', "Rsxxx")]
    [InlineData(" rs  ", 11, 30, 'x', "Rsxxxxxxxxx")]
    [InlineData("      ", 3, 10, '#', "")]
    [InlineData("a                   b", 3, 10, '#', "A##")]
    public void Shortener_ShouldReturnCorrectValue(string value, int min, int max, 
        char placeholder, string expected)
    {
        string testValue = Validator.Shortener(value, min, max, placeholder);
        Assert.Equal(expected, testValue);
    }
}
