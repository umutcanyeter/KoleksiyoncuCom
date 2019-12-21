using KoleksiyoncuCom.Bussiness.Abstract;
using KoleksiyoncuCom.DataAccess.Abstract;
using KoleksiyoncuCom.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KoleksiyoncuCom.Bussiness.Concrete
{
    public class BuyerManager : IBuyerService
    {
        private IBuyerDal _buyerDal;
        public BuyerManager(IBuyerDal buyerDal)
        {
            _buyerDal = buyerDal;
        }
        public void Add(Buyer buyer)
        {
            _buyerDal.Add(buyer);
        }

        public void Delete(int buyerId)
        {
            _buyerDal.Delete(new Buyer() { BuyerId = buyerId });
        }

        public List<Buyer> GetAll()
        {
            return _buyerDal.GetList();
        }

        public Buyer GetById(int id)
        {
            return _buyerDal.Get(b => b.BuyerId == id);
        }

        public void Update(Buyer buyer)
        {
            _buyerDal.Update(buyer);
        }
    }
}
