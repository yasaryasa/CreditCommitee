using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Commitee.Models
{
    public class CommiteeProgramme
    {
        public enum CommiteeProgrammeStatus
        {
            [Display(Name = "ACTIVE")]
            ACTIVE = 1,

            [Display(Name = "PASSIVE")]
            PASSIVE = 0
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [DisplayName("Programme Name")]
        [Required]
        //[Key]
        public string programmeName { get; set; }

        [DisplayName("Programme Date")]
        [DataType(DataType.Date)]
        public DateTime programmeDate { get; set; }

        [DisplayName("End Date")]
        [Column(TypeName = "datetime2")]
        public DateTime? programmeEndDate { get; set; }

        [DefaultValue(CommiteeProgrammeStatus.ACTIVE)]
        [DisplayName("Programme Status")]
        public CommiteeProgrammeStatus programmeStatus { get; set; }

        [DisplayName("Created By")]
        public string createdBy { get; set; }

        [DisplayName("Create Date")]
        [Column(TypeName = "datetime2")]
        public DateTime createDate { get; set; }

        public virtual List<CommiteeItem> commiteeItems { get; set; }
    }
}