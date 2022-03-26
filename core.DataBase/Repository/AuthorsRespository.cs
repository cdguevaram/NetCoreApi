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
    public class AuthorsRespository : Repository<Authors>, IAuthorsRepository
    {
        public AuthorsRespository(DbContext context)
            :base(context)
        {

        }
    }
}
