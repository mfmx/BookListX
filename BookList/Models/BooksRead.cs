using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookList.Models
{
    public class BooksRead
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "#")]
        public int BookReadID { get; set; }

        [Display(Name="Book's Name")]
        public string BookReadName { get; set; }


        [Display(Name = "Book's Author")]
        public string BookReadAuthor { get; set; }

        [Display(Name = "Book's Genres ")]
        public string BookReadGenre { get; set; }

        //[Display(Name = "Enter Date ")]
        //public DateTime BookEnteredDate { get; set; }



    }
}