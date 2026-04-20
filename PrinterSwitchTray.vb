Module PrinterSwitchTray

   <STAThread>
   Sub Main()
      Application.EnableVisualStyles()
      Application.SetCompatibleTextRenderingDefault(False)
      Application.Run(New PrinterSwitch())
   End Sub

End Module
