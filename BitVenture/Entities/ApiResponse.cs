namespace BitVenture.Entities
{
    public class ApiResponse
    {
        public string name { get; set; }
        public string height { get; set; }
        public string title { get; set; }
        public string director { get; set; }
        public Content contents { get; set; }
    }

    public class Content
    {
        public string translation { get; set; }
    }
}

