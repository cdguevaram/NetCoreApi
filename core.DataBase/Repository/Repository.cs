using core.DataBase.DataContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.DataBase.Repository
{
    public class Repository<Entity> : IRepository<Entity> where Entity : class
    {
        private DbContext m_dbContext;
        private DbSet<Entity> m_dbSet;

        public Repository(DbContext dbContext)
        {
            m_dbContext = dbContext;

            m_dbSet = m_dbContext.Set<Entity>();
        }

        protected DbContext DbContext
        {
            get
            {
                return m_dbContext;
            }
        }

        protected DbSet<Entity> DbSet
        {
            get
            {
                return m_dbSet;
            }
        }

        public virtual IQueryable<Entity> GetAll()
        {
            return m_dbSet;
        }

        public virtual Entity Get(Entity e)
        {
           return m_dbSet.Where(p => p.Equals(e)).FirstOrDefault();
        }

        public virtual void Add(Entity e)
        {
            m_dbSet.Add(e);
        }

        public virtual void Update(Entity e)
        {
            m_dbSet.Update(e); 
        }

        public virtual void Remove(Entity e)
        {
            m_dbSet.Remove(e);
        }
    }
}