# Vim Sample
Vim is a highly configurable, open-source text editor known for its efficiency and speed.<br/>
It's an improved version of the original vi editor, hence its name, "Vi Improved."  
  
We can run the vim by vim command.

```bash
# If the file.txt does not exist, the file will be created. 
vim file.txt
```

## Modes
**vim** operates in different modes.

- **Normal Mode**: This is the default mode when you open vim. In this mode, you can move the cursor, delete text, copy text, and perform other editing tasks.

- **Insert Mode**: This is where you can type and insert text. You enter Insert Mode from Normal Mode by pressing **i** (for inserting text before the cursor) or a (for inserting text after the cursor).

- **Visual Mode**: This mode allows you to select text. You enter Visual Mode from Normal Mode by pressing **v**.

- **Command-Line Mode**: This mode is used for executing commands like saving and quitting. You enter Command-Line Mode from Normal Mode by pressing **:**

##  Basic Commands in Normal Mode
### Navigation
- h: Move left
- j: Move down
- k: Move up
- l: Move right
- gg: Go to the beginning of the file
- G: Go to the end of the file
- w: Move to the beginning of the next word
- b: Move to the beginning of the previous word

### Editing
- i: Enter Insert Mode to start typing before the cursor
- a: Enter Insert Mode to start typing after the cursor
- x: Delete the character under the cursor
- dd: Delete the entire current line
- yy: Copy (yank) the current line
- p: Paste the copied text after the cursor
- u: Undo the last action
- Ctrl + r: Redo the undone action

### Saving and Quitting
- :w: Save the file
- :q: Quit vim
- :wq: Save and quit
- :q!: Quit without saving

### Entering and Exiting Insert Mode
- Press **i** in Normal Mode to enter Insert Mode.
- Press **Esc** to exit Insert Mode and return to Normal Mode.

### Visual Mode
Press **v** in Normal Mode to enter Visual Mode. You can now move the cursor to select text.

- Press **y** to yank (copy) the selected text.
- Press **d** to delete the selected text.
- Press **p** to paste the copied text.