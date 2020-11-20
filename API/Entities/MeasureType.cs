using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("MeasureTypes")]
    public class MeasureType
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string Code { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}