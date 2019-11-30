using KoleksiyoncuCom.Core.DataAccess.EntityFrameworkDataAccess;
using KoleksiyoncuCom.DataAccess.Abstract;
using KoleksiyoncuCom.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace KoleksiyoncuCom.DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, KoleksiyoncuComDbContext>, IProductDal
    {
        
    }
}
