using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLockTooTaxi;

public partial class Shared_Site : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        setCurrentPage();

        try
        {
            Operateur operateur = (Session["Operateur"] as Operateur);



            if (operateur.Profil.ToUpper() == "ADMINISTRATEUR")
            {

                liOperateurs.Visible = true;
            }

            if (operateur.Profil.ToUpper() == "SUPERVISEUR")
            {
                liOperateurs.Visible = false;

            }

            if (operateur.Profil.ToUpper() == "CONSULTANT")
            {
                liOperateurs.Visible = false;
            }

            HeadLoginName.Text = operateur.Prenom + " " + operateur.Nom;

            //NavigationMenu.SelectedItem.Selected = false;
            //NavigationMenu.Items[int.Parse(Session["currentPage"].ToString())].Selected = true;

        }
        catch (Exception)
        {

            Response.Redirect("~/Interfaces/Account/Login.aspx");
        }

    }

 



    private void setCurrentPage()
    {
        var pagename = Convert.ToString(GetPageName());
        switch (pagename)
        {
            case "Users.aspx":
                users.Attributes["class"] = "selected";
                break;

            case "Badges.aspx":
                users.Attributes["class"] = "selected";
                break;

            case "ControlesUsers.aspx":
                users.Attributes["class"] = "selected";
                break;

            case "AutorisationsQPU.aspx":
                users.Attributes["class"] = "selected";
                break;
                
            case "Taxis.aspx":
                taxis.Attributes["class"] = "selected";
                break;

            case "CarteGrise.aspx":
                taxis.Attributes["class"] = "selected";
                break;

            case "ControleTaxi.aspx":
                taxis.Attributes["class"] = "selected";
                break;

            case "Agents.aspx":
                agents.Attributes["class"] = "selected";
                break;

            case "Listes.aspx":
                listes.Attributes["class"] = "selected";
                break;

            case "AbsentsJours.aspx":
                rapports.Attributes["class"] = "selected";
                break;

            case "TaxisAbsents.aspx":
                rapports.Attributes["class"] = "selected";
                break;

            case "IdentificationsPeriode.aspx":
                rapports.Attributes["class"] = "selected";
                break;

            case "IdentificationsPeriode_Taxi.aspx":
                rapports.Attributes["class"] = "selected";
                break;

            case "SyntheseControles.aspx":
                rapports.Attributes["class"] = "selected";
                break;

            case "Historique.aspx":
                historique.Attributes["class"] = "selected";
                break;

            case "Operateur.aspx":
                operateur.Attributes["class"] = "selected";
                break;

            case "LoginsOperateur.aspx":
                operateur.Attributes["class"] = "selected";
                break;

        }
    }

    private object GetPageName()
    {
        return Request.Url.ToString().Split('/').Last();
    }


    protected void HeadLoginStatus_Click(object sender, EventArgs e)
    {
        Operateur operateur = (Session["Operateur"] as Operateur);
        Log log = new Log();
        Session["FermeturetureSession"] = log.CloseSession(operateur);

        Session.Abandon();
        Response.Redirect("~/Interfaces/Account/Login.aspx");
    }
   
   
}
