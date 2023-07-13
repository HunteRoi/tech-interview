using System.Text.RegularExpressions;

namespace AOC22.Day7;

public class Parser : IParser
{
    private static readonly Regex InstructionRegex = new(@"\$ (\w+) ?([\w/\.]+)?\n([^\$]+)?", RegexOptions.Multiline);
    private static readonly Regex OutputRegex = new(@"(dir|\d+) ([\w\.]+)", RegexOptions.Multiline);
    private const string ChangeDirectoryCommand = "cd";
    private const string ListCommand = "ls";
    private const string RootPath = "/";
    private const string ParentDirectoryRelativePath = "..";

    public FileSystemEntry Parse(string input)
    {
        if (!input.StartsWith($"$ {ChangeDirectoryCommand} {RootPath}")) throw new ArgumentException("The input does not begin with a cd command", nameof(input));

        var rootDirectory = FileSystemEntry.Folder(RootPath);
        FileSystemEntry? currentDirectory = null;

        var instructions = InstructionRegex.Matches(input);
        foreach (var (command, argument, output) in instructions.Select(instructionWithOutput => instructionWithOutput.Groups))
        {
            switch (command)
            {
                case ChangeDirectoryCommand:
                    currentDirectory = argument switch
                    {
                        RootPath => rootDirectory,
                        ParentDirectoryRelativePath => currentDirectory!.Parent,
                        _ => currentDirectory!.Directories.First(child => child.Name == argument)
                    };
                    break;
                
                case ListCommand:
                    currentDirectory!.AddChildren(ReadOutput(output).ToArray());
                    break;
            }
        }
        
        return rootDirectory;
    }

    private static IEnumerable<FileSystemEntry> ReadOutput(string entry) => OutputRegex.Matches(entry).Select(ReadOutput);

    private static FileSystemEntry ReadOutput(Match outputLine)
    {
        var name = outputLine.Groups[2].Value;
        return int.TryParse(outputLine.Groups[1].Value, out var size)
            ? FileSystemEntry.File(name, size)
            : FileSystemEntry.Folder(name);
    }
}