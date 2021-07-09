namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_Information
    {
        public int id { get; set; }

        public int id_User { get; set; }

        [StringLength(50)]
        public string User_address { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_Of_Birth { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [Required]
        [StringLength(200)]
        public string imgUrl { get; set; }

        public virtual User User { get; set; }
    }
}
