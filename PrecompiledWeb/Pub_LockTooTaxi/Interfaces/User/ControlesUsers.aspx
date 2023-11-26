<%@ page title="Controles_chauffeurs" language="C#" masterpagefile="~/Interfaces/Shared/MasterPage.master" autoeventwireup="true" inherits="Interfaces_User_ControlesUsers, App_Web_bsanhxqr" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit"  %>

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

            <asp:TableRow >

            <asp:TableCell >
                <asp:CheckBox ID="CbFiltre" runat="server"   CssClass="button" AutoPostBack="true" Checked="true" Text="Afficher les filtres" OnCheckedChanged="CbFiltre_CheckedChanged" />
            </asp:TableCell>

            <asp:TableCell>
                <asp:Button ID="Filtre" runat="server"  CssClass="button" Text="Actualiser"  onclick="Filtre_Click" ValidationGroup="filtre" />
            </asp:TableCell>

            <asp:TableCell >
                <asp:ImageButton ID="export"  CssClass="button"  ImageUrl="~/Icons/excel_Listes.png" runat="server"   ToolTip="Exporter à Excel"  onclick="exportExcel_Click" ValidationGroup="filtre"/>
            </asp:TableCell>

            <asp:TableCell >

                <asp:ImageButton ID="BtnAdd"  CssClass="button" ImageUrl="~/Icons/Add_Listes.png"  runat="server" ToolTip="Ajouter" />
                
                <asp:ModalPopupExtender ID="MPE" runat="server"  
                TargetControlID="BtnAdd"  
                PopupControlID="Panel_Ajout"             
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
                       
                        <td>Contrôle chauffeur </td>
                        <td>Date Fin validité </td>
                        <td>N° Permis</td>
                        <td>CIN</td>
                        <td>Nom </td>
                        <td>Prénom </td>
                        <td>Nb Contrôles </td>
                        </tr>


            <tr>
        
                <td>
                <asp:DropDownList ID="DDLAbrvControleUser" runat="server"  CssClass="text"  Width="155px" AppendDataBoundItems="true" AutoPostBack="true" >
                <asp:ListItem Value="0" Text=" "></asp:ListItem>
                </asp:DropDownList>
                </td>
                <td >
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
                    <asp:TextBox ID="TbNumPermis" CssClass="text"  AutoPostBack="true" runat="server" ></asp:TextBox>
                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="TbNumPermis" ValidChars="1234567890" />
                </td>
                <td>
                    <asp:TextBox ID="TbMatricule" CssClass="text"  AutoPostBack="true" runat="server" ></asp:TextBox>
                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TbMatricule" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890" />
                </td>
                <td>
                    <asp:TextBox ID="TbNom"  CssClass="text"  AutoPostBack="true" runat="server" ></asp:TextBox> 
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TbNom" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />  
                </td>
                <td>
                    <asp:TextBox ID="TbPrenom" CssClass="text"  AutoPostBack="true"  runat="server" ></asp:TextBox>   
                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="TbPrenom" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />  
                </td>
                <td>
                        
                 <asp:TextBox ID="NbrLignes" CssClass="text"  runat="server" BackColor="Black" Font-Bold="True" ForeColor="White" Height="22px"  Width="60px" AutoPostBack="true"  ReadOnly="true" > </asp:TextBox>
                </td>

            </tr>

                 </table>
                </fieldset>
         </div>
        </ContentTemplate>
        <Triggers>

            <asp:AsyncPostBackTrigger ControlID="CbFiltre" />
            <asp:AsyncPostBackTrigger ControlID="Filtre" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="GridViewControlesUsers" />
            <asp:AsyncPostBackTrigger ControlID="BtSave" EventName="Click" />
                                  
        </Triggers>
    </asp:UpdatePanel>
 

  <asp:UpdatePanel ID="Grid" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

     

            <asp:GridView ID="GridViewControlesUsers"
           
            CssClass="GridViewStyle" 
            runat="server" 
            AutoGenerateColumns="false"          
            ShowFooter="true" 
            ShowHeaderWhenEmpty="true"             
            AllowSorting="True"


            OnSorting="Users_Controls_Sorting" 
            onrowdatabound="GridViewControlesUsers_RowDataBound" 
            onpageindexchanging="GridViewControlesUsers_PageIndexChanging"            
            onrowcancelingedit="GridViewControlesUsers_RowCancelingEdit" 
            onrowediting="GridViewControlesUsers_RowEditing" 
            OnRowDeleting="GridViewControlesUsers_RowDeleting"
            onrowupdating="GridViewControlesUsers_RowUpdating"
            
             >
             
          
      

            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />    
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />



        <Columns>
        
        <asp:TemplateField  ItemStyle-HorizontalAlign="Center">
        
            <ItemTemplate >
                <!--OnRowEditing event.-->
                <asp:ImageButton   ID="lbEdit" ImageUrl="~/Icons/Modifier5.png"  CssClass="button"  ToolTip="Modifier" runat="server"  CausesValidation="false" CommandName="Edit"   CommandArgument='<%#bind("Id") %>' ></asp:ImageButton>
                <!-- OnRowDeleting event.-->
                <asp:ImageButton   ID="lbDelete" ImageUrl="~/Icons/Supprimer.png"  CssClass="button"  ToolTip="Supprimer" runat="server" CausesValidation="false" CommandName="Delete"   CommandArgument='<%#bind("Id") %>'   OnClientClick="return confirm('Etes-vous sûr que vous voulez supprimer ce contrôle ?');" ></asp:ImageButton>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:ImageButton ID="lbSave" ImageUrl="~/Icons/SaveControle.png"  CssClass="button"  ToolTip="Enregitrer" runat="server"  CausesValidation="false" CommandName="Update"    CommandArgument='<%#bind("Id") %>'></asp:ImageButton>
                <asp:ImageButton ID="lbCancel" ImageUrl="~/Icons/DeleteControle.png" CssClass="button"   ToolTip="Annuler" runat="server"  CausesValidation="false" CommandName="Cancel"   ></asp:ImageButton>
              
            </EditItemTemplate>
            
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Abréviation contrôle chauffeur" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center" >
            <ItemTemplate >
                <asp:Label ID="LblAbrevLabelControle" runat="server" Text='<%#bind("AbrevLabelControle") %>'></asp:Label>
            </ItemTemplate>

        <HeaderStyle Width="20%"></HeaderStyle>
        </asp:TemplateField>
        <asp:TemplateField  HeaderText= " Date Fin validité  " HeaderStyle-Width="15%" SortExpression="DateFinValidité"  FooterStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="Center" >
            <ItemTemplate>
                <asp:Label ID="LblDateFin" runat="server" Text='<%#bind("DateFin") %>'></asp:Label>
            </ItemTemplate>
             <FooterTemplate >

                    <asp:Button id="BtnFirst" runat="server" Text="&#9664;&#9664;"  CssClass="text" ToolTip="Première Page" OnClick="BtnFirst_Click" />  
                    <asp:Button id="BtnPrevious" runat="server" Text="&#9664;" CssClass="text" ToolTip="Page Précédente" OnClick="BtnPrevious_Click" />
                         
            </FooterTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TbSetDateFin" runat="server" Text='<%#bind("DateFin") %>' AutoPostBack="True" Width="70%" ></asp:TextBox>
                <asp:CalendarExtender ID="TbSetDateFin_CalendarExtender" runat="server" Enabled="True" TargetControlID="TbSetDateFin"> </asp:CalendarExtender>
            </EditItemTemplate>

        <HeaderStyle Width="15%"></HeaderStyle>
        </asp:TemplateField>
               <asp:TemplateField  HeaderText="N° Permis  " HeaderStyle-Width="15%" SortExpression="NumPermis"  FooterStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" >
            <ItemTemplate>
                <asp:Label ID="LblNumPermis" runat="server" Text='<%#bind("NumPermis") %>'></asp:Label>
            </ItemTemplate>
          <FooterTemplate >
                       
            <asp:Button id="BtnNext" runat="server" Text="&#9654;"  CssClass="text" ToolTip="Page Suivante" OnClick="BtnNext_Click"  />    
            <asp:Button id="BtnLast" runat="server" Text="&#9654;&#9654;"  CssClass="text" ToolTip="Dernière Page" OnClick="BtnLast_Click" />
                             
          </FooterTemplate>

        </asp:TemplateField>
        <asp:TemplateField  HeaderText="CIN  " HeaderStyle-Width="15%" SortExpression="Matricule" ItemStyle-HorizontalAlign="Center" >
            <ItemTemplate>
                <asp:Label ID="LblMatricule" runat="server" Text='<%#bind("Matricule") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField   HeaderText="Nom  " HeaderStyle-Width="15%"  SortExpression="Nom">
            <ItemTemplate>
                <asp:Label ID="LblNom" runat="server" Text='<%#bind("Nom") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Prénom  " HeaderStyle-Width="15%" SortExpression="Prenom">
            <ItemTemplate>
                <asp:Label ID="LblPrenom" runat="server" Text='<%#bind("Prenom") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        </Columns>
    </asp:GridView>

        </ContentTemplate >
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Filtre" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="GridViewControlesUsers" />

            
        </Triggers>
        </asp:UpdatePanel>
    

<asp:Panel ID="Panel_Ajout" CssClass="ModalPopupBG" runat="server" BackColor="Black" Width="600px"  >
    <div class="popup_Container">
        <div class="popup_Titlebar" id="PopupHeader">
            <div class="TitlebarLeft">
                Ajouter un Contrôle Chauffeur</div>
            <div class="TitlebarRight" onclick="$get('MainContent_BtCancel').click();" >
            </div>
        </div>
        <div class="popup_Body" style=" margin-left:150px; height: 250px;">
        <asp:Table ID="Table_Ajout" runat="server">

        <asp:TableRow >
            <asp:TableCell>
                Contrôle chauffeur:
                 <asp:RequiredFieldValidator ValidationGroup="validation" runat="server" ControlToValidate="_DDLAbrvControleUser" 
                    ErrorMessage="champ obligatoire" Display="None" ID="RequiredFieldValidator4" ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3"
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                </asp:ValidatorCalloutExtender>
            </asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList ID="_DDLAbrvControleUser"  CssClass="text" runat="server" Width="186px"></asp:DropDownList>
                     
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Height="10px"></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                Date Fin validité :
            </asp:TableCell>
            <asp:TableCell>

                <asp:TextBox ID="_TbDateFin"  CssClass="text" Width="180px" runat="server" ValidationGroup="validation"></asp:TextBox>
                <asp:CalendarExtender ID="TbDateJour_CalendarExtender" runat="server" Enabled="True" TargetControlID="_TbDateFin" ></asp:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ControlToValidate="_TbDateFin" Display="None" ErrorMessage="champ obligatoire" ValidationGroup="validation"  ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="true" TargetControlID="RequiredFieldValidator" ></asp:ValidatorCalloutExtender>
                <asp:CompareValidator ID="CompareValidator" runat="server" ControlToValidate="_TbDateFin" Type="Date" Operator="DataTypeCheck" Display="None" ErrorMessage="Format incorrect" ValidationGroup="validation"></asp:CompareValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="true" TargetControlID="CompareValidator" ></asp:ValidatorCalloutExtender>                                   
            
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Height="10px"></asp:TableRow>
        <asp:TableRow >
           <asp:TableCell>
                    N° Permis               :
           </asp:TableCell>
           <asp:TableCell>
            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" >
             <ContentTemplate>
                <asp:TextBox ID="_TbNumPermis" runat="server" CssClass="text" Width="180px" AutoPostBack="true" OnTextChanged="_TbNumPermis_TextChanged" CausesValidation="true" ></asp:TextBox>
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
        <asp:TableRow>
                    <asp:TableCell>
            CIN         :     
        </asp:TableCell>
                    <asp:TableCell>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" >
                                <ContentTemplate>
                                    <asp:TextBox ID="_TbMatricule" runat="server" CssClass="text" Width="180px" AutoPostBack="true" OnTextChanged="_TbMatricule_TextChanged" CausesValidation="true" ></asp:TextBox>
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
                    Non et Prénom     :
           </asp:TableCell>
           <asp:TableCell>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" >
             <ContentTemplate>
                <asp:TextBox ID="_TbNomPrenom" runat="server" CssClass="text" ReadOnly="true" Width="180px" AutoPostBack="true" ></asp:TextBox>               
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
        <asp:TableRow Height="30px"></asp:TableRow>
            
        </asp:Table>
         <div style="text-align: center"> 
    <asp:Button ID="BtSave" runat="server"    CssClass="button" Text="Enregistrer" ValidationGroup="validation" OnClick="BtSave_Click" />
    <div class="divider"/>
      <asp:Button ID="BtCancel" runat="server" CssClass="button" Text="Annuler" OnClick="BtCancel_Click" />
</div>
        </div>
    </div>
    </asp:Panel>
</asp:Content>

