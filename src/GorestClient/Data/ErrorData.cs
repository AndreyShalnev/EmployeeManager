namespace GorestClient.Data
{
    public class ErrorData
    {
        public string Field { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Field))
                return $"{Field}-{Message}";

            return Message;
        }
    }
}
