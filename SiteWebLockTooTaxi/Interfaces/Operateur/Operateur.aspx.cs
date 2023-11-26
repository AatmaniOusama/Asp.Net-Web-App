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




public partial class Interfaces_Operateur : System.Web.UI.Page
{
    int    IndexPage;
    double TotalOperateurs;
    double TotalOperateursByFiltre;


    Operateur    Operateur    = new Operateur();
    Service service = new Service();
    List<Operateur>   ListOperateurs;
   

    protected void Page_Load(object sender, EventArgs e)
    {
        

        try
        {
             TotalOperateurs = Operateur.getTotalOperateurs();
             Session["TotalOperateurs"] = TotalOperateurs;

            TbNom.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbPrenom.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");

            
          

            Operateur operateur = (Operateur) Session["Operateur"];

            if (operateur.Profil.ToUpper() == "ADMINISTRATEUR")
          
            {
                
                //BarreControle.Rows[0].Cells[2].Visible = true;
                //BarreControle.Rows[0].Cells[3].Visible = true;
                //BarreControle.Rows[0].Cells[4].Visible = true;
                //BarreControle.Rows[0].Cells[5].Visible = true;
                //BarreControle.Rows[0].Cells[6].Visible = true;
                //BarreControle.Rows[0].Cells[7].Visible = true;
            }

            if (operateur.Profil.ToUpper() == "CONSULTANT")
            {
             
                //BarreControle.Rows[0].Cells[2].Visible = false;
                //BarreControle.Rows[0].Cells[3].Visible = false;
                //BarreControle.Rows[0].Cells[4].Visible = false;
                //BarreControle.Rows[0].Cells[5].Visible = false;
                //BarreControle.Rows[0].Cells[6].Visible = false;
                //BarreControle.Rows[0].Cells[7].Visible = false;
            }

            if (operateur.MenusAutorises.Substring(0, 1) == "0")
            {
                Session.Abandon();
                Response.Redirect("~/");
            }


           
            if (!IsPostBack)
            {
                ViewState["sortOrder"] = "";


                DDLService.DataSource = service.GetServices();
                DDLService.DataValueField = "Numero";
                DDLService.DataTextField = "Libelle";
                DDLService.DataBind();



                TbNom.Text = Session["OperateurNom"] as string;
                TbPrenom.Text = Session["OperateurPrenom"] as string;
               
               

               

                if (!string.IsNullOrEmpty(Session["OperateurProfil"] as string))
                    DDLDroitPointage.SelectedValue = Session["OperateurProfil"] as string;
                
                if (!string.IsNullOrEmpty(Session["OperateurGroupe"] as string))
                    DDLService.SelectedValue = Session["OperateurGroupe"] as string;

               


                if (Session["IndexPageOperateur"] != null)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageOperateur"]);
               
                }
                else
                {
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
       
        string nom = TbNom.Text;
        if (nom == "")
        { nom = null; }

        string prenom = TbPrenom.Text;

        if (prenom == "")
        { prenom = null; }
       

        string ddlprofilValue = DDLDroitPointage.SelectedItem.Text;

        if (ddlprofilValue == "Tous")
        {
            ddlprofilValue = null;
        }

        string ddlserviceValue = DDLService.SelectedValue;

        if (ddlserviceValue == "0")
        {
            ddlserviceValue = null;
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        // Verifier si l'un des champs des filtre  remplis ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        if (nom != "" || prenom != "" || ddlserviceValue != "0" || ddlprofilValue != "0")
        {
            TotalOperateursByFiltre = Operateur.getTotalOperateursByFiltre(nom, prenom, ddlprofilValue, ddlserviceValue);  // getTotalOperateursByFiltre : donne le nombre des Operateurs 
                Session["TotalOperateursByFiltre"] = TotalOperateursByFiltre;
                NbrLignes.Text = TotalOperateursByFiltre.ToString();
                      
        }

        else
        {                        
            NbrLignes.Text = TotalOperateurs.ToString();
        }


         // Fin Vérification -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ListOperateurs = Operateur.GetOperateursByFiltre(IndexPage, nom, prenom, ddlprofilValue, ddlserviceValue, "Profil", "ASC");
         
            Session["ListOperateurs"] = ListOperateurs;
            GridViewOperateurs.DataSource = ListOperateurs;
            GridViewOperateurs.DataBind();


            BtnSet.Visible = false;
            BtnDelete.Visible = false;


            GridViewOperateurs.SelectedIndex = -1;


        // Gérer l'affichage des bouttons Next previous... dans le footer -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            if (((ListOperateurs.Count() < 15 && TotalOperateurs < 15) || (ListOperateurs.Count() < 15 && Convert.ToInt32(Session["TotalOperateursByFiltre"]) < 15) || TotalOperateurs == 15 || Convert.ToInt32(Session["TotalOperateursByFiltre"]) == 15) && Convert.ToInt32(Session["TotalOperateursByFiltre"]) !=0)
            {
                GridViewOperateurs.FooterRow.Cells[4].Enabled = false;
                GridViewOperateurs.FooterRow.Cells[3].Enabled = false;
               
            }


            if (IndexPage == 1 && Convert.ToInt32(Session["TotalOperateursByFiltre"]) != 0)
            {
                GridViewOperateurs.FooterRow.Cells[3].Enabled = false;
            }
            else {

                 TotalOperateursByFiltre = Convert.ToInt32(Session["TotalOperateursByFiltre"]);
                 int T = (int) TotalOperateursByFiltre / 15;
                 if (T < TotalOperateursByFiltre / 15)
                 {
                     if (IndexPage == (Convert.ToInt32(Session["TotalOperateursByFiltre"]) / 15) + 1 || IndexPage == (TotalOperateurs / 15) + 1)
                     {
                         GridViewOperateurs.FooterRow.Cells[4].Enabled = false;
                     }

                 }
                 else
                 {
                     if (IndexPage == (Convert.ToInt32(Session["TotalOperateursByFiltre"]) / 15)  || IndexPage == (TotalOperateurs / 15) )
                     {
                         GridViewOperateurs.FooterRow.Cells[4].Enabled = false;
                     }
                 }
             }
           
             
        // Fin -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
      
       
    }

    protected void Filtre(object sender, EventArgs e)
    {
        Session["OperateurNom"] = TbNom.Text;
        Session["OperateurPrenom"] = TbPrenom.Text;
        Session["OperateurProfil"] = DDLDroitPointage.SelectedValue;
        Session["OperateurGroupe"] = DDLService.SelectedValue;


            if (TbNom.Text != "" || TbPrenom.Text != "" || DDLDroitPointage.SelectedValue != "0" || DDLService.SelectedValue != "0" )
        {
            Session["IndexPageOperateur"] = 1;
            IndexPage = 1;       
        }

        if (Session["IndexPageOperateur"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageOperateur"]);
        }
        else
        {
            IndexPage = 1;

        }
        
        LaodGridView(IndexPage);
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Session["AjoutOperateur"] = true;      
        Response.Redirect("~/Interfaces/Operateur/DetailOperateur.aspx");
    }

    protected void BtnSet_Click(object sender, EventArgs e)
    {
        GridViewRow row = GridViewOperateurs.SelectedRow;
        int id = int.Parse((row.FindControl("Selected") as Label).Text);
        Session["ModifOperateur"] = id;
        Session["AjoutOperateur"] = false;
        Response.Redirect("~/Interfaces/Operateur/DetailOperateur.aspx");
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {

        GridViewRow row = GridViewOperateurs.SelectedRow;
        int id = int.Parse((row.FindControl("Selected") as Label).Text);
        int i = 0;

        i = Operateur.SupprimerUnOperateur(id);

        if (i == 1)
        {
            Response.Redirect("~/Interfaces/Operateur/Operateur.aspx");
        }
      
    }

    protected void ImgVisualiser_Click(object sender, GridViewCommandEventArgs e)
    {

        //if (e.CommandName == "Visualiser")
        //{
        //    // Get the row selected and its index
        //    GridViewRow selected = (GridViewRow)((Control)(e.CommandSource)).Parent.Parent;

        //    // int index = selected.RowIndex;
        //    // Session["RowIndex"] = index;

        //    Session["DetailsFor"] = (selected.FindControl("LblMatricule") as Label).Text;
        //    Session["ChampsEnabled"] = false;
        //    Session["Ajout"] = false;

        //    Response.Redirect("~/Interfaces/Operateur/Detail.aspx");
        //}
    }
    
    protected void GridViewOperateurs_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GridViewOperateurs_IndexChanging(object sender, GridViewPageEventArgs e)
    {
        
        GridViewOperateurs.PageIndex = e.NewPageIndex;
        GridViewOperateurs.DataSource = Session["ListOperateurs"] as List<Operateur>;
        GridViewOperateurs.DataBind();
    }

    protected void CbFiltre_CheckedChanged(object sender, EventArgs e)
    {
        Panelfilter.Visible = CbFiltre.Checked;

    }

    protected void GridViewOperateurs_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow row = GridViewOperateurs.Rows[e.NewSelectedIndex];
      
        string FlagAutorise = (row.FindControl("Selected") as Label).Text;



     //   photo_Operateur.ImageUrl = "~/Photo.ashx/?Id=" + (row.FindControl("LblMatricule") as Label).Text;

       // photo_Operateur.Visible = true;

        BtnDelete.Visible = true;
        BtnSet.Visible = true;

        GridViewOperateurs.FooterRow.Cells[6].Text = "Page : " + Convert.ToInt32(Session["IndexPageOperateur"]) +"";
      
    
    }

    protected void GridViewOperateurs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            e.Row.Attributes.Add("OnClick", String.Format("javascript:__doPostBack('ctl00$MainContent$GridViewOperateurs','Select${0}')", e.Row.RowIndex));

            
            if (Convert.ToInt32(Session["IndexPageOperateur"]) == 0)
            {
                Session["IndexPageOperateur"] = 1;
                IndexPage = Convert.ToInt32(Session["IndexPageOperateur"]);
            }
           
        }

       if (e.Row.RowType == DataControlRowType.Footer)
       { 
        e.Row.Cells[6].Text = "Page : "+IndexPage+"";
       }

       
    }

    protected void BtnLast_Click(object sender, EventArgs e)
    {
        if (TbNom.Text != "" || TbPrenom.Text != "" || DDLDroitPointage.SelectedValue != "0" || DDLService.SelectedValue != "0" )
        { 

           
             TotalOperateursByFiltre = Convert.ToInt32(Session["TotalOperateursByFiltre"]);
             int T = (int) TotalOperateursByFiltre / 15;
             if (T < TotalOperateursByFiltre / 15)
             {
                 Session["IndexPageOperateur"] = ((int)TotalOperateursByFiltre / 15) + 1;
             }
             else 
             {
                 Session["IndexPageOperateur"] = ((int)TotalOperateursByFiltre / 15);
             }
        }
        else
         {
             TotalOperateurs = Convert.ToInt32(Session["TotalOperateurs"]);
             int T = (int)TotalOperateurs / 15;
             if (T < TotalOperateurs / 15)
             {
                 Session["IndexPageOperateur"] = ((int)TotalOperateurs / 15) + 1;
             }
             else
             {
                 Session["IndexPageOperateur"] = ((int)TotalOperateurs / 15);
             }

        }

         IndexPage = Convert.ToInt32(Session["IndexPageOperateur"]);
         LaodGridView(IndexPage);
    }

    protected void BtnNext_Click(object sender, EventArgs e)
    {

       
        
            if (Session["IndexPageOperateur"] != null )
            {

             
                    Session["IndexPageOperateur"] = Convert.ToInt32(Session["IndexPageOperateur"]) + 1;
                    IndexPage = Convert.ToInt32(Session["IndexPageOperateur"]);
             

            }
            
            
            else
            {
                Session["IndexPageOperateur"] = 2;
                IndexPage = 2;
            }
            LaodGridView(IndexPage);

        
    }

    protected void BtnPrevious_Click(object sender, EventArgs e)
    {

            Session["IndexPageOperateur"] = Convert.ToInt32(Session["IndexPageOperateur"]) - 1;
            IndexPage = Convert.ToInt32(Session["IndexPageOperateur"]);
            LaodGridView(IndexPage);
        
    }

    protected void BtnFirst_Click(object sender, EventArgs e)
    {
        Session["IndexPageOperateur"] =  1;
        IndexPage = Convert.ToInt32(Session["IndexPageOperateur"]);
        LaodGridView(IndexPage);
    }

    protected void exportExcel_Click(object sender, EventArgs e)
    {

        Response.Clear();
        string FileName = "attachment;filename=" + "Liste_des_Operateurs" + ".xls";
        Response.AddHeader("content-disposition", FileName);

        Response.ContentEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");

        Response.ContentType = "application/vnd.xlsx";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        HtmlForm form = new HtmlForm();
        Page mypage = new System.Web.UI.Page();

        GridViewOperateurs.AllowPaging = false;
        GridViewOperateurs.AllowSorting = false;
        GridViewOperateurs.ShowFooter = false;
       

        GridViewOperateurs.EditIndex = -1;

       

        GridViewOperateurs.Columns[0].Visible = false;
        GridViewOperateurs.Columns[1].Visible = false;

        string nom = TbNom.Text;
        if (nom == "")
        { nom = null; }

        string prenom = TbPrenom.Text;

        if (prenom == "")
        { prenom = null; }


        string ddlprofilValue = DDLDroitPointage.SelectedValue;

        if (ddlprofilValue == "0")
        {
            ddlprofilValue = null;
        }

        string ddlserviceValue = DDLService.SelectedValue;

        if (ddlserviceValue == "0")
        {
            ddlserviceValue = null;
        }



        GridViewOperateurs.DataSource = (new Operateur()).GetOperateursToExport(nom, prenom, ddlprofilValue, ddlserviceValue,"Profil","ASC");    
        GridViewOperateurs.DataBind();

        PrepareGridViewForExport(GridViewOperateurs);

        mypage.Controls.Add(form);
        form.Controls.Add(GridViewOperateurs);

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

        GridViewOperateurs.DataSource = ListOperateurs;
        GridViewOperateurs.DataBind();
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
        string nom = TbNom.Text;
        if (nom == "")
        { nom = null; }

        string prenom = TbPrenom.Text;

        if (prenom == "")
        { prenom = null; }



        string ddlprofilValue = DDLDroitPointage.SelectedItem.Text;

        if (ddlprofilValue == "Tous")
        {
            ddlprofilValue = null;
        }

        string ddlserviceValue = DDLService.SelectedValue;

        if (ddlserviceValue == "0")
        {
            ddlserviceValue = null;
        }

     

        if (Session["IndexPageOperateur"] != null)
        {


            Session["IndexPageOperateur"] = Convert.ToInt32(Session["IndexPageOperateur"]) ;

            IndexPage = Convert.ToInt32(Session["IndexPageOperateur"]);


        }


        else
        {
            Session["IndexPageOperateur"] = 1;
            IndexPage = 1;
        }


        ListOperateurs = Operateur.GetOperateursByFiltre(IndexPage, nom, prenom, ddlprofilValue, ddlserviceValue, sortExp, sortDir);

        if (sortExp == "Debut")
        {
            if (sortDir == "asc")
                ListOperateurs = ListOperateurs.OrderBy(o => o.Debut).ToList();
            if (sortDir == "desc")
                ListOperateurs = ListOperateurs.OrderByDescending(o => o.Debut).ToList();
        }

        if (sortExp == "Fin")
        {
            if (sortDir == "asc")
                ListOperateurs = ListOperateurs.OrderBy(o => o.Fin).ToList();
            if (sortDir == "desc")
                ListOperateurs = ListOperateurs.OrderByDescending(o => o.Fin).ToList();
        }



          


        Session["ListOperateurs"] = ListOperateurs;

        GridViewOperateurs.DataSource = ListOperateurs;
        GridViewOperateurs.DataBind();


        // Gérer l'affichage des bouttons Next previous... dans le footer -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        if (((ListOperateurs.Count() < 15 && TotalOperateurs < 15) || (ListOperateurs.Count() < 15 && Convert.ToInt32(Session["TotalOperateursByFiltre"]) < 15) || TotalOperateurs == 15 || Convert.ToInt32(Session["TotalOperateursByFiltre"]) == 15) && Convert.ToInt32(Session["TotalOperateursByFiltre"]) != 0)
        {
            GridViewOperateurs.FooterRow.Cells[4].Enabled = false;
            GridViewOperateurs.FooterRow.Cells[3].Enabled = false;

        }


        if (IndexPage == 1 && Convert.ToInt32(Session["TotalOperateursByFiltre"]) != 0)
        {
            GridViewOperateurs.FooterRow.Cells[3].Enabled = false;
        }
        else
        {

            TotalOperateursByFiltre = Convert.ToInt32(Session["TotalOperateursByFiltre"]);
            int T = (int)TotalOperateursByFiltre / 15;
            if (T < TotalOperateursByFiltre / 15)
            {
                if (IndexPage == (Convert.ToInt32(Session["TotalOperateursByFiltre"]) / 15) + 1 || IndexPage == (TotalOperateurs / 15) + 1)
                {
                    GridViewOperateurs.FooterRow.Cells[4].Enabled = false;
                }

            }
            else
            {
                if (IndexPage == (Convert.ToInt32(Session["TotalOperateursByFiltre"]) / 15) || IndexPage == (TotalOperateurs / 15))
                {
                    GridViewOperateurs.FooterRow.Cells[4].Enabled = false;
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
          // photo_Operateur.Visible = false;
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


    protected void Operateurs_Sorting(object sender, GridViewSortEventArgs e)
    {

        
        BindGridView(e.SortExpression, sortOrder);
        

        int columnIndex = 0;

        foreach (DataControlFieldHeaderCell headerCell in GridViewOperateurs.HeaderRow.Cells)
        {
            if (headerCell.ContainingField.SortExpression == e.SortExpression)
            {
                columnIndex = GridViewOperateurs.HeaderRow.Cells.GetCellIndex(headerCell);
            }
        }

        GridViewOperateurs.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);

        //int nbrRows = GridViewOperateurs.Rows.Count;
        //int heightGrid = nbrRows * 25;

        //if (heightGrid <= 470 && nbrRows <= 100)
        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader2('" + GridViewOperateurs.ClientID + "', " + heightGrid + 25 + ", 950 , 40 ,false); </script>", false);

        //if (heightGrid >= 470 && nbrRows < 100)

        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GridViewOperateurs.ClientID + "', 470, 950 , 40 ,false); </script>", false);

        //if (heightGrid >= 470 && nbrRows >= 100)

        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GridViewOperateurs.ClientID + "', 470, 950 , 40 ,true); </script>", false);

    }   

//-------------------------------------------------------------------------------------------

}

