
namespace Bank.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public Message()
        {
        }

        public Message( string text ) : this()
        {
            Text = text;
        }
    }
}
