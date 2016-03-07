using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookList.Models
{
    public class PersonBook
    {
        public int PersonBookID { get; set; }

        public string BookReadReview { get; set; }
        public int BookReadID { get; set; }
        //public int WishListID { get; set; }
        public string Id { get; set; }

        [Display(Name = "Date Entered ")]
        public DateTime BookDateEntered { get; set; }
    }
}