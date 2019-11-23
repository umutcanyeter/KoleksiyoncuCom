using KoleksiyoncuCom.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KoleksiyoncuCom.Bussiness.Abstract
{
    public interface ISellerService
    {
        List<Seller> GetAll();
        Seller GetById(int id);
        void Add(Seller seller);
        void Update(Seller seller);
        void Delete(int sellerId);
    }
}
