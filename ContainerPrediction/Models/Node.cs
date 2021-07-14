using ContainerPrediction.Enums;
using ContainerPrediction.Extensions;
using ContainerPrediction.Interfaces;

namespace ContainerPrediction.Models
{
    public class Node : INode
    {
        public Node Right { get; set; }
        public Node Left { get; set; }

        public Direction NodeDirection { get; set; }

        public int Depth { get; set; }

        public Container LeftContainer { get; set; }

        public Container RightContainer { get; set; }

        public Node(int depth)
        {
            Depth = depth;
            NodeDirection = (Direction)typeof(Direction).GetRandomEnumValue();
        }

        public void ChangeDirection()
        {
            if (NodeDirection == Direction.Left)
                NodeDirection = Direction.Right;
            else
                NodeDirection = Direction.Left;
        }
    }
}
