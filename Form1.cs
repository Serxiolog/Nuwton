using System.Windows.Forms;

namespace Nuwton
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool dataLoaded = false;
        private List<double> x1;
        private List<double> x2;
        private double x1L, x1R, x2L, x2R;

        private void LoadDataButton_Click(object sender, EventArgs e)
        {
            if (dataLoaded)
            {
                MessageBox.Show("Clear previous data");
                return;
            }
            using (var openfile = new OpenFileDialog())
            {
                openfile.Filter = "Text files (*.txt)|*.txt";
                if (openfile.ShowDialog() == DialogResult.OK)
                {
                    var filePath = openfile.FileName;
                    try
                    {
                        var lines = File.ReadAllLines(filePath);
                        x1 = lines[0].Split(", ").Select(x => double.Parse(x.Trim())).ToList();
                        x2 = lines[1].Split(", ").Select(x => double.Parse(x.Trim())).ToList();
                        x1L = double.Parse(lines[2].Split(", ")[0].Trim());
                        x1R = double.Parse(lines[2].Split(", ")[1].Trim());
                        x2L = double.Parse(lines[3].Split(", ")[0].Trim());
                        x2R = double.Parse(lines[3].Split(", ")[1].Trim());
                        dataLoaded = true;
                    }
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
        }

        private void SolveButton_Click(object sender, EventArgs e)
        {
            if (dataLoaded)
            {
                FletcherReeves fr = new();
                (List<double> x, List<string> log) = fr.LoadData(x1, x2, x1L, x1R, x2L, x2R);
                foreach (var l in log)
                {
                    listBox1.Items.Add(l);
                }
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            dataLoaded = false;
            x1.Clear();
            x2.Clear();
            listBox1.Items.Clear();
        }
    }
}
