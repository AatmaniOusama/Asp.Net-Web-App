using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLockTooTaxi;
using System.Web.UI.HtmlControls;

public partial class Interfaces_Rapport_SyntheseControles : System.Web.UI.Page
{
    Synthese synthese = new DataLockTooTaxi.Synthese();
    TypeTaxi typeTaxi = new TypeTaxi();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

       

        Operateur operateur = (Operateur)Session["Operateur"];
        if (operateur.Profil.ToUpper() == "ADMINISTRATEUR")
        {
            //BarreControle.Rows[1].Cells[4].Visible = true;

        }

        if (operateur.Profil.ToUpper() == "CONSULTANT")
        {
            //BarreControle.Rows[1].Cells[4].Visible = false;

        }

        if (operateur.Profil.ToUpper() == "SUPERVISEUR")
        {
            //BarreControle.Rows[1].Cells[4].Visible = false;

        }


        if (!IsPostBack)
        {
            List<TypeTaxi> ListTypeTaxi=typeTaxi.getTypeTaxi();
            ListTypeTaxi.RemoveAt(0);
            DDLTypeTaxi.DataSource = ListTypeTaxi;
            DDLTypeTaxi.DataTextField = "Libelle";
            DDLTypeTaxi.DataValueField = "Num";
            DDLTypeTaxi.DataBind();

            TbDateJour.Text = DateTime.Now.ToString().Substring(0, 10);

            Table_Laod();
           }
        }
        catch (Exception)
        {
        }

    }
    protected void Table_Laod()
    {
        try
        {

            DateTime dateDebut = DateTime.Parse(TbDateJour.Text + " 00:00:00");
        DateTime dateFin = dateDebut.AddDays(1);
       
            if (DDLPlage.SelectedValue == "1")
        {
            dateDebut = DateTime.Parse(TbDateJour.Text + " 08:00:00");
            dateFin = DateTime.Parse(TbDateJour.Text + " 11:00:00");
        }
        else if (DDLPlage.SelectedValue == "2")
        {
            dateDebut = DateTime.Parse(TbDateJour.Text + " 08:00:00");
            dateFin = DateTime.Parse(TbDateJour.Text + " 17:00:00");
        }
        else if (DDLPlage.SelectedValue == "3")
            {
                dateDebut = DateTime.Parse(TbDateJour.Text + " 08:00:00");
                dateFin = DateTime.Parse(TbDateJour.Text + " 22:00:00");
            }

            List<Synthese> ListSynthese = synthese.GetSyntheseTaxiAbsentsDP(int.Parse(DDLTypeTaxi.SelectedValue), dateDebut, dateFin);
            int nbrTaxi = ListSynthese.Count();
            TbNbrLignes.Text = nbrTaxi.ToString();
       

       
            TableRow HeaderRow = new TableRow();
            HeaderRow.CssClass = "GridViewHeaderStyle";

            for (int k = 0; k < 11 ; k++)
          
            {
                TableCell HeaderCellTaxi = new TableCell();
                TableCell HeaderCellDate = new TableCell();
                HeaderCellTaxi.Width = 20;
                HeaderCellDate.Width = 75;
                HeaderCellTaxi.CssClass = "GridViewHeaderStyle";
                HeaderCellTaxi.Text = "Taxi";
                HeaderCellDate.Text = "Pointage";
                HeaderRow.Cells.Add(HeaderCellTaxi);
                HeaderRow.Cells.Add(HeaderCellDate);
               
            }

            TableSyntheseControlesHeader.Rows.Add(HeaderRow);
            Session["TableSyntheseControlesHeader"] = TableSyntheseControlesHeader;
     

        // Create more rows for the table.



        for (int i = 0; i < nbrTaxi; )
        {
            TableRow tempRow = new TableRow();
            tempRow.CssClass = "GridViewRowStyle";

            for (int j = 0; j < 11 && i < nbrTaxi; j++)
        {
                TableCell tempCellTaxi = new TableCell();
                TableCell tempCellDate = new TableCell();
                tempCellTaxi.Width = 20;
                tempCellDate.Width = 75;
                tempCellTaxi.CssClass = "GridViewAlternatingRowStyle";
                tempCellTaxi.Text = ListSynthese[i].Taxi;
                tempCellDate.Text=ListSynthese[i].Badge;
                tempRow.Cells.Add(tempCellTaxi);
                tempRow.Cells.Add(tempCellDate);
                
             

                i++;
            }

            TableSyntheseControlesBody.Rows.Add(tempRow);

            Session["TableSyntheseControlesBody"] = TableSyntheseControlesBody;
        }
        }
        catch (Exception)
        {
        }
        
    }

    protected void Filtre_Click(object sender, EventArgs e)
    {
        Table_Laod();
    }
   
    protected void CbFiltre_CheckedChanged(object sender, EventArgs e)
    {
        Fieldset_Filter.Visible = CbFiltre.Checked;
    }
    
    protected void exportExcel_Click(object sender, EventArgs e)
    {

        Response.Clear();
        string FileName = "attachment;filename=SyntheseDeControleDU" + DateTime.Parse(TbDateJour.Text).ToString("ddMMyyyy") + ".xls";
        Response.AddHeader("content-disposition", FileName);
        Response.ContentType = "application/vnd.xlsx";
        
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        HtmlForm form = new HtmlForm();
        Page mypage = new System.Web.UI.Page();
        
        mypage.Controls.Add(form);
        
        
        form.Controls.Add(Session["TableSyntheseControlesHeader"] as Table);
        form.Controls.Add(Session["TableSyntheseControlesBody"] as Table);


        // Read Style file (css) here and add to response 

        System.IO.FileInfo fi = new System.IO.FileInfo(Server.MapPath("../../Styles/GridViewStyle_EXCEL.css"));
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        System.IO.StreamReader sr = fi.OpenText();
        while (sr.Peek() >= 0)
        {
            sb.Append(sr.ReadLine());
        }
        sr.Close();

        form.RenderControl(htmlWrite);
        
        Response.Write("<html><head>  <style type='text/css'>" + sb.ToString() + "</style></head><body>" + stringWrite.ToString() + "</body></html>");
        Response.Flush();
        Response.Close();
        Response.End();

    }
   
    public static void PrepareGridViewForExport(Control gvcontrol)
    {
        for (int i = 0; i < gvcontrol.Controls.Count; i++)
        {
            Control current = gvcontrol.Controls[i];
            if (current is LinkButton)
            {
                gvcontrol.Controls.Remove(current);
                gvcontrol.Controls.AddAt(i,new LiteralControl((current as LinkButton).Text));
            }
            else if (current is HyperLink)
            {
                gvcontrol.Controls.Remove(current);
                gvcontrol.Controls.AddAt(i,new LiteralControl((current as HyperLink).Text));
            }
            else if (current is DropDownList)
            {
                gvcontrol.Controls.Remove(current);
                gvcontrol.Controls.AddAt(i,new LiteralControl((current as DropDownList).
                SelectedItem.Text));
            }
            else if (current is Label)
            {
                gvcontrol.Controls.Remove(current);

                string champ = (current as Label).Text;
                if (champ == "Y" || champ == "N")
                    champ = "";
                gvcontrol.Controls.AddAt(i,
                new LiteralControl(champ));
            }
            else if (current is TextBox)
            {
                gvcontrol.Controls.Remove(current);
                gvcontrol.Controls.AddAt(i,
                new LiteralControl((current as TextBox).Text));
            }

            else if (current is HiddenField)
            {
                gvcontrol.Controls.Remove(current);
            }
            if (current.HasControls())
            {
                PrepareGridViewForExport(current);
            }
            else if (current is ImageButton)
            {
                gvcontrol.Controls.Remove(current);
            }
            if (current.HasControls())
            {
                PrepareGridViewForExport(current);
            }
        }
    }



}