Imports System.Drawing.Printing
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Public Class PrinterSwitch
   Inherits ApplicationContext

   Private TrayIcon As NotifyIcon
   Private contextMenu As ContextMenuStrip
   Private mnuAutoRefresh As ToolStripMenuItem

   <DllImport("winspool.drv", SetLastError:=True, CharSet:=CharSet.Auto)>
   Private Shared Function SetDefaultPrinter(printerName As String) As Boolean
   End Function

   '-----------------------------------------------------------------------------------------------
   ' Build context menu
   Private Sub BuildPrinterMenu()
      ' Preserve Auto Refresh state if menu already exists
      Dim autoRefresh As Boolean = If(mnuAutoRefresh IsNot Nothing, mnuAutoRefresh.Checked, False)

      contextMenu?.Dispose()
      contextMenu = New ContextMenuStrip()

      For Each printerName As String In PrinterSettings.InstalledPrinters
         Dim item As New ToolStripMenuItem(printerName)
         item.Tag = printerName
         AddHandler item.Click, AddressOf PrinterMenuItem_Click
         contextMenu.Items.Add(item)
      Next

      ' -----------------------------------------
      contextMenu.Items.Add(New ToolStripSeparator())

      ' Auto Refresh 
      mnuAutoRefresh = New ToolStripMenuItem("Auto Refresh")
      mnuAutoRefresh.Checked = autoRefresh
      mnuAutoRefresh.CheckOnClick = True
      contextMenu.Items.Add(mnuAutoRefresh)

      Dim mmuRebuild As New ToolStripMenuItem("Rebuild menu")
      AddHandler mmuRebuild.Click, Sub()
                                      BuildPrinterMenu()
                                   End Sub
      contextMenu.Items.Add(mmuRebuild)

      ' -----------------------------------------
      contextMenu.Items.Add(New ToolStripSeparator())

      ' About
      Dim mnuAbout As New ToolStripMenuItem("About")
      AddHandler mnuAbout.Click, Sub()
                                    Dim frm As New frmAbout
                                    frm.Show()
                                 End Sub
      contextMenu.Items.Add(mnuAbout)

      ' Exit
      Dim mnuExit As New ToolStripMenuItem("Exit")
      AddHandler mnuExit.Click, Sub()
                                   TrayIcon.Visible = False
                                   Application.Exit()
                                End Sub
      contextMenu.Items.Add(mnuExit)

      TrayIcon.ContextMenuStrip = contextMenu

      MarkDefaultPrinter()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' OnClick handler for printer menu items
   Private Sub PrinterMenuItem_Click(sender As Object, e As EventArgs)
      Dim item As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
      Dim selectedPrinter As String = item.Tag.ToString()
      'Shell($"rundll32 printui.dll,PrintUIEntry /y /n ""{selectedPrinter}""")
      SetDefaultPrinter(selectedPrinter)
      MarkDefaultPrinter()
   End Sub

   '-----------------------------------------------------------------------------------------------
   '  Mark the default printer in the menu
   Private Sub MarkDefaultPrinter()
      Dim defaultPrinter As String = New PrinterSettings().PrinterName

      For Each item As ToolStripMenuItem In contextMenu.Items.OfType(Of ToolStripMenuItem)()
         If item.Tag IsNot Nothing Then
            Dim isDefault As Boolean = item.Tag.ToString() = defaultPrinter
            item.Checked = isDefault
            item.Font = New Font(item.Font, If(isDefault, FontStyle.Bold, FontStyle.Regular))
         End If
      Next
   End Sub

   '-----------------------------------------------------------------------------------------------
   '  Tray icon click handler
   Private Sub TrayIcon_MouseClick(sender As Object, e As MouseEventArgs)
      If mnuAutoRefresh IsNot Nothing AndAlso mnuAutoRefresh.Checked Then
         BuildPrinterMenu()
      Else
         MarkDefaultPrinter()  ' always refresh checkmarks at minimum
      End If

      ' Show menu on both left and right click
      TrayIcon.ContextMenuStrip = contextMenu
      Dim mi As MethodInfo = GetType(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance Or BindingFlags.NonPublic)
      mi?.Invoke(TrayIcon, Nothing)
   End Sub

   Public Sub New()
      ' Create tray icon
      TrayIcon = New NotifyIcon()
      TrayIcon.Icon = My.Resources.Resources.Printer
      TrayIcon.Visible = True
      TrayIcon.ContextMenuStrip = contextMenu
      TrayIcon.Text = "PrinterSwitch v1.0"

      BuildPrinterMenu()
      AddHandler TrayIcon.MouseClick, AddressOf TrayIcon_MouseClick
   End Sub

End Class
