<%@ Page Title="Chauffeurs Absents" Language="C#" MasterPageFile="~/Interfaces/Shared/MasterPage.master" AutoEventWireup="true" CodeFile="ChauffeursAbsents.aspx.cs" Inherits="Interfaces_Rapport_chauffeursAbsents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

        <asp:Table ID="BarreControle" runat="server" CellPadding="5">
            <asp:TableRow>

            <asp:TableCell>
                <asp:CheckBox ID="CbFiltre" runat="server"  CssClass="button" AutoPostBack="true" Checked="True" Text="Afficher les filtres" OnCheckedChanged="CbFiltre_CheckedChanged" />
            </asp:TableCell>
            <asp:TableCell>
              <asp:Button ID="Filtre" runat="server" CssClass="button" Text="Actualiser" OnClick="Filtre_Click" />
            </asp:TableCell>
            <asp:TableCell>              
                <asp:ImageButton ID="export"  ImageUrl="~/Icons/excel.png" runat="server" CssClass="button"   ToolTip="Exporter à Excel"  onclick="exportExcel_Click" />  
            </asp:TableCell>
          
            </asp:TableRow>
        </asp:Table>
 
   

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
     <asp:Panel  id="Panelfilter" runat="server" Visible="true" style="width:99.4%;height:80px; margin-left:8px;">
        <fieldset id="Fieldset_Filter" runat="server" Visible="true" style="width:98%;height:50%;">
            <asp:Table ID="Table_Filter" runat="server" style="width: 1174px" >
                <asp:TableRow>

                   <asp:TableCell>

               Date Début<br />
               <telerik:RadDateTimePicker  ID="TbDateDebut" Runat="server" ShowPopupOnFocus="true"  CssClass="text" 
                Culture="fr-FR"  AutoPostBackControl="TimeView" >
                    <TimeView ID="TimeView1" runat="server" CellSpacing="-1" Culture="fr-FR"></TimeView>

                    <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>

                    <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                    <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="" 
                                        ></DateInput>

                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDateTimePicker>


                    
                <br />

            </asp:TableCell>
            <asp:TableCell>

            Date Fin<br />
            <telerik:RadDateTimePicker  ID="TbDateFin" Runat="server" ShowPopupOnFocus="true"   CssClass="text" 
                Culture="fr-FR"  AutoPostBackControl="TimeView" >
                    <TimeView ID="TimeView2" runat="server" CellSpacing="-1" Culture="fr-FR"></TimeView>

                    <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>

                    <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                    <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="" 
                                        ></DateInput>

                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDateTimePicker>


                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" 
                        runat="server" Enabled="True" TargetControlID="CustomValidator_TbDateFin">
                    </asp:ValidatorCalloutExtender>

                    <asp:CustomValidator ID="CustomValidator_TbDateFin" runat="server" ControlToValidate="TbDateFin" 
                    OnServerValidate="ServerValidation" Display="None" Visible="true"  >
                    </asp:CustomValidator>

                    

           </asp:TableCell>

                


                <asp:TableCell>
                 N° Permis <br />
                    <asp:TextBox ID="TbNumPermis" runat="server" Width="120px"   CssClass="text"  ></asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="ftbe1" runat="server" TargetControlID="TbNumPermis" ValidChars="1234567890" />
                </asp:TableCell>

                 <asp:TableCell>
                 CIN <br />
                    <asp:TextBox ID="TbMatricule" runat="server" Width="120px"   CssClass="text"  ></asp:TextBox>
                 <ajaxToolkit:FilteredTextBoxExtender ID="ftbe2" runat="server" TargetControlID="TbMatricule" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890" />
                </asp:TableCell>

                <asp:TableCell>
                 Nom<br />
                    <asp:TextBox ID="TbNom" runat="server" Width="120px"   CssClass="text"  ></asp:TextBox>
                 <ajaxToolkit:FilteredTextBoxExtender ID="ftbe3" runat="server" TargetControlID="TbNom" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                </asp:TableCell>
                   <asp:TableCell>
                    Prénom<br />
                    <asp:TextBox ID="TbPrenom" runat="server" Width="120px"  CssClass="text" ></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbe4" runat="server" TargetControlID="TbPrenom" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                   </asp:TableCell>
                  
                  
                    <asp:TableCell>
                    <br />
                 <asp:TextBox ID="TbNbrLignes" runat="server" Width="59px" BackColor="Black"  CssClass="text" AutoPostBack="true"  ReadOnly="true" 
                    Font-Bold="True" ForeColor="White"></asp:TextBox>&nbsp; 
                             Chauffeurs
                </asp:TableCell>
            
      
             </asp:TableRow>
            </asp:Table>
        </fieldset>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
     <asp:AsyncPostBackTrigger ControlID="CbFiltre" EventName = "CheckedChanged"  />
     <asp:AsyncPostBackTrigger ControlID="Filtre"  EventName = "Click" />
    </Triggers>
</asp:UpdatePanel>



<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
    <ContentTemplate>



        <asp:GridView ID="GridviewAbsentsJours" 
        runat="server"  
        CssClass="GridViewStyle"
        AutoGenerateColumns="false"
        ShowFooter="true"       
        AllowSorting="True"
        OnSorting="Users_Sorting"
        ShowHeaderWhenEmpty="true"
        onrowdatabound="GridviewAbsentsJours_RowDataBound"
        onpageindexchanging="GridviewAbsentsJours_PageIndexChanging" >


        <FooterStyle CssClass="GridViewFooterStyle" />
        <RowStyle CssClass="GridViewRowStyle" />    
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <HeaderStyle CssClass="GridViewHeaderStyle" />

           
            <Columns>

                 <asp:TemplateField HeaderText="N° Permis  " SortExpression="NumPermis" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="LblNumPermis" runat="server" Text='<%#bind("NumBadge") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CIN  " SortExpression="Matricule" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LblMatricule" runat="server" Text='<%#bind("Matricule") %>'></asp:Label>
                    </ItemTemplate>
                     <FooterTemplate >

                         <asp:Button id="BtnFirst" runat="server" Text="&#9664;&#9664;"  CssClass="text" ToolTip="Première Page" OnClick="BtnFirst_Click" />  
                         <asp:Button id="BtnPrevious" runat="server" Text="&#9664;" CssClass="text" ToolTip="Page Précédente" OnClick="BtnPrevious_Click" />
                         
                        </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nom  " HeaderStyle-Width="20%" FooterStyle-HorizontalAlign="Left" SortExpression="Nom" >
                    <ItemTemplate>
                        <asp:Label ID="LblNom" runat="server" Text='<%#bind("Nom") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate >
                       
                          <asp:Button id="BtnNext" runat="server" Text="&#9654;"  CssClass="text" ToolTip="Page Suivante" OnClick="BtnNext_Click"  />    
                          <asp:Button id="BtnLast" runat="server" Text="&#9654;&#9654;"  CssClass="text" ToolTip="Dernière Page" OnClick="BtnLast_Click" />
                             
                      </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Prénom  " HeaderStyle-Width="20%" SortExpression="Prenom"  FooterStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="LblPrenom" runat="server" Text='<%#bind("Prenom") %>'></asp:Label>
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