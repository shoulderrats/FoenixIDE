﻿using System;

namespace FoenixIDE.Processor
{
    public class Register
    {
        protected int _value;
        private int byteLength = 2;
        public const int DefaultStackValue = 0xd6ff;
        public int TopOfStack = DefaultStackValue;

        /// <summary>
        /// Forces the upper 8 bits to 0 when the register changes to 8 bit mode, or when writing or reading 
        /// the value in 8 bit mode. If this is false, the value is hidden, but preserved. If this is true, 
        /// the top 8 bits are destroyed when the width is set to 8 bits. 
        /// </summary>
        public bool DiscardUpper = false;
        //readonly string[] formatWidth = new string[] { "X", "X1", "X2", "X3", "X4" };

        //public enum int
        //{
        //    Bits8 = 1,
        //    Bits16 = 2,
        //    Bits24 = 3,
        //}

        public virtual int Value
        {
            get
            {
                return byteLength == 1 ? _value & 0xff : _value;
            }

            set
            {
                if (byteLength == 1)
                {
                    if (DiscardUpper)
                        _value = value & 0xff;
                    else
                        _value = (value & 0xff) | (_value & 0xff00);
                }
                else
                    _value = value & 0xffff;
            }
        }

        public void Dec()
        {
            _value -= 1;
        }

        public void Inc()
        {
            _value += 1;
        }
        public virtual int Low
        {
            get { return _value & 0xff; }
            //set { this.Value = (int)((this.Value & 0xff00) | (value & 0xff)); }
        }

        /*
        public virtual int High
        {
            get { return (int)((this._value & 0xff00) >> 8); }
            set { this.Value = (int)((this.Value & 0xff) | ((value & 0xff) << 8)); }
        }*/

        public virtual void Swap()
        {
            int v = _value;
            int low = (v & 0xFF) << 8;
            int high = (v & 0xFF00) >> 8;
            _value = high + low;
        }

        /// <summary>
        /// Register width in bytes. 1=8 bits, 2=16 bits
        /// </summary>
        public virtual int Width
        {
            get
            {
                return byteLength;
            }

            set
            {
                byteLength = value;
            }
        }

        public virtual int MinUnsigned
        {
            get
            {
                return 0;
            }
        }

        public virtual int MaxUnsigned
        {
            get
            {
                return byteLength == 1 ? 0xff : 0xffff;
            }
        }

        public virtual int MinSigned
        {
            get
            {
                return byteLength == 1 ? -128 : -32768;
            }
        }

        public virtual int MaxSigned
        {
            get
            {
                return byteLength == 1 ? 127 : 32767;
            }
        }

        public virtual void SetFromVector(int v)
        {
            throw new NotImplementedException();
        }

        public virtual bool GetZeroFlag()
        {
            return Value == 0;
        }

        public override string ToString()
        {
            return byteLength switch
            {
                2 => "$" + Value.ToString("X4"),
                1 => "$" + Value.ToString("X2"),
                _ => Value.ToString(),
            };
        }

        /// <summary>
        /// Build a 24-bit address using this as a bank register
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public virtual int GetLongAddress(int Address)
        {
            if (byteLength == 2)
                return (Value << 8) | Address;
            else if (byteLength == 1)
                return (Value << 16) | Address;
            else
                return Value;
        }

        /// <summary>
        /// Build a 24-bit address using this as a bank register
        /// </summary>
        /// <param name="Address">Register to use as address</param>
        /// <returns></returns>
        public virtual int GetLongAddress(Register Address)
        {
            if (byteLength == 2)
                return (Value << 8) | Address.Value;
            else if (byteLength == 1)
                return (Value << 16) | Address.Value;
            else
                return Value;
        }

        public virtual void Reset()
        {
            TopOfStack = DefaultStackValue;
            Value = DefaultStackValue;
            byteLength = 2;
        }
    }

    /// <summary>
    /// A register that is always 16 bits, such as the Direct Page register
    /// </summary>
    public class Register16 : Register
    {
        public override int Width
        {
            get
            {
                return 2;
            }

            set
            {
                base.Width = 2;
            }
        }

        /// <summary>
        /// Get a direct page address. Offsets the register's value by 8 bits, then adds 
        /// the supplied address.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public int GetLongAdddress(int address)
        {
            return (Value << 8) | address;
        }

        /// <summary>
        /// Get a direct page address. Offsets the register's value by 8 bits, then adds 
        /// the supplied address.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public int GetLongAdddress(Register index)
        {
            return (Value << 8) | index.Value;
        }
    }

    /// <summary>
    /// Defines a register that is always 16 bits, such as the program counter or the Direct Page register.
    /// </summary>
    /// 
    /// <summary>
    /// 
    /// Defines an 8 bit register.
    /// </summary>
    public class Register8 : Register
    {
        public Register8()
        {
            base.Width = 1;
        }

        public override int Width
        {
            get
            {
                return 1;
            }

            set
            {
                base.Width = 1;
            }
        }
    }

    public class RegisterBankNumber : Register8
    {
        private int _LV = 0;
        /// <summary>
        /// Adds the 16-bit address in the register to this bank to get a 24-bit address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public virtual int GetLongAddress(Register16 Address)
        {
            return _LV | Address.Value;
        }

        /// <summary>
        /// Adds this bank register to a 16-bit address to form a 24-bit address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public override int GetLongAddress(int Address)
        {
            return _LV | Address;
        }

        public override int Value
        {
            get => base.Value;
            set
            {
                _value = value;
                _LV = value << 16;
            }
        }
    }

    /// <summary>
    /// A register that is always 16 bits, such as the Direct Page register
    /// </summary>
    public class RegisterDirectPage : Register
    {
        public override int Width
        {
            get
            {
                return 2;
            }

            set
            {
                base.Width = 2;
            }
        }

        /// <summary>
        /// Get a direct page address. Offsets the register's value by 8 bits, then adds 
        /// the supplied address.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public override int GetLongAddress(int address)
        {
            return Value + address;
        }

    }

}
