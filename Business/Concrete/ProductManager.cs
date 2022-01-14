using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performans;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerts.Validation;
using Core.Utilities.Interceptors;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal productDal;
        public ProductManager(IProductDal _productDal)
        {
            productDal = _productDal;
        }
        [ValidationAspect(typeof(ProductValidator),Priority=1)]
        [CacheRemoveAspect("IProductService.Get")]
        //[ValidationAspect(typeof(ProductValidator), Priority = 2)]
        public IResult Add(Product product)
        {
            //ValidationTool.Validate(new ProductValidator(),product);
            productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IDataResult<Product> GetById(int productId)
        {
          
            return new SuccessDataResult<Product>(productDal.Get(p => p.ProductId==productId));
        }
        //Bu operasyonun çalışması 5 sn den fazla olursa
        [PerformansAspect(5)]
        public IDataResult<List<Product>> GetList()
        {
            Thread.Sleep(5000);
            return new SuccessDataResult<List<Product>>(productDal.GetList());
        }

        [SecuredOperation("Product.List,Admin")]
        //[CachAspect(duration:1)]
        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(productDal.GetList(p=>p.CategoryId==categoryId));
        }

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Product product)
        {
            productDal.Update(product);
           // productDal.Add(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        
        public IResult Update(Product product)
        {
           productDal.Update(product);
           return new SuccessResult(Messages.ProductUpdated);

        }

        
    }
}
