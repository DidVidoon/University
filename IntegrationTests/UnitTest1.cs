using Microsoft.AspNetCore.Mvc.Testing;
using Model;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xunit;

namespace IntegrationTests
{
    [TestClass]
    public class UnitTest1
        : IClassFixture<WebApplicationFactory<Presentation.Startup>>
    {
        private readonly HttpClient _client;

        public UnitTest1(WebApplicationFactory<Presentation.Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Index_WhenCalled_ReturnsApplicationForm()
        {
            var httpResponse = await _client.GetAsync("/Courses/Index");

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            StringAssert.Contains("Courses", stringResponse);
        }

        [Fact]
        public async Task DeleteCourseTest()
        {
            var response = await _client.DeleteAsync("Courses/Delete");
            Assert.IsFalse(response.StatusCode == HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Create_WhenCalled_ReturnsCreateForm()
        {
            var httpResponse = await _client.GetAsync("/Courses/Create");

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            StringAssert.Contains("Create", stringResponse);
        }

        [Fact]
        public async Task Create_SentModel_CreatesModels()
        {
            var newCourse = new Course()
            {
                CourseId = 1,
                Name = "Test Course",
                Description = "Testing"
            };

            var response = await _client.PostAsync("/Courses",
                           new StringContent(JsonConvert.SerializeObject(newCourse), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            Assert.IsFalse(response.StatusCode == HttpStatusCode.Created);
        }
    }
}