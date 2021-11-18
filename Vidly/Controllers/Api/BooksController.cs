using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class BooksController : ApiController
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public BooksController()
        {
            _context = new ApplicationDbContext();
        }
        public BooksController(IMapper mapper)
        {
            _mapper = mapper;
        }
        public IEnumerable<BooksDto> GetBooks(string query = null)
        {
            var booksQuery = _context.Books
                .Include(m => m.Genre)
                .Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
                booksQuery = booksQuery.Where(m => m.Name.Contains(query));

            return booksQuery.Select(x => new BooksDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Genre = new GenreDto()
                    {
                        Id = x.GenreId,
                        Name = x.Genre.Name
                    }
                })
                .ToList();
        }

        public IHttpActionResult GetBook(int id)
        {
            var book = _context.Books.SingleOrDefault(c => c.Id == id);

            if (book == null)
                return NotFound();

            return Ok(_mapper.Map<Books, BooksDto>(book));
        }

        [HttpPost]
        //[Authorize(Roles = RoleName.CanManageBooks)]
        public IHttpActionResult CreateBook(BooksDto bookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var book = _mapper.Map<BooksDto, Books>(bookDto);
            _context.Books.Add(book);
            _context.SaveChanges();

            bookDto.Id = book.Id;
            return Created(new Uri(Request.RequestUri + "/" + book.Id), bookDto);
        }

        [HttpPut]
        //[Authorize(Roles = RoleName.CanManageBooks)]
        public IHttpActionResult UpdateBook(int id, BooksDto bookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var bookInDb = _context.Books.SingleOrDefault(c => c.Id == id);

            if (bookInDb == null)
                return NotFound();

            _mapper.Map(bookDto, bookInDb);

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        //[Authorize(Roles = RoleName.CanManageBooks)]
        public IHttpActionResult DeleteBook(int id)
        {
            var bookInDb = _context.Books.SingleOrDefault(c => c.Id == id);

            if (bookInDb == null)
                return NotFound();

            _context.Books.Remove(bookInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
