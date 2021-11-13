using NotesManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotesManagement
{
    public partial class Form1 : Form
    {
        DataSerializer dataSerializer = new DataSerializer();
        String filePath = "etudiants.save";
        String filePath1 = "notes.save";


        DataTable dt = new DataTable("table");

        public Form1()
        {
            Module m1 = new Module("POO", "POO");
            Module m2 = new Module("Oracle", "Oracle");
            Module m3 = new Module("DOT NET", "DOT NET");
            Module m4 = new Module("UML", "UML");
            Module m5 = new Module("TEC", "TEC");
            Module m6 = new Module("WEB", "WEB");

            data.modules.Add(m1);
            data.modules.Add(m2);
            data.modules.Add(m3);
            data.modules.Add(m4);
            data.modules.Add(m5);
            data.modules.Add(m6);


            InitializeComponent();


        }

        private void add_click_Click(object sender, EventArgs e)
        {
            Etudiant etudiant = new Etudiant();
            etudiant.CNE = textBox1.Text;
            etudiant.NOM = textBox2.Text;
            etudiant.SEXE = (radioButton1.Checked ? radioButton1.Text : radioButton2.Text);


            if (!data.etudiants.Contains(etudiant))
            {
                data.etudiants.Add(etudiant);

                if (dt.Rows.Contains(etudiant.CNE))
                    MessageBox.Show("etudiant deja ajoute");

                else
                {
                    Button moy = new Button();
                    moy.Text = "Calculer";
                    dt.Rows.Add(etudiant.NOM, etudiant.CNE, "", "", "", "", "", "", "_______");

                }
            }



            comboBox1.DataSource = data.etudiants.Select(i => i.CNE).Distinct().ToList();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("Full Name", Type.GetType("System.String"));
            dt.Columns.Add("CNE", Type.GetType("System.String"));
            dt.Columns.Add("POO", Type.GetType("System.String"));
            dt.Columns.Add("Oracle", Type.GetType("System.String"));
            dt.Columns.Add("DOT NET", Type.GetType("System.String"));
            dt.Columns.Add("UML", Type.GetType("System.String"));
            dt.Columns.Add("TEC", Type.GetType("System.String"));
            dt.Columns.Add("WEB", Type.GetType("System.String"));
            dt.Columns.Add("Moyenne", Type.GetType("System.String"));

            DataColumn[] keyColumns = new DataColumn[1];
            keyColumns[0] = dt.Columns["CNE"];

            dt.PrimaryKey = keyColumns;
            dataGridView1.DataSource = dt;

            data.etudiants = (List<Etudiant>)dataSerializer.binaryDeserialize(filePath);
            data.notes = (List<Note>)dataSerializer.binaryDeserialize(filePath1);

            comboBox1.DataSource = data.etudiants.Select(i => i.CNE).Distinct().ToList();

            foreach(Etudiant etudiant in data.etudiants)
            {
                if (!dt.Rows.Contains(etudiant.CNE))
                    dt.Rows.Add(etudiant.NOM, etudiant.CNE, "", "", "", "", "", "", null);
            }


            foreach (DataRow dr in dt.Rows)
            {
                foreach (Note n in data.notes)
                    if (n.ETUDIANTCNE == dr["CNE"].ToString())
                    {

                        switch (n.MODULEID)
                        {
                            case "POO":
                                dr["POO"] = n.VALEUR;
                                break;

                            case "Oracle":
                                dr["Oracle"] = n.VALEUR;
                                break;

                            case "DOT NET":
                                dr["DOT NET"] = n.VALEUR;
                                break;

                            case "UML":
                                dr["UML"] = n.VALEUR;
                                break;

                            case "TEC":
                                dr["TEC"] = n.VALEUR;
                                break;

                            case "WEB":
                                dr["WEB"] = n.VALEUR;
                                break;

                        }
                        try
                        {

                            if (checkRow(dr))
                                dr["Moyenne"] = n.moyenne(dr);
                        }
                        catch (Exception xp)
                        {

                        }

                    }


            }


        }

        public bool checkRow(DataRow dr)
        {
            float nPoo = float.Parse(dr["POO"].ToString());
            float nOracle = float.Parse(dr["Oracle"].ToString());
            float nDOTNET = float.Parse(dr["DOT NET"].ToString());
            float nUml = float.Parse(dr["UML"].ToString());
            float nTec = float.Parse(dr["TEC"].ToString());
            float nWeb = float.Parse(dr["WEB"].ToString());


            if (!String.IsNullOrEmpty(dr["POO"].ToString()) &&
                !String.IsNullOrEmpty(dr["Oracle"].ToString()) &&
                !String.IsNullOrEmpty(dr["DOT NET"].ToString()) &&
                !String.IsNullOrEmpty(dr["UML"].ToString()) &&
                !String.IsNullOrEmpty(dr["TEC"].ToString()) &&
                !String.IsNullOrEmpty(dr["WEB"].ToString())
                )
                return true;
            return false;
        }

        public void changeColor()
        {
            foreach(DataRow dr in dt.Rows)
            {
                float m = 0;
                float.TryParse(dr["Moyenne"].ToString(), out m);

                if(m>= 10)
                {
                    dr["Moyenne"] += " V";
                }

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1 && !String.IsNullOrEmpty(textBox3.Text) && !String.IsNullOrEmpty(listBox1.SelectedItem.ToString()))
            {
                String cne = comboBox1.SelectedItem.ToString();
                String module = listBox1.SelectedItem.ToString();
                float val = float.Parse(textBox3.Text);

                Note note = new Note();
                note.ETUDIANTCNE = cne;
                note.MODULEID = module;
                note.VALEUR = val;

                if (!data.notes.Contains(note))
                    data.notes.Add(note);

                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["CNE"].ToString() == note.ETUDIANTCNE)
                        {
                            switch (note.MODULEID)
                            {
                                case "POO":
                                    dr["POO"] = note.VALEUR;
                                    break;

                                case "Oracle":
                                    dr["Oracle"] = note.VALEUR;
                                    break;

                                case "DOT NET":
                                    dr["DOT NET"] = note.VALEUR;
                                    break;

                                case "UML":
                                    dr["UML"] = note.VALEUR;
                                    break;

                                case "TEC":
                                    dr["TEC"] = note.VALEUR;
                                    break;

                                case "WEB":
                                    dr["WEB"] = note.VALEUR;
                                    break;
                            }
                            if (checkRow(dr))
                                dr["Moyenne"] = note.moyenne(dr);
                        }


                    }
                    dataSerializer.binarySerialize(data.etudiants, filePath);
                    dataSerializer.binarySerialize(data.notes, filePath1);
                }
                catch (Exception exception) { 
                    
                }
                

            }

        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataSerializer.binarySerialize(data.etudiants, filePath);
            dataSerializer.binarySerialize(data.notes, filePath1);

            if (MessageBox.Show("Do you want to exit? ", "Management app", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if(e.ColumnIndex == 8 )
            {
                float val = 0;

                float.TryParse(e.Value.ToString(), out val);
                if (val >= 10)
                    e.CellStyle.BackColor = Color.LightGreen;
                else if (val < 10 && val > 6)
                    e.CellStyle.BackColor = Color.Gray;

                else if(val <6 && val >0)
                    e.CellStyle.BackColor = Color.Red;
            }
        }
    }
    static class data
    {

        public static List<Etudiant> etudiants = new List<Etudiant>();
        public static List<Module> modules = new List<Module>();
        public static List<Note> notes = new List<Note>();
    }

    class DataSerializer{
        public void binarySerialize(Object data, String filePath)
        {

            FileStream fileStream;
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(filePath)) File.Delete(filePath);
            fileStream = File.Create(filePath);
            bf.Serialize(fileStream, data);
            fileStream.Close();
        }
        
        public Object binaryDeserialize(String filePath)
        {
            Object obj = null;

            FileStream fileStream;
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(filePath))
            {
                fileStream = File.OpenRead(filePath);
                obj = bf.Deserialize(fileStream);
                fileStream.Close();
            }
            return obj;
        }
    }
}


