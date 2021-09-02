using AutoMapper;
using Diplomski.Data.Interfaces;
using Diplomski.DTOs;
using Diplomski.DTOs.Product;
using Diplomski.Entities;
using Diplomski.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;

namespace Diplomski.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly ImagesPathsOptions _imagesPathsOptions;


        public ProductsController(
            IProductRepository repository, 
            IMapper mapper,
            IWebHostEnvironment environment,
            IOptions<ImagesPathsOptions> imagesPathsOptions)
        {
            _repository = repository;
            _mapper = mapper;
            _environment = environment;
            _imagesPathsOptions = imagesPathsOptions.Value;
        }


        [HttpGet]
        public IActionResult GetProducts(
            [FromQuery]string naziv,
            [FromQuery]string[] kategorija,
            [FromQuery]string[] proizvodjac,
            [FromQuery]string[] ambalaza,
            [FromQuery]decimal[] zapremina)
        {
            var products = this._repository.GetProducts(naziv,kategorija,proizvodjac,ambalaza,zapremina);

            var productsDTO = _mapper.Map<IEnumerable<ProductSimpleReadDTO>>(products);

            return Ok(productsDTO);
        }


        [HttpGet("{id}", Name ="GetProductByID")]
        public IActionResult GetProductByID(int id)
        {
            var product = _repository.GetProductByID(id);

            if(product is null)
            {
                return NotFound("Proizvod ne postoji");
            }

            var productDTO = _mapper.Map<ProductFullReadDTO>(product);

            return Ok(productDTO);
        }


        [HttpPost]
        [Authorize]
        public IActionResult CreateProduct([FromForm]ProductCreateDTO productToBeCreated)
        {
            var product = _mapper.Map<Product>(productToBeCreated);

            var imageFormFile = productToBeCreated.ProductImage;
            var imagesFolder = Path.Combine(_environment.WebRootPath, _imagesPathsOptions.ProductImagePath);
            var imageFileName = ImageUploader.SaveImage(imageFormFile, imagesFolder);
            product.ImageFileName = imageFileName;

            _repository.CreateProduct(product);

            _repository.SaveChanges();

            var productDetailsDTO = _mapper.Map<ProductFullReadDTO>(product);

            return CreatedAtRoute(
                nameof(GetProductByID),
                new { id = product.ProductID },
                productDetailsDTO
                );
        }


        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateProduct(int id, [FromForm]ProductUpdateDTO updatedProduct)
        {
            var productToBeUpdated = _repository.GetProductByID(id);

            if(productToBeUpdated is null)
            {
                return NotFound("Proizvod ne postoji");
            }

            if(updatedProduct.ProductImage is not null)
            {
                var imageFormFile = updatedProduct.ProductImage;
                var imagesFolder = Path.Combine(_environment.WebRootPath, _imagesPathsOptions.ProductImagePath);
                var newImageFileName = ImageUploader.SaveImage(imageFormFile, imagesFolder);

                var pathToOldFile = Path.Combine(imagesFolder, productToBeUpdated.ImageFileName);
                ImageUploader.DeleteImage(pathToOldFile);

                productToBeUpdated.ImageFileName = newImageFileName;
            }

            _mapper.Map(updatedProduct, productToBeUpdated);

            _repository.SaveChanges();

            return NoContent();
        }


        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteProductByID(int id)
        {
            var product = _repository.GetProductByID(id);

            if(product is null)
            {
                return NotFound("Proizvod nije pronadjen");
            }

            var pathToProductImage = Path.Combine(
                _environment.WebRootPath,
                _imagesPathsOptions.ProductImagePath,
                product.ImageFileName
            );

            ImageUploader.DeleteImage(pathToProductImage);

            _repository.DeleteProduct(product);

            _repository.SaveChanges();

            return NoContent();
        }


        [HttpGet("volumes")]
        public IActionResult GetAllProductVolumes()
        {
            var volumes = _repository.GetAllVolumes();

            var volumesDTO = _mapper.Map<IEnumerable<VolumeSimpleReadDTO>>(volumes);

            return Ok(volumesDTO);
        }


        [HttpGet("packagingMaterials")]
        public IActionResult GetAllPackagingMaterials()
        {
            var packagingMaterials = _repository.GetAllPackagingMaterials();

            var packagingMaterialsDTO = _mapper.Map<IEnumerable<PackagingMaterialSimpleReadDTO>>(packagingMaterials);

            return Ok(packagingMaterialsDTO);
        }
        

        [HttpGet("categories")]
        public IActionResult GetAllCategories()
        {
            var categories = _repository.GetAllCategories();

            var categoriesDTO = _mapper.Map<IEnumerable<CategorySimpleReadDTO>>(categories);

            return Ok(categoriesDTO);
        }

    }
}
