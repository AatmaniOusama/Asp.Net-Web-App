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





public partial class Agent_index : System.Web.UI.Page
{
    int IndexPage;
    double TotalAgents;
    double TotalAgentsByFiltre;

    Visiteur agent = new Visiteur();
   
     List<Visiteur> ListAgents;
    

    Service service = new Service();
    Events events = new Events();
    Listes listes = new Listes();

   

    protected void Page_Load(object sender, EventArgs e)
    {
        

        try
        {
             TotalAgents = agent.getTotalVisiteurs();
             Session["TotalAgents"] = TotalAgents;

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

                BarreControle.Rows[0].Cells[2].Visible = true; // Exporter vers Excel
                BarreControle.Rows[0].Cells[3].Visible = false; // Ajouter Agent
                BarreControle.Rows[0].Cells[4].Visible = false; // Modifier Agent 
                BarreControle.Rows[0].Cells[5].Visible = false; // Supprimer Agent
                BarreControle.Rows[0].Cells[6].Visible = false; // Autoriser / Interdire Agent
                BarreControle.Rows[0].Cells[7].Visible = false; // Update Borne Now
            }
            if (operateur.Profil.ToUpper() == "CONSULTANT")
            {

                BarreControle.Rows[0].Cells[2].Visible = true;
                BarreControle.Rows[0].Cells[3].Visible = false;
                BarreControle.Rows[0].Cells[4].Visible = false;
                BarreControle.Rows[0].Cells[5].Visible = false;
                BarreControle.Rows[0].Cells[6].Visible = false;
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

               
             

              
                

                TbMatricule.Text = Session["agentMatricule"] as string;
                TbNom.Text= Session["agentNom"] as string;
             

               

                if (!string.IsNullOrEmpty(Session["agentFlagAutorise"] as string))
                RbAutorise.SelectedValue = Session["agentFlagAutorise"] as string;

                if (!string.IsNullOrEmpty(Session["agentEnroler"] as string))
                    RbEnroler.SelectedValue = Session["agentEnroler"] as string;

               


                if (Session["IndexPageAgent"] != null)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageAgent"]);
               
                }
                else
                {
                    Session["IndexPageAgent"] = 1;
                    IndexPage = 1;
                }



                LaodGridView(IndexPage);

               
               
            }
        }
        catch (Exception)
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
       
        string rbautoriseValue = RbAutorise.SelectedValue;
        if (rbautoriseValue == "YN")
        {
            rbautoriseValue = null;
        }

        string rbenrolerValue = RbEnroler.SelectedValue;
      

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        // Verifier si l'un des champs des filtre  remplis ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        if (TbMatricule.Text != "" || TbNom.Text != "" || (RbAutorise.SelectedValue == "Y" || RbAutorise.SelectedValue == " ") || (RbEnroler.SelectedValue == "1" || RbEnroler.SelectedValue == "0"))
        {                       
                TotalAgentsByFiltre = agent.getTotalVisiteursByFiltre(matricule, nom, rbautoriseValue,rbenrolerValue);  // getTotalVisiteursByFiltre : donne le nombre des agents Enrolé et Non Enrolé
                Session["TotalAgentsByFiltre"] = TotalAgentsByFiltre;
                NbrLignes.Text = TotalAgentsByFiltre.ToString();
                      
        }

        else
        {                        
            NbrLignes.Text = TotalAgents.ToString();
        }


         // Fin Vérification -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

           
          ListAgents = agent.getVisiteurs(IndexPage, matricule, nom, rbautoriseValue, rbenrolerValue);
         
            Session["ListAgents"] = ListAgents;
            GridViewAgents.DataSource = ListAgents;
            GridViewAgents.DataBind();

            BtInterdire.Visible = false;
            BtAutoriser.Visible = false;
            BtnSet.Visible = false;
            BtnDelete.Visible = false;
            BtnUpdateLecteurNow.Visible = false;

            GridViewAgents.SelectedIndex = -1;


            FooterButtonGridViewAgents();
       
    }

    protected void FooterButtonGridViewAgents()
    {
        // Gérer l'affichage des bouttons Next previous... dans le footer -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        if ((ListAgents.Count() < 15 && TotalAgents < 15) || (ListAgents.Count() < 15 && Convert.ToInt32(Session["TotalAgentsByFiltre"]) < 15 && Convert.ToInt32(Session["TotalAgentsByFiltre"]) > 0) || TotalAgents == 15 || (Convert.ToInt32(Session["TotalAgentsByFiltre"]) == 15 && Convert.ToInt32(Session["TotalAgentsByFiltre"]) != 0))
        {
            GridViewAgents.FooterRow.Cells[4].Enabled = false;
            GridViewAgents.FooterRow.Cells[5].Enabled = false;

        }

        if (TbMatricule.Text != "" || TbNom.Text != "" || (RbAutorise.SelectedValue == "Y" || RbAutorise.SelectedValue == " ") || (RbEnroler.SelectedValue == "1" || RbEnroler.SelectedValue == "0"))
        {

            if (IndexPage == 1 && Convert.ToInt32(Session["TotalAgentsByFiltre"]) != 0)
            {
                GridViewAgents.FooterRow.Cells[4].Enabled = false;
            }

            else
            {

                TotalAgentsByFiltre = Convert.ToInt32(Session["TotalAgentsByFiltre"]);
                int T = (int)TotalAgentsByFiltre / 15;

                if (T < TotalAgentsByFiltre / 15)
                {
                    if (IndexPage == T + 1)
                    {
                        GridViewAgents.FooterRow.Cells[5].Enabled = false;

                    }

                }
                else
                {
                    if (IndexPage == T)
                    {
                        GridViewAgents.FooterRow.Cells[5].Enabled = false;

                    }

                }
            }
        }


        else
        {

            if (IndexPage == 1 && TotalAgents != 0)
            {
                GridViewAgents.FooterRow.Cells[4].Enabled = false;
            }
            else
            {


                int TU = (int)TotalAgents / 15;

                if (TU < TotalAgents / 15)
                {
                    if (IndexPage == TU + 1)
                    {
                        GridViewAgents.FooterRow.Cells[5].Enabled = false;


                    }

                }
                else
                {
                    if (IndexPage == TU)
                    {
                        GridViewAgents.FooterRow.Cells[5].Enabled = false;


                    }
                }
            }

        }

        // Fin -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    }

    protected void Filtre(object sender, EventArgs e)
    {
        Session["agentMatricule"] = TbMatricule.Text;
        Session["agentNom"] = TbNom.Text;
       
       
        Session["agentFlagAutorise"] = RbAutorise.SelectedValue;
        Session["agentEnroler"] = RbEnroler.SelectedValue;
        


        if (TbMatricule.Text != "" || TbNom.Text != "" || RbEnroler.SelectedValue != "-1" || RbAutorise.SelectedValue != "YN" )
        {
            Session["IndexPageAgent"] = 1;
            IndexPage = 1;
        
        }

        if (Session["IndexPageAgent"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageAgent"]);

        }
        else
        {
            IndexPage = 1;
           
        }
      
        
        LaodGridView(IndexPage);
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
   
        Session["ChampsEnabledAgent"] = true;
        Session["AjoutAgent"] = true;
        Session["DetailsForAgent"] = "";
        Response.Redirect("~/Interfaces/Agent/DetailAgent.aspx");
    }

    protected void BtnSet_Click(object sender, EventArgs e)
    {
        GridViewRow row = GridViewAgents.SelectedRow;
        Session["DetailsForAgent"] = (row.FindControl("LblMatricule") as Label).Text;
        Session["ChampsEnabledAgent"] = true;
        Session["AjoutAgent"] = false;
        Response.Redirect("~/Interfaces/Agent/DetailAgent.aspx");
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        //string dd = (GridViewAgents.SelectedRow.FindControl("LblMatricule") as Label).Text;
        GridViewRow row = GridViewAgents.SelectedRow;
        string matricule = (row.FindControl("LblMatricule") as Label).Text;
        int i = 0;

        i = agent.SupprimerUnVisiteur(matricule);

            if (i == 1)
            {
                Response.Redirect("~/Interfaces/Agent/Agents.aspx");
            }


           // BtnDelete.Visible = false;

    }

    protected void BtnUpdateLecteurNowClick(object sender, EventArgs e)
    {        
        GridViewRow row = GridViewAgents.SelectedRow;
        string matricule = (row.FindControl("LblMatricule") as Label).Text;

        int FlagUopdateOK = agent.EnvoieAuLecteur(matricule);

       if (FlagUopdateOK == 1)
       {
           if (Session["IndexPageAgent"] != null)
           {
               IndexPage = Convert.ToInt32(Session["IndexPageAgent"]);
           }
           else
           {
               IndexPage = 1;

           }
           LaodGridView(IndexPage);
          
       }
    }

    protected void ImgVisualiser_Click(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Visualiser")
        {
            // Get the row selected and its index
            GridViewRow selected = (GridViewRow)((Control)(e.CommandSource)).Parent.Parent;

           // int index = selected.RowIndex;
           // Session["RowIndex"] = index;

            Session["DetailsForAgent"] = (selected.FindControl("LblMatricule") as Label).Text;
            Session["ChampsEnabledAgent"] = false;
            Session["AjoutAgent"] = false;


          
            Response.Redirect("~/Interfaces/Agent/DetailAgent.aspx");
        }
    }
   
    protected void GridViewAgents_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GridViewAgents_IndexChanging(object sender, GridViewPageEventArgs e)
    {
        
        GridViewAgents.PageIndex = e.NewPageIndex;
        GridViewAgents.DataSource = Session["ListAgents"] as List<Visiteur>;
        GridViewAgents.DataBind();
        //LaodGridView();

    }

    protected void CbFiltre_CheckedChanged(object sender, EventArgs e)
    {
        Panelfilter.Visible = CbFiltre.Checked;

    }

    protected void GridViewAgents_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow row = GridViewAgents.Rows[e.NewSelectedIndex];
      
        string FlagAutorise = (row.FindControl("Selected") as Label).Text;

        BtnDelete.Visible = true;
        BtnSet.Visible = true;

        BtAutoriser.Visible = (FlagAutorise != "Y");
        BtInterdire.Visible = (FlagAutorise == "Y");

        BtnUpdateLecteurNow.Visible = true;

        
      
    }

    protected void GridViewAgents_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
            e.Row.Attributes.Add("OnClick", String.Format("javascript:__doPostBack('ctl00$MainContent$GridViewAgents','Select${0}')", e.Row.RowIndex));

            if (Convert.ToInt32(Session["IndexPageAgent"]) == 0)
            {
                Session["IndexPageAgent"] = 1;
                IndexPage = Convert.ToInt32(Session["IndexPageAgent"]);
            }

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
       


        if (TbMatricule.Text != "" || TbNom.Text != "" || RbEnroler.SelectedValue != "-1" || RbAutorise.SelectedValue != "YN")
        {
            int Q = Convert.ToInt32(Session["TotalAgentsByFiltre"]) / 15;
            int R = Convert.ToInt32(Session["TotalAgentsByFiltre"]) % 15;

            if (R > 0)
            {
                Q = Q + 1;
                e.Row.Cells[6].Text = "Page : " + Convert.ToInt32(Session["IndexPageAgent"]) + " / " + Q + "";
            }
            else
            {
                e.Row.Cells[6].Text = "Page : " + Convert.ToInt32(Session["IndexPageAgent"]) + " / " + Q + "";
            }
        }
        else
        {
            int Q = Convert.ToInt32(Session["TotalAgents"]) / 15;
            int R = Convert.ToInt32(Session["TotalAgents"]) % 15;

            if (R > 0)
            {
                Q = Q + 1;
                e.Row.Cells[6].Text = "Page : " + Convert.ToInt32(Session["IndexPageAgent"]) + " / " + Q + "";
            }
            else
            {
                e.Row.Cells[6].Text = "Page : " + Convert.ToInt32(Session["IndexPageAgent"]) + " / " + Q + "";
            }

        }
       
       }

       
    }

    protected void BtAutoriser_Click(object sender, EventArgs e)
    {
        
      
        GridViewRow row = GridViewAgents.Rows[GridViewAgents.SelectedIndex];
        Visiteur agent = new Visiteur();

        string matricule = (row.FindControl("LblMatricule") as Label).Text;
        int i = agent.ModifierFlagAutorise("Y", matricule);

        if (i == 1)
        {
            if (Session["IndexPageAgent"] != null)
            {
                IndexPage = Convert.ToInt32(Session["IndexPageAgent"]);
            }
            else
            {
                IndexPage = 1;

            }

            LaodGridView(IndexPage);

            if (TbMatricule.Text != "" || TbNom.Text != "" || RbEnroler.SelectedValue != "-1" || RbAutorise.SelectedValue != "YN")
            { 

                int TotalRows = GridViewAgents.Rows.Count;

                if (TotalAgentsByFiltre > 0)
                {
                    if (TotalRows >= 1)
                    {
                        if (row.RowIndex == TotalRows)
                        {
                            GridViewAgents.SelectRow(row.RowIndex - 1);
                        }
                        else if ((row.RowIndex < TotalRows) && (row.RowIndex >= 0))
                        {
                            GridViewAgents.SelectRow(row.RowIndex);
                        }

                    }
                    if (TotalRows == 0)
                    {
                        IndexPage = Convert.ToInt32(Session["IndexPageAgent"]) - 1;
                        Session["IndexPageAgent"] = IndexPage;
                        LaodGridView(IndexPage);
                        GridViewAgents.SelectRow(GridViewAgents.Rows.Count - 1);
                    }
                }

                else if (TotalAgentsByFiltre == 0)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageAgent"]);
                    Session["IndexPageAgent"] = IndexPage;
                    LaodGridView(IndexPage);
                }
            }

            else
            {
                int TotalRows = GridViewAgents.Rows.Count;
                if (TotalAgents > 0)
                {
                    if (TotalRows >= 1)
                    {
                        if (row.RowIndex == TotalRows)
                        {
                            GridViewAgents.SelectRow(row.RowIndex - 1);
                        }
                        else if ((row.RowIndex < TotalRows) && (row.RowIndex >= 0))
                        {
                            GridViewAgents.SelectRow(row.RowIndex);
                        }

                    }
                    if (TotalRows == 0)
                    {
                        IndexPage = Convert.ToInt32(Session["IndexPageAgent"]) - 1;
                        Session["IndexPageAgent"] = IndexPage;
                        LaodGridView(IndexPage);
                        GridViewAgents.SelectRow(GridViewAgents.Rows.Count - 1);
                    }
                }

                else if (TotalAgents == 0)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageAgent"]);
                    Session["IndexPageAgent"] = IndexPage;
                    LaodGridView(IndexPage);
                }
            }
        }






    }

    protected void BtInterdir_Click(object sender, EventArgs e)
    {
        GridViewRow row = GridViewAgents.Rows[GridViewAgents.SelectedIndex];
        Visiteur agent = new Visiteur();

        string matricule = (row.FindControl("LblMatricule") as Label).Text;
        int i = agent.ModifierFlagAutorise(" ", matricule);

        if (i == 1)
        {
            if (Session["IndexPageAgent"] != null)
            {
                IndexPage = Convert.ToInt32(Session["IndexPageAgent"]);
            }
            else
            {
                IndexPage = 1;

            }

            LaodGridView(IndexPage);

            if (TbMatricule.Text != "" || TbNom.Text != "" || RbEnroler.SelectedValue != "-1" || RbAutorise.SelectedValue != "YN")
            {

                int TotalRows = GridViewAgents.Rows.Count;

                if (TotalAgentsByFiltre > 0)
                {
                    if (TotalRows >= 1)
                    {
                        if (row.RowIndex == TotalRows)
                        {
                            GridViewAgents.SelectRow(row.RowIndex - 1);
                        }
                        else if ((row.RowIndex < TotalRows) && (row.RowIndex >= 0))
                        {
                            GridViewAgents.SelectRow(row.RowIndex);
                        }

                    }
                    if (TotalRows == 0)
                    {
                        IndexPage = Convert.ToInt32(Session["IndexPageAgent"]) - 1;
                        Session["IndexPageAgent"] = IndexPage;
                        LaodGridView(IndexPage);
                        GridViewAgents.SelectRow(GridViewAgents.Rows.Count - 1);
                    }
                }

                else if (TotalAgentsByFiltre == 0)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageAgent"]);
                    Session["IndexPageAgent"] = IndexPage;
                    LaodGridView(IndexPage);
                }
            }

            else
            {
                int TotalRows = GridViewAgents.Rows.Count;
                if (TotalAgents > 0)
                {
                    if (TotalRows >= 1)
                    {
                        if (row.RowIndex == TotalRows)
                        {
                            GridViewAgents.SelectRow(row.RowIndex - 1);
                        }
                        else if ((row.RowIndex < TotalRows) && (row.RowIndex >= 0))
                        {
                            GridViewAgents.SelectRow(row.RowIndex);
                        }

                    }
                    if (TotalRows == 0)
                    {
                        IndexPage = Convert.ToInt32(Session["IndexPageAgent"]) - 1;
                        Session["IndexPageAgent"] = IndexPage;
                        LaodGridView(IndexPage);
                        GridViewAgents.SelectRow(GridViewAgents.Rows.Count - 1);
                    }
                }

                else if (TotalAgents == 0)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageAgent"]);
                    Session["IndexPageAgent"] = IndexPage;
                    LaodGridView(IndexPage);
                }
            }
        }



    }

    protected void BtnLast_Click(object sender, EventArgs e)
    {
         if (TbMatricule.Text != "" || TbNom.Text!= "" ||  RbEnroler.SelectedValue != "-1" || RbAutorise.SelectedValue !="YN" )
        { 

           
             TotalAgentsByFiltre = Convert.ToInt32(Session["TotalAgentsByFiltre"]);
             int T = (int) TotalAgentsByFiltre / 15;
             if (T < TotalAgentsByFiltre / 15)
             {
                 Session["IndexPageAgent"] = ((int)TotalAgentsByFiltre / 15) + 1;
             }
             else 
             {
                 Session["IndexPageAgent"] = ((int)TotalAgentsByFiltre / 15);
             }
        }
        else
         {
             TotalAgents = Convert.ToInt32(Session["TotalAgents"]);
             int T = (int)TotalAgents / 15;
             if (T < TotalAgents / 15)
             {
                 Session["IndexPageAgent"] = ((int)TotalAgents / 15) + 1;
             }
             else
             {
                 Session["IndexPageAgent"] = ((int)TotalAgents / 15);
             }
             
        }

         IndexPage = Convert.ToInt32(Session["IndexPageAgent"]);
         LaodGridView(IndexPage);
    }


    protected void BtnNext_Click(object sender, EventArgs e)
    {

       
        
            if (Session["IndexPageAgent"] != null )
            {

             
                    Session["IndexPageAgent"] = Convert.ToInt32(Session["IndexPageAgent"]) + 1;
                    IndexPage = Convert.ToInt32(Session["IndexPageAgent"]);
             

            }
            
            
            else
            {
                Session["IndexPageAgent"] = 2;
                IndexPage = 2;
            }
            LaodGridView(IndexPage);

        
    }

    protected void BtnPrevious_Click(object sender, EventArgs e)
    {

            Session["IndexPageAgent"] = Convert.ToInt32(Session["IndexPageAgent"]) - 1;
            IndexPage = Convert.ToInt32(Session["IndexPageAgent"]);
            LaodGridView(IndexPage);
        
    }

    protected void BtnFirst_Click(object sender, EventArgs e)
    {
        Session["IndexPageAgent"] =  1;
        IndexPage = Convert.ToInt32(Session["IndexPageAgent"]);
        LaodGridView(IndexPage);
    }

    protected void exportExcel_Click(object sender, EventArgs e)
    {

        Response.Clear();
        string FileName = "attachment;filename=" + "Liste_des_Agents" + ".xls";
        Response.AddHeader("content-disposition", FileName);
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
        Response.ContentType = "application/vnd.xlsx";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        HtmlForm form = new HtmlForm();
        Page mypage = new System.Web.UI.Page();

        GridViewAgents.AllowPaging = false;
        GridViewAgents.AllowSorting = false;
        GridViewAgents.ShowFooter = false;

        GridViewAgents.EditIndex = -1;

      
       

        GridViewAgents.Columns[0].Visible = false;

        string matricule = TbMatricule.Text;

        if (matricule == "")
        { matricule = null; }


        string nom = TbNom.Text;
        if (nom == "")
        { nom = null; }
      
        string rbautoriseValue = RbAutorise.SelectedValue;
        if (rbautoriseValue == "YN")
        {
            rbautoriseValue = null;
        }

        string rbenrolerValue = RbEnroler.SelectedValue;
      



        GridViewAgents.DataSource = (new Visiteur()).getVisiteursToExport(matricule, nom, rbautoriseValue,rbenrolerValue, "Matricule");    
        GridViewAgents.DataBind();

      

        PrepareGridViewForExport(GridViewAgents);

        mypage.Controls.Add(form);
        form.Controls.Add(GridViewAgents);
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


        GridViewAgents.DataSource = ListAgents;
        GridViewAgents.DataBind();
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
        string matricule = TbMatricule.Text;

        if (matricule == "")
        { matricule = null; }


        string nom = TbNom.Text;
        if (nom == "")
        { nom = null; }
        
        string rbautoriseValue = RbAutorise.SelectedValue;
        if (rbautoriseValue == "YN")
        {
            rbautoriseValue = null;
        }

        string rbenrolerValue = RbEnroler.SelectedValue;
        


        ListAgents = Session["ListAgents"] as List<Visiteur>;

        if (Session["IndexPageAgent"] != null)
        {


            Session["IndexPageAgent"] = Convert.ToInt32(Session["IndexPageAgent"]) ;

            IndexPage = Convert.ToInt32(Session["IndexPageAgent"]);


        }


        else
        {
            Session["IndexPageAgent"] = 1;
            IndexPage = 1;
        }


       

        ListAgents = agent.getVisiteurs(IndexPage, matricule, nom, rbautoriseValue,rbenrolerValue, sortExp, sortDir);


        Session["ListAgents"] = ListAgents;

        GridViewAgents.DataSource = ListAgents;
        GridViewAgents.DataBind();

        // Gérer l'affichage des bouttons Next previous... dans le footer -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        if (((ListAgents.Count() < 15 && TotalAgents < 15) || (ListAgents.Count() < 15 && Convert.ToInt32(Session["TotalAgentsByFiltre"]) < 15) || TotalAgents == 15 || Convert.ToInt32(Session["TotalAgentsByFiltre"]) == 15) && Convert.ToInt32(Session["TotalAgentsByFiltre"]) != 0)
        {
            GridViewAgents.FooterRow.Cells[4].Enabled = false;
            GridViewAgents.FooterRow.Cells[5].Enabled = false;

        }


        if (IndexPage == 1 && Convert.ToInt32(Session["TotalAgentsByFiltre"]) != 0)
        {
            GridViewAgents.FooterRow.Cells[5].Enabled = false;
        }
        else
        {

            TotalAgentsByFiltre = Convert.ToInt32(Session["TotalAgentsByFiltre"]);
            int T = (int)TotalAgentsByFiltre / 15;
            if (T < TotalAgentsByFiltre / 15)
            {
                if (IndexPage == (Convert.ToInt32(Session["TotalAgentsByFiltre"]) / 15) + 1 || IndexPage == (TotalAgents / 15) + 1)
                {
                    GridViewAgents.FooterRow.Cells[4].Enabled = false;
                }

            }
            else
            {
                if (IndexPage == (Convert.ToInt32(Session["TotalAgentsByFiltre"]) / 15) || IndexPage == (TotalAgents / 15))
                {
                    GridViewAgents.FooterRow.Cells[4].Enabled = false;
                }
            }
        }


        // Fin -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
      
       
        
    }


    Image sortImage = new Image();
    public string sortOrder
    {
    

        get
        {
          // photo_agent.Visible = false;
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


    protected void agents_Sorting(object sender, GridViewSortEventArgs e)
    {

        
        BindGridView(e.SortExpression, sortOrder);
        

        int columnIndex = 0;

        foreach (DataControlFieldHeaderCell headerCell in GridViewAgents.HeaderRow.Cells)
        {
            if (headerCell.ContainingField.SortExpression == e.SortExpression)
            {
                columnIndex = GridViewAgents.HeaderRow.Cells.GetCellIndex(headerCell);
            }
        }

        GridViewAgents.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);


    }   

//-------------------------------------------------------------------------------------------

}
