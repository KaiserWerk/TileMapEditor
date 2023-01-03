using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;
using TileMapEditor.Model;

namespace TileMapEditor
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool formEnabled;
        public bool FormEnabled { get => formEnabled; set { formEnabled = value; OnPropertyChanged(nameof(FormEnabled)); } }

        private bool isWelcomeScreenActive = true;
        public bool IsWelcomeScreenActive { get => isWelcomeScreenActive; set { isWelcomeScreenActive = value; OnPropertyChanged(nameof(IsWelcomeScreenActive)); } }

        private bool isWorkScreenActive = false;
        public bool IsWorkScreenActive { get => isWorkScreenActive; set { isWorkScreenActive = value; OnPropertyChanged(nameof(IsWorkScreenActive)); } }

        private string filename;
        public string Filename { get => filename; set { filename = value; OnPropertyChanged(nameof(Filename)); } }

        private List<MapField> allMapFields;
        public List<MapField> AllMapFields { get => allMapFields; set { allMapFields = value; OnPropertyChanged(nameof(AllMapFields)); } }

        public MainWindow()
        {
            InitializeComponent();
        }



        private void LoadMap_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Maps (*.map)|*.map|JSON files (*.json)|*.json";
            if (fd.ShowDialog() == true)
            {
                this.Filename = fd.FileName;

                try
                {
                    this.AllMapFields = JsonSerializer.Deserialize<List<MapField>>(File.ReadAllText(this.Filename));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load map: " + ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (this.IsWelcomeScreenActive)
                {
                    this.IsWelcomeScreenActive = false;
                    this.IsWorkScreenActive = true;
                    this.FormEnabled = true;
                }
            }

        }

        private void CreateMap_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "Maps (*.map)|*.map";
            if (fd.ShowDialog() == true)
            {
                this.Filename = fd.FileName;

                if (this.IsWelcomeScreenActive)
                {
                    this.IsWelcomeScreenActive = false;
                    this.IsWorkScreenActive = true;
                    this.FormEnabled = true;
                }
            }

        }

        private void GenerateRandomMap_Click(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
