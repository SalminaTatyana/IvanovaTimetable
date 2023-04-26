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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для CheckLessonsTypeOnEqual.xaml
    /// </summary>
    public partial class CheckLessonsTypeOnEqual : Window
    {
        public CheckLessonsTypeOnEqual()
        {
            InitializeComponent();
        }
        public static FileInfo TimetableFile { get; set; }
       
        public CheckLessonsTypeOnEqual(FileInfo timetableFile)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            TimetableFile = timetableFile;
            InitializeComponent();

        }
    }
}
