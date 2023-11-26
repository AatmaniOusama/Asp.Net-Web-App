using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLockTooTaxi;
using System.Web.UI.HtmlControls;

public partial class Interfaces_Rapport_IdentificationsPeriode : System.Web.UI.Page
{

    Service service = new Service();
    Agrement agrement = new Agrement();
    List<Agrement> ListAgrements;
    TypeTaxi typetaxi = new TypeTaxi();

    protected void Page_Load(object sender, EventArgs e)
    {

        
        try
        {

        

        Operateur operateur = (Operateur)Session["Operateur"];
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


            
            

            if (Session["TaxiIdenDateDebut"] as DateTime? != null)
                TbDateDebut.SelectedDate = Session["TaxiIdenDateDebut"] as DateTime?;
            else
                TbDateDebut.SelectedDate = DateTime.Parse(DateTime.Now.ToString().Substring(0, 10) + " 00:00:00");

            if (Session["TaxiIdenDateFin"] as DateTime? != null)
                TbDateFin.SelectedDate = Session["TaxiIdenDateFin"] as DateTime?;
            else
                TbDateFin.SelectedDate = DateTime.Parse(DateTime.Now.ToString().Substring(0, 10) + " 23:59:59");
           
            TbNumTaxi.Text = Session["TaxiIdenNumTaxi"] as string;
            DDLTypeTaxi.SelectedValue = Session["TaxiIdenTypeTaxi"] as string;


            if (!string.IsNullOrEmpty(Session["TaxiIdenAvecPointage"] as string))
            RbIdentification.SelectedValue = Session["TaxiIdenAvecPointage"] as string;

            DDLTypeTaxi.DataSource = typetaxi.getTypeTaxi();
            DDLTypeTaxi.DataValueField = "Num";
            DDLTypeTaxi.DataTextField = "Libelle";
            DDLTypeTaxi.DataBind();


            LaodGridView();


        }
        }
        catch (Exception)
        {
        }
    }

    protected void LaodGridView()
    {
        agrement = new Agrement();




        List<Agrement> ListAgrements = agrement.getIdentification_Periode_Taxi(TbDateDebut.SelectedDate.Value,TbDateFin.SelectedDate.Value);




        if (RbIdentification.SelectedValue == "true")
            ListAgrements = ListAgrements.FindAll(c => !(c.DateDebut == "" && c.DateFin == ""));
        else
            ListAgrements = ListAgrements.FindAll(c => (c.DateDebut == "" && c.DateFin == ""));
      
        if (TbNumTaxi.Text != "")
        {
            ListAgrements = ListAgrements.FindAll(delegate(Agrement item) { return item.NumTaxi.Contains(TbNumTaxi.Text.ToUpper()); });
        }


        if (DDLTypeTaxi.SelectedValue != "-1")
        {
            ListAgrements = ListAgrements.FindAll(delegate(Agrement item) { return item.TypeTaxi == DDLTypeTaxi.SelectedItem.Text; });
        }
       


        TbNbrLignes.Text = ListAgrements.Count.ToString();



        GridviewIdentification.DataSource = ListAgrements.OrderBy(a => int.Parse(a.NumTaxi)).ToList(); ;
        GridviewIdentification.DataBind();

        Session["Agrement_Ident"] = ListAgrements.OrderBy(a => int.Parse(a.NumTaxi)).ToList(); 

     
      
    }
    protected void CbFiltre_CheckedChanged(object sender, EventArgs e)
    {
        Fieldset_Filter.Visible = CbFiltre.Checked;
    }

    protected void Filtre_Click(object sender, EventArgs e)
    {
        Session["TaxiIdenDateDebut"] = TbDateDebut.SelectedDate.Value;
        Session["TaxiIdenDateFin"] = TbDateFin.SelectedDate.Value;
        Session["TaxiIdenNumTaxi"] = TbNumTaxi.Text;
        Session["TaxiIdenTypeTaxi"] = DDLTypeTaxi.SelectedValue;
        Session["TaxiIdenAvecPointage"] = RbIdentification.SelectedValue;
       


        LaodGridView();
    }
    protected void GridviewIdentification_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridviewIdentification.PageIndex = e.NewPageIndex;

        GridviewIdentification.DataSource = Session["Agrement_Ident"] as List<Agrement>;
        GridviewIdentification.DataBind();

       
    }


    protected void GridViewUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.Cells[3].FindControl("LblDateDebut") as Label).Text == "01/01/1900")
                (e.Row.Cells[3].FindControl("LblDateDebut") as Label).Text = "";

            if ((e.Row.Cells[6].FindControl("LblDateFin") as Label).Text == "01/01/1900")
                (e.Row.Cells[6].FindControl("LblDateFin") as Label).Text = "";
        }
    }


    protected void exportExcel_Click(object sender, EventArgs e)
    {


        Response.Clear();
        string FileName = "attachment;filename=PremierDernierPointagesTaxis.xls";
        Response.AddHeader("content-disposition", FileName);

        Response.ContentEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");

        Response.ContentType = "application/vnd.xlsx";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        HtmlForm form = new HtmlForm();
        Page mypage = new System.Web.UI.Page();

        GridviewIdentification.AllowPaging = false;
        GridviewIdentification.AllowSorting = false;
        GridviewIdentification.ShowFooter = false;

        GridviewIdentification.EditIndex = -1;

        

        GridviewIdentification.DataSource = Session["Agrement_Ident"]  as List<Agrement>;
        GridviewIdentification.DataBind();

        PrepareGridViewForExport(GridviewIdentification);

        mypage.Controls.Add(form);
        form.Controls.Add(GridviewIdentification);

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


        ListAgrements = Session["Agrement_Ident"] as List<Agrement>;

        switch (sortExp)
        {
            case "NumTaxi":

                foreach (Agrement agrement in ListAgrements)
                {
                    if (string.IsNullOrWhiteSpace(agrement.NumTaxi) || string.IsNullOrEmpty(agrement.NumTaxi))
                        agrement.NumTaxi = "0";
                }
                if (sortDir == "desc")
                    ListAgrements = ListAgrements.OrderByDescending(o => int.Parse( o.NumTaxi)).ToList();
                else
                    ListAgrements = ListAgrements.OrderBy(o => int.Parse( o.NumTaxi)).ToList();
                break;

            case "TypeTaxi":
                if (sortDir == "desc")
                    ListAgrements = ListAgrements.OrderByDescending(o => o.TypeTaxi).ToList();
                else
                    ListAgrements = ListAgrements.OrderBy(o => o.TypeTaxi).ToList();
                break;

            case "DateDebut":

                foreach (Agrement agrement in ListAgrements)
                {
                    if (string.IsNullOrWhiteSpace(agrement.DateDebut) || string.IsNullOrEmpty(agrement.DateDebut))
                        agrement.DateDebut = "01/01/1900";
                }
                if (sortDir == "desc")
                    ListAgrements = ListAgrements.OrderByDescending(o => DateTime.Parse(o.DateDebut)).ToList();
                else
                    ListAgrements = ListAgrements.OrderBy(o => DateTime.Parse(o.DateDebut)).ToList();
                break;


            case "Nom_Prem":
                if (sortDir == "desc")
                    ListAgrements = ListAgrements.OrderByDescending(o => o.Nom_Prem).ToList();
                else
                    ListAgrements = ListAgrements.OrderBy(o => o.Nom_Prem).ToList();
                break;

            case "Prenom_Prem":

                if (sortDir == "desc")
                    ListAgrements = ListAgrements.OrderByDescending(o => o.Prenom_Prem).ToList();
                else
                    ListAgrements = ListAgrements.OrderBy(o => o.Prenom_Prem).ToList();
                break;

            case "DateFin":
                foreach (Agrement agrement in ListAgrements)
                {
                    if (string.IsNullOrWhiteSpace(agrement.DateFin) || string.IsNullOrEmpty(agrement.DateFin))
                        agrement.DateFin = "01/01/1900";
                }
                if (sortDir == "desc")
                    ListAgrements = ListAgrements.OrderByDescending(o => DateTime.Parse(o.DateFin)).ToList();
                else
                    ListAgrements = ListAgrements.OrderBy(o => DateTime.Parse(o.DateFin)).ToList();
                break;

            case "Nom_Dern":
                if (sortDir == "desc")
                    ListAgrements = ListAgrements.OrderByDescending(o => o.Nom_Dern).ToList();
                else
                    ListAgrements = ListAgrements.OrderBy(o => o.Nom_Dern).ToList();
                break;



            case "Prenom_Dern":

                if (sortDir == "desc")
                    ListAgrements = ListAgrements.OrderByDescending(o => o.Prenom_Dern).ToList();
                else
                    ListAgrements = ListAgrements.OrderBy(o => o.Prenom_Dern).ToList();
                break;

        }

        Session["Agrement_Ident"] = ListAgrements;

        GridviewIdentification.DataSource = ListAgrements;
        GridviewIdentification.DataBind();

    }


    Image sortImage = new Image();
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

    protected void Agrement_Sorting(object sender, GridViewSortEventArgs e)
    {
        BindGridView(e.SortExpression, sortOrder);

        int columnIndex = 0;

        foreach (DataControlFieldHeaderCell headerCell in GridviewIdentification.HeaderRow.Cells)
        {
            if (headerCell.ContainingField.SortExpression == e.SortExpression)
            {
                columnIndex = GridviewIdentification.HeaderRow.Cells.GetCellIndex(headerCell);
            }
        }

        GridviewIdentification.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);

        //int nbrRows = GridviewIdentification.Rows.Count;
        //int heightGrid = nbrRows * 25;

        //if (heightGrid <= 426 && nbrRows <= 100)
        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader2('" + GridviewIdentification.ClientID + "', " + heightGrid + 25 + ", 950 , 40 ,false); </script>", false);

        //if (heightGrid >= 426 && nbrRows < 100)

        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GridviewIdentification.ClientID + "', 426, 950 , 40 ,false); </script>", false);

        //if (heightGrid >= 426 && nbrRows >= 100)

        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GridviewIdentification.ClientID + "', 426, 950 , 40 ,true); </script>", false);

    }

    //-------------------------------------------------------------------------------------------


}