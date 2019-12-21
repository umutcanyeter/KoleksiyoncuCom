using KoleksiyoncuCom.Core.DataAccess.EntityFrameworkDataAccess;
using KoleksiyoncuCom.DataAccess.Abstract;
using KoleksiyoncuCom.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KoleksiyoncuCom.DataAccess.Concrete.EntityFramework
{
    public class EfBuyerDal : EfEntityRepositoryBase<Buyer, KoleksiyoncuComDbContext>, IBuyerDal
    {
    }
}
