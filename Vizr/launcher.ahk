~Control::
if (A_PriorHotkey <> "~Control" or A_TimeSincePriorHotkey > 400)
{
    ; Too much time between presses, so this isn't a double-press.
    KeyWait, Control
    return
}
Run, Vizr.exe
return