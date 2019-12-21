using KoleksiyoncuCom.Core.DataAccess;
using KoleksiyoncuCom.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KoleksiyoncuCom.DataAccess.Abstract
{
    public interface ICartDal : IEntityRepository<Cart>
    {
        Cart GetByUserId(string userId);
        void DeleteFromCart(int cartId, int productId);
    }
}
