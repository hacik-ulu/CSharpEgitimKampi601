﻿using CSharpEgitimKampi601.Entities;
using CSharpEgitimKampi601.Services;
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
    public partial class Form1 : Form
    {
        private readonly CustomerOperations _customerOperations;
        public Form1(CustomerOperations customerOperations)
        {
            _customerOperations = customerOperations;
            InitializeComponent();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addedCustomer = new Customer()
            {
                CustomerName = txtCustomerName.Text,
                CustomerSurname = txtCustomerSurname.Text,
                CustomerBalance = decimal.Parse(txtCustomerBalance.Text),
                CustomerCity = txtCustomerCity.Text,
                CustomerShoppingCount = int.Parse(txtCustomerShoppingAmount.Text)
            };
            _customerOperations.AddCustomer(addedCustomer);
            MessageBox.Show("Müşteri Ekleme İşlemi Başarılı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnCustomerList_Click(object sender, EventArgs e)
        {
            var customers = _customerOperations.GetAllCustomers().ToList();
            dataGridView1.DataSource = customers;
        }
        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            string deletedCustomerId = txtCustomerId.Text;
            _customerOperations.DeleteCustomer(deletedCustomerId);
            MessageBox.Show("Müşteri Silme İşlemi Başarılı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void btnCustomerUpdate_Click(object sender, EventArgs e)
        {
            var updatedCustomer = new Customer()
            {
                CustomerId = txtCustomerId.Text,
                CustomerName = txtCustomerName.Text,
                CustomerSurname = txtCustomerSurname.Text,
                CustomerBalance = decimal.Parse(txtCustomerBalance.Text),
                CustomerCity = txtCustomerCity.Text,
                CustomerShoppingCount = int.Parse(txtCustomerShoppingAmount.Text)
            };
            _customerOperations.UpdateCustomer(updatedCustomer);
        }
        private void btnGetCustomerById_Click(object sender, EventArgs e)
        {
            var customer = _customerOperations.GetCustomerById(txtCustomerId.Text);
            dataGridView1.DataSource = new List<Customer> { customer };
        }
    }
}
