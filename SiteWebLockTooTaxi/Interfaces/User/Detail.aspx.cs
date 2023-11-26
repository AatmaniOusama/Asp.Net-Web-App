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
using System.Drawing.Printing;
using System.Windows.Forms;

public partial class Interfaces_User_Detail : System.Web.UI.Page
{
    User user = null;

    Droits droit = null;

    Service service = null;
    Badge badge = null;

    protected void Page_Load(object sender, EventArgs e)
    {  //-----Date:27/01/2017: Zouhair LOUALID-----------------Ajout de commentaire dans le formulaire--------------------------------------
           
        try
        {
            user = new User();

            droit = new Droits();

            service = new Service();
            badge = new Badge();


            TbNom.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbPrenom.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbMatricule.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbLNaissance.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbAdresse.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbCodePostale.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbVille.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbTel.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbNSS.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbNCI.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbDateDelivrance.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbPermis.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbAncienNP.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");


            Operateur operateur = (Operateur)Session["Operateur"];

            if (operateur.Profil == "ADMINISTRATEUR")
            {
                BtnSet.Visible = true;
                ScriptManager.RegisterStartupScript(Page, GetType(), "CacheItems", "<script>CacheItems()</script>", false);
            }
            if (operateur.Profil == "CONSULTANT")
            {
                BtnSet.Enabled = false;

            }
         


            bool ChampsEnabled = bool.Parse(Session["ChampsEnabled"].ToString());
            bool Ajout = bool.Parse(Session["Ajout"].ToString());

            Page.Title = "La Fiche de Chauffeur";

            if (!Ajout)
            {

                user = user.GetUnUser(Session["DetailsFor"].ToString());
            }

            Session["item"] = user;

            photo.ImageUrl = "~/Photo.ashx/?Id=" + user.Matricule;

            if (!IsPostBack)
            {

                DDLService.DataSource = service.GetServices();
                DDLService.DataValueField = "Numero";
                DDLService.DataTextField = "Libelle";
                DDLService.DataBind();

                DDLDroit.DataSource = droit.GetDroitsAcces();
                DDLDroit.DataValueField = "NumDroit";
                DDLDroit.DataTextField = "LibelleDroit";
                DDLDroit.DataBind();

                Badge item = new Badge();
                item.NumBadge = user.NumBadge;

                List<Badge> ListBadges = new List<Badge>();

                ListBadges.Add(item);
                ListBadges.AddRange(badge.GetBadgesAutorise());


                DDLBadge.DataSource = ListBadges;
                DDLBadge.DataValueField = "NumBadge";
                DDLBadge.DataTextField = "NumBadge";
                DDLBadge.DataBind();

                RemplirChamps(user);
                EnabledIdentite(ChampsEnabled, Ajout);
                EnabledProfil(ChampsEnabled);

            }



        }
        catch (Exception)
        {
            Response.Redirect("~/Interfaces/User/Users.aspx");
        }
    }

    protected void RemplirChamps(User item)
    {
        bool Ajout = bool.Parse(Session["Ajout"].ToString());

        RbSexe.Items.FindByValue(item.Sexe.ToString()).Selected = true;

        TbNom.Text = item.Nom;

        TbPrenom.Text = item.Prenom;

        TbMatricule.Text = item.Matricule;

        DDLCivilite.Items.FindByValue(item.Civilite.ToString()).Selected = true;

        TbDNaissance.Text = item.DateNaissance;

        TbLNaissance.Text = item.LieuNaiss;

        TbAdresse.Text = item.Adresse1;
        TbObservation.Text = item.Observations;
        //TbCommentaire.Text = item.Commentaire;

        TbCodePostale.Text = item.CodePostal;

        TbVille.Text = item.Ville;

        TbTel.Text = item.Telephone;

        TbEmail.Text = item.EMail;

        TbNSS.Text = item.NumSS;

        TbNCI.Text = item.NumCI;

        TbDateDelivrance.Text = "";

        TbPermis.Text = item.NumPermis;

        TbDateDelivrancePermis.Text = "";

        RbFlagAutorise.Items.FindByValue(item.FlagAutorise).Selected = true;

        if (!Ajout)
        {
            TbDateCreation.Text = item.DateCreat.ToString();
            TbDateModif.Text = item.DateModif.ToString();
        }
        else
        {
            TbDateCreation.Text = "";
            TbDateModif.Text = "";
        }


        TbDateDebut.Text = DateTime.Parse(item.DateDebut.ToString()).Date.ToShortDateString();

        TbDateFin.Text = DateTime.Parse(item.DateFin.ToString()).Date.ToShortDateString();

        DDLService.Items.FindByValue(item.NumService.ToString()).Selected = true;

        DDLDroit.Items.FindByValue(item.NumDroitAcces.ToString()).Selected = true;

        CbBadge.Checked = item.CheckBadge;

        TbAncienNP.Text = item.AncienNumBadge;

        DDLBadge.Items.FindByText(item.NumBadge).Selected = true;

        //if (item.Photo != null)
        //{
        //    //On place l'image dans un fichier temporaire
        //    MemoryStream stream = new MemoryStream();
        //    stream.Write(item.Photo, 0, item.Photo.Length);
        //    Bitmap bitmap = new Bitmap(stream);
        //    Response.ContentType = "icons/JPEG";

        //    //Affichage de l'image
        //    bitmap.Save(Response.OutputStream, ImageFormat.Jpeg);
        //    photo.ImageUrl = "~/icons/" + item.Matricule + ".jpg";
        //}


    }

    protected void EnabledIdentite(bool enabled, bool Matricule)
    {

        RbSexe.Enabled = enabled;

        TbNom.Enabled = enabled;

        TbPrenom.Enabled = enabled;

        TbMatricule.Enabled = Matricule;

        DDLCivilite.Enabled = enabled;

        TbDNaissance.Enabled = enabled;

        TbLNaissance.Enabled = enabled;

        TbAdresse.Enabled = enabled;


        TbObservation.Enabled = enabled;

        //TbCommentaire.Enabled = enabled;

        TbCodePostale.Enabled = enabled;

        TbVille.Enabled = enabled;

        TbTel.Enabled = enabled;

        TbEmail.Enabled = enabled;

        TbNSS.Enabled = enabled;

        TbNCI.Enabled = enabled;

        TbDateDelivrance.Enabled = enabled;

        TbPermis.Enabled = enabled;
        TbDateDelivrancePermis.Enabled = enabled;

        RbFlagAutorise.Enabled = enabled;

        if (!enabled)
        {
            TdSave.Visible = false;
            txtSearch.Visible = false;
            TdSet.Visible = true;
        }
        else
        {
            TdSave.Visible = true;
            

            txtSearch.Visible = true;
            TdSet.Visible = false;
            ScriptManager.RegisterStartupScript(Page, GetType(), "CacheItems", "<script>CacheItems()</script>", false);
        }

    }

    protected void EnabledProfil(bool enabled)
    {
        if (RbFlagAutorise.SelectedValue != "Y")
            enabled = false;

        TbDateDebut.Enabled = enabled;

        TbDateFin.Enabled = enabled;

        DDLDroit.Enabled = enabled;

        DDLService.Enabled = enabled;

        CbBadge.Enabled = enabled;

        if (CbBadge.Checked)
            DDLBadge.Enabled = enabled;

        TbAncienNP.Enabled = enabled;

    }

    protected void CbBadge_CheckedChanged(object sender, EventArgs e)
    {
        if (CbBadge.Checked == true)
            DDLBadge.Enabled = true;
        else DDLBadge.Enabled = false;
    }

    protected void BtnBack_Click(object sender, EventArgs e)
    {
        //if( Session["currentPage"].ToString() =="0")
        if (!bool.Parse(Session["Ajout"].ToString()))
        {
            Session["SelectUserSet"] = true;
        }
        else
        {
            Session["SelectUserAdd"] = false;
        }

        Response.Redirect("~/Interfaces/User/Users.aspx");

        //if (Session["currentPage"].ToString() == "5")
        Response.Redirect("~/Interfaces/Historique/Historique.aspx");
    }

    protected void RbFlagAutorise_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (RbFlagAutorise.SelectedValue == "Y")
            EnabledProfil(true);
        else
            EnabledProfil(false);

        EnabledIdentite(true, bool.Parse(Session["Ajout"].ToString()));

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        bool Ajout = bool.Parse(Session["Ajout"].ToString());
        Page.Validate();
        if (Page.IsValid)
        {
            User item = Session["item"] as User;

            //**********************************************************//
            // remplir la class user par les valeurs des champs de saisi//
            //**********************************************************//
            if (TbMatricule.Text != "")
                item.Matricule = TbMatricule.Text;
            else
                item.Matricule = null;

            if (TbNom.Text != "")
                item.Nom = TbNom.Text;
            else
                item.Nom = null;

            if (TbPrenom.Text != "")
                item.Prenom = TbPrenom.Text;
            else
                item.Prenom = null;

            item.NumBadge = DDLBadge.SelectedValue;
            item.NumService = int.Parse(DDLService.SelectedValue);
            item.NumDroitAcces = int.Parse(DDLDroit.SelectedValue);
            item.FlagAutorise = RbFlagAutorise.SelectedValue;
            item.DateDebut = DateTime.Parse(TbDateDebut.Text);
            item.DateFin = DateTime.Parse(TbDateFin.Text);

            item.Modified = true;

            if (!Ajout)
            {
                item.DateModif = DateTime.Now;
            }
            else
            {
                item.DateCreat = DateTime.Now;
                item.DateModif = DateTime.Now;
            }

            item.Sexe = int.Parse(RbSexe.SelectedValue);
            item.Civilite = int.Parse(DDLCivilite.SelectedValue);

            if (TbDNaissance.Text != "")
                item.DateNaissance = TbDNaissance.Text;
            else
                item.DateNaissance = null;

            if (TbLNaissance.Text != "")
                item.LieuNaiss = TbLNaissance.Text;
            else
                item.LieuNaiss = null;

            if (TbTel.Text != "")
                item.Telephone = TbTel.Text;
            else
                item.Telephone = null;

            if (TbEmail.Text != "")
                item.EMail = TbEmail.Text;
            else
                item.EMail = null;

            if (TbNCI.Text != "")
                item.NumCI = TbNCI.Text;
            else
                item.NumCI = null;

            if (TbNSS.Text != "")
                item.NumSS = TbNSS.Text;
            else
                item.NumSS = null;

            if (TbAdresse.Text != "")
                item.Adresse1 = TbAdresse.Text;
            else
                item.Adresse1 = null;

            if (TbObservation.Text != "")
                item.Observations = TbObservation.Text;
            else
                item.Observations = null;
/*
            if (TbCommentaire.Text != "")
                item.Commentaire = TbCommentaire.Text;
            else
                item.Commentaire = null;*/
            //---------------------------------------------------------------------------------------------------------------------------
            

            if (TbCodePostale.Text != "")
                item.CodePostal = TbCodePostale.Text;
            else
                item.CodePostal = null;

            if (TbVille.Text != "")
                item.Ville = TbVille.Text;
            else
                item.Ville = null;

            item.CheckBadge = CbBadge.Checked;

            if (TbPermis.Text != "")
                item.NumPermis = TbPermis.Text;
            else
                item.NumPermis = null;

            if (TbDateDelivrancePermis.Text != "")
                item.DatePermis = TbDateDelivrancePermis.Text;
            else
                item.DatePermis = null;


            if (TbAncienNP.Text != "")
                item.AncienNumBadge = TbAncienNP.Text;
            else
                item.AncienNumBadge = null;

            /***Fin***/

            int EtatOperation = 0;

            //**********************************************//
            //s'il s'agit d'une Ajout =>Foction AjouterUser //
            //Si non => Fonction ModifierUser               //
            //**********************************************//
            if (bool.Parse(Session["Ajout"].ToString()))
            {
                bool UserExiste = user.MatriculeExiste(TbMatricule.Text);
                if (!UserExiste)
                {
                    EtatOperation = user.AjouterUnUser(item);
                    Session["SelectUserAdd"] = true;
                    string nom = TbNom.Text;
                    string prenom = TbPrenom.Text;
                    string myStringVariable = "Le nouveau chauffeur ' " + nom + " " + prenom + " ' a été ajouté avec succès ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + myStringVariable + "');", true);
                }
                else
                {
                    string matricule = TbMatricule.Text;
                    string myStringVariable = " Le chauffeur de Matricule ' " + matricule + " ' existe déjà !! ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + myStringVariable + "');", true);
                }
            }
            else
            {
                EtatOperation = user.ModifierUnUser(item);
                Session["SelectUserSet"] = true;
                string nom = item.Nom;
                string prenom = item.Prenom;
                string myStringVariable = "Le Chauffeur ' " + nom + " " + prenom +  " ' a été modifié avec succès ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + myStringVariable + "');", true);

            }
            if (EtatOperation == 1)
            {
                Session["ChampsEnabled"] = false;
                Session["Ajout"] = false;
                Response.Redirect("~/Interfaces/User/Users.aspx");
            }
        }

    }

    protected void BtnSet_Click(object sender, EventArgs e)
    {

        Session["ChampsEnabled"] = true;
        EnabledIdentite(true, false);
        EnabledProfil(true);
        Session["SelectUserSet"] = true;
    }




}
