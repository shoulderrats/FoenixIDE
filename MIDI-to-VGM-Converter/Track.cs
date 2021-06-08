﻿using System;
using System.Text;

namespace MIDI_to_VGM_Converter
{
    public class Track
    {
        public int startOffset = -1;
        public int length = 0;
        public TimeSignature timeSignature;
        public int totalDeltaTime = 0;
    }

    public enum METype
    {
        progchange, noteoff, noteon
    }

    // refer to http://www.shikadi.net/moddingwiki/OP2_Bank_Format
    // and the OPL3 Bank Editor: https://github.com/Wohlstand/OPL3BankEditor/blob/master/README.md
    public class GeneralMidi
    {
        private static readonly byte[] GenMidi = Properties.Resources.GENMIDI;
        public static byte[] GetInstrument(int program)
        {
            byte[] buffer = new byte[36];
            int offset = 8 + 36 * program;
            Array.Copy(GenMidi, offset, buffer, 0, 36);
            return buffer;
        }
    }

    public class MidiEvent
    {
        public int deltaTime;
        public int index; // index in time
        public int wait; // this is the converted sample count at 44100 Hz.
        public METype type = METype.noteon;
        public byte note;
        public byte velocity = 0;
        public byte program = 0;
        // we have 18 single voice (2 ops) channels or 6 double-voice (4 ops) + 6 single-voice (2 ops) channels - so use channels cautiously.
        // I need to write an algorithm to pick the correct channels - don't let MIDI do this
        // For example, channel 10 in MIDI is usually a rythm track (drums)
        public byte midiChannel = 0;

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append("I:").Append(index).Append(" D:").Append(deltaTime).Append(" W:").Append(wait);
            sb.Append(" T:").Append(type).Append(" C:").Append(midiChannel);
            switch (type)
            {
                case METype.progchange:
                    sb.Append(" P:").Append(program);
                    break;
                default:
                    sb.Append(" N:").Append(note).Append(" V:").Append(velocity);
                    break;
            }

            return sb.ToString();
        }

        // We're mapping channels to use the drums
        private readonly byte[] channelToOperatorOffset = { 0, 1, 2, 8, 9, 0xA, 0x10, 0x11, 0x12 };
        public byte[] GetBytes()
        {
            byte[] buffer = null;
            byte oplChnl = MainForm.channelMap[midiChannel];
            if (oplChnl < 18)
            {
                byte baseReg = (oplChnl < 9) ? (byte)0x5e : (byte)0x5f;
                switch (type)
                {
                    case METype.progchange:
                        // read the patch file
                        byte[] gmData = GeneralMidi.GetInstrument(program);
                        MainForm.ChannelKSL[oplChnl] = gmData[8];

                        if ((gmData[0] & 4) != 0)
                        {
                            buffer = new byte[66];
                            // double voice instrument
                            // operator 1
                            byte addr1 = 0x20;
                            byte addr2 = 0x28;
                            Array.Copy(new byte[3] { baseReg, (byte)(addr1 + channelToOperatorOffset[oplChnl % 9]), gmData[4] }, 0, buffer, 0, 3);
                            Array.Copy(new byte[3] { baseReg, (byte)(addr1 + 0x40 + channelToOperatorOffset[oplChnl % 9]), gmData[5] }, 0, buffer, 3, 3);
                            Array.Copy(new byte[3] { baseReg, (byte)(addr1 + 0x60 + channelToOperatorOffset[oplChnl % 9]), gmData[6] }, 0, buffer, 6, 3);
                            Array.Copy(new byte[3] { baseReg, (byte)(addr1 + 0xC0 + channelToOperatorOffset[oplChnl % 9]), gmData[7] }, 0, buffer, 9, 3);
                            Array.Copy(new byte[3] { baseReg, (byte)(addr1 + 0x20 + channelToOperatorOffset[oplChnl % 9]), (byte)(gmData[8] + gmData[9]) }, 0, buffer, 12, 3);
                            Array.Copy(new byte[3] { baseReg, (byte)(addr1 + 0xA0 + oplChnl % 9), (byte)(gmData[10] | 0x30) }, 0, buffer, 15, 3);
                            // operator 2
                            Array.Copy(new byte[3] { baseReg, (byte)(addr1 + 3 + channelToOperatorOffset[oplChnl % 9]), gmData[11] }, 0, buffer, 18, 3);
                            Array.Copy(new byte[3] { baseReg, (byte)(addr1 + 0x40 + 3 + channelToOperatorOffset[oplChnl % 9]), gmData[12] }, 0, buffer, 21, 3);
                            Array.Copy(new byte[3] { baseReg, (byte)(addr1 + 0x60 + 3 + channelToOperatorOffset[oplChnl % 9]), gmData[13] }, 0, buffer, 24, 3);
                            Array.Copy(new byte[3] { baseReg, (byte)(addr1 + 0xC0 + 3 + channelToOperatorOffset[oplChnl % 9]), gmData[14] }, 0, buffer, 27, 3);
                            Array.Copy(new byte[3] { baseReg, (byte)(addr1 + 0x20 + 3 + channelToOperatorOffset[oplChnl % 9]), (byte)(gmData[15] | gmData[16]) }, 0, buffer, 30, 3);
                            // operator 3
                            Array.Copy(new byte[3] { baseReg, (byte)(addr2 + channelToOperatorOffset[oplChnl % 9]), gmData[20] }, 0, buffer, 33, 3);
                            Array.Copy(new byte[3] { baseReg, (byte)(addr2 + 0x40 + channelToOperatorOffset[oplChnl % 9]), gmData[21] }, 0, buffer, 36, 3);
                            Array.Copy(new byte[3] { baseReg, (byte)(addr2 + 0x60 + channelToOperatorOffset[oplChnl % 9]), gmData[22] }, 0, buffer, 39, 3);
                            Array.Copy(new byte[3] { baseReg, (byte)(addr2 + 0xC0 + channelToOperatorOffset[oplChnl % 9]), gmData[23] }, 0, buffer, 42, 3);
                            Array.Copy(new byte[3] { baseReg, (byte)(addr2 + 0x20 + channelToOperatorOffset[oplChnl % 9]), (byte)(gmData[24] + gmData[25]) }, 0, buffer, 45, 3);
                            Array.Copy(new byte[3] { baseReg, (byte)(addr2 + 0xA0 + oplChnl % 9), (byte)(gmData[26] | 0x30) }, 0, buffer, 48, 3);
                            // operator 4
                            Array.Copy(new byte[3] { baseReg, (byte)(addr2 + 3 + channelToOperatorOffset[oplChnl % 9]), gmData[27] }, 0, buffer, 51, 3);
                            Array.Copy(new byte[3] { baseReg, (byte)(addr2 + 0x40 + 3 + channelToOperatorOffset[oplChnl % 9]), gmData[28] }, 0, buffer, 54, 3);
                            Array.Copy(new byte[3] { baseReg, (byte)(addr2 + 0x60 + 3 + channelToOperatorOffset[oplChnl % 9]), gmData[29] }, 0, buffer, 57, 3);
                            Array.Copy(new byte[3] { baseReg, (byte)(addr2 + 0xC0 + 3 + channelToOperatorOffset[oplChnl % 9]), gmData[30] }, 0, buffer, 60, 3);
                            Array.Copy(new byte[3] { baseReg, (byte)(addr2 + 0x20 + 3 + channelToOperatorOffset[oplChnl % 9]), (byte)(gmData[31] | gmData[32]) }, 0, buffer, 63, 3);
                        }
                        else
                        {
                            buffer = new byte[33];
                            // Single voice channel
                            // operator 1
                            Array.Copy(new byte[3] { baseReg, (byte)(0x20 + channelToOperatorOffset[oplChnl % 9]), gmData[4] }, 0, buffer, 0, 3);  // tremolo/vibrato/sustain/KSR/Freq Mult
                            Array.Copy(new byte[3] { baseReg, (byte)(0x60 + channelToOperatorOffset[oplChnl % 9]), gmData[5] }, 0, buffer, 3, 3);  // attack/decay
                            Array.Copy(new byte[3] { baseReg, (byte)(0x80 + channelToOperatorOffset[oplChnl % 9]), gmData[6] }, 0, buffer, 6, 3);  // sustain/release
                            Array.Copy(new byte[3] { baseReg, (byte)(0xE0 + channelToOperatorOffset[oplChnl % 9]), gmData[7] }, 0, buffer, 9, 3);  // Waveform select
                            Array.Copy(new byte[3] { baseReg, (byte)(0x40 + channelToOperatorOffset[oplChnl % 9]), (byte)(gmData[8] + gmData[9]) }, 0, buffer, 12, 3);  // KSL/Output Level
                            Array.Copy(new byte[3] { baseReg, (byte)(0xC0 + oplChnl % 9), (byte)(gmData[10] | 0xF0) }, 0, buffer, 15, 3);  // Speaker/Feedback/Syn Type
                                                                                                                                           // operator 2
                            Array.Copy(new byte[3] { baseReg, (byte)(0x23 + channelToOperatorOffset[oplChnl % 9]), gmData[11] }, 0, buffer, 18, 3);  // tremolo/vibrato/sustain/KSR/Freq Mult
                            Array.Copy(new byte[3] { baseReg, (byte)(0x63 + channelToOperatorOffset[oplChnl % 9]), gmData[12] }, 0, buffer, 21, 3);  // attack/decay
                            Array.Copy(new byte[3] { baseReg, (byte)(0x83 + channelToOperatorOffset[oplChnl % 9]), gmData[13] }, 0, buffer, 24, 3);  // sustain/release
                            Array.Copy(new byte[3] { baseReg, (byte)(0xE3 + channelToOperatorOffset[oplChnl % 9]), gmData[14] }, 0, buffer, 27, 3);  // Waveform select
                            Array.Copy(new byte[3] { baseReg, (byte)(0x43 + channelToOperatorOffset[oplChnl % 9]), (byte)(gmData[15] + gmData[16]) }, 0, buffer, 30, 3);  // KSL/Output Level
                        }
                        break;
                    case METype.noteon:

                        if (midiChannel == 9 && MainForm.PercussionSet != 0)
                        {
                            buffer = new byte[6];
                            bool BD = (note == 35) | (note == 36);
                            bool SN = (note == 38) | (note == 40);
                            bool TT = (note == 41) | (note == 45);
                            bool CY = (note == 49) | (note == 55) | (note == 57);
                            bool HH = (note == 46) | (note == 42);

                            buffer[0] = 0x5e;
                            buffer[1] = 0xBD;
                            buffer[3] = 0x5e;
                            if (BD)
                            {
                                MainForm.PercussionSet = velocity == 0 ? (byte)(MainForm.PercussionSet & ~0x10) : (byte)(MainForm.PercussionSet | 0x10);
                                buffer[4] = 0x40 + 0x10;

                            }
                            if (SN)
                            {
                                MainForm.PercussionSet = velocity == 0 ? (byte)(MainForm.PercussionSet & ~0x8) : (byte)(MainForm.PercussionSet | 0x8);
                                buffer[4] = 0x40 + 0x14;
                            }
                            if (TT)
                            {
                                MainForm.PercussionSet = velocity == 0 ? (byte)(MainForm.PercussionSet & ~0x4) : (byte)(MainForm.PercussionSet | 0x4);
                                buffer[4] = 0x40 + 0x12;
                            }
                            if (CY)
                            {
                                MainForm.PercussionSet = velocity == 0 ? (byte)(MainForm.PercussionSet & ~0x2) : (byte)(MainForm.PercussionSet | 0x2);
                                buffer[4] = 0x40 + 0x15;
                            }
                            if (HH)
                            {
                                MainForm.PercussionSet = velocity == 0 ? (byte)(MainForm.PercussionSet & ~0x1) : (byte)(MainForm.PercussionSet | 0x1);
                                buffer[4] = 0x40 + 0x11;
                            }
                            buffer[2] = MainForm.PercussionSet;
                            buffer[5] = (byte)(0x3F - (velocity >> 1));  // attenuation
                        }
                        else
                        {
                            buffer = new byte[9];
                            byte[] onFreq = GetFreq(note);
                            buffer[0] = baseReg;
                            buffer[1] = (byte)(0xA0 + oplChnl % 9);
                            buffer[2] = onFreq[0];
                            buffer[3] = baseReg;
                            buffer[4] = (byte)(0xB0 + oplChnl % 9);
                            buffer[5] = (byte)(onFreq[1] | (velocity != 0 ? 0x20 : 0));  // set KEY on

                            buffer[6] = baseReg;
                            buffer[7] = (byte)(0x40 + channelToOperatorOffset[oplChnl % 9]);
                            buffer[8] = (byte)(MainForm.ChannelKSL[oplChnl] | (0x3F - (velocity >> 1)));  // attenuation
                        }
                        break;
                    case METype.noteoff:
                        buffer = new byte[3];
                        //byte[] offFreq = GetFreq(note);
                        if (midiChannel == 9 && MainForm.PercussionSet != 0)
                        {
                            bool BD = (note == 35) | (note == 36);
                            bool SN = (note == 38) | (note == 40);
                            bool TT = (note == 41) | (note == 45);
                            bool CY = (note == 49) | (note == 57);
                            bool HH = (note == 46) | (note == 42);
                            buffer[0] = 0x5e;
                            buffer[1] = 0xBD;
                            if (BD)
                            {
                                MainForm.PercussionSet = (byte)(MainForm.PercussionSet & ~0x10);
                                buffer[2] = 0x40 + 0x10;

                            }
                            if (SN)
                            {
                                MainForm.PercussionSet = (byte)(MainForm.PercussionSet & ~0x8);
                                buffer[2] = 0x40 + 0x14;
                            }
                            if (TT)
                            {
                                MainForm.PercussionSet = (byte)(MainForm.PercussionSet & ~0x4);
                                buffer[2] = 0x40 + 0x12;
                            }
                            if (CY)
                            {
                                MainForm.PercussionSet = (byte)(MainForm.PercussionSet & ~0x2);
                                buffer[2] = 0x40 + 0x15;
                            }
                            if (HH)
                            {
                                MainForm.PercussionSet = (byte)(MainForm.PercussionSet & ~0x1);
                                buffer[2] = 0x40 + 0x11;
                            }
                        }
                        else
                        {
                            buffer[0] = baseReg;
                            buffer[1] = (byte)(0xB0 + oplChnl % 9);
                            buffer[2] = 0;
                        }
                        break;
                }
            }
            else
            {
                // program the drum channels

            }
            return buffer;
        }

        // note 60 is middle C on an 88-note keyboard
        private readonly ushort[] noteFNumbers = { 0x016B, 0x0181, 0x0198, 0x01B0, 0x01CA, 0x01E5, 0x0202, 0x0220, 0x0241, 0x0263, 0x0287, 0x02AE };  // c# to c
        private byte[] GetFreq(byte note)
        {
            byte[] buffer = new byte[2];
            byte octave = (byte)((((note - 1) / 12 - 1) << 2) & 0x1C);
            int offset = (note - 1) % 12;
            ushort val = noteFNumbers[offset];
            buffer[0] = (byte)(val & 0xFF);
            buffer[1] = (byte)((val >> 8) | octave);
            return buffer;
        }
    }
}
