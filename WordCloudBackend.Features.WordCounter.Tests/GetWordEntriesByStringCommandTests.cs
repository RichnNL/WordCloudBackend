namespace WordCloudBackend.Features.WordCounter.Tests;

public class GetWordEntriesByStringCommandTests
{
    private readonly IWordCounterService _fakeWordCounterService;
    private readonly ITextParserService _fakeTextParserService;
    private readonly GetWordEntriesByStringCommandHandler _handler;

    public GetWordEntriesByStringCommandTests()
    {
        _fakeWordCounterService = A.Fake<IWordCounterService>();
        _fakeTextParserService = A.Fake<ITextParserService>();
        _handler = new GetWordEntriesByStringCommandHandler(_fakeWordCounterService, _fakeTextParserService);
    }

    [Fact]
    public async Task Handle_ShouldCallWordCounterWithParsedTextAndCorrectSortOrder()
    {
        // Arrange
        var rawText = "raw text";
        var parsedText = "parsed text";
        var command = new GetWordEntriesByStringCommand(rawText);
        
        A.CallTo(() => _fakeTextParserService.ParseText(rawText, A<TextParserOptions>._))
            .Returns(parsedText);

        // Act
        await _handler.Handle(command);

        // Assert
        A.CallTo(() => _fakeWordCounterService.CountWords(parsedText, WordEntrySortOrder.CountDescending))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_ShouldReturnWordEntriesFromWordCounterService()
    {
        // Arrange
        var expectedEntries = new List<WordEntry>
        {
            new("world", 2),
            new("hello", 1)
        };
        
        var command = new GetWordEntriesByStringCommand("any text");
        
        A.CallTo(() => _fakeWordCounterService.CountWords(A<string>._, A<WordEntrySortOrder>._))
            .Returns(expectedEntries);

        // Act
        var result = await _handler.Handle(command);

        // Assert
        result.Should().BeEquivalentTo(expectedEntries);
        result.Should().BeInDescendingOrder(x => x.Count);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Handle_NullOrWhitespaceText_ShouldStillCallServices(string emptyText)
    {
        // Arrange
        var command = new GetWordEntriesByStringCommand(emptyText);

        // Act
        await _handler.Handle(command);

        // Assert
        A.CallTo(() => _fakeTextParserService.ParseText(emptyText, A<TextParserOptions>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeWordCounterService.CountWords(A<string>._, A<WordEntrySortOrder>._)).MustHaveHappenedOnceExactly();
    }
}