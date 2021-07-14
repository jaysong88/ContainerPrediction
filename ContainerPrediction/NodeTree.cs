using System;
using System.Collections.Generic;
using System.Linq;
using ContainerPrediction.Enums;
using ContainerPrediction.Models;

namespace ContainerPrediction
{
    public class NodeTree
    {
        public Node treeRoot;

        private int treeDepth;

        private List<Container> Containers = new List<Container>();

        public void BuildTree(int depth)
        {
            int currentDepth = 1;
            int count = 0;
            treeRoot = new Node(currentDepth);
            treeDepth = depth;

            if (depth == 1)
            {
                treeRoot.LeftContainer = new Container(++count);
                treeRoot.RightContainer = new Container(++count);
                Containers.Add(treeRoot.LeftContainer);
                Containers.Add(treeRoot.RightContainer);
                return;
            }

            // Queue is FIFO, we can follow the order to add child nodes to parent nodes
            var queue = new Queue<Node>();
            queue.Enqueue(treeRoot);

            while (currentDepth < depth)
            {
                var tempNode = queue.Dequeue();
                currentDepth = tempNode.Depth;
                if (currentDepth < depth)
                {
                    tempNode.Left = new Node(currentDepth + 1);
                    tempNode.Right = new Node(currentDepth + 1);
                    if(currentDepth == depth - 1)
                    {
                        // For the lowest depth node, we will attach containers to both directions to serve balls
                        tempNode.Left.LeftContainer = new Container(++count);
                        tempNode.Left.RightContainer = new Container(++count);
                        tempNode.Right.LeftContainer = new Container(++count);
                        tempNode.Right.RightContainer = new Container(++count);

                        // Adding these containers into collection, which will be easier to check empty container
                        Containers.Add(tempNode.Left.LeftContainer);
                        Containers.Add(tempNode.Left.RightContainer);
                        Containers.Add(tempNode.Right.LeftContainer);
                        Containers.Add(tempNode.Right.RightContainer);
                    }
                    queue.Enqueue(tempNode.Left);
                    queue.Enqueue(tempNode.Right);
                }
            }
            queue.Clear();
        }

        public int PredictEmptyContainerNumber()
        {          
            // The logic to predict is simple: 
            // if we have balls one less than containers, each gate will decide which direction will miss a ball
            // for example, if we have 15 balls in total, the inital gate on top is left, it means that we will have 8 balls go to left, but 7 go to right
            // in this case, we just need to follow the opposite gate till the end to find which container will miss the ball
            Node node = treeRoot;

            while (node.Right != null && node.Left != null)
            {
                if (node.NodeDirection == Direction.Left)
                    node = node.Right;
                else
                    node = node.Left;
            }

            if (node.NodeDirection == Direction.Left)
                return node.RightContainer.Number;
            else
                return node.LeftContainer.Number;
        }

        public int GetEmptyContainerNumber()
        {
            int numberOfBalls = (int)Math.Pow(2, treeDepth) - 1;
            for(int i=0; i < numberOfBalls; i++)
            {
                var node = treeRoot;
                while (node.Right != null && node.Left != null)
                {
                    if (node.NodeDirection == Direction.Left)
                    {
                        node.ChangeDirection();
                        node = node.Left;
                    }
                    else
                    {
                        node.ChangeDirection();
                        node = node.Right;
                    }
                }

                if (node.NodeDirection == Direction.Left)
                    node.LeftContainer.IsEmpty = false;
                else
                    node.RightContainer.IsEmpty = false;

                node.ChangeDirection();
            }

            var container = Containers.Where(o => o.IsEmpty == true).FirstOrDefault();

            if (container != null)
                return container.Number;
            else
                return -1;
        }
    }
}
