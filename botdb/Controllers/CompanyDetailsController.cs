using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace botdb.Controllers
{
    internal class CompanyDetailsController : ApiController
    {
        List<CompanyDetailsList> returnList = new List<CompanyDetailsList>();
        public List<CompanyDetailsList> getCompanyDetails(string companyName)
        {
            using (WebClient webClient = new WebClient())
            {
                WebClient n = new WebClient();
                var data = n.DownloadString(@"http://hgerpmisdashboard.azurewebsites.net/api/BOT/GetInvoiceDetails?UserId=%27Ravi%27&CompanyId="+companyName);

                DataSet ds = JsonConvert.DeserializeObject<DataSet>(data);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CompanyDetailsList item = new CompanyDetailsList();

                    item.Total_x0020_CM = dr["Total CM"].ToString();
                    item.Total_x0020_FOB = dr["Total FOB"].ToString();
                    item.OrderQty = dr["OrderQty"].ToString();

                    returnList.Add(item);
                }
                return returnList;
            }
        }

        public int getItems()
        {
            return returnList.Count();
        }
    }
}