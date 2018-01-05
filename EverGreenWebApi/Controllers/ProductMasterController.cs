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
    public class ProductMasterController : ApiController
    {
        static readonly IProductMasterRepository _repository = new ProductMasterRepository();

        [HttpGet]
        public HttpResponseMessage GetAllProductList()
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                var data = _repository.GetAllProductList();
                if (data.Count() > 0)
                {
                    //response.isSuccess = true;
                    //response.serverResponseTime = System.DateTime.Now;
                    return Request.CreateResponse(HttpStatusCode.OK, new { data, response });
                }
                else
                {
                    //response.isSuccess = false;
                    //response.serverResponseTime = System.DateTime.Now;
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { response });
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
            }
        }

        [HttpGet]
        public HttpResponseMessage AddNewProduct(string ProductId, string ProductName, string Price,  string FoodType)
        {
            ProductModel model = new ProductModel();
            model.ProductId = Convert.ToInt32(ProductId);
            //model.CategoryId = Convert.ToInt32(CategoryName);
            model.ProductName = ProductName;
            model.Price = Convert.ToDecimal(Price);
            model.FoodType = FoodType;
            //if (FoodType)
            //{
            //    model.FoodType = "N";
            //}
            //else
            //{
            //    model.FoodType = "V";
            //}           

            ResponseStatus response = new ResponseStatus();
            try
            {
                var data = _repository.AddNewProduct(model);
                //if (data.ProductId > 0)
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
