using System;
using System.Collections.Generic;

namespace botdb.Controllers
{
    [Serializable]

    public class OrderBookDetails
    {
        public string COMPANYID { get; set; }
        public string SALESID { get; set; }
        public string SEASONID { get; set; }
        public string CUSTOMERID { get; set; }
        public string DIVISIONID { get; set; }
        public string STYLENUMBER { get; set; }
        public string DESIGNNUMBER { get; set; }
        public string MERCHNAME { get; set; }
        public string SALESORDERCURRCODE { get; set; }
        public DateTime CREATEDDATETIME { get; set; }
        public string COMPANYID1 { get; set; }
        public string SALESID1 { get; set; }
        public string SITEID { get; set; }
        public string GARMENTITEMTYPEID { get; set; }
        public string FGITEMID { get; set; }
        public string FGITEMNAME { get; set; }
        public double ORDERQTY { get; set; }
        public double CMPERPIECESIGNOFF { get; set; }
        public double FOBPERPIECESIGNOFF { get; set; }
        public double SMVPERPIECESIGNOFF { get; set; }
        public DateTime FIRSTORDERCONFDATE { get; set; }
        public DateTime PCDDATE { get; set; }
        public string METHODOFPAYEMNT { get; set; }
        public string PAYMENTTERM { get; set; }
        public DateTime CREATEDDATETIME1 { get; set; }
        public DateTime TRANSFERDATETIME { get; set; }
        public string CUSTOMERREF { get; set; }
        public DateTime DELIVERYDATE { get; set; }
        public string DELIVERYCOUNTRYREGIONID { get; set; }
        public string CUSTREFDESCRIPTION { get; set; }
    }

    public class RootObject
    {
        public List<OrderBookDetails> OrderBookDetails { get; set; }
    }
}