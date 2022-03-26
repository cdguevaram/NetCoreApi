using core.Business.IUOW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BooksAuthorsApiController : ControllerBase
    {
        IBooksUow booksUow;
        IAuthorsUow autorsUow;
        public BooksAuthorsApiController(IBooksUow booksuow, IAuthorsUow authorsuow)
        {
            this.booksUow = booksuow;
            this.autorsUow = authorsuow;
        }
        [HttpPost]
        [Route("SynckBooks")]
        public async Task<IActionResult> SyncBooks()
        {
            try
            {
                var LibrosAfectados = await this.booksUow.SyncBooks();
                return Ok(new { librosAfectados = LibrosAfectados });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("SyncAuthros")]
        public async Task<IActionResult> SyncAuthors()
        {
            try
            {
                var autoresAfectados = await this.autorsUow.SyncAuthors();
                return Ok(new { autoresAfectados = autoresAfectados });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        [Route("GetBooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = this.booksUow.Book.GetAll().ToList();
                return Ok(books);
            }catch (Exception ex)
            {
                return BadRequest(ex);
            }            
        }

        [HttpGet]
        [Route("GetBook")]
        public IActionResult GetAllBooks(int id)
        {
            try
            {
                var books = this.booksUow.Book.GetAll().Where(p=>p.id == id).Include(p => p.Authors).FirstOrDefault();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetAuthors")]
        public IActionResult GetAllAuthors()
        {
            try
            {
                var authors = this.autorsUow.Authors.GetAll().ToList();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetAuthor")]
        public IActionResult GetAllAuthors(int id)
        {
            try
            {
                var authors = this.autorsUow.Authors.GetAll().Where(p=>p.id == id).Include(p=>p.Book).FirstOrDefault();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        [Route("GetFilteredBooks")]
        public IActionResult FilterBook(int author, string fechaI, string fechaF)
        {
            try
            {
                DateTime fechai =  DateTime.ParseExact(fechaI, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime fechaf = DateTime.ParseExact(fechaF, "yyyy-MM-dd", CultureInfo.InvariantCulture);


                var books = this.booksUow.Book.GetAll().Include(p => p.Authors).AsQueryable();
                if (author != 0)
                {
                   
                    books = books.Where(p => p.Authors.Where(p => p.id == author).ToList().Count > 0);
                }

                var result = books.Where(p => p.publishDate.Value.Date >= fechai && p.publishDate.Value.Date <= fechaf).ToList();

                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
