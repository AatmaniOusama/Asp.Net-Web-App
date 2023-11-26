using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLockTooTaxi.MyDataSetTableAdapters;

namespace DataLockTooTaxi
{
    public class LabelControle
    {
        public int Id {get;set;}
        public string Abrev{get;set;} 
        public string Libelle{get;set;}
        public int Type { get; set; }

        public LabelControle()
        {
            Id = -1;
            Libelle = " ";
        }

        public List<LabelControle> getLabelsControles()
        {
            LabelsControlesTableAdapter LabelsControlesAdapter = new LabelsControlesTableAdapter();
            List<LabelControle> ListLabelsControles = new List<LabelControle>();
            //ListLabelsControles.Add(new LabelControle());
            foreach(MyDataSet.LabelsControlesRow row in LabelsControlesAdapter.GetLabelsControles())
            {
                LabelControle item = new LabelControle();

                item.Id = row.Id;

                if (!row.IsAbrevNull())
                item.Abrev = row.Abrev;

                if (!row.IsLibelleNull())
                item.Libelle = row.Libelle;

                if (!row.IsTypeNull())
                item.Type = row.Type;

                ListLabelsControles.Add(item);

            }
            return ListLabelsControles;
            
        }


        public List<LabelControle> getLabelsControlesByType(int Type)
        {
            LabelsControlesTableAdapter LabelsControlesAdapter = new LabelsControlesTableAdapter();
            List<LabelControle> ListLabelsControles = new List<LabelControle>();
          // ListLabelsControles.Add(new LabelControle());
            foreach (MyDataSet.LabelsControlesRow row in LabelsControlesAdapter.GetLabelsControlesByType(Type))
            {
                LabelControle item = new LabelControle();

                item.Id = row.Id;

                if (!row.IsAbrevNull())
                    item.Abrev = row.Abrev;

                if (!row.IsLibelleNull())
                    item.Libelle = row.Libelle;

                if (!row.IsTypeNull())
                    item.Type = row.Type;

                ListLabelsControles.Add(item);

            }
            return ListLabelsControles;

        }
    }
}
