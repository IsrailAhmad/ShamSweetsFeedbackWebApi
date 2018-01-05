using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public UserModel UserLogin(string username,string password,int role)
        {
            UserModel data = new UserModel();
            using (shamsweetsfeedback_androidEntities context = new shamsweetsfeedback_androidEntities())
            {
                var result = context.usermasters.Where(x => x.UserName == username && x.Password == password && x.Role == role).FirstOrDefault();
                if (result.LoginId > 0)
                {
                    data.LoginId = result.LoginId;
                    data.UserName = result.UserName;
                    data.EmailId = result.EmailId;
                    data.Role = (int)result.Role;
                }
                return data;
            }
        }


    }
}