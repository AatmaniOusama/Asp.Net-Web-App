using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLockTooTaxi;
using System.Web.UI.HtmlControls;

public partial class Interfaces_Rapport_IdentificationsPeriode : System.Web.UI.Page
{
    User user = null;
    List<User> ListIdentificationPeriodeUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

       

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




            if (Session["UserIdenDateDebut"] as DateTime? != null)
                TbDateDebut.SelectedDate = Session["UserIdenDateDebut"] as DateTime?;
            else
                TbDateDebut.SelectedDate = DateTime.Parse(DateTime.Now.ToString().Substring(0, 10) + " 00:00:00");

            if (Session["UserIdenDateFin"] as DateTime? != null)
                TbDateFin.SelectedDate = Session["UserIdenDateFin"] as DateTime?;
            else
                TbDateFin.SelectedDate = DateTime.Parse(DateTime.Now.ToString().Substring(0, 10) + " 23:59:59");

            TbNumPermis.Text = Session["UserIdenNumPermis"] as string;
            TbMatricule.Text = Session["UserIdenMatricule"] as string;
            TbNom.Text = Session["UserIdenNom"] as string;
            TbPrenom.Text = Session["UserIdenPrenom"] as string;
           


            if (!string.IsNullOrEmpty(Session["UserIdenAvecPointage"] as string))
                RbIdentification.SelectedValue = Session["UserIdenAvecPointage"] as string;


            LaodGridView();
        }    
        }
        catch (Exception)
        {
        }
    }

    protected void LaodGridView()
    {
        user = new User();
        List<User> ListIdentificationPeriodeUser = user.Identifications(TbDateDebut.SelectedDate.Value,TbDateFin.SelectedDate.Value);

        if(RbIdentification.SelectedValue=="true")
            ListIdentificationPeriodeUser = ListIdentificationPeriodeUser.FindAll(c=>!(c.DateDebut.ToString()=="" && c.DateFin.ToString()==""));
        else
            ListIdentificationPeriodeUser = ListIdentificationPeriodeUser.FindAll(c => (c.DateDebut.ToString() == "" && c.DateFin.ToString() == ""));

        if (TbNumPermis.Text != "")
        {
            ListIdentificationPeriodeUser = ListIdentificationPeriodeUser.FindAll(delegate(User item) { return item.NumBadge.Contains(TbNumPermis.Text.ToUpper()); });
        }
        if (TbMatricule.Text != "")
        {
            ListIdentificationPeriodeUser = ListIdentificationPeriodeUser.FindAll(delegate(User item) { return item.Matricule.Contains(TbMatricule.Text.ToUpper()); });
        }
        if (TbNom.Text != "")
        {
            ListIdentificationPeriodeUser = ListIdentificationPeriodeUser.FindAll(delegate(User item) { return item.Nom.Contains(TbNom.Text.ToUpper()); });
        }
        if (TbPrenom.Text != "")
        {
            ListIdentificationPeriodeUser = ListIdentificationPeriodeUser.FindAll(delegate(User item) { return item.Prenom.Contains(TbPrenom.Text.ToUpper()); });
        }
       

       

       

        GridviewIdentification.DataSource = ListIdentificationPeriodeUser;
        GridviewIdentification.DataBind();

        Session["IdentificationPeriodeUser"] = ListIdentificationPeriodeUser;

        TbNbrLignes.Text = ListIdentificationPeriodeUser.Count.ToString();



       
    }




    protected void CbFiltre_CheckedChanged(object sender, EventArgs e)
    {
        Fieldset_Filter.Visible = CbFiltre.Checked;
    }

    protected void Filtre_Click(object sender, EventArgs e)
    {

        Session["UserIdenDateDebut"] = TbDateDebut.SelectedDate;
        Session["UserIdenDateFin"] = TbDateFin.SelectedDate;
        Session["UserIdenNumPermis"] = TbNumPermis.Text;
        Session["UserIdenMatricule"] = TbMatricule.Text;
        Session["UserIdenNom"] = TbNom.Text ;
        Session["UserIdenPrenom"] = TbPrenom.Text ;        
        Session["UserIdenAvecPointage"] = RbIdentification.SelectedValue;


        LaodGridView();
    }

   

    protected void GridviewIdentification_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridviewIdentification.PageIndex = e.NewPageIndex;

        GridviewIdentification.DataSource =Session["IdentificationPeriodeUser"]  as List<User>;
        GridviewIdentification.DataBind();

     

    }


    protected void GridViewUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if ((e.Row.Cells[4].FindControl("LblDateFin") as Label).Text == "01/01/1900")
                (e.Row.Cells[4].FindControl("LblDateFin") as Label).Text = "";

            if ((e.Row.Cells[0].FindControl("LblNumPermis") as Label).Text == "0")
                (e.Row.Cells[0].FindControl("LblNumPermis") as Label).Text = "";
        }
    }

    protected void exportExcel_Click(object sender, EventArgs e)
    {

        Response.Clear();
        string FileName = "attachment;filename=PremierDernierPointagesChauffeurs.xls";
        Response.AddHeader("content-disposition", FileName);
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
        Response.ContentType = "application/vnd.xlsx";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        HtmlForm form = new HtmlForm();
        Page mypage = new System.Web.UI.Page();

        GridviewIdentification.AllowPaging = false;
        GridviewIdentification.AllowSorting = false;
        GridviewIdentification.ShowFooter = false;

        GridviewIdentification.EditIndex = -1;

        GridviewIdentification.DataSource = Session["IdentificationPeriodeUser"] as IList<User>;
        GridviewIdentification.DataBind();


        PrepareGridViewForExport(GridviewIdentification);

        mypage.Controls.Add(form);
        form.Controls.Add(GridviewIdentification);

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


        ListIdentificationPeriodeUser = Session["IdentificationPeriodeUser"]  as List<User>;

        switch (sortExp)
        {

            case "NumPermis":
                foreach (User user in ListIdentificationPeriodeUser)
                {
                    if (string.IsNullOrWhiteSpace(user.NumBadge) || string.IsNullOrEmpty(user.NumBadge))
                        user.NumBadge = "0";
                }
                if (sortDir == "desc")
                    ListIdentificationPeriodeUser = ListIdentificationPeriodeUser.OrderByDescending(o => int.Parse(o.NumBadge)).ToList();
                else
                    ListIdentificationPeriodeUser = ListIdentificationPeriodeUser.OrderBy(o => int.Parse(o.NumBadge)).ToList();
                break;

            case "Matricule":

                if (sortDir == "desc")
                    ListIdentificationPeriodeUser = ListIdentificationPeriodeUser.OrderByDescending(o => o.Matricule).ToList();
                else
                    ListIdentificationPeriodeUser = ListIdentificationPeriodeUser.OrderBy(o => o.Matricule).ToList();
                break;

            case "Nom":
                if (sortDir == "desc")
                    ListIdentificationPeriodeUser = ListIdentificationPeriodeUser.OrderByDescending(o => o.Nom).ToList();
                else
                    ListIdentificationPeriodeUser = ListIdentificationPeriodeUser.OrderBy(o => o.Nom).ToList();
                break;

            case "Prenom":
                if (sortDir == "desc")
                    ListIdentificationPeriodeUser = ListIdentificationPeriodeUser.OrderByDescending(o => o.Prenom).ToList();
                else
                    ListIdentificationPeriodeUser = ListIdentificationPeriodeUser.OrderBy(o => o.Prenom).ToList();
                break;

            case "DateDebut":

                if (sortDir == "desc")
                    ListIdentificationPeriodeUser = ListIdentificationPeriodeUser.OrderByDescending(o => o.DateDebut).ToList();
                else
                    ListIdentificationPeriodeUser = ListIdentificationPeriodeUser.OrderBy(o => o.DateDebut).ToList();
                break;

            case "DateFin":
                foreach (User user in ListIdentificationPeriodeUser)
                {
                    if (string.IsNullOrWhiteSpace(user.DateFin.ToString()) || string.IsNullOrEmpty(user.DateFin.ToString()))
                        user.DateFin = DateTime.Parse("01/01/1900");
                }
                if (sortDir == "desc")
                    ListIdentificationPeriodeUser = ListIdentificationPeriodeUser.OrderByDescending(o => o.DateFin).ToList();
                else
                    ListIdentificationPeriodeUser = ListIdentificationPeriodeUser.OrderBy(o => o.DateFin).ToList();
                break;

        }
        Session["IdentificationPeriodeUser"] = ListIdentificationPeriodeUser;

        GridviewIdentification.DataSource = ListIdentificationPeriodeUser;
        GridviewIdentification.DataBind();

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

    protected void user_Sorting(object sender, GridViewSortEventArgs e)
    {
        BindGridView(e.SortExpression, sortOrder);
        int columnIndex = 0;

        foreach (DataControlFieldHeaderCell headerCell in GridviewIdentification.HeaderRow.Cells)
        {
            if (headerCell.ContainingField.SortExpression == e.SortExpression)
            {
                columnIndex = GridviewIdentification.HeaderRow.Cells.GetCellIndex(headerCell);
            }
        }

        GridviewIdentification.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);

       
    }

    //-------------------------------------------------------------------------------------------
    
}