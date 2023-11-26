using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Interfaces_Shared_Print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Control ctrl = (Control)Session["ctrl"];
        Control ctrl2 = (Control)Session["ctrl2"];
        PrintHelper.PrintWebControl(ctrl, ctrl2);
    }
}