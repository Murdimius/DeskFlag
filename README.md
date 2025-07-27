# DeskFlag

A lightweight, proudly animated flag that lives in your system tray and waves on your desktop — because nothing says patriotism like a few extra CPU cycles.

## 🇺🇸 What It Does

- Animates a flag in the bottom-right corner of your desktop
- Sits quietly in your system tray
- Exits cleanly when you’re done being patriotic

## 🧰 Requirements

- Windows 10 or later
- .NET 8/9 (unless you use the standalone EXE from Releases)

## 🚀 How to Run

### Option 1: Prebuilt

1. Download the latest ZIP from [Releases](https://github.com/Murdimius/DeskFlag/releases)
2. Unzip it wherever you want
3. Run `DeskFlag.exe`
4. Right-click the tray flag icon to exit

### Option 2: Build It Yourself

```bash
git clone https://github.com/Murdimius/DeskFlag.git
cd DeskFlag
dotnet build -c Release
