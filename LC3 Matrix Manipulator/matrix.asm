;Free to modify x4000 - x5000
.ORIG x3000
;Save Initial register values
;LD R0, Start
;STR R0, R0, #0
;ADD R0, R0, #1
;STR R1, R0, #0
;ADD R0, R0, #1
;STR R2, R0, #0
;ADD R0, R0, #1
;STR R3, R0, #0
;ADD R0, R0, #1
;STR R4, R0, #0
;ADD R0, R0, #1
;STR R5, R0, #0
;ADD R0, R0, #1
;STR R6, R0, #0
;ADD R0, R0, #1
;STR R7, R0, #0

;Load Operation value to R0 and determine label to jump to
LDI R0, OP
ADD R1, R0, #-6
BRzp INVALIDOP   ;Check for invalid op greater than 5
ADD R1, R0, #-1
BRz LSHIFT
ADD R1, R0, #-2
BRz RSHIFT
ADD R1, R0, #-3
BRz USHIFT
ADD R1, R0, #-4
BRz DSHIFT
ADD R1, R0, #-5
BRz TRANSPOSE
BR DONE

RSHIFT 
LDI R1, Shift
LDI R2, Cols
LDI R3, Rows
ADD R0, R1, R2 ;Check if amount to shift = width
BRz DONE

LD R0, Matrix  ;Initialize Current position at end of beginning of matrix
ADD R0, R0, R2
ADD R0, R0, #-1
LOOP1
LDI R2, Cols  ;Reload initial col value to R2
STI R0, EndValue  ;Load last value of the row to EndValue
ADD R0, R0, -1 ;Decrement once outside of loop
INLOOP1 
STR R0, R0, #1   ;Save pos to (pos+1)
ADD R0, R0, #-1 ;Shift Pointer Left
Add R2, R2, #-1 ;Decrement col count
BRzp INLOOP1     ;Continue inter loop until pos pointer hits beginning of row -1
LDI R5, EndValue ;Load end value to a temp register, R5
ADD R0, R0, #1 ;Shift R0 up 1 back to begining of row
STI R0, EndValue ;Replace the first value of the row with EndValue
ADD R1, R1, #-1   ;Decerement Shift counter
BRp LOOP1         ;If shift counter is above zero, loop back to outer loop1
ADD R3, R3, #-1  ;Decrement row counter
BRp LOOP1  ;If row counter is above zero, loop back to outer loop1
BR DONE 





INVALIDOP AND, R0, R0, #0
LSHIFT AND, R0, R0, #0
USHIFT AND, R0, R0, #0
DSHIFT AND, R0, R0, #0
TRANSPOSE AND, R0, R0, #0
;Throw Error



DONE TRAP x25  
;Labels
Rows   .FILL x5000
Cols   .FILL x5001
OP     .FILL x5002
Shift  .FILL x5003
Matrix .FILL x5004
Start  .FILL x4000 ;Store registers in x4000 - x4007
Tmp1   .FILL x4010 
EndValue .FILL x4011


	.END