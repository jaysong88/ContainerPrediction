using System;
using System.Collections.Generic;
using System.Text;
using ContainerPrediction.Enums;
using ContainerPrediction.Models;
using Xunit;

namespace ContainerPrediction.UnitTests
{
    public class NodeTreeTests
    {
        [Theory]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(8)]
        [InlineData(10)]
        public void TreeDepthShouldBeSameAsInputDepth(int depth)
        {
            NodeTree tree = new NodeTree();
            tree.BuildTree(depth);
            var node = tree.treeRoot;
            while (node.Right != null && node.Left != null)
            {
                if (node.NodeDirection == Direction.Left)
                {
                    node = node.Left;
                }
                else
                {
                    node = node.Right;
                }
            }

            Assert.Equal(depth, node.Depth);
            Assert.NotNull(node.LeftContainer);
            Assert.NotNull(node.RightContainer);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(9)]
        [InlineData(16)]
        [InlineData(12)]
        public void PredictResultShouldBeSameAsActualResult(int depth)
        {
            NodeTree tree = new NodeTree();
            tree.BuildTree(depth);
            var predictResult = tree.PredictEmptyContainerNumber();
            var actualResult = tree.GetEmptyContainerNumber();
            Assert.Equal(predictResult, actualResult);
        }
    }
}
