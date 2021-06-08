﻿using FoenixIDE.Timers;
using System;

namespace FoenixIDE.Simulator.Devices
{
    public class TimerRegister : MemoryLocations.MemoryRAM, IDisposable
    {
        private readonly MultimediaTimer hiresTimer = null;

        public delegate void RaiseInterruptFunction();
        public RaiseInterruptFunction TimerInterruptDelegate;
        const int CPU_FREQ = 14318000;

        public TimerRegister(int StartAddress, int Length) : base(StartAddress, Length)
        {
            hiresTimer = new MultimediaTimer(1000);
            hiresTimer.Elapsed += new MultimediaElapsedEventHandler(Timer_Tick);
        }

        public override void WriteByte(int Address, byte Value)
        {
            // Address 0 is control register
            data[Address] = Value;
            if (Address == 0)
            {
                bool enabled = (Value & 1) != 0;
                if (enabled)
                {
                    hiresTimer.Start();
                }
                else
                {
                    hiresTimer.Stop();
                }
            }
            else if (Address > 4 && Address < 8)
            {
                // Calculate interval in milliseconds
                int longInterval = data[5] + (data[6] << 8) + (data[7] << 16);
                double msInterval = (double)(longInterval) * 1000 / CPU_FREQ;
                uint adjInterval = (uint)msInterval;
                if (adjInterval == 0)
                {
                    hiresTimer.Interval = 1;
                }
                else
                {
                    hiresTimer.Interval = adjInterval;
                }


            }
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            TimerInterruptDelegate?.Invoke();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            hiresTimer?.Dispose();
        }
    }
}
