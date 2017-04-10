using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Web.Datanomics.Models
{
    public class Article
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }

        public string Describe { get; set; }

        [AllowHtml]
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public int CategoryID { get; set; }
    }
}
