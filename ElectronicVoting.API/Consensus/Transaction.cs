namespace ElectronicVoting.API.Consensus
{
    public class Transaction
    {
        public string Id { get; set; }
        public string Value { get; set; }
        
        public string From { get; set; }
        public string To { get; set; }
    }
}