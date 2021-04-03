using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TechLibrary.Domain;
using TechLibrary.Models;
using TechLibrary.Services;
using TechLibrary.Contracts.Requests;
using System;

namespace TechLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(ILogger<BooksController> logger, IBookService bookService, IMapper mapper)
        {
            _logger = logger;
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PageQuery pageQuery)
        {
            _logger.LogInformation("Get all books");

            // Going to pass the queries to the book service
            var pagefilter = _mapper.Map<PageFilter>(pageQuery);
            var books = await _bookService.GetBooksAsync(pagefilter);

            var bookResponse = _mapper.Map<List<BookResponse>>(books);


            return Ok(bookResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"Get book by id {id}");

            var book = await _bookService.GetBookByIdAsync(id);

            //var bookResponse = new ApiResponse<BookResponse>(_mapper.Map<BookResponse>(book));
            var bookResponse = _mapper.Map<BookResponse>(book);

            return Ok(bookResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookResponse bookRequest)
        {
            try
            {
                //string dateInput = "Jan 1, 2009";
                //var parsedDate = DateTime.Parse(dateInput);
                if (!string.IsNullOrEmpty(bookRequest.PublishedDate))
                    bookRequest.PublishedDate = DateTime.Parse(bookRequest.PublishedDate).ToString();
                var mapBookRequest = _mapper.Map<Book>(bookRequest);
                await _bookService.CreateNewBookAsync(mapBookRequest);
            }
            catch(Exception e)
            {
                throw e;
            }
            

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";

            return Created(baseUrl, bookRequest);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditBook(int id, [FromBody] BookResponse bookRequest)
        {
            try {
                bookRequest.BookId = id;

                var mapBookRequest = _mapper.Map<Book>(bookRequest);

                await _bookService.UpdateBookAsync(mapBookRequest);
            }
            catch (Exception e)
            {
                throw e;
            }
            

            return Ok(bookRequest);
        }

    }
}
