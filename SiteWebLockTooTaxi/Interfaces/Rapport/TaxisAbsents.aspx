<%@ Page Title="Rapport Taxis Absents (Période continue)"  Language="C#" MasterPageFile="~/Interfaces/Shared/MasterPage.master" AutoEventWireup="true" CodeFile="TaxisAbsents.aspx.cs" Inherits="Interfaces_Rapport_TaxisAbsents" %>
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
                <asp:CheckBox ID="CbFiltre" runat="server" CssClass="button" AutoPostBack="true" Checked="True" Text="Afficher les filtres" OnCheckedChanged="CbFiltre_CheckedChanged" />
            </asp:TableCell>
            <asp:TableCell>
             <asp:Button ID="Filtre" runat="server"  CssClass="button" Text="Actualiser" OnClick="Filtre_Click" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:ImageButton ID="export"   CssClass="button" ImageUrl="~/Icons/excel.png" runat="server"   ToolTip="Exporter à Excel"  onclick="exportExcel_Click" />        
            </asp:TableCell>
            
            </asp:TableRow>
        </asp:Table>
  
 

<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:Panel  id="Panelfilter" runat="server" Visible="true" style="width:99.4%;height:95px; margin-left:8px;">
        <fieldset id="Fieldset_Filter" runat="server" style="width:98%;height:65%;">
                       <table style="width: 1174px">
                    <tr>
                        <td>Date Début</td>
                        <td>Date Fin</td>
                        
                        <td>Type Taxi</td>
                        <td>Agrément</td>
                        <td>Plaque</td>
                    </tr>
                    <tr>
                        <td>  
                                   
                       <telerik:RadDateTimePicker  ID="TbDateDebut" Runat="server" ShowPopupOnFocus="true"  CssClass="text" 
                         Culture="fr-FR"  AutoPostBackControl="TimeView" >
                    <TimeView ID="TimeView1" runat="server" CellSpacing="-1" Culture="fr-FR"></TimeView>

                    <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>

                    <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                    <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="" 
                                        ></DateInput>

                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDateTimePicker>

                        </td> 
                        <td>  
                                            
                       <telerik:RadDateTimePicker  ID="TbDateFin" Runat="server" ShowPopupOnFocus="true"   CssClass="text" 
                   Culture="fr-FR"  AutoPostBackControl="TimeView" >
                    <TimeView ID="TimeView2" runat="server" CellSpacing="-1" Culture="fr-FR"></TimeView>

                    <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>

                    <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                    <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="" 
                                        ></DateInput>

                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDateTimePicker>
                       
                        </td>  
                        
                        <td>
                        <asp:DropDownList runat="server" id="DDLGroupe" Width="90px"  CssClass="text"  ></asp:DropDownList>
                        </td> 
                        <td>
                        <asp:TextBox runat="server" id="TbAgrement" Width="90px"  CssClass="text" ></asp:TextBox>
                          <ajaxToolkit:FilteredTextBoxExtender ID="ftbe1" runat="server" TargetControlID="TbAgrement" ValidChars="1234567890" />
                        </td>    
                        <td>
                        <asp:TextBox runat="server" id="TbPlaque" Width="90px"  CssClass="text" ></asp:TextBox>
                          <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TbPlaque" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890-" />
                        </td>    
                        <td class="style1">&nbsp;
                          <asp:TextBox ID="TbNbrLignes" runat="server"  AutoPostBack="true" Width="59px" BackColor="Black" Font-Bold="True" ForeColor="White"  CssClass="text" ReadOnly="true" ></asp:TextBox>  TAXIS       
                        </td>
                      </tr>
                       </table>
        </fieldset>
           </asp:Panel>
    </ContentTemplate>
    <Triggers>
     <asp:AsyncPostBackTrigger ControlID="CbFiltre" />
      <asp:AsyncPostBackTrigger ControlID="Filtre" />
     
    </Triggers>
</asp:UpdatePanel>


<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

     


        <asp:GridView ID="GridviewAbsentsPeriodeContinue" 
        runat="server" 
        CssClass="GridViewStyle"
        AutoGenerateColumns="false"
        ShowFooter="false"
        AllowPaging="true"
        AllowSorting = "true"
        OnSorting="Taxi_Sorting"
        PageSize="15"
        ShowHeaderWhenEmpty="true"
        onpageindexchanging="GridviewAbsentsJours_PageIndexChanging" >


        <FooterStyle CssClass="GridViewFooterStyle" />
        <RowStyle CssClass="GridViewRowStyle" />    
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <HeaderStyle CssClass="GridViewHeaderStyle" />


           
            <Columns>
                <asp:TemplateField HeaderText="Agrément  "  SortExpression="NumAgrement" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%" >
                    <ItemTemplate>
                        <asp:Label  ID="LblAgrement" runat="server" Text='<%#bind("NumAgrement") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Propriétaire  "  SortExpression="Nom"  HeaderStyle-Width="30%">
                    <ItemTemplate>
                        <asp:Label ID="LblProprietaire" runat="server" Text='<%# bind("Nom") %>'></asp:Label>
                        <asp:Label ID="Label2" runat="server" Text="  "></asp:Label>
                        <asp:Label ID="Label1" runat="server" Text='<%# bind("Prenom") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type Taxi  " SortExpression="TypeTaxi" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                    <ItemTemplate>
                        <asp:Label ID="LblTypeTaxi" runat="server" Text='<%#bind("TypeTaxi") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="PLaque  " SortExpression="Plaque" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20%">
                    <ItemTemplate>
                        <asp:Label ID="LblPlaque" runat="server" Text='<%#bind("Plaque") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Commune  " SortExpression="Commune" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20%">
                    <ItemTemplate>
                        <asp:Label ID="LblCommune" runat="server" Text='<%#bind("Commune") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>


    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Filtre" />
    </Triggers>
</asp:UpdatePanel>

</asp:Content>

