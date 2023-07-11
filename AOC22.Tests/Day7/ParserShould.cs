using AOC22.Day7;
using System;
using Xunit;

namespace AOC22.Tests.Day7;

public class ParserShould
{
    private IParser _parser;

    [Fact(DisplayName = "Given the \"cd\" and \"ls\" commands once, it should return a file system entry representing the root directory and its content of depth 1")]
    public void ParseCdAndLsAndReturnDirectoryAndContent()
    {
        throw new NotImplementedException();
    }

    [Fact(DisplayName = "Given the \"cd /\" command, it should return a file system entry representing the root directory")]
    public void ParseCdAndReturnDirectory()
    {
        throw new NotImplementedException();
    }

    [Fact(DisplayName = "Given the \"cd\" and \"ls\" commands several times, it should return a file system entry representing the root directory and its content of depth 2")]
    public void ParseWholeStructureAndReturnTree()
    {
        throw new NotImplementedException();
    }
}