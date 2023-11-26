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




public partial class Interfaces_Operateur_LoginsOperateur : System.Web.UI.Page
{
    int    IndexPage;
    double TotalLoginsOperateurs;
    double TotalLoginsOperateursByFiltre;


    Log    login    = new Log();
   
  
    List<Log>   ListLoginsOperateurs;
   

    protected void Page_Load(object sender, EventArgs e)
    {
        

        try
        {
            

             TotalLoginsOperateurs = login.getTotalLoginsOperateurs();
             Session["TotalLoginsOperateurs"] = TotalLoginsOperateurs;

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
               



                if (Session["LoginOperateurDateDebut"] as DateTime? != null)
                    TbDateDebut.SelectedDate = Session["LoginOperateurDateDebut"] as DateTime?;
                else
                    TbDateDebut.SelectedDate = DateTime.Now.AddDays(-1);

                if (Session["LoginOperateurDateFin"] as DateTime? != null)
                    TbDateFin.SelectedDate = Session["LoginOperateurDateFin"] as DateTime?;
                else
                    TbDateFin.SelectedDate = DateTime.Now;


                TbNom.Text = Session["LoginOperateurNom"] as string;
                TbPrenom.Text = Session["LoginOperateurPrenom"] as string;

                if (!string.IsNullOrEmpty(Session["LoginOperateurProfil"] as string))
                    DDLDroitPointage.SelectedValue = Session["LoginOperateurProfil"] as string;
                
               
               


                if (Session["IndexPageLoginOperateur"] != null)
                {
                    IndexPage = Convert.ToInt32(Session["IndexPageLoginOperateur"]);
               
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

        DateTime debut = TbDateDebut.SelectedDate.Value;
        DateTime fin = TbDateFin.SelectedDate.Value;

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        // Verifier si l'un des champs des filtre  remplis ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------




        if (TbDateDebut.SelectedDate.ToString() != "" || TbDateFin.SelectedDate.ToString() != "" ||  nom != "" || prenom != "" || ddlprofilValue != "0")
        {
           
            TotalLoginsOperateursByFiltre = login.getTotalLoginsOperateursByFiltre(debut, fin,nom, prenom, ddlprofilValue);  // getTotalLoginsOperateursByFiltre : donne le nombre de fois des logins Operateurs 
                Session["TotalLoginsOperateursByFiltre"] = TotalLoginsOperateursByFiltre;
               
                      
        }

        

         // Fin Vérification -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ListLoginsOperateurs = login.GetLoginsOperateursByFiltre(IndexPage,debut, fin, nom, prenom, ddlprofilValue);
         
            Session["ListLoginsOperateurs"] = ListLoginsOperateurs;
            GridViewLoginsOperateurs.DataSource = ListLoginsOperateurs;
            GridViewLoginsOperateurs.DataBind();


          


            GridViewLoginsOperateurs.SelectedIndex = -1;


        // Gérer l'affichage des bouttons Next previous... dans le footer -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            if (((ListLoginsOperateurs.Count() < 15 && TotalLoginsOperateurs < 15) || (ListLoginsOperateurs.Count() < 15 && Convert.ToInt32(Session["TotalLoginsOperateursByFiltre"]) < 15) || TotalLoginsOperateurs == 15 || Convert.ToInt32(Session["TotalLoginsOperateursByFiltre"]) == 15) && Convert.ToInt32(Session["TotalLoginsOperateursByFiltre"]) !=0)
            {
                GridViewLoginsOperateurs.FooterRow.Cells[3].Enabled = false;
                GridViewLoginsOperateurs.FooterRow.Cells[2].Enabled = false;
               
            }


            if (IndexPage == 1 && Convert.ToInt32(Session["TotalLoginsOperateursByFiltre"]) != 0)
            {
                GridViewLoginsOperateurs.FooterRow.Cells[2].Enabled = false;
            }
            else {

                 TotalLoginsOperateursByFiltre = Convert.ToInt32(Session["TotalLoginsOperateursByFiltre"]);
                 int T = (int) TotalLoginsOperateursByFiltre / 15;
                 if (T < TotalLoginsOperateursByFiltre / 15)
                 {
                     if (IndexPage == (Convert.ToInt32(Session["TotalLoginsOperateursByFiltre"]) / 15) + 1 || IndexPage == (TotalLoginsOperateurs / 15) + 1)
                     {
                         GridViewLoginsOperateurs.FooterRow.Cells[3].Enabled = false;
                     }

                 }
                 else
                 {
                     if (IndexPage == (Convert.ToInt32(Session["TotalLoginsOperateursByFiltre"]) / 15)  || IndexPage == (TotalLoginsOperateurs / 15) )
                     {
                         GridViewLoginsOperateurs.FooterRow.Cells[3].Enabled = false;
                     }
                 }
             }
           
             
        // Fin -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
      
       
    }

    protected void Filtre(object sender, EventArgs e)
    {

        Session["LoginOperateurDateDebut"] = TbDateDebut.SelectedDate.Value;
        Session["LoginOperateurDateFin"] = TbDateFin.SelectedDate.Value;
        Session["LoginOperateurNom"] = TbNom.Text;
        Session["LoginOperateurPrenom"] = TbPrenom.Text;
        Session["LoginOperateurProfil"] = DDLDroitPointage.SelectedValue;



        if (TbDateDebut.SelectedDate.ToString() != "" || TbDateFin.SelectedDate.ToString() != "" || TbNom.Text != "" || TbPrenom.Text != "" || DDLDroitPointage.SelectedValue != "0")
        {
            Session["IndexPageLoginOperateur"] = 1;
            IndexPage = 1;       
        }

        if (Session["IndexPageLoginOperateur"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageLoginOperateur"]);
        }
        else
        {
            IndexPage = 1;

        }
        
        LaodGridView(IndexPage);
    }

    protected void GridViewLoginsOperateurs_IndexChanging(object sender, GridViewPageEventArgs e)
    {
        
        GridViewLoginsOperateurs.PageIndex = e.NewPageIndex;
        GridViewLoginsOperateurs.DataSource = Session["ListLoginsOperateurs"] as List<Log>;
        GridViewLoginsOperateurs.DataBind();
    }

    protected void CbFiltre_CheckedChanged(object sender, EventArgs e)
    {
        Panelfilter.Visible = CbFiltre.Checked;

    }

    protected void GridViewLoginsOperateurs_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
     //   GridViewRow row = GridViewLoginsOperateurs.Rows[e.NewSelectedIndex];
      
     //   string FlagAutorise = (row.FindControl("Selected") as Label).Text;

     ////   photo_login.ImageUrl = "~/Photo.ashx/?Id=" + (row.FindControl("LblMatricule") as Label).Text;

     //  // photo_login.Visible = true;

     //   BtnDelete.Visible = true;
     //   BtnSet.Visible = true;

     //   GridViewLoginsOperateurs.FooterRow.Cells[6].Text = "Page : " + Convert.ToInt32(Session["IndexPageLoginOperateur"]) +"";
      
    
    }

    protected void GridViewLoginsOperateurs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //e.Row.Attributes.Add("OnClick", String.Format("javascript:__doPostBack('ctl00$MainContent$GridViewLoginsOperateurs','Select${0}')", e.Row.RowIndex));


            if (Convert.ToInt32(Session["IndexPageLoginOperateur"]) == 0)
            {
                Session["IndexPageLoginOperateur"] = 1;
                IndexPage = Convert.ToInt32(Session["IndexPageLoginOperateur"]);
            }

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = "Page : " + IndexPage + "";
        }

       
    }

    protected void BtnLast_Click(object sender, EventArgs e)
    {
        if (TbDateDebut.SelectedDate.ToString() != "" || TbDateFin.SelectedDate.ToString() != "" || TbNom.Text != "" || TbPrenom.Text != "" || DDLDroitPointage.SelectedValue != "0")
        { 

           
             TotalLoginsOperateursByFiltre = Convert.ToInt32(Session["TotalLoginsOperateursByFiltre"]);
             int T = (int) TotalLoginsOperateursByFiltre / 15;
             if (T < TotalLoginsOperateursByFiltre / 15)
             {
                 Session["IndexPageLoginOperateur"] = ((int)TotalLoginsOperateursByFiltre / 15) + 1;
             }
             else 
             {
                 Session["IndexPageLoginOperateur"] = ((int)TotalLoginsOperateursByFiltre / 15);
             }
        }
        else
         {
             TotalLoginsOperateurs = Convert.ToInt32(Session["TotalLoginsOperateurs"]);
             int T = (int)TotalLoginsOperateurs / 15;
             if (T < TotalLoginsOperateurs / 15)
             {
                 Session["IndexPageLoginOperateur"] = ((int)TotalLoginsOperateurs / 15) + 1;
             }
             else
             {
                 Session["IndexPageLoginOperateur"] = ((int)TotalLoginsOperateurs / 15);
             }

        }

         IndexPage = Convert.ToInt32(Session["IndexPageLoginOperateur"]);
         LaodGridView(IndexPage);
    }

    protected void BtnNext_Click(object sender, EventArgs e)
    {

       
        
            if (Session["IndexPageLoginOperateur"] != null )
            {

             
                    Session["IndexPageLoginOperateur"] = Convert.ToInt32(Session["IndexPageLoginOperateur"]) + 1;
                    IndexPage = Convert.ToInt32(Session["IndexPageLoginOperateur"]);
             

            }
            
            
            else
            {
                Session["IndexPageLoginOperateur"] = 2;
                IndexPage = 2;
            }
            LaodGridView(IndexPage);

        
    }

    protected void BtnPrevious_Click(object sender, EventArgs e)
    {

            Session["IndexPageLoginOperateur"] = Convert.ToInt32(Session["IndexPageLoginOperateur"]) - 1;
            IndexPage = Convert.ToInt32(Session["IndexPageLoginOperateur"]);
            LaodGridView(IndexPage);
        
    }

    protected void BtnFirst_Click(object sender, EventArgs e)
    {
        Session["IndexPageLoginOperateur"] =  1;
        IndexPage = Convert.ToInt32(Session["IndexPageLoginOperateur"]);
        LaodGridView(IndexPage);
    }

    protected void exportExcel_Click(object sender, EventArgs e)
    {

        Response.Clear();
        string FileName = "attachment;filename=" + "Liste_Logins_des_Operateurs" + ".xls";
        Response.AddHeader("content-disposition", FileName);

        Response.ContentEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");

        Response.ContentType = "application/vnd.xlsx";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        HtmlForm form = new HtmlForm();
        Page mypage = new System.Web.UI.Page();

        GridViewLoginsOperateurs.AllowPaging = false;
        GridViewLoginsOperateurs.AllowSorting = false;
        GridViewLoginsOperateurs.ShowFooter = false;
       

        GridViewLoginsOperateurs.EditIndex = -1;

       

        GridViewLoginsOperateurs.Columns[0].Visible = false;
        GridViewLoginsOperateurs.Columns[1].Visible = false;

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

        DateTime debut = TbDateDebut.SelectedDate.Value;
        DateTime fin = TbDateFin.SelectedDate.Value;



        GridViewLoginsOperateurs.DataSource = (new Log()).GetLoginsOperateursToExport(debut, fin, nom, prenom, ddlprofilValue);    
        GridViewLoginsOperateurs.DataBind();

        PrepareGridViewForExport(GridViewLoginsOperateurs);

        mypage.Controls.Add(form);
        form.Controls.Add(GridViewLoginsOperateurs);

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

        GridViewLoginsOperateurs.DataSource = ListLoginsOperateurs;
        GridViewLoginsOperateurs.DataBind();
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



}