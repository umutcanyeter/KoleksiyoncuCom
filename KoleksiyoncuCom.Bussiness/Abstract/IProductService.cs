using KoleksiyoncuCom.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KoleksiyoncuCom.Bussiness.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();
        List<Product> GetByCategory(int categoryId);
        List<Product> GetBySellerId(int sellerId);
        Product GetById(int productId);
        void Add(Product product);
        void Update(Product product);
        void Delete(int productId);
    }
}
