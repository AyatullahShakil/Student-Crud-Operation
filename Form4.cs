using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;


namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-IV4FO9J2\SQLEXPRESS;Initial Catalog=CRUD;User ID=sa;Password=******");
       public int ID;
        private void Form4_Load(object sender, EventArgs e)
        {
            GetStudentsRecord();
        }

        private void GetStudentsRecord()
        {
            
            SqlCommand cmd = new SqlCommand("Select * from Student", con);
            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dt;





        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Student VALUES (@Name, @Roll, @Address, @Mobile)",con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                cmd.Parameters.AddWithValue("@Roll", textBox3.Text);
                cmd.Parameters.AddWithValue("@Address", textBox2.Text);
                cmd.Parameters.AddWithValue("@Mobile", textBox4.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student Added");
                GetStudentsRecord();
                RestForm();

            }
        }

        private bool IsValid()
        {
            if(textBox3.Text == string.Empty)
            {
                MessageBox.Show("Student Name is Required");
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RestForm();
        }

        private void RestForm()
        {
            ID = 0;
            textBox3.Clear();
            textBox1.Clear();
            textBox4.Clear();
            textBox2.Clear();

            textBox3.Focus();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[4].Value);
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (ID > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE Student SET Name = @Name, Roll= @Roll,Address = @Address,Mobile = @Mobile WHERE ID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                cmd.Parameters.AddWithValue("@Roll", textBox3.Text);
                cmd.Parameters.AddWithValue("@Address", textBox2.Text);
                cmd.Parameters.AddWithValue("@Mobile", textBox4.Text);
                cmd.Parameters.AddWithValue("@ID", this.ID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Updated");
                GetStudentsRecord();
                RestForm();
            }
            else
            {
                MessageBox.Show("Please select a student");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ID > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Student WHERE ID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", this.ID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Deleted");
                GetStudentsRecord();
                RestForm();
            }
            else
            {
                MessageBox.Show("Please select a student for delete");
            }
        }
    }
}
