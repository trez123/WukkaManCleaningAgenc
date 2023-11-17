using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;

namespace WukkamanCleaningAgencyFrontend.Models
{
    public class Shift
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("MOP & WIPE OFFICE FLOOR")]
        public bool MWOF { get; set; }

        [DisplayName("DUST & CLEAN FURNITURE")]
        public bool DCF { get; set; }

        [DisplayName("CLEAN GLASS WINDOWS & DOORS")]
        public bool CGWD  { get; set; }

        [DisplayName("EMPTY WASTE BINS")]
        public bool EWB { get; set; }

        [DisplayName("CLEAN BATHROOMS")]
        public bool CB { get; set; }

        [DisplayName("RESTOCK TOILETRIES")]
        public bool RT { get; set; }

        [DisplayName("SHIFT START")]
        [DisplayFormat(DataFormatString = "{0:MMM-dd-yy}")]
        public DateTime ShiftStart { get; set; }

        [DisplayName("SHIFT END")]
        [DisplayFormat(DataFormatString = "{0:MMM-dd-yy}")]
        public DateTime ShiftEnd { get; set; }
        public int EmployeeId { get; set; }
        
        [ForeignKey("EmployeeId")]
        public virtual Employee? Employee { get; set; }
    }
}