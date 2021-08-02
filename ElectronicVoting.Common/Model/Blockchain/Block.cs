using System.Collections.Generic;

namespace ElectronicVoting.Common.Model.Blockchain
{
    public class Block
    {
        public string Hash { get; set; }
        public string PreviousHash { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}