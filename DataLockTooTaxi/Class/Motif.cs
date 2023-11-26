using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLockTooTaxi.MyDataSetTableAdapters;

namespace DataLockTooTaxi.Class
{
    public class Motif
    {
        public int IdMotif { get; set; }
        public string AbrevMotif { get; set; }
        public string LibelleMotif { get; set; }



        public Motif()
        {
            IdMotif = -1;
            AbrevMotif = " ";
            LibelleMotif = "";

        }



        public List<Motif> GetMotifs()
        {
            MotifsTableAdapter MotifsAdapter = new MotifsTableAdapter();
            List<Motif> ListMotifs = new List<Motif>();
            MyDataSet.MotifsDataTable TableMotifs = MotifsAdapter.GetMotifs();

            foreach (MyDataSet.MotifsRow row in TableMotifs)
            {
                Motif item = new Motif();
               
                item.IdMotif = row.IdMotif;

                if (!row.IsAbrevMotifNull())
                item.AbrevMotif = row.AbrevMotif;

                if (!row.IsLibelleMotifNull())
                item.LibelleMotif = row.LibelleMotif;
               

                ListMotifs.Add(item);
            }

            return ListMotifs;

        }
        /************************************************/
        /*           Get IdMotif                        */
        /************************************************/

        public int getIdMotif(string libelleMotif)
        {
            MotifsTableAdapter MotifsAdapter = new MotifsTableAdapter();
            return int.Parse(MotifsAdapter.GetIdMotif(libelleMotif).ToString());
        }
    }
} 