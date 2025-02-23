using System.Threading.Tasks;
using DotNetProductAPIShowCase.Applications.DTOS;
using DotNetProductAPIShowCase.Applications.Exceptions;
using DotNetProductAPIShowCase.Domains;
using DotNetProductAPIShowCase.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetProductAPIShowCase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ProductService _service;
        private readonly IValidator<ProductDTO> _productDTOValidator;
        private readonly IValidator<ProductPageDTO> _productPageDTOValidator;
        private readonly IValidator<UpdateProductPriceDTO> _updateProductPriceDTO;

        public ProductController(ProductService productService, IValidator<ProductDTO> productDTOValidator, IValidator<ProductPageDTO> productPageDTOValidator, IValidator<UpdateProductPriceDTO> updateProductPriceDTO)
        {
            _service = productService;
            _productDTOValidator = productDTOValidator;
            _productPageDTOValidator = productPageDTOValidator;
            _updateProductPriceDTO = updateProductPriceDTO;
        }

        // separate validation method into private for DRY principle
        private async Task ValidateProductDTO(ProductDTO product)
        {
            ValidationResult validationResult = await _productDTOValidator.ValidateAsync(product);

            if (!validationResult.IsValid)
            {
                throw new BadRequestException("productDTO is not correct");
            }
        }

        private async Task ValidateProductPageDTO(ProductPageDTO productPage)
        {
            ValidationResult validationResult = await _productPageDTOValidator.ValidateAsync(productPage);

            if (!validationResult.IsValid)
            {
                throw new BadRequestException("productPageDTO is not correct");
            }
        }

        private async Task ValidateUpdateProductPriceDTO(UpdateProductPriceDTO updateProductPrice)
        {
            ValidationResult validationResult = await _updateProductPriceDTO.ValidateAsync(updateProductPrice);

            if (!validationResult.IsValid)
            {
                throw new BadRequestException("updateProductPriceDTO is not correct");
            }
        }

        private void ValidateId(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("id is not correct");
            }
        }

        [HttpGet]
        [Route("healthcheck")]
        public ActionResult<string> HealthCheck()
        {
            return Ok("Hello World!");
        }

        [HttpGet]
        [Route("product")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts([FromQuery] ProductPageDTO productPage)
        {
            await this.ValidateProductPageDTO(productPage);

            IEnumerable<Product> products = await this._service.GetAllProducts(productPage);

            return Ok(products.ToList<Product>());
        }

        [HttpGet]
        [Route("product/{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            this.ValidateId(id);

            Product product = await this._service.GetProductById(id);

            return Ok(product);
        }

        [HttpPost]
        [Route("product")]
        public async Task<ActionResult<int>> AddProduct([FromBody] ProductDTO product)
        {
            await this.ValidateProductDTO(product);

            int insertedProductId = await this._service.AddProduct(product);

            return Ok(insertedProductId);
        }

        [HttpPut]
        [Route("product/{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] ProductDTO product)
        {
            await this.ValidateProductDTO(product);

            Product updatedProduct = await this._service.UpdateProduct(id, product);

            return Ok(updatedProduct);
        }

        [HttpPatch]
        [Route("product/{id}")]
        public async Task<ActionResult<Product>> UpdateProductPrice(int id, [FromBody] UpdateProductPriceDTO updateProductPrice)
        {
            await this.ValidateUpdateProductPriceDTO(updateProductPrice);

            Product updatedProduct = await this._service.UpdateProductPrice(id, updateProductPrice.Price);

            return Ok(updatedProduct);
        }

        [HttpDelete]
        [Route("product/{id}")]
        public async Task<ActionResult<bool>> DeleteProduct(int id)
        {
            this.ValidateId(id);

            await this._service.DeleteProduct(id);

            return Ok(true);
        }
    }
}
