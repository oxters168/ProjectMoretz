namespace ProjectMoretz
{
    partial class MainForm
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
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainPanel = new System.Windows.Forms.Panel();
            this.overviewsLabel = new System.Windows.Forms.Label();
            this.overviewsBrowseButton = new System.Windows.Forms.Button();
            this.overviewsFolderBox = new System.Windows.Forms.TextBox();
            this.overviewMapsSplit = new System.Windows.Forms.SplitContainer();
            this.overviewBox = new System.Windows.Forms.PictureBox();
            this.addMapButton = new System.Windows.Forms.Button();
            this.mapsList = new System.Windows.Forms.ListBox();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.overviewMapsSplit)).BeginInit();
            this.overviewMapsSplit.Panel1.SuspendLayout();
            this.overviewMapsSplit.Panel2.SuspendLayout();
            this.overviewMapsSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.overviewBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.overviewsLabel);
            this.mainPanel.Controls.Add(this.overviewsBrowseButton);
            this.mainPanel.Controls.Add(this.overviewsFolderBox);
            this.mainPanel.Controls.Add(this.overviewMapsSplit);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(535, 396);
            this.mainPanel.TabIndex = 0;
            // 
            // overviewsLabel
            // 
            this.overviewsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.overviewsLabel.AutoSize = true;
            this.overviewsLabel.Location = new System.Drawing.Point(9, 348);
            this.overviewsLabel.Name = "overviewsLabel";
            this.overviewsLabel.Size = new System.Drawing.Size(89, 13);
            this.overviewsLabel.TabIndex = 8;
            this.overviewsLabel.Text = "Overviews Folder";
            // 
            // overviewsBrowseButton
            // 
            this.overviewsBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.overviewsBrowseButton.Location = new System.Drawing.Point(240, 363);
            this.overviewsBrowseButton.Name = "overviewsBrowseButton";
            this.overviewsBrowseButton.Size = new System.Drawing.Size(75, 20);
            this.overviewsBrowseButton.TabIndex = 7;
            this.overviewsBrowseButton.Text = "Browse";
            this.overviewsBrowseButton.UseVisualStyleBackColor = true;
            this.overviewsBrowseButton.Click += new System.EventHandler(this.button_Click);
            // 
            // overviewsFolderBox
            // 
            this.overviewsFolderBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.overviewsFolderBox.Location = new System.Drawing.Point(12, 364);
            this.overviewsFolderBox.Name = "overviewsFolderBox";
            this.overviewsFolderBox.Size = new System.Drawing.Size(222, 20);
            this.overviewsFolderBox.TabIndex = 4;
            this.overviewsFolderBox.TextChanged += new System.EventHandler(this.overviewsFolderBox_TextChanged);
            // 
            // overviewMapsSplit
            // 
            this.overviewMapsSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.overviewMapsSplit.Location = new System.Drawing.Point(12, 12);
            this.overviewMapsSplit.Name = "overviewMapsSplit";
            // 
            // overviewMapsSplit.Panel1
            // 
            this.overviewMapsSplit.Panel1.Controls.Add(this.overviewBox);
            // 
            // overviewMapsSplit.Panel2
            // 
            this.overviewMapsSplit.Panel2.Controls.Add(this.addMapButton);
            this.overviewMapsSplit.Panel2.Controls.Add(this.mapsList);
            this.overviewMapsSplit.Size = new System.Drawing.Size(511, 254);
            this.overviewMapsSplit.SplitterDistance = 398;
            this.overviewMapsSplit.TabIndex = 3;
            // 
            // overviewBox
            // 
            this.overviewBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.overviewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.overviewBox.Location = new System.Drawing.Point(0, 0);
            this.overviewBox.Name = "overviewBox";
            this.overviewBox.Size = new System.Drawing.Size(398, 254);
            this.overviewBox.TabIndex = 1;
            this.overviewBox.TabStop = false;
            // 
            // addMapButton
            // 
            this.addMapButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addMapButton.Location = new System.Drawing.Point(3, 228);
            this.addMapButton.Name = "addMapButton";
            this.addMapButton.Size = new System.Drawing.Size(103, 23);
            this.addMapButton.TabIndex = 7;
            this.addMapButton.Text = "Add Map";
            this.addMapButton.UseVisualStyleBackColor = true;
            this.addMapButton.Click += new System.EventHandler(this.button_Click);
            // 
            // mapsList
            // 
            this.mapsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapsList.FormattingEnabled = true;
            this.mapsList.Items.AddRange(new object[] {
            "de_dust2",
            "cs_assault",
            "de_inferno",
            "de_mirage",
            "de_cache"});
            this.mapsList.Location = new System.Drawing.Point(0, 0);
            this.mapsList.Name = "mapsList";
            this.mapsList.Size = new System.Drawing.Size(109, 212);
            this.mapsList.TabIndex = 0;
            this.mapsList.SelectedIndexChanged += new System.EventHandler(this.mapsList_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 396);
            this.Controls.Add(this.mainPanel);
            this.MinimumSize = new System.Drawing.Size(345, 240);
            this.Name = "MainForm";
            this.Text = "GO:Sight";
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.overviewMapsSplit.Panel1.ResumeLayout(false);
            this.overviewMapsSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.overviewMapsSplit)).EndInit();
            this.overviewMapsSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.overviewBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.ListBox mapsList;
        private System.Windows.Forms.PictureBox overviewBox;
        private System.Windows.Forms.SplitContainer overviewMapsSplit;
        private System.Windows.Forms.Label overviewsLabel;
        private System.Windows.Forms.Button overviewsBrowseButton;
        private System.Windows.Forms.TextBox overviewsFolderBox;
        private System.Windows.Forms.Button addMapButton;
    }
}

