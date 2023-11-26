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

public partial class Interfaces_Agent_DetailAgent : System.Web.UI.Page
{
    Visiteur visiteur = null;
    Droits droit = null;
 
   

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            visiteur = new Visiteur();
            droit = new Droits();



            TbMatricule.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbNom.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbPrenom.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");        
            TbAdresse.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbCodePostale.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbVille.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbTel.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");          
            TbNCI.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");

            

            Operateur operateur = (Operateur) Session["Operateur"];

            if (operateur.Profil == "ADMINISTRATEUR")
            {
            BtnSet.Visible = true;
            }
            if (operateur.Profil == "CONSULTANT")
            {
                BtnSet.Enabled = false;
               
            }


            bool ChampsEnabled = bool.Parse(Session["ChampsEnabledAgent"].ToString());
            bool Ajout = bool.Parse(Session["AjoutAgent"].ToString());

            Page.Title = "La Fiche de l'Agent";

            if (!Ajout)
            {
                visiteur = visiteur.GetUnVisiteur(Session["DetailsForAgent"].ToString());
            }

            Session["item"] = visiteur;

            photo.ImageUrl = "../../Icons/Inconnu.jpg";

            if (!IsPostBack)
            {
                
               

                DDLDroit.DataSource = droit.GetDroitsAcces();
                DDLDroit.DataValueField = "NumDroit";
                DDLDroit.DataTextField = "LibelleDroit";
                DDLDroit.DataBind();

              
                RemplirChamps(visiteur);
                EnabledIdentite(ChampsEnabled, Ajout);
                EnabledProfil(ChampsEnabled);

            }



        }
        catch (Exception)
        {
            Response.Redirect("~/Interfaces/Agent/Agents.aspx");
        }
    }

    protected void RemplirChamps(Visiteur item)
    {


         RbSexe.Items.FindByValue(item.Sexe).Selected = true;

        TbNom.Text = item.Nom;

        TbPrenom.Text = item.Prenom;

        TbMatricule.Text = item.Matricule;

        TbDNaissance.Text = item.DateNaissance;

        TbAdresse.Text = item.Adresse1;

        TbCodePostale.Text = item.CodePostal;

        TbVille.Text = item.Ville;

        TbTel.Text = item.Telephone;

        TbEmail.Text = item.EMail;

        TbNCI.Text = item.NumCI;

        RbFlagAutorise.Items.FindByValue(item.FlagAutorise).Selected = true;

        TbDateCreation.Text = item.DateCreat.ToString();

        TbDateModif.Text = item.DateModif.ToString();

        TbDateDebut.Text = item.DateDebut.ToString();

        TbDateFin.Text = item.DateFin.ToString();

        DDLDroit.Items.FindByValue(item.NumDroitAcces.ToString()).Selected = true;

   


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

        TbDNaissance.Enabled = enabled;
        
        TbAdresse.Enabled = enabled;

        TbCodePostale.Enabled = enabled;

        TbVille.Enabled = enabled;

        TbTel.Enabled = enabled;

        TbEmail.Enabled = enabled;
       
        TbNCI.Enabled = enabled;
             
        TbNom.Enabled = enabled;

        RbFlagAutorise.Enabled = enabled;

        if (!enabled)
        {
            TdSave.Visible = false;
            TdSet.Visible = true;
        }
        else
        {
            TdSave.Visible = true;
            TdSet.Visible = false;
        }

    }

    protected void EnabledProfil(bool enabled)
    {
        if (RbFlagAutorise.SelectedValue != "Y")
            enabled = false;

        TbDateDebut.Enabled = enabled;

        TbDateFin.Enabled = enabled;

        DDLDroit.Enabled = enabled;

      

        //if (CbBadge.Checked)
        //    DDLBadge.Enabled = enabled;

      
    }

   
    protected void CbBadge_CheckedChanged(object sender, EventArgs e)
    {
    //    if (CbBadge.Checked == true)
    //        DDLBadge.Enabled = true;
    //    else DDLBadge.Enabled = false;
    }
   
   
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Interfaces/Agent/Agents.aspx");
    }

    protected void RbFlagAutorise_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (RbFlagAutorise.SelectedValue == "Y")
            EnabledProfil(true);
        else
            EnabledProfil(false);

        EnabledIdentite(true, bool.Parse(Session["AjoutAgent"].ToString()));

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            Visiteur item = Session["item"] as Visiteur;

            //**********************************************************//
            // remplir la class Agent par les valeurs des champs de saisi//
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

            item.Numservice = 0;
            item.Tel = null;
            item.Fax = null;
            item.FlagAutorise = RbFlagAutorise.SelectedValue;
            item.DateDebut = DateTime.Parse(TbDateDebut.Text);
            item.DateFin = DateTime.Parse(TbDateFin.Text);
            item.NumDroitAcces = 1;
            item.NumBadge = null;
            item.NbEmpreintes = 2;
            item.Adresse2 = null;

            if (TbAdresse.Text != "")
                item.Adresse1 = TbAdresse.Text;
            else
                item.Adresse1 = null;

            if (TbCodePostale.Text != "")
                item.CodePostal = TbCodePostale.Text;
            else
                item.CodePostal = null;

            if (TbVille.Text != "")
                item.Ville = TbVille.Text;
            else
                item.Ville = null;

            if (TbTel.Text != "")
                item.Telephone = TbTel.Text;
            else
                item.Telephone = null;     
            
            item.Mobile = null;

            if (TbEmail.Text != "")
                item.EMail = TbEmail.Text;
            else
                item.EMail = null;

            item.DateCreat = DateTime.Parse(TbDateCreation.Text);
            item.DateModif = DateTime.Now;

            if (TbDNaissance.Text != "")
                item.DateNaissance = TbDNaissance.Text;
            else
                item.DateNaissance = null;

            item.HeureMois = 0;
            item.TauxNormal = 0;
            item.TauxSup1 = 0;
            item.TauxSup2 = 0;
            item.Fonction = null;
            item.TauxSup3 = 0;
            item.Sexe = RbSexe.SelectedValue;
            item.Civilite = "0";

            if (TbNCI.Text != "")
                item.NumCI = TbNCI.Text;
            else
                item.NumCI = null;

            item.NumSS = null;
            item.NumHoraire = 0;
            item.CodePin = 0;
            item.CheckEmpreinte = true;
            item.CheckBadge = false;
            item.CheckPin = true;
            item.CheckAutoDeclar = false;
            item.TauxSup0 = 0;
            item.TypeTemps = 0;
            item.Modified = true;
            item.BadgeEncoded = false;
            item.NumContrat = 0;
            item.TypeUser = 3;
            item.IdUser1 = 0;
            item.IdUser2 = 0;
            item.IdUser3 = 0;
            item.UpdateLecNow = true;
            item.JourRepos1 = 0;
            item.JourRepos2 = 0;
            item.Motif = null;
            item.IdMotif = 0;


            /***Fin***/

            int EtatOperation = 0;

            //**********************************************//
            //s'il s'agit d'une Ajout =>Foction AjouterAgent//
            //Si non => Fonction ModifierAgent             //
            //*******************************************//
            if (bool.Parse(Session["AjoutAgent"].ToString()))
            {
                EtatOperation = visiteur.AjouterUnVisiteur(item);
            }
            else
                EtatOperation = visiteur.ModifierUnVisiteur(item);

            if (EtatOperation == 1)
            {
                Session["ChampsEnabledAgent"] = false;
                Session["AjoutAgent"] = false;
                Response.Redirect("~/Interfaces/Agent/Agents.aspx");
            }
        }

    }

    protected void BtnSet_Click(object sender, EventArgs e)
    {

        Session["ChampsEnabledAgent"] = true;
        EnabledIdentite(true, false);
        EnabledProfil(true);

    }

   
}