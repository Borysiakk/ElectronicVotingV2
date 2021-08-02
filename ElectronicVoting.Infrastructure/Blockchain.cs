using System.Collections.Generic;
using ElectronicVoting.Common.Model.Blockchain;

namespace ElectronicVoting.Infrastructure
{
    public class Blockchain
    {
        private Block Currently { get; set; }
        private List<Block> Blocks { get; set; }
    }
}