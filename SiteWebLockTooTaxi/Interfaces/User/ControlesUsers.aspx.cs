using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using DataLockTooTaxi;
using System.Web.UI.HtmlControls;
using DataLockTooTaxi.MyDataSetTableAdapters;


public partial class Interfaces_User_ControlesUsers : System.Web.UI.Page
{

    int IndexPage;
    double TotalControlesUser;
    double TotalControlesUserByFiltre;

    ControleUser controleUser = new ControleUser();
    List<ControleUser> ListControleUser;
    LabelControle labelControle = new LabelControle();
 

    protected void Page_Load(object sender, EventArgs e)
    {


       
        LabelControle labelControle = new LabelControle();
        List<LabelControle> ListeLabelControle = new List<LabelControle>();
        
      

        

        try
        {
            TotalControlesUser = controleUser.getTotalControlesUser();
            Session["TotalControlesUser"] = TotalControlesUser;


            TbMatricule.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbNom.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbPrenom.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");

           

            Operateur operateur = (Operateur)Session["Operateur"];

            if (operateur.Profil.ToUpper() == "ADMINISTRATEUR")
            {
                 BarreControle.Rows[0].Cells[2].Visible = true;
                 BarreControle.Rows[0].Cells[3].Visible = true;
                 GridViewControlesUsers.Columns[0].Visible = true;
                 
            }

            if (operateur.Profil.ToUpper() == "SUPERVISEUR")
            {
                BarreControle.Rows[0].Cells[2].Visible = true;  // Exporter vers Excel
                BarreControle.Rows[0].Cells[3].Visible = true;  // Ajouter Controle User
                GridViewControlesUsers.Columns[0].Visible = true;  // Modifer les controles

            }

            if (operateur.Profil.ToUpper() == "CONSULTANT")
            {
                BarreControle.Rows[0].Cells[2].Visible = true;
                BarreControle.Rows[0].Cells[3].Visible = false;
                GridViewControlesUsers.Columns[0].Visible = false;
                Panel_Ajout.Visible = false;
            }

        }
        catch (Exception)
        {

            Response.Redirect("~/Interfaces/Account/Login.aspx");
        }

        if (!IsPostBack)
        {



            ViewState["sortOrder"] = "";

          


            List<LabelControle> listLabelControle = labelControle.getLabelsControlesByType(2);
            


           DDLAbrvControleUser.DataSource = listLabelControle;
           DDLAbrvControleUser.DataValueField = "Id";
           DDLAbrvControleUser.DataTextField = "Abrev";
           DDLAbrvControleUser.DataBind();


           DDLAbrvControleUser.SelectedValue = Session["userControleAbrevLabelControle"]  as string;
           if (Session["userControleDateFinValidite"] as DateTime? != null)
               TbFiltreDateFin.SelectedDate = Session["userControleDateFinValidite"] as DateTime?;
           else
               TbFiltreDateFin.SelectedDate = null;
           TbNumPermis.Text = Session["userControleNumPermis"] as string;
           TbMatricule.Text = Session["userControleMatricule"] as string;
           TbNom.Text = Session["userControleNom"] as string;
           TbPrenom.Text = Session["userControlePrenom"] as string;



            


         
            _DDLAbrvControleUser.DataSource = listLabelControle;
            _DDLAbrvControleUser.DataValueField = "Id";
            _DDLAbrvControleUser.DataTextField = "Abrev";
            _DDLAbrvControleUser.DataBind();





            if (Session["IndexPageControlesUser"] != null)
            {
                IndexPage = Convert.ToInt32(Session["IndexPageControlesUser"]);

            }
            else
            {

                Session["IndexPageControlesUser"] = 1;
                IndexPage = 1;
            }


            LaodGridView(IndexPage);

     
        }

    }
    
    protected void LaodGridView(int IndexPage)
    {

        // ---------------  Récupérer les Valeurs des Filtres ----------------------------------------------------------------------------------------------------------------------------------------

        int? ddlAbrevControlesUsers = int.Parse(DDLAbrvControleUser.SelectedValue);
        if (DDLAbrvControleUser.SelectedValue == "0")
        {
            ddlAbrevControlesUsers = null;
        }

        DateTime? dateFin = TbFiltreDateFin.SelectedDate;
        if (TbFiltreDateFin.IsEmpty)
        { dateFin = null; }


        string numPermis = TbNumPermis.Text;
        if (numPermis == "")
        { numPermis = null; }

        string matricule = TbMatricule.Text;
        if (matricule == "")
        { matricule = null; }

        string nom = TbNom.Text;
        if (nom == "")
        { nom = null; }

        string prenom = TbPrenom.Text;
        if (prenom == "")
        { prenom = null; }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // Verifier si l'un des champs des filtre  remplis ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        if (DDLAbrvControleUser.SelectedValue != "0" || !TbFiltreDateFin.IsEmpty || TbNumPermis.Text != "" || TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "")
        {
            TotalControlesUserByFiltre = controleUser.getTotalControlesUserByFiltre(ddlAbrevControlesUsers, dateFin, numPermis, matricule, nom, prenom);
            Session["TotalControlesUserByFiltre"] = TotalControlesUserByFiltre;
            NbrLignes.Text = TotalControlesUserByFiltre.ToString();

        }

        else
        {
            NbrLignes.Text = controleUser.getTotalControlesUser().ToString();
        }


        // Fin Vérification -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        ListControleUser = controleUser.getAllControlesUser(IndexPage, ddlAbrevControlesUsers, dateFin, numPermis, matricule, nom, prenom);

        Session["ListControleUser"] = ListControleUser;
        GridViewControlesUsers.DataSource = ListControleUser;
        GridViewControlesUsers.DataBind();

        GridViewControlesUsers.SelectedIndex = -1;

        FooterButtonGridViewControlesUser();

 
            
    }

    protected void FooterButtonGridViewControlesUser()
    {
        // Gérer l'affichage des bouttons Next previous... dans le footer -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        if ((ListControleUser.Count() < 15 && TotalControlesUser < 15) || (ListControleUser.Count() < 15 && Convert.ToInt32(Session["TotalControlesUserByFiltre"]) < 15 && Convert.ToInt32(Session["TotalControlesUserByFiltre"]) > 0) || TotalControlesUser == 15 || (Convert.ToInt32(Session["TotalControlesUserByFiltre"]) == 15 && Convert.ToInt32(Session["TotalControlesUserByFiltre"]) != 0))
        {
            GridViewControlesUsers.FooterRow.Cells[3].Enabled = false;
            GridViewControlesUsers.FooterRow.Cells[2].Enabled = false;


        }


        if (DDLAbrvControleUser.SelectedValue != "0" || !TbFiltreDateFin.IsEmpty || TbNumPermis.Text != "" || TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "")
        {

            if (IndexPage == 1 && Convert.ToInt32(Session["TotalControlesUserByFiltre"]) != 0)
            {
                GridViewControlesUsers.FooterRow.Cells[2].Enabled = false;
            }
            else
            {

                TotalControlesUserByFiltre = Convert.ToInt32(Session["TotalControlesUserByFiltre"]);
                int T = (int)TotalControlesUserByFiltre / 15;
                if (T < TotalControlesUserByFiltre / 15)
                {
                    if (IndexPage == T + 1)
                    {
                        GridViewControlesUsers.FooterRow.Cells[3].Enabled = false;

                    }

                }
                else
                {
                    if (IndexPage == T)
                    {
                        GridViewControlesUsers.FooterRow.Cells[3].Enabled = false;


                    }
                }
            }

        }
        else
        {
            if (IndexPage == 1 && TotalControlesUser != 0)
            {
                GridViewControlesUsers.FooterRow.Cells[2].Enabled = false;
            }
            else
            {


                int TA = (int)TotalControlesUser / 15;
                if (TA < TotalControlesUser / 15)
                {
                    if (IndexPage == TA + 1)
                    {
                        GridViewControlesUsers.FooterRow.Cells[3].Enabled = false;


                    }

                }
                else
                {
                    if (IndexPage == TA)
                    {

                        GridViewControlesUsers.FooterRow.Cells[3].Enabled = false;

                    }
                }
            }
        }
        // Fin -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    }

    protected void Filtre_Click(object sender, EventArgs e)
    {

        Session["userControleAbrevLabelControle"] = DDLAbrvControleUser.SelectedValue;
        Session["userControleDateFinValidite"] = TbFiltreDateFin.SelectedDate;
        Session["userControleNumPermis"] = TbNumPermis.Text;
        Session["userControleMatricule"] = TbMatricule.Text;
        Session["userControleNom"] = TbNom.Text;
        Session["userControlePrenom"] = TbPrenom.Text;


        
        if (DDLAbrvControleUser.SelectedValue != "0" || !TbFiltreDateFin.IsEmpty || TbNumPermis.Text != "" || TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "")
        {
            Session["IndexPageControlesUser"] = 1;
            IndexPage = 1;
        }
        else
        {
            if (Session["IndexPageControlesUser"] != null)
            {
                IndexPage = Convert.ToInt32(Session["IndexPageControlesUser"]);
            }
            else
            {
                Session["IndexPageControlesUser"] = 1;
                IndexPage = 1;

            }
        }


        LaodGridView(IndexPage);
    }
 
    protected void GridViewControlesUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewControlesUsers.PageIndex = e.NewPageIndex;
        GridViewControlesUsers.DataSource = Session["ListControleUser"] as List<ControleUser>;
        GridViewControlesUsers.DataBind();

        GridViewControlesUsers.FooterRow.Cells[3].Text = "Page : " + Convert.ToInt32(Session["IndexPageControlesUser"]) + "";

       
    }

    protected void GridViewControlesUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            if (DDLAbrvControleUser.SelectedValue != "0" || !TbFiltreDateFin.IsEmpty || TbNumPermis.Text != "" || TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "")
            {
                int Q = Convert.ToInt32(Session["TotalControlesUserByFiltre"]) / 15;
                int R = Convert.ToInt32(Session["TotalControlesUserByFiltre"]) % 15;

                if (R > 0)
                {
                    Q = Q + 1;
                    e.Row.Cells[6].Text = "Page : " + Convert.ToInt32(Session["IndexPageControlesUser"]) + " / " + Q + "";
                }
                else
                {
                    e.Row.Cells[6].Text = "Page : " + Convert.ToInt32(Session["IndexPageControlesUser"]) + " / " + Q + "";
                }
            }
            else
            {
                int Q = Convert.ToInt32(Session["TotalControlesUser"]) / 15;
                int R = Convert.ToInt32(Session["TotalControlesUser"]) % 15;

                if (R > 0)
                {
                    Q = Q + 1;
                    e.Row.Cells[6].Text = "Page : " + Convert.ToInt32(Session["IndexPageControlesUser"]) + " / " + Q + "";
                }
                else
                {
                    e.Row.Cells[6].Text = "Page : " + Convert.ToInt32(Session["IndexPageControlesUser"]) + " / " + Q + "";
                }

            }


        }


    }

    protected void GridViewControlesUsers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewControlesUsers.EditIndex = e.NewEditIndex;
       
        if (Session["IndexPageControlesUser"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageControlesUser"]);

        }
        else
        {
            IndexPage = 1;
        }


        LaodGridView(IndexPage);

    }

    protected void GridViewControlesUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridViewControlesUsers.EditIndex = -1;
        if (Session["IndexPageControlesUser"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageControlesUser"]);

        }
        else
        {
            IndexPage = 1;
        }


        LaodGridView(IndexPage);

    }

    protected void GridViewControlesUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        ControleUser ControleUser = new ControleUser();
        Operateur operateur = Session["Operateur"] as Operateur;
        GridViewRow row = GridViewControlesUsers.Rows[e.RowIndex];
        DateTime dateFin = DateTime.Parse((row.FindControl("TbSetDateFin") as TextBox).Text);
        int Id = int.Parse((row.FindControl("lbSave") as ImageButton).CommandArgument);
        ControleUser.ModifierUnControleuser(dateFin, operateur.ID, Id);
        GridViewControlesUsers.EditIndex = -1;

        if (Session["IndexPageControlesUser"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageControlesUser"]);

        }
        else
        {
            IndexPage = 1;
        }


        LaodGridView(IndexPage);
    }

    protected void GridViewControlesUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ControleUser ControleUser = new ControleUser();
        GridViewRow row = GridViewControlesUsers.Rows[e.RowIndex];
        int i = 0;
        int Id = int.Parse((row.FindControl("lbDelete") as ImageButton).CommandArgument);
        i = ControleUser.SupprimerUnControleuser(Id);
        if (i == 1)
        {
            GridViewControlesUsers.EditIndex = -1;

            if (Session["IndexPageControlesUser"] != null)
            {
                IndexPage = Convert.ToInt32(Session["IndexPageControlesUser"]);

            }
            else
            {
                IndexPage = 1;
            }


            LaodGridView(IndexPage);
        }
    }

    protected void _TbMatricule_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string Matricule = _TbMatricule.Text;
            if (Matricule != "")
            {
                User user = new User();
                _TbNumPermis.Text = user.getNumPermis(Matricule);
                _TbNomPrenom.Text = user.geNomCompletByMatricule(Matricule);
            }
            else
            {
                _TbNumPermis.Text = "";
                _TbNomPrenom.Text = "";
            }
        }
        catch (Exception)
        {
            _TbNumPermis.Text = "";
            _TbNomPrenom.Text = "";
        }
    }

    protected void _TbNumPermis_TextChanged(object sender, EventArgs e)
    {
        try
        {
            
        string numPermis = _TbNumPermis.Text;
          if (numPermis != "")
        {
            User user = new User();
            _TbMatricule.Text = user.getMatricule(numPermis);
            _TbNomPrenom.Text = user.geNomCompletByMatricule(_TbMatricule.Text);
        }
          else
          {
              _TbMatricule.Text = "";
              _TbNomPrenom.Text = "";
          }
          
          
          }
        catch (Exception)
        {
            _TbMatricule.Text = "";
            _TbNomPrenom.Text = "";
        }
    }

    protected void BtSave_Click(object sender, EventArgs e)
    {
        Operateur operateur = (Operateur)Session["Operateur"];
        User user = new DataLockTooTaxi.User();
        ControleUser item = new ControleUser();
        int i=0;

        string matricule = _TbMatricule.Text;
        string AbrevControleUser=_DDLAbrvControleUser.SelectedItem.Text;


        if (!(item.controleUserExiste(matricule, AbrevControleUser)))
            {
                item.IdLabelControle = int.Parse(_DDLAbrvControleUser.SelectedValue);
                item.DateFin = _TbDateFin.Text + " 12:00:00";


                item.IdUser = user.GetIdUser(_TbMatricule.Text);
                item.IdOpCreation = operateur.ID;
                item.IdOpModif = operateur.ID;
                i = controleUser.AjouterControleUser(item);
               

            }

            else
            {
             
                    string myStringVariable = "Impossible d ajouter ce type de contrôle sur ce Chauffeur, Car il existe déjà !! ";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + myStringVariable + "')", true);
                   
                    MPE.Hide();
                   
            }

        if(i==1)
        {
            Response.Redirect("~/Interfaces/User/ControlesUsers.aspx");
        }

    }

    protected void BtCancel_Click(object sender, EventArgs e)
    {
        MPE.Hide();
      
    }

    protected void CbFiltre_CheckedChanged(object sender, EventArgs e)
    {
        DivFilter.Visible = CbFiltre.Checked;

    }

    protected void BtnLast_Click(object sender, EventArgs e)
    {
        if (DDLAbrvControleUser.SelectedValue != "0" || !TbFiltreDateFin.IsEmpty || TbNumPermis.Text != "" || TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "")
        {


            TotalControlesUserByFiltre = Convert.ToInt32(Session["TotalControlesUserByFiltre"]);
            int T = (int)TotalControlesUserByFiltre / 15;
            if (T < TotalControlesUserByFiltre / 15)
            {
                Session["IndexPageControlesUser"] = ((int)TotalControlesUserByFiltre / 15) + 1;
            }
            else
            {
                Session["IndexPageControlesUser"] = ((int)TotalControlesUserByFiltre / 15);
            }
        }
        else
        {
            TotalControlesUser = Convert.ToInt32(Session["TotalControlesUser"]);
            int T = (int)TotalControlesUser / 15;
            if (T < TotalControlesUser / 15)
            {
                Session["IndexPageControlesUser"] = ((int)TotalControlesUser / 15) + 1;
            }
            else
            {
                Session["IndexPageControlesUser"] = ((int)TotalControlesUser / 15);
            }


        }


        IndexPage = Convert.ToInt32(Session["IndexPageControlesUser"]);
        LaodGridView(IndexPage);
    }

    protected void BtnNext_Click(object sender, EventArgs e)
    {



        if (Session["IndexPageControlesUser"] != null)
        {


            Session["IndexPageControlesUser"] = Convert.ToInt32(Session["IndexPageControlesUser"]) + 1;
            IndexPage = Convert.ToInt32(Session["IndexPageControlesUser"]);


        }


        else
        {
            Session["IndexPageControlesUser"] = 2;
            IndexPage = 2;
        }

        LaodGridView(IndexPage);


    }

    protected void BtnPrevious_Click(object sender, EventArgs e)
    {

        Session["IndexPageControlesUser"] = Convert.ToInt32(Session["IndexPageControlesUser"]) - 1;
        IndexPage = Convert.ToInt32(Session["IndexPageControlesUser"]);
        LaodGridView(IndexPage);

    }

    protected void BtnFirst_Click(object sender, EventArgs e)
    {
        Session["IndexPageControlesUser"] = 1;
        IndexPage = Convert.ToInt32(Session["IndexPageControlesUser"]);
        LaodGridView(IndexPage);
    }

    protected void exportExcel_Click(object sender, EventArgs e)
    {

         Response.Clear();
        string FileName = "attachment;filename=" + HttpUtility.HtmlDecode( HttpUtility.HtmlEncode(this.Page.Title)) + ".xls";
        Response.AddHeader("content-disposition", FileName);

        Response.ContentEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");

        Response.ContentType = "application/vnd.xlsx";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        HtmlForm form = new HtmlForm();
        Page mypage = new System.Web.UI.Page();

        GridViewControlesUsers.AllowPaging = false;
        GridViewControlesUsers.AllowSorting = false;

        GridViewControlesUsers.ShowFooter = false;

        GridViewControlesUsers.EditIndex = -1;

     

        GridViewControlesUsers.Columns[GridViewControlesUsers.Columns.Count - 1].Visible = false;
        GridViewControlesUsers.Columns[0].Visible = false;

        // ---------------  Récupérer les Valeurs des Filtres ----------------------------------------------------------------------------------------------------------------------------------------

        int? ddlAbrevControlesUsers = int.Parse(DDLAbrvControleUser.SelectedValue);
        if (DDLAbrvControleUser.SelectedValue == "0")
        {
            ddlAbrevControlesUsers = null;
        }

        DateTime? dateFin = TbFiltreDateFin.SelectedDate;
        if (TbFiltreDateFin.IsEmpty)
        { dateFin = null; }


        string numPermis = TbNumPermis.Text;
        if (numPermis == "")
        { numPermis = null; }

        string matricule = TbMatricule.Text;
        if (matricule == "")
        { matricule = null; }

        string nom = TbNom.Text;
        if (nom == "")
        { nom = null; }

        string prenom = TbPrenom.Text;
        if (prenom == "")
        { prenom = null; }


        GridViewControlesUsers.DataSource = (new ControleUser()).getAllControleUserByFiltre(ddlAbrevControlesUsers, dateFin, numPermis, matricule, nom, prenom);    
        GridViewControlesUsers.DataBind();
        

        PrepareGridViewForExport(GridViewControlesUsers);

        mypage.Controls.Add(form);
        form.Controls.Add(GridViewControlesUsers);

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

        if (DDLAbrvControleUser.SelectedValue != "0" || !TbFiltreDateFin.IsEmpty || TbNumPermis.Text != "" || TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "")
        {

        int? ddlAbrevControlesUsers = int.Parse(DDLAbrvControleUser.SelectedValue);
        if (DDLAbrvControleUser.SelectedValue == "0")
        {
            ddlAbrevControlesUsers = null;
        }

        DateTime? dateFin = TbFiltreDateFin.SelectedDate;
        if (TbFiltreDateFin.IsEmpty)
        { dateFin = null; }


        string numPermis = TbNumPermis.Text;
        if (numPermis == "")
        { numPermis = null; }

        string matricule = TbMatricule.Text;
        if (matricule == "")
        { matricule = null; }

        string nom = TbNom.Text;
        if (nom == "")
        { nom = null; }

        string prenom = TbPrenom.Text;
        if (prenom == "")
        { prenom = null; }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        ListControleUser = controleUser.getAllControleUserByFiltre(ddlAbrevControlesUsers, dateFin, numPermis, matricule, nom, prenom);

        }
        else
        {
            ListControleUser = controleUser.getControleUsers();
        }



        switch (sortExp)
        {
            case "Matricule":
                if (sortDir == "desc")
                    ListControleUser = ListControleUser.OrderByDescending(o => o.Matricule).ToList();
                else
                    ListControleUser = ListControleUser.OrderBy(o => o.Matricule).ToList();
                break;

            case "Nom":
                if (sortDir == "desc")
                    ListControleUser = ListControleUser.OrderByDescending(o => o.Nom).ToList();
                else
                    ListControleUser = ListControleUser.OrderBy(o => o.Nom).ToList();
                break;

            case "Prenom":
                if (sortDir == "desc")
                    ListControleUser = ListControleUser.OrderByDescending(o => o.Prenom).ToList();
                else
                    ListControleUser = ListControleUser.OrderBy(o => o.Prenom).ToList();
                break;

            case "DateFinValidité":
                if (sortDir == "desc")
                    ListControleUser = ListControleUser.OrderByDescending(o => DateTime.Parse(o.DateFin)).ToList();
                else
                    ListControleUser = ListControleUser.OrderBy(o => DateTime.Parse(o.DateFin)).ToList();

                break;

           
        }

     

        if (Session["IndexPageControlesUser"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageControlesUser"]);
        }
        else
        {
            Session["IndexPageControlesUser"] = 1;
            IndexPage = 1;

        }



        Session["ListControleUser"] = ListControleUser.Skip((Convert.ToInt32(Session["IndexPageAgrement"]) - 1) * 15).Take(15);

        GridViewControlesUsers.DataSource = ListControleUser.Skip((Convert.ToInt32(Session["IndexPageAgrement"]) - 1) * 15).Take(15);
        GridViewControlesUsers.DataBind();
        FooterButtonGridViewControlesUser();

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

    protected void Users_Controls_Sorting(object sender, GridViewSortEventArgs e)
    {
        BindGridView(e.SortExpression, sortOrder);


        int columnIndex = 0;

        foreach (DataControlFieldHeaderCell headerCell in GridViewControlesUsers.HeaderRow.Cells)
        {
            if (headerCell.ContainingField.SortExpression == e.SortExpression)
            {
                columnIndex = GridViewControlesUsers.HeaderRow.Cells.GetCellIndex(headerCell);
            }
        }

        GridViewControlesUsers.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);

       

    }

    //-------------------------------------------------------------------------------------------
    
}