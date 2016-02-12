using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using SlimDX;
using KUtility;
using System.IO;

namespace ProjectMoretz
{
    public partial class MainForm : Form
    {
        public const string OVERVIEWS_PATH = "overviewsPath", MAPS_PATH = "mapsPath";

        public static string overviewsPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Steam\\steamapps\\common\\Counter-Strike Global Offensive\\csgo\\resource\\overviews";
        public static string mapsPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Steam\\steamapps\\common\\Counter-Strike Global Offensive\\csgo\\maps";

        public MainForm()
        {
            InitializeComponent();

            LoadSettings();
            FillOverviewsPathBox();
            FillMapsList();
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (sender == overviewsBrowseButton)
            {
                FolderBrowserDialog selectFolder = new FolderBrowserDialog();
                if(System.IO.Directory.Exists(overviewsPath)) selectFolder.SelectedPath = overviewsPath;

                if (selectFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    overviewsPath = selectFolder.SelectedPath;
                    FillOverviewsPathBox();
                    SaveSettings();
                }
            }
            if (sender == addMapButton)
            {
                OpenFileDialog selectFile = new OpenFileDialog();
                selectFile.Filter = "BSP|*.bsp";
                selectFile.Multiselect = true;
                if (System.IO.Directory.Exists(mapsPath)) selectFile.InitialDirectory = mapsPath;

                if (selectFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (selectFile.CheckFileExists)
                    {
                        LoadMaps(selectFile.FileNames);
                        FillMapsList();
                        mapsPath = selectFile.FileName.Substring(0, selectFile.FileName.LastIndexOf("\\"));
                        SaveSettings();
                    }
                }
            }
        }

        private void FillOverviewsPathBox()
        {
            overviewsFolderBox.Text = overviewsPath;
        }
        private void FillMapsList()
        {
            mapsList.Items.Clear();
            for (int i = 0; i < BSPMap.loadedMaps.Keys.Count; i++)
            {
                mapsList.Items.Add(BSPMap.loadedMaps[BSPMap.loadedMaps.Keys.ElementAt(i)]);
            }
        }
        private void LoadMaps(params string[] files)
        {
            foreach (string file in files)
            {
                BSPMap newMap = new BSPMap(file);
                if(!newMap.alreadyLoaded) newMap.LoadMap();
            }
        }

        private void mapsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BSPMap selectedMap = (BSPMap)mapsList.SelectedItem;
            if (selectedMap.overviewImage != null && selectedMap.overviewImage.images.Length > 0) overviewBox.BackgroundImage = selectedMap.overviewImage.images[0];
        }

        private void overviewsFolderBox_TextChanged(object sender, EventArgs e)
        {
            overviewsPath = overviewsFolderBox.Text;
            SaveSettings();
        }

        public static void SaveSettings()
        {
            Properties.Settings.Default[OVERVIEWS_PATH] = overviewsPath;
            Properties.Settings.Default[MAPS_PATH] = mapsPath;
            Properties.Settings.Default.Save();
        }
        public static void LoadSettings()
        {
            overviewsPath = (string)Properties.Settings.Default[OVERVIEWS_PATH];
            mapsPath = (string)Properties.Settings.Default[MAPS_PATH];
        }
    }
}
