using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLockTooTaxi.MyDataSetTableAdapters;

namespace DataLockTooTaxi.Class
{
    public class CodesMissions
    {
        public int     NumCode      { get; set; }
        public string  ValeurCode   { get; set; }
        public string  LibelleCode  { get; set; }
        public string  CodeSap      { get; set; }
        public int     Lecteur      { get; set; }
        public int     Type         { get; set; }

        public CodesMissions() 
        {
         NumCode = -1 ;
         ValeurCode =  "" ;  
         LibelleCode =  "" ; 
         CodeSap = "" ;    
         Lecteur =  -1 ;
         Type = -1;
        }

        /*******************************************************/
        /*               la liste des Codes Refus              */
        /*******************************************************/

        public List<CodesMissions> getCodesRefus()
        {
            CodesMissionsTableAdapter CodesMissionsAdapter = new CodesMissionsTableAdapter();

            MyDataSet.CodesMissionsDataTable TableCodesMissions = CodesMissionsAdapter.GetCodeRefus();
            List<CodesMissions> ListCodesMissions = new List<CodesMissions>();
            CodesMissions item = new CodesMissions();
            ListCodesMissions.Add(item);
            foreach (MyDataSet.CodesMissionsRow row in TableCodesMissions)
            {
                item = new CodesMissions();

                item.NumCode = row.NumCode;

                if (!row.IsValeurCodeNull())
                    item.ValeurCode = row.ValeurCode;

                if (!row.IsLibelleCodeNull())
                    item.LibelleCode = row.LibelleCode;

                ListCodesMissions.Add(item);
            }

            return ListCodesMissions;
        }
    }
}
