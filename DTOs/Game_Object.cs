using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class Game_Object
    {
        public string name;
        private int speed;
        public double health { get; set; }
        public DateTime birthday { get; }
        public List<Game_Object> items { get; set; }

    }
}
