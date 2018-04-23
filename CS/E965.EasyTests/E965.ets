#DropDB E965_EasyTest
#Application E965Web
#Application E965Win

*Action Navigation(Default.File System Store Object Demo)
*Action New
#IfDef E965Win
*ExecuteEditorAction File
#EndIf
#IfDef E965Web
*ExecuteEditorAction File(Browse)
#EndIf
*FillForm
 File name: = %windir%\explorer.exe
*HandleDialog
 Respond = Open
*CheckFieldValues
 File = *explorer.exe
;T224594 
*Action Save
*ExecuteEditorAction File(Open)
*Action Save and Close
*Action Navigation(Default.File System Store Object Demo)
*Sleep 500
*SelectRecords
 Columns = File
 Row = explorer.exe
#IfDef E965Win
*ActionAvailable Add From File...
*ActionAvailable Save As...
*ActionAvailable Open...
#EndIf
*Action Delete
*HandleDialog
 Respond = Yes



#IfDef E965Win
!ActionAvailable Open...
!ActionAvailable Save As...
*Action Add From File...
*FillForm
 File name: = %windir%\explorer.exe
*HandleDialog
 Respond = Open
*SelectRecords
 Columns = File
 Row = explorer.exe
*ActionAvailable Save As...
*ActionAvailable Open...
*Action Delete
*HandleDialog
 Respond = Yes

*Action New
*ExecuteEditorAction File
*FillForm
 File name: = %windir%\explorer.exe
*HandleDialog
 Respond = Open
*CheckFieldValues
 File = explorer.exe
;T224594 
*Action Save
*ExecuteEditorAction File(Open)
*Action Save and Close
*SelectRecords
 Columns = File
 Row = explorer.exe
*ActionAvailable Add From File...
*ActionAvailable Save As...
*ActionAvailable Open...
*ProcessRecord
 File = explorer.exe
*ExecuteEditorAction File(Clear Content)
!ExecuteEditorAction File(Open)
!ExecuteEditorAction File(Save As...)
!ExecuteEditorAction File(Clear Content)
*CheckFieldValues
 File = ''
*Action Cancel
*HandleDialog
 Respond = Yes
*CheckFieldValues
 File = explorer.exe
;*ExecuteEditorAction File(Open)

;B216326 (-)
;*Action Delete
;*HandleDialog
; Respond = Yes
;*Sleep 3000
*Action Save and Close
#EndIf

;B222892
*Action Navigation(Default.File System Store Object Demo)
*Action New
#IfDef E965Win
*ExecuteEditorAction File
#EndIf
#IfDef E965Web
*ExecuteEditorAction File(Browse)
#EndIf
*FillForm
 File name: = %windir%\explorer.exe
*HandleDialog
 Respond = Open
*Action Save and Close
*Action Navigation(Default.File System Store Object Demo)
*Sleep 500
*ProcessRecord
 File = explorer.exe
#IfDef E965Win
*ExecuteEditorAction File(Clear Content)
*ExecuteEditorAction File
#EndIf
#IfDef E965Web
*Action Edit
*ExecuteEditorAction File(Clear)
*ExecuteEditorAction File(Browse)
#EndIf
*FillForm
 File name: = %windir%\explorer.exe
*HandleDialog
 Respond = Open
*Action Save
*ExecuteEditorAction File(Open)

*Action Navigation(Default.File System Store Object Demo)
*Action Test
*ActionVisible Succeeded

;T352882
*Sleep 500
*ProcessRecord
 File = explorer.exe
*Action Delete
*HandleDialog
 Respond = Yes
