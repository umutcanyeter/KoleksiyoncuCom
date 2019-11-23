using KoleksiyoncuCom.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KoleksiyoncuCom.Bussiness.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
    }
}
