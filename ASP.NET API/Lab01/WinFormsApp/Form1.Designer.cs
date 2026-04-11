namespace WinFormsApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView_courses = new DataGridView();
            textBoxName = new TextBox();
            textBoxDesc = new TextBox();
            textBoxDura = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView_courses).BeginInit();
            SuspendLayout();
            // 
            // dataGridView_courses
            // 
            dataGridView_courses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_courses.Location = new Point(26, 196);
            dataGridView_courses.Name = "dataGridView_courses";
            dataGridView_courses.Size = new Size(445, 242);
            dataGridView_courses.TabIndex = 0;
            dataGridView_courses.CellContentClick += dataGridView_courses_CellContentClick;
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(593, 40);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(139, 23);
            textBoxName.TabIndex = 1;
            // 
            // textBoxDesc
            // 
            textBoxDesc.Location = new Point(593, 83);
            textBoxDesc.Name = "textBoxDesc";
            textBoxDesc.Size = new Size(139, 23);
            textBoxDesc.TabIndex = 2;
            // 
            // textBoxDura
            // 
            textBoxDura.Location = new Point(593, 130);
            textBoxDura.Name = "textBoxDura";
            textBoxDura.Size = new Size(139, 23);
            textBoxDura.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(520, 48);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 5;
            label1.Text = "Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(520, 91);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 6;
            label2.Text = "Description";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(520, 138);
            label3.Name = "label3";
            label3.Size = new Size(53, 15);
            label3.TabIndex = 7;
            label3.Text = "Duration";
            // 
            // button1
            // 
            button1.Location = new Point(625, 172);
            button1.Name = "button1";
            button1.Size = new Size(84, 23);
            button1.TabIndex = 8;
            button1.Text = "Add";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBoxDura);
            Controls.Add(textBoxDesc);
            Controls.Add(textBoxName);
            Controls.Add(dataGridView_courses);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView_courses).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView_courses;
        private TextBox textBoxName;
        private TextBox textBoxDesc;
        private TextBox textBoxDura;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button button1;
    }
}
