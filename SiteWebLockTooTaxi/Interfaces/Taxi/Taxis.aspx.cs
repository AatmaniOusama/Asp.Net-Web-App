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



public partial class Agrement_index : System.Web.UI.Page
{
    int IndexPage;
    double TotalAgrements;
    double TotalAgrementsByFiltre;


    Agrement agrement = new Agrement();
    TypeTaxi typeTaxi = new TypeTaxi();
    List<Agrement> ListAgrements;

    AgrementTableAdapter AgrementAdapeter = new AgrementTableAdapter();




    protected void Page_Load(object sender, EventArgs e)
    {
        // LaodGridView();

        try
        {
            TotalAgrements = agrement.getTotalAgrements();
            Session["TotalAgrements"] = TotalAgrements;

            TbNom.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbPrenom.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbImmatriculation.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");



            Operateur operateur = (Operateur)Session["Operateur"];

            if (operateur.Profil.ToUpper() == "ADMINISTRATEUR")
            {
                BarreControle.Rows[0].Cells[2].Visible = true;
                BarreControle.Rows[0].Cells[3].Visible = true;
                BarreControle.Rows[0].Cells[4].Visible = true;
                BarreControle.Rows[0].Cells[5].Visible = true;
                BarreControle.Rows[0].Cells[6].Visible = true;

            }

            if (operateur.Profil.ToUpper() == "SUPERVISEUR")
            {
                BarreControle.Rows[0].Cells[2].Visible = true; // Exporter vers Excel
                BarreControle.Rows[0].Cells[3].Visible = false; // Ajouter Agrément
                BarreControle.Rows[0].Cells[4].Visible = false; // Modifier Agrément
                BarreControle.Rows[0].Cells[5].Visible = false; // Supprimer Agrément 
                BarreControle.Rows[0].Cells[6].Visible = true; // Autoriser/ Interdire Agrément

            }

            if (operateur.Profil.ToUpper() == "CONSULTANT")
            {
                BarreControle.Rows[0].Cells[2].Visible = true;
                BarreControle.Rows[0].Cells[3].Visible = false;
                BarreControle.Rows[0].Cells[4].Visible = false;
                BarreControle.Rows[0].Cells[5].Visible = false;
                BarreControle.Rows[0].Cells[6].Visible = false;

            }


            if (operateur.MenusAutorises.Substring(0, 1) == "0")
            {
                Session.Abandon();
                Response.Redirect("~/");
            }



            if (!IsPostBack)
            {

                ViewState["sortOrder"] = "";


                DDLTypeTaxi.DataSource = typeTaxi.getTypeTaxi();
                DDLTypeTaxi.DataValueField = "Num";
                DDLTypeTaxi.DataTextField = "Libelle";
                DDLTypeTaxi.DataBind();


                if (Session["TaxiDateDebut"] as DateTime? != null)
                    TbDateDebut.SelectedDate = Session["TaxiDateDebut"] as DateTime?;
                else
                    TbDateDebut.SelectedDate = null;

                if (Session["TaxiDateFin"] as DateTime? != null)
                    TbDateFin.SelectedDate = Session["TaxiDateFin"] as DateTime?;
                else
                    TbDateFin.SelectedDate = null;


                TbAgrement.Text = Session["TaxiAgrement"] as string;
                TbNom.Text = Session["TaxiNom"] as string;
                DDLTypeTaxi.SelectedValue = Session["TaxiTypeTaxi"] as string;

                if (!string.IsNullOrEmpty(Session["TaxiRbAutoise"] as string))
                    RbAutorise.SelectedValue = Session["TaxiRbAutoise"] as string;


                if (Session["IndexPageAgrement"] != null)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageAgrement"]);

                }
                else
                {
                    IndexPage = 1;
                }


                if (Convert.ToBoolean(Session["SelectAgrementSet"]) == true)
                {
                    LaodGridView(IndexPage);
                    GridViewRow row = Session["AgrementRowselected"] as GridViewRow;
                    GridViewAgrements.SelectRow(row.RowIndex);
                    Session["SelectAgrementSet"] = false;
                }
                else if (Convert.ToBoolean(Session["SelectAgrementAdd"]) == true)
                {
                    LaodGridView(IndexPage);
                    GridViewAgrements.SelectRow(0);
                    Session["SelectAgrementAdd"] = false;
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


    protected void ImgVisualiser_Click(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Visualiser")
        {
            // Get the row selected and its index
            GridViewRow selected = (GridViewRow)((Control)(e.CommandSource)).Parent.Parent;

            // GridViewRow row = GridViewAgrements.SelectedRow;
            string typeTaxi = (selected.FindControl("LblTypeTAxi") as Label).Text;
            int type = agrement.getTypeTaxi(typeTaxi);


            Session["TaxiDetailsFor"] = (selected.FindControl("LblAgrement") as Label).Text;
            Session["TaxiDetailsFor2"] = type;
            Session["TaxiChampsEnabled"] = false;
            Session["TaxiAjout"] = false;
            Session["AgrementRowselected"] = selected;

            Response.Redirect("~/Interfaces/Taxi/DetailTaxi.aspx");

        }
    }

    protected void LaodGridView(int IndexPage)
    {
        // ---------------  Récupérer les Valeurs des Filtres ----------------------------------------------------------------------------------------------------------------------------------------

        DateTime? dateDebut = TbDateDebut.SelectedDate;
        if (TbDateDebut.IsEmpty)
        { dateDebut = null; }

        DateTime? dateFin = TbDateFin.SelectedDate;
        if (TbDateFin.IsEmpty)
        { dateFin = null; }

        string numAgrement = TbAgrement.Text;
        if (numAgrement == "")
        { numAgrement = null; }

        string immatriculation = TbImmatriculation.Text;
        if (immatriculation == "")
        { immatriculation = null; }

        string nom = TbNom.Text;
        if (nom == "")
        { nom = null; }

        string prenom = TbPrenom.Text;
        if (prenom == "")
        { prenom = null; }

        int? ddltypeTaxi = int.Parse(DDLTypeTaxi.SelectedValue);
        if (DDLTypeTaxi.SelectedValue == "-1")
        {
            ddltypeTaxi = null;
        }
        string rbautoriseValue = RbAutorise.SelectedValue;
        if (rbautoriseValue == "TF")
        {
            rbautoriseValue = null;
        }


        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // Verifier si l'un des champs des filtre  remplis ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        if (!TbDateDebut.IsEmpty || !TbDateFin.IsEmpty || TbAgrement.Text != "" || TbImmatriculation.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || DDLTypeTaxi.SelectedValue != "-1" || RbAutorise.SelectedValue != "TF")
        {
            TotalAgrementsByFiltre = agrement.getTotalAgrementsByFiltre(dateDebut, dateFin, numAgrement, immatriculation, nom, prenom, ddltypeTaxi, rbautoriseValue);  // getTotalAgrementsByFiltre : donne le nombre des Agréments en tenant compte des filtres
            Session["TotalAgrementsByFiltre"] = TotalAgrementsByFiltre;
            NbrLignes.Text = TotalAgrementsByFiltre.ToString();

        }

        else
        {
            NbrLignes.Text = TotalAgrements.ToString();
        }


        // Fin Vérification -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        ListAgrements = agrement.getAllAgrements(IndexPage, dateDebut, dateFin, numAgrement, immatriculation, nom, prenom, ddltypeTaxi, rbautoriseValue);

        Session["ListAgrements"] = ListAgrements;
        GridViewAgrements.DataSource = ListAgrements;
        GridViewAgrements.DataBind();

        BtInterdire.Visible = false;
        BtAutoriser.Visible = false;
        BtnSet.Visible = false;
        BtnDelete.Visible = false;


        GridViewAgrements.SelectedIndex = -1;


        FooterButtonGridViewAgrements();



    }

    protected void FooterButtonGridViewAgrements()
    {
        // Gérer l'affichage des bouttons Next previous... dans le footer -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        if ((ListAgrements.Count() < 15 && TotalAgrements < 15) || (ListAgrements.Count() < 15 && Convert.ToInt32(Session["TotalAgrementsByFiltre"]) < 15 && Convert.ToInt32(Session["TotalAgrementsByFiltre"]) > 0) || TotalAgrements == 15 || (Convert.ToInt32(Session["TotalAgrementsByFiltre"]) == 15 && Convert.ToInt32(Session["TotalAgrementsByFiltre"]) != 0))
        {
            GridViewAgrements.FooterRow.Cells[6].Enabled = false;
            GridViewAgrements.FooterRow.Cells[5].Enabled = false;


        }


        if (!TbDateDebut.IsEmpty || !TbDateFin.IsEmpty || TbAgrement.Text != "" || TbImmatriculation.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || DDLTypeTaxi.SelectedValue != "-1" || RbAutorise.SelectedValue != "TF")
        {

            if (IndexPage == 1 && Convert.ToInt32(Session["TotalAgrementsByFiltre"]) != 0)
            {
                GridViewAgrements.FooterRow.Cells[5].Enabled = false;
            }
            else
            {

                TotalAgrementsByFiltre = Convert.ToInt32(Session["TotalAgrementsByFiltre"]);
                int T = (int)TotalAgrementsByFiltre / 15;
                if (T < TotalAgrementsByFiltre / 15)
                {
                    if (IndexPage == T + 1)
                    {
                        GridViewAgrements.FooterRow.Cells[6].Enabled = false;

                    }

                }
                else
                {
                    if (IndexPage == T)
                    {
                        GridViewAgrements.FooterRow.Cells[6].Enabled = false;


                    }
                }
            }

        }
        else
        {
            if (IndexPage == 1 && TotalAgrements != 0)
            {
                GridViewAgrements.FooterRow.Cells[5].Enabled = false;
            }
            else
            {


                int TA = (int)TotalAgrements / 15;
                if (TA < TotalAgrements / 15)
                {
                    if (IndexPage == TA + 1)
                    {
                        GridViewAgrements.FooterRow.Cells[6].Enabled = false;


                    }

                }
                else
                {
                    if (IndexPage == TA)
                    {
                        GridViewAgrements.FooterRow.Cells[6].Enabled = false;


                    }
                }
            }
        }
        // Fin -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    }

    protected void Filtre(object sender, EventArgs e)
    {
        Session["TaxiDateDebut"] = TbDateDebut.SelectedDate;
        Session["TaxiDateFin"] = TbDateFin.SelectedDate;

        Session["TaxiAgrement"] = TbAgrement.Text;
        Session["TaxiImmatriculation"] = TbImmatriculation.Text;
        Session["TaxiNom"] = TbNom.Text;
        Session["TaxiPrenom"] = TbPrenom.Text;
        Session["TaxiTypeTaxi"] = DDLTypeTaxi.SelectedValue;
        Session["TaxiRbAutoise"] = RbAutorise.SelectedValue;

        if (!TbDateDebut.IsEmpty || !TbDateFin.IsEmpty || TbAgrement.Text != "" || TbImmatriculation.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || DDLTypeTaxi.SelectedValue != "-1" || RbAutorise.SelectedValue != "TF")
        {
            Session["IndexPageAgrement"] = 1;
            IndexPage = 1;
        }
        else
        {
            if (Session["IndexPageAgrement"] != null)
            {
                IndexPage = Convert.ToInt32(Session["IndexPageAgrement"]);
            }
            else
            {
                Session["IndexPageAgrement"] = 1;
                IndexPage = 1;

            }
        }


        LaodGridView(IndexPage);

    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {

        Session["TaxiChampsEnabled"] = true;
        Session["TaxiAjout"] = true;
        Session["TaxiDetailsFor"] = "";
        Response.Redirect("~/Interfaces/Taxi/DetailTaxi.aspx");

    }

    protected void BtnSet_Click(object sender, EventArgs e)
    {
        GridViewRow row = GridViewAgrements.SelectedRow;
        string typeTaxi = (row.FindControl("LblTypeTAxi") as Label).Text;
        int type = agrement.getTypeTaxi(typeTaxi);


        Session["TaxiDetailsFor"] = (row.FindControl("LblAgrement") as Label).Text;
        Session["TaxiDetailsFor2"] = type;
        Session["TaxiChampsEnabled"] = true;
        Session["TaxiAjout"] = false;
        Session["AgrementRowselected"] = row;

        Response.Redirect("~/Interfaces/Taxi/DetailTaxi.aspx");
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        GridViewRow row = GridViewAgrements.SelectedRow;

        string numAgrement = (row.FindControl("LblAgrement") as Label).Text;
        string typeTaxi = (row.FindControl("LblTypeTAxi") as Label).Text;
        int type = agrement.getTypeTaxi(typeTaxi);

        int i = 0;
        i = agrement.SupprimerUnAgrement(numAgrement, type);

        if (i == 1)
        {
            Response.Redirect("~/Interfaces/Taxi/Taxis.aspx");
        }





    }



    protected void GridViewAgrements_IndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewAgrements.PageIndex = e.NewPageIndex;
        GridViewAgrements.DataSource = Session["ListAgrements"] as List<Agrement>;
        GridViewAgrements.DataBind();

    }

    protected void CbFiltre_CheckedChanged(object sender, EventArgs e)
    {
        Panelfilter.Visible = CbFiltre.Checked;

    }

    protected void GridViewAgrements_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow row = GridViewAgrements.Rows[e.NewSelectedIndex];
        string Valide = (row.FindControl("Selected") as Label).Text;

        BtnDelete.Visible = true;
        BtnSet.Visible = true;
        BtAutoriser.Visible = (Valide != "True");
        BtInterdire.Visible = (Valide == "True");


    }

    protected void GridViewAgrements_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Add onclick attribute to select row.
            e.Row.Attributes.Add("onclick", String.Format("javascript:__doPostBack('ctl00$MainContent$GridViewAgrements','Select${0}')", e.Row.RowIndex));


            if (Convert.ToInt32(Session["IndexPageAgrement"]) == 0)
            {
                Session["IndexPageAgrement"] = 1;
                IndexPage = Convert.ToInt32(Session["IndexPageAgrement"]);
            }

            if ((e.Row.Cells[1].FindControl("Selected") as Label).Text == "True")
            {
                e.Row.Cells[0].FindControl("ImgInvalide").Visible = false;
                e.Row.Cells[0].FindControl("ImgValide").Visible = true;
            }
            if ((e.Row.Cells[1].FindControl("Selected") as Label).Text == "False")
            {
                e.Row.Cells[0].FindControl("ImgInvalide").Visible = true;
                e.Row.Cells[0].FindControl("ImgValide").Visible = false;
            }

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            if (!TbDateDebut.IsEmpty || !TbDateFin.IsEmpty || TbAgrement.Text != "" || TbImmatriculation.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || DDLTypeTaxi.SelectedValue != "-1" || RbAutorise.SelectedValue != "TF")
            {
                int Q = Convert.ToInt32(Session["TotalAgrementsByFiltre"]) / 15;
                int R = Convert.ToInt32(Session["TotalAgrementsByFiltre"]) % 15;

                if (R > 0)
                {
                    Q = Q + 1;
                    e.Row.Cells[9].Text = "Page : " + Convert.ToInt32(Session["IndexPageAgrement"]) + " / " + Q + "";
                }
                else
                {
                    e.Row.Cells[9].Text = "Page : " + Convert.ToInt32(Session["IndexPageAgrement"]) + " / " + Q + "";
                }
            }
            else
            {
                int Q = Convert.ToInt32(Session["TotalAgrements"]) / 15;
                int R = Convert.ToInt32(Session["TotalAgrements"]) % 15;

                if (R > 0)
                {
                    Q = Q + 1;
                    e.Row.Cells[9].Text = "Page : " + Convert.ToInt32(Session["IndexPageAgrement"]) + " / " + Q + "";
                }
                else
                {
                    e.Row.Cells[9].Text = "Page : " + Convert.ToInt32(Session["IndexPageAgrement"]) + " / " + Q + "";
                }

            }

        }

    }

    protected void BtAutoriser_Click(object sender, EventArgs e)
    {
        GridViewRow row = GridViewAgrements.Rows[GridViewAgrements.SelectedIndex];
        Agrement agrement = new Agrement();

        string numAgrement = (row.FindControl("LblAgrement") as Label).Text;
        string typeTaxi = (row.FindControl("LblTypeTAxi") as Label).Text;
        int type = agrement.getTypeTaxi(typeTaxi);
        int i = agrement.ModifierValide(true, numAgrement, type);

        if (i == 1)
        {
            if (Session["IndexPageAgrement"] != null)
            {
                IndexPage = Convert.ToInt32(Session["IndexPageAgrement"]);
            }
            else
            {
                IndexPage = 1;

            }

            LaodGridView(IndexPage);

            if (!TbDateDebut.IsEmpty || !TbDateFin.IsEmpty || TbAgrement.Text != "" || TbImmatriculation.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || DDLTypeTaxi.SelectedValue != "-1" || RbAutorise.SelectedValue != "TF")
            {

                int TotalRows = GridViewAgrements.Rows.Count;

                if (TotalAgrementsByFiltre > 0)
                {
                    if (TotalRows >= 1)
                    {
                        if (row.RowIndex == TotalRows)
                        {
                            GridViewAgrements.SelectRow(row.RowIndex - 1);
                        }
                        else if ((row.RowIndex < TotalRows) && (row.RowIndex >= 0))
                        {
                            GridViewAgrements.SelectRow(row.RowIndex);
                        }

                    }
                    if (TotalRows == 0)
                    {
                        IndexPage = Convert.ToInt32(Session["IndexPageAgrement"]) - 1;
                        Session["IndexPageAgrement"] = IndexPage;
                        LaodGridView(IndexPage);
                        GridViewAgrements.SelectRow(GridViewAgrements.Rows.Count - 1);
                    }
                }

                else if (TotalAgrementsByFiltre == 0)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageAgrement"]);
                    Session["IndexPageAgrement"] = IndexPage;
                    LaodGridView(IndexPage);
                }
            }
            else
            {
                int TotalRows = GridViewAgrements.Rows.Count;
                if (TotalAgrements > 0)
                {
                    if (TotalRows >= 1)
                    {
                        if (row.RowIndex == TotalRows)
                        {
                            GridViewAgrements.SelectRow(row.RowIndex - 1);
                        }
                        else if ((row.RowIndex < TotalRows) && (row.RowIndex >= 0))
                        {
                            GridViewAgrements.SelectRow(row.RowIndex);
                        }
                    }
                    if (TotalRows == 0)
                    {
                        IndexPage = Convert.ToInt32(Session["IndexPageAgrement"]) - 1;
                        Session["IndexPageAgrement"] = IndexPage;
                        LaodGridView(IndexPage);
                        GridViewAgrements.SelectRow(GridViewAgrements.Rows.Count - 1);
                    }
                }

                else if (TotalAgrements == 0)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageAgrement"]);
                    Session["IndexPageAgrement"] = IndexPage;
                    LaodGridView(IndexPage);
                }
            }
        }


    }

    protected void BtInterdir_Click(object sender, EventArgs e)
    {
        GridViewRow row = GridViewAgrements.Rows[GridViewAgrements.SelectedIndex];
        Agrement agrement = new Agrement();

        string numAgrement = (row.FindControl("LblAgrement") as Label).Text;
        string typeTaxi = (row.FindControl("LblTypeTAxi") as Label).Text;


        int type = agrement.getTypeTaxi(typeTaxi);
        int i = agrement.ModifierValide(false, numAgrement, type);

        if (i == 1)
        {
            if (Session["IndexPageAgrement"] != null)
            {
                IndexPage = Convert.ToInt32(Session["IndexPageAgrement"]);
            }
            else
            {
                IndexPage = 1;

            }
            LaodGridView(IndexPage);

            if (!TbDateDebut.IsEmpty || !TbDateFin.IsEmpty || TbAgrement.Text != "" || TbImmatriculation.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || DDLTypeTaxi.SelectedValue != "-1" || RbAutorise.SelectedValue != "TF")
            {

                int TotalRows = GridViewAgrements.Rows.Count;

                if (TotalAgrementsByFiltre > 0)
                {
                    if (TotalRows >= 1)
                    {
                        if (row.RowIndex == TotalRows)
                        {
                            GridViewAgrements.SelectRow(row.RowIndex - 1);
                        }
                        else if ((row.RowIndex < TotalRows) && (row.RowIndex >= 0))
                        {
                            GridViewAgrements.SelectRow(row.RowIndex);
                        }

                    }
                    if (TotalRows == 0)
                    {
                        IndexPage = Convert.ToInt32(Session["IndexPageAgrement"]) - 1;
                        Session["IndexPageAgrement"] = IndexPage;
                        LaodGridView(IndexPage);
                        GridViewAgrements.SelectRow(GridViewAgrements.Rows.Count - 1);
                    }
                }

                else if (TotalAgrementsByFiltre == 0)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageAgrement"]);
                    Session["IndexPageAgrement"] = IndexPage;
                    LaodGridView(IndexPage);
                }
            }

            else
            {
                int TotalRows = GridViewAgrements.Rows.Count;
                if (TotalAgrements > 0)
                {
                    if (TotalRows >= 1)
                    {
                        if (row.RowIndex == TotalRows)
                        {
                            GridViewAgrements.SelectRow(row.RowIndex - 1);
                        }
                        else if ((row.RowIndex < TotalRows) && (row.RowIndex >= 0))
                        {
                            GridViewAgrements.SelectRow(row.RowIndex);
                        }

                    }
                    if (TotalRows == 0)
                    {
                        IndexPage = Convert.ToInt32(Session["IndexPageAgrement"]) - 1;
                        Session["IndexPageAgrement"] = IndexPage;
                        LaodGridView(IndexPage);
                        GridViewAgrements.SelectRow(GridViewAgrements.Rows.Count - 1);
                    }
                }

                else if (TotalAgrements == 0)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageAgrement"]);
                    Session["IndexPageAgrement"] = IndexPage;
                    LaodGridView(IndexPage);
                }
            }
        }


    }

    protected void BtnLast_Click(object sender, EventArgs e)
    {
        if (!TbDateDebut.IsEmpty || !TbDateFin.IsEmpty || TbAgrement.Text != "" || TbImmatriculation.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || DDLTypeTaxi.SelectedValue != "-1" || RbAutorise.SelectedValue != "TF")
        {


            TotalAgrementsByFiltre = Convert.ToInt32(Session["TotalAgrementsByFiltre"]);
            int T = (int)TotalAgrementsByFiltre / 15;
            if (T < TotalAgrementsByFiltre / 15)
            {
                Session["IndexPageAgrement"] = ((int)TotalAgrementsByFiltre / 15) + 1;
            }
            else
            {
                Session["IndexPageAgrement"] = ((int)TotalAgrementsByFiltre / 15);
            }
        }
        else
        {
            TotalAgrements = Convert.ToInt32(Session["TotalAgrements"]);
            int T = (int)TotalAgrements / 15;
            if (T < TotalAgrements / 15)
            {
                Session["IndexPageAgrement"] = ((int)TotalAgrements / 15) + 1;
            }
            else
            {
                Session["IndexPageAgrement"] = ((int)TotalAgrements / 15);
            }


        }


        IndexPage = Convert.ToInt32(Session["IndexPageAgrement"]);
        LaodGridView(IndexPage);
    }

    protected void BtnNext_Click(object sender, EventArgs e)
    {



        if (Session["IndexPageAgrement"] != null)
        {


            Session["IndexPageAgrement"] = Convert.ToInt32(Session["IndexPageAgrement"]) + 1;
            IndexPage = Convert.ToInt32(Session["IndexPageAgrement"]);


        }


        else
        {
            Session["IndexPageAgrement"] = 2;
            IndexPage = 2;
        }

        LaodGridView(IndexPage);


    }

    protected void BtnPrevious_Click(object sender, EventArgs e)
    {

        Session["IndexPageAgrement"] = Convert.ToInt32(Session["IndexPageAgrement"]) - 1;
        IndexPage = Convert.ToInt32(Session["IndexPageAgrement"]);
        LaodGridView(IndexPage);

    }

    protected void BtnFirst_Click(object sender, EventArgs e)
    {
        Session["IndexPageAgrement"] = 1;
        IndexPage = Convert.ToInt32(Session["IndexPageAgrement"]);
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

        GridViewAgrements.AllowPaging = false;
        GridViewAgrements.AllowSorting = false;
        GridViewAgrements.ShowFooter = false;

        GridViewAgrements.EditIndex = -1;

        GridViewAgrements.Columns[0].Visible = false;
        GridViewAgrements.Columns[1].Visible = false;




        GridViewAgrements.DataSource = Session["ListAgrements"] as List<Agrement>;
        GridViewAgrements.DataBind();






        PrepareGridViewForExport(GridViewAgrements);

        mypage.Controls.Add(form);
        form.Controls.Add(GridViewAgrements);

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
        // Tester s'il y un filtre ou pas 

        if (!TbDateDebut.IsEmpty || !TbDateFin.IsEmpty || TbAgrement.Text != "" || TbImmatriculation.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || DDLTypeTaxi.SelectedValue != "-1" || RbAutorise.SelectedValue != "TF")
        {
            DateTime? dateDebut = TbDateDebut.SelectedDate;
            if (TbDateDebut.IsEmpty)
            { dateDebut = null; }

            DateTime? dateFin = TbDateFin.SelectedDate;
            if (TbDateFin.IsEmpty)
            { dateFin = null; }

            string numAgrement = TbAgrement.Text;
            if (numAgrement == "")
            { numAgrement = null; }

            string immatriculation = TbImmatriculation.Text;
            if (immatriculation == "")
            { immatriculation = null; }

            string nom = TbNom.Text;
            if (nom == "")
            { nom = null; }

            string prenom = TbPrenom.Text;
            if (prenom == "")
            { prenom = null; }

            int? ddltypeTaxi = int.Parse(DDLTypeTaxi.SelectedValue);
            if (DDLTypeTaxi.SelectedValue == "-1")
            {
                ddltypeTaxi = null;
            }
            string rbautoriseValue = RbAutorise.SelectedValue;
            if (rbautoriseValue == "TF")
            {
                rbautoriseValue = null;
            }

            ListAgrements = agrement.getAllAgrementsByFiltre(dateDebut, dateFin, numAgrement, immatriculation, nom, prenom, ddltypeTaxi, rbautoriseValue);

        }
        else
        {
            ListAgrements = agrement.getAgrements();
        }


        switch (sortExp)
        {
            case "Agrement":
                if (sortDir == "desc")
                    ListAgrements = ListAgrements.OrderByDescending(o => int.Parse(o.NumAgrement)).ToList();
                else
                    ListAgrements = ListAgrements.OrderBy(o => int.Parse(o.NumAgrement)).ToList();
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

            case "DateDebut":
                if (sortDir == "desc")
                    ListAgrements = ListAgrements.OrderByDescending(o => DateTime.Parse(o.DateDebut)).ToList();
                else
                    ListAgrements = ListAgrements.OrderBy(o => DateTime.Parse(o.DateDebut)).ToList();
                break;

            case "DateFin":
                if (sortDir == "desc")
                    ListAgrements = ListAgrements.OrderByDescending(o => DateTime.Parse(o.DateFin)).ToList();
                else
                    ListAgrements = ListAgrements.OrderBy(o => DateTime.Parse(o.DateFin)).ToList();
                break;

            case "Nom":
                if (sortDir == "desc")
                    ListAgrements = ListAgrements.OrderByDescending(o => o.Nom).ToList();
                else
                    ListAgrements = ListAgrements.OrderBy(o => o.Nom).ToList();
                break;

            case "Prenom":
                if (sortDir == "desc")
                    ListAgrements = ListAgrements.OrderByDescending(o => o.Prenom).ToList();
                else
                    ListAgrements = ListAgrements.OrderBy(o => o.Prenom).ToList();
                break;
            case "Commune":
                if (sortDir == "desc")
                    ListAgrements = ListAgrements.OrderByDescending(o => o.Commune).ToList();
                else
                    ListAgrements = ListAgrements.OrderBy(o => o.Commune).ToList();
                break;



        }





        if (Session["IndexPageAgrement"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageAgrement"]);
        }
        else
        {
            Session["IndexPageAgrement"] = 1;
            IndexPage = 1;

        }



        Session["ListAgrements"] = ListAgrements.Skip((Convert.ToInt32(Session["IndexPageAgrement"]) - 1) * 15).Take(15);

        GridViewAgrements.DataSource = ListAgrements.Skip((Convert.ToInt32(Session["IndexPageAgrement"]) - 1) * 15).Take(15);
        GridViewAgrements.DataBind();
        FooterButtonGridViewAgrements();
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

    protected void Agrements_Sorting(object sender, GridViewSortEventArgs e)
    {



        BindGridView(e.SortExpression, sortOrder);

        int columnIndex = 0;

        foreach (DataControlFieldHeaderCell headerCell in GridViewAgrements.HeaderRow.Cells)
        {
            if (headerCell.ContainingField.SortExpression == e.SortExpression)
            {
                columnIndex = GridViewAgrements.HeaderRow.Cells.GetCellIndex(headerCell);
            }
        }

        GridViewAgrements.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);


    }

    //-------------------------------------------------------------------------------------------

}
