using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public class WebSiteLoginRepository : IWebSiteLoginRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public UserLoginModel WebsiteLogin(string username, string password)
        {
            using (shamsweetsfeedback_androidEntities context = new shamsweetsfeedback_androidEntities())
            {
                var result = context.website_login.Where(u => u.UserName == username && u.Password == password && u.IsActive == "Y")
                    .Select(u => new UserLoginModel()
                    {
                        UserID = u.UserId,
                        UserName = u.UserName,
                        FirstName = u.FisrtName,
                        LastName = u.LastName
                    }).FirstOrDefault();
                return result;
            }
        }

        //public ResponseStatus SaveCustomerData(CustomerEntryModel model)
        //{
        //    ResponseStatus respponse = new ResponseStatus();
        //    CategoryModel data = new CategoryModel();
        //    using (shamsweetsfeedback_androidEntities context = new shamsweetsfeedback_androidEntities())
        //    {
        //        feedbackcustomermaster c = new feedbackcustomermaster();
        //        c.CustomerName = model.CustomerName;
        //        c.PhoneNumber = model.PhoneNumber;
        //        c.EmailId = model.Email;
        //        c.Address = model.Address;
        //        context.feedbackcustomermasters.Add(c);
        //        var result = context.SaveChanges();
        //        if (result > 0)
        //        {
        //            respponse.isSuccess = true;
        //            respponse.serverResponseTime = System.DateTime.Now;
        //        }
        //        else
        //        {
        //            respponse.isSuccess = false;
        //            respponse.serverResponseTime = System.DateTime.Now;
        //        }
        //    }
        //    return respponse;
        //}
    }
}