using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace WindowsFormsApp15
{
    public partial class Form1 : Form
    {
        static string path = @"Data Source=KOMPUTER\SQLEXPRESS;
                               Initial Catalog = mur;
                               Integrated Security=SSPI;";

        public Form1()
        {
            InitializeComponent(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                using (var connect = new SqlConnection(path))
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand(textBox1.Text, connect);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        bool IsHead = true;
                        while (reader.Read())
                        {

                            if (IsHead)
                            {

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    dt.Columns.Add(reader.GetName(i));
                                }
                                IsHead = false;
                            }
                            DataRow dataRow = dt.NewRow();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                dataRow[i] = reader[i];
                            }
                            dt.Rows.Add(dataRow);
                        }
                    }
                }
                dataGridView1.DataSource = dt;
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        SqlDataAdapter adapter;
        DataSet dataSet;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(path);
                adapter = new SqlDataAdapter(textBox1.Text, sqlConnection);

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);  
                dataSet = new DataSet();
                adapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];
            }
            catch(SqlException ex)
            { 
                MessageBox.Show(ex.Message);    
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            adapter.Update(dataSet);
        }
    }
}
