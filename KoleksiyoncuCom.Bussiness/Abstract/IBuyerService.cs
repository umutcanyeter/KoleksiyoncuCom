using KoleksiyoncuCom.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KoleksiyoncuCom.Bussiness.Abstract
{
    public interface IBuyerService
    {
        List<Buyer> GetAll();
        Buyer GetById(int id);
        void Add(Buyer buyer);
        void Update(Buyer buyer);
        void Delete(int buyerId);
    }
}
