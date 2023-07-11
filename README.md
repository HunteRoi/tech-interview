# Capyx - Quick Tech Interview Project

## Context

Provided [a file](./AOC22/Day7/input.txt) containing a file system description like the following:
```
$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k
```
you are asked to create an implementation of the [`IParser` interface](./AOC22/Day7/IParser.cs).

There are several commands from the Linux OS that you should probably keep in mind for the parser :
- [ls](https://man7.org/linux/man-pages/man1/ls.1.html) - used to list the content of a directory
- [cd](https://man7.org/linux/man-pages/man1/cd.1p.html) - used to change the current directory

If you were to print the example above in a nicely shown tree view, it would look like this :
```
- / (dir)
  - a (dir)
    - e (dir)
      - i (file, size=584)
    - f (file, size=29116)
    - g (file, size=2557)
    - h.lst (file, size=62596)
  - b.txt (file, size=14848514)
  - c.dat (file, size=8504156)
  - d (dir)
    - j (file, size=4060174)
    - d.log (file, size=8033020)
    - d.ext (file, size=5626152)
    - k (file, size=7214296)
```

## Objective

Let's create a parser and test it **in pair programming**!

## Requirements

- You cannot change the `ÌParser` interface, not the `FileSystemEntry` class.
- You should use the provided `FileSystemEntry` class to represent a file system file or folder.
- You are to fully cover your code by writing tests.


## Notes
Resources : https://adventofcode.com/2022/day/7  
Solution : https://github.com/hunteroi/advent-of-code
