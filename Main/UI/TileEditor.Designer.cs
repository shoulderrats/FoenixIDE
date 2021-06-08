﻿namespace FoenixIDE.Simulator.UI
{
    partial class TileEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            redPen.Dispose();
            whiteBrush.Dispose();
            whitePen.Dispose();
            yellowPen.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TileEditor));
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.Tilemap3Button = new System.Windows.Forms.Button();
            this.Tilemap2Button = new System.Windows.Forms.Button();
            this.Tilemap1Button = new System.Windows.Forms.Button();
            this.Tilemap0Button = new System.Windows.Forms.Button();
            this.TilesetViewer = new System.Windows.Forms.PictureBox();
            this.TileSelectedLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TilemapGroup = new System.Windows.Forms.GroupBox();
            this.WindowY = new System.Windows.Forms.TextBox();
            this.WindowX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SaveTilesetButton = new System.Windows.Forms.Button();
            this.ClearTilemapButton = new System.Windows.Forms.Button();
            this.TileHeight = new System.Windows.Forms.TextBox();
            this.TileWidth = new System.Windows.Forms.TextBox();
            this.YLabel = new System.Windows.Forms.Label();
            this.XLabel = new System.Windows.Forms.Label();
            this.TilemapAddress = new System.Windows.Forms.TextBox();
            this.TilesetAddressLabel = new System.Windows.Forms.Label();
            this.TilemapEnabledCheckbox = new System.Windows.Forms.CheckBox();
            this.TilesetList = new System.Windows.Forms.ComboBox();
            this.TilesetGroup = new System.Windows.Forms.GroupBox();
            this.Stride256Checkbox = new System.Windows.Forms.CheckBox();
            this.LutList = new System.Windows.Forms.ComboBox();
            this.TilesetAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.HeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TilesetViewer)).BeginInit();
            this.TilemapGroup.SuspendLayout();
            this.TilesetGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HeaderPanel.Controls.Add(this.Tilemap3Button);
            this.HeaderPanel.Controls.Add(this.Tilemap2Button);
            this.HeaderPanel.Controls.Add(this.Tilemap1Button);
            this.HeaderPanel.Controls.Add(this.Tilemap0Button);
            this.HeaderPanel.Location = new System.Drawing.Point(1, -2);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(364, 29);
            this.HeaderPanel.TabIndex = 0;
            // 
            // Tilemap3Button
            // 
            this.Tilemap3Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Tilemap3Button.Location = new System.Drawing.Point(246, 3);
            this.Tilemap3Button.Name = "Tilemap3Button";
            this.Tilemap3Button.Size = new System.Drawing.Size(75, 23);
            this.Tilemap3Button.TabIndex = 3;
            this.Tilemap3Button.Tag = "3";
            this.Tilemap3Button.Text = "Tilemap 3";
            this.Tilemap3Button.UseVisualStyleBackColor = true;
            this.Tilemap3Button.Click += new System.EventHandler(this.Tilemap0Button_Click);
            // 
            // Tilemap2Button
            // 
            this.Tilemap2Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Tilemap2Button.Location = new System.Drawing.Point(165, 3);
            this.Tilemap2Button.Name = "Tilemap2Button";
            this.Tilemap2Button.Size = new System.Drawing.Size(75, 23);
            this.Tilemap2Button.TabIndex = 2;
            this.Tilemap2Button.Tag = "2";
            this.Tilemap2Button.Text = "Tilemap 2";
            this.Tilemap2Button.UseVisualStyleBackColor = true;
            this.Tilemap2Button.Click += new System.EventHandler(this.Tilemap0Button_Click);
            // 
            // Tilemap1Button
            // 
            this.Tilemap1Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Tilemap1Button.Location = new System.Drawing.Point(84, 3);
            this.Tilemap1Button.Name = "Tilemap1Button";
            this.Tilemap1Button.Size = new System.Drawing.Size(75, 23);
            this.Tilemap1Button.TabIndex = 1;
            this.Tilemap1Button.Tag = "1";
            this.Tilemap1Button.Text = "Tilemap 1";
            this.Tilemap1Button.UseVisualStyleBackColor = true;
            this.Tilemap1Button.Click += new System.EventHandler(this.Tilemap0Button_Click);
            // 
            // Tilemap0Button
            // 
            this.Tilemap0Button.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.Tilemap0Button.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Tilemap0Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Tilemap0Button.Location = new System.Drawing.Point(3, 3);
            this.Tilemap0Button.Name = "Tilemap0Button";
            this.Tilemap0Button.Size = new System.Drawing.Size(75, 23);
            this.Tilemap0Button.TabIndex = 0;
            this.Tilemap0Button.Tag = "0";
            this.Tilemap0Button.Text = "Tilemap 0";
            this.Tilemap0Button.UseVisualStyleBackColor = false;
            this.Tilemap0Button.Click += new System.EventHandler(this.Tilemap0Button_Click);
            // 
            // TilesetViewer
            // 
            this.TilesetViewer.BackColor = System.Drawing.Color.Black;
            this.TilesetViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TilesetViewer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TilesetViewer.Location = new System.Drawing.Point(46, 246);
            this.TilesetViewer.Name = "TilesetViewer";
            this.TilesetViewer.Size = new System.Drawing.Size(276, 276);
            this.TilesetViewer.TabIndex = 4;
            this.TilesetViewer.TabStop = false;
            this.TilesetViewer.Paint += new System.Windows.Forms.PaintEventHandler(this.TilesetViewer_Paint);
            this.TilesetViewer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TilesetViewer_MouseClick);
            this.TilesetViewer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TilesetViewer_MouseMove);
            // 
            // TileSelectedLabel
            // 
            this.TileSelectedLabel.AutoSize = true;
            this.TileSelectedLabel.Location = new System.Drawing.Point(185, 170);
            this.TileSelectedLabel.Name = "TileSelectedLabel";
            this.TileSelectedLabel.Size = new System.Drawing.Size(81, 13);
            this.TileSelectedLabel.TabIndex = 4;
            this.TileSelectedLabel.Text = "Tile Selected: $";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tileset:";
            // 
            // TilemapGroup
            // 
            this.TilemapGroup.Controls.Add(this.WindowY);
            this.TilemapGroup.Controls.Add(this.WindowX);
            this.TilemapGroup.Controls.Add(this.label2);
            this.TilemapGroup.Controls.Add(this.label3);
            this.TilemapGroup.Controls.Add(this.SaveTilesetButton);
            this.TilemapGroup.Controls.Add(this.ClearTilemapButton);
            this.TilemapGroup.Controls.Add(this.TileHeight);
            this.TilemapGroup.Controls.Add(this.TileWidth);
            this.TilemapGroup.Controls.Add(this.YLabel);
            this.TilemapGroup.Controls.Add(this.XLabel);
            this.TilemapGroup.Controls.Add(this.TilemapAddress);
            this.TilemapGroup.Controls.Add(this.TilesetAddressLabel);
            this.TilemapGroup.Controls.Add(this.TilemapEnabledCheckbox);
            this.TilemapGroup.Location = new System.Drawing.Point(5, 30);
            this.TilemapGroup.Name = "TilemapGroup";
            this.TilemapGroup.Size = new System.Drawing.Size(353, 133);
            this.TilemapGroup.TabIndex = 1;
            this.TilemapGroup.TabStop = false;
            this.TilemapGroup.Text = "Tilemap Properties";
            // 
            // WindowY
            // 
            this.WindowY.Location = new System.Drawing.Point(223, 66);
            this.WindowY.MaxLength = 4;
            this.WindowY.Name = "WindowY";
            this.WindowY.Size = new System.Drawing.Size(38, 20);
            this.WindowY.TabIndex = 10;
            this.WindowY.Text = "1023";
            this.WindowY.TextChanged += new System.EventHandler(this.WindowY_TextChanged);
            // 
            // WindowX
            // 
            this.WindowX.Location = new System.Drawing.Point(84, 66);
            this.WindowX.MaxLength = 4;
            this.WindowX.Name = "WindowX";
            this.WindowX.Size = new System.Drawing.Size(38, 20);
            this.WindowX.TabIndex = 8;
            this.WindowX.Text = "1023";
            this.WindowX.TextChanged += new System.EventHandler(this.WindowX_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(154, 69);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Window Y:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 69);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Window X:";
            // 
            // SaveTilesetButton
            // 
            this.SaveTilesetButton.Location = new System.Drawing.Point(199, 101);
            this.SaveTilesetButton.Name = "SaveTilesetButton";
            this.SaveTilesetButton.Size = new System.Drawing.Size(86, 23);
            this.SaveTilesetButton.TabIndex = 12;
            this.SaveTilesetButton.Text = "Save Tileset";
            this.SaveTilesetButton.UseVisualStyleBackColor = true;
            this.SaveTilesetButton.Click += new System.EventHandler(this.SaveTilemapButton_Click);
            // 
            // ClearTilemapButton
            // 
            this.ClearTilemapButton.Location = new System.Drawing.Point(109, 101);
            this.ClearTilemapButton.Name = "ClearTilemapButton";
            this.ClearTilemapButton.Size = new System.Drawing.Size(86, 23);
            this.ClearTilemapButton.TabIndex = 11;
            this.ClearTilemapButton.Text = "Clear Tileset";
            this.ClearTilemapButton.UseVisualStyleBackColor = true;
            this.ClearTilemapButton.Click += new System.EventHandler(this.ClearTilemapButton_Click);
            // 
            // Height
            // 
            this.TileHeight.Location = new System.Drawing.Point(223, 46);
            this.TileHeight.MaxLength = 4;
            this.TileHeight.Name = "Height";
            this.TileHeight.Size = new System.Drawing.Size(38, 20);
            this.TileHeight.TabIndex = 6;
            this.TileHeight.Text = "1023";
            this.TileHeight.TextChanged += new System.EventHandler(this.Height_TextChanged);
            // 
            // Width
            // 
            this.TileWidth.Location = new System.Drawing.Point(84, 46);
            this.TileWidth.MaxLength = 4;
            this.TileWidth.Name = "Width";
            this.TileWidth.Size = new System.Drawing.Size(38, 20);
            this.TileWidth.TabIndex = 4;
            this.TileWidth.Text = "1023";
            this.TileWidth.TextChanged += new System.EventHandler(this.Width_TextChanged);
            // 
            // YLabel
            // 
            this.YLabel.AutoSize = true;
            this.YLabel.Location = new System.Drawing.Point(154, 49);
            this.YLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.YLabel.Name = "YLabel";
            this.YLabel.Size = new System.Drawing.Size(41, 13);
            this.YLabel.TabIndex = 5;
            this.YLabel.Text = "Height:";
            // 
            // XLabel
            // 
            this.XLabel.AutoSize = true;
            this.XLabel.Location = new System.Drawing.Point(14, 49);
            this.XLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.XLabel.Name = "XLabel";
            this.XLabel.Size = new System.Drawing.Size(38, 13);
            this.XLabel.TabIndex = 3;
            this.XLabel.Text = "Width:";
            // 
            // TilemapAddress
            // 
            this.TilemapAddress.Location = new System.Drawing.Point(257, 17);
            this.TilemapAddress.MaxLength = 6;
            this.TilemapAddress.Name = "TilemapAddress";
            this.TilemapAddress.Size = new System.Drawing.Size(88, 20);
            this.TilemapAddress.TabIndex = 2;
            this.TilemapAddress.TextChanged += new System.EventHandler(this.TilemapAddress_TextChanged);
            // 
            // TilesetAddressLabel
            // 
            this.TilesetAddressLabel.AutoSize = true;
            this.TilesetAddressLabel.Location = new System.Drawing.Point(154, 20);
            this.TilesetAddressLabel.Name = "TilesetAddressLabel";
            this.TilesetAddressLabel.Size = new System.Drawing.Size(97, 13);
            this.TilesetAddressLabel.TabIndex = 1;
            this.TilesetAddressLabel.Text = "Tilemap Address: $";
            // 
            // TilemapEnabledCheckbox
            // 
            this.TilemapEnabledCheckbox.AutoSize = true;
            this.TilemapEnabledCheckbox.Location = new System.Drawing.Point(14, 19);
            this.TilemapEnabledCheckbox.Name = "TilemapEnabledCheckbox";
            this.TilemapEnabledCheckbox.Size = new System.Drawing.Size(105, 17);
            this.TilemapEnabledCheckbox.TabIndex = 0;
            this.TilemapEnabledCheckbox.Text = "Tilemap Enabled";
            this.TilemapEnabledCheckbox.UseVisualStyleBackColor = true;
            this.TilemapEnabledCheckbox.CheckedChanged += new System.EventHandler(this.TilemapEnabledCheckbox_CheckedChanged);
            // 
            // TilesetList
            // 
            this.TilesetList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TilesetList.FormattingEnabled = true;
            this.TilesetList.Items.AddRange(new object[] {
            "Tileset 0",
            "Tileset 1",
            "Tileset 2",
            "Tileset 3"});
            this.TilesetList.Location = new System.Drawing.Point(48, 165);
            this.TilesetList.Name = "TilesetList";
            this.TilesetList.Size = new System.Drawing.Size(121, 21);
            this.TilesetList.TabIndex = 3;
            this.TilesetList.SelectedIndexChanged += new System.EventHandler(this.TilesetList_SelectedIndexChanged);
            // 
            // TilesetGroup
            // 
            this.TilesetGroup.Controls.Add(this.Stride256Checkbox);
            this.TilesetGroup.Controls.Add(this.LutList);
            this.TilesetGroup.Controls.Add(this.TilesetAddress);
            this.TilesetGroup.Controls.Add(this.label4);
            this.TilesetGroup.Location = new System.Drawing.Point(5, 192);
            this.TilesetGroup.Name = "TilesetGroup";
            this.TilesetGroup.Size = new System.Drawing.Size(353, 48);
            this.TilesetGroup.TabIndex = 5;
            this.TilesetGroup.TabStop = false;
            this.TilesetGroup.Text = "Tileset Properties";
            // 
            // Stride256Checkbox
            // 
            this.Stride256Checkbox.AutoSize = true;
            this.Stride256Checkbox.Location = new System.Drawing.Point(273, 19);
            this.Stride256Checkbox.Name = "Stride256Checkbox";
            this.Stride256Checkbox.Size = new System.Drawing.Size(74, 17);
            this.Stride256Checkbox.TabIndex = 3;
            this.Stride256Checkbox.Text = "Stride 256";
            this.Stride256Checkbox.UseVisualStyleBackColor = true;
            this.Stride256Checkbox.CheckedChanged += new System.EventHandler(this.LutList_SelectedIndexChanged);
            // 
            // LutList
            // 
            this.LutList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LutList.FormattingEnabled = true;
            this.LutList.Items.AddRange(new object[] {
            "LUT 0",
            "LUT 1",
            "LUT 2",
            "LUT 3"});
            this.LutList.Location = new System.Drawing.Point(203, 17);
            this.LutList.Name = "LutList";
            this.LutList.Size = new System.Drawing.Size(62, 21);
            this.LutList.TabIndex = 2;
            this.LutList.SelectedIndexChanged += new System.EventHandler(this.LutList_SelectedIndexChanged);
            // 
            // TilesetAddress
            // 
            this.TilesetAddress.Location = new System.Drawing.Point(109, 17);
            this.TilesetAddress.MaxLength = 6;
            this.TilesetAddress.Name = "TilesetAddress";
            this.TilesetAddress.Size = new System.Drawing.Size(88, 20);
            this.TilesetAddress.TabIndex = 1;
            this.TilesetAddress.TextChanged += new System.EventHandler(this.TilesetAddress_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Tileset Address: $";
            // 
            // TileEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 545);
            this.Controls.Add(this.TilesetGroup);
            this.Controls.Add(this.TilesetList);
            this.Controls.Add(this.TilemapGroup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TileSelectedLabel);
            this.Controls.Add(this.TilesetViewer);
            this.Controls.Add(this.HeaderPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "TileEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Tile Editor";
            this.Load += new System.EventHandler(this.TileEditor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TileEditor_KeyDown);
            this.HeaderPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TilesetViewer)).EndInit();
            this.TilemapGroup.ResumeLayout(false);
            this.TilemapGroup.PerformLayout();
            this.TilesetGroup.ResumeLayout(false);
            this.TilesetGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.Button Tilemap3Button;
        private System.Windows.Forms.Button Tilemap2Button;
        private System.Windows.Forms.Button Tilemap1Button;
        private System.Windows.Forms.Label TileSelectedLabel;
        private System.Windows.Forms.PictureBox TilesetViewer;
        private System.Windows.Forms.Button Tilemap0Button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox TilemapGroup;
        private System.Windows.Forms.TextBox WindowY;
        private System.Windows.Forms.TextBox WindowX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SaveTilesetButton;
        private System.Windows.Forms.Button ClearTilemapButton;
        private System.Windows.Forms.TextBox TileHeight;
        private System.Windows.Forms.TextBox TileWidth;
        private System.Windows.Forms.Label YLabel;
        private System.Windows.Forms.Label XLabel;
        private System.Windows.Forms.TextBox TilemapAddress;
        private System.Windows.Forms.Label TilesetAddressLabel;
        private System.Windows.Forms.CheckBox TilemapEnabledCheckbox;
        private System.Windows.Forms.ComboBox TilesetList;
        private System.Windows.Forms.GroupBox TilesetGroup;
        private System.Windows.Forms.TextBox TilesetAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox Stride256Checkbox;
        private System.Windows.Forms.ComboBox LutList;
    }
}