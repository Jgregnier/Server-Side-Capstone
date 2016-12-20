using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Cape.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }

        public ICollection<Transaction> Transactions;

        public ApplicationUser User;
    }
}