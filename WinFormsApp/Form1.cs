using System.Diagnostics;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private string[]? Files; // ת���ļ�·��
        private string? Directory; // ת���ļ���·��
        private string? Output; // ����ļ���·��
        private string? OriginalOutput;
        private bool SameOutputEverChecked;

        private static TranscodeService service = new TranscodeService();

        public Form1()
        {
            InitializeComponent();
        }

        public void ShowFiles(string[]? files)
        {
            if (files == null) // δѡȡ�ļ�ʱ������
            {
                Files = null;
                textBox_file.Clear();
                return;
            }

            textBox_file.Clear();
            var count = files.Length;

            // �������ı�������ʾѡȡ�������ļ����ļ���
            if (count == 1) // ��ֻ��һ���ļ�
            {
                textBox_file.Text = files[0]; // ֱ����ʾ�ļ���
            }

            else // ����ֹһ���ļ�
            {
                for (var i = 0; i < count; i++)
                {
                    textBox_file.Text += "\"" + files[i] + "\""; // ���ļ������߼��ϰ������
                    if (i >= count - 1)
                        continue;
                    textBox_file.Text += " "; // ʹ�ÿո�ָ����ļ���
                }
            }

            ShowDirectory(null); // ת���ļ���ת���ļ��в���ͬʱѡ��
        }

        public void ShowDirectory(string? directory)
        {
            if (directory == null) // δѡȡĿ¼ʱ��ֱ�ӽ���
            {
                Directory = null;
                textBox_directory.Clear();
                return;
            }

            textBox_directory.Clear();
            textBox_directory.Text = directory;

            ShowFiles(null); // ת���ļ���ת���ļ��в���ͬʱѡ��
        }

        public void ShowOutput(string? output)
        {
            if (output == null && !SameOutputEverChecked) // δѡȡ���Ŀ¼ʱ��ֱ�ӽ���
            {
                Output = null;
                textBox_output.Clear();
                return;
            }

            textBox_output.Clear();
            textBox_output.Text = output;
        }

        private void button_file_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true, // ����ѡ�����ļ�
                Title = "ѡ���ת���ļ�",
                Filter = "�����ļ�(*.*)|*.*"
            }; // ���ļ��Ի���
            Files = (fileDialog.ShowDialog() == DialogResult.OK) ? fileDialog.FileNames : null;
            ShowFiles(Files);
            if (checkBox_sameOutput.Checked && Files != null) // ѡ�����������ͬ�ļ���
            {
                Output = Path.GetDirectoryName(Files[0]);
                ShowOutput(Output);
            }
        }

        private void button_chooseDirectory_Click(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog
            {
                Description = "ѡ���ת���ļ���",
                ShowNewFolderButton = false // �������ڸöԻ������½��ļ���
            }; // ���ļ��жԻ���
            Directory = (folderDialog.ShowDialog() == DialogResult.OK) ? folderDialog.SelectedPath : null;
            ShowDirectory(Directory);
            if (checkBox_sameOutput.Checked) // ѡ�����������ͬ�ļ���
            {
                Output = Directory;
                ShowOutput(Output);
            }
        }

        private void button_chooseOutput_Click(object sender, EventArgs e)
        {
            OriginalOutput = Output;
            var folderDialog = new FolderBrowserDialog
            {
                Description = "ѡ������ļ���"
            }; // ���ļ��жԻ���
            Output = (folderDialog.ShowDialog() == DialogResult.OK) ? folderDialog.SelectedPath : null;
            ShowOutput(Output);
            if (OriginalOutput != Output) // ����������ļ���
            {
                checkBox_sameOutput.Checked = false;
            }
        }

        private void button_convert_Click(object sender, EventArgs e)
        {
            try
            {
                if (Files != null) // ��ѡȡת���ļ�
                {
                    // �����ǣ�Ҳ������ļ�����׺
                    if (!checkBox_override.Checked && !checkBox_fileSuffix.Checked)
                    {
                        string?[] inputs = Files.Select(file => Path.GetDirectoryName(file)).Distinct().ToArray();
                        if (inputs.Any() && inputs.Contains(Output))
                        {
                            if (MessageBox.Show("��ѡ�������ļ���������Ŀ¼��ͬ������ܵ���Դ�ļ������󸲸ǣ��Ƿ������", "Ŀ¼��ͬ����", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                            {
                                return;
                            }
                        }
                    }

                    service.DownLoadFiles(service.TranscodeFiles(service.UploadFiles(Files), checkBox_bom.Checked, checkBox_fileSuffix.Checked), Output);
                }
                else if (Directory != null) // ��ѡȡת���ļ���
                {
                    // �����ǣ�Ҳ������ļ�����׺
                    if (!checkBox_override.Checked && !checkBox_fileSuffix.Checked)
                    {
                        if (string.Equals(Directory, Output))
                        {
                            if (MessageBox.Show("��ѡ�������ļ���������Ŀ¼��ͬ������ܵ���Դ�ļ������󸲸ǣ��Ƿ������", "Ŀ¼��ͬ����", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                            {
                                return;
                            }
                        }
                    }

                    service.DownLoadFiles(service.TranscodeFiles(service.UploadFolder(Directory, checkBox_recur.Checked), checkBox_bom.Checked, checkBox_fileSuffix.Checked), Output);
                }
                else // ���߾�δѡ��
                {
                    throw new ArgumentNullException("ת���ļ���ת���ļ��о�δѡ��");
                }

                MessageBox.Show("ת���ɹ���");
                if (checkBox_openOutput.Checked && Output != null)
                {
                    Process.Start("Explorer.exe", Output);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_openDirectory_Click(object sender, EventArgs e)
        {
            if (Directory != null)
            {
                Process.Start("Explorer.exe", Directory);
            }
        }

        private void button_openOutput_Click(object sender, EventArgs e)
        {
            if (Output != null)
            {
                Process.Start("Explorer.exe", Output);
            }
        }

        // TODO: �޸��˸�ѡ�������ļ����ڶ�������¿��ܴ��ڵ���ʾ���������
        private void checkBox_sameOutput_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox_sameOutput.Checked) // ȡ����ѡ
            {
                checkBox_override.Checked = false;
            }

            if (Files != null || Directory != null)
            {
                if (checkBox_sameOutput.Checked)
                {
                    OriginalOutput = Output;
                    Output = (Files != null) ? Path.GetDirectoryName(Files[0]) : Directory;
                    ShowOutput(Output);

                    SameOutputEverChecked = true;
                }
                else
                {
                    Output = OriginalOutput;
                    ShowOutput(Output);
                }
            }
        }

        private void checkBox_override_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_override.Checked)
            {
                checkBox_sameOutput.Checked = true;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            service.ClearTempFiles();
        }
    }
}