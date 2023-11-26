<%@ page title="Chauffeurs" language="C#" masterpagefile="~/Interfaces/Shared/MasterPage.master" autoeventwireup="true" inherits="User_index, App_Web_bsanhxqr" %>
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
                <asp:CheckBox ID="CbFiltre" runat="server"  CssClass="button" AutoPostBack="true" Checked="true" Text="Afficher les filtres" OnCheckedChanged="CbFiltre_CheckedChanged" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="BtnActualiser"  runat="server" Text="Actualiser" CssClass="button" onclick="Filtre" />
            </asp:TableCell>
            
            <asp:TableCell>
              <asp:ImageButton ID="export"  ImageUrl="~/Icons/excel.png" runat="server"   CssClass="button" ToolTip="Exporter à Excel" OnClick="btnPrintFromCodeBehind_Click" />
               </asp:TableCell>
            <asp:TableCell>
                <asp:ImageButton ID="BtnAdd" ImageUrl="~/Icons/add_Chauffeur.png"  CssClass="button" runat="server" ToolTip="Ajouter" onclick="BtnAdd_Click"    />
            </asp:TableCell>
            <asp:TableCell>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  >
                <ContentTemplate>
                <asp:ImageButton ID="BtnSet" ImageUrl="~/Icons/modifier_chauffeur.png" CssClass="button" runat="server" ToolTip="Modifier"   OnClick="BtnSet_Click" Visible = "false" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridViewUsers" EventName="SelectedIndexChanging" />
                    <asp:AsyncPostBackTrigger ControlID="GridViewUsers" EventName="RowDataBound" />
                    <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
                    <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
                    
                </Triggers>
            </asp:UpdatePanel>

            </asp:TableCell>
            <asp:TableCell>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional"  >
                <ContentTemplate>
                <asp:ImageButton ID="BtnDelete" ImageUrl="~/Icons/delete_Chauffeur.png" CssClass="button" runat="server" ToolTip="Supprimer"  OnClick="BtnDelete_Click"  OnClientClick="return confirm('Etes-vous sûr que vous voulez supprimer ce chauffeur ?');" Visible = "false"  />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridViewUsers" EventName="SelectedIndexChanging" />
                    <asp:AsyncPostBackTrigger ControlID="GridViewUsers" EventName="RowDataBound" />
                    <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
                    <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
                </Triggers>
            </asp:UpdatePanel>

            </asp:TableCell>
            <asp:TableCell>
                 <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional"  >
                <ContentTemplate> 
                <asp:ImageButton ID="BtAutoriser" ImageUrl="~/Icons/autoriser_Chauffeur.png"  CssClass="button" runat="server" ToolTip="Autoriser"  OnClick="BtAutoriser_Click" Visible = "false" />
                <asp:ImageButton ID="BtInterdire" ImageUrl="~/Icons/interdire_Chauffeur.png" CssClass="button" runat="server" ToolTip="Interdire"  OnClick="BtInterdir_Click"  Visible = "false" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridViewUsers" EventName="SelectedIndexChanging" />
                    <asp:AsyncPostBackTrigger ControlID="GridViewUsers" EventName="RowDataBound" />
                    <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
                    <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
                </Triggers>
            </asp:UpdatePanel>
            
            </asp:TableCell>
            <asp:TableCell>
                 <asp:UpdatePanel ID="UpdateLecteurNowPanel" runat="server" UpdateMode="Conditional"  >
                <ContentTemplate> 
                <asp:ImageButton ID="BtnUpdateLecteurNow" ImageUrl="~/Icons/EnvoieAuLecteur.jpg"  CssClass="button" runat="server" ToolTip="Envoie aux bornes de pointages"  OnClick="BtnUpdateLecteurNowClick" OnClientClick="return confirm('Mettre à jour immédiatement ce chauffeur dans les bornes de pointages');" Visible = "false" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridViewUsers" EventName="SelectedIndexChanging" />
                    <asp:AsyncPostBackTrigger ControlID="GridViewUsers" EventName="RowDataBound" />
                    <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
                    <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
                </Triggers>
            </asp:UpdatePanel>
            
            </asp:TableCell>

            </asp:TableRow>
            </asp:Table>
         
    <asp:UpdatePanel runat="server" ID="UpdatePanel_Filtre" UpdateMode="Conditional" >
    <ContentTemplate>     
        <asp:Panel  id="Panelfilter" runat="server" Visible="true" style="width:99.4%;height:95px; margin-left:8px;">
         
            <fieldset  style="width:98%;height:65%;">

                <%--<legend>Filtres</legend>--%>

                <table style="width: 100%">
                    <tr>
                        <td>CIN</td>
                        <td>Nom</td>
                        <td>Prénom</td>
                        <td>N° Permis</td>
                       
                        
                        <td rowspan="3">
                        <asp:RadioButtonList ID="RbAutorise" runat="server" AutoPostBack="True"  >
                            <asp:ListItem Value="YN" Selected="True" >Tous</asp:ListItem>
                            <asp:ListItem Value="Y" >Autorisés</asp:ListItem>
                            <asp:ListItem Value=" ">Interdits</asp:ListItem>
                            
                        </asp:RadioButtonList>

                        </td>
                        <td rowspan="3">
                            <asp:RadioButtonList ID="RbEnroler" runat="server" AutoPostBack="True" >
                                <asp:ListItem Value="-1" Selected="True" >Tous</asp:ListItem>
                                <asp:ListItem Value="1" >Enrolés</asp:ListItem>
                                <asp:ListItem Value="0" >Non Enrolés</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td rowspan="3">
                       
                            <asp:RadioButtonList ID="RbCoder" runat="server" AutoPostBack="True" >
                                <asp:ListItem Value="-1" Selected="True" >Tous</asp:ListItem>
                                <asp:ListItem Value="1" >Codés</asp:ListItem>
                                <asp:ListItem Value="0">Non Codés</asp:ListItem>
                            </asp:RadioButtonList>
                           

                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TbMatricule" runat="server" AutoPostBack="true" CssClass="text" Width="90px"></asp:TextBox>
                             <ajaxToolkit:FilteredTextBoxExtender ID="ftbe3" runat="server" TargetControlID="TbMatricule" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890" />
                        </td>
                        <td >
                            <asp:TextBox ID="TbNom" runat="server" AutoPostBack="true"  CssClass="text" Width="90px" ></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbe2" runat="server" TargetControlID="TbNom" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                        </td>
                          <td >
                            <asp:TextBox ID="TbPrenom" runat="server" AutoPostBack="true"  CssClass="text" Width="90px" ></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TbPrenom" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                        </td>
                        <td >
                            <asp:TextBox ID="TbBadge" runat="server" AutoPostBack="true"  CssClass="text" Width="90px"  ></asp:TextBox>

                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbe1" runat="server" TargetControlID="TbBadge" ValidChars="1234567890" />
                        </td>
                        
                      
                        <td class="style1">&nbsp;
                        
                            <asp:TextBox ID="NbrLignes" runat="server" BackColor="Black" Font-Bold="True" CssClass="text"
                             ForeColor="White" Height="23px" style="margin-left: 108px ; text-align:center;" Width="59px" ReadOnly="true">
                             </asp:TextBox>&nbsp; 
                             Chauffeurs

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
        <asp:AsyncPostBackTrigger ControlID="BtAutoriser" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="BtInterdire" EventName="Click" />
    </Triggers>
    </asp:UpdatePanel>      
   

    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional" >
    <ContentTemplate>
    

    
       <div id="divPrint">
        <asp:GridView ID="GridViewUsers" 
       
        CssClass="GridViewStyle" 
        runat="server"
        AutoGenerateColumns="False"  
        ShowFooter="true"          
        ShowHeaderWhenEmpty="true"
        AllowSorting="True"
        OnSorting="Users_Sorting"                 
        
        OnRowCommand="ImgVisualiser_Click"
        onrowdeleting="GridViewUsers_RowDeleting" 
        OnPageIndexChanging="GridViewUsers_IndexChanging" 
        onrowdatabound="GridViewUsers_RowDataBound" 
        onselectedindexchanging="GridViewUsers_SelectedIndexChanging" >
         
                  
        <FooterStyle CssClass="GridViewFooterStyle" />
        <RowStyle CssClass="GridViewRowStyle" />    
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />    
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <HeaderStyle CssClass="GridViewHeaderStyle" />

    
                <Columns>
                   
                    <asp:ButtonField  Text="" CommandName="Select"  HeaderStyle-Width="0px" HeaderStyle-BackColor="White" HeaderStyle-BorderColor="White" ItemStyle-Width="0px" ItemStyle-BackColor="White"  ItemStyle-BorderColor="White" FooterStyle-BackColor="White" FooterStyle-BorderColor="White"/>
                    <asp:TemplateField  ItemStyle-HorizontalAlign="Center" ItemStyle-Height ="25px" ItemStyle-Width="2px" HeaderStyle-Width="2%" >

                   <ItemTemplate>
                     <asp:ImageButton   ID="ImgValide" ImageUrl="~/Icons/button-green.png"  CssClass="button"  ToolTip="Valide" runat="server"   CommandName="Visualiser" > </asp:ImageButton>
                      <asp:ImageButton   ID="ImgInvalide" ImageUrl="~/Icons/button-red.png"  CssClass="button"  ToolTip="Invalide" runat="server"    CommandName="Visualiser"> </asp:ImageButton>
               </ItemTemplate>
                 </asp:TemplateField>
                    <asp:TemplateField   HeaderText="CIN  " HeaderStyle-Width="8%" SortExpression="Matricule"  ItemStyle-HorizontalAlign="Center"  ControlStyle-Height="25"  >
                        <ItemTemplate>
                            <asp:Label runat="server" ID="Selected" Text='<%#bind("FlagAutorise") %>' Visible="false" ></asp:Label>
                            <asp:Label ID="LblMatricule" runat="server" Text='<%#Eval("Matricule") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField >
                    <asp:TemplateField HeaderText="Nom  "  SortExpression="Nom" HeaderStyle-Width="14%" >
                        <ItemTemplate>
                            <asp:Label ID="LblNom" runat="server"  Text='<%#bind("Nom") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Prénom  " SortExpression="Prenom" HeaderStyle-Width="15%" >
                        <ItemTemplate>
                            <asp:Label ID="LblPrenom" runat="server" Text='<%#bind("Prenom") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="N° Permis  " SortExpression="NumBadge" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="6%">
                        <ItemTemplate>
                            <asp:Label ID="LblNBadge" runat="server" Text='<%#bind("NumBadge") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>            
                    <asp:TemplateField HeaderText="Droit de pointage  " HeaderStyle-Width="16%"  ItemStyle-HorizontalAlign="Center" SortExpression="DroitAcces">
                        <ItemTemplate>
                            <asp:Label ID="LblDroitAccés" runat="server" Text='<%#bind("LibelleDroit") %>'></asp:Label>
                        </ItemTemplate>
                        
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date Début  " HeaderStyle-Width="12%"  FooterStyle-HorizontalAlign="Right" SortExpression="DateDebut" ItemStyle-HorizontalAlign="Center" >
                       
                        <ItemTemplate>
                            <asp:Label ID="LblDateDebut" runat="server" Text='<%#bind("DateDebut") %>'></asp:Label>
                        </ItemTemplate>

                         <FooterTemplate >

                         <asp:Button id="BtnFirst" runat="server" Text="&#9664;&#9664;"  CssClass="text" ToolTip="Première Page" OnClick="BtnFirst_Click" />  
                         <asp:Button id="BtnPrevious" runat="server" Text="&#9664;" CssClass="text" ToolTip="Page Précédente" OnClick="BtnPrevious_Click" />
                         
                        </FooterTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date Fin  " HeaderStyle-Width="12%" FooterStyle-HorizontalAlign="Left" SortExpression="DateFin" ItemStyle-HorizontalAlign="Center" >
                        
                        <ItemTemplate>
                            <asp:Label ID="LblDateFin" runat="server" Text='<%#bind("DateFin") %>'></asp:Label>
                        </ItemTemplate>

                        <FooterTemplate >
                       
                          <asp:Button id="BtnNext" runat="server" Text="&#9654;"  CssClass="text" ToolTip="Page Suivante" OnClick="BtnNext_Click"  />    
                          <asp:Button id="BtnLast" runat="server" Text="&#9654;&#9654;"  CssClass="text" ToolTip="Dernière Page" OnClick="BtnLast_Click" />
                             
                      </FooterTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Téléphone  " HeaderStyle-Width="7%" SortExpression="Telephone"  FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="LblTelephone" runat="server" Text='<%#bind("Telephone") %>'></asp:Label>
                        </ItemTemplate>
<%--                        <FooterTemplate >
                        <asp:Label id="LbelIndexPage" runat="server" Text="Page:"    Enabled="false" />                      
                        </FooterTemplate>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ville  " HeaderStyle-Width="12%" SortExpression="Ville" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label ID="LblVille" runat="server" Text='<%#bind("Ville") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>

                  
                    
                 
                  
                
                </Columns>
            </asp:GridView>

  </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
        <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
        <asp:AsyncPostBackTrigger ControlID="BtAutoriser"  />  
        <asp:AsyncPostBackTrigger ControlID="BtInterdire"  />      
        <asp:AsyncPostBackTrigger ControlID="GridViewUsers"  />    
    </Triggers>
    </asp:UpdatePanel>


</asp:Content>

