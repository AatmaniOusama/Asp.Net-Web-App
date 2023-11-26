using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLockTooTaxi.MyDataSetTableAdapters;

namespace DataLockTooTaxi
{
    public class Commune
    {
        public int num {get;set;}
        public string Libelle { get; set; }

        public Commune() 
        {
            num = 0;
            Libelle = "Tous";
        }

        /*******************************************************/
        /*               la liste des Communes                 */
        /*******************************************************/

        public List<Commune> getCommunes()
        {
            CommunesTableAdapter CommuneAdapter = new CommunesTableAdapter();
            MyDataSet.CommunesDataTable TableCommunes = CommuneAdapter.GetData();
            List<Commune> ListCommunes = new List<Commune>();
            Commune item = new Commune();
            ListCommunes.Add(item);
            foreach (MyDataSet.CommunesRow row in TableCommunes)
            {
                item = new Commune();
                
                item.num = row.IdCommune;

                if (!row.IsNomCommuneNull())
                item.Libelle = row.NomCommune;

                ListCommunes.Add(item);
            }

            return ListCommunes;
        }
    }
}
