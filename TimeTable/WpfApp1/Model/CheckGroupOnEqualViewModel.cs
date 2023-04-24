﻿using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model.ExcelFile;

namespace WpfApp1.Model
{
    public class CheckGroupOnEqualViewModel
    {
        public GroupFileMeneger groupFileManager = new GroupFileMeneger();
        private List<GroupsAll> groups;
        private List<GroupsAll> badGroups;
        public List<GroupsAll> Groups { get { return groups; } }
        public List<GroupsAll> BadGroups { get { return badGroups; } }
        public CheckGroupOnEqualViewModel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            groupFileManager = new GroupFileMeneger();
            groups = new List<GroupsAll>();
            badGroups = new List<GroupsAll>();
            InitIdialGroupListAsync();
        }
        public async Task InitIdialGroupListAsync()
        {
            try
            {
                List<Group> file = await groupFileManager.Read();
                foreach (var item in file)
                {
                    groups.Add(new GroupsAll(item.Name, item.Cource, item.StudentNumber));
                }
                InitBadGroupList();

            }
            catch (Exception ex)
            {

            }

        }
        public void InitBadGroupList()
        {
            using (ExcelPackage excelPackage = new ExcelPackage(CheckGroupOnEqual.TimetableFile))
            {
                try
                {
                    GetGroupFromTimetable(excelPackage);
                }
                catch (Exception ex)
                {

                }

            }
        }
        public void GetGroupFromTimetable(ExcelPackage excelPackage)
        {
            try
            {
                List<GroupsAll> groupFromTimetable = new List<GroupsAll>();
                int listCount=excelPackage.Workbook.Worksheets.Count();
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

                        if (item.Cells[7, i].Value != null && item.Column(i).Width > 0)
                        {
                            if (!item.Cells[7, i].Value.ToString().Contains("время") && !item.Cells[7, i].Value.ToString().Contains("пара") && item.Cells[7, i].Value.ToString() != "1" && item.Cells[7, i].Value.ToString() != "2")
                            {
                                bool contains = false;
                                foreach (var group in groupFromTimetable)
                                {
                                    if (group.GroupNames == item.Cells[7, i].Value.ToString())
                                    {
                                        contains = true;
                                    }
                                }
                                if (!contains)
                                {
                                    if (item.Cells[7, i].Value.ToString().Contains("51") || item.Cells[7, i].Value.ToString().Contains("52"))
                                    {
                                        groupFromTimetable.Add(new GroupsAll(item.Cells[7, i].Value.ToString(), 5));
                                    }
                                    if (item.Cells[7, i].Value.ToString().Contains("41") || item.Cells[7, i].Value.ToString().Contains("42"))
                                    {
                                        groupFromTimetable.Add(new GroupsAll(item.Cells[7, i].Value.ToString(), 4));
                                    }
                                    if (item.Cells[7, i].Value.ToString().Contains("31") || item.Cells[7, i].Value.ToString().Contains("32"))
                                    {
                                        groupFromTimetable.Add(new GroupsAll(item.Cells[7, i].Value.ToString(), 3));
                                    }
                                    if (item.Cells[7, i].Value.ToString().Contains("21") || item.Cells[7, i].Value.ToString().Contains("22"))
                                    {
                                        groupFromTimetable.Add(new GroupsAll(item.Cells[7, i].Value.ToString(), 2));
                                    }
                                    if (item.Cells[7, i].Value.ToString().Contains("11") || item.Cells[7, i].Value.ToString().Contains("12"))
                                    {
                                        groupFromTimetable.Add(new GroupsAll(item.Cells[7, i].Value.ToString(), 1));
                                    }
                                }
                            }

                        }
                    }
                }
                foreach (var item in groupFromTimetable)
                {
                    bool flag = false;
                    foreach (var group in groups)
                    {
                        if (item.GroupNames==group.GroupNames)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        badGroups.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}