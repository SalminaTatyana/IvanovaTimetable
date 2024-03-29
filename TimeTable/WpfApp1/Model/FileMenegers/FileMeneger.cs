﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class FileManager
    {
        public  string path = "file/saveExelsFile" + ".json";
        public  async Task Save(OldFile file)
        {
            if (!Directory.Exists("file"))
            {
                Directory.CreateDirectory("file");
            }
            if (!File.Exists(path))
            {
                File.CreateText(path);
            }
            if (file!=null)
            {
                using (StreamWriter fs = new StreamWriter(path, true))
                {
                    fs.WriteLine(JsonConvert.SerializeObject(file).ToString());
                    fs.Close();
                }
            }
            // сохранение данных
           
        }
        public  async Task<List<OldFile>> Read()
        {
            if (!Directory.Exists("file"))
            {
                Directory.CreateDirectory("file");
            }
            List<OldFile> files = new List<OldFile>();
            if (!File.Exists(path))
            {
                File.CreateText(path);
            }
            using (StreamReader fs = new StreamReader(path, true))
            {
                while (fs.Peek() >= 0)
                {
                    files.Add(JsonConvert.DeserializeObject<OldFile>(fs.ReadLine().ToString()));
                }
                fs.Close();
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
