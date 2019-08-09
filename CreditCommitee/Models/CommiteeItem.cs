using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Commitee.Models
{
    public class CommiteeItem
    {

        public enum ItemStatus
        {
            [Display(Name = "NOT_STARTED")]
            NOT_STARTED = 0,
            
            [Display(Name = "STARTED")]
            STARTED = 1,

            [Display(Name = "ENDED")]
            ENDED = 2
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [DisplayName("Task Name")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(450)]
        [Required]
        [Index("IX_ItemName", 2, IsUnique = true)]
        public String itemName { get; set; }

        [DisplayName("End Date")]
        //[Column(TypeName = "datetime2")]
        [DataType(DataType.Date)]
        public DateTime? itemEndDate { get; set; }

        [DisplayName("Start Date")]
        //[Column(TypeName = "datetime2")]
        [DataType(DataType.Date)]
        public DateTime? itemStartDate { get; set; }

        [DisplayName("Task Order")]
        public int itemOrder { get; set; }

        [DefaultValue(ItemStatus.NOT_STARTED)]
        [DisplayName("Task Status")]
        public ItemStatus itemStatus { get; set; }

        [DisplayName("Programme")]
        [Required]
        [Index("IX_ItemName", 1, IsUnique = true)]
        public int programmeId { get; set; }

        [ForeignKey("programmeId")]
        public virtual CommiteeProgramme commiteeProgramme { get; set; }

        [DisplayName("Created By")]
        public string createdBy { get; set; }

        [DisplayName("Create Date")]
        [Column(TypeName = "datetime2")]
        public DateTime createDate { get; set; }
    }
}