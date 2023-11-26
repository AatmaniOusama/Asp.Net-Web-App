using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLockTooTaxi.MyDataSetTableAdapters;

namespace DataLockTooTaxi
{
    public class User
    {
        public int Id { get; set; }
        public string Matricule { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int NumService { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string FlagAutorise { get; set; }
        public DateTime? DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public int NumDroitAcces { get; set; }
        public string NumBadge { get; set; }
        public string NumCode { get; set; }
        public int NbEmpreintes { get; set; }
        public string Adresse1 { get; set; }
        public string Adresse2 { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string EMail { get; set; }
        public DateTime? DateCreat { get; set; }
        public DateTime? DateModif { get; set; }
        public string DateNaissance { get; set; }
        public int HeureMois { get; set; }
        public int TauxNormal { get; set; }
        public int TauxSup1 { get; set; }
        public int TauxSup2 { get; set; }
        public string Fonction { get; set; }
        public int TauxSup3 { get; set; }
        public int Sexe { get; set; }
        public int Civilite { get; set; }
        public string NumCI { get; set; }
        public string NumSS { get; set; }
        public int NumHoraire { get; set; }
        public bool CheckAutoDeclar { get; set; }
        public bool CheckBadge { get; set; }
        public bool CheckEmpreinte { get; set; }
        public bool CheckPin { get; set; }
        public int CodePin { get; set; }
        public int TauxSup0 { get; set; }
        public int TypeTemps { get; set; }
        public bool Modified { get; set; }
        public bool BadgeEncoded { get; set; }
        public int NumContrat { get; set; }
        public int TypeUser { get; set; }
        public int IdUser1 { get; set; }
        public int IdUser2 { get; set; }
        public int IdUser3 { get; set; }
        public bool UpdateLecNow { get; set; }
        public int JourRepos1 { get; set; }
        public int JourRepos2 { get; set; }
        public int NbJrsCongesAnnuels { get; set; }
        public string LieuNaiss { get; set; }
        public string DateCI { get; set; }
        public string NumPermis { get; set; }
        public string DatePermis { get; set; }
        public string AncienNumBadge { get; set; }
        public int TypeAd { get; set; }
        public string Mdp { get; set; }
        public string UserLogin { get; set; }
        public bool CheckPointageWeb { get; set; }
        public string Observations { get; set; }
        public string Commentaire { get; set; }// Date:27/01/2017--By Zouhair LOUALID--------ajout de Commentaire------------
        public DateTime? DateFinMichoc { get; set; }
        public bool CheckBlocageJoursRepos { get; set; }
        public bool CheckPremierPointage { get; set; }

        public string BioData { get; set; }
        public string NomComplet { get; set; }
        public string NomService { get; set; }
        public string LibelleContrat { get; set; }
        public byte[] Photo { get; set; }
        public string Motif { get; set; }
        public string LibelleHoraire { get; set; }
        public string LibelleDroit { get; set; }
        public string LibelleListe { get; set; }
        public int Total { get; set; }
        //  public string DateDebutAbs             { get; set; }




        public User()
        {

            Id = -1;
            Matricule = "";
            Nom = "";
            Prenom = "";
            NumService = 0;
            Tel = null;
            Fax = null;
            FlagAutorise = "Y";
            DateDebut = DateTime.Now;
            DateFin = DateTime.Now.AddYears(10);
            NumDroitAcces = 1;
            NumBadge = "";
            NumCode = null;
            NbEmpreintes = 2;
            Adresse1 = null;
            Adresse2 = null;
            CodePostal = null;
            Ville = null;
            Telephone = null;
            Mobile = null;
            EMail = null;
            DateCreat = DateTime.Now;
            DateModif = DateTime.Now;
            DateNaissance = null;
            HeureMois = 0;
            TauxNormal = 0;
            TauxSup1 = 0;
            TauxSup2 = 0;
            Fonction = null;
            TauxSup3 = 0;
            Sexe = 1;
            Civilite = 0;
            NumCI = null;
            NumSS = null;
            NumHoraire = 0;
            CheckAutoDeclar = false;
            CheckBadge = true;
            CheckEmpreinte = false;
            CheckPin = false;
            CodePin = 0;
            TauxSup0 = 0;
            TypeTemps = 0;
            Modified = false;
            BadgeEncoded = false;
            NumContrat = 0;
            TypeUser = 1;
            IdUser1 = 0;
            IdUser2 = 0;
            IdUser3 = 0;
            UpdateLecNow = true;
            JourRepos1 = 7;
            JourRepos2 = 0;
            NbJrsCongesAnnuels = 0;
            LieuNaiss = null;
            DateCI = null;
            NumPermis = null;
            DatePermis = null;
            AncienNumBadge = null;
            TypeAd = 0;
            Mdp = null;
            UserLogin = null;
            CheckPointageWeb = false;
            Observations = null;
            DateFinMichoc = null;
            CheckBlocageJoursRepos = false;
            CheckPremierPointage = false;
            Commentaire = null;// Date:27/01/2017--By Zouhair LOUALID--------ajout de Commentaire------------

            Motif = "";
        }

        /********************************************************************/
        /*     Remplir la  _DDLNom par Nom et Prenom                        */
        /********************************************************************/

        public override string ToString()
        {
            return Nom + " " + Prenom;
        }

        /********************************************************************/
        /*    Renvoie la liste des Noms Complets                            */
        /********************************************************************/

        public List<User> GetNomsComplets()
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();

            List<User> ListUsers = new List<User>();
            MyDataSet.UsersDataTable TableUsers = UsersAdapter.GetNomsComplets();
            foreach (MyDataSet.UsersRow row in TableUsers)
            {
                User user = new User();

                user.Id = row.IdUser;
                user.Matricule = row.Matricule;
                user.NomComplet = user.Nom = row.Nom;
                user.NomComplet += " ";
                user.NomComplet += user.Prenom = row.Prenom;
                if (!row.IsNumBadgeNull())
                    user.NumBadge = row.NumBadge;
                else
                    user.NumBadge = "";
                ListUsers.Add(user);
            }

            return ListUsers;
        }

        /********************************************************************/
        /*    chercher un Nom d'un chauffeur par son  Matricule             */
        /********************************************************************/

        public string geNomCompletByMatricule(string matricule)
        {
            User item = new User();

            UsersTableAdapter UsersAdapter = new UsersTableAdapter();

            MyDataSet.UsersDataTable TableUsers = new MyDataSet.UsersDataTable();

            TableUsers = UsersAdapter.GetNomByMatricule(matricule);

            foreach (MyDataSet.UsersRow row in TableUsers)
            {
                item.NomComplet = item.Nom = row.Nom;
                item.NomComplet += " ";
                item.NomComplet += item.Prenom = row.Prenom;
            }

            return item.NomComplet;

        }

        /********************************************************************/
        /*    Renvoie une Liste par son Numéro                              */
        /********************************************************************/

        public List<User> getListesByNumList(int NumListe, string TypeListe, string numPermis, string matricule, string nom, string prenom, string libelleMotif)
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();

            List<User> ListUsers = new List<User>();

            MyDataSet.UsersDataTable TableUsers = new MyDataSet.UsersDataTable();

            switch (TypeListe)
            {
                case "Chauffeurs":

                    TableUsers = UsersAdapter.GetListesByNumList(NumListe, numPermis, matricule, nom, prenom, libelleMotif);
                    break;

                case "Opérateurs":

                    TableUsers = UsersAdapter.GetListesOperateurByNumList(NumListe, matricule, nom, prenom, libelleMotif);
                    break;

                case "Agents":

                    TableUsers = UsersAdapter.GetListesVisiteurByNumList(NumListe, matricule, nom, prenom, libelleMotif);
                    break;
                case "Chauffeurs demandés":

                    TableUsers = UsersAdapter.GetListesByNumList(NumListe, numPermis, matricule, nom, prenom, libelleMotif);
                    break;
            }

            foreach (MyDataSet.UsersRow row in TableUsers)
            {
                User user = new User();

                if (row.NumBadge != null)
                    user.NumBadge = row.NumBadge;

                if (row.Matricule != null)
                    user.Matricule = row.Matricule;

                if (row.Nom != null)
                    user.Nom = row.Nom;

                if (row.Prenom != null)
                    user.Prenom = row.Prenom;

                if (row.Motif != null)

                    user.Motif = row.Motif;

                ListUsers.Add(user);
            }

            return ListUsers;



        }

        /*****************************************************************************************/
        /*    Renvoie la liste des Utilisateurs  Avec Filtre   (sert pour le trie et l'export)  */
        /*****************************************************************************************/

        public List<User> getAllUsersByFiltre(string matricule, string nom, string prenom, string numPermis, string rbautoriseValue, string rbenrolerValue, int? rbCoderValue)
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();

            List<User> ListUsers = new List<User>();



            MyDataSet.UsersDataTable TableUsers = null;


            TableUsers = UsersAdapter.GetAllUsersByFiltre(matricule, nom, prenom, numPermis, rbautoriseValue, rbenrolerValue, rbCoderValue);


            foreach (MyDataSet.UsersRow row in TableUsers)
            {
                User user = new User();


                if (!row.IsMatriculeNull())
                    user.Matricule = row.Matricule;

                if (!row.IsNomNull())
                    user.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    user.Prenom = row.Prenom;

                if (!row.IsNumBadgeNull())
                    user.NumBadge = row.NumBadge;

                if (!row.IsLibelleDroitNull())
                    user.LibelleDroit = row.LibelleDroit;

                if (!row.IsNumDroitAccesNull())
                    user.NumDroitAcces = row.NumDroitAcces;

                user.FlagAutorise = row.FlagAutorise;

                if (!row.IsDateDebutNull())
                    user.DateDebut = DateTime.Parse(row.DateDebut.ToString("dd/MM/yyy HH:mm:ss"));

                if (!row.IsDateFinNull())
                    user.DateFin = DateTime.Parse(row.DateFin.Date.ToString("dd/MM/yyy HH:mm:ss"));

                if (!row.IsTelephoneNull())
                    user.Telephone = row.Telephone;


                if (!row.IsVilleNull())
                    user.Ville = row.Ville;



                ListUsers.Add(user);
            }



            return ListUsers;
        }

        /**************************************************************************/
        /*    Renvoie la liste des Utilisateurs  (sert pour le trie et l'export)  */
        /**************************************************************************/

        public List<User> getAllUsers()
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();

            List<User> ListUsers = new List<User>();



            MyDataSet.UsersDataTable TableUsers = null;


            TableUsers = UsersAdapter.GetAllUsers();



            foreach (MyDataSet.UsersRow row in TableUsers)
            {
                User user = new User();


                if (!row.IsMatriculeNull())
                    user.Matricule = row.Matricule;

                if (!row.IsNomNull())
                    user.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    user.Prenom = row.Prenom;

                if (!row.IsNumBadgeNull())
                    user.NumBadge = row.NumBadge;

                if (!row.IsLibelleDroitNull())
                    user.LibelleDroit = row.LibelleDroit;

                if (!row.IsNumDroitAccesNull())
                    user.NumDroitAcces = row.NumDroitAcces;

                user.FlagAutorise = row.FlagAutorise;

                if (!row.IsDateDebutNull())
                    user.DateDebut = DateTime.Parse(row.DateDebut.ToString("dd/MM/yyy HH:mm:ss"));

                if (!row.IsDateFinNull())
                    user.DateFin = DateTime.Parse(row.DateFin.Date.ToString("dd/MM/yyy HH:mm:ss"));

                if (!row.IsTelephoneNull())
                    user.Telephone = row.Telephone;


                if (!row.IsVilleNull())
                    user.Ville = row.Ville;



                ListUsers.Add(user);
            }



            return ListUsers;
        }

        /********************************************************************/
        /*    Renvoie la liste des Utilisateurs                             */
        /********************************************************************/

        public List<User> GetUsers(int Index, string matricule, string nom, string prenom, string numPermis, string rbautoriseValue, int? rbCoderValue)
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();

            List<User> ListUsers = new List<User>();

            MyDataSet.UsersDataTable TableUsers = null;


            TableUsers = UsersAdapter.GetUsers(Index, matricule, nom, prenom, numPermis, rbautoriseValue, rbCoderValue);


            foreach (MyDataSet.UsersRow row in TableUsers)
            {
                User user = new User();


                if (!row.IsMatriculeNull())
                    user.Matricule = row.Matricule;

                if (!row.IsNomNull())
                    user.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    user.Prenom = row.Prenom;

                if (!row.IsNumBadgeNull())
                    user.NumBadge = row.NumBadge;

                if (!row.IsLibelleDroitNull())
                    user.LibelleDroit = row.LibelleDroit;

                if (!row.IsNumDroitAccesNull())
                    user.NumDroitAcces = row.NumDroitAcces;

                user.FlagAutorise = row.FlagAutorise;

                if (!row.IsDateDebutNull())
                    user.DateDebut = DateTime.Parse(row.DateDebut.Date.ToString("dd/MM/yyy HH:mm:ss"));


                if (!row.IsDateFinNull())
                    user.DateFin = DateTime.Parse(row.DateFin.Date.ToString("dd/MM/yyy HH:mm:ss"));

                if (!row.IsTelephoneNull())
                    user.Telephone = row.Telephone;


                if (!row.IsVilleNull())
                    user.Ville = row.Ville;

                ListUsers.Add(user);
            }



            return ListUsers;
        }

        /********************************************************************/
        /*    Renvoie la liste des Utilisateurs   Enrolés                          */
        /********************************************************************/

        public List<User> GetUsersEnroler(int Index, string matricule, string nom, string prenom, string numPermis, string rbautoriseValue, string rbenrolerValue, int? rbCoderValue)
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();

            List<User> ListUsers = new List<User>();

            MyDataSet.UsersDataTable TableUsers = null;


            TableUsers = UsersAdapter.GetUsersEnroler(Index, matricule, nom, prenom, numPermis, rbautoriseValue, rbenrolerValue, rbCoderValue);


            foreach (MyDataSet.UsersRow row in TableUsers)
            {
                User user = new User();


                if (!row.IsMatriculeNull())
                    user.Matricule = row.Matricule;

                if (!row.IsNomNull())
                    user.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    user.Prenom = row.Prenom;

                if (!row.IsNumBadgeNull())
                    user.NumBadge = row.NumBadge;

                if (!row.IsLibelleDroitNull())
                    user.LibelleDroit = row.LibelleDroit;

                if (!row.IsNumDroitAccesNull())
                    user.NumDroitAcces = row.NumDroitAcces;

                user.FlagAutorise = row.FlagAutorise;

                if (!row.IsDateDebutNull())
                    user.DateDebut = DateTime.Parse(row.DateDebut.Date.ToString("dd/MM/yyy HH:mm:ss"));


                if (!row.IsDateFinNull())
                    user.DateFin = DateTime.Parse(row.DateFin.Date.ToString("dd/MM/yyy HH:mm:ss"));

                if (!row.IsTelephoneNull())
                    user.Telephone = row.Telephone;


                if (!row.IsVilleNull())
                    user.Ville = row.Ville;

                ListUsers.Add(user);
            }



            return ListUsers;
        }

        /*********************************************************************/
        /* Renvoie un Chauffeur par sa Matricule (CIN)                       */
        /*********************************************************************/

        public User GetUnUser(string matricule)
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();

            MyDataSet.UsersDataTable TableUser = UsersAdapter.GetUserByMatricule(matricule);
            User user = new User();
            foreach (MyDataSet.UsersRow row in TableUser)
            {

                if (!row.IsMatriculeNull())
                    user.Matricule = row.Matricule;

                if (!row.IsNomNull())
                    user.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    user.Prenom = row.Prenom;

                if (!row.IsBioDataNull())
                    user.BioData = "1";

                if (!row.IsNumBadgeNull())
                    user.NumBadge = row.NumBadge;

                if (!row.IsNumServiceNull())
                    user.NumService = row.NumService;


                if (!row.IsNomServiceNull())
                    user.NomService = row.NomService;
                else
                    user.NomService = "";

                if (!row.IsLibelleDroitNull())
                    user.LibelleDroit = row.LibelleDroit;

                if (!row.IsNumDroitAccesNull())
                    user.NumDroitAcces = row.NumDroitAcces;


                user.FlagAutorise = row.FlagAutorise;

                if (!row.IsDateDebutNull())
                    user.DateDebut = DateTime.Parse(row.DateDebut.Date.ToString("dd/MM/yyy HH:mm:ss"));

                if (!row.IsDateFinNull())
                    user.DateFin = DateTime.Parse(row.DateFin.Date.ToString("dd/MM/yyy HH:mm:ss"));

                if (!row.IsDateCreatNull())
                    user.DateCreat = row.DateCreat;

                if (!row.IsDateModifNull())
                    user.DateModif = row.DateModif;

                if (!row.IsSexeNull())
                {
                    if (row.Sexe == 0)
                        user.Sexe = 0;
                    else
                        user.Sexe = 1;
                }

                if (!row.IsCiviliteNull())
                {
                    switch (row.Civilite)
                    {
                        case 0:
                            user.Civilite = 0;
                            break;
                        case 1:
                            user.Civilite = 1;
                            break;
                        case 2:
                            user.Civilite = 2;
                            break;
                        case 3:
                            user.Civilite = 3;
                            break;
                        case 4:
                            user.Civilite = 4;
                            break;
                        default:
                            user.Civilite = int.Parse("");
                            break;
                    }

                }


                if (!row.IsDateNaissanceNull() && row.DateNaissance.Length > 9)
                    user.DateNaissance = row.DateNaissance.Substring(0, 10);

                if (!row.IsLieuNaissNull())
                    user.LieuNaiss = row.LieuNaiss;

                if (!row.IsTelephoneNull())
                    user.Telephone = row.Telephone;

                if (!row.IsEMailNull())
                    user.EMail = row.EMail;

                if (!row.IsNumCINull())
                    user.NumCI = row.NumCI;

                if (!row.IsNumSSNull())
                    user.NumSS = row.NumSS;

                if (!row.IsAdresse1Null())
                    user.Adresse1 = row.Adresse1;

                if (!row.IsObservationsNull())
                    user.Observations = row.Observations;

              

                if (!row.IsCodePostalNull())
                    user.CodePostal = row.CodePostal;

                if (!row.IsVilleNull())
                    user.Ville = row.Ville;


                if (!row.IsUpdateLecNowNull())
                    user.UpdateLecNow = row.UpdateLecNow;

                if (!row.IsNumPermisNull())
                    user.NumPermis = row.NumPermis;

                if (!row.IsDatePermisNull())
                    user.DatePermis = row.DatePermis;

                if (!row.IsAncienNumBadgeNull())
                    user.AncienNumBadge = row.AncienNumBadge;

                if (!row.IsPhotoNull())
                    user.Photo = row.Photo;


            }

            return user;
        }

        /*********************************************************************/
        /*            Ajouter Un Utilisateur                                 */
        /*********************************************************************/

        public int AjouterUnUser(User item)
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();
            return UsersAdapter.InsertUser(MaxIdUser() + 1, item.Matricule, item.Nom, item.Prenom, item.NumService, item.Tel, item.Fax, item.FlagAutorise, item.DateDebut, item.DateFin, item.NumDroitAcces, item.NumBadge, item.NumCode, item.NbEmpreintes, item.Adresse1, item.Adresse2,
                       item.CodePostal, item.Ville, item.Telephone, item.Mobile, item.EMail, item.DateCreat, item.DateModif, item.DateNaissance, item.HeureMois, item.TauxNormal, item.TauxSup1, item.TauxSup2, item.Fonction, item.TauxSup3, item.Sexe,
                       item.Civilite, item.NumCI, item.NumSS, item.NumHoraire, item.CheckAutoDeclar, item.CheckBadge, item.CheckEmpreinte, item.CheckPin, item.CodePin, item.TauxSup0, item.TypeTemps, item.Modified, item.BadgeEncoded,
                       item.NumContrat, item.TypeUser, item.IdUser1, item.IdUser2, item.IdUser3, item.UpdateLecNow, item.JourRepos1, item.JourRepos2, item.NbJrsCongesAnnuels, item.LieuNaiss, item.DateCI, item.NumPermis, item.DatePermis,
                       item.AncienNumBadge, item.TypeAd, item.Mdp, item.UserLogin, item.CheckPointageWeb, item.Observations, item.DateFinMichoc, item.CheckBlocageJoursRepos, item.CheckPremierPointage); // Date:27/01/2017--By Zouhair LOUALID--------ajout de Commentaire------------

        }

        /*********************************************************************/
        /*            Modifier Un Utilisateur                                */
        /*********************************************************************/

        public int ModifierUnUser(User item)
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();
            return UsersAdapter.UpdateUser(item.Nom, item.Prenom, item.NumService, item.Tel, item.Fax, item.FlagAutorise, item.DateDebut, item.DateFin, item.NumDroitAcces, item.NumBadge, item.NumCode, item.NbEmpreintes, item.Adresse1, item.Adresse2,
                       item.CodePostal, item.Ville, item.Telephone, item.Mobile, item.EMail, item.DateCreat, item.DateModif, item.DateNaissance, item.HeureMois, item.TauxNormal, item.TauxSup1, item.TauxSup2, item.Fonction, item.TauxSup3, item.Sexe,
                       item.Civilite, item.NumCI, item.NumSS, item.NumHoraire, item.CheckAutoDeclar, item.CheckBadge, item.CheckEmpreinte, item.CheckPin, item.CodePin, item.TauxSup0, item.TypeTemps, item.Modified, item.BadgeEncoded,
                       item.NumContrat, item.TypeUser, item.IdUser1, item.IdUser2, item.IdUser3, item.UpdateLecNow, item.JourRepos1, item.JourRepos2, item.NbJrsCongesAnnuels, item.LieuNaiss, item.DateCI, item.NumPermis, item.DatePermis,
                       item.AncienNumBadge, item.TypeAd, item.Mdp, item.UserLogin, item.CheckPointageWeb, item.Observations, item.DateFinMichoc, item.CheckBlocageJoursRepos, item.CheckPremierPointage,  item.Matricule); // Date:27/01/2017--By Zouhair LOUALID--------ajout de Commentaire------------

        }

        /*********************************************************************/
        /*   Modifier FlagAutorise  d'Un Utilisateur                         */
        /*********************************************************************/

        public int ModifierFlagAutorise(string FlagAutorise, string Matricule)
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();
            return UsersAdapter.SetFlagAutorise(FlagAutorise, Matricule);
        }

        /*********************************************************************/
        /*           Supprimer Un Utilisateur                                */
        /*********************************************************************/

        public int SupprimerUnUser(string matricule)
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();
            return UsersAdapter.DeleteUnUser(matricule);
        }

        /*********************************************************************/
        /*           Get IdUser d'un Utilisateur                             */
        /*********************************************************************/

        public int GetIdUser(string matricule)
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();
            return int.Parse(UsersAdapter.GetIdUser(matricule).ToString());
        }

        /*********************************************************************/
        /*                  Max IdUser                                       */
        /*********************************************************************/

        public int MaxIdUser()
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();
            try
            {
                return UsersAdapter.GetMaxIdUser().Value;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /*********************************************************************/
        /*               Total des Chauffeurs                                */
        /*********************************************************************/

        public int getTotalUsers()
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();
            try
            {
                return UsersAdapter.GetTotalUsers().Value;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /*********************************************************************/
        /*               Total des Chauffeurs Absents                               */
        /*********************************************************************/

        public int getTotalUsersAbsents(DateTime? dateDebut, DateTime? dateFin)
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();
            try
            {
                int total = 0;
                total = int.Parse(UsersAdapter.GetTotalUsersAbsents(dateDebut, dateFin).ToString());
                return total;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /*********************************************************************/
        /*               Total des Chauffeurs Enrolé Avec Filtre             */
        /*********************************************************************/

        public int getTotalUsersByFiltre(string matricule, string nom, string prenom, string numPermis, string rbautoriseValue, string rbenrolerValue, int? rbCoderValue)
        {
            try
            {
                int total = 0;
                UsersTableAdapter UsersAdapter = new UsersTableAdapter();
                total = int.Parse(UsersAdapter.GetTotalUsersByFiltre(matricule, nom, prenom, numPermis, rbautoriseValue, rbenrolerValue, rbCoderValue).ToString());
                return total;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /*********************************************************************/
        /*               Total des Chauffeurs Absents Avec Filtre             */
        /*********************************************************************/

        public int getTotalUsersAbsentsByFiltre(DateTime? dateDebut, DateTime? dateFin, string numPermis, string matricule, string nom, string prenom)
        {
            try
            {
                int total = 0;
                UsersTableAdapter UsersAdapter = new UsersTableAdapter();
                total = int.Parse(UsersAdapter.GetTotalUsersAbsentsByFiltre(dateDebut, dateFin, numPermis, matricule, nom, prenom).ToString());
                return total;
            }
            catch (Exception)
            {
                return 0;
            }

        }

        /*********************************************************************/
        /*         savoir si un matricule existe                             */
        /*********************************************************************/

        public bool MatriculeExiste(string matricule)
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();

            if (UsersAdapter.MatriculeExiste(matricule).GetHashCode() == 1)
                return true;
            else
                return false;

        }

        /*********************************************************************/
        /*                Chauffeurs Absents                                  */
        /*********************************************************************/

        public List<User> getChauffeursAbsents(int IndexPage, DateTime? DateDebut, DateTime? DateFin, string numPermis, string matricule, string nom, string prenom)
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();

            List<User> ListAbsentsJours = new List<User>();

            MyDataSet.UsersDataTable TableAbsentsJours = UsersAdapter.GetChauffeursAbsents(IndexPage, DateDebut, DateFin, numPermis, matricule, nom, prenom);

            foreach (MyDataSet.UsersRow row in TableAbsentsJours)
            {
                User item = new User();

                item.Id = row.IdUser;

                if (!row.IsNumBadgeNull())
                    item.NumBadge = row.NumBadge;

                if (!row.IsMatriculeNull())
                    item.Matricule = row.Matricule;

                if (!row.IsNomNull())
                    item.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;

                ListAbsentsJours.Add(item);
            }


            return ListAbsentsJours;
        }

        /*********************************************************************/
        /*                Chauffeurs Absents  without index                                */
        /*********************************************************************/

        public List<User> getAllChauffeursAbsents(DateTime? DateDebut, DateTime? DateFin, string numPermis, string matricule, string nom, string prenom)
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();

            List<User> ListAbsentsJours = new List<User>();

            MyDataSet.UsersDataTable TableAbsentsJours = UsersAdapter.GetAllChauffeursAbsents(DateDebut, DateFin, numPermis, matricule, nom, prenom);

            foreach (MyDataSet.UsersRow row in TableAbsentsJours)
            {
                User item = new User();

                item.Id = row.IdUser;

                if (!row.IsNumBadgeNull())
                    item.NumBadge = row.NumBadge;

                if (!row.IsMatriculeNull())
                    item.Matricule = row.Matricule;

                if (!row.IsNomNull())
                    item.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;

                ListAbsentsJours.Add(item);
            }
            return ListAbsentsJours;
        }

        /*********************************************************************/
        /*           Identifications Période                                 */
        /*********************************************************************/

        public List<User> Identifications(DateTime Debut, DateTime Fin)
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();

            List<User> ListIdentifications = new List<User>();

            foreach (MyDataSet.UsersRow row in UsersAdapter.GetIdentificationsPeriode(Debut, Fin))
            {
                User item = new User();

                if (!row.IsNumBadgeNull())
                    item.NumBadge = row.NumBadge;

                if (!row.IsMatriculeNull())
                    item.Matricule = row.Matricule;

                if (!row.IsNomNull())
                    item.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;

                if (!row.IsDateDebutNull())
                    item.DateDebut = row.DateDebut;
                else
                    item.DateDebut = null;

                if (!row.IsDateFinNull() && (row.DateFin != row.DateDebut))
                    item.DateFin = row.DateFin;
                else
                    item.DateFin = null;

                ListIdentifications.Add(item);
            }

            return ListIdentifications;
        }

        /*********************************************************************/
        /*           Envoie Lecteur / Mettre à jour le lecteur               */
        /*********************************************************************/

        public int EnvoieAuLecteur(string matricule)
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();
            return UsersAdapter.UpdateLecteurNow(matricule);

        }

        /*********************************************************************/
        /*           Get  Numéro de Permis par Matricule              */
        /*********************************************************************/

        public string getNumPermis(string matricule)
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();
            return UsersAdapter.GetNumPermis(matricule).ToString();
        }

        /*********************************************************************/
        /*           Get Matricule parNuméro de Permis               */
        /*********************************************************************/

        public string getMatricule(string numPermis)
        {
            UsersTableAdapter UsersAdapter = new UsersTableAdapter();
            return UsersAdapter.GetMatricule(numPermis).ToString();

        }

    }
}
