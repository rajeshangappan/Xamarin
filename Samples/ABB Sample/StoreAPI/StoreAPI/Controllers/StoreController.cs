using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Contracts;
using StoreAPI.Models;
using System;

namespace StoreAPI.Controllers
{
    /// <summary>
    /// Defines the <see cref="StoreController" />.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        #region PRIVATE_VARIABLES

        /// <summary>
        /// Defines the _productService.
        /// </summary>
        private IProductService _productService;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreController"/> class.
        /// </summary>
        /// <param name="productService">The productService<see cref="IProductService"/>.</param>
        public StoreController(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// The AddProduct.
        /// </summary>
        /// <param name="product">The product<see cref="Product"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [AllowAnonymous]
        [HttpGet("aproduct")]
        public IActionResult AddProduct(Product product)
        {
            var result = _productService.AddProducts(product);

            return Ok(new { data = result });
        }

        /// <summary>
        /// The DeleteProduct.
        /// </summary>
        /// <param name="product">The product<see cref="Product"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [Authorize]
        [HttpPost("dproduct")]
        public IActionResult DeleteProduct(Product product)
        {
            var result = _productService.DeleteProduct(product);
            return Ok(new { data = result });
        }

        /// <summary>
        /// The GetProduct.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [AllowAnonymous]
        [HttpGet("product")]
        public IActionResult GetProduct(Guid id)
        {
            var result = _productService.GetProduct(id);
            if (result == null)
            {
                return new NotFoundResult();
            }

            return Ok(new { data = result });
        }

        /// <summary>
        /// The GetProducts.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [AllowAnonymous]
        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            return Ok(_productService.GetProducts());
        }

        /// <summary>
        /// The UpdateProduct.
        /// </summary>
        /// <param name="product">The product<see cref="Product"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpPost("uproduct")]
        public IActionResult UpdateProduct(Product product)
        {
            var result = _productService.Updateproduct(product);
            return Ok(new { data = result });
        }

        #endregion
    }
}
