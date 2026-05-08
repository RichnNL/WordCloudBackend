namespace WordCloudBackend.Features.WordCounter.Tests;

public class WordCounterServiceTests
{
    [Fact]
    public void CountWords_ShouldCountFrequenciesCorrectly()
    {
        // Arrange
        var service = new WordCounterService();
        var text = "hello world hello";

        // Act
        var result = service.CountWords(text, WordEntrySortOrder.CountDescending);

        // Assert
        result.Should().HaveCount(2);
        
        result[0].Word.Should().Be("hello");
        result[0].Count.Should().Be(2);
        
        result[1].Word.Should().Be("world");
        result[1].Count.Should().Be(1);
    }
    
    [Fact]
    public void CountWords_WhenSortOrderIsDescending_ShouldReturnWordsInDescendingOrder()
    {
        // Arrange
        var service = new WordCounterService();
        var text = "hello world world";

        // Act
        var result = service.CountWords(text, WordEntrySortOrder.CountDescending);

        // Assert
        result[0].Word.Should().Be("world");
        result[0].Count.Should().Be(2);
        
        result[1].Word.Should().Be("hello");
        result[1].Count.Should().Be(1);
    }
    
    [Fact]
    public void CountWords_WhenSortOrderIsAscending_ShouldReturnWordsInAscendingOrder()
    {
        // Arrange
        var service = new WordCounterService();
        var text = "hello world world";

        // Act
        var result = service.CountWords(text, WordEntrySortOrder.CountAscending);

        // Assert
        result[0].Word.Should().Be("hello");
        result[0].Count.Should().Be(1);
        
        result[1].Word.Should().Be("world");
        result[1].Count.Should().Be(2);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void CountWords_NullOrEmptyText_ShouldThrowArgumentException(string text)
    {
        // Arrange
        var service = new WordCounterService();

        // Act
        Action act = () => service.CountWords(text, WordEntrySortOrder.CountDescending);

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}