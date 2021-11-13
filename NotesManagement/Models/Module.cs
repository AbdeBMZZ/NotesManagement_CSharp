using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesManagement.Models
{
    class Module
    {
        private String id;
        private String nom;

        public Module(String id, String nom)
        {
            this.id = id;
            this.nom = nom;
        }
        public String ID { get => id; set => id = value; }

        public String NOM { get => nom; set => nom = value; }
    }
}
