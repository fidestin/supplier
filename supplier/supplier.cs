using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Runtime.Serialization;
using System.Net.Mail;

namespace supplierEngine
{
    public static class SQLHelp
    {
        public static string RunQuery(string sqlQuery)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Rico"].ConnectionString);
                string strAnswer = null;
                myConnection.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(sqlQuery, myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    strAnswer = myReader[0].ToString();
                }
                myConnection.Close();
                return strAnswer;
            }
            catch (Exception ex)
            {
                // SendErrorEmail("brendan.mcardle@gmail.com", "FIDESTIN Error : RunQuery \n" + sqlQuery + "\n" + ex.Message, "ERROR");
                //return "Error in Runquery";
                throw;
            }
        }
    }
    

    public class supplierUtility
    {
        [DataContract]
        public class Supplier
        {
            public string ID;
            public string destination;
            public string fbLink;
            public string fbImage;
            public string twitterLink;
            public string twitterImage;
            public string youTubeLink;
            public string youTubeImg;
            public string mapLink;
            public string mapAPICall;
            public string redeemText;
            public string tandc;
            public string qrImg;
            public string offerText;
            public string destLink;

        }


                /// <summary>
        /// Returns a supplier object populated...
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public Supplier loadSupplier(int supplierID)
        {
            SqlConnection myConnection = null;
            Supplier supplier = new Supplier();

            //Get the supplier data first
            myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Rico"].ConnectionString);
            string sqlQuery = " select * from supplier where ID=" + supplierID.ToString();

            SqlCommand myCommand = new SqlCommand(sqlQuery, myConnection);
            myConnection.Open();
            myCommand.Connection = myConnection;
            SqlDataReader myReader = null;
            myReader = myCommand.ExecuteReader();
            string storeLatX=null;
            string storeLatY=null;
            int voucherID=0;

            while (myReader.Read())
            {
                supplier.ID = myReader["ID"].ToString();
                supplier.destination = myReader["destination"].ToString();
                supplier.fbLink = myReader["fbLink"].ToString();
                supplier.fbImage = myReader["fbImage"].ToString();
                supplier.twitterLink = myReader["twitterLink"].ToString();
                supplier.twitterImage = myReader["twitterImage"].ToString();
                supplier.youTubeLink = myReader["youTubeLink"].ToString();
                supplier.youTubeImg = myReader["youTubeImg"].ToString();
                supplier.mapLink = myReader["mapLink"].ToString();

                //Allow the stores location to be added now to the mapLink - different stores for the same supplier...
                supplier.mapLink = supplier.mapLink.Replace("storeLatX", storeLatX);
                supplier.mapLink = supplier.mapLink.Replace("storeLatY", storeLatY);

                supplier.mapAPICall = myReader["mapAPICall"].ToString();
                supplier.tandc = myReader["tandc"].ToString();
                supplier.redeemText = myReader["redeemText"].ToString() + "-" + voucherID.ToString();
                supplier.qrImg = myReader["qrImg"].ToString();
                supplier.offerText = myReader["offerText"].ToString();
                supplier.destLink = myReader["destLink"].ToString();
            }

            myConnection.Close();

            return supplier;

        }
    }

    public class serviceUtility
    {
        public class serviceAvailability
        {
            public int id { get; set; }
            public int mon { get; set; }
            public int tue { get; set; }
            public int wed { get; set; }
            public int thurs { get; set; }
            public int fri { get; set; }
            public int sat { get; set; }
            public int sun { get; set; }
            public int storeSupplierID { get; set; }
            public int weekID { get; set; }
        }

       


        public void UpdateOrInsertAvailability(int servID, int mon, int tue, int wed, int thurs, int fri, int sat, int sun, int storeSupplierID, int weekID)
        {
            string sqlQuery="";
            if (servID==0)
            {
                //INSERT
                sqlQuery=" insert into supplierAvailability values (" + mon.ToString() + "," + tue.ToString() + "," + wed.ToString() + "," + thurs.ToString() + "," + fri.ToString() + "," + sat.ToString() + "," + sun.ToString() + "," + storeSupplierID.ToString() + "," + weekID.ToString() + ")";
            }
            else
            {
                //UPDATE
                sqlQuery= " Update supplierAvailability set mon=" + mon.ToString() + ",tue="+tue.ToString() + ",wed=" + wed.ToString() + ",thurs="+thurs.ToString()+",fri="+fri.ToString() + ",sat="+sat.ToString()+",sun="+sun.ToString()+",storeSupplierID="+storeSupplierID.ToString() +",weekID="+weekID.ToString()+" where id="+servID.ToString();
            }

            SQLHelp.RunQuery(sqlQuery);
        }



        public serviceAvailability loadAvailability(int storeSupplierID)
        {
            SqlConnection myConnection = null;
            serviceAvailability supplierAvailability = new serviceAvailability();

            //Get the supplier data first
            myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Rico"].ConnectionString);
            string sqlQuery = " select * from supplierAvailability where ID=" + storeSupplierID.ToString();

            SqlCommand myCommand = new SqlCommand(sqlQuery, myConnection);
            myConnection.Open();
            myCommand.Connection = myConnection;
            SqlDataReader myReader = null;
            myReader = myCommand.ExecuteReader();
            string storeLatX = null;
            string storeLatY = null;
            int voucherID = 0;

            while (myReader.Read())
            {
                supplierAvailability.id = Convert.ToInt32(myReader["ID"].ToString());
                supplierAvailability.mon = Convert.ToInt32(myReader["mon"].ToString());
                supplierAvailability.tue = Convert.ToInt32(myReader["tue"].ToString());
                supplierAvailability.wed = Convert.ToInt32(myReader["wed"].ToString());
                supplierAvailability.thurs = Convert.ToInt32(myReader["thurs"].ToString());
                supplierAvailability.fri = Convert.ToInt32(myReader["fri"].ToString());
                supplierAvailability.sat = Convert.ToInt32(myReader["sat"].ToString());
                supplierAvailability.sun = Convert.ToInt32(myReader["sun"].ToString());
                supplierAvailability.weekID = Convert.ToInt32(myReader["weekID"].ToString());
            }

            myConnection.Close();
            return supplierAvailability;

        }
    }

    public class CampaignUtility
    {
        //define the campaignCustomer
        public class CampaignCustomer
        {
            public int campaignID { get; set; }
            public int customerID { get; set; }
            public string customerEmail { get; set; }

        }

        //loads the customers for a particular campaign
        public List<CampaignCustomer> loadCustomers(int campaignID)
        {

            List<CampaignCustomer> recipients = new List<CampaignCustomer>();

            SqlConnection myConnection = null;
            CampaignCustomer campaignCustomer = new CampaignCustomer();

            //Get the supplier data first
            myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Rico"].ConnectionString);
            string sqlQuery = " select CA.*,CU.EMail from CampaignCustomers CA  inner join Customer CU on CA.customerID=CU.id where campaignID=" + campaignID.ToString();

            SqlCommand myCommand = new SqlCommand(sqlQuery, myConnection);
            myConnection.Open();
            myCommand.Connection = myConnection;
            SqlDataReader myReader = null;
            myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
                campaignCustomer = new CampaignCustomer();
                campaignCustomer.campaignID = campaignID;
                campaignCustomer.customerID = Convert.ToInt16(myReader["customerID"].ToString());
                campaignCustomer.customerEmail = myReader["EMail"].ToString();
                recipients.Add(campaignCustomer);
            }

            myConnection.Close();

            return recipients;
        }

        public void SendErrorEmail(string To, string Body, string subject)
        {
            try
            {
                string tableHTML = "<table border='0'><tr><td>" + Body + "</td></tr></table>";
                MailMessage msg = new MailMessage("mailer@handygrub.com", To, subject, tableHTML);
                SmtpClient client = new SmtpClient("localhost");            ///this refers to smtp.handygrub.com according to the forum.
                string[] arrEmails;
                char[] cDelimiter = { ';' };

                arrEmails = To.Split(cDelimiter);

                if (arrEmails.Length > 0)
                {
                    msg.To.Clear();
                    for (int idx = 0; idx < arrEmails.Length; idx++)
                    {
                        msg.To.Add(arrEmails[idx].ToString());
                    }
                }
                msg.IsBodyHtml = true;
                client.Send(msg);
            }
            catch (Exception ex)
            {
                SendErrorEmail("brendan.mcardle@gmail.com", "Error \nSendEmail\n" + ex.Message, "ERROR");
                // throw ex;
            }
        }

        public void SaveCampaignEmails(int campaignID, string[] customerEmails)
        {
            string sqlQuery=string.Empty;

            try
            {
                //first of all clear out the existing customers for THIS campaign before we insert the new list
                sqlQuery = " delete from campaigncustomers where campaignID=" + campaignID.ToString();
                SQLHelp.RunQuery(sqlQuery);

                foreach (string customerEmail in customerEmails)
                {
                    sqlQuery = " exec AddEmailToCampaign '" + customerEmail + "',"  +campaignID.ToString();
                    //Call proc to add this customer if required
                    //and add it to the campaign...
                    SQLHelp.RunQuery(sqlQuery);
                }
            }
            catch (Exception ex)
            {
                SendErrorEmail("brendan.mcardle@gmail.com", "Error in SaveCampaignEmails\n " + sqlQuery, "Error Supplier Factory Web Service");

                throw ex;
            }

        }

        //save the customers for a particular campaign
        public void SaveCampaignCustomer(int campaignID, int[] customers)
        {
            //first of all clear out the existing customers before we insert the new list
            string sqlQuery = " delete from campaigncustomers where campaignID=" + campaignID.ToString();
            SQLHelp.RunQuery(sqlQuery);

            //Now insert for each of the customers
            foreach (int campaignCustomerID in customers)
            {
               // int customerID =campaignCustomer.customerID;
                sqlQuery = " insert into campaigncustomers values (" + campaignID + "," + campaignCustomerID + ")";
                SQLHelp.RunQuery(sqlQuery);
            }
        }
    }
}