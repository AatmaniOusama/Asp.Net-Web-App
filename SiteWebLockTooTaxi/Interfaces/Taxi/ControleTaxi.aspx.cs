using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLockTooTaxi;
using System.Web.UI.HtmlControls;

public partial class Interfaces_Vehicule_ControleVehicule : System.Web.UI.Page 
{
    int IndexPage;
    double TotalControlesVehicule;
    double TotalControlesVehiculeByFiltre;


    Vehicules taxi = new Vehicules();
    TypeTaxi typeTaxi = new TypeTaxi();

    LabelControle labelControle = new LabelControle();
    Controles controle = new Controles();
    List<Controles> ListeControlesVh;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            TotalControlesVehicule = controle.getTotalControlesVehicule();
            Session["TotalControlesVehicule"] = TotalControlesVehicule;


            TbImmatriculation.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");

           

            Operateur operateur = (Operateur)Session["Operateur"];

            if (operateur.Profil.ToUpper() == "ADMINISTRATEUR")
            {
                BarreControle.Rows[0].Cells[2].Visible = true;
                BarreControle.Rows[0].Cells[3].Visible = true;
                GridViewControlesVehicules.Columns[0].Visible =true;
               
            }

            if (operateur.Profil.ToUpper() == "SUPERVISEUR")
            {
                BarreControle.Rows[0].Cells[2].Visible = true; // Exporter Vers Excel
                BarreControle.Rows[0].Cells[3].Visible = true; // Ajouter Controle Agrément
                GridViewControlesVehicules.Columns[0].Visible = true; // Modifier Controle Agrément

            }

            if (operateur.Profil.ToUpper() == "CONSULTANT")
            {
                BarreControle.Rows[0].Cells[2].Visible = true;
                BarreControle.Rows[0].Cells[3].Visible = false;
                GridViewControlesVehicules.Columns[0].Visible = false;
                Panel_Ajout.Visible = false;
               
            }
           

        }
        catch (Exception)
        { }

        if (!IsPostBack)
        {
            
            ViewState["sortOrder"] = "";


            //List<Controles> listControleUser = controle.getControles();

            DDLAbrvControle.DataSource=labelControle.getLabelsControlesByType(1);
            DDLAbrvControle.DataValueField = "Id";
            DDLAbrvControle.DataTextField = "Abrev";           
            DDLAbrvControle.DataBind();

            _DDLAbrvControleVehicule.DataSource = labelControle.getLabelsControlesByType(1);
            _DDLAbrvControleVehicule.DataValueField = "Id";
            _DDLAbrvControleVehicule.DataTextField = "Abrev";           
            _DDLAbrvControleVehicule.DataBind();

            DDLTypeTaxi.DataSource = typeTaxi.getTypeTaxi();
            DDLTypeTaxi.DataTextField = "Libelle";
            DDLTypeTaxi.DataValueField = "Num";
            DDLTypeTaxi.DataBind();
           

            _DDLTypeTaxi.DataSource = typeTaxi.getTypeTaxi();
            _DDLTypeTaxi.DataTextField = "Libelle";
            _DDLTypeTaxi.DataValueField = "Num";
            _DDLTypeTaxi.DataBind();
            _DDLTypeTaxi.Items.Remove(DDLTypeTaxi.Items.FindByText("Tous"));



            DDLAbrvControle.SelectedValue = Session["TaxiControleAbrevLabelControle"] as string;

            if (Session["TaxiControleDateFinValidite"] as DateTime? != null)
                TbFiltreDateFin.SelectedDate = Session["TaxiControleDateFinValidite"] as DateTime?;
                else
                    TbFiltreDateFin.SelectedDate = null;
            
            TbNTaxi.Text = Session["TaxiControleNumTaxi"] as string;
            DDLTypeTaxi.SelectedValue = Session["TaxiControleTypeTaxi"] as string;
            TbImmatriculation.Text = Session["TaxiControleImmatriculation"] as string;


            if (Session["IndexPageControlesVehicule"] != null)
            {
                IndexPage = Convert.ToInt32(Session["IndexPageControlesVehicule"]);

            }
            else
            {
                Session["IndexPageControlesVehicule"] = 1;
                IndexPage = 1;
            }


            LaodGridView(IndexPage);

        }

        
    }

    protected void LaodGridView(int IndexPage)
    {

        // ---------------  Récupérer les Valeurs des Filtres ----------------------------------------------------------------------------------------------------------------------------------------

        int? ddlAbrevControlesVh = int.Parse(DDLAbrvControle.SelectedValue);
        if (DDLAbrvControle.SelectedValue == "0")
        {
            ddlAbrevControlesVh = null;
        } 



        DateTime? dateFin = TbFiltreDateFin.SelectedDate;
        if (TbFiltreDateFin.IsEmpty)
        { dateFin = null; }



        string numTaxi = TbNTaxi.Text;
        if (numTaxi == "")
        { numTaxi = null; }


        int? ddltypeTaxi = int.Parse(DDLTypeTaxi.SelectedValue);
        if (DDLTypeTaxi.SelectedValue == "-1")
        {
            ddltypeTaxi = null;
        } 

        string immatriculation = TbImmatriculation.Text;
        if (immatriculation == "")
        { immatriculation = null; }

 

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // Verifier si l'un des champs des filtre  remplis ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        if (DDLAbrvControle.SelectedValue != "0" || !TbFiltreDateFin.IsEmpty || TbNTaxi.Text != "" || DDLTypeTaxi.SelectedValue != "-1" || TbImmatriculation.Text != "")
        {
            TotalControlesVehiculeByFiltre = controle.getTotalControlesVehiculeByFiltre(ddlAbrevControlesVh, dateFin, numTaxi,ddltypeTaxi, immatriculation);  
            Session["TotalControlesVehiculeByFiltre"] = TotalControlesVehiculeByFiltre;
            NbrLignes.Text = TotalControlesVehiculeByFiltre.ToString();

        }

        else
        {
            NbrLignes.Text = TotalControlesVehicule.ToString();
        }


        // Fin Vérification -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        ListeControlesVh = controle.getAllControlesVehicule(IndexPage, ddlAbrevControlesVh, dateFin, numTaxi, ddltypeTaxi, immatriculation);

        Session["ListControleVehicule"] = ListeControlesVh;
        GridViewControlesVehicules.DataSource = ListeControlesVh;
        GridViewControlesVehicules.DataBind();

        GridViewControlesVehicules.SelectedIndex = -1; 

        FooterButtonGridViewControlesVehicules();

    }

    protected void FooterButtonGridViewControlesVehicules()
    {
        // Gérer l'affichage des bouttons Next previous... dans le footer -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        if ((ListeControlesVh.Count() < 15 && TotalControlesVehicule < 15) || (ListeControlesVh.Count() < 15 && Convert.ToInt32(Session["TotalControlesVehiculeByFiltre"]) < 15 && Convert.ToInt32(Session["TotalControlesVehiculeByFiltre"]) > 0) || TotalControlesVehicule == 15 || (Convert.ToInt32(Session["TotalControlesVehiculeByFiltre"]) == 15 && Convert.ToInt32(Session["TotalControlesVehiculeByFiltre"]) != 0))
        {
            GridViewControlesVehicules.FooterRow.Cells[3].Enabled = false;
            GridViewControlesVehicules.FooterRow.Cells[2].Enabled = false;


        }


        if (DDLAbrvControle.SelectedValue != "0" || !TbFiltreDateFin.IsEmpty || TbNTaxi.Text != "" || DDLTypeTaxi.SelectedValue != "-1" || TbImmatriculation.Text != "")
        {

            if (IndexPage == 1 && Convert.ToInt32(Session["TotalControlesVehiculeByFiltre"]) != 0)
            {
                GridViewControlesVehicules.FooterRow.Cells[2].Enabled = false;
            }
            else
            {

                TotalControlesVehiculeByFiltre = Convert.ToInt32(Session["TotalControlesVehiculeByFiltre"]);
                int T = (int)TotalControlesVehiculeByFiltre / 15;
                if (T < TotalControlesVehiculeByFiltre / 15)
                {
                    if (IndexPage == T + 1)
                    {
                        GridViewControlesVehicules.FooterRow.Cells[3].Enabled = false;

                    }

                }
                else
                {
                    if (IndexPage == T)
                    {
                        GridViewControlesVehicules.FooterRow.Cells[3].Enabled = false;


                    }
                }
            }

        }
        else
        {
            if (IndexPage == 1 && TotalControlesVehicule != 0)
            {
                GridViewControlesVehicules.FooterRow.Cells[2].Enabled = false;
            }
            else
            {


                int TA = (int)TotalControlesVehicule / 15;
                if (TA < TotalControlesVehicule / 15)
                {
                    if (IndexPage == TA + 1)
                    {
                        GridViewControlesVehicules.FooterRow.Cells[3].Enabled = false;


                    }

                }
                else
                {
                    if (IndexPage == TA)
                    {

                        GridViewControlesVehicules.FooterRow.Cells[3].Enabled = false;

                    }
                }
            }
        }
        // Fin -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    }

    protected void Filtre_Click(object sender, EventArgs e)
    {


        Session["TaxiControleAbrevLabelControle"] = DDLAbrvControle.SelectedValue;
        Session["TaxiControleDateFinValidite"] = TbFiltreDateFin.SelectedDate;
        Session["TaxiControleNumTaxi"] = TbNTaxi.Text;
        Session["TaxiControleTypeTaxi"] = DDLTypeTaxi.Text;
        Session["TaxiControleImmatriculation"] = TbImmatriculation.Text;

        if (DDLAbrvControle.SelectedValue != "0" || !TbFiltreDateFin.IsEmpty || TbNTaxi.Text != "" || DDLTypeTaxi.SelectedValue != "-1" || TbImmatriculation.Text != "")
        {
            Session["IndexPageControlesVehicule"] = 1;
            IndexPage = 1;
        }
        else
        {
            if (Session["IndexPageControlesVehicule"] != null)
            {
                IndexPage = Convert.ToInt32(Session["IndexPageControlesVehicule"]);
            }
            else
            {
                Session["IndexPageControlesVehicule"] = 1;
                IndexPage = 1;

            }
        }


        LaodGridView(IndexPage);
    }

    protected void GridViewControlesVehicules_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewControlesVehicules.PageIndex = e.NewPageIndex;

        GridViewControlesVehicules.DataSource = Session["ListControleVehicule"] as List<Controles>;
        GridViewControlesVehicules.DataBind();

        GridViewControlesVehicules.FooterRow.Cells[5].Text = "Page : " + Convert.ToInt32(Session["IndexPageControlesVehicule"]) + "";

    }

    protected void GridViewControlesVehicules_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if ((e.Row.Cells[3].FindControl("LblNTaxi") as Label).Text == "0")
                (e.Row.Cells[3].FindControl("LblNTaxi") as Label).Text = "";

            if ((e.Row.Cells[4].FindControl("LblTypeTaxi") as Label).Text == "0")
                (e.Row.Cells[4].FindControl("LblTypeTaxi") as Label).Text = "";

            if ((e.Row.Cells[5].FindControl("LblImmatriculation") as Label).Text == "0")
                (e.Row.Cells[5].FindControl("LblImmatriculation") as Label).Text = "";

         
            
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {


            if (DDLAbrvControle.SelectedValue != "0" || !TbFiltreDateFin.IsEmpty || TbNTaxi.Text != "" || DDLTypeTaxi.SelectedValue != "-1" || TbImmatriculation.Text != "")
            {
                int Q = Convert.ToInt32(Session["TotalControlesVehiculeByFiltre"]) / 15;
                int R = Convert.ToInt32(Session["TotalControlesVehiculeByFiltre"]) % 15;

                if (R > 0)
                {
                    Q = Q + 1;
                    e.Row.Cells[5].Text = "Page : " + Convert.ToInt32(Session["IndexPageControlesVehicule"]) + " / " + Q + "";
                }
                else
                {
                    e.Row.Cells[5].Text = "Page : " + Convert.ToInt32(Session["IndexPageControlesVehicule"]) + " / " + Q + "";
                }
            }
            else
            {
                int Q = Convert.ToInt32(Session["TotalControlesVehicule"]) / 15;
                int R = Convert.ToInt32(Session["TotalControlesVehicule"]) % 15;

                if (R > 0)
                {
                    Q = Q + 1;
                    e.Row.Cells[5].Text = "Page : " + Convert.ToInt32(Session["IndexPageControlesVehicule"]) + " / " + Q + "";
                }
                else
                {
                    e.Row.Cells[5].Text = "Page : " + Convert.ToInt32(Session["IndexPageControlesVehicule"]) + " / " + Q + "";
                }

            }

           
        }
       
    }

    protected void GridViewControlesVehicules_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewControlesVehicules.EditIndex = e.NewEditIndex;

        if (Session["IndexPageControlesVehicule"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageControlesVehicule"]);

        }
        else
        {
            IndexPage = 1;
        }


        LaodGridView(IndexPage);
    }

    protected void GridViewControlesVehicules_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridViewControlesVehicules.EditIndex = -1;

        if (Session["IndexPageControlesVehicule"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageControlesVehicule"]);

        }
        else
        {
            IndexPage = 1;
        }


        LaodGridView(IndexPage);
    }
   
    protected void GridViewControlesVehicules_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ImageButton lkDelete = GridViewControlesVehicules.Rows[e.RowIndex].FindControl("lbDelete") as ImageButton;
        int i = 0;
        i=controle.SupprimerUnControle(int.Parse(lkDelete.CommandArgument.ToString()));
        if (i == 1)
        {
            GridViewControlesVehicules.EditIndex = -1;
           
            if (Session["IndexPageControlesVehicule"] != null)
            {
                IndexPage = Convert.ToInt32(Session["IndexPageControlesVehicule"]);

            }
            else
            {
                IndexPage = 1;
            }


            LaodGridView(IndexPage);
        }
    }

    protected void _TbNumTaxi_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Controles controle = new Controles();
            Agrement agrement = new Agrement();
            string NumTaxi = _TbNumTaxi.Text;

           
                if (NumTaxi != "")
                {
                    string TypeTaxi = _DDLTypeTaxi.SelectedItem.Text;
                    int numTaxiExiste = agrement.numTaxiExiste(NumTaxi, TypeTaxi);

                    if (numTaxiExiste == 1)
                    {
                        string Immat = controle.getImmatByNumTaxiTypeTaxi(NumTaxi, TypeTaxi);
                        _TbImmatriculation.Text = Immat;
                      
                    }
                    else
                    {
                        _TbNumTaxi.Text = "";
                        _TbImmatriculation.Text = "";

                        
                    }
                }
                else
                {
                    _TbNumTaxi.Text = "";
                    _TbImmatriculation.Text = "";
                }

            
        }
        catch (Exception)
        {
            _TbNumTaxi.Text = "";
            _TbImmatriculation.Text = "";
        }
        
    }
  
    protected void BtSave_Click(object sender, EventArgs e)
    {
        Operateur operateur = (Operateur)Session["Operateur"];
       
        Controles item = new Controles();
        Agrement agrement = new Agrement();
        
        int i = 0;

        string NumTaxi = _TbNumTaxi.Text;       
        string TypeTaxi = _DDLTypeTaxi.SelectedItem.Text;
        int type = agrement.getTypeTaxi(TypeTaxi);
        string NumImmat = _TbImmatriculation.Text;

        string AbrevControleVehicule=_DDLAbrvControleVehicule.SelectedItem.Text;


        if (!(item.controleExiste(NumTaxi,TypeTaxi, AbrevControleVehicule)))
            {
                item.IdLabelControle = int.Parse(_DDLAbrvControleVehicule.SelectedValue);
                item.DateFin = _TbDateFin.Text;
               

                item.IdVehicule = agrement.getUnAgrement(NumTaxi,type).IdVehicule;
                item.IdOpCreation = operateur.ID;
                item.IdOpModif = operateur.ID;
                i = controle.AjouterUnControle(item);
                

                    if (i == 1)
                    {
                        Response.Redirect("~/Interfaces/Taxi/ControleTaxi.aspx");
                       
                    }

            }

            else
            {


                string myStringVariable = "Impossible d'ajouter ce type de contrôle sur ce Taxi, Car il existe déjà !! ";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + myStringVariable + "')", true);

                MPE2.Hide();
                   
               
            }


        }

    protected void BtCancel_Click(object sender, EventArgs e)
    {
        MPE2.Hide();

    }

    protected void GridViewControlesVehicules_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      

         if (e.CommandName == "Save")
        {
            GridViewRow Row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            TextBox _TbDateFin = Row.FindControl("TbSetDateFin") as TextBox;
            Controles item = new Controles();

            item.IdOpModif = (Session["Operateur"] as Operateur).ID;
            item.DateFin = _TbDateFin.Text;
            item.Id = int.Parse(e.CommandArgument.ToString());

            controle.ModifierUnControle(item);

            GridViewControlesVehicules.EditIndex = -1;

            if (Session["IndexPageControlesVehicule"] != null)
            {
                IndexPage = Convert.ToInt32(Session["IndexPageControlesVehicule"]);

            }
            else
            {
                IndexPage = 1;
            }


            LaodGridView(IndexPage);
 
        }
       
    }

    protected void CbFiltre_CheckedChanged(object sender, EventArgs e)
    {
        DivFilter.Visible = CbFiltre.Checked;

    }

    protected void BtnLast_Click(object sender, EventArgs e)
    {
        if (DDLAbrvControle.SelectedValue != "0" || !TbFiltreDateFin.IsEmpty || TbNTaxi.Text != "" || DDLTypeTaxi.SelectedValue != "-1" || TbImmatriculation.Text != "")
        {



            TotalControlesVehiculeByFiltre = Convert.ToInt32(Session["TotalControlesVehiculeByFiltre"]);
            int T = (int)TotalControlesVehiculeByFiltre / 15;
            if (T < TotalControlesVehiculeByFiltre / 15)
            {
                Session["IndexPageControlesVehicule"] = ((int)TotalControlesVehiculeByFiltre / 15) + 1;
            }
            else
            {
                Session["IndexPageControlesVehicule"] = ((int)TotalControlesVehiculeByFiltre / 15);
            }
        }
        else
        {
            TotalControlesVehicule = Convert.ToInt32(Session["TotalControlesVehicule"]);
            int T = (int)TotalControlesVehicule / 15;
            if (T < TotalControlesVehicule / 15)
            {
                Session["IndexPageControlesVehicule"] = ((int)TotalControlesVehicule / 15) + 1;
            }
            else
            {
                Session["IndexPageControlesVehicule"] = ((int)TotalControlesVehicule / 15);
            }


        }


        IndexPage = Convert.ToInt32(Session["IndexPageControlesVehicule"]);
        LaodGridView(IndexPage);
    }

    protected void BtnNext_Click(object sender, EventArgs e)
    {



        if (Session["IndexPageControlesVehicule"] != null)
        {


            Session["IndexPageControlesVehicule"] = Convert.ToInt32(Session["IndexPageControlesVehicule"]) + 1;
            IndexPage = Convert.ToInt32(Session["IndexPageControlesVehicule"]);


        }


        else
        {
            Session["IndexPageControlesVehicule"] = 2;
            IndexPage = 2;
        }

        LaodGridView(IndexPage);


    }

    protected void BtnPrevious_Click(object sender, EventArgs e)
    {

        Session["IndexPageControlesVehicule"] = Convert.ToInt32(Session["IndexPageControlesVehicule"]) - 1;
        IndexPage = Convert.ToInt32(Session["IndexPageControlesVehicule"]);
        LaodGridView(IndexPage);

    }

    protected void BtnFirst_Click(object sender, EventArgs e)
    {
        Session["IndexPageControlesVehicule"] = 1;
        IndexPage = Convert.ToInt32(Session["IndexPageControlesVehicule"]);
        LaodGridView(IndexPage);
    }

    protected void exportExcel_Click(object sender, EventArgs e)
    {

        Response.Clear();
        string FileName = "attachment;filename=" + this.Page.Title + ".xls";
        Response.AddHeader("content-disposition", FileName);

        Response.ContentEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");

        Response.ContentType = "application/vnd.xlsx";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        HtmlForm form = new HtmlForm();
        Page mypage = new System.Web.UI.Page();

        GridViewControlesVehicules.AllowPaging = false;
        GridViewControlesVehicules.AllowSorting = false;
        GridViewControlesVehicules.ShowFooter = false;

        GridViewControlesVehicules.EditIndex = -1;


        // ---------------  Récupérer les Valeurs des Filtres ----------------------------------------------------------------------------------------------------------------------------------------

        int? ddlAbrevControlesVh = int.Parse(DDLAbrvControle.SelectedValue);
        if (DDLAbrvControle.SelectedValue == "0")
        {
            ddlAbrevControlesVh = null;
        }



        DateTime? dateFin = TbFiltreDateFin.SelectedDate;
        if (TbFiltreDateFin.IsEmpty)
        { dateFin = null; }



        string numTaxi = TbNTaxi.Text;
        if (numTaxi == "")
        { numTaxi = null; }


        int? ddltypeTaxi = int.Parse(DDLTypeTaxi.SelectedValue);
        if (DDLTypeTaxi.SelectedValue == "-1")
        {
            ddltypeTaxi = null;
        }

        string immatriculation = TbImmatriculation.Text;
        if (immatriculation == "")
        { immatriculation = null; }


        GridViewControlesVehicules.DataSource = (new Controles()).getAllControlesVehiculeByFiltre(ddlAbrevControlesVh, dateFin, numTaxi, ddltypeTaxi, immatriculation);    
        GridViewControlesVehicules.DataBind();


        GridViewControlesVehicules.Columns[GridViewControlesVehicules.Columns.Count - 1].Visible = false;
        GridViewControlesVehicules.Columns[0].Visible = false;

        PrepareGridViewForExport(GridViewControlesVehicules);

        mypage.Controls.Add(form);
        form.Controls.Add(GridViewControlesVehicules);

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
        // Tester s'il y un filtre ou pas 
        if (DDLAbrvControle.SelectedValue != "0" || !TbFiltreDateFin.IsEmpty || TbNTaxi.Text != "" || DDLTypeTaxi.SelectedValue != "-1" || TbImmatriculation.Text != "")
        {


            int? ddlAbrevControlesVh = int.Parse(DDLAbrvControle.SelectedValue);
            if (DDLAbrvControle.SelectedValue == "0")
            {
                ddlAbrevControlesVh = null;
            }



            DateTime? dateFin = TbFiltreDateFin.SelectedDate;
            if (TbFiltreDateFin.IsEmpty)
            { dateFin = null; }



            string numTaxi = TbNTaxi.Text;
            if (numTaxi == "")
            { numTaxi = null; }


            int? ddltypeTaxi = int.Parse(DDLTypeTaxi.SelectedValue);
            if (DDLTypeTaxi.SelectedValue == "-1")
            {
                ddltypeTaxi = null;
            }

            string immatriculation = TbImmatriculation.Text;
            if (immatriculation == "")
            { immatriculation = null; }



            ListeControlesVh = controle.getAllControlesVehiculeByFiltre(ddlAbrevControlesVh, dateFin, numTaxi, ddltypeTaxi, immatriculation);

        }
        else
        {
            ListeControlesVh = controle.getControles();
        }


        switch (sortExp)
        {


            case "DateFin":
                if (sortDir == "desc")
                    ListeControlesVh = ListeControlesVh.OrderByDescending(o => DateTime.Parse(o.DateFin)).ToList();
                else
                    ListeControlesVh = ListeControlesVh.OrderBy(o => DateTime.Parse(o.DateFin)).ToList();

                break;


            case "NumTaxi":
                foreach (Controles controle in ListeControlesVh)
                {
                    if (string.IsNullOrWhiteSpace(controle.NumTaxi) || string.IsNullOrEmpty(controle.NumTaxi))
                        controle.NumTaxi = "0";
                }
                if (sortDir == "desc")
                    ListeControlesVh = ListeControlesVh.OrderByDescending(o => int.Parse(o.NumTaxi)).ToList();
                else
                    ListeControlesVh = ListeControlesVh.OrderBy(o => int.Parse(o.NumTaxi)).ToList();
                break;

            case "TypeTaxi":
                foreach (Controles controle in ListeControlesVh)
                {
                    if (string.IsNullOrWhiteSpace(controle.TypeTaxi) || string.IsNullOrEmpty(controle.TypeTaxi))
                        controle.TypeTaxi = "0";
                }
                if (sortDir == "desc")
                    ListeControlesVh = ListeControlesVh.OrderByDescending(o => o.TypeTaxi).ToList();
                else
                    ListeControlesVh = ListeControlesVh.OrderBy(o => o.TypeTaxi).ToList();
                break;

            case "Immat":
                foreach (Controles controle in ListeControlesVh)
                {
                    if (string.IsNullOrWhiteSpace(controle.Immat) || string.IsNullOrEmpty(controle.Immat))
                        controle.Immat = "0";
                }
                if (sortDir == "desc")
                    ListeControlesVh = ListeControlesVh.OrderByDescending(o => o.Immat).ToList();
                else
                    ListeControlesVh = ListeControlesVh.OrderBy(o => o.Immat).ToList();
                break;


        }


        if (Session["IndexPageControlesVehicule"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageControlesVehicule"]);
        }
        else
        {
            Session["IndexPageControlesVehicule"] = 1;
            IndexPage = 1;

        }



        Session["ListControleVehicule"] = ListeControlesVh.Skip((Convert.ToInt32(Session["IndexPageAgrement"]) - 1) * 15).Take(15);

        GridViewControlesVehicules.DataSource = ListeControlesVh.Skip((Convert.ToInt32(Session["IndexPageAgrement"]) - 1) * 15).Take(15);
        GridViewControlesVehicules.DataBind();
        FooterButtonGridViewControlesVehicules();


        

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

    protected void Taxi_Controls_Sorting(object sender, GridViewSortEventArgs e)
    {
        BindGridView(e.SortExpression, sortOrder);


        int columnIndex = 0;

        foreach (DataControlFieldHeaderCell headerCell in GridViewControlesVehicules.HeaderRow.Cells)
        {
            if (headerCell.ContainingField.SortExpression == e.SortExpression)
            {
                columnIndex = GridViewControlesVehicules.HeaderRow.Cells.GetCellIndex(headerCell);
            }
        }

        GridViewControlesVehicules.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);

      


    }

    //-------------------------------------------------------------------------------------------


}