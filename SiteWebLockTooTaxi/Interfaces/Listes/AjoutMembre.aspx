
<%@ Page Language="C#" MasterPageFile="~/Interfaces/Shared/MasterPage.master" AutoEventWireup="true" CodeFile="AjoutMembre.aspx.cs" Inherits="Interfaces_Listes_AjoutMembre" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit"  %>
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
    <div Id="Div1" runat="server" style="margin-left:450px;">
    <fieldset class="login"  style="width:438px; height:297px;">
    <legend>Ajout Membre à La liste </legend>
    <asp:Table ID="Table_Ajout_ToList" runat="server">

         <asp:TableRow >
           <asp:TableCell>
                    N° Permis               :
           </asp:TableCell>
           <asp:TableCell>
            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" >
             <ContentTemplate>
                <asp:TextBox ID="_TbNumPermis" runat="server" CssClass="text" Width="250px" AutoPostBack="true" OnTextChanged="_TbNumPermis_TextChanged" CausesValidation="true" ></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="validation" runat="server" ControlToValidate="_TbNumPermis"  ErrorMessage="champ obligatoire" Display="None" ID="RequiredFieldValidator2"  ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2"> </asp:ValidatorCalloutExtender>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="_TbNumPermis" ValidChars="1234567890" />  
                </ContentTemplate>
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="_TbMatricule" />
            </Triggers>
            </asp:UpdatePanel>
        </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Height="10px"></asp:TableRow>
        <asp:TableRow ID="RowMatricule" runat = "server">
                    <asp:TableCell>
            CIN         :     
        </asp:TableCell>
                    <asp:TableCell>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" >
                                <ContentTemplate>
                                    <asp:TextBox ID="_TbMatricule" runat="server" CssClass="text" Width="250px" AutoPostBack="true" OnTextChanged="_TbMatricule_TextChanged" CausesValidation="true" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="validation" runat="server" ControlToValidate="_TbMatricule"  ErrorMessage="champ obligatoire" Display="None" ID="RequiredFieldValidator1"  ></asp:RequiredFieldValidator>
                                <asp:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1"> </asp:ValidatorCalloutExtender>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="_TbMatricule" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890" />  
                                </ContentTemplate>
                                <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="_TbNumPermis" />
                                </Triggers>
                        </asp:UpdatePanel>    
                    </asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow Height="10px"></asp:TableRow>
        
        <asp:TableRow >
           <asp:TableCell>
                    Nom et Prénom     :
           </asp:TableCell>
           <asp:TableCell>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" >
             <ContentTemplate>
                <asp:TextBox ID="_TbNomPrenom" runat="server" CssClass="text" ReadOnly="true" Width="250px" AutoPostBack="true" ></asp:TextBox>               
                 <asp:RequiredFieldValidator ValidationGroup="validation" runat="server" ControlToValidate="_TbNomPrenom"  ErrorMessage="champ obligatoire" Display="None" ID="RequiredFieldValidator3"  ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3"> </asp:ValidatorCalloutExtender>
                </ContentTemplate>
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="_TbMatricule" />
            <asp:AsyncPostBackTrigger ControlID="_TbNumPermis" />
            </Triggers>
            </asp:UpdatePanel>
        </asp:TableCell>
        </asp:TableRow>
 
        <asp:TableRow style="height:10px;" ></asp:TableRow>
        <asp:TableRow ID="RowMotifs" runat = "server">
            <asp:TableCell>
                Motifs Demande :
            </asp:TableCell>
                    <asp:TableCell>           
                        <asp:DropDownList ID="DDLMotifs" runat="server" CssClass="text" Width="300px" ></asp:DropDownList>             
                    </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow style="height:10px;" ></asp:TableRow>


        </asp:Table>
         
         <div style="height:50px;" ></div>
        <div style="text-align: center"> 
                <asp:Button ID="BtSave3" runat="server" CssClass="button" Text="Enregistrer" ValidationGroup="validation" OnClick="BtSave3_Click"  />
         <div class="divider"/>
                <asp:Button ID="BtCancel3" runat="server" CssClass="button" Text="Annuler" OnClick="BtCancel3_Click" />
      </div>

    </fieldset>
     </div>

    
    </ContentTemplate>
    <Triggers>      
        <asp:AsyncPostBackTrigger ControlID="BtSave3" />
        <asp:PostBackTrigger ControlID="BtCancel3" />
       
    </Triggers>
</asp:UpdatePanel>





</asp:Content>

