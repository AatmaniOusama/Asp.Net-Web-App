<%@ page title="Insérer pointage" language="C#" masterpagefile="~/Interfaces/Shared/MasterPage.master" autoeventwireup="true" inherits="Interfaces_Historique_InsererPointage, App_Web_y1tlwqpm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

    <style type="text/css">
        .style1
        {
            width: 150px;
        }
        .style2
        {
            width: 297px;
        }
        </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <div Id="Div1" runat="server" style="  margin-left:450px; " >
        <fieldset class="login" style="width:438px; height:340px;">
        <legend> Insérer Pointage</legend>

    <table>

     
     <tr>
            <td class="style1">
            Borne de pointage
            </td>

            <td>

               <asp:DropDownList ID="DDLLecteur" runat="server" Width="206px"   CssClass="text"  >
                </asp:DropDownList>

            </td>
        </tr>
     <tr style="height :10px;"></tr>
     <tr>
                <td class="style1">
                  N° Permis de confiance        
                </td>
                <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <asp:TextBox ID="TbNumPermis" runat="server"  AutoPostBack="true" Width="200px"  OnTextChanged="TbNumPermis_TextChanged"></asp:TextBox>               
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="TbNumPermis" ValidChars="0123456789" />
                </ContentTemplate>
                <Triggers>                
               
                <asp:AsyncPostBackTrigger ControlID="DDLChauffeur" />   
                </Triggers>
                </asp:UpdatePanel> 
                </td>
        </tr>
     <tr style="height :10px;"></tr>
     <tr>
            <td class="style1">
            Nom Chauffeur </td>
            <td>
              <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                <asp:DropDownList ID="DDLChauffeur" AutoPostBack="true" runat="server" Width="206px"  onselectedindexchanged="DDLChauffeur_SelectedIndexChanged">
                </asp:DropDownList>
                </ContentTemplate>
                <Triggers>              
              
                <asp:AsyncPostBackTrigger ControlID="TbNumPermis" />              
                </Triggers>
            </asp:UpdatePanel> 
            </td>
        </tr>      
     <tr style="height :10px;"></tr>   
     <tr>
                <td class="style1">
                    N° Taxi      
                </td>
                <td>
                    <asp:TextBox ID="TbNumTaxi" runat="server" Width="200px" CssClass="text"></asp:TextBox>
                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="TbNumTaxi" ValidChars="0123456789" />
                </td>
     </tr>
     <tr style="height :10px;"></tr>
     <tr>
            <td class="style1">
            Borne de pointage
            </td>

            <td>
                        <asp:DropDownList ID="DDLTypeTaxi" runat="server" CssClass="text"   Width="206px" >
                           
                        </asp:DropDownList>
          </td>  
        </tr>
     <tr style="height :10px;"></tr>
     <tr>
                <td class="style1">
                    Code Refus     
                </td>
                <td>
                    <asp:DropDownList ID="DDLCodeRefus" runat="server" Width="206px" CssClass="text"></asp:DropDownList>
                </td>
     </tr>
               
    </table>
    <table style="margin-left:200px;">

        <tr style="height :30px;"></tr>
        <tr>
        <td runat="server" id="TdSet" >
            <asp:Button ID="BtnSavePointage" runat="server" Text="Enregistrer"  OnClick="BtnSavePointageClick" />
            <asp:Button ID="BtnCancel" runat="server" Text="Annuler" OnClick="BtnBack_Click"  />
        </td>
                      
        </tr>
   </table>

        </fieldset>
   </div>


   





</asp:Content>


