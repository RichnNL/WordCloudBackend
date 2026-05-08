namespace WordCloudBackend.Features.WordCounter.Tests;

public class GetWordEntriesByStringCommandTests
{
    [Fact]
    public async Task Handle_ShouldCallServiceWithCorrectTextAndSortOrder()
    {
        // Arrange
        var testText = "hello world";
        var expectedResult = AutoBogus.AutoFaker.Generate<WordEntry>(2);
        
        var fakeService = A.Fake<IWordCounterService>();
        A.CallTo(() => fakeService.CountWords(testText, WordEntrySortOrder.CountDescending)).Returns(expectedResult);
        
        var command = new GetWordEntriesByStringCommand(testText);
        var handler = new GetWordEntriesByStringCommandHandler(fakeService);

        // Act
        var result = await handler.Handle(command);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
        A.CallTo(() => fakeService.CountWords(testText, WordEntrySortOrder.CountDescending)).MustHaveHappenedOnceExactly();
    }
}