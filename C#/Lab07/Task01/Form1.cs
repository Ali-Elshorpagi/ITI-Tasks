using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int range) || range < 2)
            {
                MessageBox.Show("Please enter a valid integer greater than 1.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            button1.Enabled = false;
            label1.Text = $"Result: Calculating...";
            Cursor = Cursors.WaitCursor;
            int countNumbers = await CountPrimeNumbers(range);
            Cursor = Cursors.Default;
            label1.Text = $"Result: {countNumbers}";
            button1.Enabled = true;
        }
        private async Task<int> CountPrimeNumbers(int range)
        {
            return await Task.Run(() =>
            {
                int cnt = 0;

                for (int i = 2; i <= range; ++i)
                {
                    bool isPrime = true;

                    for (int j = 2; j * j <= i; j++)
                    {
                        if (i % j == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }

                    if (isPrime)
                        ++cnt;
                }
                return cnt;
            });
        }
        private void label1_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label1_Click_1(object sender, EventArgs e) { }
    }
}
