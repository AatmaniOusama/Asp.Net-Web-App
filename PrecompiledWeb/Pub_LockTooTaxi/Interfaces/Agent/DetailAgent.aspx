<%@ page title="" language="C#" masterpagefile="~/Interfaces/Shared/MasterPage.master" autoeventwireup="true" inherits="Interfaces_Agent_DetailAgent, App_Web_cbzejwgp" %>

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

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  >
    <ContentTemplate>
        <div Id="DivIdentite" runat="server" style="width:35%;float:left;">
    <fieldset class="login" style="width:90%; height:600px;">
    <legend>Identité Agent</legend>
    <table>
        <tr>
            <td colspan="2" class="style1">
                <asp:RadioButtonList runat="server" Font-Overline="false" RepeatDirection="Horizontal"   CssClass="text" 
                    TextAlign="Left" Width="300px" ID="RbSexe" AutoPostBack="True">
                    <asp:ListItem Text="Homme" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Femme" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
                <td class="style1">
                    Matricule
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="TbMatricule" Display="None" ErrorMessage="champ obligatoire" 
                        ValidationGroup="validation"></asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender" 
                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                    </asp:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:TextBox ID="TbMatricule" runat="server" Width="200px" CssClass="text" ></asp:TextBox>
                </td>
            </tr>
        <tr>
            <td class="style1">Nom
            <asp:RequiredFieldValidator ValidationGroup="validation" runat="server" ControlToValidate="TbNom" 
                    ErrorMessage="champ obligatoire" Display="None" ID="RequiredFieldValidator3" ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender"
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                </asp:ValidatorCalloutExtender>
            </td>
            <td>
                <asp:TextBox ID="TbNom" runat="server" Width="200px" CssClass="text" ></asp:TextBox>
            </td>
        </tr>        
        <tr>
            <td class="style1">Prénom
            <asp:RequiredFieldValidator ValidationGroup="validation" Display="None" 
                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="TbPrenom" 
                    ErrorMessage="champ obligatoire" ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" 
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                </asp:ValidatorCalloutExtender>
            </td>
            <td>
                <asp:TextBox ID="TbPrenom" runat="server" Width="200px" CssClass="text" ></asp:TextBox>
            </td>
         </tr>   
        <tr>
                <td class="style1">
                    Date naissance</td>
                <td>
                    <asp:TextBox ID="TbDNaissance" runat="server" Width="200px" CssClass="text" ></asp:TextBox>
                    <asp:CalendarExtender ID="TbDNaissance_CalendarExtender" runat="server" 
                        Enabled="True" TargetControlID="TbDNaissance">
                    </asp:CalendarExtender>
                </td>
            </tr>
        <tr>
        <td class="style1">
            Adresse</td>
        <td>
            <asp:TextBox ID="TbAdresse" runat="server" Width="200px"  CssClass="text" TextMode="MultiLine"></asp:TextBox>
        </td>
         </tr>   
        <tr>
                <td class="style1">
                    Code postale</td>
                <td>
                    <asp:TextBox ID="TbCodePostale" runat="server" CssClass="text"  Width="200px"></asp:TextBox>
                </td>
            </tr>
        <tr>
                <td class="style1">
                    Ville</td>
                <td>
                    <asp:TextBox ID="TbVille" runat="server" Width="200px" CssClass="text" ></asp:TextBox>
                </td>
            </tr>
        <tr>
                <td class="style1">
                    Téléphone</td>
                <td>
                    <asp:TextBox ID="TbTel" runat="server" Width="200px" CssClass="text" ></asp:TextBox>
                </td>
            </tr>
        <tr>
            <td class="style1">
                Email</td>
            <td>
                <asp:TextBox ID="TbEmail" runat="server" Width="200px" CssClass="text" ></asp:TextBox>
            </td>
        </tr>         
        <tr>
        <td class="style1">
            Numéro Carte d&#39;Identité</td>
        <td>
            <asp:TextBox ID="TbNCI" runat="server" Width="200px" CssClass="text" ></asp:TextBox>
        </td>
        </tr>
            
    </table>
    </fieldset>
</div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="RbFlagAutorise"/>
        <asp:AsyncPostBackTrigger ControlID="BtnSet" />
    </Triggers>
</asp:UpdatePanel>

<asp:UpdatePanel ID="PanelProfil" runat="server" UpdateMode="Conditional"  >
    <ContentTemplate>
        <div style="width:35%;float:left;" ID="DivProfil" runat="server">
        <fieldset class="login" style="width:90%;height:600px">
        <legend>Autorisations</legend>

        <table>

        <tr>
            <td>
                <asp:RadioButtonList ID="RbFlagAutorise" runat="server" AutoPostBack="true" CssClass="text" 
                    RepeatDirection="Horizontal" TextAlign="Left" Width="278px"
                    onselectedindexchanged="RbFlagAutorise_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="Y">Autorisé</asp:ListItem>
                    <asp:ListItem Value=" ">Interdit</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>

        </table>
        <table>

         <tr>
        <td class="style1">Date début validité</td>
        <td>
            <asp:TextBox ID="TbDateDebut" runat="server" Width="200px" CssClass="text"  Enabled="False"></asp:TextBox>
            <asp:CalendarExtender ID="TbDateDebut_CalendarExtender" runat="server" 
                Enabled="True" TargetControlID="TbDateDebut">
            </asp:CalendarExtender>
        </td>
    </tr>
         <tr>
        <td class="style1">Date fin validité</td>
        <td>
            <asp:TextBox ID="TbDateFin" runat="server" Width="200px" CssClass="text"  Enabled="False"></asp:TextBox>
            <asp:CalendarExtender ID="TbDateFin_CalendarExtender" runat="server" 
                Enabled="True" TargetControlID="TbDateFin">
            </asp:CalendarExtender>
        </td>
    </tr>      
         <tr>
        <td class="style1">Droit de pointage</td>
        <td>
            <asp:DropDownList ID="DDLDroit" runat="server" Width="200px"  CssClass="text" 
                AutoPostBack="True" Enabled="False">
            </asp:DropDownList>
        </td>
    </tr>
         <tr>
        <td class="style1">
            <asp:CheckBox ID="CbBadge" runat="server" Text="Autoriser badge"  CssClass="text" 
                AutoPostBack="true" oncheckedchanged="CbBadge_CheckedChanged" 
                Enabled="False" Visible="false" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
        
        </table>

        </fieldset>
        </div>
    </ContentTemplate>
    <Triggers>
            <asp:AsyncPostBackTrigger ControlID="RbFlagAutorise" />
            <asp:AsyncPostBackTrigger ControlID="BtnSet" />
    </Triggers>
</asp:UpdatePanel>

<div style="width:30%;float:left;">
    <div style="text-align:center;">
    <fieldset>
        <asp:Image ID="photo" runat="server" Height="370px"  CssClass="text"  ImageAlign="Middle"  Width="250px" />
    </fieldset>
    </div>
    <fieldset class="login" style="width:90%;">
    <legend>Propriétés Fiche Agent</legend>
    <table>
        <tr>
            <td class="style2">Date de création</td>
            <td>
                <asp:TextBox ID="TbDateCreation" runat="server" ReadOnly="True" Width="130px"  CssClass="text" 
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">Date de modification</td>
            <td width="200px">
                <asp:TextBox ID="TbDateModif" runat="server" ReadOnly="True" Width="130px"  CssClass="text" 
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
    </table>
    </fieldset>

    <asp:UpdatePanel ID="CommandesPanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <fieldset class="login" style="width:90%;">
            <legend>Commandes</legend>
                <table>
                <tr><td runat="server" id="TdSet">
                        <asp:Button ID="BtnSet" runat="server"  CssClass="button" Text="Modifier" onclick="BtnSet_Click" />
                        <asp:Button ID="BtnBack" runat="server" CssClass="button" Text="Retour" onclick="BtnBack_Click" />
                    </td><td runat="server" id="TdSave">
                        <asp:Button ID="BtnSave" runat="server" CssClass="button" Text="Enregistrer" ValidationGroup="validation"
                            onclick="BtnSave_Click" />
                        <asp:Button ID="BtnCancel" runat="server" CssClass="button" Text="Annuler" 
                            onclick="BtnBack_Click" />
                </td></tr>
                </table>
             </fieldset>
         </ContentTemplate>
         <Triggers>
         <asp:PostBackTrigger ControlID="BtnSave" />
         </Triggers>
     </asp:UpdatePanel>
</div> 

</asp:Content>

