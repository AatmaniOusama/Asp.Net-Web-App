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






public partial class Interfaces_User_Badges : System.Web.UI.Page
{
    int IndexPage ;
    double TotalBadges;
    double TotalBadgesByFiltre;

    Badge badge = new Badge();
   
     List<Badge> ListBadges;
    

    Service service = new Service();
    Events events = new Events();
    Listes listes = new Listes();

   

    protected void Page_Load(object sender, EventArgs e)
    {
        

        try
        {
             TotalBadges = badge.getTotalBadges();
             Session["TotalBadges"] = TotalBadges;

            TbNumPermis.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbNom.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbPrenom.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbMatricule.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");

          
          

            Operateur operateur = (Operateur) Session["Operateur"];

            if (operateur.Profil.ToUpper() == "ADMINISTRATEUR")
          
            {

                BarreControle.Rows[0].Cells[2].Visible = true;
                BarreControle.Rows[0].Cells[3].Visible = true;
               
                BarreControle.Rows[0].Cells[4].Visible = true;
                BarreControle.Rows[0].Cells[5].Visible = true;
               
               
            }

            if (operateur.Profil.ToUpper() == "SUPERVISEUR")
            {

                BarreControle.Rows[0].Cells[2].Visible = true;   // Exporter vers Excel
                BarreControle.Rows[0].Cells[3].Visible = false;  // Ajouter Badge
                BarreControle.Rows[0].Cells[4].Visible = false;  // Suprimer Badge
                BarreControle.Rows[0].Cells[5].Visible = false;  // Autoriser Interdire Badge
                Panel_Ajout.Visible = false;

            }

            if (operateur.Profil.ToUpper() == "CONSULTANT")
            {

                BarreControle.Rows[0].Cells[2].Visible = true;
                BarreControle.Rows[0].Cells[3].Visible = false;
                BarreControle.Rows[0].Cells[4].Visible = false;
                BarreControle.Rows[0].Cells[5].Visible = false;
                Panel_Ajout.Visible = false;
              
            }

            if (operateur.MenusAutorises.Substring(0, 1) == "0")
            {
                Session.Abandon();
                Response.Redirect("~/");
            }


           
            if (!IsPostBack)
            {
                ViewState["sortOrder"] = "";





                TbNumPermis.Text = Session["badgeNumPermis"] as string;
                TbNom.Text = Session["badgeNom"] as string;
                TbPrenom.Text = Session["badgePrenom"] as string;
                TbMatricule.Text = Session["badgeMatricule"] as string;
                
                if (!string.IsNullOrEmpty(Session["badgeFlagAutorise"] as string))
                RbAutorise.SelectedValue = Session["badgeFlagAutorise"] as string;

                if (!string.IsNullOrEmpty(Session["badgeAttribue"] as string))
                    RbAttribue.SelectedValue = Session["badgeAttribue"] as string;

               


                if (Session["IndexPageBadge"] != null)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageBadge"]);
               
                }
                else
                {
                    Session["IndexPageBadge"] = 1;
                    IndexPage = 1;
                }


              if (Convert.ToBoolean(Session["SelectBadgeAdd"]) == true)
                {
                    LaodGridView(IndexPage);
                    GridViewBadges.SelectRow(0);
                    Session["SelectBadgeAdd"] = false;
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

        string numBadge = TbNumPermis.Text;
        if (numBadge == "")
        { numBadge = null; }
       
        string nom = TbNom.Text;
        if (nom == "")
        { nom = null; }

        string prenom = TbPrenom.Text;
        if (prenom == "")
        { prenom = null; }

        string matricule = TbMatricule.Text;
        if (matricule == "")
        { matricule = null; }

        string rbautoriseValue = RbAutorise.SelectedValue;
        if (rbautoriseValue == "YN")
        {
            rbautoriseValue = null;
        }

        string rbAttribue= RbAttribue.SelectedValue;
       

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        // Verifier si l'un des champs des filtre  remplis ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        if (TbNumPermis.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || TbMatricule.Text != "" || (RbAutorise.SelectedValue == "Y" || RbAutorise.SelectedValue == " ") || (RbAttribue.SelectedValue == "1" || RbAttribue.SelectedValue == "0"))
        {
                TotalBadgesByFiltre = badge.getTotalBadgesByFiltre(numBadge, rbautoriseValue, nom, prenom, matricule, rbAttribue);  // getTotalBadgesByFiltre : donne le nombre des badges Enrolé et Non Enrolé
                Session["TotalBadgesByFiltre"] = TotalBadgesByFiltre;
                NbrLignes.Text = TotalBadgesByFiltre.ToString();
                      
        }

        else
        {                        
            NbrLignes.Text = TotalBadges.ToString();
        }


         // Fin Vérification -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ListBadges = badge.getBadges(IndexPage,numBadge, rbautoriseValue, nom, prenom, matricule, rbAttribue);
         
            Session["ListBadges"] = ListBadges;
            GridViewBadges.DataSource = ListBadges;
            GridViewBadges.DataBind();

            BtInterdire.Visible = false;
            BtAutoriser.Visible = false;          
            BtnDelete.Visible = false;
           

            GridViewBadges.SelectedIndex = -1;
            FooterButtonGridViewBadges();

 
    }

    protected void FooterButtonGridViewBadges()
    {
       
        // Gérer l'affichage des bouttons Next previous... dans le footer -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
           

        if (((ListBadges.Count() < 15 && TotalBadges < 15) || (ListBadges.Count() < 15 && Convert.ToInt32(Session["TotalBadgesByFiltre"]) < 15&& Convert.ToInt32(Session["TotalBadgesByFiltre"]) > 0 ) || TotalBadges == 15 || Convert.ToInt32(Session["TotalBadgesByFiltre"]) == 15) && Convert.ToInt32(Session["TotalBadgesByFiltre"]) != 0)
        {

            GridViewBadges.FooterRow.Cells[5].Enabled = false;
            GridViewBadges.FooterRow.Cells[4].Enabled = false;

        }

        if (TbNumPermis.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || TbMatricule.Text != "" || (RbAutorise.SelectedValue == "Y" || RbAutorise.SelectedValue == " ") || (RbAttribue.SelectedValue == "1" || RbAttribue.SelectedValue == "0"))
        {
            if (IndexPage == 1 && Convert.ToInt32(Session["TotalBadgesByFiltre"]) != 0)
            {
                GridViewBadges.FooterRow.Cells[4].Enabled = false;
            }
            else
            {

                TotalBadgesByFiltre = Convert.ToInt32(Session["TotalBadgesByFiltre"]);
                int T = (int)TotalBadgesByFiltre / 15;

                if (T < TotalBadgesByFiltre / 15)
                {
                    if (IndexPage == T + 1)
                    {
                        GridViewBadges.FooterRow.Cells[5].Enabled = false;


                    }

                }
                else
                {
                    if (IndexPage == T)
                    {
                        GridViewBadges.FooterRow.Cells[5].Enabled = false;


                    }
                }
            }
        }


        else
        {

            if (IndexPage == 1 && TotalBadges != 0)
            {
                GridViewBadges.FooterRow.Cells[4].Enabled = false;
            }
            else
            {


                int TU = (int)TotalBadges / 15;

                if (TU < TotalBadges / 15)
                {
                    if (IndexPage == TU + 1)
                    {
                        GridViewBadges.FooterRow.Cells[5].Enabled = false;


                    }

                }
                else
                {
                    if (IndexPage == TU)
                    {
                        GridViewBadges.FooterRow.Cells[5].Enabled = false;


                    }
                }
            }

        }

        // Fin -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
      
    }

    protected void Filtre(object sender, EventArgs e)
    {
  
        
        
        Session["badgeNumPermis"] = TbNumPermis.Text;
        Session["badgeNom"] = TbNom.Text; 
        Session["badgePrenom"] = TbPrenom.Text ;
        Session["badgeMatricule"] = TbMatricule.Text;                    
        Session["badgeFlagAutorise"] = RbAutorise.SelectedValue;
        Session["badgeAttribue"] = RbAttribue.SelectedValue;



        if (TbNumPermis.Text != "" || TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || RbAttribue.SelectedValue != "-1" || RbAutorise.SelectedValue != "YN")
        {
            Session["IndexPageBadge"] = 1;
            IndexPage = 1;
        
        }

        if (Session["IndexPageBadge"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageBadge"]);

        }
        else
        {
            Session["IndexPageBadge"] = 1;
            IndexPage = 1;
           
        }
      
        
        LaodGridView(IndexPage);
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Session["SelectBadgeAdd"] = true;
    }

    protected void BtSaveAdd_Click(object sender, EventArgs e)
    {

        Badge item = new Badge();
        int i = 0;

        if (!(item.BadgeExiste(TbNumPermisAdd.Text)))
        {
            item.NumBadge = TbNumPermisAdd.Text;
            item.FlagAutorise = "Y";

            i = item.AjouterUnBadge(item);
            Session["SelectBadgeAdd"] = true;
           
            if (i == 1)
            {
                Response.Redirect("~/Interfaces/User/Badges.aspx");
               
            }

        }
        else
        {

            string numPermis = TbNumPermisAdd.Text;
            string myStringVariable = "Impossible d ajouter le permis de confiance N° " + " " + numPermis + " " + "à la liste , car il existe déja !!";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + myStringVariable + "')", true);

          
        }

       
    }
  
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
       
        GridViewRow row = GridViewBadges.SelectedRow;
        string numBadge = (row.FindControl("LbNumPermis") as Label).Text;
        int i = 0;

        i = badge.SupprimerUnBadge(numBadge);

            if (i == 1)
            {
                Response.Redirect("~/Interfaces/User/Badges.aspx");
            }

    }
  
    protected void GridViewBadges_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GridViewBadges_IndexChanging(object sender, GridViewPageEventArgs e)
    {
        
        GridViewBadges.PageIndex = e.NewPageIndex;
        GridViewBadges.DataSource = Session["ListBadges"] as List<Badge>;
        GridViewBadges.DataBind();

    }

    protected void CbFiltre_CheckedChanged(object sender, EventArgs e)
    {
        Panelfilter.Visible = CbFiltre.Checked;

    }

    protected void GridViewBadges_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow row = GridViewBadges.Rows[e.NewSelectedIndex];
      
        string FlagAutorise = (row.FindControl("Selected") as Label).Text;

        BtnDelete.Visible = true;
        

        BtAutoriser.Visible = (FlagAutorise != "Y");
        BtInterdire.Visible = (FlagAutorise == "Y");


    }

    protected void GridViewBadges_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("OnClick", String.Format("javascript:__doPostBack('ctl00$MainContent$GridViewBadges','Select${0}')", e.Row.RowIndex));      
    
            if (Convert.ToInt32(Session["IndexPageBadge"]) == 0)
            {
                Session["IndexPageBadge"] = 1;
                IndexPage = Convert.ToInt32(Session["IndexPageBadge"]);
            }

            if ((e.Row.Cells[2].FindControl("Selected") as Label).Text == "Y")
            {
                e.Row.Cells[0].FindControl("ImgInvalide").Visible = false;
                e.Row.Cells[0].FindControl("ImgValide").Visible = true;
            }
            if ((e.Row.Cells[2].FindControl("Selected") as Label).Text == " ")
            {
                e.Row.Cells[0].FindControl("ImgInvalide").Visible = true;
                e.Row.Cells[0].FindControl("ImgValide").Visible = false;
            }

            if ((e.Row.Cells[0].FindControl("LbNumPermis") as Label).Text == "0")
                (e.Row.Cells[0].FindControl("LbNumPermis") as Label).Text = "";
        }

       if (e.Row.RowType == DataControlRowType.Footer)
       { 
        e.Row.Cells[6].Text = "Page : "+IndexPage+"";

        if (TbNumPermis.Text != "" || TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || RbAttribue.SelectedValue != "-1" || RbAutorise.SelectedValue != "YN")
        {
            int Q = Convert.ToInt32(Session["TotalBadgesByFiltre"]) / 15;
            int R = Convert.ToInt32(Session["TotalBadgesByFiltre"]) % 15;

            if (R > 0)
            {
                Q = Q + 1;
                e.Row.Cells[6].Text = "Page : " + Convert.ToInt32(Session["IndexPageBadge"]) + " / " + Q + "";
            }
            else
            {
                e.Row.Cells[6].Text = "Page : " + Convert.ToInt32(Session["IndexPageBadge"]) + " / " + Q + "";
            }
        }
        else
        {
            int Q = Convert.ToInt32(Session["TotalBadges"]) / 15;
            int R = Convert.ToInt32(Session["TotalBadges"]) % 15;

            if (R > 0)
            {
                Q = Q + 1;
                e.Row.Cells[6].Text = "Page : " + Convert.ToInt32(Session["IndexPageBadge"]) + " / " + Q + "";
            }
            else
            {
                e.Row.Cells[6].Text = "Page : " + Convert.ToInt32(Session["IndexPageBadge"]) + " / " + Q + "";
            }

        }

       }

       
    }

    protected void BtAutoriser_Click(object sender, EventArgs e)
    {
        GridViewRow row = GridViewBadges.Rows[GridViewBadges.SelectedIndex];
        Badge badge = new Badge();

        string numBadge = (row.FindControl("LbNumPermis") as Label).Text;

        int i = badge.ModifierFlagAutorise("Y", numBadge);

        if (i == 1)
        {
            if (Session["IndexPageBadge"] != null)
            {
                IndexPage = Convert.ToInt32(Session["IndexPageBadge"]);
            }
            else
            {
                IndexPage = 1;

            }

            LaodGridView(IndexPage);

            if (TbNumPermis.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || TbMatricule.Text != "" || (RbAutorise.SelectedValue == "Y" || RbAutorise.SelectedValue == " ") || (RbAttribue.SelectedValue == "1" || RbAttribue.SelectedValue == "0"))
            {

                int TotalRows = GridViewBadges.Rows.Count;

                if (TotalBadgesByFiltre > 0)
                {
                    if (TotalRows >= 1)
                    {
                        if (row.RowIndex == TotalRows)
                        {
                            GridViewBadges.SelectRow(row.RowIndex - 1);
                        }
                        else if ((row.RowIndex < TotalRows) && (row.RowIndex >= 0))
                        {
                            GridViewBadges.SelectRow(row.RowIndex);
                        }

                    }
                    if (TotalRows == 0)
                    {
                        IndexPage = Convert.ToInt32(Session["IndexPageBadge"]) - 1;
                        Session["IndexPageBadge"] = IndexPage;
                        LaodGridView(IndexPage);
                        GridViewBadges.SelectRow(GridViewBadges.Rows.Count - 1);
                    }
                }

                else if (TotalBadgesByFiltre == 0)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageBadge"]);
                    Session["IndexPageBadge"] = IndexPage;
                    LaodGridView(IndexPage);
                }
            }

            else
            {
                int TotalRows = GridViewBadges.Rows.Count;
                if (TotalBadges > 0)
                {
                    if (TotalRows >= 1)
                    {
                        if (row.RowIndex == TotalRows)
                        {
                            GridViewBadges.SelectRow(row.RowIndex - 1);
                        }
                        else if ((row.RowIndex < TotalRows) && (row.RowIndex >= 0))
                        {
                            GridViewBadges.SelectRow(row.RowIndex);
                        }

                    }
                    if (TotalRows == 0)
                    {
                        IndexPage = Convert.ToInt32(Session["IndexPageBadge"]) - 1;
                        Session["IndexPageBadge"] = IndexPage;
                        LaodGridView(IndexPage);
                        GridViewBadges.SelectRow(GridViewBadges.Rows.Count - 1);
                    }
                }

                else if (TotalBadges == 0)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageBadge"]);
                    Session["IndexPageBadge"] = IndexPage;
                    LaodGridView(IndexPage);
                }
            }
        }

    }

    protected void BtInterdir_Click(object sender, EventArgs e)
    {
        GridViewRow row = GridViewBadges.Rows[GridViewBadges.SelectedIndex];
        Badge badge = new Badge();

        string numBadge = (row.FindControl("LbNumPermis") as Label).Text;

        int i = badge.ModifierFlagAutorise(" ", numBadge);

        if (i == 1)
        {
            if (Session["IndexPageBadge"] != null)
            {
                IndexPage = Convert.ToInt32(Session["IndexPageBadge"]);
            }
            else
            {
                IndexPage = 1;

            }

            LaodGridView(IndexPage);

            if (TbNumPermis.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || TbMatricule.Text != "" || (RbAutorise.SelectedValue == "Y" || RbAutorise.SelectedValue == " ") || (RbAttribue.SelectedValue == "1" || RbAttribue.SelectedValue == "0"))
            {

                int TotalRows = GridViewBadges.Rows.Count;

                if (TotalBadgesByFiltre > 0)
                {
                    if (TotalRows >= 1)
                    {
                        if (row.RowIndex == TotalRows)
                        {
                            GridViewBadges.SelectRow(row.RowIndex - 1);
                        }
                        else if ((row.RowIndex < TotalRows) && (row.RowIndex >= 0))
                        {
                            GridViewBadges.SelectRow(row.RowIndex);
                        }

                    }
                    if (TotalRows == 0)
                    {
                        IndexPage = Convert.ToInt32(Session["IndexPageBadge"]) - 1;
                        Session["IndexPageBadge"] = IndexPage;
                        LaodGridView(IndexPage);
                        GridViewBadges.SelectRow(GridViewBadges.Rows.Count - 1);
                    }
                }

                else if (TotalBadgesByFiltre == 0)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageBadge"]);
                    Session["IndexPageBadge"] = IndexPage;
                    LaodGridView(IndexPage);
                }
            }

            else
            {
                int TotalRows = GridViewBadges.Rows.Count;
                if (TotalBadges > 0)
                {
                    if (TotalRows >= 1)
                    {
                        if (row.RowIndex == TotalRows)
                        {
                            GridViewBadges.SelectRow(row.RowIndex - 1);
                        }
                        else if ((row.RowIndex < TotalRows) && (row.RowIndex >= 0))
                        {
                            GridViewBadges.SelectRow(row.RowIndex);
                        }

                    }
                    if (TotalRows == 0)
                    {
                        IndexPage = Convert.ToInt32(Session["IndexPageBadge"]) - 1;
                        Session["IndexPageBadge"] = IndexPage;
                        LaodGridView(IndexPage);
                        GridViewBadges.SelectRow(GridViewBadges.Rows.Count - 1);
                    }
                }

                else if (TotalBadges == 0)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageBadge"]);
                    Session["IndexPageBadge"] = IndexPage;
                    LaodGridView(IndexPage);
                }
            }
        }

    }

    protected void BtnLast_Click(object sender, EventArgs e)
    {
         if (TbMatricule.Text != "" || TbNom.Text!= "" ||  RbAttribue.SelectedValue != "-1" || RbAutorise.SelectedValue !="YN" )
        { 

           
             TotalBadgesByFiltre = Convert.ToInt32(Session["TotalBadgesByFiltre"]);
             int T = (int) TotalBadgesByFiltre / 15;
             if (T < TotalBadgesByFiltre / 15)
             {
                 Session["IndexPageBadge"] = ((int)TotalBadgesByFiltre / 15) + 1;
             }
             else 
             {
                 Session["IndexPageBadge"] = ((int)TotalBadgesByFiltre / 15);
             }
        }
        else
         {
             TotalBadges = Convert.ToInt32(Session["TotalBadges"]);
             int T = (int)TotalBadges / 15;
             if (T < TotalBadges / 15)
             {
                 Session["IndexPageBadge"] = ((int)TotalBadges / 15) + 1;
             }
             else
             {
                 Session["IndexPageBadge"] = ((int)TotalBadges / 15);
             }

        }

         IndexPage = Convert.ToInt32(Session["IndexPageBadge"]);
         LaodGridView(IndexPage);
    }

    protected void BtnNext_Click(object sender, EventArgs e)
    {

       
        
            if (Session["IndexPageBadge"] != null )
            {

             
                    Session["IndexPageBadge"] = Convert.ToInt32(Session["IndexPageBadge"]) + 1;
                    IndexPage = Convert.ToInt32(Session["IndexPageBadge"]);
             

            }
            
            
            else
            {
                Session["IndexPageBadge"] = 2;
                IndexPage = 2;
            }
            LaodGridView(IndexPage);

        
    }

    protected void BtnPrevious_Click(object sender, EventArgs e)
    {

            Session["IndexPageBadge"] = Convert.ToInt32(Session["IndexPageBadge"]) - 1;
            IndexPage = Convert.ToInt32(Session["IndexPageBadge"]);
            LaodGridView(IndexPage);
        
    }

    protected void BtnFirst_Click(object sender, EventArgs e)
    {
        Session["IndexPageBadge"] =  1;
        IndexPage = Convert.ToInt32(Session["IndexPageBadge"]);
        LaodGridView(IndexPage);
    }

    protected void BtCancel_Click(object sender, EventArgs e)
    {
        MPE.Hide();

       Session["SelectBadgeAdd"] = false;

    }

    protected void exportExcel_Click(object sender, EventArgs e)
    {

        Response.Clear();
        string FileName = "attachment;filename=" + "Liste_des_badges" + ".xls";
        Response.AddHeader("content-disposition", FileName);

        Response.ContentEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
        Response.ContentType = "application/vnd.xlsx";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        HtmlForm form = new HtmlForm();
        Page mypage = new System.Web.UI.Page();

        GridViewBadges.AllowPaging = false;
        GridViewBadges.AllowSorting = false;

        GridViewBadges.EditIndex = -1;



        GridViewBadges.Columns[0].Visible = false;
        GridViewBadges.Columns[1].Visible = false;

        GridViewBadges.ShowFooter = false;

        if (TbNumPermis.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || TbMatricule.Text != "" || (RbAutorise.SelectedValue == "Y" || RbAutorise.SelectedValue == " ") || (RbAttribue.SelectedValue == "1" || RbAttribue.SelectedValue == "0"))
        {

            string numBadge = TbNumPermis.Text;
            if (numBadge == "")
            { numBadge = null; }

            string rbautoriseValue = RbAutorise.SelectedValue;
            if (rbautoriseValue == "YN")
            {
                rbautoriseValue = null;
            }

            string nom = TbNom.Text;
            if (nom == "")
            { nom = null; }

            string prenom = TbPrenom.Text;
            if (prenom == "")
            { prenom = null; }

            string matricule = TbMatricule.Text;
            if (matricule == "")
            { matricule = null; }

            string rbAttribue = RbAttribue.SelectedValue;


            GridViewBadges.DataSource = (new Badge()).getAllBadgesByFiltre(numBadge, rbautoriseValue, matricule, nom, prenom, rbAttribue);
            
        }
        else 
        {
            GridViewBadges.DataSource = (new Badge()).getAllBadges();
            
        }


        GridViewBadges.DataBind();


        PrepareGridViewForExport(GridViewBadges);

        mypage.Controls.Add(form);
        form.Controls.Add(GridViewBadges);


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
        Response.Write("<html><head><style type='text/css'>" + sb.ToString() + "</style></head><body>" + stringWrite.ToString() + "</body></html>");
        Response.Flush();
        Response.Close();
        Response.End();

        GridViewBadges.DataSource = ListBadges;
        GridViewBadges.DataBind();
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
                if (champ == "Y" || champ == " ")
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
        // Tester s'il y un filtre ou pas 


        if (TbNumPermis.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || TbMatricule.Text != "" || (RbAutorise.SelectedValue == "Y" || RbAutorise.SelectedValue == " ") || (RbAttribue.SelectedValue == "1" || RbAttribue.SelectedValue == "0"))
        {
            string numBadge = TbNumPermis.Text;
            if (numBadge == "")
            { numBadge = null; }

            string nom = TbNom.Text;
            if (nom == "")
            { nom = null; }

            string prenom = TbPrenom.Text;
            if (prenom == "")
            { prenom = null; }

            string matricule = TbMatricule.Text;
            if (matricule == "")
            { matricule = null; }

            string rbautoriseValue = RbAutorise.SelectedValue;
            if (rbautoriseValue == "YN")
            {
                rbautoriseValue = null;
            }

            string rbAttribue = RbAttribue.SelectedValue;
           


            ListBadges = badge.getAllBadgesByFiltre(numBadge, rbautoriseValue, nom, prenom, matricule, rbAttribue);

        }
        else
        {
            ListBadges = badge.getAllBadges();
        }

        switch (sortExp)
        {
            case "NumPermis":
                foreach (Badge badge in ListBadges)
                {
                    if (string.IsNullOrWhiteSpace(badge.NumBadge) || string.IsNullOrEmpty(badge.NumBadge))
                        badge.NumBadge = "0";
                }
                if (sortDir == "desc")
                    ListBadges = ListBadges.OrderByDescending(o => int.Parse(o.NumBadge)).ToList();
                else
                    ListBadges = ListBadges.OrderBy(o => int.Parse(o.NumBadge)).ToList();
                break;

            case "FlagAutorise":
                if (sortDir == "desc")
                    ListBadges = ListBadges.OrderByDescending(o => o.FlagAutorise).ToList();
                else
                    ListBadges = ListBadges.OrderBy(o => o.FlagAutorise).ToList();
                break;

            case "Nom":
                if (sortDir == "desc")
                    ListBadges = ListBadges.OrderByDescending(o => o.Nom).ToList();
                else
                    ListBadges = ListBadges.OrderBy(o => o.Nom).ToList();
                break;

            case "Prenom":
                if (sortDir == "desc")
                    ListBadges = ListBadges.OrderByDescending(o => o.Prenom).ToList();
                else
                    ListBadges = ListBadges.OrderBy(o => o.Prenom).ToList();
                break;

            case "Matricule":

                if (sortDir == "desc")
                    ListBadges = ListBadges.OrderByDescending(o => o.Matricule).ToList();
                else
                    ListBadges = ListBadges.OrderBy(o => o.Matricule).ToList();
                break;

        }



        if (Session["IndexPageBadge"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageBadge"]);
        }
        else
        {
            Session["IndexPageBadge"] = 1;
            IndexPage = 1;

        }



        Session["ListBadges"] = ListBadges.Skip((Convert.ToInt32(Session["IndexPageBadge"]) - 1) * 15).Take(15);

        GridViewBadges.DataSource = ListBadges.Skip((Convert.ToInt32(Session["IndexPageBadge"]) - 1) * 15).Take(15);
        GridViewBadges.DataBind();
        FooterButtonGridViewBadges();

       
        
    }


    Image sortImage = new Image();
    public string sortOrder
    {
    

        get
        {
          // photo_badge.Visible = false;
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


    protected void badges_Sorting(object sender, GridViewSortEventArgs e)
    {

        
        BindGridView(e.SortExpression, sortOrder);
        

        int columnIndex = 0;

        foreach (DataControlFieldHeaderCell headerCell in GridViewBadges.HeaderRow.Cells)
        {
            if (headerCell.ContainingField.SortExpression == e.SortExpression)
            {
                columnIndex = GridViewBadges.HeaderRow.Cells.GetCellIndex(headerCell);
            }
        }

        GridViewBadges.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);

       
    }   

//-------------------------------------------------------------------------------------------

}
