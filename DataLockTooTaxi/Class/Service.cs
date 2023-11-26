using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLockTooTaxi.MyDataSetTableAdapters;

namespace DataLockTooTaxi
{
    public class Service
    {
        public int  Numero { get; set; }
        public string Libelle { get; set; }
        public int NumDroitAcces { get; set; }
        public int NumServicePere { get; set; }
        public int NumListSignataires { get; set; }

        public Service() {
            Numero = 0;
            Libelle = "# Tous #";
            NumDroitAcces = -1;
            NumServicePere = -1;
            NumListSignataires = -1;
        }

        public List<Service> GetServices()
        {
            ServicesTableAdapter ServiceAdapter = new ServicesTableAdapter();
            MyDataSet.ServicesDataTable TableServices = ServiceAdapter.GetServices();
            List<Service> ListServices = new List<Service>();
            ListServices.Add(new Service());
            foreach (MyDataSet.ServicesRow row in TableServices)
            {
                Service item = new Service();

               
                item.Numero = row.NumService;

                if (!row.IsNomServiceNull())
                item.Libelle = row.NomService;

                if (!row.IsNumDroitAccesNull())
                item.NumDroitAcces = row.NumDroitAcces;

                if (!row.IsNumServicePereNull())
                item.NumServicePere = row.NumServicePere;

                if (!row.IsNumListSignatairesNull())
                item.NumListSignataires = row.NumListSignataires;

                ListServices.Add(item);
            }

            return ListServices;

        }

    }
}
