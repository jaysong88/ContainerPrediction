using ContainerPrediction.Enums;
using ContainerPrediction.Models;
using System;
using Xunit;

namespace ContainerPrediction.UnitTests
{
    public class NodeTests
    {
        [Fact]
        public void ChangeDirectionShouldUpdateDirectionSuccessfully()
        {
            Node node = new Node(5);
            if(node.NodeDirection == Direction.Left)
            {
                node.ChangeDirection();
                Assert.Equal(Direction.Right, node.NodeDirection);
            }
            else
            {
                node.ChangeDirection();
                Assert.Equal(Direction.Left, node.NodeDirection);
            }
        }
    }
}
