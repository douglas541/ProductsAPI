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
        private static List<Product> products = new List<Product>();

        [HttpGet("GetProduct")]
        public async Task<ActionResult<List<Product>>> GetProduct(int codigo)
        {
            try
            {
                var product = products.Find(product => product.codigo == codigo);
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
        public async Task<ActionResult<List<Product>>> AddProduct(Product product)
        {
            try
            {
                products.Add(product);
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
                var product = products.Find(product => product.codigo == codigo);
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
                var product = products.Find(product => product.codigo == productRequest.codigo);
                if (product == null)
                    return BadRequest("Product not found!");

                product.codigo = productRequest.codigo;
                product.descricao = productRequest.descricao;
                product.preco = productRequest.preco;
                product.nome = productRequest.nome;

                return Ok(products);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}