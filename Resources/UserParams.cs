namespace Spartan.Resources
{
    public class UserParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pazeSize = 5;
        public int PazeSize
        {
            get { return _pazeSize = 5; }
            set { _pazeSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public int UserId { get; set; }
        public string Gender { get; set; }
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 99;
        public string OrderBy { get; set; }
    }
}