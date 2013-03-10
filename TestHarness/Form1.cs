using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using supplierEngine;

using TestHarness.com.fidestin.supplier;




namespace TestHarness
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            supplierFactory WSsupplierFactory = new supplierFactory();
            int[] customerarray={388,389,399,401};

            //Save via the webservice...
            WSsupplierFactory.SaveCampaignCustomers(29, customerarray);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            supplierFactory WSsupplierFactory = new supplierFactory();

            //Save via the web service 0=INSERT;
            WSsupplierFactory.UpdateOrInsertAvailability(3, 9, 9, 5, 3, 6, 7, 8, 1, 14);

        }
    }
}
