namespace WordCloudBackend.Features.WordCounter.Tests;

public class WordCounterServiceTests
{
    [Fact]
    public void CountWords_WhenSortOrderIsDescending_ShouldReturnWordsInDescendingOrder()
    {
        // Arrange
        var service = new WordCounterService();

        // Act
        var result = service.CountWords("hello world", WordEntrySortOrder.CountDescending);

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

        // Act
        var result = service.CountWords("hello world", WordEntrySortOrder.CountAscending);

        // Assert
        result[0].Word.Should().Be("hello");
        result[0].Count.Should().Be(1);
        
        result[1].Word.Should().Be("world");
        result[1].Count.Should().Be(2);
    }
}