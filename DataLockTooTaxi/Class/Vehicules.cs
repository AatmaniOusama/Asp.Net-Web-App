using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLockTooTaxi.MyDataSetTableAdapters;

namespace DataLockTooTaxi
{
    public class Vehicules
    {
        public int Id { get; set; }
        public string DateCreation { get; set; }
        public int IdOpCreation { get; set; }
        public string DateModif { get; set; }
        public int IdOpModif { get; set; }
        public string Immat { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Cin { get; set; }
        public string DateMec { get; set; }
        public string DateImmat { get; set; }
        public string Modele { get; set; }
        public string Marque { get; set; }

        public Vehicules()
        {
            Id = -1;
            Immat = "";
            DateImmat = DateTime.Now.ToString().Substring(0,10);
            DateCreation = DateTime.Now.ToString().Substring(0, 10);
            IdOpCreation= -1;
            DateModif=DateTime.Now.ToString().Substring(0,10);
            IdOpModif=-1;
            Nom ="";
            Prenom = "";
            Cin = "";
            DateMec=DateTime.Now.ToString().Substring(0, 10);
            Modele = "";
            Marque="";
        }

        public List<Vehicules> getAllCarteGrises()
        {
            VehiculesTableAdapter VehiculesAdapter = new VehiculesTableAdapter();
            List<Vehicules> listCartesGrises = new List<Vehicules>();
            


            foreach (MyDataSet.VehiculesRow row in VehiculesAdapter.GetAllCarteGrises())
            {
                Vehicules item = new Vehicules();
                item.Id = row.Id;

                if (!row.IsImmatNull())
                item.Immat = row.Immat;

                if (!row.IsDateImmatNull())
                item.DateImmat = row.DateImmat.ToString().Substring(0, 10);

                if (!row.IsDateMecNull())
                item.DateMec = row.DateMec.ToString().Substring(0, 10);

                if (!row.IsNomNull())
                    item.Nom = row.Nom;
                else item.Nom = "";

                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;
                else item.Prenom = "";

                if (!row.IsCinNull())
                    item.Cin = row.Cin;
                else item.Cin = "";

                if (!row.IsModeleNull())
                    item.Modele = row.Modele;
                else item.Modele = "";

                if (!row.IsMarqueNull())
                    item.Marque = row.Marque;
                else item.Marque = "";

                listCartesGrises.Add(item);
            }

            return listCartesGrises;

        }

        public List<Vehicules> getCarteGrises(int Index, DateTime? dateImmat, DateTime? dateMec, string immat, string marque, string modele, string nom, string prenom, string cin)
        {
            VehiculesTableAdapter VehiculesAdapter = new VehiculesTableAdapter();
            List<Vehicules> listCartesGrises = new List<Vehicules>();



            foreach (MyDataSet.VehiculesRow row in VehiculesAdapter.GetCarteGrises(Index,dateImmat, dateMec, immat, marque, modele, nom, prenom, cin))
            {
                Vehicules item = new Vehicules();
                item.Id = row.Id;

                if (!row.IsImmatNull())
                    item.Immat = row.Immat;

                if (!row.IsDateImmatNull())
                    item.DateImmat = row.DateImmat.ToString().Substring(0, 10);

                if (!row.IsDateMecNull())
                    item.DateMec = row.DateMec.ToString().Substring(0, 10);

                if (!row.IsNomNull())
                    item.Nom = row.Nom;
                else item.Nom = "";

                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;
                else item.Prenom = "";

                if (!row.IsCinNull())
                    item.Cin = row.Cin;
                else item.Cin = "";

                if (!row.IsModeleNull())
                    item.Modele = row.Modele;
                else item.Modele = "";

                if (!row.IsMarqueNull())
                    item.Marque = row.Marque;
                else item.Marque = "";

                listCartesGrises.Add(item);
            }

            return listCartesGrises;

        }

        public List<Vehicules> getAllCarteGrisesByFiltre(DateTime? dateImmat, DateTime? dateMec, string immat, string marque, string modele, string nom, string prenom, string cin)
        {
            VehiculesTableAdapter VehiculesAdapter = new VehiculesTableAdapter();
            List<Vehicules> listCartesGrises = new List<Vehicules>();



            foreach (MyDataSet.VehiculesRow row in VehiculesAdapter.GetAllCarteGrisesByFiltre(dateImmat, dateMec, immat, marque, modele, nom, prenom, cin))
            {
                Vehicules item = new Vehicules();
                item.Id = row.Id;

                if (!row.IsImmatNull())
                    item.Immat = row.Immat;

                if (!row.IsDateImmatNull())
                    item.DateImmat = row.DateImmat.ToString().Substring(0, 10);

                if (!row.IsDateMecNull())
                    item.DateMec = row.DateMec.ToString().Substring(0, 10);

                if (!row.IsNomNull())
                    item.Nom = row.Nom;
                else item.Nom = "";

                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;
                else item.Prenom = "";

                if (!row.IsCinNull())
                    item.Cin = row.Cin;
                else item.Cin = "";

                if (!row.IsModeleNull())
                    item.Modele = row.Modele;
                else item.Modele = "";

                if (!row.IsMarqueNull())
                    item.Marque = row.Marque;
                else item.Marque = "";

                listCartesGrises.Add(item);
            }

            return listCartesGrises;

        }
      
        public List<Vehicules> GetImmatriculation()
        {
            VehiculesTableAdapter VehiculesAdapter = new VehiculesTableAdapter();
            List<Vehicules> listImmatriculations = new List<Vehicules>();



            foreach (MyDataSet.VehiculesRow row in VehiculesAdapter.GetImmatriculation())
            {
                Vehicules item = new Vehicules();


                item.Id = row.Id;

                if (!row.IsImmatNull())
                    item.Immat = row.Immat;

                listImmatriculations.Add(item);
            }

            return listImmatriculations;

        }

        public List<Vehicules> getImmatriculationNonAttribue()
        {
            VehiculesTableAdapter VehiculesAdapter = new VehiculesTableAdapter();
            List<Vehicules> listImmatriculations = new List<Vehicules>();



            foreach (MyDataSet.VehiculesRow row in VehiculesAdapter.GetImmatriculationNonAttribue())
            {
                Vehicules item = new Vehicules();

              
                item.Id = row.Id;

                if (!row.IsImmatNull())
                item.Immat = row.Immat;

                listImmatriculations.Add(item);
            }

            return listImmatriculations;

        }

        public Vehicules getUnVehicule(string immat)
        {
            VehiculesTableAdapter VehiculesAdapter = new VehiculesTableAdapter();


            Vehicules item = new Vehicules();

            foreach (MyDataSet.VehiculesRow row in VehiculesAdapter.GetUnVehicule(immat))
            {
                
                item.Id = row.Id;

                if (!row.IsImmatNull())
                item.Immat = row.Immat;

                if (!row.IsDateImmatNull())
                item.DateImmat = row.DateImmat.ToString().Substring(0, 10);

                if (!row.IsDateMecNull())
                item.DateMec = row.DateMec.ToString().Substring(0, 10);

                if (!row.IsNomNull())
                    item.Nom = row.Nom;
                else item.Nom = "";

                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;
                else item.Prenom = "";

                if (!row.IsCinNull())
                    item.Cin = row.Cin;
                else item.Cin = "";

                if (!row.IsModeleNull())
                    item.Modele = row.Modele;
                else item.Modele = "";

                if (!row.IsMarqueNull())
                    item.Marque = row.Marque;
                else item.Marque = "";
            }

            return item;

        }


        /************************************************/
        /*               Total des CartesGrises                 */
        /************************************************/

        public int getTotalCarteGrises()
        {
            VehiculesTableAdapter VehiculesAdapter = new VehiculesTableAdapter();
            try
            {
                return int.Parse(VehiculesAdapter.GetTotalCarteGrises().ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }


        /*************************************************************/
        /*               Total des CartesGrises  Avec Filtre     */
        /*************************************************************/

        public int getTotalCarteGrisesByFiltre(DateTime? dateImmat, DateTime? dateMec, string immat, string marque, string modele, string nom, string prenom, string cin)
        {
            try
            {
                int total = 0;

                VehiculesTableAdapter VehiculesAdapter = new VehiculesTableAdapter();


                total = int.Parse(VehiculesAdapter.GetTotalCarteGrisesByFiltre(dateImmat, dateMec, immat, marque, modele, nom, prenom, cin).ToString());


                return total;

            }
            catch (Exception)
            {
                return 0;
            }

        }



        public int updateVehicule(Vehicules item,string Immat)
      {
         VehiculesTableAdapter VehiculeAdapter = new VehiculesTableAdapter();

         return VehiculeAdapter.UpdateVehicule(DateTime.Parse(item.DateCreation), item.IdOpCreation, DateTime.Parse(item.DateMec), DateTime.Parse(item.DateModif), item.IdOpModif, item.Immat, item.Nom, item.Prenom, item.Cin, DateTime.Parse(item.DateImmat),item.Marque,item.Modele,Immat);
           
        }

        public int insertVehicule(Vehicules item)
        {
            VehiculesTableAdapter VehiculeAdapter = new VehiculesTableAdapter();
            return VehiculeAdapter.InsertVehicule(DateTime.Parse(item.DateCreation), item.IdOpCreation, DateTime.Parse(item.DateModif), item.IdOpModif, item.Immat, item.Nom, item.Prenom, item.Cin, DateTime.Parse(item.DateMec), DateTime.Parse(item.DateImmat), item.Marque, item.Modele);
                     
        }

        public int SupprimerUnVehicule(string immat)
        { 
           VehiculesTableAdapter VehiculeAdapter = new VehiculesTableAdapter();
           return VehiculeAdapter.SupprimerUnVehicule(immat);
        }
           
    }
}