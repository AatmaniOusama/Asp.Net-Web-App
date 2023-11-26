
<%@ page title="" language="C#" masterpagefile="~/Interfaces/Shared/MasterPage.master" autoeventwireup="true" inherits="Interfaces_Operateur_AjoutOperateur, App_Web_eei3chx1" %>
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

    <div Id="Div1" runat="server" >
        <fieldset class="login" style="width:438px; height:500px;">
        <legend> Ajout Opérateur</legend>

     <table>
     <tr>
                <td class="style1">
                    Nom       
                </td>
                <td>
                    <asp:TextBox ID="TbNom" runat="server" Width="200px" CssClass="text"></asp:TextBox>
                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TbNom" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                </td>
     </tr>
     <tr>
                <td class="style1">
                    Prénom       
                </td>
                <td>
                    <asp:TextBox ID="TbPrenom" runat="server" Width="200px" CssClass="text"></asp:TextBox>
                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TbPrenom" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                </td>
     </tr>
     <tr>
            <td class="style1">
            Profil
            </td>

            <td>

                <asp:DropDownList ID="DDLProfil" runat="server" Width="206px"  AutoPostBack="true" Enabled="true" CssClass="text">
                <asp:ListItem Text="ADMINISTRATEUR" Value="1" > </asp:ListItem>
                <asp:ListItem Text="SUPERVISEUR" Value="2" > </asp:ListItem>
                <asp:ListItem Text="CONSULTANT" Value="3" > </asp:ListItem>
                </asp:DropDownList>

            </td>
        </tr>
     <tr>
            <td class="style1">Date Début Validité    
            
                 <asp:RequiredFieldValidator ValidationGroup="validation" Display="None" 
                    ID="RequiredFieldValidator5" runat="server" ControlToValidate="TbDateDebutValidite" 
                    ErrorMessage="champ obligatoire" ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" 
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                </asp:ValidatorCalloutExtender>      
            </td>
             <td>       
             
             
               <asp:TextBox ID="TbDateDebutValidite" runat="server" Width="200px" CssClass="text"></asp:TextBox>
                    <asp:CalendarExtender ID="TbDateDebutAQP_CalendarExtender" runat="server" 
                        Enabled="True" TargetControlID="TbDateDebutValidite">
                    </asp:CalendarExtender>
                    
                
               </td>
            </tr>
     <tr>
            <td class="style1">Date Fin Validité 
            
             <asp:RequiredFieldValidator ValidationGroup="validation" Display="None" 
                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="TbDateFinValidite" 
                    ErrorMessage="champ obligatoire" ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" 
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                </asp:ValidatorCalloutExtender>       
            </td>
             <td>       
             
             
               <asp:TextBox ID="TbDateFinValidite" runat="server" Width="200px" CssClass="text"></asp:TextBox>
                    <asp:CalendarExtender ID="TbDateFinAQP_CalendarExtender" runat="server" 
                        Enabled="True" TargetControlID="TbDateFinValidite">
                    </asp:CalendarExtender>
                    
                
               </td>
            </tr>
     <tr>
            <td class="style1">
            Groupe géré
            </td>

            <td>

               <asp:DropDownList ID="DDLService" runat="server" Width="206px" AutoPostBack="True"  CssClass="text" >
                                <asp:ListItem  Value="0" Text="#Tous#">Tous</asp:ListItem>
              </asp:DropDownList>

            </td>
        </tr>     
     <tr>
                    <td class="style1">
                    Login 
                    </td>
                    <td >
                      <asp:TextBox ID="UserName" runat="server" Width="200px" CssClass="text" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                             CssClass="failureNotification" ErrorMessage="Un Login est requis." ToolTip="Un Login est requis."  ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </td>
             </tr>                          
     <tr>
                    <td  class="style1">
                       Mot de passe 
                    </td>
                    
                    <td >
                        <asp:TextBox ID="Password" runat="server"  Width="200px" TextMode="Password" CssClass="text"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                             CssClass="failureNotification" ErrorMessage="Un mot de passe est requis." ToolTip="Un mot de passe est requis." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                         
                  </td>
                  
                    </tr>      
               
    </table>
    <table style="margin-left:200px;">

        <tr style="height :30px;"></tr>
        <tr>
        <td runat="server" id="TdSet" >
            <asp:Button ID="BtnSaveAQP" runat="server" Text="Enregistrer"  OnClick="BtnSaveAjoutOpClick" />
            <asp:Button ID="BtnCancel" runat="server" Text="Annuler" OnClick="BtnBack_Click"  />
        </td>
                      
        </tr>
   </table>

        </fieldset>
   </div>


   





</asp:Content>

