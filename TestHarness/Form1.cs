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

        private void button3_Click(object sender, EventArgs e)
        {
            supplierFactory WSsupplierFactory = new supplierFactory();
            
            //Save via the web service 0=INSERT;
            Supplier thisSupplier= WSsupplierFactory.loadS(4);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            supplierFactory WSsupplierFactory = new supplierFactory();

            serviceAvailability restoAvail = WSsupplierFactory.loadSAvail(3);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            supplierFactory WSsupplierFactory = new supplierFactory();

            //Save this relationship availability to table..0=INSERT
           WSsupplierFactory.UpdateOrInsertAvailability(0, 2, 2, 2, 2, 2, 2, 2, 4, 14);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            supplierFactory WSsupplierFactory = new supplierFactory();

            string[] custEmails=new string[]{"brendan.mcardle@gmail.com","brendanjmcardle@gmail.com","somecrap@gmail.com"};


            var a = new ArrayOfString { "test" };

            //Save this relationship availability to table..0=INSERT
            WSsupplierFactory.SaveCampaignEmails(34, custEmails);
        }
    }
}
