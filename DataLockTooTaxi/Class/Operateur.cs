using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using DataLockTooTaxi.MyDataSetTableAdapters;

namespace DataLockTooTaxi
{
    public class Operateur
    {
        [DllImport("LK2DLL.dll", CharSet = CharSet.Ansi,CallingConvention=CallingConvention.Cdecl)] //Importer la DLL LK2DLL
        public static extern void Decrypt_String(string csStrCrypted, bool bNewVersion,StringBuilder StrDecrypted);
        public const int SIZE_CRYPT_BUF = 200; //Taille max d'un mot de passe cryptée



   
         public int         ID             {get; set;} 
         public string      Nom            {get; set;}  
         public string      Prenom         {get; set;} 
         public DateTime    DateCreation   {get; set;}  
         public int         IDCreateur     {get; set;}  
         public DateTime    Debut          {get; set;}
         public DateTime    Fin            {get; set;} 
         public string      Profil         {get; set;} 
         public string      Login          {get; set;}  
         public string      MotPasse       {get; set;} 
         public string      RefEmpreinte   {get; set;} 
         public string      NomService     {get; set;} 
         public string      MenusAutorises {get; set;}

       

         public Operateur()
         {
             ID = -1;
             Nom = "";
             Prenom = "";
             DateCreation = DateTime.Now;
             IDCreateur = 0;
             Debut = DateTime.Now;
             Fin = DateTime.Now;
             Profil = "";
             Login = "";
             MotPasse = "";
             RefEmpreinte = null;
             NomService = "";
             MenusAutorises="";

    
         }

         public override string ToString()   // sert pour le remplissage du DDLOperateurs par Nom et Prenom 
         {
             return Nom + " " + Prenom;
         }

         public List<Operateur> GetOerateurs()
         {
             OperateursTableAdapter OperateurAdapter = new OperateursTableAdapter();
             List<Operateur> ListOperateurs = new List<Operateur>();
             MyDataSet.OperateursDataTable TableOperateurs = OperateurAdapter.GetOperateurs();

             foreach (MyDataSet.OperateursRow row in TableOperateurs)
             {
                 Operateur item = new Operateur();

                 item.ID = row.ID;

                 if(!row.IsNomNull())
                 item.Nom =row.Nom;

                 if (!row.IsPrenomNull())
                 item.Prenom =row.Prenom;

                 if (!row.IsDateCreationNull())
                 item.DateCreation = row.DateCreation;

                 if (!row.IsIDCreateurNull())
                 item.IDCreateur = row.IDCreateur;

                 if (!row.IsDebutNull())
                 item.Debut = row.Debut;

                 if (!row.IsProfilNull())
                 item.Profil =row.Profil;

                 if (!row.IsLoginNull())
                 item.Login =row.Login;

                 if (!row.IsMotPasseNull())
                 item.MotPasse =row.MotPasse;

                 if(!row.IsRefEmpreinteNull())
                    item.RefEmpreinte =row.RefEmpreinte;

                 if (!row.IsNomServiceNull())
                 item.NomService =row.NomService;

                 if (!row.IsMenusAutorisesNull())
                 item.MenusAutorises = row.MenusAutorises;

                 ListOperateurs.Add(item);
             }

             return ListOperateurs;
             
         }

         public Operateur GetOperateurByLogin(string Login, string MotPasse)
         {
             OperateursTableAdapter OperateurAdapter = new OperateursTableAdapter();
             MyDataSet.OperateursDataTable TableOperateurs = OperateurAdapter.GetOperateurByLogin(Login);
             Operateur item = new Operateur();
             StringBuilder StrDecrypted = new StringBuilder(SIZE_CRYPT_BUF);

             foreach (MyDataSet.OperateursRow row in TableOperateurs)
             {
                 Decrypt_String(row.MotPasse,true, StrDecrypted);
                 
                 if (MotPasse == StrDecrypted.ToString())
                 {
                     item.ID = row.ID;
                     if (!row.IsNomNull())
                         item.Nom = row.Nom;

                     if (!row.IsPrenomNull())
                         item.Prenom = row.Prenom;

                     if (!row.IsDateCreationNull())
                         item.DateCreation = row.DateCreation;

                     if (!row.IsIDCreateurNull())
                         item.IDCreateur = row.IDCreateur;

                     if (!row.IsDebutNull())
                         item.Debut = row.Debut;

                     if (!row.IsProfilNull())
                         item.Profil = row.Profil;

                     if (!row.IsLoginNull())
                         item.Login = row.Login;

                     item.MotPasse = StrDecrypted.ToString();

                     if (!row.IsRefEmpreinteNull())
                        item.RefEmpreinte = row.RefEmpreinte;

                     if (!row.IsNomServiceNull())
                         item.NomService = row.NomService;

                     if (!row.IsMenusAutorisesNull())
                         item.MenusAutorises = row.MenusAutorises;
                 }

             }

             return item;
         }


         /************************************************/
         /*    Afficher la liste des Operateurs        */
         /************************************************/

         public List<Operateur> GetOperateursByFiltre(int Index, string nom, string prenom, string ddlprofilValue, string ddlserviceValue, string sortColonne, string sortOrder)
         {
             OperateursTableAdapter OperateurAdapter = new OperateursTableAdapter();

             List<Operateur> ListOperateurs = new List<Operateur>();



             MyDataSet.OperateursDataTable TableOperateurs = null;

             TableOperateurs = OperateurAdapter.GetOperateursByFiltre(Index, nom, prenom, ddlprofilValue, ddlserviceValue, sortColonne, sortOrder);
                
                 
             
             foreach (MyDataSet.OperateursRow row in TableOperateurs)
             {
                 Operateur item = new Operateur();
                 item.ID = row.ID;

                 if (!row.IsNomNull())
                     item.Nom = row.Nom;

                 if (!row.IsPrenomNull())
                     item.Prenom = row.Prenom;

                 if (!row.IsDateCreationNull())
                     item.DateCreation = row.DateCreation;

                 if (!row.IsIDCreateurNull())
                     item.IDCreateur = row.IDCreateur;

                 if (!row.IsDebutNull())
                     item.Debut = row.Debut;

                 if (!row.IsFinNull())
                     item.Fin = row.Fin;

                 if (!row.IsProfilNull())
                     item.Profil = row.Profil;

                 if (!row.IsLoginNull())
                     item.Login = row.Login;

                 if (!row.IsMotPasseNull())
                     item.MotPasse = row.MotPasse;

                 if (!row.IsRefEmpreinteNull())
                     item.RefEmpreinte = row.RefEmpreinte;

                 if (!row.IsNomServiceNull())
                     item.NomService = row.NomService;

                 if (!row.IsMenusAutorisesNull())
                     item.MenusAutorises = row.MenusAutorises;

                 ListOperateurs.Add(item);
             }



             return ListOperateurs;
         }




         /***********************************************************/
         /*     la liste des Operateurs  Pour l'Export      */
         /*************************************************************/

         public List<Operateur> GetOperateursToExport(string nom, string prenom, string ddlprofilValue, string ddlserviceValue, string sortColonne, string sortOrder)
         {
             OperateursTableAdapter OperateurAdapter = new OperateursTableAdapter();

             List<Operateur> ListOperateurs = new List<Operateur>();
             MyDataSet.OperateursDataTable TableOperateurs = null;

             TableOperateurs = OperateurAdapter.GetOperateursToExport(nom, prenom, ddlprofilValue, ddlserviceValue, sortColonne,  sortOrder);
            

             foreach (MyDataSet.OperateursRow row in TableOperateurs)
             {
                 Operateur item = new Operateur();
                 item.ID = row.ID;

                 if (!row.IsNomNull())
                     item.Nom = row.Nom;

                 if (!row.IsPrenomNull())
                     item.Prenom = row.Prenom;

                 if (!row.IsDateCreationNull())
                     item.DateCreation = row.DateCreation;

                 if (!row.IsIDCreateurNull())
                     item.IDCreateur = row.IDCreateur;

                 if (!row.IsDebutNull())
                     item.Debut = row.Debut;

                 if (!row.IsFinNull())
                     item.Fin = row.Fin;

                 if (!row.IsProfilNull())
                     item.Profil = row.Profil;

                 if (!row.IsLoginNull())
                     item.Login = row.Login;

                 if (!row.IsMotPasseNull())
                     item.MotPasse = row.MotPasse;

                 if (!row.IsRefEmpreinteNull())
                     item.RefEmpreinte = row.RefEmpreinte;

                 if (!row.IsNomServiceNull())
                     item.NomService = row.NomService;

                 if (!row.IsMenusAutorisesNull())
                     item.MenusAutorises = row.MenusAutorises;

                 ListOperateurs.Add(item);
             }



             return ListOperateurs;
         }



       

         /************************************************/
         /* affiche Operateur qui a la  Matricule @var */
         /************************************************/

         public Operateur GetOperateurByID(int ID)
         {
             OperateursTableAdapter OperateurAdapter = new OperateursTableAdapter();

             MyDataSet.OperateursDataTable TableOperateur = OperateurAdapter.GetOperateurByID(ID);
             Operateur item = new Operateur();
             foreach (MyDataSet.OperateursRow row in TableOperateur)
             {               
                 item.ID = row.ID;

                 if (!row.IsNomNull())
                     item.Nom = row.Nom;

                 if (!row.IsPrenomNull())
                     item.Prenom = row.Prenom;

                 if (!row.IsDateCreationNull())
                     item.DateCreation = row.DateCreation;

                 if (!row.IsIDCreateurNull())
                     item.IDCreateur = row.IDCreateur;

                 if (!row.IsDebutNull())
                     item.Debut = row.Debut;

                 if (!row.IsFinNull())
                     item.Fin = row.Fin;

                 if (!row.IsProfilNull())
                     item.Profil = row.Profil;

                 if (!row.IsLoginNull())
                     item.Login = row.Login;

                 if (!row.IsMotPasseNull())
                     item.MotPasse = row.MotPasse;

                 if (!row.IsRefEmpreinteNull())
                     item.RefEmpreinte = row.RefEmpreinte;

                 if (!row.IsNomServiceNull())
                     item.NomService = row.NomService;

                 if (!row.IsMenusAutorisesNull())
                     item.MenusAutorises = row.MenusAutorises;

             }

             return item;
         }

         /************************************************/
         /*            Ajouter Un Operateur           */
         /************************************************/

         public int AjouterUnOperateur(Operateur item)
         {
             OperateursTableAdapter OperateurAdapter = new OperateursTableAdapter();
             return OperateurAdapter.InsertOperateur(MaxIdOperateur() + 1, item.Nom, item.Prenom, item.DateCreation, item.IDCreateur, item.Debut, item.Fin, item.Profil, item.Login,
                 item.MotPasse, item.RefEmpreinte, item.NomService, item.MenusAutorises);
                       
         }


         /************************************************/
         /*            Modifier Un Operateur           */
         /************************************************/

         public int ModifierUnOperateur(Operateur item)
         {
             OperateursTableAdapter OperateurAdapter = new OperateursTableAdapter();
             return OperateurAdapter.UpdateOperateur(item.Nom, item.Prenom, item.DateCreation, item.IDCreateur, item.Debut, item.Fin, item.Profil, item.Login,
                 item.MotPasse, item.RefEmpreinte, item.NomService, item.MenusAutorises,ID);

         }

      

         /************************************************/
         /*           Supprimer Un Operateur           */
         /************************************************/

         public int SupprimerUnOperateur(int ID)
         {
             OperateursTableAdapter OperateurAdapter = new OperateursTableAdapter();
             return OperateurAdapter.DeleteUnOperateur(ID);
         }

      

         /************************************************/
         /*                  Max IdOperateur            */
         /************************************************/

         public int MaxIdOperateur()
         {
             OperateursTableAdapter OperateurAdapter = new OperateursTableAdapter();
             try
             {
                 return  int.Parse(OperateurAdapter.GetMaxIdOperateur().ToString());
             }
             catch (Exception)
             {
                 return 0;
             }
         }
         /************************************************/
         /*               Total des Operateurs           */
         /************************************************/

         public int getTotalOperateurs()
         {
             OperateursTableAdapter OperateurAdapter = new OperateursTableAdapter();
             try
             {
                 return int.Parse(OperateurAdapter.GetTotalOperateurs().ToString());
                 
             }
             catch (Exception)
             {
                 return 0;
             }
         }

       


         /*************************************************************/
         /*               Total des Operateurs  Avec Filtre     */
         /*************************************************************/

         public int getTotalOperateursByFiltre(string nom, string prenom, string ddlprofilValue, string ddlserviceValue)
         {
             try
             {
                 int total = 0;

                 OperateursTableAdapter OperateurAdapter = new OperateursTableAdapter();

                
                     total = int.Parse(OperateurAdapter.GetTotalOperateursByFiltre(nom, prenom, ddlprofilValue, ddlserviceValue).ToString());

                 

                 return total;

             }
             catch (Exception)
             {
                 return 0;
             }

         }

      

    }

}
