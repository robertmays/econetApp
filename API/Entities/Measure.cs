using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Measures")]
    public class Measure
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime EnquiryDate { get; set; }
        public int CustomerId { get; set; }
        public MeasureType MeasureType { get; set; }
        public int MeasureTypeId { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string MeasureAddedBy { get; set; }

    }
}