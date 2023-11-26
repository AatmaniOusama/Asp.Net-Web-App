<%@ page title="Prem et Dern Pointages" language="C#" masterpagefile="~/Interfaces/Shared/MasterPage.master" autoeventwireup="true" inherits="Interfaces_Rapport_IdentificationsPeriode, App_Web_i5melif2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 
        <asp:Table ID="BarreControle" runat="server" CellPadding="5">
            <asp:TableRow>
            <asp:TableCell>
                <asp:CheckBox ID="CbFiltre" runat="server" CssClass="button" AutoPostBack="true" Checked="True" Text="Afficher les filtres" OnCheckedChanged="CbFiltre_CheckedChanged" />
            
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="Filtre" runat="server"  CssClass="button" Text="Actualiser" OnClick="Filtre_Click" />             
            </asp:TableCell>
            <asp:TableCell>
               <asp:ImageButton ID="export"  ImageUrl="~/Icons/excel.png" runat="server"  CssClass="button"   ToolTip="Exporter à Excel"  onclick="exportExcel_Click" />
            </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
   

<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
       <asp:Panel  id="Panelfilter" runat="server" Visible="true" style="width:99.4%;height:130px; margin-left:8px;">
        <fieldset id="Fieldset_Filter" runat="server" Visible="true" style="width:98%;height:65%;">
            <asp:Table ID="Table_Filter" runat="server" style="width: 1174px" >
                <asp:TableRow>
                <asp:TableCell>
                    Début<br />
                <telerik:RadDateTimePicker  ID="TbDateDebut" Runat="server" ShowPopupOnFocus="true"  CssClass="text" 
                    AutoPostBack="True" Culture="fr-FR" AutoPostBackControl="TimeView" >
                    <TimeView  runat="server" CellSpacing="-1" Culture="fr-FR"></TimeView>

                    <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>

                    <Calendar  runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                    <DateInput runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth=""></DateInput>

                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                </telerik:RadDateTimePicker>

                   <br /> Fin<br />
            <telerik:RadDateTimePicker  ID="TbDateFin" Runat="server" ShowPopupOnFocus="true"  CssClass="text" 
                Culture="fr-FR" AutoPostBack="True" AutoPostBackControl="TimeView" >
                    <TimeView runat="server" CellSpacing="-1" Culture="fr-FR"></TimeView>

                    <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>

                    <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                    <DateInput runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="" 
                                        AutoPostBack="True"></DateInput>

                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDateTimePicker>

                </asp:TableCell>
                 <asp:TableCell>
                    N° Permis <br />
                    <asp:TextBox ID="TbNumPermis" runat="server" Width="100px" CssClass="text"   ></asp:TextBox>
                 </asp:TableCell>

                 <asp:TableCell>
                    CIN <br />
                    <asp:TextBox ID="TbMatricule" runat="server" Width="100px" CssClass="text"   ></asp:TextBox>
                 </asp:TableCell>
                  
                <asp:TableCell>
                    Nom<br />
                    <asp:TextBox ID="TbNom" runat="server" Width="100px" CssClass="text"    ></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell>
                    Prénom<br />
                    <asp:TextBox ID="TbPrenom" runat="server" Width="100px" CssClass="text" ></asp:TextBox>
                </asp:TableCell>
               
                       
                <asp:TableCell>
                     <asp:RadioButtonList ID="RbIdentification" runat="server" CellPadding="10"  CssClass="text" >
                        <asp:ListItem Text=" Avec pointages" Selected="True" Value="true"></asp:ListItem>
                        <asp:ListItem Text=" Sans pointage" Value="false"></asp:ListItem>
                     </asp:RadioButtonList>
                </asp:TableCell>

                <asp:TableCell>
                <asp:TextBox ID="TbNbrLignes" runat="server" Width="59px" BackColor="Black"  CssClass="text" 
                    Font-Bold="True" ForeColor="White"></asp:TextBox>&nbsp;Chauffeurs          
            </asp:TableCell>

                </asp:TableRow>


            </asp:Table>
        </fieldset>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
     <asp:AsyncPostBackTrigger ControlID="CbFiltre" />
      <asp:AsyncPostBackTrigger ControlID="Filtre" EventName="Click"  />
    </Triggers>
</asp:UpdatePanel>





<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
    <ContentTemplate>



        <asp:GridView ID="GridviewIdentification" 
        runat="server"  
        CssClass="GridViewStyle"
        AutoGenerateColumns="false"
        AllowPaging="true" 
        AllowSorting="true"
        OnSorting="user_Sorting"
        PageSize="15" 
        ShowFooter="false"
      ShowHeaderWhenEmpty="true"

        onpageindexchanging="GridviewIdentification_PageIndexChanging"
         onrowdatabound="GridViewUsers_RowDataBound">

        <FooterStyle CssClass="GridViewFooterStyle" />
        <RowStyle CssClass="GridViewRowStyle" />    
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <HeaderStyle CssClass="GridViewHeaderStyle" />

       
            <Columns>  
            <asp:TemplateField HeaderText="N° Permis  "  SortExpression="NumPermis" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                    <ItemTemplate>
                        <asp:Label ID="LblNumPermis" runat="server" Text='<%#bind("NumBadge") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="CIN  "  SortExpression="Matricule" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                    <ItemTemplate>
                        <asp:Label ID="LblMatricule" runat="server" Text='<%#bind("Matricule") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nom  "  SortExpression="Nom"  HeaderStyle-Width="20%">
                    <ItemTemplate>
                        <asp:Label ID="LblNom" runat="server" Text='<%#bind("Nom") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Prénom  "  SortExpression="Prenom"  HeaderStyle-Width="15%">
                    <ItemTemplate>
                        <asp:Label ID="LblPrenom" runat="server" Text='<%#bind("Prenom") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Premier Pointage  " SortExpression="DateDebut" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20%">
                    <ItemTemplate>
                        <asp:Label   ID="LblDateDebut" runat="server" Text='<%#bind("DateDebut") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DernierPointage  " SortExpression="DateFin" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20%">
                    <ItemTemplate>
                        <asp:Label  ID="LblDateFin" runat="server" Text='<%#bind("DateFin") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
<%--
         </div>

    <div id="DivFooterRow" style="overflow:hidden">
    </div>
    </div>--%>

    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Filtre" />
    </Triggers>
</asp:UpdatePanel>

</asp:Content>

