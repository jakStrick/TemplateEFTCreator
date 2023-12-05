using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using TemplateEFTCreator.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

/***************************************************************************
 * This app creates an EFT file based on the EVG Model and mapping inputs.
 * Reads in Model types from and xml file.
 * Allows user to select a foler where the output file is created.
 * If there is no file one will be created.
 * Creator: David Strickland
 * Date: 11/11/2023
 * Version 2.1
 *************************************************************************/

namespace TemplateEFTCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitForm();
        }

        private bool m_init = true;
        private readonly FileManager fileManager = new FileManager();

        //add the path and model
        private void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (TextBoxesEmpty())
            {
                MessageBox.Show("Please Select a Model and enter Path and Model#. Model# must be Numeric");
                return;
            }

            string mText = comboBox1.SelectedItem.ToString();

            if (m_init)
            {
                comboBox1.Enabled = false;
                m_init = false;
                ButtonFinish.Enabled = true;
                fileManager.SetFolderName(mText);
                AddMappings("openTags");
            }

            AddMappings("mappingTags");
            ClearTextBoxes();
        }

        private void InitForm()
        {
            Model model = new Model();
            model.SetModelList();

            if (model.ModelList != null)
            {
                InitializeComponent();
                XElement mElements = XElement.Parse(model.ModelList.ToString());
                IEnumerable<string> models =
                 from seg in mElements.Descendants("Models")
                 select (string)seg;
                comboBox1.DataSource = models.ToList();
            }
            else
            {
                const string message =
                "Model list is empty. Please select a file with a model list.";
                const string caption = "Model list File Selection";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Information);
                InitForm();
            }

            ButtonFinish.Enabled = false;

            //format Form and load it in the center of screen
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            //Set EVG logo size
            var imageSize = pictureBox1.Image.Size;
            var fitSize = pictureBox1.ClientSize;
            pictureBox1.SizeMode = imageSize.Width > fitSize.Width || imageSize.Height > fitSize.Height ?
                PictureBoxSizeMode.Zoom : PictureBoxSizeMode.CenterImage;
        }

        //add the closing tags
        private void ButtonFinish_Click(object sender, EventArgs e)
        {
            ButtonFinish.Enabled = false;
            comboBox1.Enabled = true;
            m_init = true;

            AddMappings("closeTags");

            ClearTextBoxes();
        }

        private void AddMappings(string tagType)
        {
            Tags tags = new Tags();
            string model = comboBox1.SelectedItem.ToString();
            string path = textBoxPath.Text;
            string modelNum = textBoxModelNum.Text;

            fileManager.WriteToFile(tags.AddTags(model, path, modelNum, tagType));
        }

        private void ClearTextBoxes()
        {
            textBoxPath.Text = "";
            textBoxModelNum.Text = "";
        }

        private bool TextBoxesEmpty()
        {
            //string modelText = comboBox1 ;
            string pathText = textBoxPath.Text;
            string modelNumText = textBoxModelNum.Text;

            //int i = 0;
            bool isANumber = int.TryParse(modelNumText, out _);

            if (comboBox1.SelectedIndex == -1 || pathText == "" || modelNumText == "" || !isANumber)
            {
                return true;
            }

            return false;
        }

        //Exit the app.
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Close();
        }
    }
}