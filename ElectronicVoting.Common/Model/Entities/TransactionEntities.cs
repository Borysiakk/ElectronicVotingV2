using System.ComponentModel.DataAnnotations;

namespace ElectronicVoting.Common.Model.Entities
{
    public class TransactionEntities
    {
        [Key]
        public string Id { get; set; }
        public string From { get; set; }
        public bool SuperValidator { get; set; }
        public string TransactionId { get; set; }
    }
}