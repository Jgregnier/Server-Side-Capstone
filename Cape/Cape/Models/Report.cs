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

        public List<Transaction> Transactions { get; set; }

        public string UserId { get; set; }

    }
}