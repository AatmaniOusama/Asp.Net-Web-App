
using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLockTooTaxi;
using System.Text.RegularExpressions;
using DataLockTooTaxi.MyDataSetTableAdapters;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls.WebParts;



public partial class CarteGrise_index : System.Web.UI.Page
{
    int IndexPage;
    double TotalCarteGrises;
    double TotalCarteGrisesByFiltre;

    Vehicules vehicule = new Vehicules();
    List<Vehicules> ListVehicules;
    VehiculesTableAdapter VehiculesAdapeter = new VehiculesTableAdapter();


    protected void Page_Load(object sender, EventArgs e)
    {
        



        try
        {
            TotalCarteGrises = vehicule.getTotalCarteGrises();
            Session["TotalCarteGrises"] = TotalCarteGrises;


            TbImmat.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbModele.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbMarque.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbNomProprietaire.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbPrenomProprietaire.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbCinProprietaire.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");


           

            Operateur operateur = (Operateur)Session["Operateur"];

            if (operateur.Profil.ToUpper() == "ADMINISTRATEUR")
            {            
                BarreControle.Rows[0].Cells[2].Visible = true;
                BarreControle.Rows[0].Cells[3].Visible = true;
                BarreControle.Rows[0].Cells[4].Visible = true;
                BarreControle.Rows[0].Cells[5].Visible = true;
            }

            if (operateur.Profil.ToUpper() == "SUPERVISEUR")
            {
                BarreControle.Rows[0].Cells[2].Visible = true;  // Exporter vers Excel
                BarreControle.Rows[0].Cells[3].Visible = false; // Ajouter Carte Grise
                BarreControle.Rows[0].Cells[4].Visible = false; // Modifier Carte Grise
                BarreControle.Rows[0].Cells[5].Visible = false; // Supprimer Carte Grise
             }

            if (operateur.Profil.ToUpper() == "CONSULTANT")
            {
                BarreControle.Rows[0].Cells[2].Visible = true;
                BarreControle.Rows[0].Cells[3].Visible = false;
                BarreControle.Rows[0].Cells[4].Visible = false;
                BarreControle.Rows[0].Cells[5].Visible = false;
            }
            



            if (operateur.MenusAutorises.Substring(0, 1) == "0")
            {
                Session.Abandon();
                Response.Redirect("~/");
            }



            if (!IsPostBack)
            {

                ViewState["sortOrder"] = "";


                if (Session["CarteGriseDateImmat"] as DateTime? != null)
                    TbDateImmat.SelectedDate = Session["CarteGriseDateImmat"] as DateTime?;
                else
                    TbDateImmat.SelectedDate = null;

                if (Session["CarteGriseDateMiseEnCirculation"] as DateTime? != null)
                    TbDateMiseEnCirculation.SelectedDate = Session["CarteGriseDateMiseEnCirculation"] as DateTime?;
                else
                    TbDateMiseEnCirculation.SelectedDate = null;

                
                TbImmat.Text = Session["ImmatCarteGrise"] as string;
                TbModele.Text = Session["ModeleCarteGrise"] as string;
                TbMarque.Text = Session["MarqueCarteGrise"] as string;
                TbNomProprietaire.Text = Session["NomCarteGrise"] as string;
                TbPrenomProprietaire.Text = Session["PrenomCarteGrise"] as string;
                TbCinProprietaire.Text = Session["CinCarteGrise"] as string;

                if (Session["IndexPageCarteGrise"] != null)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageCarteGrise"]);

                }
                else
                {
                    Session["IndexPageCarteGrise"] = 1;
                    IndexPage = 1;
                }

                if (Convert.ToBoolean(Session["SelectCarteGriseSet"]) == true)
                {
                    LaodGridView(IndexPage);
                    GridViewRow row = Session["SelectCarteGriseSet"] as GridViewRow;
                    GridViewCartesGrises.SelectRow(row.RowIndex);
                    Session["SelectCarteGriseSet"] = false;
                }
                else if (Convert.ToBoolean(Session["SelectCarteGriseAdd"]) == true)
                {
                    LaodGridView(IndexPage);
                    GridViewCartesGrises.SelectRow(0);
                    Session["SelectCarteGriseAdd"] = false;
                }
                else
                {
                    LaodGridView(IndexPage);

                }
    


            }
        }
        catch (Exception)
        {
        }
    }

    protected void LaodGridView(int IndexPage)
    {

        // ---------------  Récupérer les Valeurs des Filtres ----------------------------------------------------------------------------------------------------------------------------------------

        DateTime? dateImmat = TbDateImmat.SelectedDate;
        if (TbDateImmat.IsEmpty)
        { dateImmat = null; }

        DateTime? dateMiseEnCirculation = TbDateMiseEnCirculation.SelectedDate;
        if (TbDateMiseEnCirculation.IsEmpty)
        { dateMiseEnCirculation = null; }


        string immat = TbImmat.Text;
        if (immat == "")
        { immat = null; }

        string marque = TbMarque.Text;
        if (marque == "")
        { marque = null; }


        string modele = TbModele.Text;
        if (modele == "")
        { modele = null; }


        string nom = TbNomProprietaire.Text;
        if (nom == "")
        { nom = null; }

        string prenom = TbPrenomProprietaire.Text;
        if (prenom == "")
        { prenom = null; }

        string cin = TbCinProprietaire.Text;
        if (cin == "")
        { cin = null; }

     

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        // Verifier si l'un des champs des filtre  remplis ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        if (!TbDateImmat.IsEmpty || !TbDateMiseEnCirculation.IsEmpty || TbImmat.Text != "" || TbMarque.Text != "" || TbModele.Text != "" || TbNomProprietaire.Text != "" || TbPrenomProprietaire.Text != "" || TbCinProprietaire.Text != "" )
        {
            TotalCarteGrisesByFiltre = vehicule.getTotalCarteGrisesByFiltre(dateImmat, dateMiseEnCirculation, immat, marque, modele, nom, prenom, cin);  
            Session["TotalCarteGrisesByFiltre"] = TotalCarteGrisesByFiltre;
            NbrLignes.Text = TotalCarteGrisesByFiltre.ToString();

        }

        else
        {
            NbrLignes.Text = TotalCarteGrises.ToString();
        }


        // Fin Vérification -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ListVehicules = vehicule.getCarteGrises(IndexPage, dateImmat, dateMiseEnCirculation, immat, marque, modele, nom, prenom, cin);
         
    
        Session["ListCartesGrises"] = ListVehicules;

        GridViewCartesGrises.DataSource = ListVehicules;
        GridViewCartesGrises.DataBind();
       

      
        BtnSet.Visible = false;
        BtnDelete.Visible = false;

        GridViewCartesGrises.SelectedIndex = -1;

        FooterButtonGridViewCartesGrises();
    }

    protected void FooterButtonGridViewCartesGrises()
    {

        // Gérer l'affichage des bouttons Next previous... dans le footer -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        if (((ListVehicules.Count() < 15 && TotalCarteGrises < 15) || (ListVehicules.Count() < 15 && Convert.ToInt32(Session["TotalCarteGrisesByFiltre"]) < 15) || TotalCarteGrises == 15 || Convert.ToInt32(Session["TotalCarteGrisesByFiltre"]) == 15) && Convert.ToInt32(Session["TotalCarteGrisesByFiltre"]) != 0)
        {
            GridViewCartesGrises.FooterRow.Cells[5].Enabled = false;
            GridViewCartesGrises.FooterRow.Cells[4].Enabled = false;

        }

        if (!TbDateImmat.IsEmpty || !TbDateMiseEnCirculation.IsEmpty || TbImmat.Text != "" || TbMarque.Text != "" || TbModele.Text != "" || TbNomProprietaire.Text != "" || TbPrenomProprietaire.Text != "" || TbCinProprietaire.Text != "")
        {
            if (IndexPage == 1 && Convert.ToInt32(Session["TotalCarteGrisesByFiltre"]) != 0)
            {
                GridViewCartesGrises.FooterRow.Cells[4].Enabled = false;
            }
            else
            {

                TotalCarteGrisesByFiltre = Convert.ToInt32(Session["TotalCarteGrisesByFiltre"]);
                int T = (int)TotalCarteGrisesByFiltre / 15;

                if (T < TotalCarteGrisesByFiltre / 15)
                {
                    if (IndexPage == T + 1)
                    {
                        GridViewCartesGrises.FooterRow.Cells[5].Enabled = false;


                    }

                }
                else
                {
                    if (IndexPage == T)
                    {
                        GridViewCartesGrises.FooterRow.Cells[5].Enabled = false;


                    }
                }
            }
        }


        else
        {

            if (IndexPage == 1 && TotalCarteGrises != 0)
            {
                GridViewCartesGrises.FooterRow.Cells[4].Enabled = false;
            }
            else
            {


                int TU = (int)TotalCarteGrises / 15;

                if (TU < TotalCarteGrises / 15)
                {
                    if (IndexPage == TU + 1)
                    {
                        GridViewCartesGrises.FooterRow.Cells[5].Enabled = false;


                    }

                }
                else
                {
                    if (IndexPage == TU)
                    {
                        GridViewCartesGrises.FooterRow.Cells[5].Enabled = false;


                    }
                }
            }

        }

        // Fin -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    }

    protected void Filtre(object sender, EventArgs e)
    {
        Session["CarteGriseDateImmat"] = TbDateImmat.SelectedDate;
        Session["CarteGriseDateMiseEnCirculation"] = TbDateMiseEnCirculation.SelectedDate;       
        Session["ImmatCarteGrise"] = TbImmat.Text;
        Session["ModeleCarteGrise"] = TbModele.Text;
        Session["MarqueCarteGrise"] = TbMarque.Text;
        Session["NomCarteGrise"] = TbNomProprietaire.Text;
        Session["PrenomCarteGrise"] = TbPrenomProprietaire.Text;
        Session["CinCarteGrise"] = TbCinProprietaire.Text;



        if (!TbDateImmat.IsEmpty || !TbDateMiseEnCirculation.IsEmpty || TbImmat.Text != "" || TbMarque.Text != "" || TbModele.Text != "" || TbNomProprietaire.Text != "" || TbPrenomProprietaire.Text != "" || TbCinProprietaire.Text != "")
        {
            Session["IndexPageCarteGrise"] = 1;
            IndexPage = 1;

        }

        if (Session["IndexPageCarteGrise"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageCarteGrise"]);

        }
        else
        {
            Session["IndexPageCarteGrise"] = 1;
            IndexPage = 1;

        }


        LaodGridView(IndexPage);
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {


        Session["CarteGriseAjout"] = true; 
     
        Response.Redirect("~/Interfaces/Taxi/DetailCarteGrise.aspx");
    }

    protected void BtnSet_Click(object sender, EventArgs e)
    {
        GridViewRow row = GridViewCartesGrises.SelectedRow;
     
        Session["ModifCarteGrise"] = (row.FindControl("LblImmat") as Label).Text;
        Session["CarteGriseAjout"] = false;
        Session["CarteGriseRowselected"] = row;
        Response.Redirect("~/Interfaces/Taxi/DetailCarteGrise.aspx");

    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        GridViewRow row = GridViewCartesGrises.SelectedRow;
        string Immat = (row.FindControl("LblImmat") as Label).Text;


       int  i = vehicule.SupprimerUnVehicule(Immat);

        if (i == 1)
        {
            Response.Redirect("~/Interfaces/Taxi/CarteGrise.aspx");
        }

        

    }

    protected void GridViewCartesGrises_IndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewCartesGrises.PageIndex = e.NewPageIndex;

        GridViewCartesGrises.DataSource = Session["ListCartesGrises"] as List<Vehicules>;
        GridViewCartesGrises.DataBind();
     
    }

    protected void CbFiltre_CheckedChanged(object sender, EventArgs e)
    {
        Panelfilter.Visible = CbFiltre.Checked;

    }

    protected void GridViewCartesGrises_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow row = GridViewCartesGrises.Rows[e.NewSelectedIndex];

        BtnDelete.Visible = true;
        BtnSet.Visible = true;

       

    }

    protected void GridViewCartesGrises_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Add onclick attribute to select row.
            e.Row.Attributes.Add("onclick", String.Format("javascript:__doPostBack('ctl00$MainContent$GridViewCartesGrises','Select${0}')", e.Row.RowIndex));

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[8].Text = "Page : " + IndexPage + "";

            if (!TbDateImmat.IsEmpty || !TbDateMiseEnCirculation.IsEmpty || TbImmat.Text != "" || TbMarque.Text != "" || TbModele.Text != "" || TbNomProprietaire.Text != "" || TbPrenomProprietaire.Text != "" || TbCinProprietaire.Text != "")
            {
                int Q = Convert.ToInt32(Session["TotalCarteGrisesByFiltre"]) / 15;
                int R = Convert.ToInt32(Session["TotalCarteGrisesByFiltre"]) % 15;

                if (R > 0)
                {
                    Q = Q + 1;
                    e.Row.Cells[8].Text = "Page : " + Convert.ToInt32(Session["IndexPageCarteGrise"]) + " / " + Q + "";
                }
                else
                {
                    e.Row.Cells[8].Text = "Page : " + Convert.ToInt32(Session["IndexPageCarteGrise"]) + " / " + Q + "";
                }
            }
            else
            {
                int Q = Convert.ToInt32(Session["TotalCarteGrises"]) / 15;
                int R = Convert.ToInt32(Session["TotalCarteGrises"]) % 15;

                if (R > 0)
                {
                    Q = Q + 1;
                    e.Row.Cells[8].Text = "Page : " + Convert.ToInt32(Session["IndexPageCarteGrise"]) + " / " + Q + "";
                }
                else
                {
                    e.Row.Cells[8].Text = "Page : " + Convert.ToInt32(Session["IndexPageCarteGrise"]) + " / " + Q + "";
                }

            }

        }

    }

    protected void GridViewCartesGrises_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void BtnLast_Click(object sender, EventArgs e)
    {
        if (!TbDateImmat.IsEmpty || !TbDateMiseEnCirculation.IsEmpty || TbImmat.Text != "" || TbMarque.Text != "" || TbModele.Text != "" || TbNomProprietaire.Text != "" || TbPrenomProprietaire.Text != "" || TbCinProprietaire.Text != "")
        {


            TotalCarteGrisesByFiltre = Convert.ToInt32(Session["TotalCarteGrisesByFiltre"]);
            int T = (int)TotalCarteGrisesByFiltre / 15;
            if (T < TotalCarteGrisesByFiltre / 15)
            {
                Session["IndexPageCarteGrise"] = ((int)TotalCarteGrisesByFiltre / 15) + 1;
            }
            else
            {
                Session["IndexPageCarteGrise"] = ((int)TotalCarteGrisesByFiltre / 15);
            }
        }
        else
        {
            TotalCarteGrises = Convert.ToInt32(Session["TotalCarteGrises"]);
            int T = (int)TotalCarteGrises / 15;
            if (T < TotalCarteGrises / 15)
            {
                Session["IndexPageCarteGrise"] = ((int)TotalCarteGrises / 15) + 1;
            }
            else
            {
                Session["IndexPageCarteGrise"] = ((int)TotalCarteGrises / 15);
            }


        }


        IndexPage = Convert.ToInt32(Session["IndexPageCarteGrise"]);
        LaodGridView(IndexPage);
    }

    protected void BtnNext_Click(object sender, EventArgs e)
    {



        if (Session["IndexPageCarteGrise"] != null)
        {


            Session["IndexPageCarteGrise"] = Convert.ToInt32(Session["IndexPageCarteGrise"]) + 1;
            IndexPage = Convert.ToInt32(Session["IndexPageCarteGrise"]);


        }


        else
        {
            Session["IndexPageCarteGrise"] = 2;
            IndexPage = 2;
        }

        LaodGridView(IndexPage);


    }

    protected void BtnPrevious_Click(object sender, EventArgs e)
    {

        Session["IndexPageCarteGrise"] = Convert.ToInt32(Session["IndexPageCarteGrise"]) - 1;
        IndexPage = Convert.ToInt32(Session["IndexPageCarteGrise"]);
        LaodGridView(IndexPage);

    }

    protected void BtnFirst_Click(object sender, EventArgs e)
    {
        Session["IndexPageCarteGrise"] = 1;
        IndexPage = Convert.ToInt32(Session["IndexPageCarteGrise"]);
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

        GridViewCartesGrises.AllowPaging = false;
        GridViewCartesGrises.AllowSorting = false;
        GridViewCartesGrises.ShowFooter = false;

        GridViewCartesGrises.EditIndex = -1;



        GridViewCartesGrises.DataSource = Session["ListCartesGrises"] as List<Vehicules>;
        GridViewCartesGrises.DataBind();


        GridViewCartesGrises.Columns[GridViewCartesGrises.Columns.Count - 1].Visible = false;
        GridViewCartesGrises.Columns[0].Visible = false;

        PrepareGridViewForExport(GridViewCartesGrises);

        mypage.Controls.Add(form);
        form.Controls.Add(GridViewCartesGrises);

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

    //------------------------------ Fonctions de Tri ------------------------------------------

    private void BindGridView(string sortExp, string sortDir)
    {
        // Tester s'il y un filtre ou pas 

        if (!TbDateImmat.IsEmpty || !TbDateMiseEnCirculation.IsEmpty || TbImmat.Text != "" || TbMarque.Text != "" || TbModele.Text != "" || TbNomProprietaire.Text != "" || TbPrenomProprietaire.Text != "" || TbCinProprietaire.Text != "")
        {
            DateTime? dateImmat = TbDateImmat.SelectedDate;
            if (TbDateImmat.IsEmpty)
            { dateImmat = null; }

            DateTime? dateMiseEnCirculation = TbDateMiseEnCirculation.SelectedDate;
            if (TbDateMiseEnCirculation.IsEmpty)
            { dateMiseEnCirculation = null; }


            string immat = TbImmat.Text;
            if (immat == "")
            { immat = null; }

            string marque = TbMarque.Text;
            if (marque == "")
            { marque = null; }


            string modele = TbModele.Text;
            if (modele == "")
            { modele = null; }


            string nom = TbNomProprietaire.Text;
            if (nom == "")
            { nom = null; }

            string prenom = TbPrenomProprietaire.Text;
            if (prenom == "")
            { prenom = null; }

            string cin = TbCinProprietaire.Text;
            if (cin == "")
            { cin = null; }

            ListVehicules = vehicule.getAllCarteGrisesByFiltre(dateImmat, dateMiseEnCirculation, immat, marque, modele, nom, prenom, cin);
        }
        else
        {
            ListVehicules = vehicule.getAllCarteGrises();
        }

       

        switch (sortExp)
        {
            case "Immat":
                if (sortDir == "desc")
                    ListVehicules = ListVehicules.OrderByDescending(o => o.Immat).ToList();
                else
                    ListVehicules = ListVehicules.OrderBy(o => o.Immat).ToList();
                break;

            case "DateImmat":
                if (sortDir == "desc")
                    ListVehicules = ListVehicules.OrderByDescending(o => DateTime.Parse(o.DateImmat)).ToList();
                else
                    ListVehicules = ListVehicules.OrderBy(o => DateTime.Parse(o.DateImmat)).ToList();
                break;

            case "DateMec":
                if (sortDir == "desc")
                    ListVehicules = ListVehicules.OrderByDescending(o => DateTime.Parse(o.DateMec)).ToList();
                else
                    ListVehicules = ListVehicules.OrderBy(o => DateTime.Parse(o.DateMec)).ToList();
                break;

            case "Marque":
                if (sortDir == "desc")
                    ListVehicules = ListVehicules.OrderByDescending(o => o.Marque).ToList();
                else
                    ListVehicules = ListVehicules.OrderBy(o => o.Marque).ToList();
                break;

            case "Modele":
                if (sortDir == "desc")
                    ListVehicules = ListVehicules.OrderByDescending(o => o.Modele).ToList();
                else
                    ListVehicules = ListVehicules.OrderBy(o => o.Modele).ToList();
                break;

            case "Nom":
                if (sortDir == "desc")
                    ListVehicules = ListVehicules.OrderByDescending(o => o.Nom).ToList();
                else
                    ListVehicules = ListVehicules.OrderBy(o => o.Nom).ToList();
                break;

            case "Prenom":
                if (sortDir == "desc")
                    ListVehicules = ListVehicules.OrderByDescending(o => o.Prenom).ToList();
                else
                    ListVehicules = ListVehicules.OrderBy(o => o.Prenom).ToList();
                break;
            case "Cin":
                if (sortDir == "desc")
                    ListVehicules = ListVehicules.OrderByDescending(o => o.Cin).ToList();
                else
                    ListVehicules = ListVehicules.OrderBy(o => o.Cin).ToList();
                break;



        }


        if (Session["IndexPageCarteGrise"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageCarteGrise"]);
        }
        else
        {
            Session["IndexPageCarteGrise"] = 1;
            IndexPage = 1;

        }

        Session["ListVehicules"] = ListVehicules.Skip((Convert.ToInt32(Session["IndexPageCarteGrise"]) - 1) * 15).Take(15);

        GridViewCartesGrises.DataSource = ListVehicules.Skip((Convert.ToInt32(Session["IndexPageCarteGrise"]) - 1) * 15).Take(15);
        GridViewCartesGrises.DataBind();
        FooterButtonGridViewCartesGrises();

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

    protected void CarteGrise_Sorting(object sender, GridViewSortEventArgs e)
    {
        BindGridView(e.SortExpression, sortOrder);

        int columnIndex = 0;

        foreach (DataControlFieldHeaderCell headerCell in GridViewCartesGrises.HeaderRow.Cells)
        {
            if (headerCell.ContainingField.SortExpression == e.SortExpression)
            {
                columnIndex = GridViewCartesGrises.HeaderRow.Cells.GetCellIndex(headerCell);
            }
        }

        GridViewCartesGrises.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);

        

    }

    //-------------------------------------------------------------------------------------------
}
