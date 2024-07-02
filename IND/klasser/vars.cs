using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IND.klasser
{



    public class Personal
    {
        [Key]
        public int PersonID { get; set; } // PK
        public string Namn { get; set; }
        public string Befattning { get; set; }
        public DateTime AnstallningsDatum { get; set; }
        public virtual ICollection<Betyg> Betyg { get; set; }
        public virtual ICollection<Lon> Lons { get; set; } 
    }

    public class Elev
    {
        [Key]
        public int ElevID { get; set; } // PK
        public string Namn { get; set; }
        public string Klass { get; set; }
        public virtual ICollection<Betyg> Betyg { get; set; }
    }

    public class Kurs
    {
        [Key]
        public int KursID { get; set; } // PK
        public string KursNamn { get; set; }
        public bool Aktiv { get; set; }
        public virtual ICollection<Betyg> Betyg { get; set; }
    }

    public class Betyg
    {
        [Key]
        public int BetygID { get; set; } // PK
        public int ElevID { get; set; }
        public int KursID { get; set; }
        public int LarareID { get; set; }
        public string BetygValue { get; set; }
        public DateTime Datum { get; set; }

        public virtual Elev Elev { get; set; }
        public virtual Kurs Kurs { get; set; }
        public virtual Personal Larare { get; set; }
    }

    public class Lon
    {
        public int LonID { get; set; } //PK
        public int PersonID { get; set; } //PK2FK
        public decimal LonBelopp { get; set; } 
        public string Avdelning { get; set; } 

        public virtual Personal Personal { get; set; } 
    }
    public class ElevInfo
    {
        public int ElevID { get; set; }
        public string Namn { get; set; }
        public string Klass { get; set; }
        public string BetygValue { get; set; }
        public string KursNamn { get; set; }
        public string LarareNamn { get; set; }
        public decimal? LonBelopp { get; set; }  //nullable
    }



}
