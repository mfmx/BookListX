﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookList.Models
{
    public class DisplayBook
    {
        
        public string Name { get; set; }


      
        public string Author { get; set; }

      
        public string Genre { get; set; }

       
        public DateTime DateEntered { get; set; }

        public int BookReadID { get; set; }
     
    }
}