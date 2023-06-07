using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
 
using AzFnHttpService.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
namespace AzFnHttpService
{
    /// <summary>
    /// IActionResult: The Http Response
    /// </summary>
    public  class HttpAPIFunction
    {
        MyCompanyContext ctx;

        public HttpAPIFunction()
        {
            ctx  = new MyCompanyContext();
        }


        [FunctionName("Get")]
        public  async Task<IActionResult> GetProducts(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Products")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<Product> products = await ctx.Products.ToListAsync();
            return new OkObjectResult(products);
        }

        [FunctionName("GetById")]
        public  async Task<IActionResult> GetProductById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Products/{id:int}")] HttpRequest req,int id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            Product product =  await ctx.Products.FindAsync(id);
            if (product == null)
                return new NotFoundObjectResult("Resource is not found");

            return new OkObjectResult(product);
        }

        [FunctionName("Post")]
        public  async Task<IActionResult> CreateProduct(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "Products")] HttpRequest req ,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // 1. Read the BOdy as strean and convert it into string
            
            string bodyData = new StreamReader(req.Body).ReadToEnd();

            // 2. Deserialize the data into the Product Object using Systerm.Text.Json.JsonSerializer.Deserialize() method

            Product product =  JsonSerializer.Deserialize<Product>(bodyData);

            // 3 addd data to DbSet
            var result = await ctx.Products.AddAsync(product);
            await ctx.SaveChangesAsync();

            return new OkObjectResult(result.Entity);
        }
        [FunctionName("Put")]
        public  async Task<IActionResult> UpdateProduct(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "Products/{id:int}")] HttpRequest req,int id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            Product productToUpdate = await ctx.Products.FindAsync(id);
            if (productToUpdate == null)
                return new NotFoundObjectResult("Resource is not found");

            // 1. Read the BOdy as strean and convert it into string

            string bodyData = new StreamReader(req.Body).ReadToEnd();

            // 2. Deserialize the data into the Product Object using Systerm.Text.Json.JsonSerializer.Deserialize() method

            Product product = JsonSerializer.Deserialize<Product>(bodyData);

            // Update

            productToUpdate.ProductName = product.ProductName;
            productToUpdate.Manufacturere = product.Manufacturere;
            productToUpdate.Price = product.Price;

            await ctx.SaveChangesAsync();



            return new OkObjectResult(productToUpdate);
        }
        [FunctionName("Delete")]
        public  async Task<IActionResult> DeleteProduct(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "Products/{id:int}")] HttpRequest req, int id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            Product productToDelete = await ctx.Products.FindAsync(id);
            if (productToDelete == null)
                return new NotFoundObjectResult("Resource is not found");

            // Delete it
            ctx.Products.Remove(productToDelete);
            await ctx.SaveChangesAsync();
            return new OkObjectResult("Record is Deleted");
        }
    }
}
