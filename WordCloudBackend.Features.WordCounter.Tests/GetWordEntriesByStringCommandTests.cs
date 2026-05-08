namespace WordCloudBackend.Features.WordCounter.Tests;

public class GetWordEntriesByStringCommandTests
{
    [Fact]
    public async Task Handle_ShouldCallServicesWithCorrectArguments()
    {
        // Arrange
        var rawText = "   Hello   World123!  ";
        var parsedText = "hello";
        var expectedResult = AutoBogus.AutoFaker.Generate<WordEntry>(2);
        
        var fakeTextParserService = A.Fake<ITextParserService>();
        A.CallTo(() => fakeTextParserService.ParseText(rawText, A<TextParserOptions>._)).Returns(parsedText);

        var fakeWordCounterService = A.Fake<IWordCounterService>();
        A.CallTo(() => fakeWordCounterService.CountWords(parsedText, WordEntrySortOrder.CountDescending)).Returns(expectedResult);
        
        var command = new GetWordEntriesByStringCommand(rawText);
        var handler = new GetWordEntriesByStringCommandHandler(fakeWordCounterService, fakeTextParserService);

        // Act
        var result = await handler.Handle(command);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
        A.CallTo(() => fakeTextParserService.ParseText(rawText, A<TextParserOptions>._)).MustHaveHappenedOnceExactly();
        
        A.CallTo(() => fakeWordCounterService.CountWords(parsedText, WordEntrySortOrder.CountDescending)).MustHaveHappenedOnceExactly();
    }
}