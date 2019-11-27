using KoleksiyoncuCom.Bussiness.Abstract;
using KoleksiyoncuCom.DataAccess.Abstract;
using KoleksiyoncuCom.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace KoleksiyoncuCom.Bussiness.Concrete
{
    public class UsersAndSellersManager : IUsersAndSellersService
    {
        private IUsersAndSellersDal _usersAndSellersDal;
        public UsersAndSellersManager(IUsersAndSellersDal usersAndSellersDal)
        {
            _usersAndSellersDal = usersAndSellersDal;
        }
        public void Add(UsersAndSellers usersAndSellers)
        {
            _usersAndSellersDal.Add(usersAndSellers);
        }

        public void Delete(string userId)
        {
            _usersAndSellersDal.Delete(new UsersAndSellers { UserId = userId });
        }

        public UsersAndSellers GetBySellerId(int sellerId)
        {
            return _usersAndSellersDal.Get(s => s.SellerId == sellerId);
        }

        public UsersAndSellers GetByUserId(string userId)
        {
            return _usersAndSellersDal.Get(s => s.UserId == userId);
        }

        public void Update(UsersAndSellers usersAndSellers)
        {
            _usersAndSellersDal.Update(usersAndSellers);
        }
    }
}
