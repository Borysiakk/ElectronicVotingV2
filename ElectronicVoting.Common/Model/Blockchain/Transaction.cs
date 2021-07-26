namespace ElectronicVoting.Common.Model.Blockchain
{
    public class Transaction
    {
        public string Id { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Voice { get; set; }
    }
}