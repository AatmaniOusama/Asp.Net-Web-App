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

public partial class Interfaces_User_AutorisationsQPU : System.Web.UI.Page
{


      
    Listes listes = new Listes();
   AutorisationQPU autQPU = new AutorisationQPU(); 
    List<AutorisationQPU> ListAutorisationQPU;
   

    protected void Page_Load(object sender, EventArgs e)
    {
       

        try
        {
            TbNomChauffeur.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbPrenomChauffeur.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbMatriculeChauffeur.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
           

           

            Operateur operateur = (Operateur)Session["Operateur"];

            if (operateur.Profil.ToUpper() == "ADMINISTRATEUR")
            {       
                BarreControle.Rows[0].Cells[2].Visible = true;
                BarreControle.Rows[0].Cells[3].Visible = true;
                BarreControle.Rows[0].Cells[4].Visible = true;
                
            
              
            }

            if (operateur.Profil.ToUpper() == "SUPERVISEUR")
            {
                BarreControle.Rows[0].Cells[2].Visible = true;  // Exporter vers Excel
                BarreControle.Rows[0].Cells[3].Visible = true;  // Ajouter AQPU
                BarreControle.Rows[0].Cells[4].Visible = true;  // Autoriser Interdire AQPU
                
               
            }



            if (operateur.Profil.ToUpper() == "CONSULTANT")
            {
                BarreControle.Rows[0].Cells[2].Visible = true;
                BarreControle.Rows[0].Cells[3].Visible = false;
                BarreControle.Rows[0].Cells[4].Visible = false;
                
         
               
            }
           

            if (operateur.MenusAutorises.Substring(0, 1) == "0")
            {
                Session.Abandon();
                Response.Redirect("~/");
            }



            if (!IsPostBack)
            {

                ViewState["sortOrder"] = "";

               
                DDLListes.DataSource = listes.getListesByType(1);
                DDLListes.DataValueField = "Num_liste";
                DDLListes.DataTextField = "Libelle";
                DDLListes.DataBind();


                 if (Session["DateDebutAQPU"] as DateTime? != null)
                    TbDateDebut.SelectedDate = Session["DateDebutAQPU"] as DateTime?;
                else
                    TbDateDebut.SelectedDate = DateTime.Parse(DateTime.Now.ToString().Substring(0, 10) + " 00:00:00");

                if (Session["DateFinAQPU"] as DateTime? != null)
                    TbDateFin.SelectedDate = Session["DateFinAQPU"] as DateTime?;
                else
                    TbDateFin.SelectedDate = DateTime.Parse(DateTime.Now.ToString().Substring(0, 10) + " 23:59:59");

                TbNomChauffeur.Text = Session["NomChauffeurAQPU"] as string;
                TbMatriculeChauffeur.Text = Session["MatriculeChauffeurAQPU"] as string;
                DDLListes.SelectedValue = Session["ListesAQPU"] as string;


                LaodGridView();


            }
        }
        catch (Exception)
        {
        }
    }

    protected void LaodGridView()
    {
        ListAutorisationQPU = autQPU.getAutorisationQPU();

        if (DDLListes.SelectedValue != "-1")
        {
            ListAutorisationQPU = autQPU.getAutorisationQPU_NumListe();
            ListAutorisationQPU = ListAutorisationQPU.FindAll(delegate(AutorisationQPU item) { return item.NumListe == int.Parse(DDLListes.SelectedValue); });
        }

        if (!TbDateDebut.IsEmpty)
            ListAutorisationQPU = ListAutorisationQPU.FindAll(delegate(AutorisationQPU item) { return item.DateDebut >= TbDateDebut.SelectedDate.Value; });

        if (!TbDateFin.IsEmpty)
            ListAutorisationQPU = ListAutorisationQPU.FindAll(delegate(AutorisationQPU item) { return item.DateFin <= TbDateFin.SelectedDate.Value; });
       
        if (TbNomChauffeur.Text != "")
            ListAutorisationQPU = ListAutorisationQPU.FindAll(delegate(AutorisationQPU item) { Regex myRegex = new Regex(@"^(" + TbNomChauffeur.Text.ToUpper() + ")"); return myRegex.IsMatch(item.NomUser); });
        
        if (TbPrenomChauffeur.Text != "")
            ListAutorisationQPU = ListAutorisationQPU.FindAll(delegate(AutorisationQPU item) { Regex myRegex = new Regex(@"^(" + TbPrenomChauffeur.Text.ToUpper() + ")"); return myRegex.IsMatch(item.PrenomUser); });

        if (TbMatriculeChauffeur.Text != "")
            ListAutorisationQPU = ListAutorisationQPU.FindAll(delegate(AutorisationQPU item) { Regex myRegex = new Regex(@"^(" + TbMatriculeChauffeur.Text.ToUpper() + ")"); return myRegex.IsMatch(item.MatriculeUser); });



       

        if ((RbAutoriseAQPU.SelectedValue == "Y"|| RbAutoriseAQPU.SelectedValue == "N"))
            ListAutorisationQPU = ListAutorisationQPU.FindAll(delegate(AutorisationQPU item) { return item.Valide == RbAutoriseAQPU.SelectedItem.Value; });

        Session["ListAutorisationQPU"] = ListAutorisationQPU.OrderBy(a => int.Parse(a.NumAuto)).ToList(); 



        GridViewAutorisations.DataSource = ListAutorisationQPU.OrderBy(a=> int.Parse(a.NumAuto)).ToList();
        GridViewAutorisations.DataBind();


        //int nbrRows = GridViewAutorisations.Rows.Count;
        //int heightGrid = nbrRows * 25;

        //if (heightGrid <= 470 && nbrRows <= 100)
        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader2('" + GridViewAutorisations.ClientID + "', " + heightGrid + 25 + ", 950 , 40 ,false); </script>", false);

        //if (heightGrid >= 470 && nbrRows < 100)

        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GridViewAutorisations.ClientID + "', 470, 950 , 40 ,false); </script>", false);

        //if (heightGrid >= 470 && nbrRows >= 100)

        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GridViewAutorisations.ClientID + "', 470, 950 , 40 ,true); </script>", false);


        NbrLignes.Text = ListAutorisationQPU.Count().ToString();

        BtInterdire.Visible = false;
        BtAutoriser.Visible = false;       
       


        GridViewAutorisations.SelectedIndex = -1;
    }

    protected void Filtre(object sender, EventArgs e)
    {
        Session["DateDebutAQPU"] = TbDateDebut.SelectedDate.Value;
        Session["DateFinAQPU"] = TbDateFin.SelectedDate.Value;
        Session["NomChauffeurAQPU"] = TbNomChauffeur.Text;
        Session["MatriculeChauffeurAQPU"] = TbMatriculeChauffeur.Text;
        Session["ListesAQPU"] = DDLListes.SelectedValue;

         
        LaodGridView();
    }


    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Session["AQPAjout"] = true;
        Response.Redirect("~/Interfaces/User/DetailQPU.aspx");
    }

   




    protected void GridViewAutorisations_IndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewAutorisations.PageIndex = e.NewPageIndex;
        GridViewAutorisations.DataSource = Session["ListAutorisationQPU"] as List<AutorisationQPU>;
        GridViewAutorisations.DataBind();

        //int nbrRows = GridViewAutorisations.Rows.Count;
        //int heightGrid = nbrRows * 25;

        //if (heightGrid <= 470 && nbrRows <= 100)
        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader2('" + GridViewAutorisations.ClientID + "', " + heightGrid + 25 + ", 950 , 40 ,false); </script>", false);

        //if (heightGrid >= 470 && nbrRows < 100)

        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GridViewAutorisations.ClientID + "', 470, 950 , 40 ,false); </script>", false);

        //if (heightGrid >= 470 && nbrRows >= 100)

        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GridViewAutorisations.ClientID + "', 470, 950 , 40 ,true); </script>", false);

    }

    protected void CbFiltre_CheckedChanged(object sender, EventArgs e)
    {
        Panelfilter.Visible = CbFiltre.Checked;

    }

    protected void GridViewAutorisations_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow row = GridViewAutorisations.Rows[e.NewSelectedIndex];


        string Valide = (row.FindControl("LblValide") as Label).Text;



       
        

        BtAutoriser.Visible = (Valide != "Y");
        BtInterdire.Visible = (Valide == "Y");

        //int nbrRows = GridViewAutorisations.Rows.Count;
        //int heightGrid = nbrRows * 25;

        //if (heightGrid <= 470 && nbrRows <= 100)
        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader2('" + GridViewAutorisations.ClientID + "', " + heightGrid + 25 + ", 950 , 40 ,false); </script>", false);

        //if (heightGrid >= 470 && nbrRows < 100)

        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GridViewAutorisations.ClientID + "', 470, 950 , 40 ,false); </script>", false);

        //if (heightGrid >= 470 && nbrRows >= 100)

        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GridViewAutorisations.ClientID + "', 470, 950 , 40 ,true); </script>", false);



    //    BtnUpdateLecteurNow.Visible = true;


    }

    protected void GridViewAutorisations_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Add onclick attribute to select row.
            e.Row.Attributes.Add("onclick", String.Format("javascript:__doPostBack('ctl00$MainContent$GridViewAutorisations','Select${0}')", e.Row.RowIndex));

            if ((e.Row.Cells[9].FindControl("LblValide") as Label).Text == "Y")
            {
                e.Row.Cells[0].FindControl("ImgInvalide").Visible = false;
                e.Row.Cells[0].FindControl("ImgValide").Visible = true;
            }
            if ((e.Row.Cells[9].FindControl("LblValide") as Label).Text == "N")
            {
                e.Row.Cells[0].FindControl("ImgInvalide").Visible = true;
                e.Row.Cells[0].FindControl("ImgValide").Visible = false;
            }
        
        }

        
    }

    protected void GridViewAutorisations_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
  

    protected void BtAutoriser_Click(object sender, EventArgs e)
    {
        GridViewRow row = GridViewAutorisations.Rows[GridViewAutorisations.SelectedIndex];
        AutorisationQPU AutoQPU = new AutorisationQPU();

        string matricule = (row.FindControl("LblMatricule") as Label).Text;


        int i = AutoQPU.ModifierValidite(true, matricule);

        if (i == 1)
        {
            BtInterdire.Visible = true;
            BtAutoriser.Visible = false;
        }

    }

    protected void BtInterdir_Click(object sender, EventArgs e)
    {
        GridViewRow row = GridViewAutorisations.Rows[GridViewAutorisations.SelectedIndex];
        AutorisationQPU AutoQPU = new AutorisationQPU();

        string matricule = (row.FindControl("LblMatricule") as Label).Text;


        int i = AutoQPU.ModifierValidite(false, matricule);

        if (i == 1)
        {
            BtInterdire.Visible = false;
            BtAutoriser.Visible = true;
        }

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

        GridViewAutorisations.AllowPaging = false;
        GridViewAutorisations.AllowSorting = false;
        GridViewAutorisations.ShowFooter = false;

        GridViewAutorisations.EditIndex = -1;

      
        GridViewAutorisations.Columns[0].Visible = false;
        GridViewAutorisations.Columns[1].Visible = false;


       



        GridViewAutorisations.DataSource = Session["ListAutorisationQPU"] as List<AutorisationQPU>;
        GridViewAutorisations.DataBind();

        PrepareGridViewForExport(GridViewAutorisations);

        mypage.Controls.Add(form);
        form.Controls.Add(GridViewAutorisations);

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
                gvcontrol.Controls.AddAt(i,
                new LiteralControl((current as Label).Text));
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

    }
