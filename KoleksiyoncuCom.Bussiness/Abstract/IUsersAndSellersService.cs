using KoleksiyoncuCom.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace KoleksiyoncuCom.Bussiness.Abstract
{
    public interface IUsersAndSellersService
    {
        UsersAndSellers GetBySellerId(int sellerId);
        UsersAndSellers GetByUserId(string userId);
        void Add(UsersAndSellers usersAndSellers);
        void Update(UsersAndSellers usersAndSellers);
        void Delete(string userId);
    }
}
