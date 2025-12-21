using AutoMapper;
using Ecom.Core.interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDContext _context;
        private readonly IMapper _mapper;
        private readonly IImageManagementService _managementService;

        public ICategoryRepositry CategoryRepositry { get; } 

        public IProductRepositry ProductRepositry { get; }

        public IPhotoRepositry PhotoRepositry { get; }
        public UnitOfWork(AppDContext context, IMapper mapper, IImageManagementService managementService)
        {
            _context = context;
            _mapper = mapper; 
            _managementService = managementService;
            CategoryRepositry = new CategoryRepositry(_context);
            ProductRepositry = new ProductRepositry(_context, _mapper, _managementService);
            PhotoRepositry = new PhotoRepositry(_context);
        }
    }
}
