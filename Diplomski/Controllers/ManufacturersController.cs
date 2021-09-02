using AutoMapper;
using Diplomski.Data.Interfaces;
using Diplomski.DTOs.Manufacturer;
using Diplomski.Entities;
using Diplomski.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Diplomski.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly ImagesPathsOptions _paths;

        public ManufacturersController(
            IManufacturerRepository repository, 
            IMapper mapper,
            IWebHostEnvironment environment,
            IOptions<ImagesPathsOptions> paths
            )
        {
            _repository = repository;
            _mapper = mapper;
            _paths = paths.Value;
            _environment = environment;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllManufacturers()
        {
            var manufacturers = _repository.GetAllManufacturers();
            var manufacturersDTO = _mapper.Map<IEnumerable<ManufacturerSimpleReadDTO>>(manufacturers);
            return Ok(manufacturersDTO);
        }


        [HttpGet("{id}", Name = "GetManufacturerByID")]
        [AllowAnonymous]
        public IActionResult GetManufacturerByID(int id)
        {
            var manufacturer = _repository.GetManufacturerByID(id);
            if(manufacturer == null)
            {
                return NotFound("Korisnik nije pronađen");
            }
            var manufacturerDTO = _mapper.Map<ManufacturerFullReadDTO>(manufacturer);
            return Ok(manufacturerDTO);
        }


        [HttpPost]
        public IActionResult CreateManufacturer([FromForm]ManufacturerCreateDTO manufacturerCreateDTO)
        {
            var manufacturer = _mapper.Map<Manufacturer>(manufacturerCreateDTO);

            var bannerFormFile = manufacturerCreateDTO.BannerImageFile;
            var bannerFolderPath = Path.Combine(_environment.WebRootPath, _paths.ManufacturerBannerPath);
            string bannerImageFileName = ImageUploader.SaveImage(bannerFormFile, bannerFolderPath);
            manufacturer.BannerImageFileName = bannerImageFileName;
            
            var logoFormFile = manufacturerCreateDTO.LogoImageFile;
            var logoFolderPath = Path.Combine(_environment.WebRootPath, _paths.ManufacturerLogoPath);
            string logoImageFileName = ImageUploader.SaveImage(logoFormFile, logoFolderPath);
            manufacturer.LogoImageFileName = logoImageFileName;
            

            _repository.CreateManufacturer(manufacturer);

            _repository.SaveChanges();

            var manufacturerDetailsDTO = _mapper.Map<ManufacturerFullReadDTO>(manufacturer);

            return CreatedAtRoute(
                nameof(GetManufacturerByID),
                new { id = manufacturer.ManufacturerID },
                manufacturerDetailsDTO);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateManufacturer(int id, [FromForm]ManufacturerUpdateDTO manufacturerUpdateDTO)
        {
            var manufacturerToBeUpdated = _repository.GetManufacturerByID(id);

            if(manufacturerToBeUpdated == null)
            {
                return NotFound();
            }

            _mapper.Map(manufacturerUpdateDTO, manufacturerToBeUpdated);


            if (manufacturerUpdateDTO.BannerImageFile is not null)
            {
                var newBannerFile = manufacturerUpdateDTO.BannerImageFile;
                var bannerFolderPath = Path.Combine(_environment.WebRootPath, _paths.ManufacturerBannerPath);
                string newBannerImageFileName = ImageUploader.SaveImage(newBannerFile, bannerFolderPath);

                string pathToOldBanner = Path.Combine(bannerFolderPath, manufacturerToBeUpdated.BannerImageFileName);
                ImageUploader.DeleteImage(pathToOldBanner);

                manufacturerToBeUpdated.BannerImageFileName = newBannerImageFileName;
            }

            if(manufacturerUpdateDTO.LogoImageFile is not null)
            {
                var newLogoFile = manufacturerUpdateDTO.LogoImageFile;
                var logoFolderPath = Path.Combine(_environment.WebRootPath, _paths.ManufacturerLogoPath);
                string newFileName = ImageUploader.SaveImage(newLogoFile, logoFolderPath);

                string pathToOldLogo = Path.Combine(logoFolderPath, manufacturerToBeUpdated.LogoImageFileName);
                ImageUploader.DeleteImage(pathToOldLogo);
                
                manufacturerToBeUpdated.LogoImageFileName = newFileName;
            }

            _repository.UpdateManufacturer(manufacturerToBeUpdated);
            _repository.SaveChanges();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteManufacturerByID(int id)
        {
            var manufacturer = _repository.GetManufacturerByID(id);

            if(manufacturer == null)
            {
                return NotFound();
            }

            var bannersFolderPath = Path.Combine(_environment.WebRootPath, _paths.ManufacturerBannerPath);
            var logosFolderPath = Path.Combine(_environment.WebRootPath, _paths.ManufacturerLogoPath);

            var pathToManufacturersBanner = Path.Combine(bannersFolderPath, manufacturer.BannerImageFileName);
            var pathToManufacturersLogo = Path.Combine(logosFolderPath, manufacturer.LogoImageFileName);

            ImageUploader.DeleteImage(pathToManufacturersBanner);
            ImageUploader.DeleteImage(pathToManufacturersLogo);

            _repository.DeleteManufacturer(manufacturer);
            _repository.SaveChanges();
            return NoContent();
        }

    }
}
