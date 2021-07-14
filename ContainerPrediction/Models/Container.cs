namespace ContainerPrediction.Models
{
    public class Container
    {
        public int Number { get; set; }

        public bool IsEmpty { get; set; }

        public Container(int number)
        {
            Number = number;
            IsEmpty = true;
        }
    }
}
