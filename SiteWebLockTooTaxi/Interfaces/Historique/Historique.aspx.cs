using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using DataLockTooTaxi;
using System.Web.UI.HtmlControls;

public partial class Interfaces_Historique_Historique : System.Web.UI.Page
{
    int IndexPage;
    double TotalHistorique;
    double TotalHistoriqueByFiltre;
     

    Events events = new Events();
    TypeTaxi typeTaxi = new TypeTaxi();
    Lecteur lecteur = new Lecteur();

    List<Events> ListEvents;



    protected void Page_Load(object sender, EventArgs e)
    {
     
        
        try
        {
            //RbTypeCodeRefus.Attributes.Add("selected", "True");
         
            TotalHistorique = events.getTotalEvents();
            Session["TotalHistorique"] = TotalHistorique;

            TbMatriculeFiltre.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbNomFiltre.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbImmatriculation.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");



            Operateur operateur = (Operateur)Session["Operateur"];

            if (operateur.Profil.ToUpper() == "ADMINISTRATEUR")
            {
                BarreControle.Rows[0].Cells[2].Visible = true;
                BarreControle.Rows[0].Cells[3].Visible = true;
                BarreControle.Rows[0].Cells[4].Visible = true;
            }


            if (operateur.Profil.ToUpper() == "SUPERVISEUR")
            {
                BarreControle.Rows[0].Cells[2].Visible = true;
                BarreControle.Rows[0].Cells[3].Visible = true;
                BarreControle.Rows[0].Cells[4].Visible = true;

            }

            if (operateur.Profil.ToUpper() == "CONSULTANT")
            {
                BarreControle.Rows[0].Cells[2].Visible = true;
                BarreControle.Rows[0].Cells[3].Visible = false;
                BarreControle.Rows[0].Cells[4].Visible = false;

            }


            Session["HistoriqueTypeCodeRefus"] = "Tous";

                if (!IsPostBack)
                {

                    ViewState["sortOrder"] = "";


                    DDLLecteur.DataSource = lecteur.GetLecteurs();
                    DDLLecteur.DataTextField = "NomLecteur";
                    DDLLecteur.DataValueField = "Adresse";
                    DDLLecteur.DataBind();

                    DDLTypeTaxi.DataSource = typeTaxi.getTypeTaxi();
                    DDLTypeTaxi.DataTextField = "Libelle";
                    DDLTypeTaxi.DataValueField = "Num";
                    DDLTypeTaxi.DataBind();


                    if (Session["HistoriqueDateDebut"] as DateTime? != null)
                        TbDateDebut.SelectedDate = Session["HistoriqueDateDebut"] as DateTime?;
                    else
                        TbDateDebut.SelectedDate = DateTime.Parse(DateTime.Now.ToString().Substring(0, 10) + " 00:00:00");

                    if (Session["HistoriqueDateFin"] as DateTime? != null)
                        TbDateFin.SelectedDate = Session["HistoriqueDateFin"] as DateTime?;
                    else
                        TbDateFin.SelectedDate = DateTime.Parse(DateTime.Now.ToString().Substring(0, 10) + " 23:59:59");


                    DDLLecteur.SelectedValue = Session["HistoriqueLecteur"] as string;
                    TbMatriculeFiltre.Text = Session["HistoriqueMatriculeFiltre"] as string;
                    TbNPermis.Text = Session["HistoriqueNPermis"] as string;
                    TbNTaxi.Text = Session["HistoriqueNTaxi"] as string;
                    TbCodeRefus.Text = Session["HistoriqueCodeRefus"] as string;
                    RbTypeCodeRefus.SelectedValue = Session["HistoriqueTypeCodeRefus"] as string;

                    DDLModePointage.SelectedValue = Session["HistoriqueModePointage"] as string;
                    TbNomFiltre.Text = Session["HistoriqueNomFiltre"] as string;
                    TbImmatriculation.Text = Session["HistoriqueImmatriculation"] as string;
                    DDLTypeTaxi.SelectedValue = Session["HistoriqueTypeTaxi"] as string;
                    TbMatriculeAgent.Text = Session["HistoriqueMatriculeAgent"] as string;

                    if (Session["IndexPageHistorique"] != null)
                    {
                        IndexPage = Convert.ToInt32(Session["IndexPageHistorique"]);

                    }
                    else
                    {
                        Session["IndexPageHistorique"] = 1;
                        IndexPage = 1;
                    }


                    GridView_Load(IndexPage);
                


            }
        }
        catch (Exception)
        { 
        }


    }

    protected void GridView_Load(int IndexPage)
    {
        
        Events events = new Events();
        
        // ---------------  Récupérer les Valeurs des Filtres ----------------------------------------------------------------------------------------------------------------------------------------

        DateTime debut = TbDateDebut.SelectedDate.Value;
        DateTime fin = TbDateFin.SelectedDate.Value;

        string matricule = TbMatriculeFiltre.Text;
        if (matricule == "")
        { matricule = null; }
             
        string nom = TbNomFiltre.Text;
        if (nom == "")
        { nom = null; }

        string numPermis = TbNPermis.Text;
        if (numPermis == "")
        { numPermis = null; }

        string immatriculation = TbImmatriculation.Text;
        if (immatriculation == "")
        { immatriculation = null; }


        string numTaxi = TbNTaxi.Text;
        if (numTaxi == "")
        { numTaxi = null; }
        
        string codeRefus = TbCodeRefus.Text;
        if (codeRefus == "")
        { codeRefus = null; }

        string matriculeAgent = TbMatriculeAgent.Text;
        if (matriculeAgent == "")
        { matriculeAgent = null; }



        int? typeCode = int.Parse(RbTypeCodeRefus.SelectedItem.Value);
     
        if (typeCode == -1)
        {
            typeCode = null;
        }
        int? numLecteur = int.Parse(DDLLecteur.SelectedValue);
        if (numLecteur == -1)
        {
            numLecteur = null;
        }

        int? sous_Type = int.Parse(DDLModePointage.SelectedValue);
        if (sous_Type == -1)
        {
            sous_Type = null;
        }
        int? typeTaxi = int.Parse(DDLTypeTaxi.SelectedValue);
        if (typeTaxi == -1)
        {
            typeTaxi = null;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        // Verifier si l'un des champs des filtre  remplis ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        if (!TbDateDebut.IsEmpty || !TbDateFin.IsEmpty || TbMatriculeFiltre.Text != "" || TbNomFiltre.Text != "" || TbNPermis.Text != "" || TbImmatriculation.Text != "" || TbNTaxi.Text != "" || DDLTypeTaxi.SelectedValue !="-1" ||TbCodeRefus.Text != "" || TbMatriculeAgent.Text != "" || RbTypeCodeRefus.SelectedItem.Value !="-1" || DDLModePointage.SelectedValue !="-1" || DDLLecteur.SelectedValue != "-1")
        {

            TotalHistoriqueByFiltre = events.getTotalEventsByFiltre(debut, fin, matricule, nom, numPermis, immatriculation, numTaxi,typeTaxi, codeRefus, matriculeAgent,typeCode,sous_Type, numLecteur);
            Session["TotalHistoriqueByFiltre"] = TotalHistoriqueByFiltre;
            TbNbrLignes.Text = TotalHistoriqueByFiltre.ToString();      
           
        }

        else
        {
            TbNbrLignes.Text = TotalHistorique.ToString();
        }


        // Fin Vérification -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ListEvents = events.getEventsByFiltre(IndexPage, debut, fin, matricule, nom, numPermis, immatriculation, numTaxi,typeTaxi,codeRefus, matriculeAgent,typeCode,sous_Type,numLecteur);


       


        Session["ListEvents"] = ListEvents;



        GridViewEvents.DataSource = ListEvents;
        GridViewEvents.DataBind();

        GridViewEvents.SelectedIndex = -1;




        FooterButtonGridViewEvents();
        FailureText.Text = " ";

        if (GridViewEvents.Rows.Count > 0)
        {
            GridViewEvents.SelectedIndex = 0;

            GridViewRow row = GridViewEvents.Rows[0];


            TbNom.Text = (row.FindControl("Nom") as Label).Text;
            TbPrenom.Text = (row.FindControl("Prenom") as Label).Text;
            TbMatricule.Text = (row.FindControl("Reference") as Label).Text;
            TbNumPermis.Text = (row.FindControl("NumBadge") as Label).Text;
            photo.ImageUrl = "~/Photo.ashx/?Id=" + (row.FindControl("Reference") as Label).Text;

        }
        else
        {
            TbNom.Text = "";
            TbPrenom.Text = "";
            TbMatricule.Text = "";
            photo.ImageUrl = "~/icons/Inconnu.jpg";
        }



    }

    protected void FooterButtonGridViewEvents()
    {
        // Gérer l'affichage des bouttons Next previous... dans le footer -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

   
      if (((ListEvents.Count() < 15 && TotalHistorique < 15) || (ListEvents.Count() < 15 && Convert.ToInt32(Session["TotalHistoriqueByFiltre"]) < 15) || TotalHistorique == 15 || Convert.ToInt32(Session["TotalHistoriqueByFiltre"]) == 15) && Convert.ToInt32(Session["TotalHistoriqueByFiltre"]) != 0)
        {
            GridViewEvents.FooterRow.Cells[6].Enabled = false;
            GridViewEvents.FooterRow.Cells[5].Enabled = false;

        }

      if (!TbDateDebut.IsEmpty || !TbDateFin.IsEmpty || TbMatriculeFiltre.Text != "" || TbNomFiltre.Text != "" || TbNPermis.Text != "" || TbImmatriculation.Text != "" || TbNTaxi.Text != "" || DDLTypeTaxi.SelectedValue != "-1" || TbCodeRefus.Text != "" || TbMatriculeAgent.Text != "" || RbTypeCodeRefus.SelectedItem.Value != "-1" || DDLModePointage.SelectedValue != "-1" || DDLLecteur.SelectedValue != "-1")
      {


            if (IndexPage == 1 && Convert.ToInt32(Session["TotalHistoriqueByFiltre"]) != 0)
            {
                GridViewEvents.FooterRow.Cells[5].Enabled = false;
            }

            else
            {

                TotalHistoriqueByFiltre = Convert.ToInt32(Session["TotalHistoriqueByFiltre"]);
                int T = (int)TotalHistoriqueByFiltre / 15;

                if (T < TotalHistoriqueByFiltre / 15)
                {
                    if (IndexPage == T + 1)
                    {
                        GridViewEvents.FooterRow.Cells[6].Enabled = false;

                    }

                }
                else
                {
                    if (IndexPage == T)
                    {
                        GridViewEvents.FooterRow.Cells[6].Enabled = false;

                    }

                }
            }
        }


        else
        {

            if (IndexPage == 1 && TotalHistorique != 0)
            {
                GridViewEvents.FooterRow.Cells[5].Enabled = false;
            }
            else
            {


                int TU = (int)TotalHistorique / 15;

                if (TU < TotalHistorique / 15)
                {
                    if (IndexPage == TU + 1)
                    {
                        GridViewEvents.FooterRow.Cells[6].Enabled = false;


                    }

                }
                else
                {
                    if (IndexPage == TU)
                    {
                        GridViewEvents.FooterRow.Cells[6].Enabled = false;


                    }
                }
            }

        }

        // Fin -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
      
    
    }

    protected void Filtre(object sender, EventArgs e)
    {
        if (Page.IsValid)

        Session["HistoriqueDateDebut"] = TbDateDebut.SelectedDate.Value;
        Session["HistoriqueDateFin"] = TbDateFin.SelectedDate.Value;
        Session["HistoriqueLecteur"] = DDLLecteur.SelectedValue;
        Session["HistoriqueMatriculeFiltre"] = TbMatriculeFiltre.Text;
        Session["HistoriqueNPermis"] = TbNPermis.Text;
        Session["HistoriqueNTaxi"] = TbNTaxi.Text;
        Session["HistoriqueCodeRefus"] = TbCodeRefus.Text;
        Session["HistoriqueTypeCodeRefus"] = RbTypeCodeRefus.SelectedValue;
        Session["HistoriqueDateFin"] = TbDateFin.SelectedDate.Value;
        Session["HistoriqueModePointage"] = DDLModePointage.SelectedValue;
        Session["HistoriqueNomFiltre"] = TbNomFiltre.Text;
        Session["HistoriqueImmatriculation"] = TbImmatriculation.Text;
        Session["HistoriqueTypeTaxi"] = DDLTypeTaxi.SelectedValue;
        Session["HistoriqueMatriculeAgent"] = TbMatriculeAgent.Text;

        if (!TbDateDebut.IsEmpty || !TbDateFin.IsEmpty || TbMatriculeFiltre.Text != "" || TbNomFiltre.Text != "" || TbNPermis.Text != "" || TbImmatriculation.Text != "" || TbNTaxi.Text != "" || DDLTypeTaxi.SelectedValue != "-1" || TbCodeRefus.Text != "" || TbMatriculeAgent.Text != "" || RbTypeCodeRefus.SelectedItem.Value != "-1" || DDLModePointage.SelectedValue != "-1" || DDLLecteur.SelectedValue != "-1")
        {
            Session["IndexPageHistorique"] = 1;
            IndexPage = 1;

        }

        if (Session["IndexPageHistorique"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageHistorique"]);

        }
        else
        {

            Session["IndexPageHistorique"] = 1;
            IndexPage = 1;

        }

        GridView_Load(IndexPage);
    }

    protected void GridViewEvents_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
   
    protected void GridViewEvents_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Visualiser")
        {
            // Get the row selected and its index
            GridViewRow selected = (GridViewRow)((Control)(e.CommandSource)).Parent.Parent;

            // int index = selected.RowIndex;
            // Session["RowIndex"] = index;

            Session["DetailsFor"] = (selected.FindControl("Reference") as Label).Text;
            Session["ChampsEnabled"] = false;
            Session["Ajout"] = false;

            Response.Redirect("~/Interfaces/User/Detail.aspx");
        }
    }
    
    protected void GridViewEvents_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow row = GridViewEvents.Rows[e.NewSelectedIndex];

         TbNom.Text = (row.FindControl("Nom") as Label).Text;
        TbPrenom.Text = (row.FindControl("Prenom") as Label).Text;
        TbMatricule.Text = (row.FindControl("Reference") as Label).Text;
        photo.ImageUrl = "~/Photo.ashx/?Id=" + (row.FindControl("Reference") as Label).Text;

        //if ((row.FindControl("ModePointage") as Label).Text == "Saisi")
        //{
        //    BtnDelete.Visible = true;
        //}
        BtnDelete.Visible = true;
       

    }
   
    protected void GridViewEvents_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
            e.Row.Attributes.Add("onclick", String.Format("javascript:__doPostBack('ctl00$MainContent$GridViewEvents','Select${0}')", e.Row.RowIndex));
            
            if (Convert.ToInt32(Session["IndexPageHistorique"]) == 0)
            {
                Session["IndexPageHistorique"] = 1;
                IndexPage = Convert.ToInt32(Session["IndexPageHistorique"]);
            }


        }


        if (e.Row.RowType == DataControlRowType.Footer)
        {
            if (!TbDateDebut.IsEmpty || !TbDateFin.IsEmpty || TbMatriculeFiltre.Text != "" || TbNomFiltre.Text != "" || TbNPermis.Text != "" || TbImmatriculation.Text != "" || TbNTaxi.Text != "" || DDLTypeTaxi.SelectedValue != "-1" || TbCodeRefus.Text != "" || TbMatriculeAgent.Text != "" || RbTypeCodeRefus.SelectedItem.Value != "-1" || DDLModePointage.SelectedValue != "-1" || DDLLecteur.SelectedValue != "-1")
            {
                int Q = Convert.ToInt32(Session["TotalHistoriqueByFiltre"]) / 15;
                int R = Convert.ToInt32(Session["TotalHistoriqueByFiltre"]) % 15;

                if (R > 0)
                {
                    Q = Q + 1;
                    e.Row.Cells[2].Text = "Page : " + Convert.ToInt32(Session["IndexPageHistorique"]) + " / " + Q + "";
                }
                else
                {
                    e.Row.Cells[2].Text = "Page : " + Convert.ToInt32(Session["IndexPageHistorique"]) + " / " + Q + "";
                }
            }
            else
            {
                int Q = Convert.ToInt32(Session["TotalHistorique"]) / 15;
                int R = Convert.ToInt32(Session["TotalHistorique"]) % 15;

                if (R > 0)
                {
                    Q = Q + 1;
                    e.Row.Cells[2].Text = "Page : " + Convert.ToInt32(Session["IndexPageHistorique"]) + " / " + Q + "";
                }
                else
                {
                    e.Row.Cells[2].Text = "Page : " + Convert.ToInt32(Session["IndexPageHistorique"]) + " / " + Q + "";
                }

            }
        }


    }
   
    protected void CbFiltre_CheckedChanged(object sender, EventArgs e)
    {
        Panelfilter.Visible = CbFiltre.Checked;

    }
   
    protected void InsererPointageClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Interfaces/Historique/InsererPointage.aspx");
       
        if (Session["IndexPageHistorique"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageHistorique"]);

        }
        else
        {
            IndexPage = 1;

        }
        GridView_Load(IndexPage);
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        
        GridViewRow row = GridViewEvents.SelectedRow;
        int idEvent = int.Parse((row.FindControl("LblIdEvent") as Label).Text);
        int i = 0;

        i = events.SupprimerUnEvent(idEvent);

        if (i == 1)
        {
            Response.Redirect("~/Interfaces/Historique/Historique.aspx");
        }


        if (Session["IndexPageHistorique"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageHistorique"]);

        }
        else
        {
            IndexPage = 1;

        }
        GridView_Load(IndexPage);
    

    }
   
    protected void GridViewEvents_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewEvents.PageIndex = e.NewPageIndex;
        GridView_Load(IndexPage);

    }

    protected void BtnLast_Click(object sender, EventArgs e)
    {
        if (!TbDateDebut.IsEmpty || !TbDateFin.IsEmpty || TbMatriculeFiltre.Text != "" || TbNomFiltre.Text != "" || TbNPermis.Text != "" || TbImmatriculation.Text != "" || TbNTaxi.Text != "" || DDLTypeTaxi.SelectedValue != "-1" || TbCodeRefus.Text != "" || TbMatriculeAgent.Text != "" || RbTypeCodeRefus.SelectedItem.Value != "-1" || DDLModePointage.SelectedValue != "-1" || DDLLecteur.SelectedValue != "-1")
        {


            TotalHistoriqueByFiltre = Convert.ToInt32(Session["TotalHistoriqueByFiltre"]);
            int T = (int)TotalHistoriqueByFiltre / 15;
            if (T < TotalHistoriqueByFiltre / 15)
            {
                Session["IndexPageHistorique"] = ((int)TotalHistoriqueByFiltre / 15) + 1;
            }
            else
            {
                Session["IndexPageHistorique"] = ((int)TotalHistoriqueByFiltre / 15);
            }
        }
        else
        {
            TotalHistorique = Convert.ToInt32(Session["TotalHistorique"]);
            int T = (int)TotalHistorique / 15;
            if (T < TotalHistorique / 15)
            {
                Session["IndexPageHistorique"] = ((int)TotalHistorique / 15) + 1;
            }
            else
            {
                Session["IndexPageHistorique"] = ((int)TotalHistorique / 15);
            }

        }

        IndexPage = Convert.ToInt32(Session["IndexPageHistorique"]);
        GridView_Load(IndexPage);
    }

    protected void BtnNext_Click(object sender, EventArgs e)
    {



        if (Session["IndexPageHistorique"] != null)
        {


            Session["IndexPageHistorique"] = Convert.ToInt32(Session["IndexPageHistorique"]) + 1;
            IndexPage = Convert.ToInt32(Session["IndexPageHistorique"]);


        }


        else
        {
            Session["IndexPageHistorique"] = 2;
            IndexPage = 2;
        }
        GridView_Load(IndexPage);


    }

    protected void BtnPrevious_Click(object sender, EventArgs e)
    {

        Session["IndexPageHistorique"] = Convert.ToInt32(Session["IndexPageHistorique"]) - 1;
        IndexPage = Convert.ToInt32(Session["IndexPageHistorique"]);
        GridView_Load(IndexPage);

    }

    protected void BtnFirst_Click(object sender, EventArgs e)
    {
        Session["IndexPageHistorique"] = 1;
        IndexPage = Convert.ToInt32(Session["IndexPageHistorique"]);
        GridView_Load(IndexPage);
    }

    protected void CustomValidator_DateFin_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            TimeSpan? Ts = TbDateFin.SelectedDate - TbDateDebut.SelectedDate;

            if (Ts.Value <= TimeSpan.Parse("31.00:00:00") && Ts.Value > TimeSpan.Parse("00.00:00:00"))
            {
                args.IsValid = true;
                FailureText.Text += Ts.Value.ToString();
            }
            else
            {
                TbDateFin.Focus();
                FailureText.Text = "Merci de choisir une période inférieur ou égale à 31 jours.</br>";
                args.IsValid = false;
            }
        }
        catch (Exception)
        {
            FailureText.Text = "Merci de choisir une période inférieur ou égale à 31 jours.</br>";
            args.IsValid = false;
        }

    }

    protected void exportExcel_Click(object sender, EventArgs e)
    {

        Response.Clear();
        string FileName = "attachment;filename=" + "Historique_des_pointages"+ ".xls";
        Response.AddHeader("content-disposition", FileName);
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
        Response.ContentType = "application/vnd.xlsx";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        HtmlForm form = new HtmlForm();
        Page mypage = new System.Web.UI.Page();

        GridViewEvents.AllowPaging = false;
        GridViewEvents.AllowSorting = false;
        GridViewEvents.ShowFooter = false;

        GridViewEvents.EditIndex = -1;

       
      //  GridView_Load(IndexPage);
     
        GridViewEvents.Columns[0].Visible = false;


        DateTime debut = TbDateDebut.SelectedDate.Value;
        DateTime fin = TbDateFin.SelectedDate.Value;

        string matricule = TbMatriculeFiltre.Text;
        if (matricule == "")
        { matricule = null; }

        string nom = TbNomFiltre.Text;
        if (nom == "")
        { nom = null; }

        string numPermis = TbNPermis.Text;
        if (numPermis == "")
        { numPermis = null; }

        string immatriculation = TbImmatriculation.Text;
        if (immatriculation == "")
        { immatriculation = null; }


        string numTaxi = TbNTaxi.Text;
        if (numTaxi == "")
        { numTaxi = null; }

        string codeRefus = TbCodeRefus.Text;
        if (codeRefus == "")
        { codeRefus = null; }

        string matriculeAgent = TbMatriculeAgent.Text;
        if (matriculeAgent == "")
        { matriculeAgent = null; }



        int? typeCode = int.Parse(RbTypeCodeRefus.SelectedItem.Value);

        if (typeCode == -1)
        {
            typeCode = null;
        }
        int? numLecteur = int.Parse(DDLLecteur.SelectedValue);
        if (numLecteur == -1)
        {
            numLecteur = null;
        }

        int? sous_Type = int.Parse(DDLModePointage.SelectedValue);
        if (sous_Type == -1)
        {
            sous_Type = null;
        }
        int? typeTaxi = int.Parse(DDLTypeTaxi.SelectedValue);
        if (typeTaxi == -1)
        {
            typeTaxi = null;
        }


        GridViewEvents.DataSource = (new Events()).getAllEvents(debut, fin, matricule, nom, numPermis, immatriculation, numTaxi, typeTaxi, codeRefus, matriculeAgent, typeCode, sous_Type, numLecteur);
        GridViewEvents.DataBind();

        PrepareGridViewForExport(GridViewEvents);

        mypage.Controls.Add(form);
        form.Controls.Add(GridViewEvents);

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

        GridViewEvents.DataSource = ListEvents;
        GridViewEvents.DataBind();
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
            else if (current is Button)
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


        if (!TbDateDebut.IsEmpty || !TbDateFin.IsEmpty || TbMatriculeFiltre.Text != "" || TbNomFiltre.Text != "" || TbNPermis.Text != "" || TbImmatriculation.Text != "" || TbNTaxi.Text != "" || DDLTypeTaxi.SelectedValue != "-1" || TbCodeRefus.Text != "" || TbMatriculeAgent.Text != "" || RbTypeCodeRefus.SelectedItem.Value != "-1" || DDLModePointage.SelectedValue != "-1" || DDLLecteur.SelectedValue != "-1")
        {

            DateTime debut = TbDateDebut.SelectedDate.Value;
            DateTime fin = TbDateFin.SelectedDate.Value;

            string matricule = TbMatriculeFiltre.Text;
            if (matricule == "")
            { matricule = null; }

            string nom = TbNomFiltre.Text;
            if (nom == "")
            { nom = null; }

            string numPermis = TbNPermis.Text;
            if (numPermis == "")
            { numPermis = null; }

            string immatriculation = TbImmatriculation.Text;
            if (immatriculation == "")
            { immatriculation = null; }


            string numTaxi = TbNTaxi.Text;
            if (numTaxi == "")
            { numTaxi = null; }

            string codeRefus = TbCodeRefus.Text;
            if (codeRefus == "")
            { codeRefus = null; }

            string matriculeAgent = TbMatriculeAgent.Text;
            if (matriculeAgent == "")
            { matriculeAgent = null; }



            int? typeCode = int.Parse(RbTypeCodeRefus.SelectedItem.Value);

            if (typeCode == -1)
            {
                typeCode = null;
            }
            int? numLecteur = int.Parse(DDLLecteur.SelectedValue);
            if (numLecteur == -1)
            {
                numLecteur = null;
            }

            int? sous_Type = int.Parse(DDLModePointage.SelectedValue);
            if (sous_Type == -1)
            {
                sous_Type = null;
            }
            int? typeTaxi = int.Parse(DDLTypeTaxi.SelectedValue);
            if (typeTaxi == -1)
            {
                typeTaxi = null;
            }


            ListEvents = events.getAllEvents(debut, fin, matricule, nom, numPermis, immatriculation, numTaxi, typeTaxi, codeRefus, matriculeAgent, typeCode, sous_Type, numLecteur);

        }
        else
        {
            ListEvents = events.getAllEvents(null, null, null, null, null, null, null, null, null, null, null, null, null);
        }

        switch (sortExp)
        {
            case "Instant":
                if (sortDir == "desc")
                    ListEvents = ListEvents.OrderByDescending(o => DateTime.Parse(o.Instant)).ToList();
                else
                    ListEvents = ListEvents.OrderBy(o => DateTime.Parse(o.Instant)).ToList();
                break;

            case "Reference":
                if (sortDir == "desc")
                    ListEvents = ListEvents.OrderByDescending(o => o.Reference).ToList();
                else
                    ListEvents = ListEvents.OrderBy(o => o.Reference).ToList();
                break;


            case "Nom":
                if (sortDir == "desc")
                    ListEvents = ListEvents.OrderByDescending(o => o.Nom).ToList();
                else
                    ListEvents = ListEvents.OrderBy(o => o.Nom).ToList();
                break;



            case "Prenom":
                if (sortDir == "desc")
                    ListEvents = ListEvents.OrderByDescending(o => o.Prenom).ToList();
                else
                    ListEvents = ListEvents.OrderBy(o => o.Prenom).ToList();
                break;


            case "NumBadge":
                foreach (Events events in ListEvents)
                {
                    if (string.IsNullOrWhiteSpace(events.NumBadge) || string.IsNullOrEmpty(events.NumBadge))
                        events.NumBadge = "0";
                }
                if (sortDir == "desc")
                    ListEvents = ListEvents.OrderByDescending(o => int.Parse(o.NumBadge)).ToList();
                else
                    ListEvents = ListEvents.OrderBy(o => int.Parse(o.NumBadge)).ToList();
                break;

            case "NumAgrement":

                foreach (Events events in ListEvents)
                {
                    if (string.IsNullOrWhiteSpace(events.NumAgrement) || string.IsNullOrEmpty(events.NumAgrement))
                        events.NumAgrement = "0";
                }
                if (sortDir == "desc")
                    ListEvents = ListEvents.OrderByDescending(o => int.Parse(o.NumAgrement)).ToList();
                else
                    ListEvents = ListEvents.OrderBy(o => int.Parse(o.NumAgrement)).ToList();
                break;


            case "NumTaxi":
                if (sortDir == "desc")
                    ListEvents = ListEvents.OrderByDescending(o => int.Parse(o.NumTaxi)).ToList();
                else
                    ListEvents = ListEvents.OrderBy(o => int.Parse(o.NumTaxi)).ToList();
                break;


            case "LibelleTypeTaxi":
                if (sortDir == "desc")
                    ListEvents = ListEvents.OrderByDescending(o => o.LibelleTypeTaxi).ToList();
                else
                    ListEvents = ListEvents.OrderBy(o => o.LibelleTypeTaxi).ToList();
                break;

            case "CodeRefus":

                if (sortDir == "desc")
                    ListEvents = ListEvents.OrderByDescending(o => o.CodeRefus).ToList();
                else
                    ListEvents = ListEvents.OrderBy(o => o.CodeRefus).ToList();
                break;

            case "MatriculeAdmin":
                if (sortDir == "desc")
                    ListEvents = ListEvents.OrderByDescending(o => o.MatriculeAdmin).ToList();
                else
                    ListEvents = ListEvents.OrderBy(o => o.MatriculeAdmin).ToList();
                break;

            case "NomAgent":
                if (sortDir == "desc")
                    ListEvents = ListEvents.OrderByDescending(o => o.NomAgent).ToList();
                else
                    ListEvents = ListEvents.OrderBy(o => o.NomAgent).ToList();
                break;

            case "PrenomAgent":
                if (sortDir == "desc")
                    ListEvents = ListEvents.OrderByDescending(o => o.PrenomAgent).ToList();
                else
                    ListEvents = ListEvents.OrderBy(o => o.PrenomAgent).ToList();
                break;
        }


                if (Session["IndexPageHistorique"] != null)
                {

                    IndexPage = Convert.ToInt32(Session["IndexPageHistorique"]);

                }


                else
                {
                    Session["IndexPageHistorique"] = 1;
                    IndexPage = 1;
                }

                Session["ListEvents"] = ListEvents.Skip((Convert.ToInt32(Session["IndexPageHistorique"]) - 1) * 15).Take(15);
                GridViewEvents.DataSource = ListEvents.Skip((Convert.ToInt32(Session["IndexPageHistorique"]) - 1) * 15).Take(15);
                GridViewEvents.DataBind();
                FooterButtonGridViewEvents();

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

    protected void Historique_Sorting(object sender, GridViewSortEventArgs e)
    {
        BindGridView(e.SortExpression, sortOrder);

        int columnIndex = 0;

        foreach (DataControlFieldHeaderCell headerCell in GridViewEvents.HeaderRow.Cells)
        {
            if (headerCell.ContainingField.SortExpression == e.SortExpression)
            {
                columnIndex = GridViewEvents.HeaderRow.Cells.GetCellIndex(headerCell);
            }
        }

        GridViewEvents.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);
    }

    //-------------------------------------------------------------------------------------------

}