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


public partial class Interfaces_User_DetailQPU : System.Web.UI.Page
{
    AutorisationQPU AutoQPU = new AutorisationQPU();

    Agrement agrement = new Agrement();

    Listes listes = new Listes();

    ControleUser controleUser = new ControleUser();

    bool AQPAjout;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
         
      
            AQPAjout = bool.Parse(Session["AQPAjout"].ToString());

            if (!IsPostBack)
            {

                DDLListes.DataSource = listes.getListesByType(1);
                DDLListes.DataValueField = "Num_liste";
                DDLListes.DataTextField = "Libelle";
                DDLListes.DataBind();
                DDLListes.Items.Remove(DDLListes.Items.FindByText(" Tous "));

                User user= new User();
                List<User> listUser = user.GetNomsComplets();
                var ListNomPrenom = listUser.Select(p => new { Matricule = p.Matricule, Nom = p.Nom, DisplayText = p.Nom.ToString() + " " + p.Prenom.ToString() });

                DDLChauffeur.DataSource = ListNomPrenom;
                DDLChauffeur.DataValueField = "Matricule";
                DDLChauffeur.DataTextField = "DisplayText";
                DDLChauffeur.DataBind();

                if (!AQPAjout)
                {
                    Page.Title = "Modifier Autorisation";
                    AutoQPU = AutoQPU.getAutorisationQPUByNum(Session["ModifAutorisation"].ToString());
                    RemplirChampsAutorisation(AutoQPU);
                   //Session["ImmatVehicule"] = vehicule.Immat;
                }
            }            
        }

        catch (Exception)
        {
            Response.Redirect("~/Interfaces/User/AutorisationsQPU.aspx");
        }
    }

    protected void RemplirChampsAutorisation(AutorisationQPU AQPU)
    {
        TbDateDebutAQP.Text = AQPU.DateDebut.ToString().Substring(0, 10);
        TbDateFinAQP.Text = AQPU.DateFin.ToString().Substring(0, 10);
        TbNumAutorisation.Text = AQPU.NumAuto;
        TbNumAgrement.Text = AQPU.NumAgrement;
        TbDestination.Text = AQPU.Destination;

        //DDLListes.SelectedItem.Text=
        //DDLChauffeur.SelectedItem.Text=
        //TbMatriculeChauffeur.Text = 
        TbNomPassager1.Text = AQPU.NomPassager1;
        TbPrenomPassager1.Text = AQPU.PrenomPassager1;
        TbCINPassager1.Text = AQPU.CinPassager1;
        TbNomPassager2.Text = AQPU.NomPassager2;
        TbPrenomPassager2.Text = AQPU.PrenomPassager2;
        TbCINPassager2.Text = AQPU.CinPassager2;
      
    }

    protected void TbMatricule_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string Matricule = TbMatricule.Text;

            DDLChauffeur.SelectedItem.Selected = false;
            DDLChauffeur.Items.FindByValue(Matricule).Selected = true;
        }
        catch (Exception)
        {
            DDLChauffeur.SelectedItem.Selected = false;
            DDLChauffeur.Items[0].Selected = true;
        }

    }

    protected void DDLChauffeur_SelectedIndexChanged(object sender, EventArgs e)
    {        
            string matricule = DDLChauffeur.SelectedValue;
            TbMatricule.Text = matricule;       
    }

    protected void BtnSaveAQPClick(object sender, EventArgs e)
    {
       Page.Validate();
        if (Page.IsValid)
        {
                Operateur operateur = (Operateur)Session["Operateur"];   
                AutorisationQPU item = new AutorisationQPU();

                item.DateCreation = DateTime.Now;
                item.IdOpCreation = operateur.ID;
                item.DateModif = DateTime.Now;
                item.IdOpModif = operateur.ID;                
                item.DateDebut = DateTime.Parse(TbDateDebutAQP.Text);
                item.DateFin = DateTime.Parse(TbDateFinAQP.Text);
                item.NumAuto = TbNumAutorisation.Text;
                item.NumAgrement = TbNumAgrement.Text;
                item.Destination = TbDestination.Text;
                item.Valide = "True";
            //  item.Listes = DDLListes.SelectedItem.Text;        
                item.MatriculeUser = TbMatricule.Text;

                item.NomPassager1 = TbNomPassager1.Text;
                item.PrenomPassager1 = TbPrenomPassager1.Text;
                item.CinPassager1 = TbCINPassager1.Text;
                item.NomPassager2 = TbNomPassager2.Text;
                item.PrenomPassager2 = TbPrenomPassager2.Text;
                item.CinPassager2 = TbCINPassager2.Text;

                if (DDLChauffeur.Enabled == true)
                {
                    item.insertAutoQPU(item);
                }
                if (DDLListes.Enabled == true)
                {
                    int numListe = int.Parse(DDLListes.SelectedValue);
                    User user = new User();
                    List<User> listeUser = user.getListesByNumList(numListe,"Chauffeurs",null ,null, null, null, null);
                    foreach (User user1 in listeUser)
                    {
                        item.MatriculeUser = user1.Matricule;
                        item.insertAutoQPU(item);
                    
                    }
                   
                }


                  
                    string myStringVariable = "Autorisation Quiter Périmètre Urbain ajouté avec succès ! ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + myStringVariable + "')", true);
                   
                    Response.Redirect("~/Interfaces/User/AutorisationsQPU.aspx");
         }
        
    }

    protected void Rbchoix_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Rbchoix.SelectedValue == "Liste")
        {  
            DDLListes.Enabled = true;
            DDLChauffeur.Enabled=false;        
            TbMatricule.Enabled = false;
        }

        if (Rbchoix.SelectedValue == "Chauffeur")
        {
            DDLListes.Enabled = false;
            DDLChauffeur.Enabled = true;
            TbMatricule.Enabled = true;
        }

    }

    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Interfaces/User/AutorisationsQPU.aspx");
    }


}