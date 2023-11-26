<%@ Page Title="Synthèse des Taxis Absents" Language="C#" MasterPageFile="~/Interfaces/Shared/MasterPage.master" AutoEventWireup="true" CodeFile="SyntheseTaxisAbsentsDP.aspx.cs" Inherits="Interfaces_Rapport_SyntheseControles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    

     <asp:Table ID="BarreControle" runat="server" CellPadding="5">
            <asp:TableRow>
            <asp:TableCell>
                <asp:CheckBox ID="CbFiltre" runat="server" CssClass="button"  AutoPostBack="true" Checked="True" Text="Afficher les filtres" OnCheckedChanged="CbFiltre_CheckedChanged" />
            
            </asp:TableCell>
              <asp:TableCell>
                 <asp:Button ID="Filtre" runat="server" CssClass="button" Text="Actualiser" OnClick="Filtre_Click" />                           
              </asp:TableCell>
            <asp:TableCell>
                 <asp:ImageButton ID="export"  ImageUrl="~/Icons/excel.png" runat="server" CssClass="button"  ToolTip="Exporter à Excel"  onclick="exportExcel_Click" />                          
            </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

    <asp:UpdatePanel ID="UpdatePanel_Filter" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
           <asp:Panel  id="Panelfilter" runat="server" Visible="true" style="width:99.4%;height:70px; margin-left:8px;">
            <fieldset id="Fieldset_Filter" runat="server" Visible="true" style="width:98%;height:55%;">
            <asp:Table ID="Table_Filter" runat="server" width ="959px" >
                    <asp:TableRow >
                        <asp:TableCell>
                            Date du jour
                        </asp:TableCell>
                        <asp:TableCell>
                            Plage
                        </asp:TableCell>
                        <asp:TableCell>
                            Type Taxi
                        </asp:TableCell>
                        <asp:TableCell >
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:TextBox ID="TbDateJour" AutoPostBack="true" Width="75px" runat="server" CssClass="text"></asp:TextBox>
                            <asp:CalendarExtender ID="TbDateJour_CalendarExtender" runat="server" Enabled="True" TargetControlID="TbDateJour">
                            </asp:CalendarExtender>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ControlToValidate="TbDateJour" Display="None" ErrorMessage="champ obligatoire"  ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender runat="server" Enabled="true" TargetControlID="RequiredFieldValidator"></asp:ValidatorCalloutExtender>
                           <asp:CompareValidator ID="CompareValidator" runat="server" ControlToValidate="TbDateJour" Type="Date" Operator="DataTypeCheck" Display="None" ErrorMessage="Format incorrect"></asp:CompareValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="true" TargetControlID="CompareValidator"></asp:ValidatorCalloutExtender>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="DDLPlage" AutoPostBack="true" Width="100%" runat="server" CssClass="text">
                                <asp:ListItem Value="0">Toute la journnée</asp:ListItem>
                                <asp:ListItem Value="1"> 08h -> 10h </asp:ListItem>
                                <asp:ListItem Value="2"> 08h -> 16h </asp:ListItem>
                                <asp:ListItem Value="3"> 08h -> 21h </asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="DDLTypeTaxi" AutoPostBack="true" Width="100%" runat="server" CssClass="text">
                            </asp:DropDownList>
                        </asp:TableCell>
                      <asp:TableCell>&nbsp;
                          <asp:TextBox ID="TbNbrLignes" runat="server"  AutoPostBack="true" Width="59px" BackColor="Black" Font-Bold="True" ForeColor="White"  CssClass="text" ReadOnly="true" ></asp:TextBox>  TAXIS       
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </fieldset>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            
              <asp:AsyncPostBackTrigger ControlID="CbFiltre" />
            
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel_Table" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
     
            
                <asp:Table ID="TableSyntheseControlesHeader" runat="server"  GridLines="Both" Width="100%" CellPadding="4">
                 </asp:Table>
                                      
               
              
            <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical"  Height="608px" Width="101.20%" >
            
                <asp:Table ID="TableSyntheseControlesBody" runat="server"  GridLines="Both"  Width="100%" CellPadding="4"     >

                </asp:Table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Filtre" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

