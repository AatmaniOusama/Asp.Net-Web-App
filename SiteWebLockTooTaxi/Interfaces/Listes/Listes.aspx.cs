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
using DataLockTooTaxi.Class;





public partial class Listes_index : System.Web.UI.Page
{ 

   
    User user = new User();

    Motif motif = new Motif();
    
    List<Listes> ListDeListes;
    List<User> listMembres;

    Listes listes = new Listes();

    UsersTableAdapter UserAdapeter = new UsersTableAdapter();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
       
            Operateur operateur = (Operateur)Session["Operateur"];

            if (operateur.Profil.ToUpper() == "ADMINISTRATEUR")
            {
                BarreControleListe.Rows[0].Cells[0].Visible = true;
                BarreControleListe.Rows[0].Cells[1].Visible = true;
                BarreControleListe.Rows[0].Cells[2].Visible = true;
                BarreControleListeMembres.Rows[0].Cells[0].Visible = true;
                BarreControleListeMembres.Rows[0].Cells[1].Visible = true;
            }

            if (operateur.Profil.ToUpper() == "SUPERVISEUR")
            {
                BarreControleListe.Rows[0].Cells[0].Visible = true;  // Ajouter Liste
                BarreControleListe.Rows[0].Cells[1].Visible = true;  // Modifier Liste
                BarreControleListe.Rows[0].Cells[2].Visible = true;  // Supprimer Liste
                BarreControleListeMembres.Rows[0].Cells[0].Visible = true; // Ajouter Membre à une liste
                BarreControleListeMembres.Rows[0].Cells[1].Visible = true; // Exprter liste des membres vers Excel
            }

            if (operateur.Profil.ToUpper() == "CONSULTANT")
            {
                BarreControleListe.Rows[0].Cells[0].Visible = true;
                BarreControleListe.Rows[0].Cells[1].Visible = false;
                BarreControleListe.Rows[0].Cells[2].Visible = false;

                BarreControleListeMembres.Rows[0].Cells[0].Visible = false;
                BarreControleListeMembres.Rows[0].Cells[1].Visible = false;
                Panel_Ajout.Visible = false;
            }

            if (operateur.MenusAutorises.Substring(0, 1) == "0")
            {
                Session.Abandon();
                Response.Redirect("~/");
            }

            if (!IsPostBack)
            {
                MPE.Hide();
               

                ViewState["sortOrder"] = "";

                LaodGridView();
         
            }
        }
        catch (Exception)
        {
        }
    }
 
    protected void LaodGridView()
    {

        ListDeListes = listes.GetListes();

        Session["ListDeListes"] = ListDeListes;
       
        GridViewListeListes.DataSource = ListDeListes;
        GridViewListeListes.DataBind();
        
        BtnSetListe.Visible = false;
        BtnDeleteListe.Visible = false;

        GridViewListeListes.SelectedIndex = -1;

    }

    protected void LoadGridViewListeMembre(int NumListe)
    {
        string numPermis = TbNumPermis.Text;
        if (numPermis == "" || numPermis == "N° Permis")
        {
            numPermis = null;
        }

        string matricule = TbMatricule.Text;
        if (matricule == "" || matricule == "Matricule")
        { matricule= null;
        }
        string nom = TbNom.Text;
        if (nom == "" || nom == "Nom")
        {
            nom = null;
        }
        string prenom = TbPrenom.Text;
        if (prenom == "" || prenom == "Prénom")
        {
            prenom = null;
        }
        string libelleMotif = TbMotif.Text;

        if (libelleMotif == "" ||libelleMotif == "Motif demande")
        {
            libelleMotif = null;
        }


        listMembres = user.getListesByNumList(NumListe, Session["TypeListe"].ToString(),numPermis, matricule, nom, prenom, libelleMotif);
        
        Session["listMembres"] = listMembres;
       
        GridViewListeMembres.DataSource = listMembres;
        GridViewListeMembres.DataBind();

        NbrLignes.Text = listMembres.Count().ToString();
        
       GridViewListeMembres.SelectedIndex = -1;
    }

    protected void BtnSetListe_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Interfaces/Listes/ModifierListe.aspx"); 
    }

    protected void BtnAddUserToList_Click(object sender, EventArgs e)
    {
        try
        {
        
            Response.Redirect("~/Interfaces/Listes/AjoutMembre.aspx");

            MPE.Hide();
           
        }
        catch (Exception)
        { }
    }

    protected void BtnDeleteListe_Click(object sender, EventArgs e)
    {
        try
        {
           
            GridViewRow row = GridViewListeListes.SelectedRow;
            int NumListe = int.Parse((row.FindControl("LblNumList") as Label).Text);
            Listes liste = new Listes();
            liste.SupprimerUneListe(NumListe);
            Response.Redirect("~/Interfaces/Listes/Listes.aspx");

        }
        catch (Exception)
        { }
    }

    //************  PopUp Ajout/Cancel Nouvelle Liste  *******************//

    protected void BtSave_Click(object sender, EventArgs e)
    {
       
        Listes item = new Listes();
        int i = 0;

        if (!(item.ListeExiste(TbLibelleListe.Text,TbAbrevListe.Text)))
        {
            item.Abrev = TbAbrevListe.Text;
            item.Libelle = TbLibelleListe.Text;
            item.Type = DDLTypeListe.SelectedValue;
            i = item.AjouterUneListe(item);
        }


        if (i == 1)
        {
            MPE.Hide();
           
            LaodGridView();
        }

    }
  
    protected void BtCancel_Click(object sender, EventArgs e)
    {
        MPE.Hide();

    }

    protected void BtSearch(object sender, EventArgs e)
    {
        LoadGridViewListeMembre(Convert.ToInt32(Session["NumListe"]));

    }
   
    protected void GridViewListeListes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GridViewListeListes.PageIndex = e.NewPageIndex;
        GridViewListeListes.DataSource = Session["ListDeListes"] as List<User>;
        GridViewListeListes.DataBind();
       
       
    }

    protected void GridViewListeMembres_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GridViewListeMembres.PageIndex = e.NewPageIndex;
        GridViewListeMembres.DataSource = Session["listMembres"] as List<User>;
        GridViewListeMembres.DataBind();


    }
   
    protected void GridViewListeListes_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            GridViewRow row = GridViewListeListes.Rows[e.NewSelectedIndex];

            int NumListe = int.Parse((row.FindControl("LblNumList") as Label).Text);
            Session["NumListe"] = NumListe;
            Session["TypeListe"] = (row.FindControl("LblType") as Label).Text;

            LoadGridViewListeMembre(NumListe);
            PanelBarreControleListe.Visible = true;


            BtnSetListe.Visible = true;
            BtnDeleteListe.Visible = true;
        }
        catch (Exception)
        { }
        
    }

    protected void GridViewListeListes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("OnClick", String.Format("javascript:__doPostBack('ctl00$MainContent$GridViewListeListes','Select${0}')", e.Row.RowIndex));
           
        }

    }

    protected void GridViewListeMembres_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ListesUsers Listesusers = new ListesUsers();
        GridViewRow row = GridViewListeMembres.Rows[e.RowIndex];
        int i = 0;
        string matricule = ((row.FindControl("ImgDelete") as ImageButton).CommandArgument).ToString();
        string typeListe = Session["TypeListe"].ToString();

        if (typeListe == "Chauffeurs demandés")
        {
            int IdUser = user.GetIdUser(matricule);
            i = Listesusers.SupprimerUnUserFromListe(IdUser, Convert.ToInt32(Session["NumListe"]));
        }
      
        if (i == 1)
        {
            GridViewListeMembres.EditIndex = -1;

            LoadGridViewListeMembre(Convert.ToInt32(Session["NumListe"]));
        }
    }

   
   
    public override void VerifyRenderingInServerForm(Control control)
    {
    }

}
