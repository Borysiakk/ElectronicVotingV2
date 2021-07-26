using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicVoting.Common.Model.Entities
{
    [Table("InitialTransaction")]
    public class InitialTransactionEntities
    {
        [Key]
        public string Id { get; set; }
        public string Validator { get; set; }
    }
}