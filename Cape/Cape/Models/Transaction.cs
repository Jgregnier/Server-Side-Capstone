using System.ComponentModel.DataAnnotations;

namespace Cape.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public double Amount { get; set; }

        [Required]
        public int ReportId {get;set;}

        public Report Report { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}