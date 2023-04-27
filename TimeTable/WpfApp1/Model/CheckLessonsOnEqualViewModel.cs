﻿using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WpfApp1.Model.ExcelFile;
using WpfApp1.Model.FileMenegers;

namespace WpfApp1.Model
{
    public class CheckLessonsOnEqualViewModel
    {
        public LessonsFileMeneger lessonsFileMeneger = new LessonsFileMeneger();
        public LessonsTypeFileMeneger lessonsTypeFileMeneger = new LessonsTypeFileMeneger();
        private ObservableCollection<LessonsAll> lessons;
        private ObservableCollection<LessonsType> lessonsType;
        private ObservableCollection<LessonsAll> badLessons;
        public ObservableCollection<LessonsAll> Lessons { get { return lessons; } }
        public ObservableCollection<LessonsAll> BadLessons { get { return badLessons; } }
        public CheckLessonsOnEqualViewModel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            lessonsFileMeneger = new LessonsFileMeneger();
            lessonsTypeFileMeneger = new LessonsTypeFileMeneger();
            lessons = new ObservableCollection<LessonsAll>();
            lessonsType = new ObservableCollection<LessonsType>();
            badLessons = new ObservableCollection<LessonsAll>();
            InitIdialLessonsTypeListAsync();

        }
        public async Task InitIdialLessonsTypeListAsync()
        {
            try
            {
                List<string> file = await lessonsFileMeneger.Read();
                List<string> fileType = await lessonsTypeFileMeneger.Read();
                foreach (var item in file)
                {
                    lessons.Add(new LessonsAll(item));
                }
                foreach (var item in fileType)
                {
                    lessonsType.Add(new LessonsType(item));
                }
                InitBadLessonsTypeList();
            }
            catch (Exception ex)
            {

            }

        }
        public void InitBadLessonsTypeList()
        {
            using (ExcelPackage excelPackage = new ExcelPackage(CheckLessonsOnEqual.TimetableFile))
            {
                try
                {
                    GetLessonsTypeFromTimetable(excelPackage);
                }
                catch (Exception ex)
                {

                }

            }
        }
        public void GetLessonsTypeFromTimetable(ExcelPackage excelPackage)
        {
            try
            {
                List<LessonsAll> lessonsTypeFromTimetable = new List<LessonsAll>();
                int listCount = excelPackage.Workbook.Worksheets.Count();
                List<ExcelWorksheet> anotherWorksheet = new List<ExcelWorksheet>();
                for (int i = 0; i < listCount; i++)
                {
                    anotherWorksheet.Add(excelPackage.Workbook.Worksheets[i]);
                }
                foreach (var item in anotherWorksheet)
                {
                    int col = item.Dimension.End.Column;
                    for (int i = 1; i < col; i++)
                    {
                        double width = item.Column(i).Width;
                        for (int j = 8; j < 87; j = j + 2)
                        {
                            if (item.Cells[j, i].Value != null && item.Column(i).Width > 0)
                            {
                                if (!item.Cells[j, i].Value.ToString().ToLower().Contains("понедельник") &&
                                    !item.Cells[j, i].Value.ToString().ToLower().Contains("вторник") &&
                                    !item.Cells[j, i].Value.ToString().ToLower().Contains("среда") &&
                                    !item.Cells[j, i].Value.ToString().ToLower().Contains("четверг") &&
                                    !item.Cells[j, i].Value.ToString().ToLower().Contains("пятница") &&
                                    !item.Cells[j, i].Value.ToString().ToLower().Contains("суббота") &&
                                    !Regex.IsMatch(item.Cells[j, i].Value.ToString(), @"^[0-9]{3,5}.[0-9]{3,5}", RegexOptions.IgnoreCase) &&
                                    item.Cells[j, i].Value.ToString() != "1" &&
                                    item.Cells[j, i].Value.ToString() != "2" &&
                                    item.Cells[j, i].Value.ToString() != "3" &&
                                    item.Cells[j, i].Value.ToString() != "4" &&
                                    item.Cells[j, i].Value.ToString() != "5" &&
                                    item.Cells[j, i].Value.ToString() != "6" &&
                                    item.Cells[j, i].Value.ToString() != "7")
                                {
                                    Regex regex = new Regex(@"-л.{0,2}$|-п.{0,2}$");
                                    MatchCollection matches = regex.Matches(item.Cells[j, i].Value.ToString());
                                    if (matches.Count > 0)
                                    {
                                        int index = item.Cells[j, i].Value.ToString().IndexOf(matches[0].Value);
                                        string str = item.Cells[j, i].Value.ToString().Substring(0,index).Trim();
                                        lessonsTypeFromTimetable.Add(new LessonsAll(str));
                                    }
                                }

                            }
                        }

                    }
                }
                foreach (var item in lessonsTypeFromTimetable)
                {
                    bool flag = false;
                    foreach (var lessonType in lessons)
                    {
                        if (item.Names == lessonType.Names)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        bool flag2 = false;
                        foreach (var bad in badLessons)
                        {
                            if (item.Names == bad.Names)
                            {
                                flag2 = true;
                                break;
                            }
                        }
                        if (!flag2)
                        {
                            badLessons.Add(new LessonsAll(item.Names));
                        }

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}