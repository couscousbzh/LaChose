using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaChose.models
{

    public class Atomes
    {
        public Atome[] AtomeList { get; set; }
    }

    public class Atome
    {
        public string id { get; set; }
        public string nom { get; set; }
        public string slug { get; set; }
        public string electron { get; set; }
        public string numero { get; set; }
        public string symbole { get; set; }
        public string info_groupe { get; set; }
        public string info_periode { get; set; }
        public string info_bloc { get; set; }
        public string masse_volumique { get; set; }
        public string cas { get; set; }
        public string einecs { get; set; }
        public string masse_atomique { get; set; }
        public string rayon_atomique { get; set; }
        public string rayon_de_covalence { get; set; }
        public string rayon_de_van_der_waals { get; set; }
        public string configuration_electronique { get; set; }
        public string etat_oxydation { get; set; }
        public string decouverte_annee { get; set; }
        public string decouverte_noms { get; set; }
        public string decouverte_pays { get; set; }
        public string electronegativite { get; set; }
        public string point_de_fusion { get; set; }
        public string point_d_ebullition { get; set; }
        public string is_radioactif { get; set; }
    }


}
