﻿using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Model;
using WpfApp1.Model.ExcelFile;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public FileManager fileManager = new FileManager();
        public GroupFileMeneger groupFileManager = new GroupFileMeneger();
        public static FileInfo timetableFile;
        public List<Group> GroupList = new List<Group>();
        public MainWindow()
        {
            InitializeComponent();
        }
        
        
        
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        public void ChangeIdialGroupList()
        {

        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
