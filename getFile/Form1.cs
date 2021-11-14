using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace getFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        private void btnSelectFile_Click(object sender, EventArgs e)
        {

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files | *.txt"; 
            dialog.Multiselect = false; 
            if (dialog.ShowDialog() == DialogResult.OK) 
            {
                String path = dialog.FileName; 
                FileReader fileReader = new FileReader(path);
                var lines = fileReader.Task1();
                var words = fileReader.Task2();
                var exclusiveWords = fileReader.Task3();
                var sentenceLength = fileReader.Task4();
                var popularWords = fileReader.Task5();
                var whithoutK = fileReader.Task6();
                var colors = fileReader.Task8();
                resultTextBox.Text = "מספר השורות בקובץ :"+lines.ToString() ;
                resultTextBox.AppendText(Environment.NewLine);
                resultTextBox.AppendText("מספר המילים בקובץ :"+ words.ToString());
                resultTextBox.AppendText(Environment.NewLine);
                resultTextBox.AppendText("מספר המילים היחודיות בקובץ : " + exclusiveWords.ToString());
                resultTextBox.AppendText(Environment.NewLine);
                resultTextBox.AppendText("אורך המשפט הממוצע :" + sentenceLength[1].ToString());
                resultTextBox.AppendText(Environment.NewLine);
                resultTextBox.AppendText("אורך המשפט המקסימלי :" + sentenceLength[0].ToString());
                resultTextBox.AppendText(Environment.NewLine);
                resultTextBox.AppendText("המילה הפופולארית ביותר בטקסט :" + popularWords[0].ToString());
                resultTextBox.AppendText(Environment.NewLine);
                resultTextBox.AppendText("המילה הפופולארית ביותר שאינה מילה בעלת משמעות תחבירית :" + popularWords[1].ToString());
                resultTextBox.AppendText(Environment.NewLine);
                resultTextBox.AppendText("רצף המילים הארוך ביותר בטקסט שלא מכיל את 'K' האות: " + whithoutK.ToString());
                resultTextBox.AppendText(Environment.NewLine);
                resultTextBox.AppendText("שמות צבעים מופיעים בטקסט, וכמה פעמים כל אחד מופיע: ");
                foreach(var item in colors)
                {
                    resultTextBox.AppendText(Environment.NewLine);
                    resultTextBox.AppendText(item);
                }
            }
        }

       
    }
}
