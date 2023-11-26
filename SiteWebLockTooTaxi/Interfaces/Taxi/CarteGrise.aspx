<%@ Page Title="Cartes_Grises" Language="C#" MasterPageFile="~/Interfaces/Shared/MasterPage.master" AutoEventWireup="true" CodeFile="CarteGrise.aspx.cs" Inherits="CarteGrise_index" %>
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
                <asp:CheckBox ID="CbFiltre"  CssClass="button" runat="server" AutoPostBack="true" Checked="true" Text="Afficher les filtres" OnCheckedChanged="CbFiltre_CheckedChanged" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="BtnActualiser" CssClass="button"  runat="server" Text="Actualiser" onclick="Filtre" />
            </asp:TableCell>
            <asp:TableCell>
              <asp:ImageButton ID="export" CssClass="button"  ImageUrl="~/Icons/excel_Listes.png" runat="server"   ToolTip="Exporter à Excel"  onclick="exportExcel_Click" />
               </asp:TableCell>
            <asp:TableCell>
                <asp:ImageButton ID="BtnAdd" CssClass="button"  ImageUrl="~/Icons/CarteGrise.png" runat="server" ToolTip="Ajouter" onclick="BtnAdd_Click"    />
            </asp:TableCell>
            <asp:TableCell>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  >
                <ContentTemplate>
                <asp:ImageButton ID="BtnSet" ImageUrl="~/Icons/SetCarteGrise.png"  CssClass="button"  runat="server" ToolTip="Modifier"   OnClick="BtnSet_Click" Visible = "false" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridViewCartesGrises" EventName="SelectedIndexChanging" />
                    <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
                     <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
                </Triggers>
            </asp:UpdatePanel>
            

            </asp:TableCell>
            <asp:TableCell>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional"  >
                <ContentTemplate>
                <asp:ImageButton ID="BtnDelete" ImageUrl="~/Icons/DeleteCarteGrise.png" CssClass="button"  runat="server" ToolTip="Supprimer"  OnClick="BtnDelete_Click"  OnClientClick="return confirm('Etes-vous sûr que vous voulez supprimer cette carte grise ?');" Visible = "false"  />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridViewCartesGrises" EventName="SelectedIndexChanging" />
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
            <asp:Literal ID="Literal_MsgBox" runat="server"></asp:Literal>
            <fieldset style="width:98%;height:65%;">

                <table style="width: 1174px">

                    <tr>
                    
                   <td> Date Immatriculation (<) </td>
                   <td> Date Mise en circulation (<) </td>
                   <td style="width:15px;">  </td> 
                   <td>Immatriculation</td> 
                   <td style="width:15px;">  </td>                 
                   <td>Marque </td>
                   <td style="width:15px;">  </td>
                   <td>Modèle </td>
                   <td style="width:15px;">  </td>
                   <td>Nom </td>
                   <td style="width:15px;">  </td>
                   <td>Prénom </td>
                   <td style="width:15px;">  </td>
                   <td>Cin </td>  
                    <td> </td>
                 
                    </tr>

                    <tr>
                    
                    <td>           
                    <telerik:RadDateTimePicker  ID="TbDateImmat" Runat="server" ShowPopupOnFocus="true"  CssClass="text" 
                         Culture="fr-FR" AutoPostBackControl="TimeView" >
                        <TimeView ID="TimeView1" runat="server" CellSpacing="-1" Culture="fr-FR"></TimeView>

                        <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>

                        <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth=""></DateInput>

                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    </telerik:RadDateTimePicker>
                    </td>
                    <td>      
                     <telerik:RadDateTimePicker  ID="TbDateMiseEnCirculation" Runat="server" ShowPopupOnFocus="true"  CssClass="text" 
                        Culture="fr-FR"  AutoPostBackControl="TimeView" >
                            <TimeView ID="TimeView2" runat="server" CellSpacing="-1" Culture="fr-FR"></TimeView>

                            <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>

                            <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                            <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="" 
                                                AutoPostBack="True"></DateInput>

                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    </telerik:RadDateTimePicker>
                    </td>
                    <td style="width:15px;">  </td> 
                    <td>
                        <asp:TextBox ID="TbImmat" runat="server"  CssClass="text" AutoPostBack="true"  Width="90px"></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TbImmat" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890-" />
                    </td>
                    <td style="width:15px;">  </td>
                     <td>
                        <asp:TextBox ID="TbMarque" runat="server"  CssClass="text" AutoPostBack="true"  Width="90px"></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TbMarque" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890- " />
                    </td>
                    <td style="width:15px;">  </td>
                     <td>
                        <asp:TextBox ID="TbModele" runat="server"  CssClass="text" AutoPostBack="true"  Width="90px"></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="TbModele" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890- " />
                    </td>
                    <td style="width:15px;">  </td>
                    <td >
                        <asp:TextBox ID="TbNomProprietaire" runat="server" CssClass="text"  AutoPostBack="true" Width="90px" ></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="TbNomProprietaire" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                    </td>
                    <td style="width:15px;">  </td>
                    <td >
                        <asp:TextBox ID="TbPrenomProprietaire" runat="server" CssClass="text"  AutoPostBack="true" Width="90px" ></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="TbPrenomProprietaire" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                    </td>
                    <td style="width:15px;">  </td>
                      <td >
                        <asp:TextBox ID="TbCinProprietaire" runat="server" CssClass="text"  AutoPostBack="true" Width="90px" ></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="TbCinProprietaire" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890 " />
                    </td>    
                    <td style="width:15px;">  </td>         
                    <td class="style1" >                       
                        <asp:TextBox ID="NbrLignes" runat="server"  CssClass="text"  AutoPostBack="true" BackColor="Black" Font-Bold="True"   ReadOnly="true" 
                            ForeColor="White" Height="23px" style="margin-left: 108px" Width="59px">
                        </asp:TextBox>
                            
                    </td>
                    <td>Véhicules </td>
                    </tr>

                </table>

            </fieldset>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="CbFiltre" EventName="CheckedChanged" />
        <asp:AsyncPostBackTrigger ControlID="BtnActualiser" EventName="Click" />
         <asp:AsyncPostBackTrigger ControlID="BtnDelete"  EventName="Click"/>
        
    </Triggers>
    </asp:UpdatePanel>      
   
   


    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional" >
    <ContentTemplate>



 


  
        <asp:GridView ID="GridViewCartesGrises" 
        CssClass="GridViewStyle" 
        runat="server"
        AutoGenerateColumns="False"  
        ShowFooter="True"         
        ShowHeaderWhenEmpty="true"
        AllowSorting="True"
        OnSorting="CarteGrise_Sorting" 
                    
        
        onrowdeleting="GridViewCartesGrises_RowDeleting" 
        OnPageIndexChanging="GridViewCartesGrises_IndexChanging" 
        onrowdatabound="GridViewCartesGrises_RowDataBound" 
        onselectedindexchanging="GridViewCartesGrises_SelectedIndexChanging" >
           
   
         
        <FooterStyle CssClass="GridViewFooterStyle" />
        <RowStyle CssClass="GridViewRowStyle" />    
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <HeaderStyle CssClass="GridViewHeaderStyle" />


                <Columns>
                   
                    
                    <asp:ButtonField  Text="" CommandName="Select"   HeaderStyle-Width="0px" HeaderStyle-BackColor="White" HeaderStyle-BorderColor="White" ItemStyle-Width="0px" ItemStyle-BackColor="White" ItemStyle-BorderColor="White"  FooterStyle-Width="0px" FooterStyle-BackColor="White" FooterStyle-BorderColor="White"/>
                    
                    <asp:TemplateField   HeaderText="N° Immatriculation"   SortExpression="Immat"  ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"  >
                        <ItemTemplate>
                            <asp:Label ID="LblImmat" runat="server" Text='<%#Eval("Immat") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField >

                    <asp:TemplateField HeaderText="Date Immatriculation  " ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="13%" SortExpression="DateImmat">
                        <ItemTemplate>
                            <asp:Label ID="LblDateImmat" runat="server"  Text='<%#bind("DateImmat") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date Mise en Circulation  " ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="13%" SortExpression="DateMec">
                        <ItemTemplate>
                            <asp:Label ID="LblDateMec" runat="server"  Text='<%#bind("DateMec") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Marque  "  FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="10%" SortExpression="Marque">
                        <ItemTemplate>
                            <asp:Label ID="LblMarque" runat="server"  Text='<%#bind("Marque") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate >

                            <asp:Button id="BtnFirst" runat="server" Text="&#9664;&#9664;"  CssClass="text" ToolTip="Première Page" OnClick="BtnFirst_Click" />  
                            <asp:Button id="BtnPrevious" runat="server" Text="&#9664;" CssClass="text" ToolTip="Page Précédente" OnClick="BtnPrevious_Click" />
                         
                       </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Modèle  "   FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" SortExpression="Modele">
                        <ItemTemplate>
                            <asp:Label ID="LblModele" runat="server"  Text='<%#bind("Modele") %>'></asp:Label>
                        </ItemTemplate>
                             <FooterTemplate >
                       
                              <asp:Button id="BtnNext" runat="server" Text="&#9654;"  CssClass="text" ToolTip="Page Suivante" OnClick="BtnNext_Click"  />    
                              <asp:Button id="BtnLast" runat="server" Text="&#9654;&#9654;"  CssClass="text" ToolTip="Dernière Page" OnClick="BtnLast_Click" />
                             
                          </FooterTemplate>
                    </asp:TemplateField>
                     

                  <asp:TemplateField HeaderText="Nom Propriétaire  "  HeaderStyle-Width="16%" SortExpression="Nom">
                        <ItemTemplate>
                            <asp:Label ID="LblNom" runat="server"  Text='<%#bind("Nom") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                    <asp:TemplateField HeaderText="Prénom Propriétaire  "  HeaderStyle-Width="17%" SortExpression="Prenom">
                        <ItemTemplate>
                            <asp:Label ID="LblPrenom" runat="server"  Text='<%#bind("Prenom") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                   <asp:TemplateField HeaderText="Cin Propriétaire "  ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" SortExpression="Cin">
                        <ItemTemplate>
                            <asp:Label ID="LblCin" runat="server"  Text='<%#bind("Cin") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

       
   <%-- </div>

    <div id="DivFooterRow" style="overflow:hidden">
    </div>
    </div>--%>

    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
        <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
        <asp:AsyncPostBackTrigger ControlID="GridViewCartesGrises"  />    
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>

