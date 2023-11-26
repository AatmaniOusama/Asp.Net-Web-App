<%@ page title="LoginsOperateurs" language="C#" masterpagefile="~/Interfaces/Shared/MasterPage.master" autoeventwireup="true" inherits="Interfaces_Operateur_LoginsOperateur, App_Web_eei3chx1" %>
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

            </asp:TableRow>
            </asp:Table>
      
    
    <asp:UpdatePanel runat="server" ID="UpdatePanel_Filtre" UpdateMode="Conditional" >
    <ContentTemplate>     
        <asp:Panel  id="Panelfilter" runat="server" Visible="true" style="width:99.4%;height:95px; margin-left:8px;">
         
            <fieldset  style="width:98%;height:65%;">

                <%--<legend>Filtres</legend>--%>

                <table style="width: 1174px">
                    <tr>
                        <td>Début</td>
                        <td>Fin</td>
                        <td>Nom</td>
                        <td>Prénom</td>
                        <td>Droit de pointage</td>

                    </tr>
                    <tr>

                        <td>           
                    <telerik:RadDateTimePicker  ID="TbDateDebut" Runat="server" ShowPopupOnFocus="true"  CssClass="text" 
                         Culture="fr-FR" AutoPostBackControl="TimeView" >
                        <TimeView ID="TimeView1" runat="server" CellSpacing="-1" Culture="fr-FR"></TimeView>

                        <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>

                        <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth=""></DateInput>

                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    </telerik:RadDateTimePicker>
                    </td>
                        <td>      
                     <telerik:RadDateTimePicker  ID="TbDateFin" Runat="server" ShowPopupOnFocus="true"  CssClass="text" 
                        Culture="fr-FR"  AutoPostBackControl="TimeView" >
                            <TimeView ID="TimeView2" runat="server" CellSpacing="-1" Culture="fr-FR"></TimeView>

                            <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>

                            <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                            <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="" 
                                                AutoPostBack="True"></DateInput>

                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    </telerik:RadDateTimePicker>
                    </td>
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
                        
                      
                     
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="CbFiltre" EventName="CheckedChanged" />
        <asp:AsyncPostBackTrigger ControlID="BtnActualiser" EventName="Click" />
    </Triggers>
    </asp:UpdatePanel>      
   

    



    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional" >
    <ContentTemplate>
    

 
        <asp:GridView ID="GridViewLoginsOperateurs" 
        CssClass="GridViewStyle" 
        runat="server"
        AutoGenerateColumns="False"  
        ShowFooter="true"   
       
        ShowHeaderWhenEmpty="true"
        AllowSorting="false"
       
       
         
        
       
        OnPageIndexChanging="GridViewLoginsOperateurs_IndexChanging" 
        onrowdatabound="GridViewLoginsOperateurs_RowDataBound" 
        onselectedindexchanging="GridViewLoginsOperateurs_SelectedIndexChanging">
         
   
         
        <FooterStyle CssClass="GridViewFooterStyle" />
        <RowStyle CssClass="GridViewRowStyle" />    
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />

    
    
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <HeaderStyle CssClass="GridViewHeaderStyle" />

    
                <Columns>
                   
                    <asp:TemplateField HeaderText="Instant  " HeaderStyle-Width="16.6%" SortExpression="Instant " ItemStyle-HorizontalAlign="Center" >
                       
                        <ItemTemplate>
                            <asp:Label ID="LblInstant" runat="server" Text='<%#bind("Instant") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type  " HeaderStyle-Width="16.6%"  SortExpression="Type " ItemStyle-HorizontalAlign="Center" >
                        
                        <ItemTemplate>
                            <asp:Label ID="LblType" runat="server" Text='<%#bind("Type") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                               
              
                    <asp:TemplateField   HeaderText="Nom  " HeaderStyle-Width="16.6%" SortExpression="Nom" FooterStyle-HorizontalAlign="Right"  ItemStyle-HorizontalAlign="Center"  ControlStyle-Height="25"  >
                        <ItemTemplate>
                           <asp:Label ID="LblNom" runat="server" Text='<%#Eval("Nom") %>'></asp:Label>
                        </ItemTemplate>
                        
                         <FooterTemplate >

                         <asp:Button id="BtnFirst" runat="server" Text="&#9664;&#9664;"  CssClass="text" ToolTip="Première Page" OnClick="BtnFirst_Click" />  
                         <asp:Button id="BtnPrevious" runat="server" Text="&#9664;" CssClass="text" ToolTip="Page Précédente" OnClick="BtnPrevious_Click" />
                         
                        </FooterTemplate>
                    </asp:TemplateField >
                    <asp:TemplateField HeaderText="Prénom  "  SortExpression="Prenom"  FooterStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="16.6%" >
                        <ItemTemplate>
                            <asp:Label ID="LblPrenom" runat="server"  Text='<%#bind("Prenom") %>'></asp:Label>
                        </ItemTemplate>

                         <FooterTemplate >
                       
                          <asp:Button id="BtnNext" runat="server" Text="&#9654;"  CssClass="text" ToolTip="Page Suivante" OnClick="BtnNext_Click"  />    
                          <asp:Button id="BtnLast" runat="server" Text="&#9654;&#9654;"  CssClass="text" ToolTip="Dernière Page" OnClick="BtnLast_Click" />
                             
                      </FooterTemplate>

                    </asp:TemplateField>
                    
                    <asp:TemplateField  HeaderText="Droit de Pointage  " SortExpression="Profil"   ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="16.6%">
                        <ItemTemplate>
                            <asp:Label ID="LblProfil" runat="server" Text='<%#bind("Profil") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                                                   
                    
                
                
                </Columns>
            </asp:GridView>

    
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
        <asp:AsyncPostBackTrigger ControlID="GridViewLoginsOperateurs"  />    
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>


