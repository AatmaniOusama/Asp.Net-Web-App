using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLockTooTaxi.MyDataSetTableAdapters;

namespace DataLockTooTaxi
{
    public class ControleUser
    {
        public int Id {get;set;}
        public DateTime DateCreation {get;set;} 
        public int IdOpCreation {get;set;} 
        public DateTime DateModif {get;set;} 
        public int IdOpModif {get;set;} 
        public string DateFin {get;set;} 
        public int IdLabelControle {get;set;} 
        public string  AbrevLabelControle {get;set;}  
        public int IdUser {get;set;}  
        public string  Matricule {get;set;}
        public string NumPermis { get; set; } 
        public string  Nom {get;set;}
        public string Prenom { get; set; }

        public ControleUser()
        {

            Id = -1;
            DateCreation = DateTime.Now;
            IdOpCreation = -1;
            DateModif = DateTime.Now;
            IdOpModif = -1;
            DateFin = DateTime.Now.ToString().Substring(0, 10);
            IdLabelControle = -1;
            AbrevLabelControle = "";
            IdUser = -1;
            Matricule = "";
            NumPermis = "";
            Nom = "";
            Prenom = "";
        }

        public List<ControleUser> getControleUsers()
        {
            ControleUserTableAdapter ControleUserAdapter = new ControleUserTableAdapter();
            List<ControleUser> ListControleUser = new List<ControleUser>();

            foreach (MyDataSet.ControleUserRow row in ControleUserAdapter.getControleUser())
            {
                ControleUser item = new ControleUser();


                    
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

                    if (!row.IsAbrevNull())
                    item.AbrevLabelControle = row.Abrev;

                    if (!row.IsIdUserNull())
                    item.IdUser = row.IdUser;

                    if (!row.IsMatriculeNull())
                    item.Matricule = row.Matricule;

                    if (!row.IsNumBadgeNull())
                        item.NumPermis = row.NumBadge;

                    if (!row.IsNomNull())
                    item.Nom = row.Nom;

                    if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;

                    ListControleUser.Add(item);
               
            }

            return ListControleUser;
        }


        /*******************************************************/
        /*               la liste des ControleUser chauffeurs  */
        /*******************************************************/

        public List<ControleUser> getAllControlesUser(int IndexPage, int? ddlAbrevControleUser, DateTime? dateFin, string numPermis, string matricule, string nom, string prenom)
        {


            ControleUserTableAdapter ControleUserAdapter = new ControleUserTableAdapter();
            MyDataSet.ControleUserDataTable TableControleUser = ControleUserAdapter.GetAllControlesUser(IndexPage, ddlAbrevControleUser, dateFin, numPermis, matricule, nom, prenom);
            List<ControleUser> ListControleUser = new List<ControleUser>();

            foreach (MyDataSet.ControleUserRow row in TableControleUser)
            {
                ControleUser item = new ControleUser();

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

                if (!row.IsAbrevNull())
                    item.AbrevLabelControle = row.Abrev;

                if (!row.IsIdUserNull())
                    item.IdUser = row.IdUser;

                if (!row.IsMatriculeNull())
                    item.Matricule = row.Matricule;

                if (!row.IsNumBadgeNull())
                    item.NumPermis = row.NumBadge;

                if (!row.IsNomNull())
                    item.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;

                ListControleUser.Add(item);

            }

            return ListControleUser;
        }

        /**********************************************************************************/
        /*               la liste des ControleUser Taxis avec filtre (pour le trie)          */
        /**********************************************************************************/

        public List<ControleUser> getAllControleUserByFiltre(int? ddlAbrevControleUser, DateTime? dateFin, string numPermis, string matricule, string nom, string prenom)
        {


            ControleUserTableAdapter ControleUserAdapter = new ControleUserTableAdapter();
            MyDataSet.ControleUserDataTable TableControleUser = ControleUserAdapter.GetAllControleUserByFiltre(ddlAbrevControleUser, dateFin, numPermis, matricule, nom, prenom);
            List<ControleUser> ListControleUser = new List<ControleUser>();

            foreach (MyDataSet.ControleUserRow row in TableControleUser)
            {
                ControleUser item = new ControleUser();

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

                if (!row.IsAbrevNull())
                    item.AbrevLabelControle = row.Abrev;

                if (!row.IsIdUserNull())
                    item.IdUser = row.IdUser;

                if (!row.IsMatriculeNull())
                    item.Matricule = row.Matricule;

                if (!row.IsNumBadgeNull())
                    item.NumPermis = row.NumBadge;

                if (!row.IsNomNull())
                    item.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;

                ListControleUser.Add(item);

            }

            return ListControleUser;
        }




        public int AjouterControleUser(ControleUser item)
        {
            ControleUserTableAdapter ControleUserAdapter = new ControleUserTableAdapter();
            return ControleUserAdapter.InsertControleUser(DateTime.Now, item.IdOpCreation, DateTime.Now, item.IdOpModif, DateTime.Parse(item.DateFin), item.IdLabelControle, item.IdUser);

        }

        public int ModifierUnControleuser(DateTime Datefin, int IdOperateur,int IdUserControle)
        {
            ControleUserTableAdapter ControleUserAdapter = new ControleUserTableAdapter();
            return ControleUserAdapter.UpdateControleUser(DateTime.Now, IdOperateur, Datefin, IdUserControle);
        }

        public int SupprimerUnControleuser(int IdUserControle)
        {
            ControleUserTableAdapter ControleUserAdapter = new ControleUserTableAdapter();
            return ControleUserAdapter.DeleteControleUser(IdUserControle);
        }


        /************************************************/
        /*         savoir si un Controle User existe       */
        /************************************************/

        public bool controleUserExiste(string matricule, string Libelle)
        {
            ControleUserTableAdapter ControleUserAdapter = new ControleUserTableAdapter();

            if (ControleUserAdapter.ControleUserExiste(matricule, Libelle).GetHashCode() == 1)
                return true;
            else
                return false;

        }

        /*********************************************************************/
        /*               Total des Controles chauffeur                                */
        /*********************************************************************/

        public int getTotalControlesUser()
        {
            ControleUserTableAdapter ControleUserAdapter = new ControleUserTableAdapter();
            try
            {
                return int.Parse(ControleUserAdapter.GetTotalControlesUser().ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /*********************************************************************/
        /*               Total des Controles chauffeur  Avec Filtre             */
        /*********************************************************************/

        public int getTotalControlesUserByFiltre(int? ddlAbrevControleUser, DateTime? dateFin, string numPermis, string matricule, string nom, string prenom)
        {
            try
            {
                int total = 0;

                ControleUserTableAdapter ControleUserAdapter = new ControleUserTableAdapter();

                total = int.Parse(ControleUserAdapter.GetTotalControlesUserByFiltre(ddlAbrevControleUser, dateFin, numPermis, matricule, nom, prenom).ToString());

                return total;

            }
            catch (Exception)
            {
                return 0;
            }

        }





    }
}
