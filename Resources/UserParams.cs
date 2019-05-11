namespace Spartan.Resources
{
    public class UserParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int pazeSize = 5;
        public int PazeSize
        {
            get { return pazeSize = 5; }
            set { pazeSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
    }
}