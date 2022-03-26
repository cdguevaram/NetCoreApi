using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.DataBase.DataContracts
{
    public interface IRepository<Entity> where Entity : class
    {
        IQueryable<Entity> GetAll();

        Entity Get(Entity e);

        void Add(Entity e);

        void Update(Entity e);

        void Remove(Entity e);
    }
}
