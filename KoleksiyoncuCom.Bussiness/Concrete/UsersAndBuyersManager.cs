using KoleksiyoncuCom.Bussiness.Abstract;
using KoleksiyoncuCom.DataAccess.Abstract;
using KoleksiyoncuCom.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KoleksiyoncuCom.Bussiness.Concrete
{
    public class UsersAndBuyersManager : IUsersAndBuyersService
    {
        private IUsersAndBuyersDal _userAndBuyersDal;
        public UsersAndBuyersManager(IUsersAndBuyersDal userAndBuyersDal)
        {
            _userAndBuyersDal = userAndBuyersDal;
        }
        public void Add(UsersAndBuyers usersAndBuyers)
        {
            _userAndBuyersDal.Add(usersAndBuyers);
        }

        public void Delete(string userId)
        {
            _userAndBuyersDal.Delete(new UsersAndBuyers() { UserId = userId });
        }

        public UsersAndBuyers GetByBuyerId(int buyerId)
        {
            return _userAndBuyersDal.Get(b => b.BuyerId == buyerId);
        }

        public UsersAndBuyers GetByUserId(string userId)
        {
            return _userAndBuyersDal.Get(b => b.UserId == userId);
        }

        public void Update(UsersAndBuyers usersAndBuyers)
        {
            _userAndBuyersDal.Update(usersAndBuyers);
        }
    }
}
