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
using System.Text.RegularExpressions;
using WindowsFormsApp2;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        private string CurentFilePass = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Документ был изменен. \nСохранить изменения?", "Сохранение документа", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            switch (result)
            {
                case DialogResult.Yes:
                    {
                        SaveAsToolStripMenuItem_Click(sender, e);
                        CodeWindow.Text = "";
                        CurentFilePass = "";
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        return;
                    }

                case DialogResult.No:
                    {
                        CodeWindow.Text = "";
                        CurentFilePass = "";
                        break;
                    }
            }

        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Все файлы (*.*)|*.*|Текстовые документы (*.txt)|*.txt|Документы Python (*.py)|*.py|Документы CPP (*.cpp)|*.cpp|Документы CS (*.cs)|*.cs";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    var fileStream = openFileDialog.OpenFile();
                    CurentFilePass = filePath;

                    using (StreamReader reader = new StreamReader(fileStream, System.Text.Encoding.UTF8))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }
            CodeWindow.Text = fileContent;
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(CurentFilePass))
            {
                string content = CodeWindow.Text;
                File.WriteAllText(CurentFilePass, content, System.Text.Encoding.UTF8);
            }

            else
            {
                SaveAsToolStripMenuItem_Click(sender, e);
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = " ";
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = "c:\\";
                saveFileDialog.Filter = "Все файлы (*.*)|*.*|Текстовые документы (*.txt)|*.txt|Документы Python (*.py)|*.py|Документы CPP (*.cpp)|*.cpp|Документы CS (*.cs)|*.cs";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog.FileName;
                }
            }


            string text = CodeWindow.Text;
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine(text);
                }

            }
            catch
            {

            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Документ был изменен. \nСохранить изменения?", "Сохранение документа", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            switch (result)
            {
                case DialogResult.Yes: // Да - сохранить и выйти
                    {
                        SaveToolStripMenuItem_Click(sender, e);
                        Close();
                        break;
                    }

                case DialogResult.Cancel: // Отмена - вернуться к документу
                    {
                        return;
                    }

                case DialogResult.No: // Нет - выйти без сохранения изменений
                    {
                        Close();
                        break;
                    }
            }
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodeWindow.Undo();
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodeWindow.Redo();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodeWindow.Cut();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodeWindow.Copy();
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodeWindow.Paste();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodeWindow.SelectedText = "";
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodeWindow.SelectAll();
        }

        private void StartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartToolStripButton_Click(sender, e);
        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"Course Work.docx");
        }

        private void AboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"Help\О программе.html");

        }

        private void CreateToolStripButton_Click(object sender, EventArgs e)
        {
            CreateToolStripMenuItem_Click(sender, e);
        }

        private void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            OpenToolStripMenuItem_Click(sender, e);
        }

        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveToolStripMenuItem_Click(sender, e);
        }

        private void UndoToolStripButton_Click(object sender, EventArgs e)
        {
            UndoToolStripMenuItem_Click(sender, e);
        }

        private void RedoToolStripButton_Click(object sender, EventArgs e)
        {
            RedoToolStripMenuItem_Click(sender, e);
        }

        private void CutToolStripButton_Click(object sender, EventArgs e)
        {
            CutToolStripMenuItem_Click(sender, e);
        }

        private void CopyToolStripButton_Click(object sender, EventArgs e)
        {
            CopyToolStripMenuItem_Click(sender, e);
        }

        private void PasteToolStripButton_Click(object sender, EventArgs e)
        {
            PasteToolStripMenuItem_Click(sender, e);
        }

        private void Code_Window(object sender, EventArgs e)
        {
            int fontSize = 20;
            CodeWindow.Font = new Font(CodeWindow.Font.FontFamily, (float)fontSize);

        }

        private void Results_Window(object sender, EventArgs e)
        {
            int fontSize = 20;
            ResultsWindow.Font = new Font(ResultsWindow.Font.FontFamily, (float)fontSize);
        }

        private void StartToolStripButton_Click(object sender, EventArgs e)
        {
            ResultsWindow.Clear();
            Scanner scanner = new Scanner(CodeWindow.Lines);
            Syntax syntax = new Syntax(scanner.ScannedText);
            foreach (var c in syntax.Errors)
            {
                ResultsWindow.AppendText(c.FormattedError() + "\n");
            }
            if (ResultsWindow.Text == "")
                ResultsWindow.Text = "Ошибки отсутствуют";
        }

        private void HelpToolStripButton_Click(object sender, EventArgs e)
        {
            HelpToolStripMenuItem_Click(sender, e);
        }

        private void постановкаЗадачиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"Text\Formulation Of The Task.html");
        }

        private void грамматикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"Text\Grammar.html");
        }

        private void классификацияГрамматикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"Text\Grammar Classification.html");
        }

        private void методАнализаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"Text\Analysis Method.html");
        }

        private void диагностикаИНейтрализацияОшибокToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void тестовыйПримерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"Text\Test Case.html");
        }

        private void списокЛитературыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"Text\Literature List.html");
        }

        private void исходныйКодПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"Text\Program Source Code.html");
        }
    }
}





