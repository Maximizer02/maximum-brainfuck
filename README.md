# Maximum Brainfuck

### Summary
This is my take on extending the Brainfuk interpreter beyond the rather limited default functionalities. Mbf is a complete superset of Brainfuck, meaning that the Mbf interpreter can handle all standard brainfuck code but is able to do more with the extended character set.


### Basic Brainfuck
- ">" : Go to Next cell in the Tape.
- "<" : Go to Previous cell in the Tape.
- "+" : Increment value of current cell.
- "-" : Decrement value of current cell.
- "." : Print Ascii Character associated with the value of the current cell.
- "," : Read Single Ascii Character from the console into the current cell. 
- "[" : Start Loop.
- "]" : End Loop.
### Maximum Brainfuck
- "_":  Floor curent cell to 0.
- "=":  Write Value of the cell whose index is the current cells value into the curent cell.
- ":":  Like ".", but prints the actual number instead of the character.
- "@":  Set Pointer to value of current cell.
- "(":  Start off callable Method.
- ")"   End off Method.
- "#"   Call Method of current Cell value. (1st Method:0, 2nd Method:1 etc)
- "?"   Put value of current cell into cache.
- "!"   Put cached value into current cell.
- "^"   Square the current value of the cell.