<%@ page title="Taxis" language="C#" masterpagefile="~/Interfaces/Shared/MasterPage.master" autoeventwireup="true" inherits="Agrement_index, App_Web_ayy2h5v3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 294px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Table ID="BarreControle" runat="server" CellPadding="5">
        <asp:TableRow VerticalAlign="Bottom">
            <asp:TableCell>
                <asp:CheckBox ID="CbFiltre" runat="server" CssClass="button" AutoPostBack="true"
                    Checked="true" Text="Afficher les filtres" OnCheckedChanged="CbFiltre_CheckedChanged" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="BtnActualiser" runat="server" CssClass="button" Text="Actualiser"
                    OnClick="Filtre" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:ImageButton ID="export" ImageUrl="~/Icons/excel.png" CssClass="button" runat="server"
                    ToolTip="Exporter à Excel" OnClick="exportExcel_Click" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:ImageButton ID="BtnAdd" ImageUrl="~/Icons/AjoutTaxi.png" CssClass="button" runat="server"
                    ToolTip="Ajouter" OnClick="BtnAdd_Click" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:ImageButton ID="BtnSet" ImageUrl="~/Icons/ModifierTaxi.png" CssClass="button"
                            runat="server" ToolTip="Modifier" OnClick="BtnSet_Click" Visible="false" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridViewAgrements" EventName="SelectedIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="GridViewAgrements" EventName="RowDataBound" />
                        <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
                        <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:TableCell>
            <asp:TableCell>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:ImageButton ID="BtnDelete" ImageUrl="~/Icons/SupprimerTaxi.png" CssClass="button"
                            runat="server" ToolTip="Supprimer" OnClick="BtnDelete_Click" OnClientClick="return confirm('Etes-vous sûr que vous voulez supprimer cet agrément ?');"
                            Visible="false" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridViewAgrements" EventName="SelectedIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="GridViewAgrements" EventName="RowDataBound" />
                        <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
                        <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:TableCell>
            <asp:TableCell>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:ImageButton ID="BtAutoriser" ImageUrl="~/Icons/AutoriserTaxi.png" CssClass="button"
                            runat="server" ToolTip="Autoriser" OnClick="BtAutoriser_Click" Visible="false" />
                        <asp:ImageButton ID="BtInterdire" ImageUrl="~/Icons/InterdirTaxi.png" CssClass="button"
                            runat="server" ToolTip="Interdire" OnClick="BtInterdir_Click" Visible="false" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridViewAgrements" EventName="SelectedIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="GridViewAgrements" EventName="RowDataBound" />
                        <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
                        <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:UpdatePanel runat="server" ID="UpdatePanel_Filtre" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panelfilter" runat="server" Visible="true" Style="width: 99.4%; height: 95px;
                margin-left: 8px;">
                <asp:Literal ID="Literal_MsgBox" runat="server"></asp:Literal>
                <fieldset style="width: 98%; height: 65%;">
                    <table style="width: 1174px">
                        <tr>
                            <td>
                                Date de début de validité (>)
                            </td>
                            <td>
                                Date de fin de validité (<)
                            </td>
                            <td>
                                Agrément
                            </td>
                            <td>
                                N° Immat
                            </td>
                            <td>
                                Nom
                            </td>
                            <td>
                                Prénom
                            </td>
                            <td>
                                Type Taxi
                            </td>
                            <td rowspan="3">
                                <asp:RadioButtonList ID="RbAutorise" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="TF" Selected="True">Tous</asp:ListItem>
                                    <asp:ListItem Value="True">Autorisés</asp:ListItem>
                                    <asp:ListItem Value="False">Interdits</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
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
                            </td>
                            <td>
                                <telerik:RadDateTimePicker ID="TbDateFin" runat="server" ShowPopupOnFocus="true"
                                    CssClass="text" Culture="fr-FR" AutoPostBackControl="TimeView">
                                    <TimeView ID="TimeView2" runat="server" CellSpacing="-1" Culture="fr-FR">
                                    </TimeView>
                                    <TimePopupButton ImageUrl="" HoverImageUrl=""></TimePopupButton>
                                    <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                        ViewSelectorText="x">
                                    </Calendar>
                                    <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy"
                                        LabelWidth="" AutoPostBack="True">
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDateTimePicker>
                            </td>
                            <td>
                                <asp:TextBox ID="TbAgrement" runat="server" CssClass="text" AutoPostBack="true" Width="70px"></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="ftbe3" runat="server" TargetControlID="TbAgrement"
                                    ValidChars="1234567890" />
                            </td>
                            <td>
                                <asp:TextBox ID="TbImmatriculation" runat="server" CssClass="text" AutoPostBack="true"
                                    Width="70px"></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    TargetControlID="TbImmatriculation" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890-" />
                            </td>
                            <td>
                                <asp:TextBox ID="TbNom" runat="server" CssClass="text" AutoPostBack="true" Width="70px"></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                    TargetControlID="TbNom" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                            </td>
                            <td>
                                <asp:TextBox ID="TbPrenom" runat="server" CssClass="text" AutoPostBack="true" Width="70px"></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                    TargetControlID="TbPrenom" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLTypeTaxi" runat="server" CssClass="text" AutoPostBack="True"
                                    Width="60px">
                                    <asp:ListItem Selected="True" Value="-1"> Tous </asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="style1">
                                &nbsp;
                                <asp:TextBox ID="NbrLignes" runat="server" BackColor="Black" Font-Bold="True" CssClass="text"
                                    ReadOnly="true" ForeColor="White" Height="23px" Style="margin-left: 108px; text-align: center;"
                                    Width="59px">
                                </asp:TextBox>&nbsp; Agréments
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
    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="GridViewAgrements" CssClass="GridViewStyle" runat="server" AutoGenerateColumns="False"
                ShowFooter="true" ShowHeaderWhenEmpty="true" AllowSorting="True" OnSorting="Agrements_Sorting"
                OnRowCommand="ImgVisualiser_Click" OnPageIndexChanging="GridViewAgrements_IndexChanging"
                OnRowDataBound="GridViewAgrements_RowDataBound" OnSelectedIndexChanging="GridViewAgrements_SelectedIndexChanging">
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <Columns>
                    <asp:ButtonField Text="" CommandName="Select" HeaderStyle-Width="0px" HeaderStyle-BackColor="White"
                        HeaderStyle-BorderColor="White" FooterStyle-Width="0px" FooterStyle-BackColor="White"
                        FooterStyle-BorderColor="White" ItemStyle-Width="0px" ItemStyle-BackColor="White"
                        ItemStyle-BorderColor="White" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                        HeaderStyle-Width="2%">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgValide" ImageUrl="~/Icons/button-green.png" ToolTip="Valide"
                                runat="server" CommandName="Visualiser"></asp:ImageButton>
                            <asp:ImageButton ID="ImgInvalide" ImageUrl="~/Icons/button-red.png" ToolTip="Invalide"
                                runat="server" CommandName="Visualiser"></asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Agrément  " HeaderStyle-Width="7%" SortExpression="Agrement"
                        ItemStyle-HorizontalAlign="Center" ControlStyle-Height="25">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="Selected" Text='<%#bind("Valide") %>' Visible="false"></asp:Label>
                            <asp:Label ID="LblAgrement" runat="server" Text='<%#Eval("NumAgrement") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type Taxi  " HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Center"
                        SortExpression="TypeTaxi">
                        <ItemTemplate>
                            <asp:Label ID="LblTypeTAxi" runat="server" Text='<%#bind("TypeTaxi") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="N° d'Immatriculation  " HeaderStyle-Width="14%" ItemStyle-HorizontalAlign="Center"
                        SortExpression="Plaque">
                        <ItemTemplate>
                            <asp:Label ID="LblPlaque" runat="server" Text='<%#bind("Plaque") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date Début de validité " HeaderStyle-Width="14%" ItemStyle-HorizontalAlign="Center"
                        FooterStyle-HorizontalAlign="Right" SortExpression="DateDebut">
                        <ItemTemplate>
                            <asp:Label ID="LblDateDebut" runat="server" Text='<%#bind("DateDebut") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="BtnFirst" runat="server" Text="&#9664;&#9664;" CssClass="text" ToolTip="Première Page"
                                OnClick="BtnFirst_Click" />
                            <asp:Button ID="BtnPrevious" runat="server" Text="&#9664;" CssClass="text" ToolTip="Page Précédente"
                                OnClick="BtnPrevious_Click" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date Fin de validité  " HeaderStyle-Width="14%" ItemStyle-HorizontalAlign="Center"
                        FooterStyle-HorizontalAlign="Left" SortExpression="DateFin">
                        <ItemTemplate>
                            <asp:Label ID="LblDateFin" runat="server" Text='<%#bind("DateFin") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="BtnNext" runat="server" Text="&#9654;" CssClass="text" ToolTip="Page Suivante"
                                OnClick="BtnNext_Click" />
                            <asp:Button ID="BtnLast" runat="server" Text="&#9654;&#9654;" CssClass="text" ToolTip="Dernière Page"
                                OnClick="BtnLast_Click" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nom Propriétaire  " HeaderStyle-Width="17%" SortExpression="Nom">
                        <ItemTemplate>
                            <asp:Label ID="LblNom" runat="server" Text='<%#bind("Nom") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Prénom Propriétaire  " HeaderStyle-Width="15%" SortExpression="Prenom">
                        <ItemTemplate>
                            <asp:Label ID="LblPrenom" runat="server" Text='<%#bind("Prenom") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Point d'attache " HeaderStyle-Width="9%" SortExpression="Commune">
                        <ItemTemplate>
                            <asp:Label ID="LblpointAttache" runat="server" Text='<%#bind("Commune") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnActualiser" />
            <asp:AsyncPostBackTrigger ControlID="BtnDelete" />
            <asp:AsyncPostBackTrigger ControlID="BtAutoriser" />
            <asp:AsyncPostBackTrigger ControlID="BtInterdire" />
            <asp:AsyncPostBackTrigger ControlID="GridViewAgrements" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
