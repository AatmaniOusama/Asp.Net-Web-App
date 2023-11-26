<%@ Page Title="Autorisations_Quiter_Périmètre_Urbain" Language="C#" MasterPageFile="~/Interfaces/Shared/MasterPage.master" AutoEventWireup="true" CodeFile="AutorisationsQPU.aspx.cs" Inherits="Interfaces_User_AutorisationsQPU" %>
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
                <asp:CheckBox ID="CbFiltre" runat="server" CssClass="button" AutoPostBack="true" Checked="true" Text="Afficher les filtres" OnCheckedChanged="CbFiltre_CheckedChanged" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="BtnActualiser"  runat="server" CssClass="button" Text="Actualiser" onclick="Filtre" />
            </asp:TableCell>
            <asp:TableCell>
             <asp:ImageButton ID="export"  ImageUrl="~/Icons/excel.png"  CssClass="button" runat="server"   ToolTip="Exporter à Excel"  onclick="exportExcel_Click" />
               </asp:TableCell>
            <asp:TableCell>
                <asp:ImageButton ID="BtnAdd" ImageUrl="~/Icons/add_AQPU.png"  CssClass="button" runat="server" ToolTip="Ajouter" onclick="BtnAdd_Click"    />
            </asp:TableCell>          
            
            <asp:TableCell>
                 <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional"  >
                <ContentTemplate> 
                <asp:ImageButton ID="BtAutoriser" ImageUrl="~/Icons/Autoriser_AQPU.png"  CssClass="button" runat="server" ToolTip="Autoriser"  OnClick="BtAutoriser_Click" Visible = "false" />
                <asp:ImageButton ID="BtInterdire" ImageUrl="~/Icons/Interdire_AQPU.png" CssClass="button"  runat="server" ToolTip="Interdire"  OnClick="BtInterdir_Click"  Visible = "false" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridViewAutorisations" EventName="SelectedIndexChanging" />
                    <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
                    
                </Triggers>
            </asp:UpdatePanel>
            
            </asp:TableCell>



           
           
    


            </asp:TableRow>
            </asp:Table>
            
    
    
    <asp:UpdatePanel runat="server" ID="UpdatePanel_Filtre" UpdateMode="Conditional" >
    <ContentTemplate>     
        <asp:Panel  id="Panelfilter" runat="server" Visible="true"  style="width:99.4%;height:95px; margin-left:8px;">
            <asp:Literal ID="Literal_MsgBox" runat="server"></asp:Literal>
            <fieldset style="width:98%;height:65%;">

                <table style="width: 1174px"  >

                    <tr >
                    
                   <td> Date début</td>
                   <td> Date fin </td>
                   <td>Listes </td>
                   <td>Matricule </td>
                   <td>Nom  </td>
                   <td>Prénom  </td>
                   
                   
                    <td rowspan="3">
                        <asp:RadioButtonList ID="RbAutoriseAQPU" runat="server" AutoPostBack="True" >
                            <asp:ListItem Value="YN" Selected="True">Tous</asp:ListItem>
                            <asp:ListItem Value="Y">Valide</asp:ListItem>
                            <asp:ListItem Value="N">Invalide</asp:ListItem>
                            
                        </asp:RadioButtonList>  
                    </td>
                 
                    </tr>

                    <tr>

                    <td>            
                    <telerik:RadDateTimePicker  ID="TbDateDebut" Runat="server" ShowPopupOnFocus="true" 
                         Culture="fr-FR" AutoPostBackControl="TimeView" >
                        <TimeView ID="TimeView1" runat="server" CellSpacing="-1" Culture="fr-FR"></TimeView>

                        <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>

                        <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth=""></DateInput>

                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    </telerik:RadDateTimePicker>
                    </td>
                    <td>      
                     <telerik:RadDateTimePicker  ID="TbDateFin" Runat="server" ShowPopupOnFocus="true" 
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
                        <asp:DropDownList ID="DDLListes" runat="server" AutoPostBack="True" CssClass="text"  Width="100px" >
                            <asp:ListItem Selected="True" Value="0"> Tous </asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="TbMatriculeChauffeur" runat="server" CssClass="text" AutoPostBack="true"  Width="100px"></asp:TextBox>
                           <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender0" runat="server" TargetControlID="TbMatriculeChauffeur" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890" />
                    </td>
                    <td>
                        <asp:TextBox ID="TbNomChauffeur" runat="server" CssClass="text" AutoPostBack="true"  Width="100px"></asp:TextBox>
                           <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TbNomChauffeur" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                    </td>
                   <td>
                        <asp:TextBox ID="TbPrenomChauffeur" runat="server" CssClass="text" AutoPostBack="true"  Width="100px"></asp:TextBox>
                           <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TbPrenomChauffeur" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                    </td>
                    
                  
                                   
                    <td class="style1">&nbsp;                       
                        <asp:TextBox ID="NbrLignes" runat="server"  CssClass="text" BackColor="Black" Font-Bold="True"  ReadOnly="true" 
                            ForeColor="White" Height="23px" style="margin-left: 108px" Width="59px">
                        </asp:TextBox>&nbsp; 
                            Autorisations
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

    
   
   

  
        <asp:GridView ID="GridViewAutorisations" 
        CssClass="GridViewStyle" 
        runat="server"
        AutoGenerateColumns="False"  
        ShowFooter="false"  
        FooterStyle-Height="7%" 
        ShowHeaderWhenEmpty="true"
      
        AllowPaging="true"  
        PageSize="15" 
     
   
        onrowdeleting="GridViewAutorisations_RowDeleting" 
        OnPageIndexChanging="GridViewAutorisations_IndexChanging" 
        onrowdatabound="GridViewAutorisations_RowDataBound" 
        onselectedindexchanging="GridViewAutorisations_SelectedIndexChanging" >
           
   
         
        <FooterStyle CssClass="GridViewFooterStyle" />
        <RowStyle CssClass="GridViewRowStyle" />    
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <HeaderStyle CssClass="GridViewHeaderStyle" />


                <Columns>
                   
                    
                    <asp:ButtonField  Text="" CommandName="Select"  HeaderStyle-Width="0px" HeaderStyle-BackColor="White" HeaderStyle-BorderColor="White" ItemStyle-Width="0px" ItemStyle-BackColor="White" ItemStyle-BorderColor="White" />
              
              <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="2%" ItemStyle-VerticalAlign="Middle">
                 <ItemTemplate>
                     <asp:ImageButton   ID="ImgValide" CssClass="button" ImageUrl="~/Icons/button-green.png"  ToolTip="Valide" runat="server"  Enabled="false" > </asp:ImageButton>
                      <asp:ImageButton   ID="ImgInvalide" CssClass="button" ImageUrl="~/Icons/button-red.png"  ToolTip="Invalide" runat="server"  Enabled="false" > </asp:ImageButton>
               </ItemTemplate>
             </asp:TemplateField>

                    <asp:TemplateField   HeaderText="N° Autorisation  " HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%" SortExpression="NumAuto"  ItemStyle-HorizontalAlign="Center" ControlStyle-Height="25"  >
                        <ItemTemplate>
                            
                            <asp:Label ID="LbNumAutorisation" runat="server" Text='<%#Eval("NumAuto") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField >

                    <asp:TemplateField HeaderText="N° Agrement  "   HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center"  SortExpression="NumAgrement">
                        <ItemTemplate>
                            <asp:Label ID="LblNumAgrement" runat="server"  Text='<%#bind("NumAgrement") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Déstination  "   HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="12%" SortExpression="Destination">
                        <ItemTemplate>
                            <asp:Label ID="LblDestination" runat="server"  Text='<%#bind("Destination") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Matricule   "   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%" SortExpression="MatriculeUser">
                        <ItemTemplate>
                            <asp:Label ID="LblMatricule" runat="server"  Text='<%#bind("MatriculeUser") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                    <asp:TemplateField HeaderText="Nom Chauffeur   "    HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="17%"  SortExpression="NomUser">
                        <ItemTemplate>
                            <asp:Label ID="LblNom" runat="server"  Text='<%#bind("NomComplet") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                     <asp:TemplateField HeaderText="Date Début "  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="11%" ItemStyle-HorizontalAlign="Center"  SortExpression="DateDebut">
                        <ItemTemplate>
                            <asp:Label ID="LblDateDebut" runat="server"  Text='<%#bind("DateDebut") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Date Fin  "  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="11%" ItemStyle-HorizontalAlign="Center"  SortExpression="DateFin">
                        <ItemTemplate>
                            <asp:Label ID="LblDateFin" runat="server"  Text='<%#bind("DateFin") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                  <asp:TemplateField   HeaderText="Valide  "  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="5%" SortExpression="Valide"  ItemStyle-HorizontalAlign="Center" ControlStyle-Height="25"  >
                        <ItemTemplate>
                             <asp:Label runat="server" ID="LblValide" Text='<%#bind("Valide") %>' ></asp:Label>                            
                        </ItemTemplate>
                  </asp:TemplateField >

                   <asp:TemplateField HeaderText="Date Création "  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="11%" ItemStyle-HorizontalAlign="Center"  SortExpression="DateCreation">
                        <ItemTemplate>
                            <asp:Label ID="LblDateCreation" runat="server"  Text='<%#bind("DateCreation") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Date Modification  "  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="11%" ItemStyle-HorizontalAlign="Center"  SortExpression="DateModif">
                        <ItemTemplate>
                            <asp:Label ID="LblDateModification" runat="server"  Text='<%#bind("DateModif") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

  
  
 

    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
     
        <asp:AsyncPostBackTrigger ControlID="GridViewAutorisations"  />    
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>

