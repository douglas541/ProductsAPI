using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static readonly List<Product> products = new List<Product>();

        [HttpGet("GetProducts/{codigo}")]
        public async Task<ActionResult<List<Product>>> GetProducts(int codigo)
        {
            try
            {
                var product = products.Find(product => product.Codigo == codigo);
                if (product == null)
                {
                    return NotFound("File Not Found");
                }
                return Ok(product);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return Ok(products);
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult<List<Product>>> AddProduct(Product productRequest)
        {
            try
            {
                var product = products.Find(product => product.Codigo == productRequest.Codigo);
                if(product != null)
                    return StatusCode(409, $"Product Already Exists");

                products.Add(productRequest);
                return Ok(products);
            } 
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("DeleteProduct")]
        public async Task<ActionResult<List<Product>>> DeleteProduct(int codigo)
        {
            try
            {
                var product = products.Find(product => product.Codigo == codigo);
                if (product == null)
                    return BadRequest(product);

                products.Remove(product);

                return Ok(products);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<List<Product>>> UpdateProduct(Product productRequest)
        {
            try
            {
                var product = products.Find(product => product.Codigo == productRequest.Codigo);
                if (product == null)
                    return BadRequest("Product not found");

                product.Codigo = productRequest.Codigo;
                product.Descricao = productRequest.Descricao;
                product.Preco = productRequest.Preco;
                product.Nome = productRequest.Nome;

                return Ok(products);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}