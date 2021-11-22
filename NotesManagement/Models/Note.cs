using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesManagement.Models
{
    [Serializable]
    class Note
    {
        private String moduleID;
        private String etudiantCNE;
        private float valeur;

        public String MODULEID { get => moduleID; set => moduleID = value; }
        public String ETUDIANTCNE { get => etudiantCNE; set => etudiantCNE = value; }
        public float VALEUR { get => valeur; set => valeur = value; }

        public float moyenne(DataRow dr)
        {
            try
            {
                float nPoo = 0;
                float.TryParse(dr["POO"].ToString(), out nPoo);

                float nOracle = 0;
                float.TryParse(dr["Oracle"].ToString(), out nOracle);

                float nDOTNET = 0;
                float.TryParse(dr["DOT NET"].ToString(), out nDOTNET);

                float nUml = 0;
                float.TryParse(dr["UML"].ToString(), out nUml);

                float nTec = 0;
                float.TryParse(dr["TEC"].ToString(), out nTec);

                float nWeb = 0;
                float.TryParse(dr["WEB"].ToString(), out nWeb);


                if (nPoo > -1 && nPoo <= 20 &&
                    nOracle > -1 && nOracle <= 20 &&
                    nDOTNET > -1 && nDOTNET <= 20 &&
                    nUml > -1 && nUml <= 20 &&
                    nTec > -1 && nTec <= 20 &&
                    nWeb > -1 && nWeb <= 20)
                    return (nPoo + nOracle + nDOTNET + nUml + nTec + nWeb) / 6;


                return 0;
            }
            catch(Exception e) {
                Console.WriteLine(e.GetType().Name);
                return 0;
            }

        }
    }
}
