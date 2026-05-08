namespace WordCloudBackend.Features.WordCounter.Tests;

public class TextParserServiceTests
{
    private readonly TextParserService _service = new();

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void ParseText_NullOrWhiteSpaceText_ShouldReturnEmptyString(string text)
    {
        // Arrange
        var options = new TextParserOptions
        {
            ConvertToLowerCase = false,
            MinimumWordLength = 0,
            IgnoredWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        };

        // Act
        var result = _service.ParseText(text, options);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void ParseText_TrimTrue_ShouldRemoveLeadingAndTrailingWhitespace()
    {
        // Arrange
        var text = "  hello world  ";
        var options = new TextParserOptions
        {
            ConvertToLowerCase = false,
            MinimumWordLength = 0,
            IgnoredWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        };

        // Act
        var result = _service.ParseText(text, options);

        // Assert
        result.Should().Be("hello world");
    }

    [Fact]
    public void ParseText_TrimFalse_ShouldKeepLeadingAndTrailingWhitespace()
    {
        // Arrange
        var text = "  hello world  ";
        var options = new TextParserOptions
        {
            ConvertToLowerCase = false,
            MinimumWordLength = 0,
            IgnoredWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        };

        // Act
        var result = _service.ParseText(text, options);

        // Assert
        result.Should().Be("hello world");
    }

    [Fact]
    public void ParseText_ConvertToLowerCaseTrue_ShouldReturnLowerCasedText()
    {
        // Arrange
        var text = "Hello WORLD";
        var options = new TextParserOptions
        {
            ConvertToLowerCase = true,
            MinimumWordLength = 0,
            IgnoredWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        };

        // Act
        var result = _service.ParseText(text, options);

        // Assert
        result.Should().Be("hello world");
    }

    [Fact]
    public void ParseText_RemoveSpecialCharactersTrue_ShouldKeepOnlyLettersAndSpaces()
    {
        // Arrange
        var text = "Hello, world! Ċongratulations & test.";
        var options = new TextParserOptions
        {
            ConvertToLowerCase = false,
            MinimumWordLength = 0,
            IgnoredWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        };

        // Act
        var result = _service.ParseText(text, options);

        // Assert
        result.Should().Be("Hello world Ċongratulations test");
    }

    [Fact]
    public void ParseText_RemoveWordsWithDigitsTrue_ShouldRemoveWordsContainingDigits()
    {
        // Arrange
        var text = "hello w0rld test1 123 foo";
        var options = new TextParserOptions
        {
            ConvertToLowerCase = false,
            MinimumWordLength = 0,
            IgnoredWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        };

        // Act
        var result = _service.ParseText(text, options);

        // Assert
        result.Should().Be("hello foo");
    }

    [Fact]
    public void ParseText_SingleSpaceBetweenWordsTrue_ShouldReduceMultipleSpaces()
    {
        // Arrange
        var text = "hello   world      test";
        var options = new TextParserOptions
        {
            ConvertToLowerCase = false,
            MinimumWordLength = 0,
            IgnoredWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        };

        // Act
        var result = _service.ParseText(text, options);

        // Assert
        result.Should().Be("hello world test");
    }

    [Fact]
    public void ParseText_IgnoredWords_ShouldRemoveSpecifiedWords()
    {
        // Arrange
        var text = "the quick brown fox jumps over the lazy dog";
        var options = new TextParserOptions
        {
            ConvertToLowerCase = false,
            MinimumWordLength = 0,
            IgnoredWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "the", "fox" }
        };

        // Act
        var result = _service.ParseText(text, options);

        // Assert
        result.Should().Be("quick brown jumps over lazy dog");
    }

    [Fact]
    public void ParseText_MinimumWordLength_ShouldRemoveWordsShorterThanMinimum()
    {
        // Arrange
        var text = "a ab abc abcd abcde";
        var options = new TextParserOptions
        {
            ConvertToLowerCase = false,
            MinimumWordLength = 3,
            IgnoredWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        };

        // Act
        var result = _service.ParseText(text, options);

        // Assert
        result.Should().Be("abc abcd abcde");
    }
    
    [Fact]
    public void ParseText_TextWithNewLines_ShouldTreatNewLinesAsSpaces()
    {
        // Arrange
        var text = "hello\nworld\r\ntest";
        var options = new TextParserOptions
        {
            ConvertToLowerCase = false,
            MinimumWordLength = 0,
            IgnoredWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        };

        // Act
        var result = _service.ParseText(text, options);

        // Assert
        result.Should().Be("hello world test");
    }

    [Fact]
    public void ParseText_ApostropheS_ShouldRemoveApostrophe()
    {
        // Arrange
        var text = "The market's performance is good.";
        var options = new TextParserOptions
        {
            ConvertToLowerCase = true,
            MinimumWordLength = 0,
            IgnoredWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        };

        // Act
        var result = _service.ParseText(text, options);

        // Assert
        result.Should().Be("the markets performance is good");
    }

    [Fact]
    public void ParseText_CombinedOptions_ShouldApplyAllRulesCorrectly()
    {
        // Arrange
        var text = "  The Quick1 Brown FOX! jumps   over the lazy dog.  \nAnd a new line.";
        var options = new TextParserOptions
        {
            ConvertToLowerCase = true,
            IgnoredWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "the", "dog." },
            MinimumWordLength = 4
        };

        // Act
        var result = _service.ParseText(text, options);

        // Assert
        result.Should().Be("brown jumps over lazy line");
    }
}