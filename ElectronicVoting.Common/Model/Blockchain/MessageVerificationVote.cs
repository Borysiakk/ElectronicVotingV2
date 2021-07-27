namespace ElectronicVoting.Common.Model.Blockchain
{
    public class MessageVerificationVote
    {
        public string Id { get; set; }
        public string From { get; set; }
        public string TransactionId { get; set; }
        public bool IsVerificationVote { get; set; }
    }
}