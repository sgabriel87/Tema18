using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Tema18.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public int ManufacturerId { get; set; }
        public List<Key> Keys { get; set; } = new List<Key>();
        public TechnicalBook TechnicalBook { get; set; }
    }
}
