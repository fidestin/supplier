using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Net.Mail;

namespace supplierEngine
{
   
    [System.Web.Script.Services.ScriptService]
    public class supplierFactory : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hi dudes";
        }


        [WebMethod]
        public List<supplierEngine.CampaignUtility.CampaignCustomer> LoadCampaignCustomers(int campaignID)
        {
            CampaignUtility campaignUtility = new CampaignUtility();
            return campaignUtility.loadCustomers(campaignID);
        }


        [WebMethod]
        public supplierEngine.supplierUtility.Supplier loadS(int supplierID)
        {
            supplierUtility.Supplier supplier = new supplierUtility.Supplier();
            supplierUtility supplierUtility = new supplierUtility();

            supplier = supplierUtility.loadSupplier(4);
            return supplier;
        }
        
        [WebMethod]
        public List<KeyValuePair<int,string>> loadMySupplierServices(int storeID)
        {
            List<KeyValuePair<int, string>> mySupplierServices = new List<KeyValuePair<int, string>>();
            serviceUtility servUtility = new serviceUtility();
            mySupplierServices=   servUtility.loadMyService(storeID);
            return mySupplierServices;
        }

        [WebMethod]
        public supplierEngine.serviceUtility.serviceAvailability LoadServiceAvailabilityForWeek(int relationshipID, int weekID)
        {
            serviceUtility.serviceAvailability servAvailable = new serviceUtility.serviceAvailability();
            serviceUtility servUtility = new serviceUtility();

            servAvailable = servUtility.loadAvailability(relationshipID, weekID);
            return servAvailable;
        }

        [WebMethod]
        public void UpdateOrInsertAvailability(int servID, int mon, int tue, int wed, int thurs, int fri, int sat, int sun, int storeSupplierID, int weekID)
        {
            serviceUtility servUtility = new serviceUtility();
            servUtility.UpdateOrInsertAvailability(servID, mon, tue, wed, thurs, fri, sat, sun, storeSupplierID, weekID);
        }


        [WebMethod]
        public void SaveCampaignCustomers(int campaignID, int[] customers)
        {
            CampaignUtility campaignUtility = new CampaignUtility();
            campaignUtility.SaveCampaignCustomer(campaignID, customers);
        }

        [WebMethod]
        public void SaveCampaignEmails(int campaignID, string[] customerEmails)
        {
            CampaignUtility campaignUtility = new CampaignUtility();
            campaignUtility.SaveCampaignEmails(campaignID, customerEmails);
        }

        [WebMethod]
        public void SendCampaignEmails(int campaignID, string[] customerEmails)
        {
            foreach (string custEmail in customerEmails)
            {
                //Need to generate the email body HTML here and pass it in ...
                SendGridSMTPEmail(custEmail, "TEST", "<a href='http://www.rte.ie/'>RTE Link</a><BR><a href='https://twitter.com/BadAssCafe1'>BadAss Café</a><BR>HERE");

            }
        }

        //[WebMethod]
        public void SendGridSMTPEmail(string ToAddress,string SubjectTitle, string BodyContent)
        {
            MailMessage mailMsg = new MailMessage();
            mailMsg.To.Add(ToAddress);

            MailAddress mailAddress = new MailAddress("mailer@handygrub.com");
            mailMsg.From = mailAddress;

            mailMsg.Subject = "SendGrid" + SubjectTitle;
            string tableHTML = "<table border='0'><tr><td>" + BodyContent + "</td></tr></table>";
            mailMsg.Body = tableHTML;
            mailMsg.IsBodyHtml = true;

            SmtpClient smtpclient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("fidestin", "gAlway_1");
            smtpclient.Credentials = credentials;
            smtpclient.Send(mailMsg);
        }
    }
}