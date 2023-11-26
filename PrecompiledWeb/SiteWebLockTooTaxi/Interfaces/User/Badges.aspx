<%@ page title="Badges" language="C#" masterpagefile="~/Interfaces/Shared/MasterPage.master" autoeventwireup="true" inherits="Interfaces_User_Badges, App_Web_nbeglkqp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    

    <style type="text/css">
        .style1
        {
            width: 294px;
        }
        
         
    </style>
    

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

 
            <asp:Table ID="BarreControle" runat="server" CellPadding="5">
            <asp:TableRow VerticalAlign="Bottom">

            <asp:TableCell>
                <asp:CheckBox ID="CbFiltre" runat="server" AutoPostBack="true" Checked="true" Text="Afficher les filtres" OnCheckedChanged="CbFiltre_CheckedChanged" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="BtnActualiser"  runat="server" Text="Actualiser" CssClass="button" onclick="Filtre" />
            </asp:TableCell>
            <asp:TableCell>
              <asp:ImageButton ID="export"  ImageUrl="~/Icons/excel.png" runat="server"  CssClass="button"  ToolTip="Exporter à Excel"  onclick="exportExcel_Click" />
           </asp:TableCell>
            <asp:TableCell>
                <asp:ImageButton ID="BtnAdd" ImageUrl="~/Icons/Add_Badge.png" runat="server" CssClass="button" ToolTip="Ajouter"  OnClick="BtnAdd_Click" />
           
            <asp:ModalPopupExtender ID="MPE" runat="server"  
                TargetControlID="BtnAdd"  
                PopupControlID="Panel_Ajout"  
                
                BackgroundCssClass="ModalPopupBG"
                OnOkScript="CloseScript()"
                CancelControlID="BtCancel">             
                </asp:ModalPopupExtender>

            </asp:TableCell>

             
            <asp:TableCell>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional"  >
                <ContentTemplate>
                <asp:ImageButton ID="BtnDelete" ImageUrl="~/Icons/Delete_Badge.png" runat="server"  CssClass="button" ToolTip="Supprimer"  OnClick="BtnDelete_Click"  OnClientClick="return confirm('Etes-vous sûr que vous voulez supprimer ce badge ?');" Visible = "false"  />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridViewBadges" EventName="SelectedIndexChanging" />
                    <asp:AsyncPostBackTrigger ControlID="GridViewBadges" EventName="RowDataBound" />
                    <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
                     <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
                </Triggers>
            </asp:UpdatePanel>

            </asp:TableCell>
            <asp:TableCell>
                 <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional"  >
                <ContentTemplate> 
                <asp:ImageButton ID="BtAutoriser" ImageUrl="~/Icons/Autoriser_Badge.png"  CssClass="button" runat="server" ToolTip="Autoriser"  OnClick="BtAutoriser_Click" Visible = "false" />
                <asp:ImageButton ID="BtInterdire" ImageUrl="~/Icons/Interdire_Badge.png" CssClass="button" runat="server" ToolTip="Interdire"  OnClick="BtInterdir_Click"  Visible = "false" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridViewBadges" EventName="SelectedIndexChanging" />
                    <asp:AsyncPostBackTrigger ControlID="GridViewBadges" EventName="RowDataBound" />
                    <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
                    <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
                </Triggers>
            </asp:UpdatePanel>
            
            </asp:TableCell>
            
            </asp:TableRow>
            </asp:Table>
      
    
    <asp:UpdatePanel runat="server" ID="UpdatePanel_Filtre" UpdateMode="Conditional" >
    <ContentTemplate>     
        <asp:Panel  id="Panelfilter" runat="server" Visible="true" style="width:99.4%;height:95px; margin-left:8px;" >
            <asp:Literal ID="Literal_MsgBox" runat="server"></asp:Literal>
            <fieldset  style="width:98%;height:65%;">

                <%--<legend>Filtres</legend>--%>

                <table style="width: 1174px">
                    <tr>
                        <td>N° Permis</td>
                        <td>Nom</td>
                        <td>Prénom</td>
                        <td>CIN</td>
                        
                        <td rowspan="3">
                        <asp:RadioButtonList ID="RbAutorise" runat="server" AutoPostBack="True" >
                            <asp:ListItem Value="YN" Selected="True">Tous</asp:ListItem>
                            <asp:ListItem Value="Y" >Autorisés</asp:ListItem>
                            <asp:ListItem Value=" ">Interdits</asp:ListItem>
                            
                        </asp:RadioButtonList>

                        </td>
                        <td rowspan="3">
                            <asp:RadioButtonList ID="RbAttribue" runat="server" AutoPostBack="True" >
                                <asp:ListItem Value="-1" Selected="True" >Tous</asp:ListItem>
                                <asp:ListItem Value="1" >Attribués</asp:ListItem>
                                <asp:ListItem Value="0" >Non Attribués</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        
                        <td class="style1">

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TbNumPermis" runat="server" AutoPostBack="true" CssClass="text" Width="90px"></asp:TextBox>
                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TbNumPermis" ValidChars="1234567890" />
                        </td>

                        <td>
                            <asp:TextBox ID="TbNom" runat="server" AutoPostBack="true" CssClass="text"  Width="90px"></asp:TextBox>
                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TbNom" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                        </td>

                         <td >
                            <asp:TextBox ID="TbPrenom" runat="server" AutoPostBack="true" CssClass="text"  Width="90px" ></asp:TextBox>
                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="TbPrenom" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                        </td>

                         <td>
                            <asp:TextBox ID="TbMatricule" runat="server" AutoPostBack="true" CssClass="text"   Width="90px"></asp:TextBox>
                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="TbMatricule" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890" />
                        </td>

                        
                      
                        <td class="style1">&nbsp;
                        
                            <asp:TextBox ID="NbrLignes" runat="server" BackColor="Black" Font-Bold="True"
                             ForeColor="White" Height="23px" style="margin-left: 108px" Width="59px" ReadOnly="true">
                             </asp:TextBox>&nbsp; 
                             Badges

                         </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="CbFiltre" EventName="CheckedChanged" />
        <asp:AsyncPostBackTrigger ControlID="BtnActualiser" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="BtnDelete" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="BtAutoriser"  />  
         <asp:AsyncPostBackTrigger ControlID="BtInterdire"  /> 
    </Triggers>
    </asp:UpdatePanel>      
   

    



    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional" >
    <ContentTemplate>
    

  

        <asp:GridView ID="GridViewBadges" 
        CssClass="GridViewStyle" 
        runat="server"
        AutoGenerateColumns="False"  
        ShowFooter="true"   
       
        ShowHeaderWhenEmpty="true"
        AllowSorting="True"
        OnSorting="badges_Sorting" 

        onrowdeleting="GridViewBadges_RowDeleting" 
        OnPageIndexChanging="GridViewBadges_IndexChanging" 
        onrowdatabound="GridViewBadges_RowDataBound" 
        onselectedindexchanging="GridViewBadges_SelectedIndexChanging">
         
   
         
        <FooterStyle CssClass="GridViewFooterStyle"/>
        <RowStyle CssClass="GridViewRowStyle" />    
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />

     
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle"/>
        <HeaderStyle CssClass="GridViewHeaderStyle"/>

    
                <Columns>
                   
             <asp:ButtonField  Text="" CommandName="Select"  HeaderStyle-Width="0px" HeaderStyle-BackColor="White" HeaderStyle-BorderColor="White"  FooterStyle-Width="0px" FooterStyle-BackColor="White" FooterStyle-BorderColor="White" ItemStyle-Width="0px" ItemStyle-BackColor="White" ItemStyle-BorderColor="White" />
                 <asp:TemplateField  ItemStyle-HorizontalAlign="Center" ItemStyle-Height ="25px" ItemStyle-Width="2px" HeaderStyle-Width="2%" >

                   <ItemTemplate>
                     <asp:ImageButton   ID="ImgValide" ImageUrl="~/Icons/button-green.png"  CssClass="button"  ToolTip="Valide" runat="server"   CommandName="Visualiser" > </asp:ImageButton>
                      <asp:ImageButton   ID="ImgInvalide" ImageUrl="~/Icons/button-red.png"  CssClass="button"  ToolTip="Invalide" runat="server"    CommandName="Visualiser"> </asp:ImageButton>
               </ItemTemplate>
                 </asp:TemplateField>
            <asp:TemplateField HeaderText="N° Permis  " ItemStyle-HorizontalAlign="Center"   HeaderStyle-Width="18%" SortExpression="NumPermis"  FooterStyle-HorizontalAlign="Right">
              
               <ItemTemplate>
                    <asp:Label ID="LbNumPermis" runat="server"  Text='<%#bind("NumBadge") %>'></asp:Label>
                </ItemTemplate>



             </asp:TemplateField>

            <asp:TemplateField   HeaderText="Autorisé  " HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center"  SortExpression="FlagAutorise"  ControlStyle-Height="25"  >
                <ItemTemplate>
                    <asp:Label runat="server" ID="Selected" Text='<%#bind("FlagAutorise") %>' Visible="true" ></asp:Label>

                
                </ItemTemplate>
            </asp:TemplateField >
            <asp:TemplateField HeaderText="Nom  "  SortExpression="Nom" FooterStyle-HorizontalAlign="right" HeaderStyle-Width="20%" >
                <ItemTemplate>
                    <asp:Label ID="LblNom" runat="server"  Text='<%#bind("Nom") %>'></asp:Label>
                </ItemTemplate>

                  <FooterTemplate >

                     <asp:Button id="BtnFirst" runat="server" Text="&#9664;&#9664;"  CssClass="text" ToolTip="Première Page" OnClick="BtnFirst_Click" />  
                         <asp:Button id="BtnPrevious" runat="server" Text="&#9664;" CssClass="text" ToolTip="Page Précédente" OnClick="BtnPrevious_Click" />
                                
                </FooterTemplate>

            </asp:TemplateField>
            <asp:TemplateField HeaderText="Prénom  "  FooterStyle-HorizontalAlign="left" SortExpression="Prenom" HeaderStyle-Width="20%" >
                <ItemTemplate>
                    <asp:Label ID="LblPrenom" runat="server" Text='<%#bind("Prenom") %>'></asp:Label>
                </ItemTemplate>
                 <FooterTemplate >

                 <asp:Button id="BtnNext" runat="server" Text="&#9654;"  CssClass="text"  ToolTip="Page Suivante" OnClick="BtnNext_Click"  />  
                 <asp:Button id="BtnLast" runat="server" Text="&#9654;&#9654;"  CssClass="text"   ToolTip="Dernière Page" OnClick="BtnLast_Click" />
                 
                </FooterTemplate>  

            </asp:TemplateField>                
                        <asp:TemplateField HeaderText="CIN  "  FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"   SortExpression="Matricule" HeaderStyle-Width="20%" >
                <ItemTemplate>
                        <asp:Label ID="LblMatricule" runat="server" Text='<%#Eval("Matricule") %>'></asp:Label>
                </ItemTemplate>
             </asp:TemplateField> 
                      
                </Columns>
            </asp:GridView>

  

    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
        <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
        <asp:AsyncPostBackTrigger ControlID="BtAutoriser"  />  
         <asp:AsyncPostBackTrigger ControlID="BtInterdire"  />    
        <asp:AsyncPostBackTrigger ControlID="GridViewBadges"  />    
    </Triggers>
    </asp:UpdatePanel>

    <asp:Panel ID="Panel_Ajout" CssClass="ModalPopupBG" runat="server" BackColor="Black" Width="400px"  >
    <div class="popup_Container">
        <div class="popup_Titlebar" id="PopupHeader">
            <div class="TitlebarLeft">
                Ajouter un Numéro de Pérmis</div>
            <div class="TitlebarRight" onclick="$get('MainContent_BtCancel').click();" >
            </div>
        </div>
        <div class="popup_Body" style="  height: 90px;">
        <asp:Table ID="Table_Ajout" runat="server" Height="85px" Width="315px">

        <asp:TableRow>
            <asp:TableCell VerticalAlign="Middle">
                Numéro de Pérmis :
                 <asp:RequiredFieldValidator ValidationGroup="validation" Display="None" 
                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="TbNumPermisAdd" 
                    ErrorMessage="champ obligatoire" ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" 
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                </asp:ValidatorCalloutExtender>
                
                <asp:RegularExpressionValidator id="RegularExpressionValidator1"
                   ControlToValidate="TbNumPermisAdd"
                   ValidationExpression="\d+"
                   Display="Static"
                   EnableClientScript="true"
                   ErrorMessage="Svp entrer juste des chiffres"
                   runat="server"/>


            </asp:TableCell>
            <asp:TableCell VerticalAlign="Top">

                <asp:TextBox ID="TbNumPermisAdd"  Width="120px" runat="server" CssClass="text"   ValidationGroup="Ajouter"></asp:TextBox>
                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="TbNumPermisAdd" ValidChars="1234567890" />
         <asp:RequiredFieldValidator ValidationGroup="validation" runat="server" ControlToValidate="TbNumPermisAdd" 
                    ErrorMessage="champ obligatoire" Display="None" ID="RequiredFieldValidator3" ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender"
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                </asp:ValidatorCalloutExtender>
         
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow  >
            <asp:TableCell ColumnSpan="2" VerticalAlign="Middle"  HorizontalAlign="Right">
                <asp:Button ID="BtSave"  CssClass="button" runat="server" Text="Enregistrer"  ValidationGroup="validation" OnClick="BtSaveAdd_Click"  />
                <asp:Button ID="BtCancel"  CssClass="button"  runat="server" Text="Annuler" OnClick="BtCancel_Click" />
            </asp:TableCell>
        </asp:TableRow>
     
         
         
        </asp:Table>
        </div>
    </div>
    </asp:Panel>
</asp:Content>

