using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLockTooTaxi;
using DataLockTooTaxi.Class;
using System.Windows.Forms;

public partial class Interfaces_Listes_AjoutMembre : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            Page.Title = "Ajout d'membre à la Liste ";


            if (!IsPostBack)
            {

                string typeListe = Session["TypeListe"].ToString();
             
                if (typeListe == "Chauffeurs demandés")
                {
                  
                  
                   

                    // Remplir DDLChauffeurs par Nom + Prénom des Motifs de Demandes

                    Motif motif = new Motif();
                    List<Motif> ListMotifs = motif.GetMotifs();

                    DDLMotifs.DataSource = ListMotifs;
                    DDLMotifs.DataValueField = "IdMotif";
                    DDLMotifs.DataTextField = "AbrevMotif";
                    DDLMotifs.DataBind();
                }
                
               
            }           

        }
        catch (Exception)
        {
            Response.Redirect("~/Interfaces/Listes/Listes.aspx");
        }
        
    }

    protected void BtSave3_Click(object sender, EventArgs e)
    {
        string typeListe = Session["TypeListe"].ToString();

        ListesUsers item = new ListesUsers();


        int i = 0;
        if (typeListe == "Chauffeurs demandés")
        {
            if (!(item.UserExistInList(_TbMatricule.Text, Convert.ToInt32(Session["NumListe"]))))
            {
                item.NumListe = Convert.ToInt32(Session["NumListe"]);
                User user = new User();

                item.IdUser = user.GetIdUser(_TbMatricule.Text);


                if (typeListe == "Chauffeurs demandés")
                {
                    item.IdMotif = int.Parse(DDLMotifs.SelectedValue);
                }
                else
                {
                    item.IdMotif = 0;
                }

                item.Ordre = 0;


                i = item.AjouterUnUserToList(item.NumListe, item.IdUser, item.IdMotif, item.Ordre);


                Response.Redirect("~/Interfaces/Listes/Listes.aspx");
            }
            else
            {
                MessageBox.Show("Ce membre exixte déjà dans la liste", "Message d'erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }


        }
          

    }
    
    protected void BtCancel3_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/Interfaces/Listes/Listes.aspx");
    }

    protected void _TbMatricule_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string Matricule = _TbMatricule.Text;
            if (Matricule != "")
            {
                User user = new User();
                _TbNumPermis.Text = user.getNumPermis(Matricule);
                _TbNomPrenom.Text = user.geNomCompletByMatricule(Matricule);
            }
            else
            {
                _TbNumPermis.Text = "";
                _TbNomPrenom.Text = "";
            }
        }
        catch (Exception)
        {
            _TbNumPermis.Text = "";
            _TbNomPrenom.Text = "";
        }
    }

    protected void _TbNumPermis_TextChanged(object sender, EventArgs e)
    {
        try
        {

            string numPermis = _TbNumPermis.Text;
            if (numPermis != "")
            {
                User user = new User();
                _TbMatricule.Text = user.getMatricule(numPermis);
                _TbNomPrenom.Text = user.geNomCompletByMatricule(_TbMatricule.Text);
            }
            else
            {
                _TbMatricule.Text = "";
                _TbNomPrenom.Text = "";
            }


        }
        catch (Exception)
        {
            _TbMatricule.Text = "";
            _TbNomPrenom.Text = "";
        }
    }
}