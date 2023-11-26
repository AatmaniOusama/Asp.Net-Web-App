using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLockTooTaxi.MyDataSetTableAdapters;

namespace DataLockTooTaxi
{
    public class Droits
    {
        public int NumDroit {get; set;}
	    public string LibelleDroit {get; set;}
	    public decimal IdPenalite {get; set;}
        public decimal IdBonification { get; set; }

        public Droits()
        { 
             NumDroit =0;
	         LibelleDroit =" ";
	         IdPenalite =0;
             IdBonification = 0;
        
        }

        public List<Droits> GetDroitsAcces() {

            DroitsTableAdapter DroitsAdapter = new DroitsTableAdapter();
            List<Droits> ListDroits = new List<Droits>();

            foreach (MyDataSet.DroitsRow row in DroitsAdapter.GetDroits())
            {
                Droits item = new Droits();

             
                item.NumDroit =row.NumDroit;

                if (!row.IsLibelleDroitNull())
                item.LibelleDroit = row.LibelleDroit;

                if(!row.IsIdPenaliteNull())
                    item.IdPenalite = row.IdPenalite;

                if(!row.IsIdBonificationNull())
                    item.IdBonification = row.IdBonification;

                ListDroits.Add(item);

            }
            return ListDroits;
        
        }

    }

}
