using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLockTooTaxi.MyDataSetTableAdapters;
namespace DataLockTooTaxi
{
    public class AutorisationQPU
    {
        public int Id { get; set; }
        public DateTime DateCreation { get; set; }
        public int IdOpCreation { get; set; }
        public DateTime DateModif { get; set; }
        public int IdOpModif { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string MatriculeUser { get; set; }
        public string NomUser { get; set; }
        public string PrenomUser { get; set; }
        public string NomComplet { get; set; }
        public string Valide { get; set; }
        public string NumAuto { get; set; }
        public string NumAgrement { get; set; }
        public string Destination { get; set; }
        public int IdAgrement { get; set; }
        public string NomPassager1 { get; set; }
        public string PrenomPassager1 { get; set; }
        public string CinPassager1 { get; set; }
        public string NomPassager2 { get; set; }
        public string PrenomPassager2 { get; set; }
        public string CinPassager2 { get; set; }
        public int  NumListe { get; set; }

        public AutorisationQPU()
     {
        Id =-1;
        DateCreation= DateTime.Now;
        IdOpCreation =1;
        DateModif =DateTime.Now;
        IdOpModif = 1;
        DateDebut =DateTime.Now;
        DateFin =DateTime.Now;
        MatriculeUser = "";
        NomUser = "";
        PrenomUser = "";
        Valide = "True";
        NumAuto = "";
        NumAgrement = "";
        Destination = "";
        IdAgrement = -1;
        NomPassager1 = "";
        PrenomPassager1 = "";
        CinPassager1 = "";
        NomPassager2 = "";
        PrenomPassager2 = "";
        CinPassager2 = "";
        NumListe = -1; 
    }

        public List<AutorisationQPU>  getAutorisationQPU()
        {
            AutoQpuTableAdapter AutoQPUAdapter = new AutoQpuTableAdapter();
            MyDataSet.AutoQpuDataTable TableAutorisationQPU = AutoQPUAdapter.GetAutorisationQPU();
            List<AutorisationQPU> ListAutorisationQPU = new List<AutorisationQPU>();

            

            foreach (MyDataSet.AutoQpuRow row in TableAutorisationQPU)
            {
                AutorisationQPU item = new AutorisationQPU();
                
                
                if(!row.IsNumAutoNull())
                item.NumAuto = row.NumAuto;

                if (!row.IsNumAgrementNull())
                item.NumAgrement = row.NumAgrement;

                if (!row.IsDestinationNull())
                item.Destination = row.Destination;

                if (!row.IsMatriculeUserNull())
                item.MatriculeUser = row.MatriculeUser;

                if (!row.IsNomUserNull())
                item.NomUser = row.NomUser;

                if (!row.IsPrenomUserNull())
                item.PrenomUser = row.PrenomUser;

                if (!row.IsNomUserNull())
                item.NomComplet = item.NomUser = row.NomUser;              
                item.NomComplet += " ";

                if (!row.IsPrenomUserNull())
                item.NomComplet += item.PrenomUser = row.PrenomUser;

                if (!row.IsDateDebutNull())
                item.DateDebut = row.DateDebut;

                if (!row.IsDateFinNull())
                item.DateFin = row.DateFin;

                if (!row.IsValideNull())
                {

                    if(row.Valide == true)
                    {
                        item.Valide = "Y";
                    }
                    if(row.Valide == false)
                    {
                        item.Valide = "N";
                    }

                }
                if (!row.IsDateCreationNull())
                item.DateCreation = row.DateCreation;

                if (!row.IsDateModifNull())
                item.DateModif = row.DateModif;

                if (!row.IsNomPassager1Null())
                item.NomPassager1 = row.NomPassager1;

                if (!row.IsPrenomPassager1Null())
                item.PrenomPassager1 = row.PrenomPassager1;

                if (!row.IsCinPassager1Null())
                item.CinPassager1 = row.CinPassager1;

                if (!row.IsNomPassager2Null())
                item.NomPassager2 = row.NomPassager2;

                if (!row.IsPrenomPassager2Null())
                item.PrenomPassager2 = row.PrenomPassager2;

                if (!row.IsCinPassager2Null())
                item.CinPassager2 = row.CinPassager2;
                

                ListAutorisationQPU.Add(item);
                
            }

            return ListAutorisationQPU;


        }

        // Get Autorisation en tenant compte du chox du filtre (-->numListe)
        public List<AutorisationQPU> getAutorisationQPU_NumListe()
        {
            AutoQpuTableAdapter AutoQPUAdapter = new AutoQpuTableAdapter();
            MyDataSet.AutoQpuDataTable TableAutorisationQPU = AutoQPUAdapter.GetAutorisationQPU_NumListe();
            List<AutorisationQPU> ListAutorisationQPU = new List<AutorisationQPU>();



            foreach (MyDataSet.AutoQpuRow row in TableAutorisationQPU)
            {
                AutorisationQPU item = new AutorisationQPU();

                if (!row.IsNumAutoNull())
                    item.NumAuto = row.NumAuto;

                if (!row.IsNumAgrementNull())
                    item.NumAgrement = row.NumAgrement;

                if (!row.IsDestinationNull())
                    item.Destination = row.Destination;

                if (!row.IsMatriculeUserNull())
                    item.MatriculeUser = row.MatriculeUser;

                if (!row.IsNomUserNull())
                    item.NomUser = row.NomUser;

                if (!row.IsPrenomUserNull())
                    item.PrenomUser = row.PrenomUser;

                if (!row.IsNomUserNull())
                    item.NomComplet = item.NomUser = row.NomUser;
                item.NomComplet += " ";

                if (!row.IsPrenomUserNull())
                    item.NomComplet += item.PrenomUser = row.PrenomUser;

                if (!row.IsDateDebutNull())
                    item.DateDebut = row.DateDebut;

                if (!row.IsDateFinNull())
                    item.DateFin = row.DateFin;

                if (!row.IsValideNull())
                {

                    if (row.Valide == true)
                    {
                        item.Valide = "Y";
                    }
                    if (row.Valide == false)
                    {
                        item.Valide = "N";
                    }

                }
                if (!row.IsDateCreationNull())
                    item.DateCreation = row.DateCreation;

                if (!row.IsDateModifNull())
                    item.DateModif = row.DateModif;

                if (!row.IsNomPassager1Null())
                    item.NomPassager1 = row.NomPassager1;

                if (!row.IsPrenomPassager1Null())
                    item.PrenomPassager1 = row.PrenomPassager1;

                if (!row.IsCinPassager1Null())
                    item.CinPassager1 = row.CinPassager1;

                if (!row.IsNomPassager2Null())
                    item.NomPassager2 = row.NomPassager2;

                if (!row.IsPrenomPassager2Null())
                    item.PrenomPassager2 = row.PrenomPassager2;

                if (!row.IsCinPassager2Null())
                    item.CinPassager2 = row.CinPassager2;

                if (!row.IsNumListeNull())
                item.NumListe = row.NumListe;

                ListAutorisationQPU.Add(item);

            }

            return ListAutorisationQPU;


        }
        public AutorisationQPU getAutorisationQPUByNum(string NumAuto)
        {
            AutoQpuTableAdapter AutoQPUAdapter = new AutoQpuTableAdapter();
            MyDataSet.AutoQpuDataTable TableAutorisationQPU = AutoQPUAdapter.GetAutorisationQPUByNum(NumAuto);

            AutorisationQPU item = new AutorisationQPU();


            foreach (MyDataSet.AutoQpuRow row in TableAutorisationQPU)
            {


                if (!row.IsNumAutoNull())
                    item.NumAuto = row.NumAuto;

                if (!row.IsNumAgrementNull())
                    item.NumAgrement = row.NumAgrement;

                if (!row.IsDestinationNull())
                    item.Destination = row.Destination;

                if (!row.IsMatriculeUserNull())
                    item.MatriculeUser = row.MatriculeUser;

                if (!row.IsNomUserNull())
                    item.NomUser = row.NomUser;

                if (!row.IsPrenomUserNull())
                    item.PrenomUser = row.PrenomUser;

                if (!row.IsNomUserNull())
                    item.NomComplet = item.NomUser = row.NomUser;
                item.NomComplet += " ";

                if (!row.IsPrenomUserNull())
                    item.NomComplet += item.PrenomUser = row.PrenomUser;

                if (!row.IsDateDebutNull())
                    item.DateDebut = row.DateDebut;

                if (!row.IsDateFinNull())
                    item.DateFin = row.DateFin;

                if (!row.IsValideNull())
                {

                    if (row.Valide == true)
                    {
                        item.Valide = "Y";
                    }
                    if (row.Valide == false)
                    {
                        item.Valide = "N";
                    }

                }
                if (!row.IsDateCreationNull())
                    item.DateCreation = row.DateCreation;

                if (!row.IsDateModifNull())
                    item.DateModif = row.DateModif;

                if (!row.IsNomPassager1Null())
                    item.NomPassager1 = row.NomPassager1;

                if (!row.IsPrenomPassager1Null())
                    item.PrenomPassager1 = row.PrenomPassager1;

                if (!row.IsCinPassager1Null())
                    item.CinPassager1 = row.CinPassager1;

                if (!row.IsNomPassager2Null())
                    item.NomPassager2 = row.NomPassager2;

                if (!row.IsPrenomPassager2Null())
                    item.PrenomPassager2 = row.PrenomPassager2;

                if (!row.IsCinPassager2Null())
                    item.CinPassager2 = row.CinPassager2;
                

                
            }

            return item;


        }

        public int insertAutoQPU(AutorisationQPU item)
        {           
            AutoQpuTableAdapter AutoQPUAdapter = new AutoQpuTableAdapter();

            return AutoQPUAdapter.InsertAutoQPU(item.DateCreation, item.IdOpCreation, item.DateModif, item.IdOpModif, item.DateDebut, item.DateFin, item.MatriculeUser,bool.Parse(item.Valide), item.NumAuto, item.NumAgrement, item.Destination,null,item.NomPassager1, item.PrenomPassager1, item.CinPassager1, item.NomPassager2, item.PrenomPassager2, item.CinPassager2);
        }


        /************************************************/
        /*   Modifier Validité  d'Une Autorisation      */
        /************************************************/

        public int ModifierValidite(bool Valide, string Matricule)
        {
            AutoQpuTableAdapter AutoQPUAdapter = new AutoQpuTableAdapter();
            return AutoQPUAdapter.SetValidite(Valide, Matricule);
        }
    }
     
}
