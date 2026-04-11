namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        HttpClient client;
        public Form1()
        {
            InitializeComponent();
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5083/api/");
        }
        private void dataGridView_courses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            HttpResponseMessage response = client.GetAsync("Course").Result;
            if (response.IsSuccessStatusCode)
            {
                List<CourseMap> crsList = response.Content.ReadAsAsync<List<CourseMap>>().Result;
                dataGridView_courses.DataSource = crsList;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxDura.Text, out int duration))
            {
                MessageBox.Show("Duration must be a valid number.");
                return;
            }

            CourseMap newCrs = new CourseMap()
            {
                id = Guid.NewGuid().ToString(),
                name = textBoxName.Text,
                description = textBoxDesc.Text,
                duration = duration
            };

            HttpResponseMessage response = client.PostAsJsonAsync("Course", newCrs).Result;
            if (response.IsSuccessStatusCode)
            {
                Form1_Load(null, null);
                textBoxName.Text = textBoxDesc.Text = textBoxDura.Text = "";
                MessageBox.Show("Addes Successfully");
            }
            else
            {
                var error = response.Content.ReadAsStringAsync().Result;
                MessageBox.Show($"Failed to add course. Status: {(int)response.StatusCode} {response.ReasonPhrase}\n{error}");
            }
        }
    }
}
