using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLockTooTaxi.MyDataSetTableAdapters;

namespace DataLockTooTaxi
{
    public class Controles
    {
        public int Id { get; set; }
        public DateTime DateCreation { get; set; }
        public int IdOpCreation { get; set; }
        public DateTime DateModif { get; set; }
        public int IdOpModif { get; set; }
        public string DateFin { get; set; }
        public int IdLabelControle { get; set; }
        public string LibelleControle { get; set; }
        public int IdVehicule { get; set; }
        public string Immat { get; set; }
        public string NumTaxi { get; set; }
        public string TypeTaxi { get; set; }

        public Controles()
        {
        Id = -1;
        DateCreation =DateTime.Now;
        IdOpCreation = -1;
        DateModif =DateTime.Now;
        IdOpModif = -1;
        DateFin = DateTime.Now.ToString().Substring(0,10);
        IdLabelControle = -1;
        LibelleControle ="";
        IdVehicule =-1;
        Immat ="";
        NumTaxi ="";
        TypeTaxi = "";

        }

        public List<Controles> getControles()
        {
            ControlesTableAdapter ControlesAdapter = new ControlesTableAdapter();
            List<Controles> ListControles = new List<Controles>();

            foreach (MyDataSet.ControlesRow row in ControlesAdapter.GetControles())
            {
                Controles item = new Controles();

                item.Id = row.Id;

                if (!row.IsDateCreationNull())
                item.DateCreation = row.DateCreation;

                if (!row.IsIdOpCreationNull())
                item.IdOpCreation = row.IdOpCreation;

                if (!row.IsDateModifNull())
                item.DateModif = row.DateModif;

                if (!row.IsIdOpModifNull())
                item.IdOpModif = row.IdOpModif;

                if (!row.IsDateFinNull())
                item.DateFin = row.DateFin.Date.ToString("dd/MM/yyy HH:mm:ss");

                if (!row.IsIdLabelControleNull())
                item.IdLabelControle = row.IdLabelControle;

                if (!row.IsLibelleControleNull())
                item.LibelleControle = row.LibelleControle;

                if (!row.IsIdVehiculeNull())
                item.IdVehicule = row.IdVehicule;

                if (!row.IsPlaqueNull())
                item.Immat = row.Plaque;

                if (!row.IsNumTaxiNull())
                item.NumTaxi = row.NumTaxi;

                if (!row.IsTypeTaxiNull())
                item.TypeTaxi = row.TypeTaxi;

                ListControles.Add(item);

            }

            return ListControles;
        }


        /*******************************************************/
        /*               la liste des Controles Taxis          */
        /*******************************************************/

        public List<Controles> getAllControlesVehicule(int IndexPage, int? ddlAbrevControlesVh, DateTime? dateFin, string numTaxi, int? ddltypeTaxi, string immatriculation)
        {
            
            
            ControlesTableAdapter ControlesAdapter = new ControlesTableAdapter();
            MyDataSet.ControlesDataTable TableControlesVh = ControlesAdapter.GetAllControlesVehicule(IndexPage, ddlAbrevControlesVh, dateFin, numTaxi, ddltypeTaxi, immatriculation);
            List<Controles> ListControles = new List<Controles>();

            foreach (MyDataSet.ControlesRow row in TableControlesVh)
            {
                Controles item = new Controles();

                item.Id = row.Id;

                if (!row.IsDateCreationNull())
                    item.DateCreation = row.DateCreation;

                if (!row.IsIdOpCreationNull())
                    item.IdOpCreation = row.IdOpCreation;

                if (!row.IsDateModifNull())
                    item.DateModif = row.DateModif;

                if (!row.IsIdOpModifNull())
                    item.IdOpModif = row.IdOpModif;

                if (!row.IsDateFinNull())
                    item.DateFin = row.DateFin.Date.ToString("dd/MM/yyy HH:mm:ss");

                if (!row.IsIdLabelControleNull())
                    item.IdLabelControle = row.IdLabelControle;

                if (!row.IsLibelleControleNull())
                    item.LibelleControle = row.LibelleControle;

                if (!row.IsIdVehiculeNull())
                    item.IdVehicule = row.IdVehicule;

                if (!row.IsPlaqueNull())
                    item.Immat = row.Plaque;

                if (!row.IsNumTaxiNull())
                    item.NumTaxi = row.NumTaxi;

                if (!row.IsTypeTaxiNull())
                    item.TypeTaxi = row.TypeTaxi;

                ListControles.Add(item);

            }

            return ListControles;
        }

        /**********************************************************************************/
        /*               la liste des Controles Taxis avec filtre (pour le trie)          */
        /**********************************************************************************/

        public List<Controles> getAllControlesVehiculeByFiltre( int? ddlAbrevControlesVh, DateTime? dateFin, string numTaxi, int? ddltypeTaxi, string immatriculation)
        {


            ControlesTableAdapter ControlesAdapter = new ControlesTableAdapter();
            MyDataSet.ControlesDataTable TableControlesVh = ControlesAdapter.GetAllControlesVehiculeByFiltre(ddlAbrevControlesVh, dateFin, numTaxi, ddltypeTaxi, immatriculation);
            List<Controles> ListControles = new List<Controles>();

            foreach (MyDataSet.ControlesRow row in TableControlesVh)
            {
                Controles item = new Controles();

                item.Id = row.Id;

                if (!row.IsDateCreationNull())
                    item.DateCreation = row.DateCreation;

                if (!row.IsIdOpCreationNull())
                    item.IdOpCreation = row.IdOpCreation;

                if (!row.IsDateModifNull())
                    item.DateModif = row.DateModif;

                if (!row.IsIdOpModifNull())
                    item.IdOpModif = row.IdOpModif;

                if (!row.IsDateFinNull())
                    item.DateFin = row.DateFin.Date.ToString("dd/MM/yyy HH:mm:ss");

                if (!row.IsIdLabelControleNull())
                    item.IdLabelControle = row.IdLabelControle;

                if (!row.IsLibelleControleNull())
                    item.LibelleControle = row.LibelleControle;

                if (!row.IsIdVehiculeNull())
                    item.IdVehicule = row.IdVehicule;

                if (!row.IsPlaqueNull())
                    item.Immat = row.Plaque;

                if (!row.IsNumTaxiNull())
                    item.NumTaxi = row.NumTaxi;

                if (!row.IsTypeTaxiNull())
                    item.TypeTaxi = row.TypeTaxi;

                ListControles.Add(item);

            }

            return ListControles;
        }





        public int  AjouterUnControle(Controles item)
        {
            ControlesTableAdapter controlesAdapter=new ControlesTableAdapter();

            return controlesAdapter.InsertControle(DateTime.Now, item.IdOpCreation, DateTime.Now, item.IdOpCreation, DateTime.Parse(item.DateFin), item.IdLabelControle, item.IdVehicule);
        }

        public int ModifierUnControle(Controles item)
        {
            ControlesTableAdapter controlesAdapter = new ControlesTableAdapter();
            return controlesAdapter.UpdateControle(DateTime.Now, item.IdOpModif, DateTime.Parse(item.DateFin), item.Id);
        }

        public int SupprimerUnControle(int Id)
        {
            ControlesTableAdapter controlesAdapter = new ControlesTableAdapter();
            return controlesAdapter.DeleteControle(Id);
        }


        public string getTypeTaxiByImmat(string Immat)
        {
            ControlesTableAdapter controlesAdapter = new ControlesTableAdapter();
            return controlesAdapter.GetTypeTaxiByImmat(Immat).ToString() ;

        }

        public string getNumTaxiByImmat(string Immat)
        {
            ControlesTableAdapter controlesAdapter = new ControlesTableAdapter();
            return controlesAdapter.GetNumTaxiByImmat(Immat);

        }

        public string getImmatByNumTaxiTypeTaxi(string NumTaxi, string TypeTaxi)
        {
            ControlesTableAdapter controlesAdapter = new ControlesTableAdapter();
            return controlesAdapter.GetImmatByNumTaxiTypeTaxi(NumTaxi, TypeTaxi).ToString();

        }

        /************************************************/
        /*         savoir si un Controle existe       */
        /************************************************/

        public bool controleExiste(string NumTaxi, string TypeTaxi, string Libelle)
        {
            ControlesTableAdapter controlesAdapter = new ControlesTableAdapter();

            if (int.Parse(controlesAdapter.ControleExiste(NumTaxi, TypeTaxi, Libelle).ToString()) == 1)
                return true;
            else
                return false;

        }

        /*********************************************************************/
        /*               Total des Controles Taxis                                */
        /*********************************************************************/

        public int getTotalControlesVehicule()
        {
            ControlesTableAdapter controlesAdapter = new ControlesTableAdapter();
            try
            {
                return int.Parse(controlesAdapter.GetTotalControlesVehicule().ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /*********************************************************************/
        /*               Total des Controles Taxis  Avec Filtre             */
        /*********************************************************************/

        public int getTotalControlesVehiculeByFiltre(int? ddlAbrevControlesVh, DateTime? dateFin, string numTaxi,int? ddltypeTaxi, string immatriculation)
        {
            try
            {
                int total = 0;

                ControlesTableAdapter controlesAdapter = new ControlesTableAdapter();

                total = int.Parse(controlesAdapter.GetTotalControlesVehiculeByFiltre(ddlAbrevControlesVh, dateFin, numTaxi, ddltypeTaxi, immatriculation).ToString());

                return total;

            }
            catch (Exception)
            {
                return 0;
            }

        }


    }
}
