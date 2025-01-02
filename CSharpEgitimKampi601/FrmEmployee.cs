using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi601
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }

        string connectionString = "server=localhost;port=5432;Database=CustomerDb; userId=postgres;password=password";

        void EmployeeList()
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter("SELECT * FROM Employees", connection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        void DepartmentList()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT departmentid, departmentname FROM Departments";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    var adapter = new NpgsqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    cmbDepartment.DataSource = dataTable;
                    cmbDepartment.DisplayMember = "departmentname";
                    cmbDepartment.ValueMember = "departmentid";
                }
            }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            EmployeeList();
        }

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            EmployeeList();
            DepartmentList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string employeeName = txtPersonelName.Text;
            string employeeSurname = txtPersonelName.Text;
            decimal employeeSalary = decimal.Parse(txtEmployeeSalary.Text);
            int departmentId = int.Parse(cmbDepartment.SelectedValue.ToString());

            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "INSERT INTO Employees (employeename, employeesurname, employeesalary, departmentid) VALUES (@p1, @p2, @p3, @p4)";
            using (var command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@p1", employeeName);
                command.Parameters.AddWithValue("@p2", employeeSurname);
                command.Parameters.AddWithValue("@p3", employeeSalary);
                command.Parameters.AddWithValue("@p4", departmentId);
                command.ExecuteNonQuery();
            }
            MessageBox.Show("Müşteri ekleme işlemi Başarılı!");
            connection.Close();
            EmployeeList();
        }
    }
}
