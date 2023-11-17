using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;

namespace WukkamanCleaningAgencyApi.Models
{
    public class Shift
    {
        [Key]
        public int Id { get; set; }

        public bool MWOF { get; set; }

        public bool DCF { get; set; }

        public bool CGWD  { get; set; }

        public bool EWB { get; set; }

        public bool CB { get; set; }

        public bool RT { get; set; }

        public DateTime ShiftStart { get; set; }

        public DateTime ShiftEnd { get; set; }

        public int EmployeeId { get; set; }
        
        [ForeignKey("EmployeeId")]
        public virtual Employee? Employee { get; set; }
    }
}