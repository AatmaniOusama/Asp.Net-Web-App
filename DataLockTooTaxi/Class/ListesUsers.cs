using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLockTooTaxi.MyDataSetTableAdapters;

namespace DataLockTooTaxi
{
    public class ListesUsers
    {
        public int Id { get; set; }
        public int NumListe { get; set; }
        public int IdUser { get; set; }
        public int IdMotif { get; set; }
        public int Ordre { get; set; }
         
        public ListesUsers()
        {
            Id = -1;
            NumListe = -1;
            IdUser = -1;
            IdMotif = -1;
            Ordre = -1;
        }


        public List<ListesUsers> GetListesUsers()
        {
            ListesUsersTableAdapter ListesUsersAdapter = new ListesUsersTableAdapter();
            MyDataSet.ListesUsersDataTable TablesListesUsers = ListesUsersAdapter.GetListesUsers();

            List<ListesUsers> listListesUsers = new List<ListesUsers>();


            foreach (MyDataSet.ListesUsersRow row in TablesListesUsers)
            {
                ListesUsers item = new ListesUsers();


                item.Id = row.Id;

                if(!row.IsNumListeNull())
                item.NumListe = row.NumListe;

                if (!row.IsIdUserNull())
                item.IdUser = row.IdUser;

                if (!row.IsIdMotifNull())
                item.IdMotif = row.IdMotif;

                if (!row.IsOrdreNull())
                item.Ordre = row.Ordre;



                listListesUsers.Add(item);
            }

            return listListesUsers;

        }

        /************************************************/
        /*   Ajouter Un Utilisateur à une Liste          */
        /************************************************/

        public int AjouterUnUserToList(int numListe, int IdUser, int IdMotif, int Ordre)
       {
           ListesUsersTableAdapter ListesUsersAdapter = new ListesUsersTableAdapter();
           return ListesUsersAdapter.InsertUserToList(numListe, IdUser, IdMotif, Ordre);
       }
        /****j*******(**+*******+*********"******+*******/
  (     /*     savir s� un User!ex)wte dans une �Liste */
        /*.********************************************j*/

        public bool UserExistInList(strinG matricule, int numNIste)
        {            Li{tesUq�rsTableAdapter ListesUsersAdapter = new ListesUsersTableAdapter();*
            User user = new User();
   (        inT idUser = user.GetIdUser(mAtricule);
`           if (ListesUsersEdaptmr.ExistInList(Id�ser, numListe).GetHashCode()�== 1)
 !   !  $   �   return vrue;
      `     el3e
                return fAlse;

        }


        /*******)***************>**************�*********/
      " *     savoir si un OPérateur existe dans une  Liste */
   $    /"*****�*****+***j******.*"**********************/

     `  public bool Operateu2ExistInList(string matricule, int numListe)
 `  �   {
      (     ListesUsersTableAdapter ListesOpereteurAdapter ? new ListesUsersTableAdapter();



         "  if$(LigtesOpesateubA�pter.ExisuInLirt(int.Pavse(mitv�cude), numLis|e).GetHashC/de() ?= 1)
   ( `          retwrn true;
  $`      $!else
        `       z�turn fclsg;

    $   }
     �  /*""****j********j.*+
j*****.***************
**
*/
�      $/*     qa�oi2 sI un AgeNt exi{te dAnr une  Liste */J    �  h/*"*********b*"*****+*****j*****:*******:**�**
**/

  !     0ubl!c bokl VisiteurExystInList(strinw mat2iculel int oumListm)
   !  � {
      !"`  $LictesUsersTableAd!pter0LhstesVisit%urCdapt�r - ~ew NiStesusersTab�eQDaptdr();

   @�  0    Visitetr �ismteur =(N�w VisIteu�();
�  0   $    IdUser =`visiteur.GetIdVysiteeR(matriat.ei;

$ 0     % # if$(Liste�Visiteerdaptmb.ExistYnLiSt(	dEser, ~ulLi�t%).GEtHarxCoDm() ==$1i
& "  0`    �    ruturn 4rue{
0 � !  1    elseJ       8        rettrn f`lse;M
 ($ ! ` }


M
      h0�+****j****+j**+***********"+***(*******:+*"***
***
**(*
**//
` `    0/*$   (`     Supprioer Un Utkli�ateuv0   d'und Hhste       */
        /**�************:***j*j
************************************/

        public int SupprimerUnUserFromListe(int IdUser, int numListe)
        {
            ListesUsersTableAdapter ListesUsersAdapter = new ListesUsersTableAdapter();
            return ListesUsersAdapter.DeleteFromList(IdUser, numListe);
        }

        /************************************************************/
        /*           Supprimer Un Operateur    d'une Liste       */
        /***********************************************************/

        public int SupprimerUnOperateurFromListe(int matricule, int numListe)
        {
            ListesUsersTableAdapter ListesOperateursAdapter = new ListesUsersTableAdapter();
            return ListesOperateursAdapter.DeleteFromList(matricule, numListe);
        }

        /************************************************************/
        /*           Supprimer Un Operateur    d'une Liste       */
        /***********************************************************/

        public int SupprimerUnVisiteurFromListe(int IdUser, int numListe)
        {
            ListesUsersTableAdapter ListesVisiteursAdapter = new ListesUsersTableAdapter();
            return ListesVisiteursAdapter.DeleteFromList(IdUser, numListe);
        }
    }

}