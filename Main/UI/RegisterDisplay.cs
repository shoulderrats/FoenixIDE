using FoenixIDE.Processor;
using System;
using System.Windows.Forms;

namespace FoenixIDE
{
    public partial class RegisterDisplay : UserControl
    {
        public RegisterDisplay()
        {
            InitializeComponent();
        }

        private CPU _cpu;
        public CPU CPU
        {
            get { return _cpu; }
            set
            {
                _cpu = value;
                SetRegisters();
            }

        }

        private void SetRegisters()
        {
            if (_cpu != null)
            {
                A.Register = _cpu.A;
                X.Register = _cpu.X;
                Y.Register = _cpu.Y;
                Stack.Register = _cpu.Stack;
                DBR.Register = _cpu.DataBank;
                D.Register = _cpu.DirectPage;
                Flags.Register = _cpu.Flags;
            }
        }

        public void UpdateRegisters()
        {
            PC.Value = _cpu.PC.ToString("X6");
            foreach (object c in groupBox1.Controls)
            {
                if (c is UI.RegisterControl rc)
                {
                    rc.UpdateValue();
                }
                else if (c is UI.AccumulatorControl ac)
                {
                    ac.UpdateValue();
                }
            }
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            UpdateRegisters();
        }
    }
}
