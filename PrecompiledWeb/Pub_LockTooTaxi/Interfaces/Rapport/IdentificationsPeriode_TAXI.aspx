<%@ page title="Prem et Dern Pointages" language="C#" masterpagefile="~/Interfaces/Shared/MasterPage.master" autoeventwireup="true" inherits="Interfaces_Rapport_IdentificationsPeriode, App_Web_il33h2sg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 
        <asp:Table ID="BarreControle" runat="server" CellPadding="5">
            <asp:TableRow>
            <asp:TableCell>
                <asp:CheckBox ID="CbFiltre" runat="server" CssClass="button"  AutoPostBack="true" Checked="True" Text="Afficher les filtres" OnCheckedChanged="CbFiltre_CheckedChanged" />
            
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="Filtre" runat="server" CssClass="button"  Text="Actualiser"   AutoPostBack="true" OnClick="Filtre_Click" />
               
            </asp:TableCell>
            
            <asp:TableCell>
             <asp:ImageButton ID="export"  ImageUrl="~/Icons/excel.png" runat="server"   CssClass="button"  ToolTip="Exporter à Excel"  onclick="exportExcel_Click" />
            </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
  
<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
     <asp:Panel  id="Panelfilter" runat="server" Visible="true" style="width:99.4%;height:130px; margin-left:8px;">
   <fieldset id="Fieldset_Filter" runat="server" Visible="true" style="width:98%;height:65%;">
            <asp:Table ID="Table_Filter" runat="server" style="width: 1174px" >
                <asp:TableRow>
                <asp:TableCell>
                    Début<br />
                <telerik:RadDateTimePicker  ID="TbDateDebut" Runat="server" ShowPopupOnFocus="true" CssClass="text" 
                    AutoPostBack="True" Culture="fr-FR" AutoPostBackControl="TimeView" >
                    <TimeView ID="TimeView1" runat="server" CellSpacing="-1" Culture="fr-FR"></TimeView>

                    <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>

                    <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                    <DateInput ID="DateInput1"  runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth=""></DateInput>

                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                </telerik:RadDateTimePicker>

                   <br /> Fin<br />
            <telerik:RadDateTimePicker  ID="TbDateFin" Runat="server" ShowPopupOnFocus="true" CssClass="text" 
                Culture="fr-FR" AutoPostBack="True" AutoPostBackControl="TimeView" >
                    <TimeView ID="TimeView2"  runat="server" CellSpacing="-1" Culture="fr-FR"></TimeView>

                    <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>

                    <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                    <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="" 
                                        AutoPostBack="True"></DateInput>

                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDateTimePicker>

                </asp:TableCell>
                <asp:TableCell>
                    Numéro TAXI
                   
                   <td>
                    <asp:TextBox ID="TbNumTaxi" runat="server"  CssClass="text" ></asp:TextBox>
                   </td>
                  
                   
                  </asp:TableCell>
                <asp:TableCell>
                    Type TAXI
                    
                   <td>
                    <asp:DropDownList ID="DDLTypeTaxi"  runat="server"  CssClass="text"  >
                    
                    </asp:DropDownList>
                   </td>
                  
                   
                </asp:TableCell>
                
                <asp:TableCell>
                     <asp:RadioButtonList ID="RbIdentification" runat="server" CellPadding="10" CssClass="text" >
                        <asp:ListItem Text=" Avec pointages" Selected="True" Value="true"></asp:ListItem>
                        <asp:ListItem Text=" Sans pointage" Value="false"></asp:ListItem>
                     </asp:RadioButtonList>
                </asp:TableCell>
              

                <asp:TableCell>
                <asp:TextBox ID="TbNbrLignes" runat="server" Width="59px" BackColor="Black" CssClass="text"  ReadOnly="true" 
                    Font-Bold="True" ForeColor="White"></asp:TextBox>&nbsp; Taxis           
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

<%--<script language="javascript" type="text/javascript">
    function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
        var tbl = document.getElementById(gridId);
        if (tbl) {
            var DivHR = document.getElementById('DivHeaderRow');
            var DivMC = document.getElementById('DivMainContent');
            var DivFR = document.getElementById('DivFooterRow');

            //*** Set divheaderRow Properties ****
            DivHR.style.height = headerHeight + 'px';
            DivHR.style.width = (parseInt(width) + 250) + 'px';
            DivHR.style.position = 'relative';
            DivHR.style.top = '0px';
            DivHR.style.zIndex = '10';
            DivHR.style.verticalAlign = 'top';

            //*** Set divMainContent Properties ****
            DivMC.style.width = (parseInt(width) + 267) + 'px';
            DivMC.style.height = height + 'px';
            DivMC.style.position = 'relative';
            DivMC.style.top = -headerHeight + 'px';
            DivMC.style.zIndex = '1';

            //*** Set divFooterRow Properties ****
            DivFR.style.width = (parseInt(width) + 250) + 'px';
            DivFR.style.position = 'relative';
            DivFR.style.top = -headerHeight + 'px';
            DivFR.style.verticalAlign = 'top';
            DivFR.style.paddingtop = '2px';

            if (isFooter) {
                var tblfr = tbl.cloneNode(true);
                tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                var tblBody = document.createElement('tbody');
                tblfr.style.width = '100%';
                tblfr.cellSpacing = "0";
                tblfr.border = "0px";
                tblfr.rules = "none";
                //*****In the case of Footer Row *******
                tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                tblfr.appendChild(tblBody);
                DivFR.appendChild(tblfr);
            }
            //****Copy Header in divHeaderRow****
            DivHR.appendChild(tbl.cloneNode(true));
        }
    }
    function MakeStaticHeader2(gridId, height, width, headerHeight, isFooter) {
        var tbl = document.getElementById(gridId);
        if (tbl) {
            var DivHR = document.getElementById('DivHeaderRow');
            var DivMC = document.getElementById('DivMainContent');
            var DivFR = document.getElementById('DivFooterRow');

            //*** Set divheaderRow Properties ****
            DivHR.style.height = headerHeight + 'px';
            DivHR.style.width = (parseInt(width) + 250) + 'px';
            DivHR.style.position = 'relative';
            DivHR.style.top = '0px';
            DivHR.style.zIndex = '10';
            DivHR.style.verticalAlign = 'top';

            //*** Set divMainContent Properties ****
            DivMC.style.width = (parseInt(width) + 267) + 'px';
            DivMC.style.height = heightGrid + 'px';
            DivMC.style.position = 'relative';
            DivMC.style.top = -headerHeight + 'px';
            DivMC.style.zIndex = '1';

            //*** Set divFooterRow Properties ****
            DivFR.style.width = (parseInt(width) + 250) + 'px';
            DivFR.style.position = 'relative';
            DivFR.style.top = -headerHeight + 'px';
            DivFR.style.verticalAlign = 'top';
            DivFR.style.paddingtop = '2px';

            if (isFooter) {
                var tblfr = tbl.cloneNode(true);
                tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                var tblBody = document.createElement('tbody');
                tblfr.style.width = '100%';
                tblfr.cellSpacing = "0";
                tblfr.border = "0px";
                tblfr.rules = "none";
                //*****In the case of Footer Row *******
                tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                tblfr.appendChild(tblBody);
                DivFR.appendChild(tblfr);
            }
            //****Copy Header in divHeaderRow****
            DivHR.appendChild(tbl.cloneNode(true));
        }
    }



    function OnScrollDiv(Scrollablediv) {

        document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
        document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
    }



    // This Script is used to maintain Grid Scroll on Partial Postback
    var scrollTop;
    //Register Begin Request and End Request 
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    //Get The Div Scroll Position
    function BeginRequestHandler(sender, args) {
        var m = document.getElementById('DivMainContent');
        scrollTop = m.scrollTop;
    }
    //Set The Div Scroll Position
    function EndRequestHandler(sender, args) {
        var m = document.getElementById('DivMainContent');
        m.scrollTop = scrollTop;
    } 

</script>--%>


<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    
   <%--  <div id="DivRoot" align="left">
    <div style="overflow: hidden;" id="DivHeaderRow">
    </div>

    <div style="overflow:auto;" onscroll="OnScrollDiv(this)" id="DivMainContent" >
   --%>
        <asp:GridView ID="GridviewIdentification"   
            runat="server"  
            CssClass="GridViewStyle" 
            PageSize="15"
            AutoGenerateColumns="false" 
            AllowPaging="true" 
            AllowSorting="True"
            OnSorting="Agrement_Sorting"
             ShowHeaderWhenEmpty="true"
            
            OnPageIndexChanging="GridviewIdentification_PageIndexChanging"
            onrowdatabound="GridViewUsers_RowDataBound">
          
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />    
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />

            <Columns>  
                
                <asp:TemplateField HeaderText="Numéro Taxi  "  SortExpression="NumTaxi" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="LbLNumTaxi" runat="server" Text='<%#bind("NumTaxi") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type Taxi  "  SortExpression="TypeTaxi" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="LblTypeTAxi" runat="server"  Text='<%#bind("TypeTaxi") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                  <asp:TemplateField HeaderText="Premier Pointage  " SortExpression="DateDebut" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label Width="100%"  ID="LblDateDebut" runat="server" Text='<%#bind("DateDebut") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            
                 <asp:TemplateField HeaderText="Nom  " SortExpression="Nom_Prem" HeaderStyle-Width="14%">
                    <ItemTemplate>
                        <asp:Label ID="LblNomPremId" runat="server" Text='<%#bind("Nom_Prem") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Prénom  "  SortExpression="Prenom_Prem" HeaderStyle-Width="14%">
                    <ItemTemplate>
                        <asp:Label ID="LblPrenomPremId" runat="server" Text='<%#bind("Prenom_Prem") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                
                <asp:TemplateField HeaderText="Dernier Pointage  " SortExpression="DateFin" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label Width="100%"  ID="LblDateFin" runat="server" Text='<%#bind("DateFin") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Nom  " SortExpression="Nom_Dern" HeaderStyle-Width="15%">
                    <ItemTemplate>
                        <asp:Label ID="LblNomDernId" runat="server" Text='<%#bind("Nom_Dern") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Prénom  " SortExpression="Prenom_Dern" HeaderStyle-Width="15%">
                    <ItemTemplate>
                        <asp:Label ID="LblPrenomDernId" runat="server" Text='<%#bind("Prenom_Dern") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

              
             
            </Columns>
        </asp:GridView>

        <%--  </div>

    <div id="DivFooterRow" style="overflow:hidden">
    </div>
</div>
--%>
    </ContentTemplate>
    <Triggers>
           <asp:AsyncPostBackTrigger ControlID="Filtre"  EventName="Click" />
           <asp:AsyncPostBackTrigger ControlID="GridviewIdentification"  />    
      
    </Triggers>
</asp:UpdatePanel>

</asp:Content>

