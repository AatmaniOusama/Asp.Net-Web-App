using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLockTooTaxi.MyDataSetTableAdapters;

namespace DataLockTooTaxi
{
    public class Lecteur
    {
        public string NomLecteur {get;set;}
        public int Adresse { get; set; }

        public Lecteur()
        {
            NomLecteur = "Tous";
            Adresse = -1;
        }

        public List<Lecteur> GetLecteurs()
        {
            LecteursTableAdapter LecteurAdapter = new  LecteursTableAdapter();
            MyDataSet.LecteursDataTable TableLecteurs = LecteurAdapter.GetLecteurs();
            List<Lecteur> ListLecteurs = new List<Lecteur>();
            ListLecteurs.Add(new Lecteur());

            foreach (MyDataSet.LecteursRow row in TableLecteurs)
            {
                Lecteur item = new Lecteur();
                
                item.Adresse = row.Adresse;

                if (!row.IsNomLecteurNull())
                item.NomLecteur = row.NomLecteur;

                ListLecteurs.Add(item);
            }
            return ListLecteurs;
        }
    }
}
