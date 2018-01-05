using EverGreenWebApi.Interfaces;
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
    public class CategoryController : ApiController
    {
        static readonly ICategoryRepository _repository = new CategoryRepository();

        [HttpPost]
        public HttpResponseMessage GetAllCategory(CategoryModel model)
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                var data = _repository.GetAllCategoryList(model.MenuId);
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
        public HttpResponseMessage GetAllCategoryList()
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                var data = _repository.GetAllCategoryList();
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
        public HttpResponseMessage AddNewCategory(string CategoryId, string categoryname)
        {
            CategoryModel model = new CategoryModel();
            model.CategoryId = Convert.ToInt32(CategoryId);
            model.CategoryName = categoryname;
            //model.StoreId = Convert.ToInt32(storeid);
           // model.MenuId = Convert.ToInt32(menuid);
            //model.CategoryDescription = CategoryDescription;
            ResponseStatus response = new ResponseStatus();
            try
            {
                var data = _repository.AddNewCategory(model);
                //if (data.CategoryId > 0)
                //{                   
                return Request.CreateResponse(HttpStatusCode.OK, new { data });
                //}
                //else
                //{                   
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
