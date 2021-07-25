namespace ElectronicVoting.API.Consensus
{
    public class Block
    {
        public string Id { get; set; }
        public string Hash { get; set; }
        public string PreviousHash { get; set; }

        private Transaction Transaction { get; set; }
    }
}