using Xunit;
using Node.Domain.AggregatesModel.NodeAggregates;
using Domain.Exceptions;

namespace Node.UnitTest.Domain
{
    public class NodeAggregateTesto
    {
        [Fact]
        public void Create_node_successfully()
        {
            // Arrange
            var name = "Start Browser";
            var description = "Process for starting the web browser";
            var code = "StartBrowser()";
            
            // Act
            var fakeNode = new Nodes(name, description, code);

            // Assert
            Assert.NotNull(fakeNode);
        }
        [Fact]
        public void Create_node_fail()
        {
            // Arrange
            var name = "";
            var description = "";
            var code = "teste";

            // Act - Assert
            Assert.Throws<DomainException>(() => new Nodes(name, description, code));
        }
    }
}
