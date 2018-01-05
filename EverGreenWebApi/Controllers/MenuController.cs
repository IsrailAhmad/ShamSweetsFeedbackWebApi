﻿using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using EverGreenWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EverGreenWebApi.Controllers
{
    public class MenuController : ApiController
    {
        static readonly IMenuRepository _repository = new MenuRepository();

        [HttpPost]
        public HttpResponseMessage GetAllMenuList(StoreModel model)
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                if (model.StoreId > 0)
                {
                    var data = _repository.GetAllMenuList(model.StoreId);
                    if (data.Count() > 0)
                    {
                        response.isSuccess = true;
                        response.serverResponseTime = System.DateTime.Now;
                        return Request.CreateResponse(HttpStatusCode.OK, new { data, response });
                    }
                    else
                    {
                        response.isSuccess = false;
                        response.serverResponseTime = System.DateTime.Now;
                        return Request.CreateResponse(HttpStatusCode.BadRequest, new { response });
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetAllMenuList()
        {
            ResponseStatus response = new ResponseStatus();
            try
            {

                var data = _repository.GetAllMenuList();
                if (data.Count() > 0)
                {
                    response.isSuccess = true;
                    response.serverResponseTime = System.DateTime.Now;
                    return Request.CreateResponse(HttpStatusCode.OK, new { data, response });
                }
                else
                {
                    response.isSuccess = false;
                    response.serverResponseTime = System.DateTime.Now;
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { response });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
            }
        }

        [HttpGet]
        public HttpResponseMessage AddNewMenu(string menuid, string menuname, decimal menuprice)
        {
            MenuModel model = new MenuModel();
            model.MenuId = Convert.ToInt32(menuid);
            model.MenuName = menuname;
            model.MenuPrice = menuprice;
            //model.StoreId = Convert.ToInt32(storeid);
            ResponseStatus response = new ResponseStatus();
            try
            {
                var data = _repository.AddNewMenu(model);
                //if (data.MenuId > 0)
                //{
                //    response.isSuccess = true;
                //    response.serverResponseTime = DateTime.Now;
                return Request.CreateResponse(HttpStatusCode.OK, new { data });
                //}
                //else
                //{
                //    response.isSuccess = false;
                //    response.serverResponseTime = DateTime.Now;
                //    return Request.CreateResponse(HttpStatusCode.BadRequest, new { data });
                //}
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
            }
        }
    }
}
