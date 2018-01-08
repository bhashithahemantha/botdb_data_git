using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace botdb.Controllers
{
    internal class CompanyListController : ApiController
    {
        List<CompanyList> returnList = new List<CompanyList>();
        public List<CompanyList> getCompanies(string userName)
        {
            using (WebClient webClient = new WebClient())
            {
                WebClient n = new WebClient();
                var data = n.DownloadString(@"http://hgerpmisdashboard.azurewebsites.net/api/BOT/GetCpmanyId?UserId="+userName);
                
                DataSet ds = JsonConvert.DeserializeObject<DataSet>(data);
               
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CompanyList item = new CompanyList();

                    item.COMPANYID = dr["COMPANYID"].ToString();

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
