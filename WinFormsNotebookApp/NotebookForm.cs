namespace WinFormsNotebookApp
{
    public partial class NotebookForm : Form
    {
        static int countDocs = 0;
        List<TabDocument> tabDocs = new();

        public NotebookForm()
        {
            InitializeComponent();

            var filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            openFileDialog.Filter = filter;
            saveFileDialog.Filter = filter;

            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.AddExtension = true;
            saveFileDialog.InitialDirectory = @"D:/RPO";

            saveFileDialog.CreatePrompt = true;
            saveFileDialog.OverwritePrompt = false;

            fontDialog.ShowColor = true;

            colorDialog.FullOpen = true;
            colorDialog.SolidColorOnly = true;

            CreateTabDocumnet();
        }

        private void fileCreateMenuItem_Click(object sender, EventArgs e)
        {
            TabDocument tabDoc = CreateTabDocumnet();

        }

        private void fileOpenMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            var fileName = openFileDialog.FileName;

            TabDocument tabDoc = CreateTabDocumnet(fileName.Remove(0, fileName.LastIndexOf("\\") + 1));
            tabDoc.FileName = fileName;

            var text = File.ReadAllText(fileName);
            tabDoc.EditBox.Text = text;
        }

        private void fileSaveMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            string fileName = saveFileDialog.FileName;
            File.WriteAllText(fileName, txtEdit.Text);
        }

        private void formatFontMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.Cancel)
                return;

            TabDocument? docCurrent = CurrentDocument();

            docCurrent.EditBox.Font = fontDialog.Font;
            docCurrent.EditBox.ForeColor = fontDialog.Color;
        }

        private void formatColorMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.Cancel)
                return;

            TabDocument? docCurrent = CurrentDocument();

            docCurrent.EditBox.BackColor = colorDialog.Color;
        }

        TabDocument CreateTabDocumnet(string title = "")
        {
            title = (title == "") ? $"Новый документ {++countDocs}" : title;

            TabPage tabPage = new TabPage(title);
            editTabControl.Controls.Add(tabPage);
            editTabControl.SelectedTab = tabPage;

            TextBox editBox = new();
            editBox.Multiline = true;
            editBox.Dock = DockStyle.Fill;
            editBox.TextChanged += editBox_TextChanged;

            tabPage.Controls.Add(editBox);

            TabDocument tabDoc = new(
                tabPage,
                editBox
                );

            tabDocs.Add(tabDoc);

            return tabDoc;
        }

        private void editBox_TextChanged(object? sender, EventArgs e)
        {
            TabDocument docCurrent = CurrentDocument();
            docCurrent.IsSave = false;
        }

        private void fileCloseMenuItem_Click(object sender, EventArgs e)
        {
            TabDocument? docCurrent = CurrentDocument();

            if(!docCurrent.IsSave)
            {
                var result = MessageBox.Show
                    ("Файл не сохранен. Сохранить?",
                    "Файл не сохранен.",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if ( result == DialogResult.Cancel)
                    return;
                else if ( result == DialogResult.Yes )
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                        return;

                    string fileName = saveFileDialog.FileName;
                    File.WriteAllText(fileName, docCurrent.EditBox.Text);
                }
            }

            tabDocs.Remove(docCurrent);
            editTabControl.Controls.Remove(docCurrent.Page);
        }

        TabDocument CurrentDocument()
        {
            TabPage? pageCurrent = editTabControl.SelectedTab;
            TabDocument? docCurrent = tabDocs.FirstOrDefault(d => d.Page == pageCurrent);
            return docCurrent;
        }
    }
}
