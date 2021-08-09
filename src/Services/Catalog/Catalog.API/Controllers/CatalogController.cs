namespace Catalog.API.Controllers
{
    using DataAccessCore.Catalog.API.Entities;
    using DataAccessCore.Catalog.API.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogRepository<Product> _catalogRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ICatalogRepository<Product> catalogRepository, ILogger<CatalogController> logger)
        {
            this._catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsAsync()
        {
            var products = await this._catalogRepository.GetProductsAsync();

            return this.Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductByIdAsync(string id)
        {
            var product = await this._catalogRepository.GetProductByIdAsync(id);

            if (Equals(product, null))
            {
                this._logger.LogError($"Product with id: {id}, not found.");

                return NotFound();
            }

            return this.Ok(product);
        }

        [HttpGet]
        [Route("[action]/{category}", Name = "GetProductsByCategory")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategoryAsync(string category)
        {
            var prosucts = await this._catalogRepository.GetProductByCategoryAsync(category);

            return this.Ok(prosucts);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProductAsync([FromBody] Product product)
        {
            await this._catalogRepository.AddAsync(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> UpdateProductAsync([FromBody] Product product)
        {
            return this.Ok(await this._catalogRepository.UpdateAsync(product));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductAsync(string id)
        {
            return this.Ok(await this._catalogRepository.DeleteAsync(id));
        }
    }
}
