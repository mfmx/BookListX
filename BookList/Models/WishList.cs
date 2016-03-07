using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookList.Models
{
    public class WishList
    {   

        public int WishListID { get; set; }
        public int BookReadID { get; set; }
        public string Id { get; set; }

        [Display(Name = "Date Entered ")]
        public DateTime BookDateEntered { get; set; }

        //[Display(Name = "Book's Name")]
        //public string  WishListBookName { get; set; }

        //[Display(Name = "Book Type")]
        //public string WishListBookAuthor { get; set; }

        //[Display(Name = "Book Type")]
        //public string WishListBookGenre { get; set;  }

       
    }
}