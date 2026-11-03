# Text Editor with Stack and Queue

A tiny text editor with write, undo, redo and show, driven by a command string.

## How it works

- The input `w Data w MINING u w Structures s` is split into commands and put into a queue (`CommandQ`).
- The commands are then taken from the queue one by one and executed:
  - `w` writes the next word
  - `u` undoes the last write
  - `r` redoes an undone write
  - `s` shows the current text
- Two stacks (`TextStack`) hold the history: `undoStack` for written words and `redoStack` for undone words. A new write clears the redo stack.

## Data structures

- `TextStack`: array based stack
- `CommandQ`: circular array queue

## Run

Open the `.sln` file in this folder with Visual Studio and press Ctrl+F5.

## Output

```
Data Structures 
```
