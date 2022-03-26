using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Entities
{
    [Table(name: "Authors")]
    public  class Authors
    {
        [Key]
        public int id { get; set; }
        public int idBook { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        
        [ForeignKey("idBook")]
        public virtual Books Book { get; set; }
    }
}
