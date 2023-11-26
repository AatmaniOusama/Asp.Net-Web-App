using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLockTooTaxi;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using DataLockTooTaxi.MyDataSetTableAdapters;
using DataLockTooTaxi.Class;


public partial class Interfaces_Historique_InsererPointage : System.Web.UI.Page
{


    Events events = new Events();
    TypeTaxi typeTaxi = new TypeTaxi();
    Lecteur lecteur = new Lecteur();
    CodesMissions codeRefus = new CodesMissions();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                User user= new User();
                List<User> listUser = user.GetNomsComplets();
                var ListNomPrenom = listUser.Select(p => new { NumBadge = p.NumBadge, Nom = p.Nom, DisplayText = p.Nom.ToString() + " " + p.Prenom.ToString() });

                DDLChauffeur.DataSource = ListNomPrenom;
                DDLChauffeur.DataValueField = "NumBadge";
                DDLChauffeur.DataTextField = "DisplayText";
                DDLChauffeur.DataBind();

                DDLLecteur.DataSource = lecteur.GetLecteurs();
                DDLLecteur.DataTextField = "NomLecteur";
                DDLLecteur.DataValueField = "Adresse";
                DDLLecteur.DataBind();
                DDLLecteur.Items.Remove(DDLLecteur.Items.FindByText("Tous"));

                DDLTypeTaxi.DataSource = typeTaxi.getTypeTaxi();
                DDLTypeTaxi.DataTextField = "Libelle";
                DDLTypeTaxi.DataValueField = "Num";
                DDLTypeTaxi.DataBind();
                DDLTypeTaxi.Items.Remove(DDLTypeTaxi.Items.FindByText("Tous"));


                DDLCodeRefus.DataSource = codeRefus.getCodesRefus();
                DDLCodeRefus.DataTextField = "LibelleCode";
                DDLCodeRefus.DataValueField = "ValeurCode";
                DDLCodeRefus.DataBind();
                DDLCodeRefus.Items.Remove(DDLCodeRefus.Items.FindByText(""));
            }            
        }

        catch (Exception)
        {
            Response.Redirect("~/Interfaces/Historique/InsererPointage.aspx");
        }
    }

    protected void TbNumPermis_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string numPermis = TbNumPermis.Text;
            DDLChauffeur.SelectedItem.Selected = false;
            DDLChauffeur.Items.FindByValue(numPermis).Selected = true;
        }
        catch (Exception)
        {
            DDLChauffeur.SelectedItem.Selected = false;
            DDLChauffeur.Items[0].Selected = true;
        }

    }

    protected void DDLChauffeur_SelectedIndexChanged(object sender, EventArgs e)
    {        
        string numPermis = DDLChauffeur.SelectedValue;
        TbNumPermis.Text = numPermis;       
    }

    protected void BtnSavePointageClick(object sender, EventArgs e)
    {
        Page.Validate();

        if (Page.IsValid)
        {
            Operateur operateur = (Operateur)Session["Operateur"];
            Events ev = new Events();              

            ev.NumLecteur=int.Parse(DDLLecteur.SelectedValue);
            ev.Nom = DDLChauffeur.SelectedItem.Text;      
            ev.NumBadge = TbNumPermis.Text;              
            ev.NumTaxi = TbNumTaxi.Text;
            ev.TypeTaxi = int.Parse(DDLTypeTaxi.SelectedValue);
            ev.CodeRefus = DDLCodeRefus.SelectedValue;
            ev.MatriculeAdmin = operateur.Login.ToString();

            ev.Reference = ev.GetReferenceByNumPermis(TbNumPermis.Text);
           int i= ev.AjouterUnEvent(ev);

             if (i==1)
             {
                string myStringVariable = "Pointage ajouté avec succès ! ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + myStringVariable + "')", true);
                Response.Redirect("~/Interfaces/Historique/Historique.aspx");
             }    
        }             
   }
        
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Interfaces/Historique/Historique.aspx");
    }


}