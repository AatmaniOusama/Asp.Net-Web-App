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
using System.Windows.Forms;

public partial class Interfaces_DetailCarteGrise : System.Web.UI.Page
{ 
    
    Vehicules vehicule = new Vehicules();
    Agrement agrement = new Agrement();
     bool existe;
    bool CarteGriseAjout;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

           TbNumImmatVehicule.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
           TbMarque.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
           TbModele.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
           TbNomProprietaire.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
           TbPrenomProprietaire.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
           TbCinProprietaire.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
           


            vehicule = new Vehicules();
          
            CarteGriseAjout = bool.Parse(Session["CarteGriseAjout"].ToString());

            Page.Title = "Carte Grise du Véhicule";


            if (!IsPostBack)
            {


                if (!CarteGriseAjout)
                {
                    vehicule = vehicule.getUnVehicule(Session["ModifCarteGrise"].ToString());
                    RemplirChampsCarteGrise(vehicule);
                    Session["ImmatVehicule"] = vehicule.Immat;
                }
                else
                {
                    RemplirChampsCarteGrise(vehicule);
                                     
                }

            }

        }

        catch (Exception)
        {
            Response.Redirect("~/Interfaces/Taxi/CarteGrise.aspx");
        }
    }
    protected void RemplirChampsCarteGrise(Vehicules vh)
    {      
        TbNumImmatVehicule.Text = vh.Immat;
        TbDateImmat.Text = vh.DateImmat.ToString().Substring(0, 10);
        TbDateMiseEnCirculation.Text = vh.DateMec.ToString().Substring(0, 10);
        TbNomProprietaire.Text = vh.Nom;
        TbPrenomProprietaire.Text = vh.Prenom;
        TbCinProprietaire.Text = vh.Cin;
        TbMarque.Text = vh.Marque;
        TbModele.Text = vh.Modele;
    }

    protected void BtnSaveCarteGriseClick(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (!CarteGriseAjout)
            {
                //****************************************************************//
                // remplir la class Vehicule par les valeurs des champs de saisi  //
                //***************************************************************//
                Vehicules item = vehicule.getUnVehicule(Session["ModifCarteGrise"].ToString());

                Operateur operateur = (Operateur)Session["Operateur"];
                item.IdOpModif = operateur.ID;
                item.DateModif = DateTime.Now.ToString();

                string ImmatOld = item.Immat;

                item.Immat = TbNumImmatVehicule.Text;
                item.DateImmat = TbDateImmat.Text;
                item.DateMec = TbDateMiseEnCirculation.Text;
                item.Marque = TbMarque.Text;
                item.Modele = TbModele.Text;
                item.Nom = TbNomProprietaire.Text;
                item.Prenom = TbPrenomProprietaire.Text;
                item.Cin = TbCinProprietaire.Text;

                vehicule.updateVehicule(item, Session["ImmatVehicule"].ToString());

                agrement.updateAgrementByImmat(item.Immat,ImmatOld);

                Response.Redirect("~/Interfaces/Taxi/CarteGrise.aspx");
            }
            else
            {
                Vehicules item = new Vehicules();

                Operateur operateur = (Operateur)Session["Operateur"];
                item.IdOpCreation = operateur.ID;
                item.IdOpModif = operateur.ID;               
                item.DateCreation = DateTime.Now.ToString();
                item.DateModif = DateTime.Now.ToString();

                item.Immat = TbNumImmatVehicule.Text;
                item.DateImmat = TbDateImmat.Text;
                item.DateMec = TbDateMiseEnCirculation.Text;
                item.Marque = TbMarque.Text;
                item.Modele = TbModele.Text;
                item.Nom = TbNomProprietaire.Text;
                item.Prenom = TbPrenomProprietaire.Text;
                item.Cin = TbCinProprietaire.Text;

                
                VehiculesTableAdapter VehiculesAdapter = new VehiculesTableAdapter();
                List<Vehicules> listImmatriculations = vehicule.GetImmatriculation();




                existe = false;

                foreach (MyDataSet.VehiculesRow row in VehiculesAdapter.GetImmatriculation())
                {
                    if (row.Immat.Equals(item.Immat))
                    {
                        existe = true;
                        break; // get out of the loop

                    }
                    existe = false;   
                }

                if (existe)
                {
                   // MessageBox.Show("Impossible d'ajouter ce véhicule car il existe déjà !");
                  // MessageBox.Show("Impossible d'ajouter ce véhicule car il existe déjà !", "Message d'erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                   string myStringVariable = "Impossible d'ajouter ce véhicule car il existe déjà ! ";
                   ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + myStringVariable + "')", true);
                   
                
                }
                else
                    vehicule.insertVehicule(item);
                   
                
               // MessageBox.Show("Véhicule ajouté avec succès !");

                //string myStringVariable2 = "Véhicule ajouté avec succès !";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + myStringVariable2 + "')", true);
                   
                    Response.Redirect("~/Interfaces/Taxi/CarteGrise.aspx");
            }
        }
    }

    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Interfaces/Taxi/CarteGrise.aspx");
    }


}