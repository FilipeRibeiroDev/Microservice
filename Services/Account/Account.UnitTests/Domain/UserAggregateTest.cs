using Account.Domain.AggregatesModel.UserAggregates;
using Account.Domain.Exceptions;
using System;
using Xunit;

namespace Account.UnitTests.Domain
{
    public class UserAggregateTest
    {
        [Fact]
        public void Create_user_is_success()
        {
            // Arrange
            var name = "Filipe Ribeiro";
            var email = "ribeirop12369@hotmail.com";
            var password = "Teste@2021";

            // Act
            var FakeUser = new User(name, email, password);

            // Assert
            Assert.NotNull(FakeUser);
        }
        [Fact]
        public void Craete_user_is_fail()
        {
            // Arrange
            var name = "Filipe Ribeiro";
            var email = "ribeiro12369hotmail.com";
            var password = "Teste@2021";

            // Act - Assert
            Assert.Throws<DomainException>(() => new User(name, email, password));
        }

      

    }
}
