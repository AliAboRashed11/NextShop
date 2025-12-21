using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;
using Ecom.Core.interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositries
{
    public class ProductRepositry : GenericRepostitry<Product>, IProductRepositry
    {
        private readonly AppDContext _context;
        private readonly IMapper _mapper;
        private readonly IImageManagementService _managementService;

        public ProductRepositry(AppDContext context, IMapper mapper,IImageManagementService managementService) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _managementService = managementService;
        }

        public async Task<bool> AddAsyncProduct(AddProductDto productDto)
        {
            if (productDto == null)
            {
                return false;
            }
            var product = _mapper.Map<Product>(productDto);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            var ImagePath = await _managementService.UploadImageAsync(productDto.Photo,productDto.Name);

            var photo = ImagePath.Select(path => new Photo
            {
                ImagName = path,
                ProductId = product.Id

            }).ToList();
            await _context.Photos.AddRangeAsync(photo);
            await _context.SaveChangesAsync();
            return true;

        }

   

        public async Task<bool> UpdateAsyncProduct(UpdateProductDto updateProductDto)
        {
            if(updateProductDto == null)
            {
                return false;
            }
            var Findproduct = await _context.Products.Include(a => a.Category)
                .Include(a=> a.Photos).FirstOrDefaultAsync(a => a.Id == updateProductDto.Id);

            if(Findproduct == null)
            {
                return false;
            }
            _mapper.Map(updateProductDto, Findproduct);

            var FindPhoto= await _context.Photos.Where(a => a.ProductId == updateProductDto.Id).ToListAsync();

            foreach (var item in FindPhoto)
            {
                _managementService.DeleteImageAsync(item.ImagName);
            }
            _context.Photos.RemoveRange(FindPhoto);

            var ImagePath = await _managementService.UploadImageAsync(updateProductDto.Photo, Findproduct.Name);
            var photo = ImagePath.Select(path => new Photo
            {
                ImagName = path,
                ProductId = Findproduct.Id
            }).ToList();
            await _context.Photos.AddRangeAsync(photo);

            _context.Products.Update(Findproduct);
            await _context.SaveChangesAsync();
            return true;


        }


        public async Task<bool> DeleteAsyncProduct(Product product)
        {
            var FindPhoto = await _context.Photos.Where(a => a.ProductId == product.Id).ToListAsync();
            foreach (var item in FindPhoto)
            {
                _managementService.DeleteImageAsync(item.ImagName);
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
        }
}
