# Maximum Brainfuck

### Summary
This is my take on extending the Brainfuck interpreter beyond the rather limited default functionalities. Mbf is a complete superset of Brainfuck, meaning that the Mbf interpreter can handle all standard brainfuck code but is able to do more with the extended character set.


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
- "_" : Floor curent cell to 0.
- "=" : Write Value of the cell whose index is the current cells value into the curent cell.
- ":" : Like ".", but prints the actual number instead of the character.
- "@" : Set Pointer to value of current cell.
- "(" : Start off callable Method.
- ")" : End off Method.
- "#" : Call Method of current Cell value. (1st Method:0, 2nd Method:1 etc)
- "?" : Put value of current cell into cache.
- "!" : Put cached value intos current cell.
- "^" : Square the current value of the cell.
- "*" : Multiply value of current cell with cached value.
- "/" : Divide value of current cell by cached value, value will be floored.
- "&" : Add cached value onto current cells value.
- "|" : Subtract cached value from current cells value.
- "§" : Like "#", but it calls the first method, regardless of the cells value.
- ";" : Like ",", but reads entire string into the Tape, starting at the Tape Pointer.
- "~" : Reset Tape and Pointer.
- "°" : Write cell index into current cell.
- "{" : Start off if clause, code in the brackets is executed when condition is true.
- "}" : Does nothing on its own, terminates an if clause.
- "$" : Conditon is true if the cells value is lower than the cached one, else false.
- "€" : Conditon is true if the cells value is higher than the cached one, else false.
- "µ" : Conditon is true if the cells value is equal to the cached one, else false.
- "\\" : Invert the current Condition value.
- '"' : Insert string into tape via code, inserts in the same way as ";".

### Demo code

#### Output Ascii number of Inputted character
    ,:

#### Fibonacci sequence
    (>>_+++^+.<<)+++++>>+<<[>&?:§>&:§?<<-]

#### Output Inputted string
    ;[.>]

#### Caesar cipher
    ;[+++.>]    

#### Print all cell numbers upto the number of the key you pressed
    ,?[>°:µ{_}]

#### Print cell numbers with line breaks (recursive)
    (°${:>_+++^+._§})++++^^?_§