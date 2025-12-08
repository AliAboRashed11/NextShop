using Ecom.Core.interfaces;
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

        public ICategoryRepositry CategoryRepositry { get; } 

        public IProductRepositry ProductRepositry { get; }

        public IPhotoRepositry PhotoRepositry { get; }
        public UnitOfWork(AppDContext context)
        {
            _context = context;
            CategoryRepositry = new CategoryRepositry(_context);
            ProductRepositry = new ProductRepositry(_context);
            PhotoRepositry = new PhotoRepositry(_context);
        }
    }
}
