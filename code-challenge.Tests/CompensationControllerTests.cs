using challenge.Controllers;
using challenge.Data;
using challenge.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using code_challenge.Tests.Integration.Extensions;

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using code_challenge.Tests.Integration.Helpers;
using System.Text;


namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestServerStartup>()
                .UseEnvironment("Development"));

            _httpClient = _testServer.CreateClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            var employee = new Employee()
            {
                EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                FirstName = "John",
                LastName = "Lennon",
                Position = "Development Manager",
                Department = "Engineering"
            };

            var compensation = new Compensation()
            {
                EmployeeId = employee.EmployeeId,
                Salary = Convert.ToDouble(10000.00),
                EffectiveDate = Convert.ToDateTime("06-08-2021")
            };


            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(compensation.Salary, newCompensation.Salary);
            Assert.AreEqual(employee.EmployeeId, newCompensation.EmployeeId);
            Assert.AreEqual(compensation.EffectiveDate, newCompensation.EffectiveDate);
            Assert.IsNotNull(newCompensation.CompensationId);
        }

        [TestMethod]
        public void GetCompensationById_Returns_Ok()
        {
            // Arrange
            var employeeId = "c0c2293d-16bd-4603-8e08-638a9d18b22c";
            var expectedEffectiveDate = Convert.ToDateTime("2021-06-08T00:00:00");
            var expectedSalary = Convert.ToDecimal(30000.00);

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var compensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(expectedEffectiveDate, compensation.EffectiveDate);
            Assert.AreEqual(expectedSalary, compensation.Salary);
        }

    }
}
