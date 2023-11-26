<%@ page language="C#" masterpagefile="~/Interfaces/Shared/MasterPage.master" autoeventwireup="true" inherits="Interfaces_Taxi_Detail, App_Web_ayy2h5v3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 150px;
        }
        .style2
        {
            width: 297px;
        }
    </style>
    <script language="JavaScript" type="text/JavaScript">
        function doPrint() { // date: 19/01/2017 par Zouhair LOUALID 

            document.getElementById("header").style.display = "none";
            document.getElementById("page").style.border = "0px solid #ffffff";
            document.getElementById("fieldset1").style.border = "0px solid #ffffff";

            document.getElementById("LegendFicheAgrement").innerHTML = "Fiche Agrement";

            document.getElementById("MainContent_TdSet").style.display = "none";
            document.getElementById("Head1").style.display = "none";
            
            window.print();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivIdentite" runat="server" style="margin-left: 450px;">
                <fieldset id="fieldset1" class="login" style="width: 454px; height: 400px;">
                    <legend id="LegendFicheAgrement">Ajouter / Modifier un agrément (Licenses Taxi)</legend>
                    <table>
                        <tr>
                            <td class="style1">
                                Date de Début de validité
                                <asp:RequiredFieldValidator ValidationGroup="validation" runat="server" ControlToValidate="TbDateDebutValidite"
                                    ErrorMessage="champ obligatoire" Display="None" ID="RequiredFieldValidator6"></asp:RequiredFieldValidator>
                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True"
                                    TargetControlID="RequiredFieldValidator6">
                                </asp:ValidatorCalloutExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="TbDateDebutValidite" runat="server" Width="200px" CssClass="text"></asp:TextBox>
                                <asp:CalendarExtender ID="TbDateMiseEnCirculation_CalendarExtender1" runat="server"
                                    Enabled="True" TargetControlID="TbDateDebutValidite">
                                </asp:CalendarExtender>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="validation"
                                    Type="Date" Operator="DataTypeCheck" ControlToValidate="TbDateDebutValidite"
                                    ErrorMessage=" * Date Invalide" ForeColor="Red">
                                </asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                Date de Fin de validité
                                <asp:RequiredFieldValidator ValidationGroup="validation" runat="server" ControlToValidate="TbDateFinValidite"
                                    ErrorMessage="champ obligatoire" Display="None" ID="RequiredFieldValidator7"></asp:RequiredFieldValidator>
                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                                    TargetControlID="RequiredFieldValidator7">
                                </asp:ValidatorCalloutExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="TbDateFinValidite" runat="server" Width="200px" CssClass="text"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="TbDateFinValidite">
                                </asp:CalendarExtender>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="validation"
                                    Type="Date" Operator="DataTypeCheck" ControlToValidate="TbDateFinValidite" ErrorMessage=" * Date Invalide"
                                    ForeColor="Red">
                                </asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                CIN
                                <%------------------------Date: 26/01/2017 Zouhair LOUALID--------------------------%>
                            </td>
                            <td>
                                <%--  Ajout de la CIN--%>
                                <asp:TextBox ID="TbCIN" runat="server" AutoPostBack="true" CssClass="text" Width="200px"></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="ftbe1" runat="server" TargetControlID="TbCIN"
                                    ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                Nom
                            </td>
                            <td>
                                <asp:TextBox ID="TbNom" CssClass="text" runat="server" Width="200px"></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="ftbe11" runat="server" TargetControlID="TbNom"
                                    ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                Prénom
                            </td>
                            <td>
                                <asp:TextBox ID="TbPrenom" CssClass="text" runat="server" Width="200px"></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="ftbe12" runat="server" TargetControlID="TbPrenom"
                                    ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                Adresse
                            </td>
                            <td>
                                <%----------------------------Ajout de la ADRESSE------------------------------------%>
                                <asp:TextBox ID="TbAdresse" runat="server" Width="200px" CssClass="text" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                Téléphone<%----------------------------Ajout de la ADRESSE------------------------------------%>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TbTelephone"
                                    Display="None" ErrorMessage="champ obligatoire" ValidationGroup="validation"></asp:RequiredFieldValidator>
                                <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender1"
                                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                </asp:ValidatorCalloutExtender>--%>
                            </td>
                            <td>
                                <asp:TextBox ID="TbTelephone" CssClass="text" runat="server" Width="200px"></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    TargetControlID="TbTelephone" ValidChars="0123456789" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                N° Agrément
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TbAgrement"
                                    Display="None" ErrorMessage="champ obligatoire" ValidationGroup="validation"></asp:RequiredFieldValidator>
                                <asp:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                </asp:ValidatorCalloutExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="TbAgrement" CssClass="text" runat="server" Width="200px"></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="ftbe13" runat="server" TargetControlID="TbAgrement"
                                    ValidChars="0123456789" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                Type Taxi
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLTypeTaxi" CssClass="text" runat="server" AutoPostBack="True"
                                    Width="206px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                N° Immatriculation
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLNumImmatriculation" CssClass="text" runat="server" Width="206px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                Point d'attache
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLPntAttache" CssClass="text" runat="server" Width="206px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table style="height: 10px; margin-left: 150px;">
                        <tr>
                            <td id="TdSet" runat="server" verticalalign="Middle" horizontalalign="right">
                                <asp:Button ID="BtnSet" runat="server" Text="Modifier" CssClass="button" OnClick="BtnSet_Click" />
                                <asp:Button ID="BtnBack" runat="server" Text="Retour" CssClass="button" OnClick="BtnBack_Click" />
                                <asp:Button ID="BtnImprimer" runat="server" CssClass="button" Text="Imprimer" OnClientClick="doPrint();" /><%--  date: 19/01/2017 par Zouhair LOUALID  Ajouter clique coté client pour impression de la fiche chauffeur--%>
                            </td>
                            <td id="TdSave" runat="server" verticalalign="Middle" horizontalalign="right">
                                <asp:Button ID="BtnSaveAgrement" runat="server" CssClass="button" Text="Enregistrer"
                                    ValidationGroup="validation" OnClick="BtnSaveAgrement_Click" />
                                <asp:Button ID="BtnCancel" runat="server" CssClass="button" Text="Annuler" OnClick="BtnBack_Click" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnSaveAgrement" />
            <asp:PostBackTrigger ControlID="BtnCancel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
