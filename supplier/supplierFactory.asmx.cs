using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;

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

    }
}