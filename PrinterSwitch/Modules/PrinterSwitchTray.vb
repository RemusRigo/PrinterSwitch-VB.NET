'--------------------------------------------------------------------------------------------------
' PrinterSwitch - A simple system tray application to quickly switch the default printer
'    © 2026 Remus Rigo
'       v1.0.2026-03-17
'--------------------------------------------------------------------------------------------------

Module PrinterSwitchTray

   <STAThread>
   Sub Main()
      Application.EnableVisualStyles()
      Application.SetCompatibleTextRenderingDefault(False)
      Application.Run(New PrinterSwitch())
   End Sub

End Module
