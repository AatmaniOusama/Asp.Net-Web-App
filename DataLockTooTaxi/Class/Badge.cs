using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLockTooTaxi.MyDataSetTableAdapters;

namespace DataLockTooTaxi
{
    public class Badge
    {
        public int    Id            { get; set; }
        public string NumBadge      { get; set; }
        public string FlagAutorise  { get; set; }
        public string Nom           { get; set; }
        public string Prenom        { get; set; }
        public string Matricule     { get; set; }

        public Badge()
        {
            Id = -1;
            NumBadge = "";
            FlagAutorise = "";
            Nom = "";
            Prenom = "";
            Matricule = "";
        }


        /*******************************************************/
        /*       la liste des badges Autorises et libres       */
        /*******************************************************/

        public List<Badge> GetBadgesAutorise()
        {
            BadgesTableAdapter BadgeAdapter = new BadgesTableAdapter();
            MyDataSet.BadgesDataTable TableBadges = BadgeAdapter.GetBadgesAutorise();
            List<Badge> ListBadges = new List<Badge>();
            
            foreach (MyDataSet.BadgesRow row in TableBadges)
            {
                Badge item = new Badge();
                
               

                if (!row.IsNumBadgeNull())
                item.NumBadge = row.NumBadge;

                if (!row.IsFlagAutoriseNull())
                item.FlagAutorise = row.FlagAutorise;

                ListBadges.Add(item);

            }

            return ListBadges;
        }

        /************************************************************************/
        /*       la liste de TOUS les badges   (pour le trie et l'export)   */
        /************************************************************************/

        public List<Badge> getAllBadges()
        {
            BadgesTableAdapter BadgesAdapter = new BadgesTableAdapter();
            List<Badge> ListBadges = new List<Badge>();
            MyDataSet.BadgesDataTable TableBadges = BadgesAdapter.GetAllBadges();

            foreach (MyDataSet.BadgesRow row in TableBadges)
            {
                Badge item = new Badge();

                
                

                if (!row.IsNumBadgeNull())
                    item.NumBadge = row.NumBadge;

                if(!row.IsFlagAutoriseNull())
                item.FlagAutorise = row.FlagAutorise;

                if (!row.IsNomNull())
                    item.Nom = row.Nom;


                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;


                if (!row.IsMatriculeNull())
                    item.Matricule = row.Matricule;

                ListBadges.Add(item);
            }

            return ListBadges;

        }


        /***************************************************************************************/
        /*       la liste de TOUS les badges  avec Filtre    (sert pour le trie et l'export)   */
        /***************************************************************************************/

        public List<Badge> getAllBadgesByFiltre(string numBadge, string rbautoriseValue, string nom, string prenom, string  matricule, string rbAttribue)
        {
            BadgesTableAdapter BadgesAdapter = new BadgesTableAdapter();
            List<Badge> ListBadges = new List<Badge>();
            MyDataSet.BadgesDataTable TableBadges = BadgesAdapter.GetAllBadgesByFiltre(numBadge, rbautoriseValue, nom, prenom, matricule, rbAttribue);

            foreach (MyDataSet.BadgesRow row in TableBadges)
            {
                Badge item = new Badge();

                


                if (!row.IsNumBadgeNull())
                    item.NumBadge = row.NumBadge;

                if (!row.IsFlagAutoriseNull())
                    item.FlagAutorise = row.FlagAutorise;

                if (!row.IsNomNull())
                    item.Nom = row.Nom;


                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;


                if (!row.IsMatriculeNull())
                    item.Matricule = row.Matricule;

                ListBadges.Add(item);
            }

            return ListBadges;

        }

        /************************************************/
        /*    Afficher la liste des Badges              */
        /************************************************/

        public List<Badge> getBadges(int Index, string numBadge, string rbautoriseValue, string nom, string prenom, string matricule, string rbAttribue)
        {
            BadgesTableAdapter BadgesAdapter = new BadgesTableAdapter();

            List<Badge> ListBadges = new List<Badge>();



            MyDataSet.BadgesDataTable TableBadges = null;

            TableBadges = BadgesAdapter.GetBadges(Index, numBadge, rbautoriseValue, nom, prenom, matricule, rbAttribue);
            

            foreach (MyDataSet.BadgesRow row in TableBadges)
            {
                Badge item = new Badge();
              


                if (!row.IsNumBadgeNull())
                    item.NumBadge = row.NumBadge;

                if (!row.IsFlagAutoriseNull())
                item.FlagAutorise = row.FlagAutorise;

                if (!row.IsNomNull())
                    item.Nom = row.Nom;


                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;


                if (!row.IsMatriculeNull())
                    item.Matricule = row.Matricule;



                ListBadges.Add(item);
            }



            return ListBadges;
        }


        /************************************************/
        /*            Ajouter Un Badge           */
        /************************************************/

        public int AjouterUnBadge(Badge item)
        {
            BadgesTableAdapter BadgesAdapter = new BadgesTableAdapter();
            return BadgesAdapter.InsertBadge( item.NumBadge, item.FlagAutorise);
        }

        /************************************************/
        /*   Modifier FlagAutorise  d'Un Utilisateur    */
        /************************************************/

        public int ModifierFlagAutorise(string FlagAutorise, string numBadge)
        {
            BadgesTableAdapter BadgesAdapter = new BadgesTableAdapter();
            return BadgesAdapter.SetFlagAutorise(FlagAutorise, numBadge);
        }

        /************************************************/
        /*           Supprimer Un Badge           */
        /************************************************/

        public int SupprimerUnBadge(string numBadge)
        {
            BadgesTableAdapter BadgesAdapter = new BadgesTableAdapter();
            return BadgesAdapter.DeleteUnBadge(numBadge);
        }


        /************************************************/
        /*                  Max IdBadge                  */
        /************************************************/

        public int MaxIdBadge()
        {
            BadgesTableAdapter BadgesAdapter = new BadgesTableAdapter();
            try
            {
                return int.Parse(BadgesAdapter.GetMaxIdBadge().ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }


        /************************************************/
        /*               Total des Badges                 */
        /************************************************/

        public int getTotalBadges()
        {
            BadgesTableAdapter BadgesAdapter = new BadgesTableAdapter();
            try
            {
                return int.Parse(BadgesAdapter.GetTotalBadges().ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }


        /*************************************************************/
        /*               Total des Badges  Avec Filtre     */
        /*************************************************************/

        public int getTotalBadgesByFiltre(string numBadge, string rbautoriseValue, string nom, string prenom, string matricule, string rbAttribue)
        {
            try
            {
                int total = 0;

                BadgesTableAdapter BadgesAdapter = new BadgesTableAdapter();


                total = int.Parse(BadgesAdapter.GetTotalBadgesByFiltre(numBadge, rbautoriseValue, nom, prenom, matricule, rbAttribue).ToString());
           

                return total;

            }
            catch (Exception)
            {
                return 0;
            }

        }


        /************************************************/
        /*         savoir si un Badge existe       */
        /************************************************/

        public bool BadgeExiste(string numBadge)
        {
            BadgesTableAdapter BadgesAdapter = new BadgesTableAdapter();

            if (BadgesAdapter.BadgeExiste(numBadge).GetHashCode() == 1)
                return true;
            else
                return false;

        }
    }
}
