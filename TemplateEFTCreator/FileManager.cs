using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TemplateEFTCreator
{
    internal class FileManager
    {
        private readonly FolderBrowserDialog folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();

        private readonly OpenFileDialog openFileDialog1 = new OpenFileDialog();

        private string FolderName { get; set; }

        // choose where your file lives
        public void SetFolderName(string modelType)
        {
            const string message =
           "Select a location for your Template File output.";
            const string caption = "Template File Location";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);

            // If the ok button was pressed ...
            if (result == DialogResult.OK)
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    FolderName = folderBrowserDialog1.SelectedPath;
                    FolderName += "\\" + modelType + " Template" + DateTime.Now.ToString("_yyyyMMddHHmmss") + ".eft";
                }
            }
        }

        public string SetFileName()
        {
            const string message =
            "Select a Template Model File";
            const string caption = "Model File Selection";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
            try
            {
                // If the ok button was pressed ...
                if (result == DialogResult.OK)
                {
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string fileName = openFileDialog1.FileName;
                        return fileName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public void WriteToFile(string text)
        {
            string path = FolderName;

            // Create a file and add the opening tags
            // This text is added only once to the file.
            try
            {
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(text);
                    }
                }
                else
                {
                    // The text is added, making the file longer over time
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public XElement ReadFromFile(string filename)
        {
            try
            {
                // Load the XML file from template model file;
                var modelFile = filename;

                XElement models = XElement.Load(modelFile);

                return models;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

            return null;
        }
    }
}