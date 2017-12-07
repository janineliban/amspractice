using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.EnterpriseServices;
using System.Linq;
using System.Web;

namespace AssetManagementSystem.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Product")]
        public string Product { get; set; }

        [Required]
        [Display(Name="Serial No.")]
        public string SerialNo { get; set; }

        [Required]
        [Display(Name="Color")]
        public string Color { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName => Product + ": " + SerialNo;

        public ICollection<Transaction> Transactions { get; set; }

    }
}