using core.Business.Helpers;
using core.Business.IUOW;
using core.DataBase;
using core.DataBase.DataContracts;
using core.DataBase.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Business.UOW
{
    public class AuthorsUow : IAuthorsUow
    {
        IAuthorsRepository _IAuthorsRepository;
        private CoreContext _context;
        private IConfiguration _conf;
        public AuthorsUow(CoreContext context, IConfiguration conf)
        {
            this._context = context;
            this._conf = conf;
        }
        public IAuthorsRepository Authors
        {
            get
            {
                return _IAuthorsRepository ?? (_IAuthorsRepository = new AuthorsRespository(this._context));
            }
        }

        public int CommitChanges()
        {
            return this._context.SaveChanges();
        }

        public async Task<int> SyncAuthors()
        {
            int afected = 0;
            ExternalData ExternalData = new ExternalData(_conf["EsternalApi:url"]);
            var books = await ExternalData.GetAllAuthors();
            books.ForEach(ubook =>
            {
                var exist = this.Authors.Get(ubook);
                if (exist == null)
                {
                    this.Authors.Add(ubook);
                }
                else
                {
                    exist.firstName = ubook.firstName;
                    exist.idBook = ubook.idBook;
                    exist.lastName= ubook.lastName;
                    
                    this.Authors.Update(exist);
                }
                afected += this.CommitChanges();
            });

            //Autores que estan en la base de datos y no estan en la api son eliminados
            var bookInDB = this.Authors.GetAll().ToList();
            bookInDB.ForEach(ub => {
                var libroExiste = books.Where(p => p.id == ub.id).FirstOrDefault();
                if (libroExiste == null)
                {
                    this.Authors.Remove(ub);
                    afected += this.CommitChanges();
                }
            });

            return afected;
        }
    }
}
