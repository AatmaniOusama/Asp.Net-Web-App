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
using System.Text;
using System.Net.Mail;





public partial class User_index : System.Web.UI.Page
{
    int    IndexPage;
    double TotalUsers;
    double TotalUsersByFiltre;



    User    user    = new User();
    Service service = new Service();
    Events  events  = new Events();
    Listes  listes  = new Listes();
    List<User>   ListUsers;
   

    protected void Page_Load(object sender, EventArgs e)
    {


        try
        {
            

             TotalUsers = user.getTotalUsers();
             Session["TotalUsers"] = TotalUsers;

            TbNom.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbMatricule.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");

           
       

            Operateur operateur = (Operateur) Session["Operateur"];

            if (operateur.Profil.ToUpper() == "ADMINISTRATEUR")
          
            {
                
                BarreControle.Rows[0].Cells[2].Visible = true;
                BarreControle.Rows[0].Cells[3].Visible = true;
                BarreControle.Rows[0].Cells[4].Visible = true;
                BarreControle.Rows[0].Cells[5].Visible = true;
                BarreControle.Rows[0].Cells[6].Visible = true;
                BarreControle.Rows[0].Cells[7].Visible = true;
            }

            if (operateur.Profil.ToUpper() == "SUPERVISEUR")
            {

                BarreControle.Rows[0].Cells[2].Visible = true; //Exporter à Excel Users
                BarreControle.Rows[0].Cells[3].Visible = false;
                BarreControle.Rows[0].Cells[4].Visible = true; // Set User
                BarreControle.Rows[0].Cells[5].Visible = false;
                BarreControle.Rows[0].Cells[6].Visible = true; // Autoriser Interdir User
                BarreControle.Rows[0].Cells[7].Visible = false;
           
            }


            if (operateur.Profil.ToUpper() == "CONSULTANT")
            {

                BarreControle.Rows[0].Cells[2].Visible = true; //Exporter à Excel Users
                BarreControle.Rows[0].Cells[3].Visible = false;
                BarreControle.Rows[0].Cells[4].Visible = false; // Set User
                BarreControle.Rows[0].Cells[5].Visible = false;
                BarreControle.Rows[0].Cells[6].Visible = false; // Autoriser Interdir User
                BarreControle.Rows[0].Cells[7].Visible = false;
            }

            if (operateur.MenusAutorises.Substring(0, 1) == "0")
            {
                Session.Abandon();
                Response.Redirect("~/");
            }


           
            if (!IsPostBack)
            {
                ViewState["sortOrder"] = "";


                TbMatricule.Text = Session["userMatricule"] as string;
                TbNom.Text= Session["userNom"] as string;
                TbPrenom.Text = Session["userPrenom"] as string;
                TbBadge.Text= Session["userNumBadge"] as string ;

               

                if (!string.IsNullOrEmpty(Session["userFlagAutorise"] as string))
                RbAutorise.SelectedValue = Session["userFlagAutorise"] as string;

                if (!string.IsNullOrEmpty(Session["userEnroler"] as string))
                    RbEnroler.SelectedValue = Session["userEnroler"] as string;

                if (!string.IsNullOrEmpty(Session["userEncoder"] as string))
                    RbCoder.SelectedValue = Session["userEncoder"] as string;


                if (Session["IndexPageUser"] != null)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageUser"]);
               
                }
                else
                {
                    Session["IndexPageUser"] = 1;
                    IndexPage = 1;
                }


                if (Convert.ToBoolean(Session["SelectUserSet"]) == true)
                {
                    LaodGridView(IndexPage);
                    GridViewRow row = Session["Rowselected"] as GridViewRow;
                    GridViewUsers.SelectRow(row.RowIndex);
                    Session["SelectUserSet"] = false;
                }
                else if (Convert.ToBoolean(Session["SelectUserAdd"]) == true)
                {
                    LaodGridView(IndexPage);
                    GridViewUsers.SelectRow(0);
                    Session["SelectUserAdd"] = false;
                }
                else
                {
                    LaodGridView(IndexPage); 
                    
                }
            }
        }
        catch (Exception ex)
        {
           
        }
    }


    protected void LaodGridView(int IndexPage)
    {


        // ---------------  Récupérer les Valeurs des Filtres ----------------------------------------------------------------------------------------------------------------------------------------
       
        string matricule = TbMatricule.Text;
        
        if (matricule == "")
        { matricule = null; }
       
       
        string nom = TbNom.Text;
        if (nom == "")
        { nom = null; }

        string prenom = TbPrenom.Text;
        if (prenom == "")
        { prenom = null; }

        string numPermis = TbBadge.Text;
        if (numPermis == "")
        { numPermis = null; }

      
        string rbautoriseValue = RbAutorise.SelectedValue;
        if (rbautoriseValue == "YN")
        {
            rbautoriseValue = null;
        }

        string rbenrolerValue = RbEnroler.SelectedValue;
        string rbCoderValue = RbCoder.SelectedValue;

        int? rbCoderValueInt = int.Parse(rbCoderValue);

        if (rbCoderValueInt == -1)
        {
            rbCoderValueInt = null;
        }

        if (rbCoderValue == "1")
        {
            rbCoderValueInt = 1;
        } 
        if (rbCoderValue == "0")
        {
            rbCoderValueInt = 0;
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        // Verifier si l'un des champs des filtre  remplis ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        if (TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || TbBadge.Text != "" || RbEnroler.SelectedValue != "-1" || RbAutorise.SelectedValue != "YN" || RbCoder.SelectedValue != "-1")
        {                       
                TotalUsersByFiltre = user.getTotalUsersByFiltre(matricule, nom,prenom, numPermis, rbautoriseValue,rbenrolerValue, rbCoderValueInt);  // getTotalUsersByFiltre : donne le nombre des Users Enrolé et Non Enrolé
                Session["TotalUsersByFiltre"] = TotalUsersByFiltre;
                NbrLignes.Text = TotalUsersByFiltre.ToString();
                      
        }

        else
        {                        
            NbrLignes.Text = TotalUsers.ToString();
        }


         // Fin Vérification -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        if (RbEnroler.SelectedValue != "-1")
        {
            ListUsers = user.GetUsersEnroler(IndexPage, matricule, nom, prenom, numPermis, rbautoriseValue, rbenrolerValue, rbCoderValueInt);

        }
        else
        {
            ListUsers = user.GetUsers(IndexPage, matricule, nom, prenom, numPermis, rbautoriseValue, rbCoderValueInt);
        }
            
        
            Session["ListUsers"] = ListUsers;
            GridViewUsers.DataSource = ListUsers;
            GridViewUsers.DataBind();

            BtInterdire.Visible = false;
            BtAutoriser.Visible = false;
            BtnSet.Visible = false;
            BtnDelete.Visible = false;
            BtnUpdateLecteurNow.Visible = false;

            GridViewUsers.SelectedIndex = -1;

            FooterButtonGridViewUsers();
       
       
    }

    protected void btnPrintFromCodeBehind_Click(object sender, EventArgs e)
    {

            GridViewUsers.AllowPaging = false;
            GridViewUsers.AllowSorting = false;
            GridViewUsers.ShowFooter = false;


            GridViewUsers.EditIndex = -1;

            

            GridViewUsers.Columns[0].Visible = false;
            GridViewUsers.Columns[1].Visible = false;

            string matricule = TbMatricule.Text;

            if (matricule == "")
            { matricule = null; }


            string nom = TbNom.Text;
            if (nom == "")
            { nom = null; }

            string prenom = TbPrenom.Text;
            if (prenom == "")
            { prenom = null; }

            string numPermis = TbBadge.Text;
            if (numPermis == "")
            { numPermis = null; }


            string rbautoriseValue = RbAutorise.SelectedValue;
            if (rbautoriseValue == "YN")
            {
                rbautoriseValue = null;
            }

            string rbenrolerValue = RbEnroler.SelectedValue;
            string rbCoderValue = RbCoder.SelectedValue;

            int? rbCoderValueInt = int.Parse(rbCoderValue);

            if (rbCoderValueInt == -1)
            {
                rbCoderValueInt = null;
            }

            if (rbCoderValue == "1")
            {
                rbCoderValueInt = 1;
            }
            if (rbCoderValue == "0")
            {
                rbCoderValueInt = 0;
            }




            GridViewUsers.DataSource = (new User()).getAllUsersByFiltre(matricule, nom, prenom, numPermis, rbautoriseValue, rbenrolerValue, rbCoderValueInt);
            GridViewUsers.DataBind();

            PrepareGridViewForExport(GridViewUsers);


            try
            {      
                ScriptManager.RegisterStartupScript(this, typeof(Page), "printGrid", "printGrid();", true);
            }
            catch { }

        
    }
  
    protected void FooterButtonGridViewUsers()
    {
        // Gérer l'affichage des bouttons Next previous... dans le footer -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

   
       if ((ListUsers.Count() < 15 && TotalUsers < 15) || (ListUsers.Count() < 15 && Convert.ToInt32(Session["TotalUsersByFiltre"]) < 15 && Convert.ToInt32(Session["TotalUsersByFiltre"]) > 0) || TotalUsers == 15 || (Convert.ToInt32(Session["TotalUsersByFiltre"]) == 15 && Convert.ToInt32(Session["TotalUsersByFiltre"]) != 0)) 
     
        {
            GridViewUsers.FooterRow.Cells[8].Enabled = false;
            GridViewUsers.FooterRow.Cells[7].Enabled = false;

        }

       if (TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || TbBadge.Text != "" || RbEnroler.SelectedValue != "-1" || RbAutorise.SelectedValue != "YN" || RbCoder.SelectedValue != "-1")
        {

            if (IndexPage == 1 && Convert.ToInt32(Session["TotalUsersByFiltre"]) != 0)
            {
                GridViewUsers.FooterRow.Cells[7].Enabled = false;
            }

            else
            {

                TotalUsersByFiltre = Convert.ToInt32(Session["TotalUsersByFiltre"]);
                int T = (int)TotalUsersByFiltre / 15;

                if (T < TotalUsersByFiltre / 15)
                {
                    if (IndexPage == T + 1)
                    {
                        GridViewUsers.FooterRow.Cells[8].Enabled = false;

                    }

                }
                else
                {
                    if (IndexPage == T)
                    {
                        GridViewUsers.FooterRow.Cells[8].Enabled = false;

                    }

                }
            }
        }


        else
        {

            if (IndexPage == 1 && TotalUsers != 0)
            {
                GridViewUsers.FooterRow.Cells[7].Enabled = false;
            }
            else
            {


                int TU = (int)TotalUsers / 15;

                if (TU < TotalUsers / 15)
                {
                    if (IndexPage == TU + 1)
                    {
                        GridViewUsers.FooterRow.Cells[8].Enabled = false;


                    }

                }
                else
                {
                    if (IndexPage == TU)
                    {
                        GridViewUsers.FooterRow.Cells[8].Enabled = false;


                    }
                }
            }

        }

        // Fin -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
      
    
    }

    protected void Filtre(object sender, EventArgs e)
    {
        Session["userMatricule"] = TbMatricule.Text;
        Session["userNom"] = TbNom.Text;
        Session["userPrenom"] = TbPrenom.Text;
        Session["userNumBadge"] = TbBadge.Text;
        
       
        Session["userFlagAutorise"] = RbAutorise.SelectedValue;
        Session["userEnroler"] = RbEnroler.SelectedValue;
        Session["userEncoder"] = RbCoder.SelectedValue;


        if (TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || TbBadge.Text != "" || RbEnroler.SelectedValue != "-1" || RbAutorise.SelectedValue != "YN" || RbCoder.SelectedValue != "-1")
        {
            Session["IndexPageUser"] = 1;
            IndexPage = 1;       
        }

        if (Session["IndexPageUser"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageUser"]);
        }
        else
        {
            Session["IndexPageUser"] = 1;
            IndexPage = 1;

        }
        
        LaodGridView(IndexPage);
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
   
        Session["ChampsEnabled"] = true;
        Session["Ajout"] = true;
        Session["DetailsFor"] = "";
        Response.Redirect("~/Interfaces/User/Detail.aspx");
    
    }

    protected void BtnSet_Click(object sender, EventArgs e)
    {
        GridViewRow row = GridViewUsers.SelectedRow;
        Session["DetailsFor"] = (row.FindControl("LblMatricule") as Label).Text;
        Session["ChampsEnabled"] = true;
        Session["Ajout"] = false;
        Session["Rowselected"] = row;
        Response.Redirect("~/Interfaces/User/Detail.aspx");
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        GridViewRow row = GridViewUsers.SelectedRow;
       
        string matricule = (row.FindControl("LblMatricule") as Label).Text;
        int i = 0;
        i = user.SupprimerUnUser(matricule);

            if (i == 1)
            { 
                Response.Redirect("~/Interfaces/User/Users.aspx");
            }

    }

    protected void BtnUpdateLecteurNowClick(object sender, EventArgs e)
    {        
        GridViewRow row = GridViewUsers.SelectedRow;
        string matricule = (row.FindControl("LblMatricule") as Label).Text;

        int FlagUopdateOK = user.EnvoieAuLecteur(matricule);

       if (FlagUopdateOK == 1)
       {
           if (Session["IndexPageUser"] != null)
           {
               IndexPage = Convert.ToInt32(Session["IndexPageUser"]);
           }
           else
           {
               IndexPage = 1;
               Session["IndexPageUser"] = 1;
           }
           LaodGridView(IndexPage);
           GridViewUsers.SelectRow(row.RowIndex);
        
       }
    }

    protected void ImgVisualiser_Click(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Visualiser")
        {
            GridViewRow selected = (GridViewRow)((Control)(e.CommandSource)).Parent.Parent;
            Session["DetailsFor"] = (selected.FindControl("LblMatricule") as Label).Text;
            Session["ChampsEnabled"] = false;
            Session["Ajout"] = false;
            Session["Rowselected"] = selected;
            Response.Redirect("~/Interfaces/User/Detail.aspx");
        }
    }

    protected void GridViewUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GridViewUsers_IndexChanging(object sender, GridViewPageEventArgs e)
    {
        
        GridViewUsers.PageIndex = e.NewPageIndex;
        GridViewUsers.DataSource = Session["ListUsers"] as List<User>;
        GridViewUsers.DataBind();

    }

    protected void CbFiltre_CheckedChanged(object sender, EventArgs e)
    {
        Panelfilter.Visible = CbFiltre.Checked;

    }

    protected void GridViewUsers_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow row = GridViewUsers.Rows[e.NewSelectedIndex];
      
        string FlagAutorise = (row.FindControl("Selected") as Label).Text;

        BtnDelete.Visible = true;
        BtnSet.Visible = true;

        BtAutoriser.Visible = (FlagAutorise != "Y");
        BtInterdire.Visible = (FlagAutorise == "Y");

        BtnUpdateLecteurNow.Visible = true;
      
    }

    protected void GridViewUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            e.Row.Attributes.Add("OnClick", String.Format("javascript:__doPostBack('ctl00$MainContent$GridViewUsers','Select${0}')", e.Row.RowIndex));

            if (Convert.ToInt32(Session["IndexPageUser"]) == 0)
            {
                Session["IndexPageUser"] = 1;
                IndexPage = Convert.ToInt32(Session["IndexPageUser"]);
            }

            if ((e.Row.Cells[4].FindControl("LblNBadge") as Label).Text == "0")
                (e.Row.Cells[4].FindControl("LblNBadge") as Label).Text = "";

            if ((e.Row.Cells[1].FindControl("Selected") as Label).Text == "Y")
            {
                e.Row.Cells[0].FindControl("ImgInvalide").Visible = false;
                e.Row.Cells[0].FindControl("ImgValide").Visible = true;
            }
            if ((e.Row.Cells[1].FindControl("Selected") as Label).Text == " ")
            {
                e.Row.Cells[0].FindControl("ImgInvalide").Visible = true;
                e.Row.Cells[0].FindControl("ImgValide").Visible = false;
            }

           
        }

       if (e.Row.RowType == DataControlRowType.Footer)
       {
           if (TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || TbBadge.Text != "" || RbEnroler.SelectedValue != "-1" || RbAutorise.SelectedValue != "YN" || RbCoder.SelectedValue != "-1")
           {
               int Q = Convert.ToInt32(Session["TotalUsersByFiltre"]) / 15;
               int R = Convert.ToInt32(Session["TotalUsersByFiltre"]) % 15;
               
               if (R > 0)
               {
                   Q = Q + 1;
                   e.Row.Cells[10].Text = "Page : " + Convert.ToInt32(Session["IndexPageUser"]) + " / " + Q + "";
               }
               else
               {
                   e.Row.Cells[10].Text = "Page : " + Convert.ToInt32(Session["IndexPageUser"]) + " / " + Q + "";
               }
           }
           else
           {
               int Q = Convert.ToInt32(Session["TotalUsers"]) / 15;
               int R = Convert.ToInt32(Session["TotalUsers"]) % 15;
             
               if (R > 0)
               {
                   Q = Q + 1;
                   e.Row.Cells[10].Text = "Page : " + Convert.ToInt32(Session["IndexPageUser"]) + " / " + Q  + "";
               }
               else
               {
                   e.Row.Cells[10].Text = "Page : " + Convert.ToInt32(Session["IndexPageUser"]) + " / " + Q + "";
               }

           }



 
       }

       
    }

    protected void BtAutoriser_Click(object sender, EventArgs e)
    {
        GridViewRow row = GridViewUsers.Rows[GridViewUsers.SelectedIndex];
        User user = new User();

        string matricule = (row.FindControl("LblMatricule") as Label).Text;
        int i = user.ModifierFlagAutorise("Y", matricule);

        if (i == 1)
        {
            if (Session["IndexPageUser"] != null)
            {
                IndexPage = Convert.ToInt32(Session["IndexPageUser"]);
            }
            else
            {
                IndexPage = 1;

            }

            LaodGridView(IndexPage);

            if (TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || TbBadge.Text != "" || RbEnroler.SelectedValue != "-1" || RbAutorise.SelectedValue != "YN" || RbCoder.SelectedValue != "-1")
            {

                int TotalRows = GridViewUsers.Rows.Count;

                if (TotalUsersByFiltre > 0)
                {
                    if (TotalRows >= 1)
                    {
                        if (row.RowIndex == TotalRows)
                        {
                            GridViewUsers.SelectRow(row.RowIndex - 1);
                        }
                        else if ((row.RowIndex < TotalRows) && (row.RowIndex >= 0))
                        {
                            GridViewUsers.SelectRow(row.RowIndex);
                        }

                    }
                    if (TotalRows == 0)
                    {
                        IndexPage = Convert.ToInt32(Session["IndexPageUser"]) - 1;
                        Session["IndexPageUser"] = IndexPage;
                        LaodGridView(IndexPage);
                        GridViewUsers.SelectRow(GridViewUsers.Rows.Count - 1);
                    }
                }

                else if (TotalUsersByFiltre == 0)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageUser"]);
                    Session["IndexPageUser"] = IndexPage;
                    LaodGridView(IndexPage);
                }
            }
       
        else
        { 
         int TotalRows = GridViewUsers.Rows.Count;
         if (TotalUsers > 0)
                {
                    if (TotalRows >= 1)
                    {
                        if (row.RowIndex == TotalRows)
                        {
                            GridViewUsers.SelectRow(row.RowIndex - 1);
                        }
                        else if ((row.RowIndex < TotalRows) && (row.RowIndex >= 0))
                        {
                            GridViewUsers.SelectRow(row.RowIndex);
                        }

                    }
                    if (TotalRows == 0)
                    {
                        IndexPage = Convert.ToInt32(Session["IndexPageUser"]) - 1;
                        Session["IndexPageUser"] = IndexPage;
                        LaodGridView(IndexPage);
                        GridViewUsers.SelectRow(GridViewUsers.Rows.Count - 1);
                    }
                }

                else if (TotalUsers == 0)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageUser"]);
                    Session["IndexPageUser"] = IndexPage;
                    LaodGridView(IndexPage);
                }
            }
        }


    }

    protected void BtInterdir_Click(object sender, EventArgs e)
    {
        GridViewRow row = GridViewUsers.Rows[GridViewUsers.SelectedIndex];
        User user = new User();

        string matricule = (row.FindControl("LblMatricule") as Label).Text;

        int i = user.ModifierFlagAutorise(" ", matricule);

        if (i == 1)
        {
            if (Session["IndexPageUser"] != null)
            {
                IndexPage = Convert.ToInt32(Session["IndexPageUser"]);
            }
            else
            {
                IndexPage = 1;

            }

            LaodGridView(IndexPage);

            if (TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || TbBadge.Text != "" || RbEnroler.SelectedValue != "-1" || RbAutorise.SelectedValue != "YN" || RbCoder.SelectedValue != "-1")
            {

                int TotalRows = GridViewUsers.Rows.Count;

                if (TotalUsersByFiltre > 0)
                {
                    if (TotalRows >= 1)
                    {
                        if (row.RowIndex == TotalRows)
                        {
                            GridViewUsers.SelectRow(row.RowIndex - 1);
                        }
                        else if ((row.RowIndex < TotalRows) && (row.RowIndex >= 0))
                        {
                            GridViewUsers.SelectRow(row.RowIndex);
                        }

                    }
                    if (TotalRows == 0)
                    {
                        IndexPage = Convert.ToInt32(Session["IndexPageUser"]) - 1;
                        Session["IndexPageUser"] = IndexPage;
                        LaodGridView(IndexPage);
                        GridViewUsers.SelectRow(GridViewUsers.Rows.Count - 1);
                    }
                }

                else if (TotalUsersByFiltre == 0)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageUser"]);
                    Session["IndexPageUser"] = IndexPage;
                    LaodGridView(IndexPage);
                }
            }

            else
            {
                int TotalRows = GridViewUsers.Rows.Count;
                if (TotalUsers > 0)
                {
                    if (TotalRows >= 1)
                    {
                        if (row.RowIndex == TotalRows)
                        {
                            GridViewUsers.SelectRow(row.RowIndex - 1);
                        }
                        else if ((row.RowIndex < TotalRows) && (row.RowIndex >= 0))
                        {
                            GridViewUsers.SelectRow(row.RowIndex);
                        }

                    }
                    if (TotalRows == 0)
                    {
                        IndexPage = Convert.ToInt32(Session["IndexPageUser"]) - 1;
                        Session["IndexPageUser"] = IndexPage;
                        LaodGridView(IndexPage);
                        GridViewUsers.SelectRow(GridViewUsers.Rows.Count - 1);
                    }
                }

                else if (TotalUsers == 0)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageUser"]);
                    Session["IndexPageUser"] = IndexPage;
                    LaodGridView(IndexPage);
                }
            }
        }


    }

    protected void BtnLast_Click(object sender, EventArgs e)
    {
        if (TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || TbBadge.Text != "" || RbEnroler.SelectedValue != "-1" || RbAutorise.SelectedValue != "YN" || RbCoder.SelectedValue != "-1")
        { 

           
             TotalUsersByFiltre = Convert.ToInt32(Session["TotalUsersByFiltre"]);
             int T = (int) TotalUsersByFiltre / 15;
             if (T < TotalUsersByFiltre / 15)
             {
                 Session["IndexPageUser"] = ((int)TotalUsersByFiltre / 15) + 1;
             }
             else 
             {
                 Session["IndexPageUser"] = ((int)TotalUsersByFiltre / 15);
             }
        }
        else
         {
             TotalUsers = Convert.ToInt32(Session["TotalUsers"]);
             int T = (int)TotalUsers / 15;
             if (T < TotalUsers / 15)
             {
                 Session["IndexPageUser"] = ((int)TotalUsers / 15) + 1;
             }
             else
             {
                 Session["IndexPageUser"] = ((int)TotalUsers / 15);
             }

           
        }


         IndexPage = Convert.ToInt32(Session["IndexPageUser"]);
         LaodGridView(IndexPage);
    }

    protected void BtnNext_Click(object sender, EventArgs e)
    {

       
        
            if (Session["IndexPageUser"] != null )
            {

             
                    Session["IndexPageUser"] = Convert.ToInt32(Session["IndexPageUser"]) + 1;
                    IndexPage = Convert.ToInt32(Session["IndexPageUser"]);
             

            }
            
            
            else
            {
                Session["IndexPageUser"] = 2;
                IndexPage = 2;
            }
            LaodGridView(IndexPage);

        
    }

    protected void BtnPrevious_Click(object sender, EventArgs e)
    {

            Session["IndexPageUser"] = Convert.ToInt32(Session["IndexPageUser"]) - 1;
            IndexPage = Convert.ToInt32(Session["IndexPageUser"]);
            LaodGridView(IndexPage);
        
    }

    protected void BtnFirst_Click(object sender, EventArgs e)
    {
        Session["IndexPageUser"] =  1;
        IndexPage = Convert.ToInt32(Session["IndexPageUser"]);
        LaodGridView(IndexPage);
    }

    protected void exportExcel_Click(object sender, EventArgs e)
    {

        Response.Clear();
        string FileName = "attachment;filename=" + "Liste_des_chauffeurs" + ".xls";
        Response.AddHeader("content-disposition", FileName);

        Response.ContentEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");

        Response.ContentType = "application/vnd.xlsx";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        HtmlForm form = new HtmlForm();
        Page mypage = new System.Web.UI.Page();

        GridViewUsers.AllowPaging = false;
        GridViewUsers.AllowSorting = false;
        GridViewUsers.ShowFooter = false;
       

        GridViewUsers.EditIndex = -1;

       

        GridViewUsers.Columns[0].Visible = false;
        GridViewUsers.Columns[1].Visible = false;

        string matricule = TbMatricule.Text;

        if (matricule == "")
        { matricule = null; }


        string nom = TbNom.Text;
        if (nom == "")
        { nom = null; }

        string prenom = TbPrenom.Text;
        if (prenom == "")
        { prenom = null; }

        string numPermis = TbBadge.Text;
        if (numPermis == "")
        { numPermis = null; }

        
        string rbautoriseValue = RbAutorise.SelectedValue;
        if (rbautoriseValue == "YN")
        {
            rbautoriseValue = null;
        }

        string rbenrolerValue = RbEnroler.SelectedValue;
        string rbCoderValue = RbCoder.SelectedValue;

        int? rbCoderValueInt = int.Parse(rbCoderValue);

        if (rbCoderValueInt == -1)
        {
            rbCoderValueInt = null;
        }

        if (rbCoderValue == "1")
        {
            rbCoderValueInt = 1;
        }
        if (rbCoderValue == "0")
        {
            rbCoderValueInt = 0;
        }




        GridViewUsers.DataSource = (new User()).getAllUsersByFiltre(matricule, nom,prenom, numPermis, rbautoriseValue, rbenrolerValue, rbCoderValueInt);    
        GridViewUsers.DataBind();

        PrepareGridViewForExport(GridViewUsers);

        mypage.Controls.Add(form);
        form.Controls.Add(GridViewUsers);

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

        GridViewUsers.DataSource = ListUsers;
        GridViewUsers.DataBind();
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


        if (TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "" || TbBadge.Text != "" || RbEnroler.SelectedValue != "-1" || RbAutorise.SelectedValue != "YN" || RbCoder.SelectedValue != "-1")
        {
            string matricule = TbMatricule.Text;

            if (matricule == "")
            { matricule = null; }


            string nom = TbNom.Text;
            if (nom == "")
            { nom = null; }

            string prenom = TbPrenom.Text;
            if (prenom == "")
            { prenom = null; }

            string numPermis = TbBadge.Text;
            if (numPermis == "")
            { numPermis = null; }

            
            string rbautoriseValue = RbAutorise.SelectedValue;
            if (rbautoriseValue == "YN")
            {
                rbautoriseValue = null;
            }

            string rbenrolerValue = RbEnroler.SelectedValue;
            string rbCoderValue = RbCoder.SelectedValue;

            int? rbCoderValueInt = int.Parse(rbCoderValue);

            if (rbCoderValueInt == -1)
            {
                rbCoderValueInt = null;
            }

            if (rbCoderValue == "1")
            {
                rbCoderValueInt = 1;
            }
            if (rbCoderValue == "0")
            {
                rbCoderValueInt = 0;
            }

            ListUsers = user.getAllUsersByFiltre(matricule, nom,prenom, numPermis, rbautoriseValue, rbenrolerValue, rbCoderValueInt);

        }
        else
        {
            ListUsers = user.getAllUsers();
        }

        switch (sortExp)
        {
            case "Matricule":
                if (sortDir == "desc")
                    ListUsers = ListUsers.OrderByDescending(o => o.Matricule).ToList();
                else
                    ListUsers = ListUsers.OrderBy(o => o.Matricule).ToList();
                break;

            case "Nom":
                if (sortDir == "desc")
                    ListUsers = ListUsers.OrderByDescending(o => o.Nom).ToList();
                else
                    ListUsers = ListUsers.OrderBy(o => o.Nom).ToList();
                break;

            case "Prenom":
                if (sortDir == "desc")
                    ListUsers = ListUsers.OrderByDescending(o => o.Prenom).ToList();
                else
                    ListUsers = ListUsers.OrderBy(o => o.Prenom).ToList();
                break;

            case "NumBadge":
                foreach (User user in ListUsers)
                {
                    if (string.IsNullOrWhiteSpace(user.NumBadge) || string.IsNullOrEmpty(user.NumBadge))
                        user.NumBadge = "0";
                }
                if (sortDir == "desc")
                    ListUsers = ListUsers.OrderByDescending(o => int.Parse(o.NumBadge)).ToList();
                else
                    ListUsers = ListUsers.OrderBy(o => int.Parse(o.NumBadge)).ToList();
                break;

            case "DroitAcces":
                if (sortDir == "desc")
                    ListUsers = ListUsers.OrderByDescending(o => o.NumDroitAcces).ToList();
                else
                    ListUsers = ListUsers.OrderBy(o => o.NumDroitAcces).ToList();
                break;

            case "DateDebut":

                if (sortDir == "desc")
                    ListUsers = ListUsers.OrderByDescending(o => DateTime.Parse(o.DateDebut.ToString())).ToList();
                else
                    ListUsers = ListUsers.OrderBy(o => DateTime.Parse(o.DateDebut.ToString())).ToList();
                break;

            case "DateFin":

                if (sortDir == "desc")
                    ListUsers = ListUsers.OrderByDescending(o => DateTime.Parse(o.DateFin.ToString())).ToList();
                else
                    ListUsers = ListUsers.OrderBy(o => DateTime.Parse(o.DateFin.ToString())).ToList();
                break;

            case "Telephone":
                if (sortDir == "desc")
                    ListUsers = ListUsers.OrderByDescending(o => o.Telephone).ToList();
                else
                    ListUsers = ListUsers.OrderBy(o => o.Telephone).ToList();
                break;

            case "Ville":
                if (sortDir == "desc")
                    ListUsers = ListUsers.OrderByDescending(o => o.Ville).ToList();
                else
                    ListUsers = ListUsers.OrderBy(o => o.Ville).ToList();
                break;



        }

  

        if (Session["IndexPageUser"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageUser"]);
        }
        else
        {
            Session["IndexPageUser"] = 1;
            IndexPage = 1;

        }



        Session["ListUsers"] = ListUsers.Skip((Convert.ToInt32(Session["IndexPageUser"]) - 1) * 15).Take(15);

        GridViewUsers.DataSource = ListUsers.Skip((Convert.ToInt32(Session["IndexPageUser"]) - 1) * 15).Take(15);
        GridViewUsers.DataBind();
        FooterButtonGridViewUsers();


    }


    Image sortImage = new Image();
    public string sortOrder
    {


        get
        {
            // photo_user.Visible = false;
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


    protected void Users_Sorting(object sender, GridViewSortEventArgs e)
    {


        BindGridView(e.SortExpression, sortOrder);


        int columnIndex = 0;

        foreach (DataControlFieldHeaderCell headerCell in GridViewUsers.HeaderRow.Cells)
        {
            if (headerCell.ContainingField.SortExpression == e.SortExpression)
            {
                columnIndex = GridViewUsers.HeaderRow.Cells.GetCellIndex(headerCell);
            }
        }

        GridViewUsers.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);

      
    }

    //-------------------------------------------------------------------------------------------


}
