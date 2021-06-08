using FoenixIDE.Processor;
using System.ComponentModel;
using System.Windows.Forms;

namespace FoenixIDE.UI
{
    public partial class RegisterControl : UserControl
    {
        public RegisterControl()
        {
            InitializeComponent();
        }

        string _caption;
        string _value;
        Register _register = null;
        RegisterBankNumber _bank = null;

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
                textBox1.Text = value;
            }
        }

        [Browsable(false)]
        public Register Register
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
            if (Bank != null && Register != null)
            {
                Value = Bank.Value.ToString("X2") + _register.Value.ToString("X4");
            }
            else if (Register != null)
            {
                Value = _register.ToString();
            }
        }

        public RegisterBankNumber Bank
        {
            get { return _bank; }
            set
            {
                _bank = value;
                UpdateValue();
            }
        }
    }
}
