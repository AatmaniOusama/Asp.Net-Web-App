using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLockTooTaxi;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class Interfaces_Rapport_chauffeursAbsents : System.Web.UI.Page
{

    int IndexPage;
    double TotalUsersAbsents;
    double TotalUsersAbsentsByFiltre;

    User user= new User();
    List<User> ListAbsentsJours ;


    protected void Page_Load(object sender, EventArgs e)
    {
       
        try
        {
               
           
            TbMatricule.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbNom.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbPrenom.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");

        Operateur operateur = (Operateur)Session["Operateur"];

        if (operateur.Profil.ToUpper() == "ADMINISTRATEUR")
        {

            BarreControle.Rows[0].Cells[2].Visible = true;

        }

        if (operateur.Profil.ToUpper() == "SUPERVISEUR")
        {

            BarreControle.Rows[0].Cells[2].Visible = true;
        }

        if (operateur.Profil.ToUpper() == "CONSULTANT")
        {

            BarreControle.Rows[0].Cells[2].Visible = true;
        }


        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";



         

            if (Session["UserAbsJrTbDateDebut"] as DateTime? != null)
                TbDateDebut.SelectedDate = Session["UserAbsJrTbDateDebut"] as DateTime?;
            else
                TbDateDebut.SelectedDate = DateTime.Parse(DateTime.Now.ToString().Substring(0, 10) + " 00:00:00");

            if (Session["UserAbsJrTbDateFin"] as DateTime? != null)
                TbDateFin.SelectedDate = Session["UserAbsJrTbDateFin"] as DateTime?;
            else
                TbDateFin.SelectedDate = DateTime.Parse(DateTime.Now.ToString().Substring(0, 10) + " 23:59:59");



            DateTime? dateDebut = TbDateDebut.SelectedDate;
            if (TbDateDebut.IsEmpty)
            { dateDebut = null; }

            DateTime? dateFin = TbDateFin.SelectedDate;
            if (TbDateFin.IsEmpty)
            { dateFin = null; }


            TotalUsersAbsents = user.getTotalUsersAbsents(dateDebut, dateFin);
            Session["TotalUsersAbsents"] = TotalUsersAbsents;






            TbNumPermis.Text = Session["UserAbsJrNumPermis"] as string;  
            TbMatricule.Text = Session["UserAbsJrMatricule"] as string;           
            TbNom.Text =  Session["UserAbsJrNom"] as string;
            TbPrenom.Text = Session["UserAbsJrPrenom"] as string;



            if (Session["IndexPageUsersAbsents"] != null)
            {
                IndexPage = Convert.ToInt32(Session["IndexPageUsersAbsents"]);

            }
            else
            {
                Session["IndexPageUsersAbsents"] = 1;
                IndexPage = 1;
            }






            LaodGridView(IndexPage);

            }
        }
        catch (Exception)
        {
        }
    }

    protected void LaodGridView(int IndexPage) {


        // ---------------  Récupérer les Valeurs des Filtres ----------------------------------------------------------------------------------------------------------------------------------------


        DateTime? dateDebut = TbDateDebut.SelectedDate;
        if (TbDateDebut.IsEmpty)
        { dateDebut = null; }

        DateTime? dateFin = TbDateFin.SelectedDate;
        if (TbDateFin.IsEmpty)
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

       
        // Verifier si l'un des champs des filtre  remplis ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        if (TbNumPermis.Text != "" ||  TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "")
        {
            TotalUsersAbsentsByFiltre = user.getTotalUsersAbsentsByFiltre(dateDebut,dateFin,numPermis, matricule, nom, prenom);
            Session["TotalUsersAbsentsByFiltre"] = TotalUsersAbsentsByFiltre;
            TbNbrLignes.Text = TotalUsersAbsentsByFiltre.ToString();

        }

        else
        {
            TotalUsersAbsents = user.getTotalUsersAbsentsByFiltre(dateDebut, dateFin, null, null, null, null);
            Session["TotalUsersAbsents"] = TotalUsersAbsents;
            TbNbrLignes.Text = TotalUsersAbsents.ToString();
        }


        ListAbsentsJours = user.getChauffeursAbsents(IndexPage, TbDateDebut.SelectedDate, TbDateFin.SelectedDate, numPermis, matricule, nom, prenom);

       
      

        
        
        Session["AbsentsJours"] = ListAbsentsJours;
        GridviewAbsentsJours.DataSource = ListAbsentsJours;
        GridviewAbsentsJours.DataBind();

        GridviewAbsentsJours.SelectedIndex = -1;

        FooterButtonGridviewAbsentsJours();
       


    }

    protected void FooterButtonGridviewAbsentsJours()
    {
        // Gérer l'affichage des bouttons Next previous... dans le footer -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        if ((ListAbsentsJours.Count() < 15 && TotalUsersAbsents < 15) || (ListAbsentsJours.Count() < 15 && Convert.ToInt32(Session["TotalUsersAbsentsByFiltre"]) < 15 && Convert.ToInt32(Session["TotalUsersAbsentsByFiltre"]) > 0) || TotalUsersAbsents == 15 || (Convert.ToInt32(Session["TotalUsersAbsentsByFiltre"]) == 15 && Convert.ToInt32(Session["TotalUsersAbsentsByFiltre"]) != 0))
        {
            GridviewAbsentsJours.FooterRow.Cells[2].Enabled = false;
            GridviewAbsentsJours.FooterRow.Cells[1].Enabled = false;

        }

        if (TbNumPermis.Text != "" || TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "")
        {
            if (IndexPage == 1 && Convert.ToInt32(Session["TotalUsersAbsentsByFiltre"]) != 0)
            {
                GridviewAbsentsJours.FooterRow.Cells[1].Enabled = false;
            }

            else
            {

                TotalUsersAbsentsByFiltre = Convert.ToInt32(Session["TotalUsersAbsentsByFiltre"]);
                int T = (int)TotalUsersAbsentsByFiltre / 15;

                if (T < TotalUsersAbsentsByFiltre / 15)
                {
                    if (IndexPage == T + 1)
                    {
                        GridviewAbsentsJours.FooterRow.Cells[2].Enabled = false;

                    }

                }
                else
                {
                    if (IndexPage == T)
                    {
                        GridviewAbsentsJours.FooterRow.Cells[2].Enabled = false;

                    }

                }
            }
        }


        else
        {
            TotalUsersAbsents = Convert.ToInt32(Session["TotalUsersAbsents"]);

            if (IndexPage == 1 && TotalUsersAbsents != 0)
            {
                GridviewAbsentsJours.FooterRow.Cells[1].Enabled = false;
            }
            else
            {

                //TotalUsersAbsents = Convert.ToInt32(Session["TotalUsersAbsents"]);
                int TU = (int)TotalUsersAbsents / 15;

                if (TU < TotalUsersAbsents / 15)
                {
                    if (IndexPage == TU + 1)
                    {
                        GridviewAbsentsJours.FooterRow.Cells[2].Enabled = false;


                    }

                }
                else
                {
                    if (IndexPage == TU)
                    {
                        GridviewAbsentsJours.FooterRow.Cells[2].Enabled = false;


                    }
                }
            }

        }

        // Fin -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    }

    protected void CbFiltre_CheckedChanged(object sender, EventArgs e)
    {
        Fieldset_Filter.Visible = CbFiltre.Checked;
    }

    protected void Filtre_Click(object sender, EventArgs e)
    {
        CustomValidator_TbDateFin.ErrorMessage = "Ne pas saisir une date inférieure à  " + TbDateDebut.SelectedDate + " !";

        if(Page.IsValid)


        Session["UserAbsJrTbDateDebut"] = TbDateDebut.SelectedDate;
        Session["UserAbsJrTbDateFin"] = TbDateFin.SelectedDate;
        Session["UserAbsJrNumPermis"] = TbNumPermis.Text;
        Session["UserAbsJrMatricule"] = TbMatricule.Text;
        Session["UserAbsJrNom"] = TbNom.Text;
        Session["UserAbsJrPrenom"] = TbPrenom.Text;

        if (TbNumPermis.Text != "" || TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "")
        {
            Session["IndexPageUsersAbsents"] = 1;
            IndexPage = 1;
        }

        if (Session["IndexPageUsersAbsents"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageUsersAbsents"]);
        }
        else
        {
            Session["IndexPageUsersAbsents"] = 1;
            IndexPage = 1;

        }

        LaodGridView(IndexPage);

    }
    
    protected void GridviewAbsentsJours_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridviewAbsentsJours.PageIndex = e.NewPageIndex;

        GridviewAbsentsJours.DataSource = Session["AbsentsJours"] as List<User> ;
        GridviewAbsentsJours.DataBind();

       

    }

    protected void GridviewAbsentsJours_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if ((e.Row.Cells[1].FindControl("LblNumPermis") as Label).Text == "0")
                (e.Row.Cells[1].FindControl("LblNumPermis") as Label).Text = "";

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            if (TbNumPermis.Text != "" || TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "")
            {
                int Q = Convert.ToInt32(Session["TotalUsersAbsentsByFiltre"]) / 15;
                int R = Convert.ToInt32(Session["TotalUsersAbsentsByFiltre"]) % 15;

                if (R > 0)
                {
                    Q = Q + 1;
                    e.Row.Cells[3].Text = "Page : " + Convert.ToInt32(Session["IndexPageUsersAbsents"]) + " / " + Q + "";
                }
                else
                {
                    e.Row.Cells[3].Text = "Page : " + Convert.ToInt32(Session["IndexPageUsersAbsents"]) + " / " + Q + "";
                }
            }
            else
            {
                int Q = Convert.ToInt32(Session["TotalUsersAbsents"]) / 15;
                int R = Convert.ToInt32(Session["TotalUsersAbsents"]) % 15;

                if (R > 0)
                {
                    Q = Q + 1;
                    e.Row.Cells[3].Text = "Page : " + Convert.ToInt32(Session["IndexPageUsersAbsents"]) + " / " + Q + "";
                }
                else
                {
                    e.Row.Cells[3].Text = "Page : " + Convert.ToInt32(Session["IndexPageUsersAbsents"]) + " / " + Q + "";
                }

            }




        }
    }

    protected void BtnLast_Click(object sender, EventArgs e)
    {
        if (TbNumPermis.Text != "" || TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "")
        {


            TotalUsersAbsentsByFiltre = Convert.ToInt32(Session["TotalUsersAbsentsByFiltre"]);
            int T = (int)TotalUsersAbsentsByFiltre / 15;
            if (T < TotalUsersAbsentsByFiltre / 15)
            {
                Session["IndexPageUsersAbsents"] = ((int)TotalUsersAbsentsByFiltre / 15) + 1;
            }
            else
            {
                Session["IndexPageUsersAbsents"] = ((int)TotalUsersAbsentsByFiltre / 15);
            }
        }
        else
        {
            TotalUsersAbsents = Convert.ToInt32(Session["TotalUsersAbsents"]);
            int T = (int)TotalUsersAbsents / 15;
            if (T < TotalUsersAbsents / 15)
            {
                Session["IndexPageUsersAbsents"] = ((int)TotalUsersAbsents / 15) + 1;
            }
            else
            {
                Session["IndexPageUsersAbsents"] = ((int)TotalUsersAbsents / 15);
            }


        }


        IndexPage = Convert.ToInt32(Session["IndexPageUsersAbsents"]);
        LaodGridView(IndexPage);
    }

    protected void BtnNext_Click(object sender, EventArgs e)
    {



        if (Session["IndexPageUsersAbsents"] != null)
        {


            Session["IndexPageUsersAbsents"] = Convert.ToInt32(Session["IndexPageUsersAbsents"]) + 1;
            IndexPage = Convert.ToInt32(Session["IndexPageUsersAbsents"]);


        }


        else
        {
            Session["IndexPageUsersAbsents"] = 2;
            IndexPage = 2;
        }
        LaodGridView(IndexPage);


    }

    protected void BtnPrevious_Click(object sender, EventArgs e)
    {

        Session["IndexPageUsersAbsents"] = Convert.ToInt32(Session["IndexPageUsersAbsents"]) - 1;
        IndexPage = Convert.ToInt32(Session["IndexPageUsersAbsents"]);
        LaodGridView(IndexPage);

    }

    protected void BtnFirst_Click(object sender, EventArgs e)
    {
        Session["IndexPageUsersAbsents"] = 1;
        IndexPage = Convert.ToInt32(Session["IndexPageUsersAbsents"]);
        LaodGridView(IndexPage);
    }

    protected void ServerValidation(object source, ServerValidateEventArgs args)
    {
        args.IsValid = TbDateFin.SelectedDate >= TbDateDebut.SelectedDate;
    }

    protected void exportExcel_Click(object sender, EventArgs e)
    {

        Response.Clear();
        string FileName = "attachment;filename=Chauffeurs Absents.xls";
        Response.AddHeader("content-disposition", FileName);

        Response.ContentEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");

        Response.ContentType = "application/vnd.xlsx";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        HtmlForm form = new HtmlForm();
        Page mypage = new System.Web.UI.Page();

        GridviewAbsentsJours.AllowPaging = false;
        GridviewAbsentsJours.AllowSorting = false;
        GridviewAbsentsJours.ShowFooter = false;

        GridviewAbsentsJours.EditIndex = -1;


         DateTime? dateDebut = TbDateDebut.SelectedDate;
        if (TbDateDebut.IsEmpty)
        { dateDebut = null; }

        DateTime? dateFin = TbDateFin.SelectedDate;
        if (TbDateFin.IsEmpty)
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





        


        GridviewAbsentsJours.DataSource = user.getAllChauffeursAbsents(TbDateDebut.SelectedDate, TbDateFin.SelectedDate, numPermis, matricule, nom, prenom);
        GridviewAbsentsJours.DataBind();
           

        PrepareGridViewForExport(GridviewAbsentsJours);

        mypage.Controls.Add(form);
        form.Controls.Add(GridviewAbsentsJours);

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


        GridviewAbsentsJours.DataSource = ListAbsentsJours;
        GridviewAbsentsJours.DataBind();
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

          // Verifier si l'un des champs des filtre  remplis ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        if (TbNumPermis.Text != "" ||  TbMatricule.Text != "" || TbNom.Text != "" || TbPrenom.Text != "")
        {

        DateTime? dateDebut = TbDateDebut.SelectedDate;
        if (TbDateDebut.IsEmpty)
        { dateDebut = null; }

        DateTime? dateFin = TbDateFin.SelectedDate;
        if (TbDateFin.IsEmpty)
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


        ListAbsentsJours = user.getAllChauffeursAbsents(dateDebut, dateFin, numPermis, matricule, nom, prenom);

        }
        else
        {
            ListAbsentsJours = user.getAllChauffeursAbsents(TbDateDebut.SelectedDate, TbDateFin.SelectedDate,null, null,null,null);
        }
          



      

        switch (sortExp)
        {

           

            case "NumPermis":
                foreach (User user in ListAbsentsJours)
                {
                    if (string.IsNullOrWhiteSpace(user.NumBadge) || string.IsNullOrEmpty(user.NumBadge))
                        user.NumBadge = "0";
                }
                if (sortDir == "desc")
                    ListAbsentsJours = ListAbsentsJours.OrderByDescending(o => int.Parse(o.NumBadge)).ToList();
                else
                    ListAbsentsJours = ListAbsentsJours.OrderBy(o => int.Parse(o.NumBadge)).ToList();
                break;

            case "Matricule":
                if (sortDir == "desc")
                    ListAbsentsJours = ListAbsentsJours.OrderByDescending(o => o.Matricule).ToList();
                else
                    ListAbsentsJours = ListAbsentsJours.OrderBy(o => o.Matricule).ToList();
                break;

            case "Nom":
                if (sortDir == "desc")
                    ListAbsentsJours = ListAbsentsJours.OrderByDescending(o => o.Nom).ToList();
                else
                    ListAbsentsJours = ListAbsentsJours.OrderBy(o => o.Nom).ToList();
                break;

            case "Prenom":
                if (sortDir == "desc")
                    ListAbsentsJours = ListAbsentsJours.OrderByDescending(o => o.Prenom).ToList();
                else
                    ListAbsentsJours = ListAbsentsJours.OrderBy(o => o.Prenom).ToList();
                break;

           


           
        }

        if (Session["IndexPageUsersAbsents"] != null)
        {
            IndexPage = Convert.ToInt32(Session["IndexPageUsersAbsents"]);
        }
        else
        {
            Session["IndexPageUsersAbsents"] = 1;
            IndexPage = 1;

        }



        Session["ListAbsentsJours"] = ListAbsentsJours.Skip((Convert.ToInt32(Session["IndexPageUsersAbsents"]) - 1) * 15).Take(15);

        GridviewAbsentsJours.DataSource = ListAbsentsJours.Skip((Convert.ToInt32(Session["IndexPageUsersAbsents"]) - 1) * 15).Take(15);
        GridviewAbsentsJours.DataBind();
        FooterButtonGridviewAbsentsJours();


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

    protected void Users_Sorting(object sender, GridViewSortEventArgs e)
    {
        BindGridView(e.SortExpression, sortOrder);
          int columnIndex = 0;

        foreach (DataControlFieldHeaderCell headerCell in GridviewAbsentsJours.HeaderRow.Cells)
        {
            if (headerCell.ContainingField.SortExpression == e.SortExpression)
            {
                columnIndex = GridviewAbsentsJours.HeaderRow.Cells.GetCellIndex(headerCell);
            }
        }

        GridviewAbsentsJours.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);

      
    }
    

    //-------------------------------------------------------------------------------------------


}