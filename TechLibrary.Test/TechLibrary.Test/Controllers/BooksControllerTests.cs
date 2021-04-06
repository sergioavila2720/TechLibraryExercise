using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechLibrary.Contracts.Requests;
using TechLibrary.Domain;
using TechLibrary.Services;

namespace TechLibrary.Controllers.Tests
{
    [TestFixture()]
    [Category("ControllerTests")]
    public class BooksControllerTests
    {

        private  Mock<ILogger<BooksController>> _mockLogger;
        private  Mock<IBookService> _mockBookService;
        private  Mock<IMapper> _mockMapper;
        private NullReferenceException _expectedException;

        [OneTimeSetUp]
        public void TestSetup()
        {
            _expectedException = new NullReferenceException("Test Failed...");
            _mockLogger = new Mock<ILogger<BooksController>>();
            _mockBookService = new Mock<IBookService>();
            _mockMapper = new Mock<IMapper>();
        }

        [Test()]
        public async Task GetAllTest()
        {
            var pagefilter = new PageFilter();
            //  Arrange
            _mockBookService.Setup(b => b.GetBooksAsync(null)).Returns(Task.FromResult(It.IsAny<List<Domain.Book>>()));
            var sut = new BooksController(_mockLogger.Object, _mockBookService.Object, _mockMapper.Object);
            var pageQuery = new PageQuery();
            //  Act
            var result = await sut.GetAll(pageQuery);

            //  Assert
            _mockBookService.Verify(s => s.GetBooksAsync(null), Times.Once, "Expected GetBooksAsync to have been called once");
        }
    }
}