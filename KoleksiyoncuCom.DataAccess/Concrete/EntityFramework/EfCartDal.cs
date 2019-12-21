using KoleksiyoncuCom.Core.DataAccess.EntityFrameworkDataAccess;
using KoleksiyoncuCom.DataAccess.Abstract;
using KoleksiyoncuCom.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KoleksiyoncuCom.DataAccess.Concrete.EntityFramework
{
    public class EfCartDal : EfEntityRepositoryBase<Cart, KoleksiyoncuComDbContext>, ICartDal
    {
        public override void Update(Cart entity)
        {
            using (var context = new KoleksiyoncuComDbContext())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }
        public Cart GetByUserId(string userId)
        {
            using (var context = new KoleksiyoncuComDbContext())
            {
                return context
                    .Carts
                    .Include(i => i.CartItems)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefault(i => i.UserId == userId);
            }
        }

        public void DeleteFromCart(int cartId, int productId)
        {
            using (var context = new KoleksiyoncuComDbContext())
            {
                var cmd = @"delete from CardItem where CartId=@p0 And ProductId=@p1";
                context.Database.ExecuteSqlCommand(cmd, cartId, productId);
            }
        }
    }
}
