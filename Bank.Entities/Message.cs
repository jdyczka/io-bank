namespace Bank.Entities
{
    public class Message
    {
        private static int _lastId = 0;

        public int Id { get; set; }
        public string Text { get; set; }

        public Message()
        {
            Id = _lastId;
            _lastId++;
        }
    }
}
