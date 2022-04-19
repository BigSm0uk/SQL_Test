using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace TestSqlController
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlConnection nrtwnSqlConnection = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            nrtwnSqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["NorthWindDB"].ConnectionString); 
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString);
            sqlConnection.Open();
            nrtwnSqlConnection.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("insert INTO [Students] (Name, Last_name, Birthday, Phone, Email)" +
                $" VALUES (N'{NameBox.Text}', N'{Last_nameBox.Text}', '{BirthdayBox.Text}', '{PhoneBox.Text}' , '{EmailBox.Text}' )", sqlConnection) ;

            MessageBox.Show(command.ExecuteNonQuery().ToString());
        }

        private void SelectBut_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SelectBox.Text, nrtwnSqlConnection);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SqlReaderBut_Click(object sender, EventArgs e)
        {
            listView1.Clear();
            SqlDataReader dataReader = null;
            
            try
            {
                SqlCommand cmd = new SqlCommand("Select ProductName , QuantityPerUnit, UnitPrice from Products", nrtwnSqlConnection);
                dataReader = cmd.ExecuteReader();

                ListViewItem item = null;
                while (dataReader.Read())
                {
                    item = new ListViewItem(new string[] {Convert.ToString(dataReader["ProductName"]), Convert.ToString(dataReader["QuantityPerUnit"]) , Convert.ToString(dataReader["UnitPrice"]) });
                  
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (dataReader != null && !dataReader.IsClosed)
                    dataReader.Close();
            }
        }
    }
}
