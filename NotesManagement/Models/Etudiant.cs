using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesManagement.Models
{
    [Serializable]
    class Etudiant
    {
        private String nom;
        private String cne;
        private String sexe;
        public String NOM { get => nom; set => nom = value; }
        public String CNE { get => cne; set => cne = value; }
        public String SEXE { get => sexe; set => sexe = value; }

    }
}
