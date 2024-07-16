using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema18.Models
{
    public class Key
    {
        public int Id { get; set; }
        public Guid AccessCode { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
