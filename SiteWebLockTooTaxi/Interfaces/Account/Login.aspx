<%@ Page Title="Se connecter" Language="C#" MasterPageFile="~/Interfaces/Shared/Login.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Interfaces_Account_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
     <asp:Panel  id="Panel1" runat="server" Visible="true" style="width:100%;height:765px; margin-left:8px; ">
    
   
    <div style="text-align:center;">
    <asp:Image ID="Image1" runat="server" Height="63px" Width="350" ImageUrl="~/Icons/IntroTaxi.png" />
         
    <h2>
        Se connecter
    </h2>
    <p>
       Veuillez entrer votre Login et votre Mot de passe.
        </p>
                 </div>
            
            <div class="accountInfo"  style="margin-left:30%; ">
             
                <fieldset class="login"  style="margin-top:10%; position:relative; background-color:Azure;">
               <%--     <legend > Identification </legend>--%>
                    
                    <td >&nbsp;&nbsp;&nbsp;
                    Login :
                    </td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <td  style=" margin-left:30%;">
                      <asp:TextBox ID="UserName" runat="server" Width="300px" CssClass="text" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                             CssClass="failureNotification" ErrorMessage="Un nom d'utilisateur est requis." ToolTip="Un nom d'utilisateur est requis." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </td>
                    </tr>
                </br>
                 </br>
                  </br>
                   
                    <td >&nbsp;&nbsp;&nbsp;
                       Mot de passe :
                    </td>&nbsp;&nbsp;&nbsp;
                    
                    <td  style=" margin-left:30%;">
                        <asp:TextBox ID="Password" runat="server"  Width="300px" TextMode="Password" CssClass="text"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                             CssClass="failureNotification" ErrorMessage="Un mot de passe est requis." ToolTip="Un mot de passe est requis." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>--%>
                                         
                  </td>
                  
                    </tr>
                    
                  <tr>
                  <p>
                  </br>
                  </p>
                   <p class="submitButton" style=" margin-right:40%;"   >
                    <asp:Button ID="LoginButton" runat="server"  Text="Se connecter"  CssClass="button"
                        ValidationGroup="LoginUserValidationGroup" onclick="LoginButton_Click" />
                </p>
                
                </tr>  
                  <%--  <p>
                     <asp:Label ID="ProfilsLabel" runat="server" AssociatedControlID="DDLProfils">Profil :</asp:Label>
                        <asp:DropDownList ID="DDLProfils" runat="server" CssClass="textEntry">
                        <asp:ListItem Text=" --- Selectionner Profil --- " Value="-1"> </asp:ListItem>
                        <asp:ListItem Text="ADMINISTRATEUR" Value="1"> </asp:ListItem>
                        <asp:ListItem Text="SUPERVISEUR" Value="2" ></asp:ListItem>
                        <asp:ListItem Text="CONSULTANT" Value="3" ></asp:ListItem>
                        </asp:DropDownList>
                    </p>--%>
                    
                </fieldset>

               
            </div>
             <span  class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            
            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" 
                 ValidationGroup="LoginUserValidationGroup"/>
                </asp:Panel>
            </ContentTemplate>


            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="LoginButton" EventName="Click" />
            </Triggers>
            </asp:UpdatePanel>
</asp:Content>

