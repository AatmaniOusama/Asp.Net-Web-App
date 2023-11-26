<%@ Page Title="Historique" Language="C#" MasterPageFile="~/Interfaces/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="Historique.aspx.cs" Inherits="Interfaces_Historique_Historique" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Table ID="BarreControle" runat="server" CellPadding="5">
        <asp:TableRow>
            <asp:TableCell>
                <asp:CheckBox ID="CbFiltre" runat="server" AutoPostBack="true" CssClass="button"
                    Checked="true" Text="Afficher les filtres" OnCheckedChanged="CbFiltre_CheckedChanged" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="BtnActualiser" runat="server" CssClass="button" Text="Actualiser"
                    OnClick="Filtre" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:ImageButton ID="export" ImageUrl="~/Icons/excel.png" runat="server" CssClass="button"
                    ToolTip="Exporter à Excel" OnClick="exportExcel_Click" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:ImageButton ID="InsererPointage" ImageUrl="~/Icons/InsererPtge.jpg" runat="server"
                            CssClass="button" ToolTip="Insérer Pointage" OnClick="InsererPointageClick" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridViewEvents" EventName="SelectedIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="GridViewEvents" EventName="RowDataBound" />
                        <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
                        <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:TableCell>
            <asp:TableCell>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:ImageButton ID="BtnDelete" ImageUrl="~/Icons/DeletePointage.PNG" CssClass="button"
                            runat="server" ToolTip="Supprimer Pointage" OnClick="BtnDelete_Click" OnClientClick="return confirm('Etes-vous sûr que vous voulez supprimer ce Pointage ?');"
                            Visible="false" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridViewEvents" EventName="SelectedIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="GridViewEvents" EventName="RowDataBound" />
                        <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
                        <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:TableCell>
            <asp:TableCell Width="10px"></asp:TableCell>
            <asp:TableCell>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="TbNbrLignes" runat="server" Width="59px" BackColor="Black" CssClass="text"
                            Font-Bold="True" ForeColor="White" AutoPostBack="true" ReadOnly="true"></asp:TextBox>&nbsp;Pointages
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridViewEvents" EventName="SelectedIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="GridViewEvents" EventName="RowDataBound" />
                        <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
                        <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:TableCell>
            <asp:TableCell>
                <span class="failureNotification">
                    <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                </span>
                <%--<asp:Button ID="BtnAdd" runat="server" Text="Ajouter" onclick="BtnAdd_Click" />--%>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panelfilter" runat="server" Visible="true">
                <fieldset>
                    <table>
                        <tr>
                            <td>
                                Début<br />
                                <telerik:RadDateTimePicker ID="TbDateDebut" runat="server" ShowPopupOnFocus="true"
                                    CssClass="text" Culture="fr-FR" AutoPostBackControl="TimeView">
                                    <TimeView ID="TimeView1" runat="server" CellSpacing="-1" Culture="fr-FR">
                                    </TimeView>
                                    <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>
                                    <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                        ViewSelectorText="x">
                                    </Calendar>
                                    <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy"
                                        LabelWidth="">
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDateTimePicker>
                                <br />
                            </td>
                            <td style="width: 10%;">
                                Borne<br />
                                <asp:DropDownList ID="DDLLecteur" runat="server" Width="90%" CssClass="text">
                                </asp:DropDownList>
                            </td>
                            <td>
                                CIN<br />
                                <asp:TextBox ID="TbMatriculeFiltre" runat="server" Width="80%" CssClass="text"></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="ftbe1" runat="server" TargetControlID="TbMatriculeFiltre"
                                    ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890" />
                            </td>
                            <td>
                                N°Permis<br />
                                <asp:TextBox ID="TbNPermis" runat="server" Width="80%" CssClass="text"></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="ftbe2" runat="server" TargetControlID="TbNPermis"
                                    ValidChars="1234567890" />
                            </td>
                            <td>
                                N°Taxi<br />
                                <asp:TextBox ID="TbNTaxi" runat="server" Width="80%" CssClass="text"></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="ftbe3" runat="server" TargetControlID="TbNTaxi"
                                    ValidChars="1234567890" />
                            </td>
                            <td>
                                Code Refus<br />
                                <asp:TextBox ID="TbCodeRefus" runat="server" Width="80%" CssClass="text"></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="ftbe4" runat="server" TargetControlID="TbCodeRefus"
                                    ValidChars="1234567890" />
                            </td>
                            <td rowspan="2" style="width: 10%;">
                                <asp:RadioButtonList ID="RbTypeCodeRefus" runat="server" CssClass="text">
                                    <asp:ListItem Value="-1" Selected="True">Tous</asp:ListItem>
                                    <asp:ListItem Value="1">Refus</asp:ListItem>
                                    <asp:ListItem Value="2">Accéptation</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td rowspan="2">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fin<br />
                                <telerik:RadDateTimePicker ID="TbDateFin" runat="server" ShowPopupOnFocus="true"
                                    CssClass="text" Culture="fr-FR" AutoPostBackControl="TimeView">
                                    <TimeView ID="TimeView2" runat="server" CellSpacing="-1" Culture="fr-FR">
                                    </TimeView>
                                    <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>
                                    <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                        ViewSelectorText="x">
                                    </Calendar>
                                    <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy"
                                        LabelWidth="">
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDateTimePicker>
                            </td>
                            <td style="width: 10%;">
                                Mode Pointage
                                <br />
                                <asp:DropDownList ID="DDLModePointage" runat="server" Width="90%" CssClass="text">
                                    <asp:ListItem Selected="True" Value="-1">Tous</asp:ListItem>
                                    <asp:ListItem Value="142">Badge</asp:ListItem>
                                    <asp:ListItem Value="144">Doigt et Badge</asp:ListItem>
                                    <asp:ListItem Value="48">Pointage Saisi</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                Nom<br />
                                <asp:TextBox ID="TbNomFiltre" runat="server" Width="80%" CssClass="text"></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="ftbe5" runat="server" TargetControlID="TbNomFiltre"
                                    ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz -" />
                            </td>
                            <td>
                                Immatriculation<br />
                                <asp:TextBox ID="TbImmatriculation" runat="server" Width="80%" CssClass="text"></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="ftbe6" runat="server" TargetControlID="TbImmatriculation"
                                    ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890-" />
                            </td>
                            <td>
                                Type Taxi<br />
                                <asp:DropDownList ID="DDLTypeTaxi" runat="server" Width="80%" CssClass="text" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Matricule Agent<br />
                                <asp:TextBox ID="TbMatriculeAgent" runat="server" Width="80%" CssClass="text"></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="ftbe7" runat="server" TargetControlID="TbMatriculeAgent"
                                    ValidChars="1234567890" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
            <asp:AsyncPostBackTrigger ControlID="BtnDelete" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="CbFiltre" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="Div1" runat="server" style="width: 80%; position: relative; float: left;">
        <asp:Panel ID="Panel1" runat="server" ScrollBars="Horizontal">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:GridView ID="GridViewEvents" runat="server" Width="190%" Height="100%" CssClass=" GridViewStyle"
                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowFooter="true" OnRowCommand="GridViewEvents_RowCommand"
                        OnSelectedIndexChanging="GridViewEvents_SelectedIndexChanging" AllowSorting="true"
                        OnSorting="Historique_Sorting" OnRowDataBound="GridViewEvents_RowDataBound">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:ButtonField ButtonType="Link" Text="" CommandName="Select" HeaderStyle-Width="0px"
                                HeaderStyle-BackColor="White" HeaderStyle-BorderColor="White" ItemStyle-Width="0px"
                                ItemStyle-BackColor="White" ItemStyle-BorderColor="White" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgVisualiser" ImageUrl="~/Icons/chauffeur_Historique.png" CssClass="button"
                                        ToolTip="Détails Chauffeur" runat="server" CommandName="Visualiser"></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pointage  " FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                SortExpression="Instant">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="Instant" Text='<%# Eval("Instant") %>'></asp:Label>
                                    <asp:Label runat="server" ID="LblIdEvent" Visible="false" Text='<%# Eval("IdEvent") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CIN  " ItemStyle-HorizontalAlign="Center" SortExpression="Reference">
                                <ItemTemplate>
                                    <asp:Label ID="Reference" runat="server" Text='<%# Eval("Reference") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nom  " HeaderStyle-Width="150px" SortExpression="Nom">
                                <ItemTemplate>
                                    <asp:Label ID="Nom" runat="server" Text='<%# Eval("Nom")%>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Prénom  " SortExpression="Prenom" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="Prenom" runat="server" Text='<%# Eval("Prenom") %>'></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="BtnFirst" runat="server" Text="&#9664;&#9664;" CssClass="text" ToolTip="Première Page"
                                        OnClick="BtnFirst_Click" />
                                    <asp:Button ID="BtnPrevious" runat="server" Text="&#9664;" CssClass="text" ToolTip="Page Précédente"
                                        OnClick="BtnPrevious_Click" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="N° Permis  " ItemStyle-HorizontalAlign="Center" SortExpression="NumBadge"
                                FooterStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="NumBadge" runat="server" Text='<%# Eval("NumBadge") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="BtnNext" runat="server" Text="&#9654;" CssClass="text" ToolTip="Page Suivante"
                                        OnClick="BtnNext_Click" />
                                    <asp:Button ID="BtnLast" runat="server" Text="&#9654;&#9654;" CssClass="text" ToolTip="Dernière Page"
                                        OnClick="BtnLast_Click" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Immatriculation  " ItemStyle-HorizontalAlign="Center"
                                SortExpression="Immatriculation">
                                <ItemTemplate>
                                    <asp:Label ID="Immatriculation" runat="server" Text='<%# Eval("Immatriculation") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="N° Taxi  " ItemStyle-HorizontalAlign="Center" SortExpression="NumTaxi">
                                <ItemTemplate>
                                    <asp:Label ID="NumTaxi" runat="server" Text='<%# Eval("NumTaxi") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type  " ItemStyle-HorizontalAlign="Center" SortExpression="TypeTaxi">
                                <ItemTemplate>
                                    <asp:Label ID="LibelleTypeTaxi" runat="server" Text='<%# Eval("LibelleTypeTaxi") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code Refus  " ItemStyle-HorizontalAlign="Center" SortExpression="CodeRefus">
                                <ItemTemplate>
                                    <asp:Label ID="CodeRefus" runat="server" Text='<%# Eval("CodeRefus") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Libellé Code Refus  " ItemStyle-HorizontalAlign="Center"
                                SortExpression="CodeRefus">
                                <ItemTemplate>
                                    <asp:Label ID="LibelleCode" runat="server" Text='<%# Eval("LibelleCode") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Borne  " SortExpression="NumLecteur">
                                <ItemTemplate>
                                    <asp:Label ID="NomLecteur" runat="server" Text='<%# Eval("NomLecteur") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mode Ptge  " ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="ModePointage" runat="server" Text='<%# Eval("LibelleModePointage") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Matricule Agent  " ItemStyle-HorizontalAlign="Center"
                                SortExpression="MatriculeAdmin">
                                <ItemTemplate>
                                    <asp:Label ID="MatriculeAdmin" runat="server" Text='<%# Eval("MatriculeAdmin") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nom Agent  " SortExpression="NomAgent">
                                <ItemTemplate>
                                    <asp:Label ID="NomAgent" runat="server" Text='<%# Eval("NomAgent") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Prénom Agent  " SortExpression="PrenomAgent">
                                <ItemTemplate>
                                    <asp:Label ID="PrenomAgent" runat="server" Text='<%# Eval("PrenomAgent") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
                    <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
                    <asp:AsyncPostBackTrigger ControlID="GridViewEvents" EventName="PageIndexChanging" />
                    <asp:AsyncPostBackTrigger ControlID="GridViewEvents" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <div id="Div2" runat="server" style="width: 20%; height: 500px; float: left;">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <fieldset style="text-align: center;">
                    <asp:Image ID="photo" runat="server" Height="280px" ImageAlign="Middle" ImageUrl="~/icons/Inconnu.jpg"
                        Width="90%" />
                </fieldset>
                <table>
                    <tr>
                        <td class="style1">
                            Nom
                        </td>
                        <td>
                            <asp:TextBox ID="TbNom" runat="server" CssClass="text"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Prénom
                        </td>
                        <td>
                            <asp:TextBox ID="TbPrenom" runat="server" CssClass="text"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            CIN
                        </td>
                        <td>
                            <asp:TextBox ID="TbMatricule" runat="server" CssClass="text"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            N° Permis
                        </td>
                        <td>
                            <asp:TextBox ID="TbNumPermis" runat="server" CssClass="text"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridViewEvents" EventName="PageIndexChanging" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
