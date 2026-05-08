namespace WordCloudBackend.Features.WordCounter.Tests;

public class GetWordEntriesByStringCommandTests
{
    [Fact]
    public void Execute_ShouldReturnDummyData()
    {
        // Arrange
        var command = new GetWordEntriesByStringCommand();

        // Act
        var result = command.Execute("hello world");

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        
        result[0].Word.Should().Be("world");
        result[0].Count.Should().Be(2);
        
        result[1].Word.Should().Be("hello");
        result[1].Count.Should().Be(1);
    }
}