using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicVoting.API.Model.Entities
{
    [Table("TransactionHistory")]
    public class TransactionEntities
    {
        [Key]
        public string Id { get; set; }
        public string From { get; set; }
    }
}