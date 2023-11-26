<%@ page title="Operateurs" language="C#" masterpagefile="~/Interfaces/Shared/MasterPage.master" autoeventwireup="true" inherits="Interfaces_Operateur, App_Web_eei3chx1" %>
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
              <asp:ImageButton ID="export"  ImageUrl="~/Icons/excel.png" runat="server"   CssClass="button" ToolTip="Exporter à Excel"  onclick="exportExcel_Click" />
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
                    <asp:AsyncPostBackTrigger ControlID="GridViewOperateurs" EventName="SelectedIndexChanging" />
                    <asp:AsyncPostBackTrigger ControlID="GridViewOperateurs" EventName="RowDataBound" />
                    <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
                    <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
                    
                </Triggers>
            </asp:UpdatePanel>

            </asp:TableCell>
            <asp:TableCell>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional"  >
                <ContentTemplate>
                <asp:ImageButton ID="BtnDelete" ImageUrl="~/Icons/delete_Chauffeur.png" CssClass="button" runat="server" ToolTip="Supprimer"  OnClick="BtnDelete_Click"  OnClientClick="return confirm('Etes-vous sûr que vous voulez supprimer cet opérateur ?');" Visible = "false"  />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridViewOperateurs" EventName="SelectedIndexChanging" />
                    <asp:AsyncPostBackTrigger ControlID="GridViewOperateurs" EventName="RowDataBound" />
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

                <table style="width: 1174px">
                    <tr>
                        <td>Nom</td>
                        <td>Prénom</td>
                        <td>Droit de pointage</td>
                        <td>Groupe</td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TbNom" runat="server" AutoPostBack="true" CssClass="text" Width="90px"></asp:TextBox>
                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TbNom" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890" />
                        </td>
                        <td >
                            <asp:TextBox ID="TbPrenom" runat="server" AutoPostBack="true"  CssClass="text" Width="90px" ></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TbPrenom" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                        </td>
                       <td>
                            <asp:DropDownList ID="DDLDroitPointage" runat="server" AutoPostBack="True"  Width="150px" CssClass="text" >
                                 <asp:ListItem  Text="Tous" Value="0" Selected="True"></asp:ListItem>
                                 <asp:ListItem  Text="Administrateur" Value="1" ></asp:ListItem>
                                 <asp:ListItem  Text="Superviseur" Value="2"></asp:ListItem>
                                 <asp:ListItem  Text="Consultant" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="DDLService" runat="server" AutoPostBack="True"  Width="150px" CssClass="text" >
                                <asp:ListItem Selected="True" Value="0">Tous</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                      
                        <td class="style1">&nbsp;
                        
                            <asp:TextBox ID="NbrLignes" runat="server" BackColor="Black" Font-Bold="True" CssClass="text"
                             ForeColor="White" Height="23px" style="margin-left: 108px" Width="59px" ReadOnly="true">
                             </asp:TextBox>&nbsp; 
                             Opérateurs

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
    </Triggers>
    </asp:UpdatePanel>      
   

    



    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional" >
    <ContentTemplate>
    

 
        <asp:GridView ID="GridViewOperateurs" 
        CssClass="GridViewStyle" 
        runat="server"
        AutoGenerateColumns="False"  
        ShowFooter="true"   
       
        ShowHeaderWhenEmpty="true"
        AllowSorting="True"
        OnSorting="Operateurs_Sorting" 
       
         
        
        OnRowCommand="ImgVisualiser_Click"
        onrowdeleting="GridViewOperateurs_RowDeleting" 
        OnPageIndexChanging="GridViewOperateurs_IndexChanging" 
        onrowdatabound="GridViewOperateurs_RowDataBound" 
        onselectedindexchanging="GridViewOperateurs_SelectedIndexChanging">
         
   
         
        <FooterStyle CssClass="GridViewFooterStyle" />
        <RowStyle CssClass="GridViewRowStyle" />    
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />

    
    
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <HeaderStyle CssClass="GridViewHeaderStyle" />

    
                <Columns>
                   
                    <asp:ButtonField  Text="" CommandName="Select"  HeaderStyle-Width="0px" HeaderStyle-BackColor="White" HeaderStyle-BorderColor="White" ItemStyle-Width="0px" ItemStyle-BackColor="White"  ItemStyle-BorderColor="White" FooterStyle-BackColor="White" FooterStyle-BorderColor="White"/>
              
                    <asp:TemplateField   HeaderText="Nom  " HeaderStyle-Width="16.6%" SortExpression="Nom"  ItemStyle-HorizontalAlign="Center"  ControlStyle-Height="25"  >
                        <ItemTemplate>
                             <asp:Label runat="server" ID="Selected"  Text='<%#bind("ID") %>' Visible="false" ></asp:Label>
                            <asp:Label ID="LblNom" runat="server" Text='<%#Eval("Nom") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField >
                    <asp:TemplateField HeaderText="Prénom  "  SortExpression="Prenom" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="16.6%" >
                        <ItemTemplate>
                            <asp:Label ID="LblPrenom" runat="server"  Text='<%#bind("Prenom") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField  HeaderText="Droit de Pointage  " SortExpression="Profil"  FooterStyle-HorizontalAlign="Right"  ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="16.6%">
                        <ItemTemplate>
                            <asp:Label ID="LblProfil" runat="server" Text='<%#bind("Profil") %>'></asp:Label>
                        </ItemTemplate>

                         <FooterTemplate >

                         <asp:Button id="BtnFirst" runat="server" Text="&#9664;&#9664;"  CssClass="text" ToolTip="Première Page" OnClick="BtnFirst_Click" />  
                         <asp:Button id="BtnPrevious" runat="server" Text="&#9664;" CssClass="text" ToolTip="Page Précédente" OnClick="BtnPrevious_Click" />
                         
                        </FooterTemplate>
                    </asp:TemplateField>
                                                   
                    <asp:TemplateField HeaderText="Groupe  " HeaderStyle-Width="16.6%"  FooterStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="LblNomService" runat="server" Text='<%#bind("NomService") %>'></asp:Label>
                        </ItemTemplate>
                        
                        <FooterTemplate >
                       
                          <asp:Button id="BtnNext" runat="server" Text="&#9654;"  CssClass="text" ToolTip="Page Suivante" OnClick="BtnNext_Click"  />    
                          <asp:Button id="BtnLast" runat="server" Text="&#9654;&#9654;"  CssClass="text" ToolTip="Dernière Page" OnClick="BtnLast_Click" />
                             
                      </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date Début  " HeaderStyle-Width="16.6%" SortExpression="Debut" ItemStyle-HorizontalAlign="Center" >
                       
                        <ItemTemplate>
                            <asp:Label ID="LblDateDebut" runat="server" Text='<%#bind("Debut") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date Fin  " HeaderStyle-Width="16.6%"  SortExpression="Fin" ItemStyle-HorizontalAlign="Center" >
                        
                        <ItemTemplate>
                            <asp:Label ID="LblDateFin" runat="server" Text='<%#bind("Fin") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                               
                
                </Columns>
            </asp:GridView>

    
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
        <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
        <asp:AsyncPostBackTrigger ControlID="GridViewOperateurs"  />    
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>


