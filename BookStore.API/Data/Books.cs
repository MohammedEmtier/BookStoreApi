using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Data
{
   //[Table("Bookss",Schema ="book")]
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        //[Column("fkg"),]
        //[Column(TypeName ="varchar(200)")]
       // [MaxLength(200)][MinLength(1)]
       //[Comment("the var is desc book")]
        public string Description { get; set; }
        //[ForeignKey("autherforignkey")]
        public Auther auther { get; set; }
        // [NotMapped]
    }
}
