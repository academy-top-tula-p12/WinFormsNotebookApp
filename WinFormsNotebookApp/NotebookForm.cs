namespace WinFormsNotebookApp
{
    public partial class NotebookForm : Form
    {
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

            //MenuStrip menuMain = new();
            //menuMain.Dock = DockStyle.Top;

            //ToolStripMenuItem fileItem = new("Файл");
            //menuMain.Items.Add(fileItem);

            //menuMain.SuspendLayout();
            //this.MainMenuStrip = menuMain;

            //ToolStripMenuItem fileOpenMenuItem = new("Открыть");
            //fileMenuItem.DropDownItems.Add(fileOpenMenuItem);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnFont_Click(object sender, EventArgs e)
        {

        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            
        }

        private void fileCreateMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fileOpenMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            var fileName = openFileDialog.FileName;
            var text = File.ReadAllText(fileName);
            txtEdit.Text = text;
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
            txtEdit.Font = fontDialog.Font;
            txtEdit.ForeColor = fontDialog.Color;
        }

        private void formatColorMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.Cancel)
                return;

            txtEdit.BackColor = colorDialog.Color;
        }
    }
}
