using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core.Entities
{
    [Table(name: "Books")]
    public class Books
    {
        [Key]
        public int id { get; set; }
        public String title { get; set; }
        public String description  { get; set; }
        public int pageCount { get; set; }
        public string excerpt { get; set; }
        public DateTime? publishDate { get; set; }

        public virtual ICollection<Authors> Authors { get; set; }
    }
}
