using System;

namespace ContainerPrediction
{
    class Program
    {
        static void Main(string[] args)
        {
            NodeTree tree = new NodeTree();
            Console.WriteLine("Please type the depth");
            var input = Console.ReadLine();
            if (!int.TryParse(input, out int depth))
            {
                Console.WriteLine("Please type valid integer.");
            }
            else
            {
                Console.WriteLine("Start to build the tree....");
                tree.BuildTree(depth);
                var predictedContainerNumber = tree.PredictEmptyContainerNumber();
                Console.WriteLine("The predicted empty container should be number " + predictedContainerNumber + " container.");

                Console.WriteLine("Start to run all the balls....");
                var actualEmptyContainerNumber = tree.GetEmptyContainerNumber();
                Console.WriteLine("The actual empty container is number " + actualEmptyContainerNumber + " container.");
            }
        }
    }
}
