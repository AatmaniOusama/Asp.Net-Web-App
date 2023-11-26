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


public partial class Interfaces_Operateur_AjoutOperateur : System.Web.UI.Page
{
    Operateur operateur = new Operateur();
    Service service = new Service();
 

    ControleUser controleUser = new ControleUser();

    bool OperateurAjout;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
          

            OperateurAjout = bool.Parse(Session["AjoutOperateur"].ToString());

            if (!IsPostBack)
            {

                DDLService.DataSource = service.GetServices();
                DDLService.DataValueField = "Numero";
                DDLService.DataTextField = "Libelle";
                DDLService.DataBind();
               

               

                if (!OperateurAjout)
                {
                    Page.Title = "Modifier Autorisation";
                    operateur = operateur.GetOperateurByID(int.Parse(Session["ModifOperateur"].ToString()));
                    RemplirChampsOperateur(operateur);
                    //Session["ImmatVehicule"] = vehicule.Immat;
                }
            }
        }

        catch (Exception)
        {
            Response.Redirect("~/Interfaces/Operateur/Operateur.aspx");
        }
    }

    protected void RemplirChampsOperateur(Operateur op)
    {
        TbNom.Text = op.Nom;
        TbPrenom.Text = op.Prenom;

        DDLProfil.Items.FindByText(op.Profil).Selected = true;
       
        TbDateDebutValidite.Text = op.Debut.ToString().Substring(0, 10);
        TbDateFinValidite.Text = op.Fin.ToString().Substring(0, 10);
        DDLService.Items.FindByText(op.NomService).Selected = true;
        UserName.Text = op.Login;
        //Password.Text = op.MotPasse;
        Password.Attributes.Add("value", op.MotPasse); //pour écrire dans le TextBox en mode Password
       

    }

     

    protected void BtnSaveAjoutOpClick(object sender, EventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            Operateur operateur = (Operateur)Session["Operateur"];
            Operateur item = new Operateur();

            item.Nom = TbNom.Text;
            item.Prenom = TbPrenom.Text;
            item.Profil = DDLProfil.SelectedItem.Text;
            item.IDCreateur = operateur.ID;
            item.Debut = DateTime.Parse(TbDateDebutValidite.Text);
            item.Fin = DateTime.Parse(TbDateFinValidite.Text);
            item.NomService = DDLService.SelectedItem.Text;
            item.Login = UserName.Text;
            item.MotPasse = Password.Text;

            //Menus autorisés --------------------------------------------------------------------------------

            if (item.Profil == "ADMINISTRATEUR")
            {
                item.MenusAutorises = "111111111111111111111111111111111111111111111111111111111110";
            }
            else if (item.Profil == "SUPERVISEUR")
            {
                item.MenusAutorises = "111111111111111111011011111111101101111111110111111111111110";
            }
            else if (item.Profil == "CONSULTANT")
            {
                item.MenusAutorises = "100010101000010100001000010010100000010000000100010101010000";
            }

            //------------------------------------------------------------------------------------------------

            int AjoutOK =item.AjouterUnOperateur(item);




            if (AjoutOK == 1)
            {
                string myStringVariable = "Autorisation Quiter Périmètre Urbain ajouté avec succès ! ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + myStringVariable + "')", true);

                Response.Redirect("~/Interfaces/Operateur/Operateur.aspx");
            }
            else
            {
                string myStringVariable = "Impossible d'ajouter cet opérateur, veuillez réessayer attentivement";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + myStringVariable + "')", true);

                Response.Redirect("~/Interfaces/Operateur/Operateur.aspx");
            }
        }
    
    }

   

    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Interfaces/Operateur/Operateur.aspx");
    }


}