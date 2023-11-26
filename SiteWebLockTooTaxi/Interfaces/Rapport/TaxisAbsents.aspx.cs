using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLockTooTaxi;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Drawing;

public partial class Interfaces_Rapport_TaxisAbsents : System.Web.UI.Page
{
    TypeTaxi typetaxi = new TypeTaxi();
    Agrement agrement = new Agrement();
    Commune commune = new Commune();
    List<Agrement> ListAgrements;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            TbPlaque.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
           
            Operateur operateur = (Operateur) Session["Operateur"];
            if (operateur.Profil.ToUpper() == "ADMINISTRATEUR")
            {

                BarreControle.Rows[0].Cells[2].Visible = true;

            }

            if (operateur.Profil.ToUpper() == "SUPERVISEUR")
            {

                BarreControle.Rows[0].Cells[2].Visible = true;

            }

            if (operateur.Profil.ToUpper() == "CONSULTANT")
            {

                BarreControle.Rows[0].Cells[2].Visible = true;
            }




        if (!IsPostBack)
        {

            ViewState["sortOrder"] = "";



            if (Session["TaxiAbsContDU"] != null)
                TbDateDebut.SelectedDate = Session["TaxiAbsContDU"] as DateTime?;
            else
                TbDateDebut.SelectedDate = DateTime.Parse(DateTime.Now.ToString().Substring(0, 10) + " 00:00:00");

            if (Session["TaxiAbsContAU"] != null)
                TbDateFin.SelectedDate = Session["TaxiAbsContAU"] as DateTime?;
            else
                TbDateFin.SelectedDate = DateTime.Parse(DateTime.Now.ToString().Substring(0, 10) + " 23:59:59");


            
            DDLGroupe.SelectedValue = Session["TaxiAbsContGroupe"] as string;
            TbPlaque.Text = Session["TaxiAbsContPlaque"] as string;
            TbAgrement.Text = Session["TaxiAbsContAgrement"] as string;




            DDLGroupe.DataSource = typetaxi.getTypeTaxi();
            DDLGroupe.DataTextField = "Libelle";
            DDLGroupe.DataValueField = "Num";
            DDLGroupe.DataBind();

        

            LaodGridView();
            }
        }
        catch (Exception)
        {
        }


    }

    protected void LaodGridView()
    {
        DateTime debut = TbDateDebut.SelectedDate.Value;
        DateTime fin = TbDateFin.SelectedDate.Value;

        List<Agrement> ListAgrements = agrement.getTaxisAbsents(debut, fin);

      
        if (DDLGroupe.SelectedValue != "-1")
            ListAgrements = ListAgrements.FindAll(a => a.TypeTaxi.ToString() == DDLGroupe.SelectedItem.Text);
        if (TbPlaque.Text != "")
            ListAgrements = ListAgrements.FindAll(a => a.Plaque.Contains(TbPlaque.Text));
        if (TbAgrement.Text != "")
            ListAgrements = ListAgrements.FindAll(a => a.NumAgrement == TbAgrement.Text);


        GridviewAbsentsPeriodeContinue.DataSource = ListAgrements.OrderBy(a => int.Parse(a.NumTaxi)).ToList();
        GridviewAbsentsPeriodeContinue.DataBind();

        Session["AbsentsPeriodeContinue"] = ListAgrements.OrderBy(a => int.Parse(a.NumTaxi)).ToList(); 
        TbNbrLignes.Text = ListAgrements.Count.ToString();


      

    }
    protected void CbFiltre_CheckedChanged(object sender, EventArgs e)
    {
        Fieldset_Filter.Visible = CbFiltre.Checked;
    }

    protected void Filtre_Click(object sender, EventArgs e)
    {
        
      
        Session["TaxiAbsContDU"] = TbDateDebut.SelectedDate.Value;
        Session["TaxiAbsContAU"] = TbDateFin.SelectedDate.Value;      
        Session["TaxiAbsContGroupe"] = DDLGroupe.SelectedValue;
        Session["TaxiAbsContPlaque"] =  TbPlaque.Text;
        Session["TaxiAbsContAgrement"] = TbAgrement.Text;


        LaodGridView();

    }

    protected void GridviewAbsentsJours_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridviewAbsentsPeriodeContinue.PageIndex = e.NewPageIndex;

        GridviewAbsentsPeriodeContinue.DataSource = Session["AbsentsPeriodeContinue"] as List<Agrement>;
        GridviewAbsentsPeriodeContinue.DataBind();
       


    }

    protected void exportExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        string FileName = "attachment;filename=" + "Taxis_Absents" + ".xls";
        Response.AddHeader("content-disposition", FileName);

        Response.ContentEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");

        Response.ContentType = "application/vnd.xlsx";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        HtmlForm form = new HtmlForm();
        Page mypage = new System.Web.UI.Page();

        GridviewAbsentsPeriodeContinue.AllowPaging = false;
        GridviewAbsentsPeriodeContinue.AllowSorting = false;
        GridviewAbsentsPeriodeContinue.ShowFooter = false;


        GridviewAbsentsPeriodeContinue.EditIndex = -1;



        


        GridviewAbsentsPeriodeContinue.DataSource = Session["AbsentsPeriodeContinue"] as IList<Agrement>;
        GridviewAbsentsPeriodeContinue.DataBind();

      

        PrepareGridViewForExport(GridviewAbsentsPeriodeContinue);

        mypage.Controls.Add(form);
        form.Controls.Add(GridviewAbsentsPeriodeContinue);

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

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    public static void PrepareGridViewForExport(Control gvcontrol)
    {
        for (int i = 0; i < gvcontrol.Controls.Count; i++)
        {
            Control current = gvcontrol.Controls[i];
            if (current is LinkButton)
            {
                gvcontrol.Controls.Remove(current);
                gvcontrol.Controls.AddAt(i,
                 new LiteralControl((current as LinkButton).Text));
            }
            else if (current is HyperLink)
            {
                gvcontrol.Controls.Remove(current);
                gvcontrol.Controls.AddAt(i,
                 new LiteralControl((current as HyperLink).Text));
            }
            else if (current is DropDownList)
            {
                gvcontrol.Controls.Remove(current);
                gvcontrol.Controls.AddAt(i,
                new LiteralControl((current as DropDownList).
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


    //------------------------------ Fonctions de Tri -------------------------------------------

    private void BindGridView(string sortExp, string sortDir)
    {


        ListAgrements = Session["AbsentsPeriodeContinue"] as List<Agrement>;

        switch (sortExp)
        {


            case "NumAgrement":

                if (sortDir == "desc")
                    ListAgrements = ListAgrements.OrderByDescending(o => int.Parse(o.NumAgrement)).ToList();
                else
                    ListAgrements = ListAgrements.OrderBy(o => int.Parse(o.NumAgrement)).ToList();
                break;

            case "Nom":
                if (sortDir == "desc")
                    ListAgrements = ListAgrements.OrderByDescending(o => o.Nom).ToList();
                else
                    ListAgrements = ListAgrements.OrderBy(o => o.Nom).ToList();
                break;

            case "TypeTaxi":
                if (sortDir == "desc")
                    ListAgrements = ListAgrements.OrderByDescending(o => o.TypeTaxi).ToList();
                else
                    ListAgrements = ListAgrements.OrderBy(o => o.TypeTaxi).ToList();
                break;
            case "Plaque":
                if (sortDir == "desc")
                    ListAgrements = ListAgrements.OrderByDescending(o => o.Plaque).ToList();
                else
                    ListAgrements = ListAgrements.OrderBy(o => o.Plaque).ToList();
                break;

            case "Commune":
                if (sortDir == "desc")
                    ListAgrements = ListAgrements.OrderByDescending(o => o.Commune).ToList();
                else
                    ListAgrements = ListAgrements.OrderBy(o => o.Commune).ToList();
                break;


        }


        Session["AbsentsPeriodeContinue"] = ListAgrements;

        GridviewAbsentsPeriodeContinue.DataSource = ListAgrements;
        GridviewAbsentsPeriodeContinue.DataBind();

    }

    System.Web.UI.WebControls.Image sortImage = new System.Web.UI.WebControls.Image();
    public string sortOrder
    {
        get
        {
            if (ViewState["sortOrder"].ToString() == "desc")
            {
                ViewState["sortOrder"] = "asc";
                sortImage.ImageUrl = "~/Icons/up.png";
            }
            else
            {
                ViewState["sortOrder"] = "desc";
                sortImage.ImageUrl = "~/Icons/down.png";
            }

            return ViewState["sortOrder"].ToString();
        }
        set
        {
            ViewState["sortOrder"] = value;
        }
    }

    protected void Taxi_Sorting(object sender, GridViewSortEventArgs e)
    {
        BindGridView(e.SortExpression, sortOrder);
        int columnIndex = 0;

        foreach (DataControlFieldHeaderCell headerCell in GridviewAbsentsPeriodeContinue.HeaderRow.Cells)
        {
            if (headerCell.ContainingField.SortExpression == e.SortExpression)
            {
                columnIndex = GridviewAbsentsPeriodeContinue.HeaderRow.Cells.GetCellIndex(headerCell);
            }
        }

        GridviewAbsentsPeriodeContinue.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);


        //int nbrRows = GridviewAbsentsPeriodeContinue.Rows.Count;
        //int heightGrid = nbrRows * 25;

        //if (heightGrid <= 470 && nbrRows <= 100)
        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader2('" + GridviewAbsentsPeriodeContinue.ClientID + "', " + heightGrid + 25 + ", 950 , 40 ,false); </script>", false);

        //if (heightGrid >= 470 && nbrRows < 100)

        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GridviewAbsentsPeriodeContinue.ClientID + "', 470, 950 , 40 ,false); </script>", false);

        //if (heightGrid >= 470 && nbrRows >= 100)

        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GridviewAbsentsPeriodeContinue.ClientID + "', 470, 950 , 40 ,true); </script>", false);

    }

    //-------------------------------------------------------------------------------------------


}