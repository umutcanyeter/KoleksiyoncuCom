using KoleksiyoncuCom.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KoleksiyoncuCom.Bussiness.Abstract
{
    public interface IUsersAndBuyersService
    {
        UsersAndBuyers GetByBuyerId(int buyerId);
        UsersAndBuyers GetByUserId(string userId);
        void Add(UsersAndBuyers usersAndBuyers);
        void Update(UsersAndBuyers usersAndBuyers);
        void Delete(string userId);
    }
}
