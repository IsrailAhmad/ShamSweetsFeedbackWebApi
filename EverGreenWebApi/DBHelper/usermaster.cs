//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EverGreenWebApi.DBHelper
{
    using System;
    using System.Collections.Generic;
    
    public partial class usermaster
    {
        public int LoginId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
        public Nullable<int> Role { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }
}