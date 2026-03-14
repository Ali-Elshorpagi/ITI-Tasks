using BLL;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private DepartmentList _departments;

        public Form1()
        {
            InitializeComponent();

            _departments = DepartmentManager.GetAllDepartments();

            comboBox1.DisplayMember = nameof(Department.Name);
            comboBox1.ValueMember = nameof(Department.Id);
            comboBox1.DataSource = _departments;

            comboBox1.SelectedIndex = -1;
            dataGridView1.DataSource = null;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                dataGridView1.DataSource = null;
                return;
            }

            if (comboBox1.SelectedValue != null &&
                int.TryParse(comboBox1.SelectedValue.ToString(), out int selectedId))
            {
                var filtered = _departments
                    .Where(d => d.Id == selectedId)
                    .ToList();

                dataGridView1.DataSource = filtered;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
