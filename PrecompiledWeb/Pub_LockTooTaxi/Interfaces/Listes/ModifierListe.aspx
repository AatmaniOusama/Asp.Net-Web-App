
<%@ page language="C#" masterpagefile="~/Interfaces/Shared/MasterPage.master" autoeventwireup="true" inherits="Interfaces_Listes_ModifierListe, App_Web_qjoeb4aj" %>

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
    <div Id="Div1" runat="server"  style="margin-left:450px;">
    <fieldset class="login"  style="width:438px; height:200px;">
    <legend>Modifier la Liste</legend>
    <asp:Table ID="Table_Modifer" runat="server">

        <asp:TableRow>
            <asp:TableCell>
                Abréviation Liste &nbsp;
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="TbAbrevListe" runat="server" Width="200px" CssClass="text" ></asp:TextBox>
                  <asp:RequiredFieldValidator ValidationGroup="Ajouter" runat="server" ControlToValidate="TbAbrevListe" 
                    ErrorMessage="champ obligatoire" Display="None" ID="RequiredFieldValidator1" ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1"
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                </asp:ValidatorCalloutExtender>
            </asp:TableCell>
        </asp:TableRow> 
        <asp:TableRow style="height :10px;" ></asp:TableRow>
        <asp:TableRow >
           <asp:TableCell>
                    Libellé Liste
                </asp:TableCell>
           <asp:TableCell>                   
              <asp:TextBox ID="TbLibelleListe" runat="server" Width="200px"  ReadOnly="false" CssClass="text" ></asp:TextBox>   
               <asp:RequiredFieldValidator ValidationGroup="Ajouter" runat="server" ControlToValidate="TbLibelleListe" 
                    ErrorMessage="champ obligatoire" Display="None" ID="RequiredFieldValidator3" ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender"
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                </asp:ValidatorCalloutExtender>
        </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow style=" height :10px;" ></asp:TableRow>
        <asp:TableRow>
                     <asp:TableCell>
                    Type Liste
                </asp:TableCell>
                    <asp:TableCell>
                     
                                    <asp:DropDownList ID="DDLTypeListe" runat="server" Enabled="false"  CssClass="text" Width="200px">
                                    <asp:ListItem Text="Chauffeurs" Value="1"></asp:ListItem>
                                   <asp:ListItem Text="Opérateurs" Value="2" ></asp:ListItem>
                                   <asp:ListItem Text="Agents" Value="4" ></asp:ListItem>
                                   <asp:ListItem Text="Chauffeurs demandés" Value="8"  Selected="True" ></asp:ListItem>
                                    </asp:DropDownList>
                                
                          
                    </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow style="height:10px;" ></asp:TableRow>
        <asp:TableRow>

            <asp:TableCell ColumnSpan="4" HorizontalAlign="right">
                <asp:Button ID="BtSave" runat="server" CssClass="button" Text="Enregistrer" ValidationGroup="Ajouter" OnClick="BtSave_Click" />
                <asp:Button ID="BtCancel" runat="server" CssClass="button" Text="Annuler" OnClick="BtCancel_Click" />
            </asp:TableCell>
        </asp:TableRow>

    </asp:Table>


        


    </fieldset>
     </div>

    
    </ContentTemplate>
    <Triggers>      
        <asp:AsyncPostBackTrigger ControlID="BtSave" />
        <asp:PostBackTrigger ControlID="BtCancel" />
       
    </Triggers>
</asp:UpdatePanel>





</asp:Content>

