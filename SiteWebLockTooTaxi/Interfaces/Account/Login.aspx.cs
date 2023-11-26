using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLockTooTaxi;
using System.Text;
using System.Runtime.InteropServices;

public partial class Interfaces_Account_Login : System.Web.UI.Page
{
    [DllImport("LK2DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)] //Importer la DLL LK2DLL
    public static extern void Decrypt_String(string csStrCrypted, bool bNewVersion, StringBuilder StrDecrypted);
    public const int SIZE_CRYPT_BUF = 200; //Taille max d'un mot de passe cryptée


    Operateur operateur = new Operateur();

    protected void Page_Load(object sender, EventArgs e)
    {
        UserName.Focus();
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {

        string UserName1 = UserName.Text;
        string Password1 = Password.Text;
        Log log = new Log();

        //string Profil = DDLProfils.SelectedItem.Text;

        operateur = operateur.GetOperateurByLogin(UserName1, Password1);
        if (!string.IsNullOrEmpty(operateur.Login as string))// || !string.IsNullOrEmpty(operateur.MotPasse as string))
        {
            String sessionId;
            sessionId = Session.SessionID;
            Session["Operateur"] = operateur;
            Session["OuvertureSession"] = log.OpenSession(operateur);
            Response.Redirect("../../Interfaces/Historique/Historique.aspx", false);
        }
        else FailureText.Text = "Le Login ou le Mot de Passe est incorrect, veuillez réessayer";
    }

}





