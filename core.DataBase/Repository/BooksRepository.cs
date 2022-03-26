using core.DataBase.DataContracts;
using core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.DataBase.Repository
{
    public class BooksRepository : Repository<Books>, IBooksRepository
    {
        public BooksRepository(DbContext context)
            :base(context)
        {

        }
    }
}
