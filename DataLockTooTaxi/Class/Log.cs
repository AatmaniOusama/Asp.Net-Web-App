using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using DataLockTooTaxi.MyDataSetTableAdapters;

namespace DataLockTooTaxi
{
    public class Log
    {
        
        public int IdLog { get; set; }
        public string Type { get; set; }
        public DateTime Instant { get; set; }
        public int IdOperateur { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Profil { get; set; }
        public string NomService { get; set; }
        public string Login { get; set; }


        public Log()
        {
            IdLog = -1;
            Type = "";
            Instant = DateTime.Now;
            IdOperateur = 0;
            Nom = "";
            Prenom = "";
            Profil = "";
            NomService = "";
            Login = "";
         
        }

       
      


        /************************************************/
        /*    Afficher la liste logins des Operateurs        */
        /************************************************/

        public List<Log> GetLoginsOperateursByFiltre(int Index, DateTime debut, DateTime fin, string nom, string prenom, string ddlprofilValue)
        {
            LogTableAdapter LogOperateurAdapter = new LogTableAdapter();

            List<Log> ListOperateurs = new List<Log>();



            MyDataSet.LogDataTable TableLogOperateurs = null;

            TableLogOperateurs = LogOperateurAdapter.GetLoginsOperateursByFiltre(Index, debut, fin, nom, prenom, ddlprofilValue);



            foreach (MyDataSet.LogRow row in TableLogOperateurs)
            {
                Log item = new Log();

                item.IdLog = row.IdLog;

                if (!row.IsTypeNull())
                {
                    if (row.Type == 1)
                        item.Type = "Fermeture Session";
                    if (row.Type == 0)
                        item.Type = "Ouverture Session";
                    if (row.Type == 2)
                        item.Type = "Ouverture Session WEB";
                }
                if (!row.IsInstantNull())
                    item.Instant = row.Instant;

                if (!row.IsNomNull())
                    item.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;

                if (!row.IsProfilNull())
                    item.Profil = row.Profil;


                if (!row.IsNomServiceNull())
                    item.NomService = row.NomService;

                if (!row.IsLoginNull())
                    item.Login = row.Login;

                ListOperateurs.Add(item);
            }



            return ListOperateurs;
        }



        /***********************************************************/
        /*   La liste des Logins des Operateurs  Pour l'Export      */
        /*************************************************************/

        public List<Log> GetLoginsOperateursToExport(DateTime debut, DateTime fin, string nom, string prenom, string ddlprofilValue)
        {
            LogTableAdapter LogOperateurAdapter = new LogTableAdapter();

            List<Log> ListOperateurs = new List<Log>();
            MyDataSet.LogDataTable TableLogOperateurs = null;

            TableLogOperateurs = LogOperateurAdapter.GetLoginsOperateursToExport(debut, fin, nom, prenom, ddlprofilValue);


            foreach (MyDataSet.LogRow row in TableLogOperateurs)
            {
                Log item = new Log();

                item.IdLog = row.IdLog;

                if (!row.IsTypeNull())
                {
                    if (row.Type == 1)
                        item.Type = "Fermeture Session";
                    if (row.Type == 0)
                        item.Type = "Ouverture Session";
                }

                if (!row.IsInstantNull())
                    item.Instant = row.Instant;

                if (!row.IsNomNull())
                    item.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;

                if (!row.IsProfilNull())
                    item.Profil = row.Profil;


                if (!row.IsNomServiceNull())
                    item.NomService = row.NomService;

                if (!row.IsLoginNull())
                    item.Login = row.Login;

                ListOperateurs.Add(item);
            }



            return ListOperateurs;
        }





     

        /************************************************/
        /*               Total des Logins des Operateurs           */
        /************************************************/

        public int getTotalLoginsOperateurs()
        {
            LogTableAdapter LogOperateurAdapter = new LogTableAdapter();
            try
            {
                return int.Parse(LogOperateurAdapter.GetTotalLoginsOperateurs().ToString());

            }
            catch (Exception)
            {
                return 0;
            }
        }



        /*************************************************************/
        /*               Total des Logins des Operateurs  Avec Filtre     */
        /*************************************************************/

        public int getTotalLoginsOperateursByFiltre(DateTime debut, DateTime fin, string nom, string prenom, string ddlprofilValue)
        {
            try
            {
                int total = 0;

                LogTableAdapter LogOperateurAdapter = new LogTableAdapter();


                total = int.Parse(LogOperateurAdapter.GetTotalLoginsOperateursByFiltre(debut, fin, nom, prenom, ddlprofilValue).ToString());



                return total;

            }
            catch (Exception)
            {
                return 0;
            }

        }



        /*************************************************************/
        /*               Ouverture Session     */
        /*************************************************************/


        public int  OpenSession(Operateur op)
        {
            LogTableAdapter LogOperateurAdapter = new LogTableAdapter();

            return LogOperateurAdapter.OpenSession(0, DateTime.Now,op.ID, op.Nom, op.Prenom, op.Profil, op.Login, op.NomService);
                   
        }


        /*************************************************************/
        /*               Fermeture Session     */
        /*************************************************************/


        public int CloseSession(Operateur op)
        {
            LogTableAdapter LogOperateurAdapter = new LogTableAdapter();

            return LogOperateurAdapter.OpenSession(1, DateTime.Now, op.ID, op.Nom, op.Prenom, op.Profil, op.Login, op.NomService);

        }

    }

}
