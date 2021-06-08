using System;
using System.Collections.Generic;

namespace FoenixIDE.Processor
{
    public class Breakpoints : SortedList<int, string>
    {
        /// <summary>
        /// Checks whether the address is a breakpoint
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        bool CheckBP(int Address)
        {
            if (Count == 0)
                return false;

            if (ContainsKey(Address))
                return true;

            return false;
        }

        public string Format(string Hex)
        {
            int val = GetIntFromHex(Hex);
            return GetHex(val);
        }

        public static string GetHex(int value)
        {
            String val = value.ToString("X6");
            return "$" + val.Substring(0, 2) + ":" + val[2..];
        }

        public static int GetIntFromHex(string Hex)
        {
            try
            {
                int ret = Convert.ToInt32(Hex.Replace("$", "").Replace(":", ""), 16);
                return ret;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int Add(string HexAddress)
        {
            try
            {
                int Addr = GetIntFromHex(HexAddress);
                Add(Addr, GetHex(Addr));
                return Addr;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Breakpoints.Add(" + HexAddress + ")");
                System.Diagnostics.Debug.WriteLine("Message:  " + ex.Message);
                return -1;
            }
        }

        public void Remove(string HexAddress)
        {
            try
            {
                int Addr = GetIntFromHex(HexAddress);
                if (ContainsKey(Addr))
                    Remove(Addr);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Breakpoints.Remove(" + HexAddress + ")");
                System.Diagnostics.Debug.WriteLine("Message:  " + ex.Message);
            }
        }

        public string[] GetHexArray()
        {
            string[] ret = new string[Count];
            Values.CopyTo(ret, 0);
            return ret;
        }

        public int[] GetIntArray()
        {
            int[] ret = new int[Count];
            Keys.CopyTo(ret, 0);
            return ret;
        }
    }
}
