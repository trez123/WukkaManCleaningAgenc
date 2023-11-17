using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;

namespace WukkamanCleaningAgencyFrontend.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string Code { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string Phone { get; set; }
    }
}
