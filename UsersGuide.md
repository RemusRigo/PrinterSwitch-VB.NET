# PrinterSwitch: User's Guide



## Downloads & Runtimes

Download the latest release from the [Releases page](https://github.com/RemusRigo/PrinterSwitch-VB.NET/releases).

|Build|Architecture|Required Runtime|
|-|-|-|
|`x64`|64-bit|none (self-contained single file)|
|`x86`|32-bit|none (self-contained single file)|

> On 64-bit Windows 10/11, use **x64**. Each build is a single `PrinterSwitch.exe` that already contains .NET 10,
> so there is nothing else to install.



## Install

PrinterSwitch is portable: there is no installer.

1. Copy `PrinterSwitch.exe` to a permanent folder, for example `D:\My Apps\PrinterSwitch\`.
2. Double-click `PrinterSwitch.exe`. A printer icon appears in the system tray.
3. Optional: to start it with Windows, press **Win+R**, type `shell:startup`, and put a shortcut to `PrinterSwitch.exe` in that folder.

> If Windows blocks the file ("Windows protected your PC"), right-click `PrinterSwitch.exe` →
> **Properties** → check **Unblock** → **OK**, then try again.



## Usage

Click the tray icon (left or right button) to open the menu:

|Menu|Description|
|-|-|
|*printer names*|All installed printers; the default printer is checked and **bold**. Click a printer to make it the default|
|**Auto Refresh**|When checked, the printer list is read again every time you open the menu|
|**Rebuild menu**|Read the printer list again now (e.g. after you install a printer)|
|**About**|Version information|
|**Exit**|Close PrinterSwitch|

> If Windows changes your default printer by itself, turn off **Settings → Bluetooth & devices →
> Printers & scanners → Let Windows manage my default printer**.



## Uninstall

1. Click the tray icon → **Exit**.
2. Delete the shortcut from `shell:startup` (if you created one) and the folder you copied `PrinterSwitch.exe` to.
