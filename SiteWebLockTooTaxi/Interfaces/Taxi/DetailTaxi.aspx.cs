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

public partial class Interfaces_Taxi_Detail : System.Web.UI.Page
{
    Agrement agrement = new Agrement();
    Vehicules vehicule = new Vehicules();
    TypeTaxi typeTaxi = new TypeTaxi();
    Commune commune = new Commune();

    bool TaxiAjout;
    bool agrmtexiste;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            TbNom.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");
            TbPrenom.Attributes.Add("OnKeyUp", "this.value=this.value.toUpperCase();");

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

            if (operateur.Profil.ToUpper() == "SUPERVISEUR")
            {
                BtnSet.Enabled = false;
            }

            agrement = new Agrement();

            bool ChampsEnabled = bool.Parse(Session["TaxiChampsEnabled"].ToString()); //----------Date:27/01/2017------ By Zouhair LOUALID------------------

            TaxiAjout = bool.Parse(Session["TaxiAjout"].ToString());
            if (!TaxiAjout)
            {
                agrement = agrement.getUnAgrement(Session["TaxiDetailsFor"].ToString(), (int)Session["TaxiDetailsFor2"]);

            }

            Page.Title = "Ajout d'un agrément";

            if (!IsPostBack)
            {


                Vehicules item = new Vehicules();
                Vehicules item_retiree = new Vehicules();


                item_retiree.Id = 0;
                item_retiree.Immat = "RETIREE";
                item_retiree.DateImmat = DateTime.Now.ToString().Substring(0, 10);
                item_retiree.DateCreation = DateTime.Now.ToString().Substring(0, 10);
                item_retiree.IdOpCreation = -1;
                item_retiree.DateModif = DateTime.Now.ToString().Substring(0, 10);
                item_retiree.IdOpModif = 1;
                item_retiree.Nom = "";
                item_retiree.Prenom = "";
                item_retiree.DateMec = DateTime.Now.ToString().Substring(0, 10);
                item_retiree.Modele = "";
                item_retiree.Marque = "";

                List<Vehicules> ListVehicules = new List<Vehicules>();

                ListVehicules.Add(item);
                ListVehicules.Add(item_retiree);

                ListVehicules.AddRange(vehicule.getImmatriculationNonAttribue());


                DDLNumImmatriculation.DataSource = ListVehicules;
                DDLNumImmatriculation.DataValueField = "Immat";
                DDLNumImmatriculation.DataTextField = "Immat";
                DDLNumImmatriculation.DataBind();


                DDLTypeTaxi.DataSource = typeTaxi.getTypeTaxi();
                DDLTypeTaxi.DataValueField = "Num";
                DDLTypeTaxi.DataTextField = "Libelle";
                DDLTypeTaxi.DataBind();

                DDLPntAttache.DataSource = commune.getCommunes();
                DDLPntAttache.DataValueField = "num";
                DDLPntAttache.DataTextField = "Libelle";
                DDLPntAttache.DataBind();



                RemplirChampsAgrement(agrement);

                EnabledAgrement(ChampsEnabled, TaxiAjout);

            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            Response.Redirect("~/Interfaces/Taxi/Taxis.aspx");
        }
    }


    protected void EnabledAgrement(bool enabled, bool Agrement)
    {
        TbDateDebutValidite.Enabled = enabled;
        TbDateFinValidite.Enabled = enabled;

        TbNom.Enabled = enabled;
        TbPrenom.Enabled = enabled;

        TbAgrement.Enabled = Agrement;

        //---------- date 27/01/2017 : Zouhair LOUALID-------------------------------------------------

        TbCIN.Enabled = enabled;  // ajout du  remplissage les champs de CIN, adresse, telephone
        TbAdresse.Enabled = enabled;
        TbTelephone.Enabled = enabled;


      //  DDLTypeTaxi.Enabled = enabled;
        DDLPntAttache.Enabled = enabled;


       // DDLTypeTaxi.Enabled = enabled;
        DDLPntAttache.Enabled = enabled;

        DDLNumImmatriculation.Enabled = enabled;
        //  DDLNumImmatriculation.Enabled = true;

        TbAgrement.Enabled = Agrement;

  //      DDLTypeTaxi.Enabled = enabled;

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

    protected void RemplirChampsAgrement(Agrement item)
    {


        TbDateDebutValidite.Text = item.DateDebut;
        TbDateFinValidite.Text = item.DateFin;

        TbNom.Text = item.Nom;
        TbPrenom.Text = item.Prenom;

        TbAgrement.Text = item.NumAgrement;

        //---------- date 27/01/2017 : Zouhair LOUALID-------------------------------------------------

        TbCIN.Text = item.CIN;  // ajout du  remplissage les champs de CIN, adresse, telephone
        TbAdresse.Text = item.Adresse;
        TbTelephone.Text = item.Telephone;


        DDLTypeTaxi.Items.Remove(DDLTypeTaxi.Items.FindByText("Tous"));
        DDLPntAttache.Items.Remove(DDLPntAttache.Items.FindByText("Tous"));

        string Immat = item.Plaque;

        if (!TaxiAjout)
        {
            DDLTypeTaxi.Items.FindByText(item.TypeTaxi).Selected = true;
            DDLPntAttache.Items.FindByText(item.PointAttache).Selected = true;

            DDLNumImmatriculation.Items.Add(Immat);
            DDLNumImmatriculation.Items.FindByText(Immat).Selected = true;

            TbAgrement.Enabled = false;

            DDLTypeTaxi.Enabled = false;

        }





    }



    protected void BtnSaveAgrement_Click(object sender, EventArgs e)
    {
            if (Page.IsValid)
            {
                if (!TaxiAjout)
                {
                    //*****************************************************************//
                    // remplir la class Agrement par les valeurs des champs de saisi   //
                    //*****************************************************************//
                    Agrement item = agrement.getUnAgrement(Session["TaxiDetailsFor"].ToString(), (int)Session["TaxiDetailsFor2"]);
                    string OldPlaque = item.Plaque;

                    item.DateDebut = TbDateDebutValidite.Text;
                    item.DateFin = TbDateFinValidite.Text;
                    item.Nom = TbNom.Text;
                    item.Prenom = TbPrenom.Text;
                    item.NumAgrement = TbAgrement.Text;

                    //---------- date 27/01/2017 : Zouhair LOUALID-------------------------------------------------
                    item.CIN = TbCIN.Text;
                    item.Adresse = TbAdresse.Text;
                    item.Telephone = TbTelephone.Text;

                    //--------------------------------------------------------------------------------
                    item.TypeTaxi = DDLTypeTaxi.SelectedValue;

                    item.Plaque = DDLNumImmatriculation.SelectedItem.Text;
                    item.PointAttache = DDLPntAttache.SelectedItem.Text;
                    item.Commune = DDLPntAttache.SelectedValue;

                    item.DateModif = DateTime.Now;
                    if (item.Plaque == "")
                    {
                        item.IdVehicule = 0;
                    }
                    if (item.Plaque != "" && item.IdVehicule == 0)
                    {
                        item.IdVehicule = item.MaxIdAgrement();
                    }

                    agrement.ModifierUnAgrement(item);


                    Session["SelectAgrementSet"] = true;
                    Response.Redirect("~/Interfaces/Taxi/Taxis.aspx");

                }
                else
                {
                    Agrement item = new Agrement();

                    item.DateDebut = TbDateDebutValidite.Text;
                    item.DateFin = TbDateFinValidite.Text;
                    item.Nom = TbNom.Text;
                    item.Prenom = TbPrenom.Text;
                    item.NumAgrement = TbAgrement.Text;

                    //---------- date 27/01/2017 : Zouhair LOUALID-------------------------------------------------
                    item.CIN = TbCIN.Text;
                    item.Adresse = TbAdresse.Text;
                    item.Telephone = TbTelephone.Text;

                    //--------------------------------------------------------------------------------
                    item.TypeTaxi = DDLTypeTaxi.SelectedValue;
                    item.Plaque = DDLNumImmatriculation.SelectedItem.Text;
                    item.PointAttache = DDLPntAttache.SelectedItem.Text;
                    item.Commune = DDLPntAttache.SelectedValue;
                    item.IdVehicule = item.MaxIdAgrement();


                    //******************* Test : si le Num Agrement est déja affecté à un autre Agrement ou Non ****************************//


                    AgrementTableAdapter AgrementAdapter = new AgrementTableAdapter();


                    agrmtexiste = false;

                    foreach (MyDataSet.AgrementRow row in AgrementAdapter.GetAgrementsByType(int.Parse(item.TypeTaxi)))
                    {
                        if (row.NumAgrement.Equals(item.NumAgrement))
                        {
                            agrmtexiste = true;
                            break; // get out of the loop

                        }
                        agrmtexiste = false;
                    }

                    if (agrmtexiste)
                    {
                        MessageBox.Show("Impossible d'ajouter cet Agrement car le N° Agrement est déjà affecté un autre agrement !", "Message d'erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    else
                    {
                        agrement.AjouterUnAgrement(item);
                        Session["SelectAgrementAdd"] = true;
                        Response.Redirect("~/Interfaces/Taxi/Taxis.aspx");
                    }
                }
            }
        
    }


    protected void BtnBack_Click(object sender, EventArgs e)
    {
        if (!TaxiAjout)
        {
            Session["SelectAgrementSet"] = true;
        }
        else
        {
            Session["SelectAgrementAdd"] = false;
        }
        Response.Redirect("~/Interfaces/Taxi/Taxis.aspx");
    }


    protected void BtnSet_Click(object sender, EventArgs e)
    {
        Session["ChampsEnabled"] = true;
        EnabledAgrement(true, false);
        Session["SelectUserSet"] = true;
    }
}