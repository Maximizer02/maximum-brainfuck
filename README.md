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
- ";" : Like ",", but reads entire string into the Tape, starting at the Tape Pointer. Works with Integer literals.
- "~" : Reset Tape and Pointer.
- "°" : Write cell index into current cell.
- "{" : Start off if clause, code in the brackets is executed when condition is true.
- "}" : Does nothing on its own, terminates an if clause.
- "$" : Conditon is true if the cells value is lower than the cached one, else false.
- "€" : Conditon is true if the cells value is higher than the cached one, else false.
- "µ" : Conditon is true if the cells value is equal to the cached one, else false.
- "%" : Condition is true when the current cells value is divisible by he cached value.
- "\\" : Invert the current Condition value.
- '"' : Insert string into tape via code, inserts in the same way as ";". Can also be used for Integer Literals with "0d\\"(decimal), "0b\\"(binary) and "0x\\"(hexadecimal)
- "´" : Call a Method by Name. Name defined directly after the \( in declaration.


### Demo code

#### Output Ascii number of Inputted character
    ,:

#### Fibonacci sequence (eg. 10)
    (>>_+++^+.<<)+++++>>+<<[>&?:§>&:§?<<-]

#### Output Inputted string
    ;[.>]

#### Caesar cipher
    ;[+++.>]    

#### Print all cell numbers upto the number of the key you pressed
    ,?[>°:µ{_}]

#### Print cell numbers with line breaks (recursive, eg. 255)
    (°${:>_+++^+._§})++++^^?_§

#### If Else Statement (eg. Whole number check)
    ++?,%{"Yes"[.>]}\{"No"[.>]}

#### FizzBuzz
    (+++)§^+^?[!>_§?°%{"Fizz"[.>]<<<<}_§++?°%{"Buzz"[.>]<<<<}_§?°%\{_§++?°%\{:}}<?_§^+.>µ{_}]

#### Print reversed String
    ;[>]°?[<.!-?]

#### Factorial Numbers (eg. 6)
    ++++++?>+>!<<[>>?-<*<-]>:

#### Integer Literals
    "0d\420":   
        Prints the Number 420
    "0x\BB":
        Prints the Number 187
    "0b\1000101"
        Prints the Number 69

#### Method selection (0d\1: 'First Method' etc. )
    (_"First Method"[.>])(_"Second Method"[.>])(_"Third Method"[.>]);-#

#### Method access by name
    (br "0d\10".)(p [.>]´br´)(foo "bar"´p´)(bar "foo"´p´)´bar´´foo´

### To Be Done / Ideas
- Functions with parameters (kinda like APL)
- \-e for extended character set
- \-r for reduced character set
- \-c for compacted character set
- \-d for default brainfuck set
- \-v for version