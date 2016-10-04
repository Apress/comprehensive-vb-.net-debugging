VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "ComPrinting"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Option Base 0

Private Type PALETTEENTRY
   peRed As Byte
   peGreen As Byte
   peBlue As Byte
   peFlags As Byte
End Type

Private Type LOGPALETTE
   palVersion As Integer
   palNumEntries As Integer
   palPalEntry(255) As PALETTEENTRY  ' Enough for 256 colors.
End Type

Private Type GUID
   Data1 As Long
   Data2 As Integer
   Data3 As Integer
   Data4(7) As Byte
End Type

Private Const RASTERCAPS As Long = 38
Private Const RC_PALETTE As Long = &H100
Private Const SIZEPALETTE As Long = 104

Private Type RECT
   Left As Long
   Top As Long
   Right As Long
   Bottom As Long
End Type

Private Declare Function CreateCompatibleDC Lib "GDI32" (ByVal hDC As Long) As Long
Private Declare Function CreateCompatibleBitmap Lib "GDI32" (ByVal hDC As Long, _
                                                             ByVal nWidth As Long, _
                                                             ByVal nHeight As Long) As Long
Private Declare Function GetDeviceCaps Lib "GDI32" (ByVal hDC As Long, ByVal iCapability As Long) As Long
Private Declare Function GetSystemPaletteEntries Lib "GDI32" (ByVal hDC As Long, _
                         ByVal wStartIndex As Long, ByVal wNumEntries As Long, _
                         lpPaletteEntries As PALETTEENTRY) As Long
Private Declare Function CreatePalette Lib "GDI32" (lpLogPalette As LOGPALETTE) As Long
Private Declare Function SelectObject Lib "GDI32" (ByVal hDC As Long, ByVal hObject As Long) As Long
Private Declare Function BitBlt Lib "GDI32" (ByVal hDCDest As Long, ByVal XDest As Long, _
                         ByVal YDest As Long, ByVal nWidth As Long, _
                         ByVal nHeight As Long, ByVal hDCSrc As Long, _
                         ByVal XSrc As Long, ByVal YSrc As Long, ByVal dwRop As Long) As Long
Private Declare Function DeleteDC Lib "GDI32" (ByVal hDC As Long) As Long
Private Declare Function GetForegroundWindow Lib "USER32" () As Long
Private Declare Function SelectPalette Lib "GDI32" (ByVal hDC As Long, ByVal hPalette As Long, _
                         ByVal bForceBackground As Long) As Long
Private Declare Function RealizePalette Lib "GDI32" (ByVal hDC As Long) As Long
Private Declare Function GetWindowDC Lib "USER32" (ByVal hWnd As Long) As Long
Private Declare Function GetDC Lib "USER32" (ByVal hWnd As Long) As Long
Private Declare Function GetWindowRect Lib "USER32" (ByVal hWnd As Long, lpRect As RECT) As Long
Private Declare Function ReleaseDC Lib "USER32" (ByVal hWnd As Long, ByVal hDC As Long) As Long
Private Declare Function GetDesktopWindow Lib "USER32" () As Long

Private Type PicBmp
   Size As Long
   Type As Long
   hBmp As Long
   hPal As Long
   Reserved As Long
End Type

Private Declare Function OleCreatePictureIndirect Lib "olepro32.dll" _
                        (PicDesc As PicBmp, RefIID As GUID, ByVal fPictureOwnsHandle As Long, _
                        IPic As IPicture) As Long

Public Function CreateBitmapPicture(ByVal hBmp As Long, ByVal hPal As Long) As Picture
'Creates a bitmap type picture object from a bitmap and palette.
Dim r As Long, Pic As PicBmp, IPic As IPicture, IID_IDispatch As GUID

'Fill in with IDispatch Interface ID.
With IID_IDispatch
   .Data1 = &H20400
   .Data4(0) = &HC0
   .Data4(7) = &H46
End With

'Fill Pic with necessary parts.
With Pic
   .Size = Len(Pic)          ' Length of structure.
   .Type = vbPicTypeBitmap   ' Type of Picture (bitmap).
   .hBmp = hBmp              ' Handle to bitmap.
   .hPal = hPal              ' Handle to palette (may be null).
End With

'Create picture object.
r = OleCreatePictureIndirect(Pic, IID_IDispatch, 1, IPic)

'Return the new Picture object.
Set CreateBitmapPicture = IPic

End Function

Public Function CaptureWindow(ByVal hWndSrc As Long, ByVal Client As Boolean, _
                              ByVal LeftSrc As Long, ByVal TopSrc As Long, _
                              ByVal WidthSrc As Long, ByVal HeightSrc As Long) As Picture
'Captures any portion of a window.
Dim hDCMemory As Long, hBmp As Long, hBmpPrev As Long, r As Long
Dim hDCSrc As Long, hPal As Long, hPalPrev As Long, RasterCapsScrn As Long
Dim HasPaletteScrn As Long, PaletteSizeScrn As Long, LogPal As LOGPALETTE

'Depending on the value of Client, get the proper device context.
If Client Then
   hDCSrc = GetDC(hWndSrc)          'Get device context for client area.
Else
   hDCSrc = GetWindowDC(hWndSrc)    'Get device context for entire window.
End If

'Create a memory device context for the copy process.
hDCMemory = CreateCompatibleDC(hDCSrc)
'Create a bitmap and place it in the memory DC.
hBmp = CreateCompatibleBitmap(hDCSrc, WidthSrc, HeightSrc)
hBmpPrev = SelectObject(hDCMemory, hBmp)

'Get screen properties.
RasterCapsScrn = GetDeviceCaps(hDCSrc, RASTERCAPS)      'Raster capabilities.
HasPaletteScrn = RasterCapsScrn And RC_PALETTE          'Palette support.
PaletteSizeScrn = GetDeviceCaps(hDCSrc, SIZEPALETTE)    'Size of palette.

'If the screen has a palette make a copy and realize it.
If HasPaletteScrn And (PaletteSizeScrn = 256) Then
   'Create a copy of the system palette.
   LogPal.palVersion = &H300
   LogPal.palNumEntries = 256
   r = GetSystemPaletteEntries(hDCSrc, 0, 256, LogPal.palPalEntry(0))
   hPal = CreatePalette(LogPal)
   ' Select the new palette into the memory DC and realize it.
   hPalPrev = SelectPalette(hDCMemory, hPal, 0)
   r = RealizePalette(hDCMemory)
End If

'Copy the on-screen image into the memory DC.
r = BitBlt(hDCMemory, 0, 0, WidthSrc, HeightSrc, hDCSrc, LeftSrc, TopSrc, vbSrcCopy)

'Remove the new copy of the  on-screen image.
hBmp = SelectObject(hDCMemory, hBmpPrev)

'If the screen has a palette get back the palette that was selected in previously.
If HasPaletteScrn And (PaletteSizeScrn = 256) Then
   hPal = SelectPalette(hDCMemory, hPalPrev, 0)
End If

'Release the device context resources back to the system.
r = DeleteDC(hDCMemory)
r = ReleaseDC(hWndSrc, hDCSrc)

'Call CreateBitmapPicture to create a picture object from the
'bitmap and palette handles. Then return the resulting picture object.
Set CaptureWindow = CreateBitmapPicture(hBmp, hPal)

End Function

Public Function CaptureScreen() As Picture
'Captures the entire screen.
Dim hWndScreen As Long

' Get a handle to the desktop window.
hWndScreen = GetDesktopWindow()

' Call CaptureWindow to capture the entire desktop give the handle
' and return the resulting Picture object.

Set CaptureScreen = CaptureWindow(hWndScreen, False, 0, 0, _
                    Screen.Width \ Screen.TwipsPerPixelX, _
                    Screen.Height \ Screen.TwipsPerPixelY)

End Function

Public Function CaptureForm(ByVal WindowHandle As Long) As Picture
'Captures an entire form including title bar and border.
Dim r As Long, RectActive As RECT

'Get the dimensions of the window.
r = GetWindowRect(WindowHandle, RectActive)

'Call CaptureWindow to capture the active window given its
'handle and return the Resulting Picture object.
Set CaptureForm = CaptureWindow(WindowHandle, False, 0, 0, _
                  RectActive.Right - RectActive.Left, _
                  RectActive.Bottom - RectActive.Top)

End Function

Public Function CaptureActiveWindow() As Picture
'Captures the currently active window on the screen.
Dim hWndActive As Long, r As Long, RectActive As RECT

'Get a handle to the active/foreground window.
hWndActive = GetForegroundWindow()

'Get the dimensions of the window.
r = GetWindowRect(hWndActive, RectActive)

'Call CaptureWindow to capture the active window given its
'handle and return the Resulting Picture object.
Set CaptureActiveWindow = CaptureWindow(hWndActive, False, 0, 0, _
                          RectActive.Right - RectActive.Left, _
                          RectActive.Bottom - RectActive.Top)

End Function

Public Sub PrintPictureToFitPage(ByRef Pic As Picture)
'Prints a Picture object as big as possible.
Const vbHiMetric As Integer = 8
Dim PicRatio As Double, PrnWidth As Double, PrnHeight As Double
Dim PrnRatio As Double, PrnPicWidth As Double, PrnPicHeight As Double

'Determine if picture should be printed in landscape or portrait
'and set the orientation.
If Pic.Height >= Pic.Width Then
   Printer.Orientation = vbPRORPortrait   'Taller than wide.
Else
   Printer.Orientation = vbPRORLandscape  'Wider than tall.
End If

'Calculate device independent Width-to-Height ratio for picture.
PicRatio = Pic.Width / Pic.Height

'Calculate the dimentions of the printable area in HiMetric.
With Printer
    
    'Calculate device independent Width to Height ratio for printer.
    PrnWidth = .ScaleX(.ScaleWidth, .ScaleMode, vbHiMetric)
    PrnHeight = .ScaleY(.ScaleHeight, .ScaleMode, vbHiMetric)
    PrnRatio = PrnWidth / PrnHeight

    'Scale the output to the printable area.
    If PicRatio >= PrnRatio Then
       PrnPicWidth = .ScaleX(PrnWidth, vbHiMetric, .ScaleMode)
       PrnPicHeight = .ScaleY(PrnWidth / PicRatio, vbHiMetric, .ScaleMode)
    Else
       PrnPicHeight = .ScaleY(PrnHeight, vbHiMetric, .ScaleMode)
       PrnPicWidth = .ScaleX(PrnHeight * PicRatio, vbHiMetric, .ScaleMode)
    End If

End With

'Print the picture using the PaintPicture method.
Printer.PaintPicture Pic, 0, 0, PrnPicWidth, PrnPicHeight

End Sub
