# Quick Tech Interview Project


## Context

There was an error with our Linux file system. We lost everything. Fortunately for us, we found some old backup text file containing the description of our filesystem. 
Here's a sample of the backup file we could read :
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

But we have a problem! The backup automated system is broken, and we don't know how to repair it. **We need you to help us create a new one!**  


Could you please help us write the necessary implementation of the [`IParser` interface](./AOC22/Day7/IParser.cs) ?  


## Getting started

These two commands from the Linux OS are found in the file we received from our backup administrator :
- [ls](https://man7.org/linux/man-pages/man1/ls.1.html) - used to list the content of a directory
- [cd](https://man7.org/linux/man-pages/man1/cd.1p.html) - used to change the current directory to the provided argument

**Objective:** We would like you to write a parser with the help of our in-house partner (pair programming).

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


## Requirements

- You must use the provided `FileSystemEntry` class to represent a file system file or folder.
- You cannot change the `ÌParser` interface, nor the `FileSystemEntry` class.
- The `FileSystemEntry` class uses the builder pattern to ease creating files and folders. Use it at your advantage!
- **You are to fully cover your code by writing tests.**
- You should pair with our in-house partner to make sure everything's going good!


<details>
<summary>

## Hints

</summary>

- A command starts with a `$` followed by a space, the command itself and probably some arguments. Perhaps a regex might help?
- A recursive algorithm could be an idea. Looping through the entire input is possible too! 😉

</details>


## Source

Resources : https://adventofcode.com/2022/day/7  

