using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLockTooTaxi.MyDataSetTableAdapters;

namespace DataLockTooTaxi
{
    public class Visiteur
    {
        public int Id { get; set; }
        public string Matricule { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int Numservice { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string FlagAutorise { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public int NumDroitAcces { get; set; }
        public string NumBadge { get; set; }
        public int NbEmpreintes { get; set; }
        public string Adresse1 { get; set; }
        public string Adresse2 { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string EMail { get; set; }
        public DateTime DateCreat { get; set; }
        public DateTime DateModif { get; set; }
        public string DateNaissance { get; set; }
        public int HeureMois { get; set; }
        public int TauxNormal { get; set; }
        public int TauxSup1 { get; set; }
        public int TauxSup2 { get; set; }
        public string Fonction { get; set; }
        public int TauxSup3 { get; set; }
        public string Sexe { get; set; }
        public string Civilite { get; set; }
        public string NumCI { get; set; }
        public string NumSS { get; set; }
        public int NumHoraire { get; set; }
        public int CodePin { get; set; }
        public bool CheckEmpreinte { get; set; }
        public bool CheckBadge { get; set; }
        public bool CheckPin { get; set; }
        public bool CheckAutoDeclar { get; set; }
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
        public string Motif { get; set; }
        public int IdMotif { get; set; }

        public byte[] Photo { get; set; }

        int Total { get; set; }
        public string NomComplet { get; set; }

        public Visiteur()
        {
            Id = -1;
            Matricule = "";
            Nom = "";
            Prenom = "";
            Numservice = 0;
            Tel = null;
            Fax = null;
            FlagAutorise = "Y";
            DateDebut = DateTime.Now;
            DateFin = DateTime.Now.AddYears(10);
            NumDroitAcces = 1;
            NumBadge = null;
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
            Sexe = "1";
            Civilite = "0";
            NumCI = null;
            NumSS = null;
            NumHoraire = 0;
            CodePin = 0;
            CheckEmpreinte = true;
            CheckBadge = false;
            CheckPin = false;
            CheckAutoDeclar = false;
            TauxSup0 = 0;
            TypeTemps = 0;
            Modified = true;
            BadgeEncoded = false;
            NumContrat = 0;
            TypeUser = 3;
            IdUser1 = 0;
            IdUser2 = 0;
            IdUser3 = 0;
            UpdateLecNow = true;
            JourRepos1 = 0;
            JourRepos2 = 0;
            Motif = null;
            IdMotif = 0;
        }

        public override string ToString()   // sert pour le remplissage du _DDLNom par Nom et Prenom 
        {
            return Nom + " " + Prenom;
        }

        public List<Visiteur> getAllVisiteurs()
        {
            VisiteursTableAdapter VisiteursAdapter = new VisiteursTableAdapter();
            List<Visiteur> ListVisiteurs = new List<Visiteur>();
            MyDataSet.VisiteursDataTable TableVisiteurs = VisiteursAdapter.GetAllVisiteurs();
            foreach (MyDataSet.VisiteursRow row in TableVisiteurs)
            {
                Visiteur item = new Visiteur();
                item.Id = row.IdUser;
                item.Matricule = row.Matricule;
                if (!row.IsNomNull())
                    item.Nom = row.Nom;
                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;
                item.FlagAutorise = row.FlagAutorise;
                if (!row.IsDateDebutNull())
                    item.DateDebut = row.DateDebut;
                if (!row.IsDateFinNull())
                    item.DateFin = row.DateFin;
                ListVisiteurs.Add(item);
            }
            return ListVisiteurs;

        }
        /************************************************/
        /*    Afficher la liste des Noms Complets       */
        /************************************************/
        public List<Visiteur> GetNomsComplets()
        {
            VisiteursTableAdapter VisiteursAdapter = new VisiteursTableAdapter();

            List<Visiteur> ListVisiteurs = new List<Visiteur>();
            MyDataSet.VisiteursDataTable TableVisiteurs = VisiteursAdapter.GetNomsComplets();
            foreach (MyDataSet.VisiteursRow row in TableVisiteurs)
            {
                Visiteur Visiteur = new Visiteur();

                Visiteur.Id = row.IdUser;
                Visiteur.Matricule = row.Matricule;

                if (!row.IsNomNull())
                    Visiteur.NomComplet = Visiteur.Nom = row.Nom;
                Visiteur.NomComplet += " ";

                if (!row.IsPrenomNull())
                    Visiteur.NomComplet += Visiteur.Prenom = row.Prenom;

                ListVisiteurs.Add(Visiteur);
            }

            return ListVisiteurs;
        }


        public string getNomByMatricule(string matricule)
        {
            Visiteur item = new Visiteur();
            VisiteursTableAdapter VisiteursAdapter = new VisiteursTableAdapter();
            MyDataSet.VisiteursDataTable TableVisiteurs = new MyDataSet.VisiteursDataTable();
            TableVisiteurs = VisiteursAdapter.GetNomByMatricule(matricule);
            foreach (MyDataSet.VisiteursRow row in TableVisiteurs)
            {
                if (!row.IsNomNull())
                    item.Nom = row.Nom;
            }

            return item.Nom;

        }




        /************************************************/
        /*    Afficher la liste des  Agents        */
        /************************************************/

        public List<Visiteur> getVisiteurs(int Index, string matricule, string nom, string rbautoriseValue, string rbenrolerValue, string sortColonne, string sortOrder)
        {
            VisiteursTableAdapter VisiteursAdapter = new VisiteursTableAdapter();

            List<Visiteur> ListVisiteurs = new List<Visiteur>();

            MyDataSet.VisiteursDataTable TableVisiteurs = null;

            if (rbenrolerValue == "1")
            {
                if (sortColonne == "Matricule")
                {
                    TableVisiteurs = VisiteursAdapter.GetVisiteursEnroleOrderByMatricule(Index, matricule, nom, rbautoriseValue, sortOrder);
                }
                if (sortColonne == "Nom")
                {
                    TableVisiteurs = VisiteursAdapter.GetVisiteursEnroleOrderByNom(Index, matricule, nom, rbautoriseValue, sortOrder);
                }
                if (sortColonne == "Prenom")
                {
                    TableVisiteurs = VisiteursAdapter.GetVisiteursEnroleOrderByPrenom(Index, matricule, nom, rbautoriseValue, sortOrder);
                }
                if (sortColonne == "DateDebut")
                {
                    TableVisiteurs = VisiteursAdapter.GetVisiteursEnroleOrderByDateDebut(Index, matricule, nom, rbautoriseValue, sortOrder);
                }
                if (sortColonne == "DateFin")
                {
                    TableVisiteurs = VisiteursAdapter.GetVisiteursEnroleOrderByDateFin(Index, matricule, nom, rbautoriseValue, sortOrder);
                }
            }
            if (rbenrolerValue == "0")
            {
                if (sortColonne == "Matricule")
                {
                    TableVisiteurs = VisiteursAdapter.GetVisiteursNonEnroleOrderByMatricule(Index, matricule, nom, rbautoriseValue, sortOrder);
                }
                if (sortColonne == "Nom")
                {
                    TableVisiteurs = VisiteursAdapter.GetVisiteursNonEnroleOrderByNom(Index, matricule, nom, rbautoriseValue, sortOrder);
                }
                if (sortColonne == "Prenom")
                {
                    TableVisiteurs = VisiteursAdapter.GetVisiteursNonEnroleOrderByPrenom(Index, matricule, nom, rbautoriseValue, sortOrder);
                }
                if (sortColonne == "DateDebut")
                {
                    TableVisiteurs = VisiteursAdapter.GetVisiteursNonEnroleOrderByDateDebut(Index, matricule, nom, rbautoriseValue, sortOrder);
                }
                if (sortColonne == "DateFin")
                {
                    TableVisiteurs = VisiteursAdapter.GetVisiteursNonEnroleOrderByDateFin(Index, matricule, nom, rbautoriseValue, sortOrder);
                }
            }
            if (rbenrolerValue == "-1")
            {
                if (sortColonne == "Matricule")
                {
                    TableVisiteurs = VisiteursAdapter.GetVisiteursOrderByMatricule(Index, matricule, nom, rbautoriseValue, sortOrder);
                }
                if (sortColonne == "Nom")
                {
                    TableVisiteurs = VisiteursAdapter.GetVisiteursOrderByNom(Index, matricule, nom, rbautoriseValue, sortOrder);
                }
                if (sortColonne == "Prenom")
                {
                    TableVisiteurs = VisiteursAdapter.GetVisiteursOrderByPrenom(Index, matricule, nom, rbautoriseValue, sortOrder);
                }
                if (sortColonne == "DateDebut")
                {
                    TableVisiteurs = VisiteursAdapter.GetVisiteursOrderByDateDebut(Index, matricule, nom, rbautoriseValue, sortOrder);
                }
                if (sortColonne == "DateFin")
                {
                    TableVisiteurs = VisiteursAdapter.GetVisiteursOrderByDateFin(Index, matricule, nom, rbautoriseValue, sortOrder);
                }
            }

            foreach (MyDataSet.VisiteursRow row in TableVisiteurs)
            {
                Visiteur item = new Visiteur();
                item.Id = row.IdUser;

                item.Matricule = row.Matricule;

                if (!row.IsNomNull())
                    item.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;

                item.FlagAutorise = row.FlagAutorise;

                if (!row.IsDateDebutNull())
                    item.DateDebut = row.DateDebut;

                if (!row.IsDateFinNull())
                    item.DateFin = row.DateFin;

                ListVisiteurs.Add(item);
            }

            return ListVisiteurs;
        }

        /************************************************/
        /*    Afficher la liste des Agents              */
        /************************************************/

        public List<Visiteur> getVisiteurs(int Index, string matricule, string nom, string rbautoriseValue, string rbenrolerValue)
        {
            VisiteursTableAdapter VisiteursAdapter = new VisiteursTableAdapter();

            List<Visiteur> ListVisiteurs = new List<Visiteur>();

            MyDataSet.VisiteursDataTable TableVisiteurs = null;

            if (rbenrolerValue == "1")
            {
                TableVisiteurs = VisiteursAdapter.GetVisiteursEnrole(Index, matricule, nom, rbautoriseValue);
            }
            if (rbenrolerValue == "0")
            {
                TableVisiteurs = VisiteursAdapter.GetVisiteursNonEnrole(Index, matricule, nom, rbautoriseValue);
            }
            if (rbenrolerValue == "-1")
            {
                TableVisiteurs = VisiteursAdapter.GetVisiteurs(Index, matricule, nom, rbautoriseValue);
            }
            foreach (MyDataSet.VisiteursRow row in TableVisiteurs)
            {
                Visiteur item = new Visiteur();
                item.Id = row.IdUser;

                item.Matricule = row.Matricule;

                if (!row.IsNomNull())
                    item.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;

                item.FlagAutorise = row.FlagAutorise;

                if (!row.IsDateDebutNull())
                    item.DateDebut = row.DateDebut;

                if (!row.IsDateFinNull())
                    item.DateFin = row.DateFin;

                ListVisiteurs.Add(item);
            }

            return ListVisiteurs;
        }

        /***********************************************************/
        /*    Afficher la liste des Agents  Pour l'Export      */
        /*************************************************************/

        public List<Visiteur> getVisiteursToExport(string matricule, string nom, string rbautoriseValue, string rbenrolerValue, string sortColonne)
        {
            VisiteursTableAdapter VisiteursAdapter = new VisiteursTableAdapter();

            List<Visiteur> ListVisiteurs = new List<Visiteur>();
            MyDataSet.VisiteursDataTable TableVisiteurs = null;

            if (rbenrolerValue == "1")
            {
                TableVisiteurs = VisiteursAdapter.GetVisiteursEnroleToExportASC(matricule, nom, rbautoriseValue);
            }
            if (rbenrolerValue == "0")
            {
                TableVisiteurs = VisiteursAdapter.GetVisiteursNonEnroleToExportASC(matricule, nom, rbautoriseValue);
            }
            if (rbenrolerValue == "-1")
            {
                TableVisiteurs = VisiteursAdapter.GetVisiteursToExportASC(matricule, nom, rbautoriseValue);
            }

            foreach (MyDataSet.VisiteursRow row in TableVisiteurs)
            {
                Visiteur item = new Visiteur();
                item.Id = row.IdUser;

                item.Matricule = row.Matricule;

                if (!row.IsNomNull())
                    item.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;

                item.FlagAutorise = row.FlagAutorise;

                if (!row.IsDateDebutNull())
                    item.DateDebut = row.DateDebut;

                if (!row.IsDateFinNull())
                    item.DateFin = row.DateFin;

                ListVisiteurs.Add(item);
            }

            return ListVisiteurs;
        }


        /************************************************/
        /* affiche Agent qui a la  Matricule @var */
        /************************************************/

        public Visiteur GetUnVisiteur(string matricule)
        {
            VisiteursTableAdapter VisiteursAdapter = new VisiteursTableAdapter();

            MyDataSet.VisiteursDataTable TableVisiteur = VisiteursAdapter.GetVisiteurByMatricule(matricule);
            Visiteur item = new Visiteur();
            foreach (MyDataSet.VisiteursRow row in TableVisiteur)
            {

                item.Id = row.IdUser;

                item.Matricule = row.Matricule;

                if (!row.IsNomNull())
                    item.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;

                // if (row.FlagAutorise != "Y")
                item.FlagAutorise = row.FlagAutorise;

                if (!row.IsDateDebutNull())
                    item.DateDebut = row.DateDebut;

                if (!row.IsDateFinNull())
                    item.DateFin = row.DateFin;

                if (!row.IsDateCreatNull())
                    item.DateCreat = row.DateCreat;

                if (!row.IsDateModifNull())
                    item.DateModif = row.DateModif;

                if (!row.IsSexeNull())
                {
                    if (row.Sexe == 0)
                        item.Sexe = "0";
                    else
                        item.Sexe = "1";
                }


                if (!row.IsCiviliteNull())
                {
                    switch (row.Civilite)
                    {
                        case 0:
                            item.Civilite = "Célibataire";
                            break;
                        case 1:
                            item.Civilite = "Marié";
                            break;
                        case 2:
                            item.Civilite = "Divorcé";
                            break;
                        case 3:
                            item.Civilite = "Veuf";
                            break;
                        case 4:
                            item.Civilite = "Pacsé";
                            break;
                        default:
                            item.Civilite = "";
                            break;
                    }
                }

                if (!row.IsFonctionNull())
                    item.Fonction = row.Fonction;

                if (!row.IsDateNaissanceNull() && row.DateNaissance.Length > 9)
                    item.DateNaissance = row.DateNaissance.Substring(0, 10);


                if (!row.IsTelephoneNull())
                    item.Telephone = row.Telephone;

                if (!row.IsEMailNull())
                    item.EMail = row.EMail;

                if (!row.IsNumCINull())
                    item.NumCI = row.NumCI;

                if (!row.IsNumSSNull())
                    item.NumSS = row.NumSS;

                if (!row.IsAdresse1Null())
                    item.Adresse1 = row.Adresse1;

                if (!row.IsCodePostalNull())
                    item.CodePostal = row.CodePostal;

                if (!row.IsVilleNull())
                    item.Ville = row.Ville;

                if (!row.IsCheckBadgeNull())
                    item.CheckBadge = row.CheckBadge;

                if (!row.IsCheckEmpreinteNull())
                    item.CheckEmpreinte = row.CheckEmpreinte;

                if (!row.IsCheckPinNull())
                    item.CheckPin = row.CheckPin;

                if (!row.IsCodePinNull())
                    item.CodePin = row.CodePin;

                if (!row.IsCheckAutoDeclarNull())
                    item.CheckAutoDeclar = row.CheckAutoDeclar;



                if (!row.IsNbEmpreintesNull())
                    item.NbEmpreintes = row.NbEmpreintes;

                if (!row.IsModifiedNull())
                    item.Modified = row.Modified;

                if (!row.IsUpdateLecNowNull())
                    item.UpdateLecNow = row.UpdateLecNow;

            }

            return item;
        }

        /************************************************/
        /*            Ajouter Un Agent           */
        /************************************************/

        public int AjouterUnVisiteur(Visiteur item)
        {
            VisiteursTableAdapter VisiteursAdapter = new VisiteursTableAdapter();
            return VisiteursAdapter.InsertVisiteur(MaxIdVisiteur() + 1, item.Matricule, item.Nom, item.Prenom, item.Numservice, item.Tel, item.Fax, item.FlagAutorise, item.DateDebut, item.DateFin, item.NumDroitAcces, item.NumBadge, item.NbEmpreintes, item.Adresse1, item.Adresse2, item.CodePostal, item.Ville, item.Telephone, item.Mobile, item.EMail, item.DateCreat, item.DateModif, item.DateNaissance, item.HeureMois, item.TauxNormal, item.TauxSup1, item.TauxSup2, item.Fonction, item.TauxSup3, int.Parse(item.Sexe), int.Parse(item.Civilite), item.NumCI, item.NumSS, item.NumHoraire, item.CodePin, item.CheckEmpreinte, item.CheckBadge, item.CheckPin, item.CheckAutoDeclar, item.TauxSup0, item.TypeTemps, item.Modified, item.BadgeEncoded, item.NumContrat, item.TypeUser, item.IdUser1, item.IdUser2, item.IdUser3, item.UpdateLecNow, item.JourRepos1, item.JourRepos2, item.Motif, item.IdMotif);


        }


        /************************************************/
        /*            Modifier Un Agent           */
        /************************************************/

        public int ModifierUnVisiteur(Visiteur item)
        {
            VisiteursTableAdapter VisiteursAdapter = new VisiteursTableAdapter();
            return VisiteursAdapter.UpdateVisiteur(item.Nom, item.Prenom, item.Numservice, item.Tel, item.Fax, item.FlagAutorise, item.DateDebut, item.DateFin, item.NumDroitAcces, item.NumBadge, item.NbEmpreintes, item.Adresse1, item.Adresse2, item.CodePostal, item.Ville, item.Telephone, item.Mobile, item.EMail, item.DateCreat, item.DateModif, item.DateNaissance, item.HeureMois, item.TauxNormal, item.TauxSup1, item.TauxSup2, item.Fonction, item.TauxSup3, int.Parse(item.Sexe), int.Parse(item.Civilite), item.NumCI, item.NumSS, item.NumHoraire, item.CodePin, item.CheckEmpreinte, item.CheckBadge, item.CheckPin, item.CheckAutoDeclar, item.TauxSup0, item.TypeTemps, item.Modified, item.BadgeEncoded, item.NumContrat, item.TypeUser, item.IdUser1, item.IdUser2, item.IdUser3, item.UpdateLecNow, item.JourRepos1, item.JourRepos2, item.Motif, item.IdMotif, item.Matricule);
        }

        /************************************************/
        /*   Modifier FlagAutorise  d'Un Utilisateur    */
        /************************************************/

        public int ModifierFlagAutorise(string FlagAutorise, string Matricule)
        {
            VisiteursTableAdapter VisiteursAdapter = new VisiteursTableAdapter();
            return VisiteursAdapter.SetFlagAutorise(FlagAutorise, Matricule);
        }

        /************************************************/
        /*           Supprimer Un Agent           */
        /************************************************/

        public int SupprimerUnVisiteur(string matricule)
        {
            VisiteursTableAdapter VisiteursAdapter = new VisiteursTableAdapter();
            return VisiteursAdapter.DeleteUnVisiteur(matricule);
        }

        /************************************************/
        /*           Get IdVisiteur d'un Agent           */
        /************************************************/

        public int GetIdVisiteur(string matricule)
        {
            VisiteursTableAdapter VisiteursAdapter = new VisiteursTableAdapter();
            return int.Parse(VisiteursAdapter.GetIdUser(matricule).ToString());
        }


        /************************************************/
        /*                  Max IdVisiteur                  */
        /************************************************/

        public int MaxIdVisiteur()
        {
            VisiteursTableAdapter VisiteursAdapter = new VisiteursTableAdapter();
            try
            {
                return int.Parse(VisiteursAdapter.GetMaxIdUser().ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /************************************************/
        /*               Total des Agents                 */
        /************************************************/

        public int getTotalVisiteurs()
        {
            VisiteursTableAdapter VisiteursAdapter = new VisiteursTableAdapter();
            try
            {
                return int.Parse(VisiteursAdapter.GetTotalVisiteurs().ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }



        /*************************************************************/
        /*               Total des Agents Enrolé Avec Filtre     */
        /*************************************************************/

        public int getTotalVisiteursByFiltre(string matricule, string nom, string rbautoriseValue, string rbenrolerValue)
        {
            try
            {
                int total = 0;

                VisiteursTableAdapter VisiteursAdapter = new VisiteursTableAdapter();

                if (rbenrolerValue == "-1")
                {
                    total = int.Parse(VisiteursAdapter.GetTotalVisiteursByFiltre(matricule, nom, rbautoriseValue).ToString());

                }
                if (rbenrolerValue == "0")
                {
                    total = int.Parse(VisiteursAdapter.GetTotalVisiteursNonEnroleByFiltre(matricule, nom, rbautoriseValue).ToString());

                }
                if (rbenrolerValue == "1")
                {
                    total = int.Parse(VisiteursAdapter.GetTotalVisiteursEnroleByFiltre(matricule, nom, rbautoriseValue).ToString());

                }

                return total;

            }
            catch (Exception)
            {
                return 0;
            }

        }

        /************************************************/
        /*         savoir si un matricule existe        */
        /************************************************/

        public bool MatriculeExiste(string matricule)
        {
            VisiteursTableAdapter VisiteursAdapter = new VisiteursTableAdapter();
            if (VisiteursAdapter.MatriculeExiste(matricule).GetHashCode() == 1)
                return true;
            else
                return false;
        }

        public int EnvoieAuLecteur(string matricule)
        {
            VisiteursTableAdapter VisiteursAdapter = new VisiteursTableAdapter();
            return VisiteursAdapter.UpdateLecteurNow(matricule);
        }
    }
}
