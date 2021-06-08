using FoenixIDE.Processor;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FoenixIDE.UI
{
    public partial class AccumulatorControl : UserControl
    {
        public AccumulatorControl()
        {
            InitializeComponent();
        }

        string _caption;
        string _value;
        RegisterAccumulator _register = null;

        public string Caption
        {
            get { return _caption; }
            set
            {
                _caption = value;
                label1.Text = value;
            }
        }

        public string Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
                if (_register == null || _register.Width == 1)
                {
                    regB.ForeColor = SystemColors.Info;
                    regB.BackColor = SystemColors.InfoText;
                }
                else
                {
                    regB.ForeColor = SystemColors.WindowText;
                    regB.BackColor = SystemColors.Window;
                }
                regB.Text = value.Substring(0, 2);
                regA.Text = value.Substring(2, 2);
            }
        }

        [Browsable(false)]
        public RegisterAccumulator Register
        {
            get
            {
                return _register;
            }

            set
            {
                _register = value;
                if (value != null)
                {
                    UpdateValue();
                }

            }
        }

        public void UpdateValue()
        {
            if (Register != null)
            {
                Value = _register.Value16.ToString("X4");
            }
        }
    }
}
