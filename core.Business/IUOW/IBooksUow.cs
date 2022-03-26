using core.DataBase.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Business.IUOW
{
  public  interface IBooksUow
    {
        IBooksRepository Book { get; }
        Task<int> SyncBooks();
        Int32 CommitChanges();
    }
}
