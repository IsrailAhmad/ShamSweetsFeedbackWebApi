using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public class ProductMasterRepository: IProductMasterRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductMasterModel> GetAllProductList()
        {
            using (shamsweetsfeedback_androidEntities context = new shamsweetsfeedback_androidEntities())
            {
                //string path = "http://103.233.79.234/Data/EverGreen_Android/ProductPictures/";

                var result = context.productmasters.OrderBy(p => p.ProductName);
                var data = result.Select(p => new ProductMasterModel()
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    //CategoryId = (int)p.CategoryId,
                    //UnitPrice = (decimal)p.UnitPrice,
                    //GST = (decimal)p.GST,
                    //Discount = (decimal)p.Discount,
                    //TaxType = p.TaxType,
                    //UOM = p.UOM,
                    //HSN = p.HSN,
                    //SweetsReset = p.SweetsReset,
                    //ProductDetails = p.ProductDetails,
                    //Lock = p.Lock,
                    //ProductPicturesUrl = path + p.ProductId + "ProductPictures.jpg",
                }).ToList();
                return data;
            }
        }

        public ProductModel AddNewProduct(ProductModel model)
        {
            ProductModel data = new ProductModel();
            using (shamsweetsfeedback_androidEntities context = new shamsweetsfeedback_androidEntities())
            {
                var pr = context.productmasters.Find(model.ProductId);
                if (pr != null)
                {
                    //pr.CategoryId = model.CategoryId;
                    pr.ProductName = model.ProductName;
                    pr.Price = model.Price;
                    pr.FoodType = model.FoodType;                              
                    var result = context.SaveChanges();
                    if (result > 0)
                    {
                        data.ProductId = pr.ProductId;
                    }
                }
                else
                {

                    productmaster p = new productmaster();
                    //p.CategoryId = model.CategoryId;
                    p.ProductName = model.ProductName;
                    p.Price = model.Price;
                    //p.GST = model.GST;
                    //p.Discount = model.Discount;
                    //p.TaxType = model.TaxType;
                    p.FoodType = model.FoodType;
                    //p.UOM = model.UOM;
                    //p.ProductDetails = model.ProductDetails;
                    //p.DeliveryCharge = model.DeliveryCharge;
                    context.productmasters.Add(p);
                    var result = context.SaveChanges();
                    if (result > 0)
                    {
                        data.ProductId = p.ProductId;
                    }
                }
            }
            return data;
        }
    }
}