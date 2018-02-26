using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GumTree.Helper
{
    public class FileBrowseHelper
    {
        public static string UploadTextFile(string DirectoryPath)
        {
            string FilePath = string.Empty;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = DirectoryPath;
                ofd.Filter = "Text Files (*.txt)|*.txt";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FilePath = ofd.FileName;
                }
            }

            return FilePath;
        }

        public static List<string> ReadFileLineByLine(string filename)
        {
            List<string> listFileContent = new List<string>();

            if (!string.IsNullOrEmpty(filename))
            {
                string[] all_lines = File.ReadAllLines(filename);

                foreach (var item in all_lines)
                {
                    try
                    {
                        listFileContent.Add(item);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            return listFileContent;
        }
    }
}
