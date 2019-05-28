# Note: I am no longer maintaining this frontend. For an up-to-date version which works with the latest XQEMU, see xqemu-manager -> https://github.com/xqemu/xqemu-manager

# XQEMU Frontend
This is a small Windows utility coded in C# which helps ease the process of launching Xbox ISOs/Dashboards through XQEMU. A friend requested this on Discord, so I decided to give it a shot.

This was my first ever attempt at developing a C# application in my life, so forgive me for the slightly unforgiving, messy code! ;P

![alt text](screenshot.png "Screenshot of the main GUI")

Usage:
======
Very simple usage; just browse for the appropriate files and hit "Start XQEMU". If you would like to view the full BIOS boot animation, tick the "Show Full Boot Animation" checkbox at the bottom to do so.
- If 'qemu-system-xbox.exe' isn't located in the same directory as the frontend, a file dialog window will appear asking you to locate the XQEMU executable.
- Before it will allow you to run XQEMU, the frontend requires you to have selected the BIOS, MCPX, and harddisk image. If you wish to boot to the dashboard, leave the ISO textbox empty.

Enjoy!
