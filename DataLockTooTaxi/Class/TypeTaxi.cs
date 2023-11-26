using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLockTooTaxi.MyDataSetTableAdapters;

namespace DataLockTooTaxi
{
    public class TypeTaxi
    {
        public int Num { get; set; }
        public string Libelle { get; set; }

        public TypeTaxi() {
            Num = -1;
            Libelle = "Tous";
        }

        public List<TypeTaxi> getTypeTaxi()
        {
            TypeTaxiTableAdapter TypeTaxiAdapter = new TypeTaxiTableAdapter();
            MyDataSet.TypeTaxiDataTable TableTypeTaxi = TypeTaxiAdapter.GetTypeTaxi();
            List<TypeTaxi> ListTypeTaxi = new List<TypeTaxi>();

            ListTypeTaxi.Add(new TypeTaxi());

            foreach (MyDataSet.TypeTaxiRow row in TableTypeTaxi)
            {
                TypeTaxi item = new TypeTaxi();


                if (!row.IsNumNull())
                item.Num = row.Num;

                if(!row.IsLibelleNull())
                item.Libelle = row.Libelle;

                ListTypeTaxi.Add(item);
            }

            return ListTypeTaxi;
        }
    }
}
