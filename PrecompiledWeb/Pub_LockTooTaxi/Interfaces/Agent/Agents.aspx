<%@ page title="Agents" language="C#" masterpagefile="~/Interfaces/Shared/MasterPage.master" autoeventwireup="true" inherits="Agent_index, App_Web_cbzejwgp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
                <asp:CheckBox ID="CbFiltre" runat="server" CssClass="button" AutoPostBack="true" Checked="true" Text="Afficher les filtres" OnCheckedChanged="CbFiltre_CheckedChanged" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="BtnActualiser"  runat="server" CssClass="button" Text="Actualiser" onclick="Filtre" />
            </asp:TableCell>
            <asp:TableCell>
              <asp:ImageButton ID="export"  ImageUrl="~/Icons/excel.png" runat="server"  CssClass="button"  ToolTip="Exporter à Excel"  onclick="exportExcel_Click" />
               </asp:TableCell>
            <asp:TableCell>
                <asp:ImageButton ID="BtnAdd" ImageUrl="~/Icons/add_Agent.png" runat="server" CssClass="button" ToolTip="Ajouter" onclick="BtnAdd_Click"    />
            </asp:TableCell>
            <asp:TableCell>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  >
                <ContentTemplate>
                <asp:ImageButton ID="BtnSet" ImageUrl="~/Icons/modifier_Agent.png" runat="server"  CssClass="button" ToolTip="Modifier"   OnClick="BtnSet_Click" Visible = "false" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridViewAgents" EventName="SelectedIndexChanging" />
                    <asp:AsyncPostBackTrigger ControlID="GridViewAgents" EventName="RowDataBound" />
                    <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
                    <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
                    
                </Triggers>
            </asp:UpdatePanel>

            </asp:TableCell>
            <asp:TableCell>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional"  >
                <ContentTemplate>
                <asp:ImageButton ID="BtnDelete" ImageUrl="~/Icons/delete_Agent.png" runat="server" CssClass="button" ToolTip="Supprimer"  OnClick="BtnDelete_Click"  OnClientClick="return confirm('Etes-vous sûr que vous voulez supprimer cet agent ?');" Visible = "false"  />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridViewAgents" EventName="SelectedIndexChanging" />
                    <asp:AsyncPostBackTrigger ControlID="GridViewAgents" EventName="RowDataBound" />
                    <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
                     <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
                </Triggers>
            </asp:UpdatePanel>

            </asp:TableCell>
            <asp:TableCell>
                 <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional"  >
                <ContentTemplate> 
                <asp:ImageButton ID="BtAutoriser" ImageUrl="~/Icons/autoriser_Agent.png" runat="server" CssClass="button" ToolTip="Autoriser"  OnClick="BtAutoriser_Click" Visible = "false" />
                <asp:ImageButton ID="BtInterdire" ImageUrl="~/Icons/Interdire_Agent.png" runat="server" CssClass="button" ToolTip="Interdire"  OnClick="BtInterdir_Click"  Visible = "false" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridViewAgents" EventName="SelectedIndexChanging" />
                    <asp:AsyncPostBackTrigger ControlID="GridViewAgents" EventName="RowDataBound" />
                    <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
                    <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
                </Triggers>
            </asp:UpdatePanel>
            
            </asp:TableCell>
            <asp:TableCell>
                 <asp:UpdatePanel ID="UpdateLecteurNowPanel" runat="server" UpdateMode="Conditional"  >
                <ContentTemplate> 
                <asp:ImageButton ID="BtnUpdateLecteurNow" ImageUrl="~/Icons/EnvoieAuLecteur.jpg" runat="server" CssClass="button" ToolTip="Envoie aux bornes de pointages"  OnClick="BtnUpdateLecteurNowClick" OnClientClick="return confirm('Mettre à jour immédiatement ce chauffeur dans les bornes de pointages');" Visible = "false" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridViewAgents" EventName="SelectedIndexChanging" />
                    <asp:AsyncPostBackTrigger ControlID="GridViewAgents" EventName="RowDataBound" />
                    <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
                    <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
                </Triggers>
            </asp:UpdatePanel>
            
            </asp:TableCell>

            </asp:TableRow>
            </asp:Table>
      
    
    <asp:UpdatePanel runat="server" ID="UpdatePanel_Filtre" UpdateMode="Conditional" >
    <ContentTemplate>     
        <asp:Panel  id="Panelfilter" runat="server" Visible="true" style="width:100%;height:95px;">
            <asp:Literal ID="Literal_MsgBox" runat="server"></asp:Literal>
            <fieldset  style="width:98%;height:65%;">

                <%--<legend>Filtres</legend>--%>

                <table style="width: 1174px">
                    <tr>
                        <td>Matricule</td>
                        <td>Nom</td>
                      
                        
                        
                        <td rowspan="3">
                        <asp:RadioButtonList ID="RbAutorise" runat="server" AutoPostBack="True" >
                            <asp:ListItem Value="YN" Selected="True">Tous</asp:ListItem>
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
                        
                        <td class="style1">

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TbMatricule" runat="server" CssClass="text" AutoPostBack="true"  Width="90px"></asp:TextBox>
                        </td>
                        <td >
                            <asp:TextBox ID="TbNom" runat="server" CssClass="text"  AutoPostBack="true" Width="90px" ></asp:TextBox>
                        </td>
                        

                        
                      
                        <td class="style1">&nbsp;
                        
                            <asp:TextBox ID="NbrLignes" runat="server" BackColor="Black" Font-Bold="True" CssClass="text"  
                             ForeColor="White" Height="23px" style="margin-left: 108px" Width="59px" ReadOnly="true">
                             </asp:TextBox>&nbsp; 
                             Agents

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
    



        <asp:GridView ID="GridViewAgents" 
        CssClass="GridViewStyle" 
        runat="server"
        AutoGenerateColumns="False"  
        ShowFooter="true"   
       
        ShowHeaderWhenEmpty="true"
        AllowSorting="True"
        OnSorting="agents_Sorting" 
       
         
        
        OnRowCommand="ImgVisualiser_Click"
        onrowdeleting="GridViewAgents_RowDeleting" 
        OnPageIndexChanging="GridViewAgents_IndexChanging" 
        onrowdatabound="GridViewAgents_RowDataBound" 
        onselectedindexchanging="GridViewAgents_SelectedIndexChanging">
         
   
         
        <FooterStyle CssClass="GridViewFooterStyle" />
        <RowStyle CssClass="GridViewRowStyle" />    
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />

     
    
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <HeaderStyle CssClass="GridViewHeaderStyle" />

    
                <Columns>
                   
             <asp:ButtonField  Text="" CommandName="Select"  HeaderStyle-Width="0px" HeaderStyle-BackColor="White" HeaderStyle-BorderColor="White" ItemStyle-Width="0px" ItemStyle-BackColor="White" ItemStyle-BorderColor="White" />
              
            <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="2%"  >

                     <ItemTemplate>
                     <asp:ImageButton   ID="ImgValide" ImageUrl="~/Icons/button-green.png"  ToolTip="Valide" runat="server"  CommandName="Visualiser" > </asp:ImageButton>
                      <asp:ImageButton   ID="ImgInvalide" ImageUrl="~/Icons/button-red.png"  ToolTip="Invalide" runat="server" CommandName="Visualiser" > </asp:ImageButton>
               </ItemTemplate>
              
                 </asp:TemplateField>
            <asp:TemplateField   HeaderText="Matricule  " HeaderStyle-Width="18%" SortExpression="Matricule"  ControlStyle-Height="25" ItemStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                    <asp:Label runat="server" ID="Selected" Text='<%#bind("FlagAutorise") %>' Visible="false" ></asp:Label>
                    <asp:Label ID="LblMatricule" runat="server" Text='<%#Eval("Matricule") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField >
            <asp:TemplateField HeaderText="Nom  "  SortExpression="Nom" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                    <asp:Label ID="LblNom" runat="server"  Text='<%#bind("Nom") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Prénom  "  FooterStyle-HorizontalAlign="Right" SortExpression="Prenom" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="LblPrenom" runat="server" Text='<%#bind("Prenom") %>'></asp:Label>
                </ItemTemplate>
                     
                <FooterTemplate >
                 <asp:Button id="BtnFirst" runat="server" Text="&#9664;&#9664;"  CssClass="text" ToolTip="Première Page" OnClick="BtnFirst_Click" />  
                 <asp:Button id="BtnPrevious" runat="server" Text="&#9664;" CssClass="text" ToolTip="Page Précédente" OnClick="BtnPrevious_Click" />
                                
                </FooterTemplate>
            </asp:TemplateField>                
            <asp:TemplateField HeaderText="Date Début  " FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" SortExpression="DateDebut" ItemStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                    <asp:Label ID="LblDateDebut" runat="server" Text='<%#bind("DateDebut") %>'></asp:Label>
                </ItemTemplate>
                    <FooterTemplate >
                 <asp:Button id="BtnNext" runat="server" Text="&#9654;"  CssClass="text"  ToolTip="Page Suivante" OnClick="BtnNext_Click"  />  
                 <asp:Button id="BtnLast" runat="server" Text="&#9654;&#9654;"  CssClass="text"   ToolTip="Dernière Page" OnClick="BtnLast_Click" />         
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date Fin  " HeaderStyle-Width="20%" SortExpression="DateFin" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                    <asp:Label ID="LblDateFin" runat="server" Text='<%#bind("DateFin") %>'></asp:Label>
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
        <asp:AsyncPostBackTrigger ControlID="GridViewAgents"  />    
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>

