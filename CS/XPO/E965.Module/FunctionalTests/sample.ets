;#DropDB E965EasyTest

#Application E965Win
#Application E965Blazor
#IfDef E965Win

*Action About...
 
*Action OK

#EndIf

#IfDef E965Blazor
!Action Navigation(My Details)
#EndIf
