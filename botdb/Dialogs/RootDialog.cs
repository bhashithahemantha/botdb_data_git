using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Net.Http;
using System.Net.Http.Headers;
using botdb.Controllers;
using System.Collections.Generic;
using System.Text;

namespace botdb.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private  List<CompanyList> returnCompanyList;
        private string userName;
        private  int companyCount;
        private  string companyName;
        private  int detailsCount;
        private List<CompanyDetailsList> returnCompanyDetails;
        private string s1;
        private string s2;
        private string s3;
        private string s4;

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            if (activity.Text.ToLower().Contains("hi") || activity.Text.ToLower().Contains("hello"))
            {
                // return our reply to the user
                await context.PostAsync($"Please enter the Username:");
                context.Wait(getCompanyListAsync);
            }

        }

        private async Task getCompanyListAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            userName = activity.Text;

            CompanyListController obj = new CompanyListController();
            returnCompanyList = obj.getCompanies(userName);
            companyCount = obj.getItems();
            
            if (companyCount != 0)
            {
                await context.PostAsync($"" + companyCount + "");

                for (int i = 0; i < companyCount; ++i)
                {
                    s1 += returnCompanyList[i].COMPANYID.ToString() + ", ";
                }

                StringBuilder message = new StringBuilder();
                message.Append($"These are the Companies available for Username : " + userName + ". ");
                message.Append($"\n");
                message.Append(":- " + s1);
                message.Append($"\n");
                message.Append($" Please Enter One of them to Further Details...");

                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                Activity replyMessage = activity.CreateReply(message.ToString());
                await connector.Conversations.ReplyToActivityAsync(replyMessage);
                context.Wait(getCompanyDetailsAsync);
            }else
            {
                await context.PostAsync($"No Company details for you...!");
                context.Wait(MessageReceivedAsync);
            }

        }

        private async Task getCompanyDetailsAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            companyName = activity.Text;
            CompanyDetailsController obj = new CompanyDetailsController();
            returnCompanyDetails = obj.getCompanyDetails(companyName);
            detailsCount = obj.getItems();

            if (returnCompanyDetails[0].Total_x0020_CM.ToString() != "")
            {
                await context.PostAsync($"" + detailsCount + "");

                for (int i = 0; i < detailsCount; ++i)
                {
                    s2 += returnCompanyDetails[i].Total_x0020_CM.ToString();
                    s3 += returnCompanyDetails[i].Total_x0020_FOB.ToString();
                    s4 += returnCompanyDetails[i].OrderQty.ToString();

                }

                StringBuilder message = new StringBuilder();
                message.Append($"These are the Details for Company ID : " + companyName + "...");
                message.Append($"\n");
                message.Append("Total CM :- " + s2 + "...");
                message.Append($"\n");
                message.Append($"Total FOB :- " + s3 + "...");
                message.Append($"\n");
                message.Append($"Order Qty :- " + s4 + "...");

                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                Activity replyMessage = activity.CreateReply(message.ToString());
                await connector.Conversations.ReplyToActivityAsync(replyMessage);

                userName = "";
                companyName = "";
                s1 = "";
                s2 = "";
                s3 = "";
                s4 = "";

                context.Wait(MessageReceivedAsync);
            }
            else
            {
                await context.PostAsync($"No Company details for "+companyName+"...!");
                context.Wait(MessageReceivedAsync);
            }
            
        }

        //private static Attachment GetCompanyCard()
        //{


        //    var receiptCard = new ReceiptCard
        //    {

        //        Title = $"These are the Companies available for Username :" + userName,
        //        Facts = new List<Fact> {
        //            new Fact("Company ID", s1),
        //        }
        //    };

        //    return receiptCard.ToAttachment();
        //}





    }
}

