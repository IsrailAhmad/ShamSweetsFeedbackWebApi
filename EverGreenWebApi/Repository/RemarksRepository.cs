﻿using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public class RemarksRepository : IRemarksRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public RemarksModel AddRemarks(int customerid, int storeid, string remarks)
        {
            RemarksModel data = new RemarksModel();
            using (shamsweetsfeedback_androidEntities context = new shamsweetsfeedback_androidEntities())
            {
                remarksmaster u = new remarksmaster();
                u.CustomerId = customerid;
                u.StoreId = storeid;
                u.Remarks = remarks;
                var cust = context.customermasters.Where(x => x.CustomerId == customerid).FirstOrDefault();
                if (cust.CustomerId > 0)
                {
                    context.remarksmasters.Add(u);
                    var result = context.SaveChanges();
                    if (result > 0)
                    {
                        data.RemarksId = u.RemarksId;
                        SendSmsOnBad(customerid);
                    }
                }
            }
            return data;
        }

        public void SendSmsOnBad(int customerid)
        {
            using (shamsweetsfeedback_androidEntities context = new shamsweetsfeedback_androidEntities())
            {
                var customer = (from c in context.customermasters
                                where c.CustomerId == customerid
                                select new CustomerModel()
                                {
                                    CustomerId = c.CustomerId,
                                    CustomerName = c.CustomerName,
                                    PhoneNumber = c.PhoneNumber,

                                }).FirstOrDefault();
                var badfeedback = (from c in context.customermasters
                                   join f in context.feedbackmasters on c.CustomerId equals f.CustomerId into j1
                                   from j2 in j1.DefaultIfEmpty()
                                   join q in context.questionmasters on j2.QuestionId equals q.QuestionId into j3
                                   from j4 in j3.DefaultIfEmpty()
                                   join a in context.answermasters on j2.AnswerId equals a.AnswerId into j5
                                   from j6 in j5.DefaultIfEmpty()
                                   where c.CustomerId == customerid && j6.AnswerId == 1
                                   select new CustomerFeedbackMasterModel()
                                   {
                                       QuestionId = (int)j2.QuestionId,
                                       QuestionName = j4.Question,
                                       AnswerId = (int)j2.AnswerId,
                                       AnswerName = j6.Answer
                                   }).ToList();

                var feedback = (from c in context.customermasters
                                join f in context.feedbackmasters on c.CustomerId equals f.CustomerId into j1
                                from j2 in j1.DefaultIfEmpty()
                                join q in context.questionmasters on j2.QuestionId equals q.QuestionId into j3
                                from j4 in j3.DefaultIfEmpty()
                                join a in context.answermasters on j2.AnswerId equals a.AnswerId into j5
                                from j6 in j5.DefaultIfEmpty()
                                where c.CustomerId == customerid && (j6.AnswerId == 2 || j6.AnswerId == 3 || j6.AnswerId == 4)
                                select new CustomerFeedbackMasterModel()
                                {
                                    QuestionId = (int)j2.QuestionId,
                                    QuestionName = j4.Question,
                                    AnswerId = (int)j2.AnswerId,
                                    AnswerName = j6.Answer
                                }).ToList();

                if (customer.PhoneNumber != null)
                {
                    if (badfeedback.Count() > 0)
                    {
                        SendSMS(customer, badfeedback);
                    }
                    if (feedback.Count() > 0)
                    {
                        //SendsmsToCustomer(customer);
                    }
                }
            }
        }
        public void SendSMS(CustomerModel customer, List<CustomerFeedbackMasterModel> badfeedback)
        {
            string receiverno = "";
            if (badfeedback.Count() > 0)
            {
                using (shamsweetsfeedback_androidEntities context = new shamsweetsfeedback_androidEntities())
                {
                    var smsreceiverlist = context.badfeedbacksmsmasters.OrderBy(x => x.MobileNo).Where(x => x.IsActive == "Y").ToList();
                    foreach (var i in smsreceiverlist)
                    {
                        receiverno += i.MobileNo + ",";
                    }
                }
                receiverno = receiverno.Trim(',');
                //string receiverno = "8448172988,9811616927,9555740041";
                string question = "";
                int intno = 0;
                foreach (var ques in badfeedback)
                {
                    question += "\n" + ++intno + ". " + ques.QuestionName;
                }
                string bodymessage = "Customer Name:" + customer.CustomerName +
                    "\nCustomer PhoneNo: " + customer.PhoneNumber +
                    "\nBad Feedback Questions are: " + question;
                string _user = HttpUtility.UrlEncode("shamsweet"); // API user name to send SMS
                string _pass = HttpUtility.UrlEncode("12345");     // API password to send SMS
                string _route = HttpUtility.UrlEncode("transactional");
                string _senderid = HttpUtility.UrlEncode("WISHHH");
                string _recipient = HttpUtility.UrlEncode(receiverno);  // who will receive message
                string _messageText = HttpUtility.UrlEncode(bodymessage); // text message

                // Creating URL to send sms
                string _createURL = "http://www.smsnmedia.com/api/push?user=" + _user + "&pwd=" + _pass + "&route=" + _route + "&sender=" + _senderid + "&mobileno=91" + _recipient + "&text=" + _messageText;

                HttpWebRequest _createRequest = (HttpWebRequest)WebRequest.Create(_createURL);
                // getting response of sms
                HttpWebResponse myResp = (HttpWebResponse)_createRequest.GetResponse();
                StreamReader _responseStreamReader = new StreamReader(myResp.GetResponseStream());
                string responseString = _responseStreamReader.ReadToEnd();
                _responseStreamReader.Close();
                myResp.Close();
            }
        }

        public void SendsmsToCustomer(CustomerModel customer)
        {
            //if (customer.PhoneNumber == "9555740041")
            //{
            try
            {
                string link = "http://bit.ly/2AKJSwB";
                string messagebody = "Dear " + customer.CustomerName +
                                     "\nThanks for feedback.\nClick on link and get extra discount\nlink : " + link +
                                     "\nThanks & Regards\nShamSweets";
                string _user = HttpUtility.UrlEncode("shamsweet"); // API user name to send SMS
                string _pass = HttpUtility.UrlEncode("12345");     // API password to send SMS
                string _route = HttpUtility.UrlEncode("transactional");
                string _senderid = HttpUtility.UrlEncode("WISHHH");
                string _recipient = HttpUtility.UrlEncode(customer.PhoneNumber);  // who will receive message
                string _messageText = HttpUtility.UrlEncode(messagebody); // text message

                // Creating URL to send sms
                string _createURL = "http://www.smsnmedia.com/api/push?user=" + _user + "&pwd=" + _pass + "&route=" + _route + "&sender=" + _senderid + "&mobileno=91" + _recipient + "&text=" + _messageText;

                HttpWebRequest _createRequest = (HttpWebRequest)WebRequest.Create(_createURL);
                // getting response of sms
                HttpWebResponse myResp = (HttpWebResponse)_createRequest.GetResponse();
                StreamReader _responseStreamReader = new StreamReader(myResp.GetResponseStream());
                string responseString = _responseStreamReader.ReadToEnd();
                _responseStreamReader.Close();
                myResp.Close();
            }
            catch (Exception ex)
            {

            }
            //}
        }
    }
}