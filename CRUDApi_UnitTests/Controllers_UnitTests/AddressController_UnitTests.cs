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
using Microsoft.AspNetCore.Http;

namespace CRUDApi_UnitTests.Controllers_UnitTests
{
    public class AddressController_UnitTests
    {
        private static AddressController ArrangeListOfAddreses(List<Address> testAddresses)
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
            var controller = ArrangeListOfAddreses(testAddresses);

            // Act
            var result = controller.GetAllAddresses() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
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
            var controller = ArrangeListOfAddreses(testAddresses);

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
            var controller = ArrangeListOfAddreses(testAddresses);

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
            var controller = ArrangeListOfAddreses(testAddresses);

            // Act
            var result = controller.GetAllAddresses() as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Equal("No addresses found.", result.Value);
        }

        [Fact]
        public void GetAllAddresses_ReturnsNotFoundResult_OnEmptyData()
        {
            // Arrange
            List<Address> testAddresses = new List<Address>();
            var controller = ArrangeListOfAddreses(testAddresses);

            // Act
            var result = controller.GetAllAddresses() as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Equal("No addresses found.", result.Value);
        }

        [Fact]
        public void GetAddressById_ReturnsOkResult_OnValidData()
        {
            // Arrange
            var testAddresses = new List<Address>
            {
                new Address { Id = 1, City = "Sibiu", Street = "Autogarii", Number = 10 },
                new Address { Id = 2, City = "Sibiu", Street = "Lemnelor", Number = 2 },
            };
            var controller = ArrangeListOfAddreses(testAddresses);

            //Act
            var result = controller.GetAddressById(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public void GetAddressById_ReturnsCorrectAddressDetails_OnValidData()
        {
            // Arrange
            var testAddresses = new List<Address>
            {
                new Address { Id = 1, City = "Sibiu", Street = "Autogarii", Number = 10 },
                new Address { Id = 2, City = "Sibiu", Street = "Lemnelor", Number = 2 },
            };
            var controller = ArrangeListOfAddreses(testAddresses);

            // Act
            var result = controller.GetAddressById(2) as OkObjectResult;
            var address = result.Value as Address;

            // Assert
            Assert.Equal(2, address.Id);
            Assert.Equal("Sibiu", address.City);
            Assert.Equal("Lemnelor", address.Street);
        }

        [Fact]
        public void GetAddressById_ReturnsNotFound_OnInvalidId()
        {
            // Arrange
            var testAddresses = new List<Address>
            {
                new Address { Id = 1, City = "Sibiu", Street = "Autogarii", Number = 10 },
                new Address { Id = 2, City = "Sibiu", Street = "Lemnelor", Number = 2 },
            };
            var controller = ArrangeListOfAddreses(testAddresses);

            // Act
            var result = controller.GetAddressById(3) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Equal("Address with ID 3 does not exist.", result.Value);
        }

        [Fact]
        public void CreateAddress_ReturnsCreatedResult_OnValidData()
        {
            // Arrange
            var controller = ArrangeListOfAddreses(new List<Address>());
            Address testAddress = new Address
            {
                Id = 1,
                City = "Sibiu",
                Street = "Gladiolelor",
                Number = 10,
            };

            // Act
            var result = controller.CreateAddress(testAddress) as CreatedAtActionResult;
            var outputValue = result.Value as Address;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            Assert.Equal(testAddress.City, outputValue.City);
        }

        [Fact]
        public void CreateAddress_ReturnsConflict_OnExistingId()
        {
            // Arrange
            var testAddresses = new List<Address>()
            {
                new Address { Id = 1, City = "Sibiu", Street = "Autogarii", Number = 10 },
                new Address { Id = 2, City = "Sibiu", Street = "Lemnelor", Number = 2 },
            };
            var controller = ArrangeListOfAddreses(testAddresses);
            Address testExistingAddress = new Address
            {
                Id = 1,
                City = "Sibiu",
                Street = "Gladiolelor",
                Number = 10,
            };

            // Act
            var result = controller.CreateAddress(testExistingAddress) as ConflictObjectResult;
            var outputValue = result.Value;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status409Conflict, result.StatusCode);
            Assert.Equal("Address with ID 1 already exists.", outputValue);
        }

        [Fact]
        public void CreateAddress_ReturnsBadRequest_OnInvalidData()
        {
            // Arrange
            Address testAddress = new Address
            {
                Id = 1,
                City = "",
                Street = "Dumbravii",
                Number = 10
            };
            var controller = ArrangeListOfAddreses(new List<Address>());
            controller.ModelState.AddModelError("City", "City is required.");

            // Act
            var result = controller.CreateAddress(testAddress) as BadRequestObjectResult;
            var outputValue = result.Value;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal("Address data is invalid.", outputValue);
        }
    }
}
