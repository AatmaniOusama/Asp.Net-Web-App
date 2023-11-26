using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLockTooTaxi.MyDataSetTableAdapters;

namespace DataLockTooTaxi
{
    public class Synthese
    {
        public string Taxi { get; set; }
        public string Badge { get; set; }
       


        public Synthese(string taxi, string badge)
        {
            Taxi = taxi;
            Badge = badge;
            
        }

        public Synthese()
        { }

        public List<Synthese> GetSyntheseControles(int TypeTaxi, bool valide, DateTime DateDebut, DateTime dateFin)
        {
            SyntheseTableAdapter SyntheseAdapter = new SyntheseTableAdapter();
            AgrementTableAdapter AgrementAdapter = new AgrementTableAdapter();

            List<Synthese> ListSynthese = new List<Synthese>();

            int  NbrTaxi = int.Parse(AgrementAdapter.DernierNumTaxi(TypeTaxi).ToString());

            for(int i=1;i<=NbrTaxi;i++)
            {
                ListSynthese.Add(new Synthese(i.ToString(), " "));
            }


            foreach (MyDataSet.SyntheseRow row in SyntheseAdapter.GetSyntheseControles(TypeTaxi, valide, DateDebut, dateFin))
            {
                if (!row.IsNumTaxiNull() && !row.IsNumBadgeNull() && !row.Isinstant1Null())
                {
                    if (int.Parse(row.NumTaxi) > 0 && int.Parse(row.NumTaxi) <= NbrTaxi)
                    {
                        try
                        {
                            ListSynthese[int.Parse(row.NumTaxi) - 1].Badge += row.NumBadge + " ";
                        }
                        catch (Exception)
                        {
                            //ListSynthese[int.Parse(row.NumTaxi) - 1].Badge += "";
                        }


                    }

                }
            }

            return ListSynthese;
        }

        public List<Synthese> GetSyntheseTaxiAbsentsDP(int TypeTaxi, DateTime dateDebut, DateTime dateFin)
        {
            SyntheseTaxiAbsentsTableAdapter SyntheseTaxiAbsentsTableAdapter = new SyntheseTaxiAbsentsTableAdapter();
            List<Synthese> ListSynthese = new List<Synthese>();
            MyDataSet.SyntheseTaxiAbsentsDataTable TableSyntheseTaxiAbsents = new MyDataSet.SyntheseTaxiAbsentsDataTable();

            TableSyntheseTaxiAbsents = SyntheseTaxiAbsentsTableAdapter.GetSyntheseLastEvent(dateDebut, dateFin, TypeTaxi, TypeTaxi, dateFin);

            int NbTaxi = TableSyntheseTaxiAbsents.Count();

            foreach (MyDataSet.SyntheseTaxiAbsentsRow row in TableSyntheseTaxiAbsents)
            {
                if (!row.IsNumTaxiNull())
                {
                    if (!row.IsCodeRefusNull() && row.CodeRefus > 01)
                    {
                        ListSynthese.Add(new Synthese(row.NumTaxi, ""));
                    }
                    else
                    {
                        ListSynthese.Add(new Synthese(row.NumTaxi, ""));
                    }
                }
            }
            foreach (MyDataSet.SyntheseTaxiAbsentsRow row in TableSyntheseTaxiAbsents)
            {
                if (!row.IsNumTaxiNull() && !row.Isinstant1Null())
                {

                    try
                    {
                        ListSynthese[row.IndexTaxi - 1].Badge += row.instant1.Date.ToString("dd/MM/yyyy") + " ";
                    }
                    catch (Exception)
                    {
                        //ListSynthese[int.Parse(row.NumTaxi) - 1].Badge += "";
                    }



                }
            }

            return ListSynthese;
        }


        public List<Synthese> GetSyntheseTaxiAbsents(int TypeTaxi, DateTime dateDebut, DateTime dateFin)
        {
            SyntheseTaxiAbsentsTableAdapter SyntheseTaxiAbsentsAdapter = new SyntheseTaxiAbsentsTableAdapter();           
            List<Synthese> ListSynthese = new List<Synthese>();
            MyDataSet.SyntheseTaxiAbsentsDataTable TableSyntheseTaxiAbsents = new MyDataSet.SyntheseTaxiAbsentsDataTable();

            TableSyntheseTaxiAbsents = SyntheseTaxiAbsentsAdapter.GetSyntheseLastEvent(dateDebut, dateFin, TypeTaxi, TypeTaxi, dateFin);
  

            foreach (MyDataSet.SyntheseTaxiAbsentsRow row in TableSyntheseTaxiAbsents)
            {
                
                   
                        ListSynthese.Add(new Synthese(row.NumTaxi, ""));

            }
           

           

            return ListSynthese;
        }

    }
}
