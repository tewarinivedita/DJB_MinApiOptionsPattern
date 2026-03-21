
namespace DJB_Core.Models
{
    public class PokeMonData
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public object Previous { get; set; }
        public Result[] Results { get; set; }
    }

    public class Result
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

}