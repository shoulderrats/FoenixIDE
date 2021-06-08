using FoenixIDE.MemoryLocations;
using FoenixIDE.Simulator.FileFormat;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using static FoenixIDE.Simulator.FileFormat.ResourceChecker;

namespace FoenixIDE.UI
{
    public partial class AssetLoader : Form
    {
        public MemoryManager MemMgrRef = null;
        public ResourceChecker ResChecker;

        public AssetLoader()
        {
            InitializeComponent();
        }

        private void AssetLoader_Load(object sender, EventArgs e)
        {
            // Add items to the combo box
            // Tiles Registers: $AF:0100 to $AF:013F
            FileTypesCombo.Items.Add("Bitmap Layer 0");
            FileTypesCombo.Items.Add("Bitmap Layer 1");

            for (int i = 0; i < 4; i++)
            {
                FileTypesCombo.Items.Add("Tilemap " + i);
            }
            for (int i = 0; i < 8; i++)
            {
                FileTypesCombo.Items.Add("Tileset " + i);
            }
            for (int i = 0; i < 64; i++)
            {
                FileTypesCombo.Items.Add("Sprite " + i);
            }
            FileTypesCombo.SelectedItem = 0; // Bitmap layer 0
            for (int i = 0; i < 4; i++)
            {
                FileTypesCombo.Items.Add("LUT " + i);
            }

            for (int i = 0; i < 4; i++)
            {
                LUTCombo.Items.Add("LUT " + i);
            }
            FileTypesCombo.SelectedIndex = 0;
            LUTCombo.SelectedIndex = 0;
        }

        /**
         * The LUT list box is only selected when loading bitmaps, sprites and tiles
         */
        private void FileTypesCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool LUTSelected = FileTypesCombo.SelectedItem.ToString().StartsWith("LUT");
            LUTCombo.Enabled = !LUTSelected;
            LoadAddressTextBox.Enabled = !LUTSelected;
            if (FileTypesCombo.SelectedItem.ToString().StartsWith("LUT"))
            {
                int lut = Convert.ToInt32(FileTypesCombo.SelectedItem.ToString()[4..]);
                LoadAddressTextBox.Enabled = false;
                LoadAddressTextBox.Text = (MemoryMap.GRP_LUT_BASE_ADDR + lut * 1024).ToString("X6");
            }
        }

        private static String FormatAddress(int address)
        {
            String size = (address).ToString("X6");
            return "$" + size.Substring(0, 2) + ":" + size[2..];
        }
        /*
         * Let the user select a file from the file system and display it in a text box.
         */
        private void BrowseFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new()
            {
                Title = "Load Bitmap",
                DefaultExt = ".bin",
                Filter = "Asset Files (*.bmp *.png *.bin *.data *.pal *.aseprite)|*.bmp;*.png;*.bin;*.data;*.pal;*.aseprite|Binary Files|*.bin|Palette Files|*.pal|Bitmap Files|*.bmp;*.png|Data Files|*.data|Any File|*.*"
            };

            // Load content of file in a TextBlock
            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                FileNameTextBox.Text = openFileDlg.FileName;
                FileInfo info = new(FileNameTextBox.Text);
                ExtLabel.Text = info.Extension;
                FileSizeResultLabel.Text = FormatAddress((int)info.Length);
                StoreButton.Enabled = true;
            }
        }

        private void StoreButton_Click(object sender, EventArgs e)
        {
            StoreButton.Enabled = false;

            // Store the address in the pointer address - little endian - 24 bits
            int destAddress = Convert.ToInt32(LoadAddressTextBox.Text.Replace(":", ""), 16);
            //FileInfo info = new(FileNameTextBox.Text);
            byte MCRHigh = (byte)(MemMgrRef.VICKY.ReadByte(1) & 3);
            int screenResX = 640;
            int screenResY = 480;
            switch (MCRHigh)
            {
                case 1:
                    screenResX = 800;
                    screenResY = 600;
                    break;
                case 2:
                    screenResX = 320;
                    screenResY = 240;
                    break;
                case 3:
                    screenResX = 400;
                    screenResY = 300;
                    break;
            }

            int conversionStride = 0;
            int maxHeight = screenResY;

            ResourceType operationType;
            if (FileTypesCombo.SelectedIndex < 2)
            {
                operationType = ResourceType.bitmap;
                conversionStride = screenResX;
            }
            else if (FileTypesCombo.SelectedIndex < 6)
            {
                operationType = ResourceType.tilemap;
                ExtLabel.Text = ".data";
            }
            else if (FileTypesCombo.SelectedIndex < 14)
            {
                operationType = ResourceType.tileset;
                conversionStride = 256;
                maxHeight = 256;
            }
            else if (FileTypesCombo.SelectedIndex < 78)
            {
                operationType = ResourceType.sprite;
                conversionStride = 32;
                maxHeight = 32;
            }
            else
            {
                operationType = ResourceType.lut;
                ExtLabel.Text = ".data";
            }

            Resource res = new()
            {
                StartAddress = destAddress,
                SourceFile = FileNameTextBox.Text,
                Name = Path.GetFileNameWithoutExtension(FileNameTextBox.Text),
                FileType = operationType,
            };


            switch (ExtLabel.Text.ToLower())
            {
                case ".png":
                    Bitmap png = new(FileNameTextBox.Text, false);
                    ConvertBitmapToRaw(png, res, (byte)LUTCombo.SelectedIndex, conversionStride, maxHeight);
                    break;
                case ".bmp":
                    Bitmap bmp = new(FileNameTextBox.Text, false);
                    ConvertBitmapToRaw(bmp, res, (byte)LUTCombo.SelectedIndex, conversionStride, maxHeight);
                    break;
                default:
                    // Read the file as raw
                    byte[] data = File.ReadAllBytes(FileNameTextBox.Text);
                    // Check if there's a resource conflict
                    res.Length = data.Length;
                    if (ResChecker.Add(res))
                    {
                        MemMgrRef.CopyBuffer(data, 0, destAddress, data.Length);
                    }
                    else
                    {
                        res.Length = -1;
                    }
                    break;
            }

            if (res.Length > 0)
            {
                // write address offset by bank $b0
                int imageAddress = destAddress - 0xB0_0000;
                byte lutValue = (byte)LUTCombo.SelectedIndex;

                int regAddress;
                // Determine which addresses to store the bitmap into.
                if (FileTypesCombo.SelectedIndex < 2)
                {
                    // Bitmaps
                    regAddress = MemoryMap.BITMAP_CONTROL_REGISTER_ADDR + FileTypesCombo.SelectedIndex * 8;
                    // enable the bitmap - TODO add the LUT
                    MemMgrRef.WriteByte(regAddress, (byte)(1 + lutValue * 2));

                }
                else if (FileTypesCombo.SelectedIndex < 6)
                {
                    // Tilemaps 4
                    int tilemapIndex = FileTypesCombo.SelectedIndex - 1;
                    regAddress = MemoryMap.TILE_CONTROL_REGISTER_ADDR + tilemapIndex * 12;

                    // enable the tilemap
                    MemMgrRef.WriteByte(regAddress, (byte)(1 + (lutValue << 1)));

                    // TODO: Need to write the size of the tilemap
                }
                else if (FileTypesCombo.SelectedIndex < 14)
                {
                    // Tilesets 8
                    int tilesetIndex = FileTypesCombo.SelectedIndex - 5;
                    regAddress = MemoryMap.TILESET_BASE_ADDR + tilesetIndex * 4;

                    MemMgrRef.WriteByte(regAddress + 3, lutValue);  // TODO: Add the stride 256 bit 3.
                }
                else
                {
                    // Sprites 64
                    int spriteIndex = FileTypesCombo.SelectedIndex - 14;
                    regAddress = MemoryMap.SPRITE_CONTROL_REGISTER_ADDR + spriteIndex * 8;

                    // enable the tilemap
                    MemMgrRef.WriteByte(regAddress, (byte)(1 + (lutValue << 1)));  // TODO: Add sprite depth
                                                                                   // write address offset by bank $b0
                                                                                   // Set the sprite at (32,32)
                    MemMgrRef.WriteWord(regAddress + 4, 32);
                    MemMgrRef.WriteWord(regAddress + 6, 32);
                }
                // write address offset by bank $b0
                MemMgrRef.WriteByte(regAddress + 1, LowByte(imageAddress));
                MemMgrRef.WriteByte(regAddress + 2, MidByte(imageAddress));
                MemMgrRef.WriteByte(regAddress + 3, HighByte(imageAddress));

                StoreButton.Enabled = true;
            }
            if (res.Length != -1)
            {
                DialogResult = DialogResult.OK;
                if (FileTypesCombo.SelectedIndex > 1 && FileTypesCombo.SelectedIndex < 6)
                {
                    //int layer = FileTypesCombo.SelectedIndex - 2;
                    //OnTileLoaded?.Invoke(layer);
                }
                Close();
            }
            else
            {
                // Keep the Asset Loader open
            }

        }

        private unsafe void ConvertBitmapToRaw(Bitmap bitmap, Resource resource, byte lutIndex, int stride, int maxHeight)
        {
            if (ResChecker.Add(resource))
            {


                // Load LUT from memory - ignore indexes 0 and 1
                int lutBaseAddress = MemoryMap.GRP_LUT_BASE_ADDR + lutIndex * 0x400 - MemoryMap.VICKY_BASE_ADDR;

                // Limit how much data is imported based on the type of image
                int importedLines = maxHeight < bitmap.Height ? maxHeight : bitmap.Height;
                int importedCols = stride < bitmap.Width ? stride : bitmap.Width;

                byte[] data = new byte[stride * importedLines]; // the bitmap is based on resolution of the machine
                resource.Length = stride * bitmap.Height;  // one byte per pixel - palette is separate

                Rectangle rect = new(0, 0, bitmap.Width, bitmap.Height);
                BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
                int bytesPerPixel = bitmapData.Stride / bitmap.Width;
                byte* bitmapPointer = (byte*)bitmapData.Scan0.ToPointer();
                bool tooManyColours = false;
                bool done = false;
                byte mask = 0xFF;
                List<int> lut = null;
                while (!done)
                {
                    done = true;
                    // Reset the Lookup Table
                    lut = new List<int>(256)
                    {
                        // Always add black and white
                        0,
                        0xFFFFFF
                    };

                    for (int i = 2; i < 256; i++)
                    {
                        int value = MemMgrRef.VICKY.ReadLong(lutBaseAddress + 4 * i);
                        if (value != 0)
                        {
                            lut.Add(value);
                        }
                        else
                        {
                            break;
                        }
                    }

                    for (int line = 0; line < importedLines; line++)
                    {
                        for (int col = 0; col < importedCols; col++)
                        {
                            byte b = 0;
                            byte r = 0;
                            byte g = 0;

                            switch (bytesPerPixel)
                            {
                                case 1:
                                    byte palIndex = bitmapPointer[line * bitmapData.Stride + col * bytesPerPixel];
                                    Color palValue = bitmap.Palette.Entries[palIndex];
                                    b = palValue.B;
                                    g = palValue.G;
                                    r = palValue.R;
                                    break;
                                case 2:
                                    ushort wordValue = (ushort)(bitmapPointer[line * bitmapData.Stride + col * bytesPerPixel] + bitmapPointer[line * bitmapData.Stride + col * bytesPerPixel + 1] * 256);
                                    b = (byte)(wordValue & 0x1F);  //  5bits
                                    g = (byte)((wordValue >> 5) & 0x3F); // 6 bits
                                    r = (byte)(wordValue >> 11); // 5 bits
                                    break;
                                case 3:
                                    b = (byte)(bitmapPointer[line * bitmapData.Stride + col * bytesPerPixel] & mask);
                                    g = (byte)(bitmapPointer[line * bitmapData.Stride + col * bytesPerPixel + 1] & mask);
                                    r = (byte)(bitmapPointer[line * bitmapData.Stride + col * bytesPerPixel + 2] & mask);
                                    break;
                                case 4:
                                    b = (byte)(bitmapPointer[line * bitmapData.Stride + col * bytesPerPixel] & mask);
                                    g = (byte)(bitmapPointer[line * bitmapData.Stride + col * bytesPerPixel + 1] & mask);
                                    r = (byte)(bitmapPointer[line * bitmapData.Stride + col * bytesPerPixel + 2] & mask);
                                    //alpha is ignored

                                    break;
                            }
                            int rgb = b + g * 256 + r * 256 * 256;
                            int index = lut.IndexOf(rgb);
                            if (index == -1)
                            {
                                if (lut.Count < 256)
                                {
                                    lut.Add(rgb);
                                    index = (byte)lut.IndexOf(rgb);
                                }
                                else
                                {
                                    tooManyColours = true;
                                    break;
                                }
                            }
                            if (index != -1)
                            {
                                data[line * stride + col] = (byte)index;
                            }
                        }
                        if (tooManyColours)
                        {
                            // TODO should use a colour histogram to count how many times a colour is used and then decimate here, based on low usage.
                            done = false;
                            tooManyColours = false;
                            mask <<= 1;
                            break;
                        }
                    }
                }

                int videoAddress = resource.StartAddress - 0xB0_0000;

                MemMgrRef.VIDEO.CopyBuffer(data, 0, videoAddress, data.Length);

                if (lut != null)
                {
                    for (int i = 0; i < lut.Count; i++)
                    {
                        int rbg = lut[i];
                        MemMgrRef.VICKY.WriteByte(lutBaseAddress + 4 * i, LowByte(rbg));
                        MemMgrRef.VICKY.WriteByte(lutBaseAddress + 4 * i + 1, MidByte(rbg));
                        MemMgrRef.VICKY.WriteByte(lutBaseAddress + 4 * i + 2, HighByte(rbg));
                    }
                }

                // Check if a LUT matching our index is present in the Resources, if so don't do anything.
                Resource resLut = ResChecker.Find(ResourceType.lut, lutBaseAddress + MemoryMap.VICKY_BASE_ADDR);
                if (resLut == null)
                {
                    Resource lutPlaceholder = new()
                    {
                        Length = 0x400,
                        FileType = ResourceType.lut,
                        Name = "Generated LUT",
                        StartAddress = lutBaseAddress + MemoryMap.VICKY_BASE_ADDR
                    };
                    ResChecker.Add(lutPlaceholder);
                }

            }
            else
            {
                resource.Length = -1;
                StoreButton.Enabled = true;
            }
        }

        /*
         * Convert a bitmap with no palette to a bytes with a color lookup table.
         */
        private void TransformBitmap(byte[] data, int startOffset, int pixelDepth, int lutPointer, int videoPointer, int width, int height)
        {
            List<int> lut = new(256)
            {
                // Always add black and white
                0,
                0xFFFFFF
            };
            // Read every pixel into a color table
            int bytes = 1;
            switch (pixelDepth)
            {
                case 16:
                    bytes = 2;
                    break;
                case 24:
                    bytes = 3;
                    break;
            }
            // Now read the bitmap
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int pointer = startOffset + ((height - y - 1) * width + x) * bytes;
                    int rgb = -1;
                    switch (pixelDepth)
                    {
                        case 16:
                            rgb = (data[pointer] & 0x1F) + ((((data[pointer] & 0xE0) >> 5) + (data[pointer + 1] & 0x3) << 3) << 8) + ((data[pointer + 1] & 0x7C) << 14);
                            break;
                        case 24:
                            rgb = data[pointer] + (data[pointer + 1] << 8) + (data[pointer + 2] << 16);
                            break;
                    }
                    if (rgb != -1)
                    {
                        int index = lut.IndexOf(rgb);
                        byte value = (byte)index;
                        if (index == -1 && lut.Count < 256)
                        {
                            lut.Add(rgb);
                            value = (byte)(lut.Count - 1);
                            // Write the value to the LUT
                            MemMgrRef.WriteByte(value * 4 + lutPointer, data[pointer]);
                            MemMgrRef.WriteByte(value * 4 + 1 + lutPointer, data[pointer + 1]);
                            MemMgrRef.WriteByte(value * 4 + 2 + lutPointer, data[pointer + 2]);
                            MemMgrRef.WriteByte(value * 4 + 3 + lutPointer, 0xFF);
                        }
                        MemMgrRef.WriteByte(videoPointer++, value);
                    }
                }
            }
        }

        private void BitmapLoader_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }


        private static byte HighByte(int value)
        {
            return ((byte)(value >> 16));
        }

        private static byte MidByte(int value)
        {
            return ((byte)((value >> 8) & 0xFF));
        }
        private static byte LowByte(int value)
        {
            return ((byte)(value & 0xFF));
        }
    }
}
