<%@ Page Title="controles_Vehicules" Language="C#" MasterPageFile="~/Interfaces/Shared/MasterPage.master" AutoEventWireup="true" CodeFile="ControleTaxi.aspx.cs" Inherits="Interfaces_Vehicule_ControleVehicule" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<style type="text/css">
        .style1
        {
            width: 143px;
        }
        .style2
        {
            width: 115px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

      <asp:Table ID="BarreControle" runat="server" CellPadding="5">

            <asp:TableRow>

                <asp:TableCell>
                    <asp:CheckBox ID="CbFiltre" runat="server" AutoPostBack="true" Checked="true"  CssClass="button" Text="Afficher les filtres" OnCheckedChanged="CbFiltre_CheckedChanged" />
                </asp:TableCell>

                <asp:TableCell>
                    <asp:Button ID="Filtre" runat="server" Text="Actualiser" CssClass="button" onclick="Filtre_Click" />                       
                </asp:TableCell>

                <asp:TableCell>
                    <asp:ImageButton ID="export"  ImageUrl="~/Icons/excel_Listes.png" runat="server" CssClass="button"  ToolTip="Exporter à Excel"  onclick="exportExcel_Click" />
                </asp:TableCell>

                <asp:TableCell >

                    <asp:ImageButton ID="BtnAdd" ImageUrl="~/Icons/Add_Listes.png"  runat="server" CssClass="button" ToolTip="Ajouter" />
                
                    <asp:ModalPopupExtender ID="MPE2" runat="server"  
                    TargetControlID="BtnAdd"  
                    PopupControlID="Panel_Ajout"  
                    DropShadow="true"
                    BackgroundCssClass="ModalPopupBG"
                    OnOkScript="CloseScript()"
                    CancelControlID="BtCancel">
                
                    </asp:ModalPopupExtender>

                </asp:TableCell>

            </asp:TableRow>

      </asp:Table>
    
      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
             <div id="DivFilter" runat="server" visible="true">
             <fieldset style="width:98%;height:65%;" >
             <table style="width: 1174px">
                  <tr>
                     <td>Abréviation du contrôle</td>
                     <td>Date de Fin de validité &lt;</td>
                     <td>N° Taxi</td>
                     <td>Type Taxi</td>
                     <td>N° Immatriculation</td>
                     <td>Nb Contrôles</td>
                 </tr>
                  <tr>
                             <td>
                                 <asp:DropDownList ID="DDLAbrvControle" runat="server" AutoPostBack="True" AppendDataBoundItems="true" CssClass="text"  Width="155px">
                                  <asp:ListItem Value="0" Text=" "></asp:ListItem>
                                  </asp:DropDownList>
                             </td>
                             <td>
                                 <telerik:RadDateTimePicker  ID="TbFiltreDateFin" Runat="server" ShowPopupOnFocus="true"  CssClass="text" 
                                 Culture="fr-FR" AutoPostBackControl="TimeView" >
                                <TimeView ID="TimeView1" runat="server" CellSpacing="-1" Culture="fr-FR"></TimeView>

                                <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>

                                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth=""></DateInput>

                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                 </telerik:RadDateTimePicker>
                            </td>
                             <td>
                                <asp:TextBox ID="TbNTaxi" runat="server" CssClass="text" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TbNTaxi" ValidChars="1234567890" />
                             </td>  
                             <td>
                                 <asp:DropDownList ID="DDLTypeTaxi" runat="server" AutoPostBack="True" Width="155px" CssClass="text" > </asp:DropDownList>
                             </td>
                             <td>
                                <asp:TextBox ID="TbImmatriculation" runat="server" AutoPostBack="True" Width="155px" CssClass="text"></asp:TextBox>
                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TbImmatriculation" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890-" />
                             </td>
                             <td>                       
                                <asp:TextBox ID="NbrLignes" runat="server" CssClass="text" BackColor="Black" Font-Bold="True" ForeColor="White" Height="22px"  Width="60px" AutoPostBack="true"  ReadOnly="true" > </asp:TextBox>
                             </td>
               </tr>
             </table>

            </fieldset>
             </div>
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="CbFiltre" />
                <asp:AsyncPostBackTrigger ControlID="Filtre" />              
                <asp:AsyncPostBackTrigger ControlID="GridViewControlesVehicules" />
                <asp:AsyncPostBackTrigger ControlID="BtSave" />
           
            </Triggers>
        </asp:UpdatePanel>


        


        <asp:UpdatePanel ID="Grid" runat="server" UpdateMode="Conditional">
        <ContentTemplate>


           <asp:GridView ID="GridViewControlesVehicules" 
            runat="server" 
            CssClass="GridViewStyle"
            AutoGenerateColumns="false" 
            ShowFooter="true" 
            ShowHeaderWhenEmpty="true" 
            AllowSorting="True"

           


            OnSorting="Taxi_Controls_Sorting"
            onpageindexchanging="GridViewControlesVehicules_PageIndexChanging" 
            onrowcancelingedit="GridViewControlesVehicules_RowCancelingEdit"
            OnRowCommand="GridViewControlesVehicules_RowCommand"
            OnRowDeleting="GridViewControlesVehicules_RowDeleting"
            onrowediting="GridViewControlesVehicules_RowEditing"
            onrowdatabound="GridViewControlesVehicules_RowDataBound" 
            >
        
  

     
        <FooterStyle CssClass="GridViewFooterStyle" />
        <RowStyle CssClass="GridViewRowStyle" />    
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <HeaderStyle CssClass="GridViewHeaderStyle" />

        <Columns>


          <asp:TemplateField ItemStyle-HorizontalAlign="Center" >
            <ItemTemplate>
                <!--OnRowEditing event.-->
                <asp:ImageButton ID="lbEdit" runat="server" ImageUrl="~/Icons/Modifier5.png" CssClass="button"  ToolTip="Modifier" CausesValidation="false" CommandName="Edit"    CommandArgument='<%#bind("Id") %>'></asp:ImageButton>
                <!-- OnRowDeleting event.-->
                <asp:ImageButton ID="lbDelete" runat="server" ImageUrl="~/Icons/Supprimer.png" CssClass="button" ToolTip="Supprimer" CausesValidation="false" CommandName="Delete"   CommandArgument='<%#bind("Id") %>' OnClientClick="return confirm('Etes-vous sûr que vous voulez supprimer ce contrôle ?');"></asp:ImageButton>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:ImageButton ID="lbCancel" runat="server" ImageUrl="~/Icons/DeleteControle.png" CssClass="button" ToolTip="Annuler"  CausesValidation="false" CommandName="Cancel"   ></asp:ImageButton>
                <asp:ImageButton ID="lbSave" runat="server"  ImageUrl="~/Icons/SaveControle.png"  CssClass="button" ToolTip="Enregitrer" CausesValidation="false" CommandName="Save"    CommandArgument='<%#bind("Id") %>'></asp:ImageButton>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Abréviation du contrôle" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center"  >
            <ItemTemplate>
                <asp:Label ID="LblAbrevControle" runat="server" Text='<%#bind("LibelleControle") %>'></asp:Label>
            </ItemTemplate>
           
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Date de Fin de validité  " HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center"  FooterStyle-HorizontalAlign="Right"  SortExpression="DateFin">
            <ItemTemplate>
                <asp:Label ID="LblDateFin" runat="server" Text='<%#bind("DateFin") %>'></asp:Label>
            </ItemTemplate>
            <FooterTemplate >

                    <asp:Button id="BtnFirst" runat="server" Text="&#9664;&#9664;"  CssClass="text" ToolTip="Première Page" OnClick="BtnFirst_Click" />  
                    <asp:Button id="BtnPrevious" runat="server" Text="&#9664;" CssClass="text" ToolTip="Page Précédente" OnClick="BtnPrevious_Click" />
                         
            </FooterTemplate>

            <EditItemTemplate>
                <asp:TextBox ID="TbSetDateFin" runat="server" Text='<%#bind("DateFin") %>' AutoPostBack="True" Width="70%" ></asp:TextBox>
                <asp:CalendarExtender ID="TbSetDateFin_CalendarExtender" runat="server" Enabled="True" TargetControlID="TbSetDateFin"></asp:CalendarExtender>
            </EditItemTemplate>
           
        </asp:TemplateField>
        <asp:TemplateField HeaderText="N° Taxi  " HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Left" SortExpression="NumTaxi" >
            <ItemTemplate>
                <asp:Label ID="LblNTaxi" runat="server" Text='<%#bind("NumTaxi") %>'></asp:Label>
            </ItemTemplate>
          <FooterTemplate >
                       
            <asp:Button id="BtnNext" runat="server" Text="&#9654;"  CssClass="text" ToolTip="Page Suivante" OnClick="BtnNext_Click"  />    
            <asp:Button id="BtnLast" runat="server" Text="&#9654;&#9654;"  CssClass="text" ToolTip="Dernière Page" OnClick="BtnLast_Click" />
                             
          </FooterTemplate>
            
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Type Taxi  " HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center" SortExpression="TypeTaxi"  >
            <ItemTemplate>
                <asp:Label ID="LblTypeTaxi" runat="server" Text='<%#bind("TypeTaxi") %>'></asp:Label>
            </ItemTemplate>
           
        </asp:TemplateField>
        <asp:TemplateField HeaderText="N° Immatriculation  " HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center"  SortExpression="Immat" >
            <ItemTemplate>
                <asp:Label ID="LblImmatriculation" runat="server" Text='<%#bind("Immat") %>'></asp:Label>
            </ItemTemplate>
           
        </asp:TemplateField>

      
       
        </Columns>
    </asp:GridView>



        </ContentTemplate >
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Filtre" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="GridViewControlesVehicules" />
             <asp:AsyncPostBackTrigger ControlID="BtSave"  EventName="Click"/>
  
        </Triggers>
        </asp:UpdatePanel>
  


    <asp:Panel ID="Panel_Ajout" CssClass="ModalPopupBG" runat="server" BackColor="Black"  Width="600px">
    <div class="popup_Container">
        <div class="popup_Titlebar" id="PopupHeader">
            <div class="TitlebarLeft">
                Ajouter un Contrôle Véhicule</div>
            <div class="TitlebarRight" onclick="$get('MainContent_BtCancel').click();" >
            </div>
        </div>
        <div class="popup_Body" style=" margin-left:150px; height: 250px;">
        <asp:Table ID="Table_Ajout" runat="server">

        <asp:TableRow>
            <asp:TableCell>
                Abréviation Controle&nbsp;
                                 <asp:RequiredFieldValidator ValidationGroup="validation" runat="server" ControlToValidate="_DDLAbrvControleVehicule" 
                    ErrorMessage="champ obligatoire" Display="None" ID="RequiredFieldValidator4" ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3"
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                </asp:ValidatorCalloutExtender>
            </asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList ID="_DDLAbrvControleVehicule" runat="server" CssClass="text" Width="204px"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Height="10px"></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                Date&nbsp; Fin&nbsp; validité&nbsp;
            </asp:TableCell>
            <asp:TableCell>

                <asp:TextBox ID="_TbDateFin"  Width="200px" CssClass="text" runat="server" ValidationGroup="validation"></asp:TextBox>
                <asp:CalendarExtender ID="TbDateJour_CalendarExtender" runat="server" Enabled="True" TargetControlID="_TbDateFin" ></asp:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ControlToValidate="_TbDateFin" Display="None" ErrorMessage="champ obligatoire" ValidationGroup="validation"  ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="true" TargetControlID="RequiredFieldValidator" ></asp:ValidatorCalloutExtender>
                <asp:CompareValidator ID="CompareValidator" runat="server" ControlToValidate="_TbDateFin" Type="Date" Operator="DataTypeCheck" Display="None" ErrorMessage="Format incorrect" ValidationGroup="validation"></asp:CompareValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="true" TargetControlID="CompareValidator" ></asp:ValidatorCalloutExtender>                                   
            
            </asp:TableCell>
        </asp:TableRow>
         <asp:TableRow Height="10px"></asp:TableRow>    
        <asp:TableRow>
                    <asp:TableCell>
            N° Taxi
        </asp:TableCell>
                    <asp:TableCell>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" >
                                <ContentTemplate>
                                    <asp:TextBox ID="_TbNumTaxi" runat="server" CssClass="text" AutoPostBack="true" OnTextChanged="_TbNumTaxi_TextChanged" Width="200px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="validation" runat="server" ControlToValidate="_TbNumTaxi"  ErrorMessage="champ obligatoire" Display="None" ID="RequiredFieldValidator1"  ></asp:RequiredFieldValidator>
                                <asp:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1"> </asp:ValidatorCalloutExtender>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="_TbNumTaxi" ValidChars="1234567890" />  
                              
                                </ContentTemplate>
                               <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="_TbNumTaxi" />
                                </Triggers>
                        </asp:UpdatePanel>    
                    </asp:TableCell>
        </asp:TableRow>
          <asp:TableRow Height="10px"></asp:TableRow>                
        <asp:TableRow>
                    <asp:TableCell>
            TypeTaxi
        </asp:TableCell>
                    <asp:TableCell>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" >
                                <ContentTemplate>
                                    <asp:DropDownList ID="_DDLTypeTaxi" runat="server" CssClass="text" AutoPostBack="true" OnTextChanged="_TbNumTaxi_TextChanged" Width="204px"></asp:DropDownList>                            
                                </ContentTemplate>
                              
                        </asp:UpdatePanel>    
                    </asp:TableCell>
        </asp:TableRow>
          <asp:TableRow Height="10px"></asp:TableRow>             
        <asp:TableRow >
           <asp:TableCell>
                   N° Immatriculation
                </asp:TableCell>
           <asp:TableCell>
            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
                <asp:TextBox ID="_TbImmatriculation" runat="server" CssClass="text" AutoPostBack="true"  Width="200px" ReadOnly="true"></asp:TextBox>
            </ContentTemplate>
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="_TbNumTaxi" />
            <asp:AsyncPostBackTrigger ControlID="_DDLTypeTaxi" />          
            </Triggers>
            </asp:UpdatePanel>
        </asp:TableCell>
        </asp:TableRow>
         <asp:TableRow Height="30px"></asp:TableRow>           
        </asp:Table>
            
             <div style="text-align: center"> 
                <asp:Button ID="BtSave" runat="server" Text="Enregistrer" CssClass="button" ValidationGroup="validation" OnClick="BtSave_Click"  />
                <div class="divider"/>
                <asp:Button ID="BtCancel" runat="server" Text="Annuler"  CssClass="button" OnClick="BtCancel_Click" />
            
      
      </div>
        
       
        </div>
    </div>
    </asp:Panel>

</asp:Content>

