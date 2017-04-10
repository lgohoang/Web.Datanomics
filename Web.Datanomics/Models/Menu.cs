using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Web.Datanomics.Models
{
    public class Menu
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public bool isParent { get; set; }
        public bool isDropdown { get; set; }
        public bool isGroup { get; set; }

        [Display(Name = "Parent")]
        public int ParentID { get; set; }
        public int? Order { get; set; }
        public bool Visible { get; set; }
        public string Target { get; set; }
    }

    public class MenuGroup
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public int MenuID { get; set; }
        public int Order { get; set; }
        public bool Visible { get; set; }
    }
}
