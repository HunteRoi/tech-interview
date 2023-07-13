using AOC22.Day7;
using System;
using FluentAssertions;
using Xunit;

namespace AOC22.Tests.Day7;

public class ParserShould : IDisposable
{
    private IParser _parser;

    #region BeforeEach/AfterEach

    public ParserShould()
    {
        _parser = new Parser();
    }

    public void Dispose()
    {
        _parser = null!;
        GC.SuppressFinalize(this);
    }

    #endregion
    
    [Theory(DisplayName = "Given an input not starting with a \"cd\" command, it should throw an ArgumentException")]
    [InlineData("$ ls")]
    [InlineData("$ ls\n$ cd /")]
    public void ParseShouldThrowArgumentExceptionWhenInputDoesNotStartWithCdCommand(string input)
    {
        var act = () => _parser.Parse(input);

        act.Should().Throw<ArgumentException>("the file system's input lecture must start with a \"cd /\"");
    }

    [Fact(DisplayName =
        "Given a \"cd\" command at first, not pointing to the root directory, it should throw an ArgumentException")]
    public void ParseShouldThrowArgumentExceptionWhenInputDoesNotStartOnRootDirectory()
    {
        const string input = "$ cd a";
        var act = () => _parser.Parse(input);

        act.Should().Throw<ArgumentException>("the file system's input must start at root directory");
    }

    [Fact(DisplayName = "Given a \"cd /\" command, it should return a file system entry representing the root directory")]
    public void ParseCdAndReturnDirectory()
    {
        const string input = "$ cd /";
        var expected = FileSystemEntry.Folder("/");

        var actual = _parser.Parse(input);

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact(DisplayName = "Given the \"ls\" command, it should return a file system entry representing the root directory's content")]
    public void ParseCdAndLsAndReturnDirectoryAndContent()
    {
        const string input = "$ cd /\n$ ls\ndir a\n14848514 b.txt\n8504156 c.dat\ndir d";
        var expected = FileSystemEntry.Folder("/").AddChildren(
            FileSystemEntry.Folder("a"),
            FileSystemEntry.File("b.txt", 14_848_514),
            FileSystemEntry.File("c.dat", 8_504_156),
            FileSystemEntry.Folder("d")
        );

        var result = _parser.Parse(input);

        result.Should().BeEquivalentTo(expected, options => options.IgnoringCyclicReferences());
    }

    [Fact(DisplayName =
        "Given several depths of commands, it should return a file system entry representing the entire root directory's content recursively")]
    public void ParseCdAndLsAndReturnDirectoryAndContentRecursively()
    {
        const string input = "$ cd /\n$ ls\ndir a\n14848514 b.txt\n8504156 c.dat\ndir d\n$ cd a\n$ ls\ndir e\n29116 f\n2557 g\n62596 h.lst";
        var expected = FileSystemEntry.Folder("/").AddChildren(
            FileSystemEntry.Folder("a").AddChildren(
                FileSystemEntry.Folder("e"),
                FileSystemEntry.File("f", 29_116),
                FileSystemEntry.File("g", 2_557),
                FileSystemEntry.File("h.lst", 62_596)
            ),
            FileSystemEntry.File("b.txt", 14_848_514),
            FileSystemEntry.File("c.dat", 8_504_156),
            FileSystemEntry.Folder("d")
        );

        var result = _parser.Parse(input);

        result.Should().BeEquivalentTo(expected, options => options.IgnoringCyclicReferences());
    }
}