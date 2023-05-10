using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        //KONSTRUKTOR
        public APITestClass(ApplicationDbContext context)
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer("DefaultConnection")
            .Options;

            context = new ApplicationDbContext(options);

            controller = new APIController(context);
        }


        [Fact]
        public void TestGet()
        {
            // Arrange
            var expectedOrganization = new Organization { OrganizationId = 8, OrganizationName = "Ekebygymnaiset" };

            // Act
            var result = controller.Get(8) as JsonResult;
            var actualOrganization = result.Value as Organization;

            // Assert
            Assert.Equal(expectedOrganization.OrganizationId, actualOrganization.OrganizationId);
            Assert.Equal(expectedOrganization.OrganizationName, actualOrganization.OrganizationName);
        }

    }
}
