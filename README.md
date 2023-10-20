# Maximum Brainfuck

### Summary
This is my take on extending the Brainfuk interpreter beyond the rather limited default functionalities. Mbf is a complete superset to Brainfuck, meaning that the Mbf interpreter can handle all standard brainfuck code but is able to do more with the extended character set.


### Basic Brainfuck
- ">" : Go to Next cell in the Tape.
- "<" : Go to Previous cell in the Tape.
- "+" : Increment value of current cell.
- "-" : Decrement value of current cell.
- "." : Print Ascii Character associated with value of current cell.
- "," : Read Single Ascii Character from console into current cell. 
- "[" : Start Loop.
- "]" : End Loop.
### Maximum Brainfuck
- "_":  Floor curent cell to 0.
- "@":  Write Value of the cell whos index is the current cells value into the curent cell.
- ":":  Like ".", but prints the actual number instead of the character
- "=":  Set Pointer to value of current cell 

##### might flip @ and = idk