using core.Api.Model;
using core.Business.Helpers;
using core.Business.IUOW;
using core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ValuesController : ControllerBase
    {
        IBooksUow _books;
        public ValuesController(IBooksUow booksuow)
        {
            this._books = booksuow;
        }
        [HttpGet]
        public  Task<List<Books>> GetAllBooks()
        {
            ExternalData requests = new ExternalData("https://fakerestapi.azurewebsites.net/");
            return requests.GetAllBooks();
        }
    }
}
