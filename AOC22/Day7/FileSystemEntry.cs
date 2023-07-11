using System.Text;

namespace AOC22.Day7;

public sealed class FileSystemEntry
{
    #region Private (constructors, fields, methods)

    private readonly int? _size;

    private FileSystemEntry(string name, int? size = null)
    {
        Name = name;
        _size = size;
        Children = new List<FileSystemEntry>();
    }

    private IList<FileSystemEntry> Children { get; }

    private bool IsFile() => _size.HasValue;

    #endregion Private (constructors, fields, methods)

    #region Public methods

    public IList<FileSystemEntry> Directories => Children.Where(child => !child.IsFile()).ToList();
    public string Name { get; }
    public FileSystemEntry? Parent { get; private set; }

    public int Size => _size ?? Children.Sum(child => child.Size);

    public FileSystemEntry AddChild(FileSystemEntry entry)
    {
        if (IsFile()) return this;

        entry.Parent = this;
        Children.Add(entry);

        return this;
    }

    public FileSystemEntry AddChildren(params FileSystemEntry[] entries)
    {
        foreach (var entry in entries)
        {
            AddChild(entry);
        }

        return this;
    }

    public override string ToString()
    {
        return $"{Name} ({(IsFile() ? $"file, size={_size}" : "dir")})";
    }

    public string ToTree(int offset = 0)
    {
        var builder = new StringBuilder();

        var value = $"- {this}";
        builder.Append(value.PadLeft(value.Length + offset));
        if (!Children.Any()) return builder.ToString();

        builder.Append('\n');
        builder.Append(string.Join("\n", Children.Select(child => child.ToTree(offset + 2))));
        return builder.ToString();
    }

    #endregion Public methods

    #region Static builders

    private static IEnumerable<FileSystemEntry> ConstructEnumerable(FileSystemEntry entry, Func<int, bool> predicate)
    {
        var directories = new List<FileSystemEntry>();
        if (entry.IsFile()) return directories;

        if (predicate(entry.Size)) directories.Add(entry);
        directories.AddRange(entry.Children.SelectMany(child => ConstructEnumerable(child, predicate)));

        return directories;
    }

    public static FileSystemEntry File(string name, int size)
    {
        return new FileSystemEntry(name, size);
    }

    public static FileSystemEntry Folder(string name)
    {
        return new FileSystemEntry(name);
    }

    #endregion Static builders
}