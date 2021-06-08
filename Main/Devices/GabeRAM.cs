﻿using FoenixIDE.MemoryLocations;
using System;

namespace FoenixIDE.Simulator.Devices
{
    public class GabeRAM : MemoryRAM
    {
        private readonly Random rng = new();

        public GabeRAM(int StartAddress, int Length) : base(StartAddress, Length)
        {

        }
        override public byte ReadByte(int Address)
        {
            if (Address == (MemoryMap.GABE_RNG_SEED_LO - MemoryMap.GABE_START))
            {
                return (byte)rng.Next(255);
            }
            return data[Address];
        }
    }
}
