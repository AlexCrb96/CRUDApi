using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CRUDApi.Controllers;
using CRUDApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CRUDApi_UnitTests.Controllers_UnitTests
{
    public class AddressController_UnitTests
    {
        private static AddressController ArrangeForGetAllAddresses(List<Address> testAddresses)
        {
            var output = new AddressController();
            var addresses = testAddresses;

            var field = typeof(AddressController).GetField("_addresses", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            field.SetValue(null, (object)addresses);

            return output;
        }

        [Fact]
        public void GetAllAddresses_ReturnsOkResult_OnValidData()
        {
            // Arrange
            var testAddresses = new List<Address>
            {
                new Address { Id = 1, City = "Sibiu", Street = "Autogarii", Number = 10 },
                new Address { Id = 2, City = "Sibiu", Street = "Lemnelor", Number = 2 },
            };
            var controller = ArrangeForGetAllAddresses(testAddresses);

            // Act
            var result = controller.GetAllAddresses() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetAllAddresses_ReturnsCorrectNumberOfAddresses_OnValidData()
        {
            // Arrange
            var testAddresses = new List<Address>
            {
                new Address { Id = 1, City = "Sibiu", Street = "Autogarii", Number = 10 },
                new Address { Id = 2, City = "Sibiu", Street = "Lemnelor", Number = 2 },
            };
            var controller = ArrangeForGetAllAddresses(testAddresses);

            // Act
            var result = controller.GetAllAddresses() as OkObjectResult;
            var addresses = result.Value as List<Address>;

            // Assert
            Assert.Equal(2, addresses.Count);
        }

        [Fact]
        public void GetAllAddresses_ReturnsCorrectAddressDetails_OnValidData()
        {
            // Arrange
            var testAddresses = new List<Address>
            {
                new Address { Id = 1, City = "Sibiu", Street = "Autogarii", Number = 10 },
                new Address { Id = 2, City = "Sibiu", Street = "Lemnelor", Number = 2 },
            };
            var controller = ArrangeForGetAllAddresses(testAddresses);

            // Act
            var result = controller.GetAllAddresses() as OkObjectResult;
            var addresses = result.Value as List<Address>;

            // Assert
            Assert.Equal("Sibiu", addresses[0].City);
            Assert.Equal("Lemnelor", addresses[1].Street);
        }

        [Fact]
        public void GetAllAddresses_ReturnsNotFoundResult_OnNullData()
        {
            // Arrange
            List<Address> testAddresses = null;
            var controller = ArrangeForGetAllAddresses(testAddresses);

            // Act
            var result = controller.GetAllAddresses() as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void GetAllAddresses_ReturnsNotFoundResult_OnEmptyData()
        {
            // Arrange
            List<Address> testAddresses = new List<Address>();
            var controller = ArrangeForGetAllAddresses(testAddresses);

            // Act
            var result = controller.GetAllAddresses() as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }
    }
}
