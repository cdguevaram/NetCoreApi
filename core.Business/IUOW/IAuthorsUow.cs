using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.DataBase.DataContracts;
namespace core.Business.IUOW
{
   public interface IAuthorsUow
    {
        IAuthorsRepository Authors { get; }
        Task<int> SyncAuthors();
        Int32 CommitChanges();
    }
}
