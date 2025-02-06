using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_THAMSO
    {
        private string mats;
        private string tents;
        private double giatri;

        public DTO_THAMSO()
        {
        }

        public DTO_THAMSO(string mats, string tents, double giatri)
        {
            this.Mats = mats;
            this.Tents = tents;
            this.Giatri = giatri;
        }

        public string Mats { get => mats; set => mats = value; }
        public string Tents { get => tents; set => tents = value; }
        public double Giatri { get => giatri; set => giatri = value; }
    }
}
