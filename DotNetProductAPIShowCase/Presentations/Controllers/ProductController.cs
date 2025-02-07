using System.Threading.Tasks;
using DotNetProductAPIShowCase.Applications.DTOS;
using DotNetProductAPIShowCase.Domains;
using DotNetProductAPIShowCase.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetProductAPIShowCase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ProductService _service;

        public ProductController(ProductService productService)
        {
            _service = productService;
        }

        [HttpGet]
        [Route("product")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts([FromQuery] ProductPageDTO productPage)
        {
            IEnumerable<Product> products = await this._service.GetAllProducts(productPage);

            return Ok(products.ToList<Product>());
        }

        [HttpGet]
        [Route("product/{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            Product product = await this._service.GetProductById(id);

            return Ok(product);
        }

        [HttpPost]
        [Route("product")]
        public async Task<ActionResult<int>> AddProduct([FromBody] ProductDTO product)
        {
            int insertedProductId = await this._service.AddProduct(product);

            return Ok(insertedProductId);
        }

        [HttpPut]
        [Route("product/{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] ProductDTO product)
        {
            Product updatedProduct = await this._service.UpdateProduct(id, product);

            return Ok(updatedProduct);
        }

        [HttpPatch]
        [Route("product/{id}")]
        public async Task<ActionResult<Product>> UpdateProductPrice(int id, [FromBody] UpdateProductPriceDTO updateProductPrice)
        {
            Product updatedProduct = await this._service.UpdateProductPrice(id, updateProductPrice.Price);

            return Ok(updatedProduct);
        }

        [HttpDelete]
        [Route("product/{id}")]
        public async Task<ActionResult<bool>> DeleteProduct(int id)
        {
            await this._service.DeleteProduct(id);

            return Ok(true);
        }
    }
}
