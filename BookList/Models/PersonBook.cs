using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookList.Models
{
    public class PersonBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonBookID { get; set; }

      

        
        public int BookReadID { get; set; }
      
        public string Id { get; set; }

        [Display(Name = "Date Entered ")]
        public DateTime BookDateEntered { get; set; }
    }
}