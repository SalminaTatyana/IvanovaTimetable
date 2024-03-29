﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model.FileMenegers
{
    public class LessonsTypeFileMeneger
    {

        public string path = "file/lessonsTypeFile" + ".txt";
        public async Task Save(List<string> file)
        {
            if (!Directory.Exists("file"))
            {
                Directory.CreateDirectory("file");
            }
            if (!File.Exists(path))
            {
                File.CreateText(path);
            }
            // сохранение данных
            if (file!=null)
            {
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    foreach (string item in file)
                    {
                        await writer.WriteLineAsync(item);
                    }
                }
            }
          
        }
        public async Task<List<string>> Read()
        {
            if (!Directory.Exists("file"))
            {
                Directory.CreateDirectory("file");
            }
            List<string> files = new List<string>();
            if (!File.Exists(path))
            {
                File.CreateText(path);
            }
            using (StreamReader reader = new StreamReader(path))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    try
                    {
                        files.Add(line);
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }

            return files;
        }
        public void Clear()
        {
            if (!Directory.Exists("file"))
            {
                Directory.CreateDirectory("file");
            }
            if (!File.Exists(path))
            {
                File.CreateText(path);
            }
            StreamWriter fs = new StreamWriter(path, false);
            fs.Close();
        }
    }
}
