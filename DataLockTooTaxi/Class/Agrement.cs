using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLockTooTaxi.MyDataSetTableAdapters;

namespace DataLockTooTaxi
{
    public class Agrement
    {
        public int Id { get; set; }
        public DateTime DateCreation { get; set; }
        public int IdOpCreation { get; set; }
        public DateTime DateModif { get; set; }
        public int IdOpModif { get; set; }
        public string DateDebut { get; set; }
        public string DateFin { get; set; }
        public string NumAgrement { get; set; }
        public string Valide { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        //------------------------------------------date 27/01/2017------------ Zouhair LOUALID----------------------
        public string CIN { get; set; }  // Ajoute de la CIN
        public string Adresse { get; set; } //Ajout de l 'adresse
        public string Telephone { get; set; }// Ajout du telephone


        public string Plaque { get; set; }
        public string PointAttache { get; set; }
        public string NumTaxi { get; set; }
        public string Commune { get; set; }
        public string TypeTaxi { get; set; }
        public int IdVehicule { get; set; }
        //variable première identification
        public string Nom_Prem { get; set; }
        public string Prenom_Prem { get; set; }
        // variable deuxième identification
        public string Nom_Dern { get; set; }
        public string Prenom_Dern { get; set; }


        public Agrement()
        {
                Id=-1; 
                DateCreation= DateTime.Now;
                IdOpCreation = 1;
                DateModif = DateTime.Now;
                IdOpModif = 1;
                DateDebut = DateTime.Now.ToString().Substring(0,10);
                DateFin = DateTime.Now.AddMonths(240).ToString().Substring(0,10);
                NumAgrement="";
                Valide = "True";
                Nom ="";
                Prenom ="";
                Plaque ="";
                //------------------------------------------date 27/01/2017------------ Zouhair LOUALID----------------------
                CIN = "";
                Adresse = "";    // constructeur ajout de CIN, Adresse, Telephone
                Telephone = "";
                //--------------------------------------------------------------------------------------------------------

                PointAttache ="";
                NumTaxi ="";
                Commune = "";
                TypeTaxi ="";
                IdVehicule = 0;
                Nom_Prem ="";
                Prenom_Prem ="";
                Nom_Dern ="";
                Prenom_Dern = "";
            
        }

        /*******************************************************/
        /*               la liste des Agrements                */
        /*******************************************************/

        public List<Agrement> getAllAgrements(int IndexPage, DateTime? dateDebut, DateTime? dateFin, string numAgrement, string immatriculation, string nom, string prenom, int? ddltypeTaxi, string rbautoriseValue)
        {
            AgrementTableAdapter AgrementAdapter = new AgrementTableAdapter();
            
            
          
                MyDataSet.AgrementDataTable TableAgrements = AgrementAdapter.GetAllAgrements(IndexPage, dateDebut, dateFin, numAgrement, immatriculation, nom, prenom, ddltypeTaxi, rbautoriseValue);
                List<Agrement> ListAgrements = new List<Agrement>();


            foreach (MyDataSet.AgrementRow row in TableAgrements)
            {
                Agrement item = new Agrement();

                if(!row.IsNumAgrementNull())
                item.NumAgrement = row.NumAgrement;

                if (!row.IsTypeTaxiNull())
                    item.TypeTaxi = row.TypeTaxi;

                if (!row.IsPlaqueNull())
                    item.Plaque = row.Plaque;

                if (!row.IsDateDebutNull())
                    item.DateDebut = row.DateDebut.ToString().Substring(0, 10);

                if (!row.IsDateFinNull())
                    item.DateFin = row.DateFin.Date.ToString().Substring(0, 10);


                if (!row.IsNomNull())
                item.Nom = row.Nom;

                if (!row.IsPrenomNull())
                item.Prenom = row.Prenom;
               
                if (!row.IsCommuneNull())
                item.Commune = row.Commune.ToString();


                if (!row.IsPointAttacheNull())
                    item.PointAttache = row.PointAttache.ToString();

                if (!row.IsValideNull())
                item.Valide = row.Valide.ToString();

                if (!row.IsIdVehiculeNull())
                    item.IdVehicule = row.IdVehicule;

                //---------- date 27/01/2017 : Zouhair LOUALID-------------------------------------------------

                //if (!row.IsIdVehiculeNull())
                item.CIN = "T214167";//row.IdVehicule;

                //if (!row.IsIdVehiculeNull())
                item.Adresse = "QUARTIER RUSS";//row.IdVehicule;

                //if (!row.IsIdVehiculeNull())
                item.Telephone = "0670434360";//row.IdVehicule;


                ListAgrements.Add(item);
            }

            return ListAgrements;


        }

        /*******************************************************/
        /*               la liste des Agrements  avec Filtre    */
        /*******************************************************/

        public List<Agrement> getAllAgrementsByFiltre( DateTime? dateDebut, DateTime? dateFin, string numAgrement, string immatriculation, string nom, string prenom, int? ddltypeTaxi, string rbautoriseValue)
        {
            AgrementTableAdapter AgrementAdapter = new AgrementTableAdapter();
            MyDataSet.AgrementDataTable TableAgrements = AgrementAdapter.GetAllAgrementsByFiltre( dateDebut, dateFin, numAgrement, immatriculation, nom, prenom, ddltypeTaxi, rbautoriseValue);
            List<Agrement> ListAgrements = new List<Agrement>();


            foreach (MyDataSet.AgrementRow row in TableAgrements)
            {
                Agrement item = new Agrement();

                if (!row.IsNumAgrementNull())
                    item.NumAgrement = row.NumAgrement;

                if (!row.IsTypeTaxiNull())
                    item.TypeTaxi = row.TypeTaxi;

                if (!row.IsPlaqueNull())
                    item.Plaque = row.Plaque;

                if (!row.IsDateDebutNull())
                    item.DateDebut = row.DateDebut.ToString().Substring(0, 10);

                if (!row.IsDateFinNull())
                    item.DateFin = row.DateFin.Date.ToString().Substring(0, 10);

                if (!row.IsNomNull())
                    item.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;

                if (!row.IsCommuneNull())
                    item.Commune = row.Commune.ToString();

                if (!row.IsPointAttacheNull())
                    item.PointAttache = row.PointAttache.ToString();

                if (!row.IsValideNull())
                    item.Valide = row.Valide.ToString();

                if (!row.IsIdVehiculeNull())
                    item.IdVehicule = row.IdVehicule;
                //---------- date 27/01/2017 : Zouhair LOUALID-------------------------------------------------

                //if (!row.IsIdVehiculeNull())
                item.CIN = "T214167";//row.IdVehicule;

                //if (!row.IsIdVehiculeNull())
                item.Adresse = "QUARTIER RUSS";//row.IdVehicule;

                //if (!row.IsIdVehiculeNull())
                item.Telephone = "0670434360";//row.IdVehicule;



                ListAgrements.Add(item);
            }

            return ListAgrements;


        }

        public List<Agrement> getAgrements()
        {
            AgrementTableAdapter AgrementAdapter = new AgrementTableAdapter();
            MyDataSet.AgrementDataTable TableAgrements = AgrementAdapter.GetAgrements();
            List<Agrement> ListAgrements = new List<Agrement>();


            foreach (MyDataSet.AgrementRow row in TableAgrements)
            {
                Agrement item = new Agrement();

                if (!row.IsNumAgrementNull())
                    item.NumAgrement = row.NumAgrement;

                if (!row.IsTypeTaxiNull())
                    item.TypeTaxi = row.TypeTaxi;

                if (!row.IsPlaqueNull())
                    item.Plaque = row.Plaque;

                if (!row.IsDateDebutNull())
                    item.DateDebut = row.DateDebut.ToString().Substring(0, 10);

                if (!row.IsDateFinNull())
                    item.DateFin = row.DateFin.Date.ToString().Substring(0, 10);


                if (!row.IsNomNull())
                    item.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;

                //---------- date 27/01/2017 : Zouhair LOUALID-------------------------------------------------

                //if (!row.IsIdVehiculeNull())
                item.CIN = "T214167";//row.IdVehicule;

                //if (!row.IsIdVehiculeNull())
                item.Adresse = "QUARTIER RUSS";//row.IdVehicule;

                //if (!row.IsIdVehiculeNull())
                item.Telephone = "0670434360";//row.IdVehicule;


                if (!row.IsCommuneNull())
                    item.Commune = row.Commune.ToString();

                if (!row.IsPointAttacheNull())
                    item.PointAttache = row.PointAttache.ToString();


                if (!row.IsValideNull())
                    item.Valide = row.Valide.ToString();

                if (!row.IsIdVehiculeNull())
                    item.IdVehicule = row.IdVehicule;


                ListAgrements.Add(item);
            }

            return ListAgrements;


        }

        public Agrement getUnAgrement(string NumAgrement, int type)
        {
            AgrementTableAdapter AgrementAdapter = new AgrementTableAdapter(); // lié un autre datadapter de Agrement2 pour realiser les modifications 
            MyDataSet.AgrementDataTable TableAgrements = AgrementAdapter.GetAgrementByNumAgrementANDtype(NumAgrement,type);


            Agrement item = new Agrement();

            foreach (MyDataSet.AgrementRow row in TableAgrements)
            {
                if (!row.IsNumAgrementNull())
                item.NumAgrement = row.NumAgrement;

                if (!row.IsNomNull())
                item.Nom = row.Nom;

                if (!row.IsPrenomNull())
                item.Prenom = row.Prenom;

                //---------- date 27/01/2017 : Zouhair LOUALID-------------------------------------------------

                if (!row.IsCinNull())
                    item.CIN = row.Cin;

                if (!row.IsAdresseNull())
                    item.Adresse = row.Adresse;

                if (!row.IsTelephoneNull())
                    item.Telephone = row.Telephone;


                if (!row.IsPlaqueNull())
                item.Plaque = row.Plaque;

                if (!row.IsTypeTaxiNull())
                item.TypeTaxi = row.TypeTaxi.ToString();

                if (!row.IsNumTaxiNull())
                item.NumTaxi = row.NumTaxi;

                if (!row.IsDateDebutNull())
                item.DateDebut = row.DateDebut.ToString().Substring(0,10);

                if (!row.IsDateFinNull())
                item.DateFin = row.DateFin.Date.ToString().Substring(0,10);

                if (!row.IsCommuneNull())
                    item.Commune = row.Commune;
                else
                    item.Commune = "";

                if (!row.IsPointAttacheNull())
                    item.PointAttache = row.PointAttache;

                if (!row.IsValideNull())
                item.Valide = row.Valide.ToString();

                if (!row.IsDateModifNull())
                    item.DateModif = row.DateModif;

                if (!row.IsDateCreationNull())
                    item.DateCreation = row.DateCreation;

                if (!row.IsIdVehiculeNull())
                item.IdVehicule = row.IdVehicule;
            }

            return item;


        }


        public Agrement getUnAgrementByPlaque(string plaque)
        {  
            AgrementTableAdapter AgrementAdapter = new AgrementTableAdapter();
            MyDataSet.AgrementDataTable TableAgrements = AgrementAdapter.GetUnAgrementByPlaque(plaque);


            Agrement item = new Agrement();

            foreach (MyDataSet.AgrementRow row in TableAgrements)
            {

                if (!row.IsNumAgrementNull())
                    item.NumAgrement = row.NumAgrement;

                if (!row.IsNomNull())
                    item.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;

                //---------- date 27/01/2017 : Zouhair LOUALID-------------------------------------------------

               if (!row.IsCinNull())
                item.CIN = row.Cin;

               if (!row.IsAdresseNull())
                item.Adresse = row.Adresse;

                if (!row.IsTelephoneNull())
                item.Telephone =row.Telephone;


                if (!row.IsPlaqueNull())
                    item.Plaque = row.Plaque;


                if (!row.IsTypeTaxiNull())
                    item.TypeTaxi = row.TypeTaxi;

                if (!row.IsNumTaxiNull())
                    item.NumTaxi = row.NumTaxi;

                if (!row.IsDateDebutNull())
                    item.DateDebut = row.DateDebut.Date.ToString().Substring(0,10);

                if (!row.IsDateFinNull())
                    item.DateFin = row.DateFin.Date.ToString().Substring(0,10);

                if (!row.IsCommuneNull())
                    item.Commune = row.Commune;
                else
                    item.Commune = "";

                if (!row.IsPointAttacheNull())
                    item.PointAttache = row.PointAttache;

                if (!row.IsValideNull())
                    item.Valide = row.Valide.ToString();

                if (!row.IsIdVehiculeNull())
                    item.IdVehicule = row.IdVehicule;

            }

            return item;


        }
        /************************************************/
        /*            Ajouter Un Agrement               */
        /************************************************/

        
        public int AjouterUnAgrement(Agrement item)
        {
            AgrementTableAdapter AgrementAdapter = new AgrementTableAdapter();
            int idCommune = int.Parse(item.Commune);
            return AgrementAdapter.InsertAgrement(item.DateCreation, item.IdOpCreation, item.DateModif, item.IdOpModif, DateTime.Parse(item.DateDebut), DateTime.Parse(item.DateFin), item.NumAgrement, true, item.Nom, item.Prenom, item.Plaque, item.PointAttache, item.NumAgrement, idCommune, int.Parse(item.TypeTaxi),item.CIN,item.Adresse,item.Telephone, item.IdVehicule);

        }

        /************************************************/
        /*            Modifier Un Agrement              */
        /************************************************/

        public int ModifierUnAgrement(Agrement item)
        {
            AgrementTableAdapter AgrementAdapter = new AgrementTableAdapter();

            int idCommune = int.Parse(item.Commune);

            return AgrementAdapter.UpdateAgrement(item.DateCreation, item.IdOpCreation, item.DateModif, item.IdOpModif,
                DateTime.Parse(item.DateDebut), DateTime.Parse(item.DateFin), item.NumAgrement,
                bool.Parse(item.Valide), item.Nom, item.Prenom, item.Plaque.ToString(),item.PointAttache, item.NumAgrement, 
                idCommune, int.Parse(item.TypeTaxi), item.IdVehicule,item.CIN.ToString(),
                item.Adresse.ToString(),item.Telephone.ToString(), item.NumAgrement,
                int.Parse(item.TypeTaxi));
        }

        public int updateAgrementByImmat(string Immat,string OldImmt)
        {
            AgrementTableAdapter AgrementAdapter = new AgrementTableAdapter();
            return AgrementAdapter.UpdateAgrementByImmat(Immat,OldImmt); 
        }


        /******************************************************/
        /*   Modifier Valide (Autorisation)  d'Un Agrement    */
        /******************************************************/

        public int ModifierValide(bool Valide, string numAgrement, int type)
        {
            AgrementTableAdapter AgrementAdapter = new AgrementTableAdapter();
            return AgrementAdapter.SetValide(Valide, numAgrement, type);
        }

        /************************************************/
        /*           Supprimer Un Agrement           */
        /************************************************/

        public int SupprimerUnAgrement(string numAgrement, int type)
        {
            AgrementTableAdapter AgrementAdapter = new AgrementTableAdapter();
            return AgrementAdapter.DeleteUnAgrement(numAgrement,type);
        }

      
        /************************************************/
        /*           Get Type Taxi                      */
        /************************************************/

        public int getTypeTaxi(string typetaxi)
        {
            AgrementTableAdapter AgrementAdapter = new AgrementTableAdapter();
            return AgrementAdapter.GetTypeTaxi(typetaxi).Value;

        }
        
        

        ///************************************************/
        ///*                  Max IdAgrement              */
        ///************************************************/  

        public int MaxIdAgrement()
        {
            AgrementTableAdapter agrementAdapter = new AgrementTableAdapter();
            try
            {
                return agrementAdapter.GetMaxIdAgrement().Value;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        /*********************************************************************/
        /*               Total des Taxis                                */
        /*********************************************************************/

        public int getTotalAgrements()
        {
            AgrementTableAdapter AgrementAdapter = new AgrementTableAdapter();
            try
            {
                return int.Parse(AgrementAdapter.GetTotalAgrements().ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /*********************************************************************/
        /*               Total des Taxis  Avec Filtre             */
        /*********************************************************************/

        public int getTotalAgrementsByFiltre(DateTime? dateDebut, DateTime? dateFin, string numAgrement, string immatriculation, string nom, string prenom, int? ddltypeTaxi, string rbautoriseValue)
        {
            try
            {
                int total = 0;

                AgrementTableAdapter AgrementAdapter = new AgrementTableAdapter();

                total = int.Parse(AgrementAdapter.GetTotalAgrementsByFiltre(dateDebut, dateFin, numAgrement, immatriculation, nom, prenom, ddltypeTaxi, rbautoriseValue).ToString());

                return total;

            }
            catch (Exception)
            {
                return 0;
            }

        }


        /*********************************************************************/
        /*       Agrement existe (utilisé dans Controles Véhicules)          */
        /*********************************************************************/

        public int numTaxiExiste(string NumAgrement, string TypeTaxi)
        {
            try
            {
                int existe = 0;

                AgrementTableAdapter AgrementAdapter = new AgrementTableAdapter();
                
                existe = int.Parse(AgrementAdapter.NumTaxiExiste(NumAgrement, TypeTaxi).ToString());

                return existe;

            }
            catch (Exception)
            {
                return 0;
            }

        }


        public List<Agrement> getTaxisAbsents(DateTime du, DateTime au)
        {
            AgrementTableAdapter AgrementAdapter = new AgrementTableAdapter();
            MyDataSet.AgrementDataTable TableAgrements = AgrementAdapter.GetTaxisAbsents(du, au);
            List<Agrement> ListAgrements = new List<Agrement>();

            foreach (MyDataSet.AgrementRow row in TableAgrements)
            {
                Agrement item = new Agrement();

                if(!row.IsNumAgrementNull())
                item.NumAgrement = row.NumAgrement;

                if (!row.IsNomNull())
                item.Nom = row.Nom;

                if (!row.IsPrenomNull())
                item.Prenom = row.Prenom;

                //---------- date 27/01/2017 : Zouhair LOUALID-------------------------------------------------

                //if (!row.IsIdVehiculeNull())
                item.CIN = "T214167";//row.IdVehicule;

                //if (!row.IsIdVehiculeNull())
                item.Adresse = "QUARTIER RUSS";//row.IdVehicule;

                //if (!row.IsIdVehiculeNull())
                item.Telephone = "0670434360";//row.IdVehicule;


                if (!row.IsPlaqueNull())
                item.Plaque = row.Plaque;

                if (!row.IsTypeTaxiNull())
                item.TypeTaxi = row.TypeTaxi;

                if (!row.IsNumTaxiNull())
                item.NumTaxi = row.NumTaxi;

                if (!row.IsDateDebutNull())
                item.DateDebut = row.DateDebut.Date.ToString().Substring(0,10);

                if (!row.IsDateFinNull())
                item.DateFin = row.DateFin.Date.ToString().Substring(0,10);

                if (!row.IsCommuneNull())
                item.Commune = row.Commune.ToString();

                if (!row.IsPointAttacheNull())
                    item.PointAttache = row.PointAttache.ToString();

                if (!row.IsValideNull())
                item.Valide = row.Valide.ToString();

                if (!row.IsIdVehiculeNull())
                    item.IdVehicule = row.IdVehicule;

                ListAgrements.Add(item);
            }

            return ListAgrements;

        }

        public List<Agrement> getIdentification_Periode_Taxi( DateTime du, DateTime au)
        {
            AgrementTableAdapter AgrementAdapter = new AgrementTableAdapter();
            MyDataSet.AgrementDataTable TableAgrements = AgrementAdapter.Identification_Periode_Taxi(du,au);
            List<Agrement> ListAgrements = new List<Agrement>();

            foreach (MyDataSet.AgrementRow row in TableAgrements)
            {
                Agrement item = new Agrement();


                if (!row.IsNumTaxiNull())
                item.NumTaxi = row.NumTaxi;

                if (!row.IsTypeTaxiNull())
                item.TypeTaxi = row.TypeTaxi;
                
                if (!row.IsNom_PremNull())
                    item.Nom_Prem = row.Nom_Prem.ToString();
                else
                    item.Nom_Prem = "";


                if (!row.IsPrenom_PremNull())
                    item.Prenom_Prem = row.Prenom_Prem.ToString();
                else
                    item.Prenom_Prem = "";


                if (!row.IsNom_DernNull())
                    item.Nom_Dern = row.Nom_Dern.ToString();
                else
                    item.Nom_Dern = "";


                if (!row.IsPrenom_DernNull())
                    item.Prenom_Dern = row.Prenom_Dern.ToString();
                else
                    item.Prenom_Dern = "";
                

                if (!row.IsDateDebutNull())
                    item.DateDebut = row.DateDebut.ToString();
                else
                    item.DateDebut = "";


                if (!row.IsDateFinNull() && (row.DateFin != row.DateDebut))
                    item.DateFin = row.DateFin.ToString();
                else
                    item.DateFin = "";
               

                ListAgrements.Add(item);
            }

            return ListAgrements;
        
        }



      
    }
}
