using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema18.Models
{
    public class TechnicalBook
    {
        public int Id { get; set; }
        public int CylinderCapacity { get; set; }
        public int YearOfManufacture { get; set; }
        public string ChassisNumber { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
