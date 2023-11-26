using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLockTooTaxi;
using DataLockTooTaxi.Class;
using System.Windows.Forms;

public partial class Interfaces_Listes_ModifierListe : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            Page.Title = "Modifer une Liste ";


            if (!IsPostBack)
            {
                Listes liste = new Listes();
                liste = liste.getListeByNum(Convert.ToInt32(Session["NumListe"])) ;

                TbAbrevListe.Text = liste.Abrev;
                TbLibelleListe.Text = liste.Libelle;
                DDLTypeListe.Items.FindByText(liste.Type).Selected= true;
            }
          }

        
        catch (Exception)
        {
            Response.Redirect("~/Interfaces/Listes/Listes.aspx");
        }

    }

    protected void BtSave_Click(object sender, EventArgs e)
    {
        Listes item = new Listes();

        int i = 0;

            item.Num_liste = Convert.ToInt32(Session["NumListe"]);
            item.Abrev = TbAbrevListe.Text;
            item.Libelle = TbLibelleListe.Text;
            item.Type = DDLTypeListe.SelectedValue;
            i = item.UpdateListe(item);
        


        if (i == 1)
        {

            Response.Redirect("~/Interfaces/Listes/Listes.aspx");
        }



    }
    protected void BtCancel_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/Interfaces/Listes/Listes.aspx");
    }
    


}