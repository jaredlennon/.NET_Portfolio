using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.Models
{
    public class Car
    {
        public int id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public float safetyRating { get; set; }
        public int modelYear { get; set; }
    }
}
