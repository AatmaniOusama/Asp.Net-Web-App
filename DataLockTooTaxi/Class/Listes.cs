using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLockTooTaxi.MyDataSetTableAdapters;

namespace DataLockTooTaxi
{
    public class Listes
    {
        public int Num_liste { get; set; }
        public string Libelle { get; set; }
        public string Abrev { get; set; }
        public string Type { get; set; }


        public Listes()
        {
            Num_liste = -1;
            Libelle = " Tous ";
            Abrev = "";
            Type = "";

        }


        public List<Listes> GetListes()
        {
            ListesTableAdapter ListesAdapter = new ListesTableAdapter();
            MyDataSet.ListesDataTable TablesListes = ListesAdapter.GetListes();

            List<Listes> listListes = new List<Listes>();
            

            foreach (MyDataSet.ListesRow row in TablesListes)
            {
                Listes item = new Listes();

                item.Num_liste = row.Num;

                if (!row.IsLibelleNull())
                item.Libelle = row.Libelle;

                if (!row.IsAbrevNull())
                item.Abrev = row.Abrev;

                if (!row.IsTypeNull())
                {
                    switch (row.Type)
                    {
                        case 1:
                            item.Type = "Chauffeurs";
                            break;
                        case 2:
                            item.Type = "Opérateurs";
                            break;
                        case 4:
                            item.Type = "Agents";
                            break;
                        case 8:
                            item.Type = "Chauffeurs demandés";
                            break;
                    }
                }

                listListes.Add(item);
            }

            return listListes;

        }

        public Listes getListeByNum(int Num)
        {
            ListesTableAdapter ListesAdapter = new ListesTableAdapter();
            MyDataSet.ListesDataTable TablesListes = ListesAdapter.GetListeByNum(Num);


            Listes item = new Listes();

            foreach (MyDataSet.ListesRow row in TablesListes)
            {
              

                item.Num_liste = row.Num;

                if (!row.IsLibelleNull())
                item.Libelle = row.Libelle;

                if (!row.IsAbrevNull())
                item.Abrev = row.Abrev;

                if (!row.IsTypeNull())
                {
                    switch (row.Type)
                    {
                        case 1:
                            item.Type = "Chauffeurs";
                            break;
                        case 2:
                            item.Type = "Opérateurs";
                            break;
                        case 4:
                            item.Type = "Agents";
                            break;
                        case 8:
                            item.Type = "Chauffeurs demandés";
                            break;
                    }
                }
            
            }

            return item;

        }


        public List<Listes> getListesByType(int Type) 
        {
            ListesTableAdapter ListesAdapter = new ListesTableAdapter();
            MyDataSet.ListesDataTable TablesListes = ListesAdapter.GetListesByType(Type);
            List<Listes> ListeListes = new List<Listes>();

          
            ListeListes.Add(new Listes());

            foreach (MyDataSet.ListesRow row in TablesListes)
            {
                Listes item = new Listes();

                item.Num_liste = row.Num;
               
                
                if (!row.IsLibelleNull())
                item.Libelle = row.Libelle;

                if (!row.IsAbrevNull())
                item.Abrev = row.Abrev;

                if (!row.IsTypeNull())
                {

                    switch (row.Type)
                    {
                        case 1:
                            item.Type = "Chauffeurs";
                            break;
                        case 2:
                            item.Type = "Opérateurs";
                            break;
                        case 4:
                            item.Type = "Agents";
                            break;
                        case 8:
                            item.Type = "Chauffeurs demandés";
                            break;
                    }
                }
                ListeListes.Add(item);
            }

          
            return ListeListes;

        }
        /************************************************/
        /*         savoir si une Liste existe       */
        /************************************************/

        public bool ListeExiste(string Libelle, string Abrev)
        {
            ListesTableAdapter ListesAdapter = new ListesTableAdapter();

            if (ListesAdapter.ListeExiste(Libelle,Abrev).GetHashCode() == 1)
                return true;
            else
                return false;

        }


        /************************************************/
        /*        Ajouter une Liste        */
        /************************************************/
        public int AjouterUneListe(Listes item)
        {
            ListesTableAdapter ListesAdapter = new ListesTableAdapter();
            return ListesAdapter.InsertListe(item.Libelle,item.Abrev,int.Parse(item.Type));
        }



        /************************************************/
        /*        Supprimer une Liste        */
        /************************************************/
        public int SupprimerUneListe(int Num)
        { 
            ListesTableAdapter ListesAdapter = new ListesTableAdapter();
            return ListesAdapter.DeleteListe(Num);
        }
        /************************************************/
        /*            Modifier Une Liste           */
        /************************************************/

        public int UpdateListe(Listes item)
        {
            ListesTableAdapter ListesAdapter = new ListesTableAdapter();


            switch (item.Type)
            {
                case "Chauffeurs":
                    item.Type = "1";
                    break;
                case "Opérateurs":
                    item.Type = "2";
                    break;
                case "Agents":
                    item.Type = "4";
                    break;
                case "Chauffeurs demandés":
                    item.Type = "8";
                    break;
            }
            return ListesAdapter.UpdateListe(item.Abrev,item.Libelle,int.Parse(item.Type),item.Num_liste);
             
           
        }

        /************************************************/
        /*        Max NumListe dans la table Liste        */
        /************************************************/
        public int MaxNumListe()
        {
            ListesTableAdapter ListesAdapter = new ListesTableAdapter();
            try
            {
                return ListesAdapter.GetMaxNumListe().Value;
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }

}

