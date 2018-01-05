using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public class MenuRepository : IMenuRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<MenuModel> GetAllMenuList(int storeid)
        {
            using (shamsweetsfeedback_androidEntities context = new shamsweetsfeedback_androidEntities())
            {
                var data = (from z in context.menumasters
                                //join r in context.orderdetails on z.OrderNumber equals r.OrderNumber
                                //join p in context.productmasters on r.ProductId equals p.ProductId into productDetails
                                //from tempc in productDetails.DefaultIfEmpty()
                                //where z.LoginId == loginid
                            orderby z.MenuName descending
                            select new MenuModel()
                            {
                                MenuId = z.MenuId,
                                MenuName = z.MenuName,
                                MenuPrice = (decimal)z.MenuPrice,
                                MenuImageUrl = "http://103.233.79.234/Data/ShamSweetsFeedback_Android/MenuImage/" + z.MenuId + ".jpeg"
                            }).ToList();
                return data; 
            }
        }

        public IEnumerable<MenuModel> GetAllMenuList()
        {
            using (shamsweetsfeedback_androidEntities context = new shamsweetsfeedback_androidEntities())
            {
                var data = (from z in context.menumasters
                                //join r in context.orderdetails on z.OrderNumber equals r.OrderNumber
                                //join p in context.productmasters on r.ProductId equals p.ProductId into productDetails
                                //from tempc in productDetails.DefaultIfEmpty()
                                //where z.LoginId == loginid
                            orderby z.MenuName descending
                            select new MenuModel()
                            {
                                MenuId = z.MenuId,
                                MenuName = z.MenuName,
                                MenuPrice = (decimal)z.MenuPrice,
                                MenuImageUrl = "http://103.233.79.234/Data/ShamSweetsFeedback_Android/MenuImage/" + z.MenuId + ".jpeg"
                            }).ToList();
                return data;
            }
        }

        public MenuModel AddNewMenu(MenuModel model)
        {
            //ResponseStatus respponse = new ResponseStatus();
            MenuModel data = new MenuModel();
            using (shamsweetsfeedback_androidEntities context = new shamsweetsfeedback_androidEntities())
            {

                var me = context.menumasters.Find(model.MenuId);
                if (me != null)
                {
                    me.MenuName = model.MenuName;
                    me.MenuPrice = model.MenuPrice;
                    //me.StoreId = model.StoreId;
                    var result = context.SaveChanges();
                    if (result > 0)
                    {
                        data.MenuId = me.MenuId;
                    }
                }
                else
                {
                    menumaster m = new menumaster();
                    m.MenuName = model.MenuName;
                    m.MenuPrice = model.MenuPrice;
                    //m.StoreId = model.StoreId;
                    context.menumasters.Add(m);
                    var result = context.SaveChanges();
                    if (result > 0)
                    {
                        data.MenuId = m.MenuId;
                    }
                }
            }
            return data;
        }
    }
}