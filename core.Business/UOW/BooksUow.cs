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
    public class BooksUow: IBooksUow
    {
        private CoreContext _context;
        private IConfiguration _conf;
        public BooksUow(CoreContext context, IConfiguration conf)
        {
            this._context = context;
            this._conf = conf;
        }
        IBooksRepository _IBooksRepository;
       public IBooksRepository Book
        { get {
                return _IBooksRepository ?? (_IBooksRepository = new BooksRepository(this._context));
            } 
        }

        

        public int CommitChanges()
        {
           return  this._context.SaveChanges();
        }

        public async Task<int> SyncBooks()
        {
            int afected = 0;
            ExternalData ExternalData = new ExternalData(_conf["EsternalApi:url"]);
            var books = await ExternalData.GetAllBooks();
            books.ForEach(ubook =>
            {
                var exist=this.Book.Get(ubook);
                if (exist == null)
                {
                    this.Book.Add(ubook);
                }
                else
                {
                    exist.description = ubook.description;
                    exist.excerpt = ubook.excerpt;
                    exist.pageCount = ubook.pageCount;
                    exist.publishDate = ubook.publishDate;
                    exist.title = ubook.title;
                    this.Book.Update(exist);
                }
                afected += this.CommitChanges();
            });

            //LIbros que estan en la base de datos y no estan en la api
            var bookInDB = this.Book.GetAll().ToList();
            bookInDB.ForEach(ub => {
               var libroExiste = books.Where(p => p.id == ub.id).FirstOrDefault();
                if(libroExiste == null)
                {
                    this.Book.Remove(ub);
                    afected += this.CommitChanges();
                }
            });

            return afected;
        }

     
    }
}
