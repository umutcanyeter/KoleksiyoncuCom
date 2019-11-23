using KoleksiyoncuCom.Bussiness.Abstract;
using KoleksiyoncuCom.DataAccess.Abstract;
using KoleksiyoncuCom.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KoleksiyoncuCom.Bussiness.Concrete
{
    public class SellerManager : ISellerService
    {
        private ISellerDal _sellerDal;
        public SellerManager(ISellerDal sellerDal)
        {
            _sellerDal = sellerDal;
        }
        public void Add(Seller seller)
        {
            _sellerDal.Add(seller);
        }

        public void Delete(int sellerId)
        {
            _sellerDal.Delete(new Seller { SellerId = sellerId });
        }

        public List<Seller> GetAll()
        {
            return _sellerDal.GetList();
        }

        public Seller GetById(int id)
        {
            return _sellerDal.Get(s => s.SellerId == id);
        }

        public void Update(Seller seller)
        {
            _sellerDal.Update(seller);
        }
    }
}
