using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Controllers;
using MyApi.Models;
using System.Collections.Generic;
using System.Linq;
using WebApplication5.Controllers;
using WebApplication5.Models;
using Xunit;

namespace WebApplication5.UnitTests
{
    public class APITestClass
    {

        private readonly APIController controller;
        private readonly ApplicationDbContext context;


        public APITestClass()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer("DefaultConnection")
            .Options;
            context = new ApplicationDbContext(options);

            controller = new APIController(context);
        }


        [Fact]
        public async Task TestGet(int id)
        {

            // Act
            var result = controller.Get(8);


            // Assert
            


        }

    }
}
