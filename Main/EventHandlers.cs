﻿using System.Windows.Forms;

namespace FoenixIDE
{
    public class TerminalKeyEventArgs : KeyPressEventArgs
    {
        public Keys Modifiers;
        public TerminalKeyEventArgs(char KeyChar, Keys Modifiers = Keys.None) : base(KeyChar)
        {
            this.Modifiers = Modifiers;
        }
    }
    //public delegate void KeyPressEventHandler(Gpu frameBuffer, TerminalKeyEventArgs e);
}