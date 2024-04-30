using AutoFixture;
using GeoApi.Domain.Utils;

namespace GeoApi.Tests.Domain.Utils
{
    public class JsonHandlerTest
    {
        private readonly Fixture _factory = new();

        public class TestPerson
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        [Fact]
        public void DeserializeJson_ValidJsonReturnsExpectedObject()
        {
            // Arrange
            var json = "{ \"Name\": \"Joao Teste\", \"Age\": 30 }";
            var expectedName = "Joao Teste";
            var expectedAge = 30;

            // Act
            var result = JsonHandler.DeserializeJson<TestPerson>(json);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedName, result.Value.Name);
            Assert.Equal(expectedAge, result.Value.Age);
        }

        [Fact]
        public void DeserializeJson_InvalidJson_ThrowsErrorException()
        {
            // Arrange
            var invalidJson = "{ \"Name\": \"Joao Teste\", \"Age\": \"invalid\" }";

            // Act
            var result = JsonHandler.DeserializeJson<TestPerson>(invalidJson);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Equal("A external request result does not comply with the expected format or schema",
                result.Errors[0].Message);
        }
    }
}
