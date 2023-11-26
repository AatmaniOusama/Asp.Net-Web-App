<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Interfaces_User_Detail" %>

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
        function doPrint() { // date: 19/01/2017 par Zouhair LOUALID*********************************** 

            //methode coté client pour impression de la fiche chauffeur
            document.getElementById("MainContent_CommandesPanel").style.display = "none";
            document.getElementById("header").style.display = "none";
            document.getElementById("MainContent_DivIdentite").style.width = "25%";
            document.getElementById("MainContent_DivProfilPointage").style.width = "25%";
            document.getElementById("fieldset").style.width = "250px";
            document.getElementById("fieldset").style.border = "0px solid #ffffff";
            document.getElementById("fieldset2").style.width = "250px";
            document.getElementById("fieldset2").style.border = "0px solid #ffffff";
            document.getElementById("fieldset3").style.width = "250px";
            document.getElementById("fieldset3").style.border = "0px solid #ffffff";
            document.getElementById("fieldset4").style.border = "0px solid #ffffff";
            document.getElementById("MainContent_RbSexe").style.width = "150px";
            document.getElementById("MainContent_RbFlagAutorise").style.width = "150px";
            document.getElementById("MainContent_TbNom").style.width = "150px";
            document.getElementById("MainContent_TbPrenom").style.width = "150px";
            document.getElementById("MainContent_TbMatricule").style.width = "150px";
            document.getElementById("MainContent_DDLCivilite").style.width = "150px";
            document.getElementById("MainContent_TbDNaissance").style.width = "150px";
            document.getElementById("MainContent_TbLNaissance").style.width = "150px";
            document.getElementById("MainContent_TbAdresse").style.width = "150px";
            document.getElementById("MainContent_TbCodePostale").style.width = "150px";
            document.getElementById("MainContent_TbVille").style.width = "150px";
            document.getElementById("MainContent_TbTel").style.width = "150px";
            document.getElementById("MainContent_TbEmail").style.width = "150px";
            document.getElementById("MainContent_TbNSS").style.width = "150px";
            document.getElementById("MainContent_TbNCI").style.width = "150px";
            document.getElementById("MainContent_TbDateDelivrance").style.width = "150px";
            document.getElementById("MainContent_TbPermis").style.width = "150px";
            document.getElementById("MainContent_TbDateDelivrancePermis").style.width = "150px";
            document.getElementById("MainContent_TbDateDelivrance").style.width = "150px";
            document.getElementById("MainContent_TbDateDebut").style.width = "150px";
            document.getElementById("MainContent_TbDateFin").style.width = "150px";
            document.getElementById("MainContent_DDLService").style.width = "150px";
            document.getElementById("MainContent_DDLDroit").style.width = "150px";
            document.getElementById("MainContent_DDLBadge").style.width = "150px";
            document.getElementById("MainContent_TbAncienNP").style.width = "150px";
            document.getElementById("MainContent_TbObservation").style.width = "150px";
            document.getElementById("MainContent_photo").style.width = "250px";
            document.getElementById("page").style.border = "0px solid #ffffff";
            document.getElementById("page").style.heigh = "700px";
            document.getElementById("divfooterrifl").style.display = "none";
            document.getElementById("divfooterrifl").style.heigh = "1px";
            document.getElementById("divImage").float = "right";
            document.getElementById("divImage").style.paddingLeft = "60px";

            document.getElementById("MainContent_DivProfilPointage").style.paddingLeft = "40px";

            window.print();
        }

        var ddlText, ddlValue, ddl; // date: 20/02/2017 par Zouhair LOUALID ------- fonction sur liste des n°permis pour la recherche coté client----
        function CacheItems() {
            ddlText = new Array();
            ddlValue = new Array();
            ddl = document.getElementById("<%=DDLBadge.ClientID %>");

            for (var i = 0; i < ddl.options.length; i++) {
                ddlText[ddlText.length] = ddl.options[i].text;
                ddlValue[ddlValue.length] = ddl.options[i].value;
            }
        }

        function ChangeClientDdpermis() {

            document.getElementById("MainContent_txtSearch").value = "";
            CacheItems();

        }

        window.onload = CacheItems;

        function FilterItems(value) {// fonction de filtre*******
            ddl.options.length = 0;
            for (var i = 0; i < ddlText.length; i++) {
                if (ddlText[i].toLowerCase().indexOf(value) != -1) {
                    AddItem(ddlText[i], ddlValue[i]);
                }
            }
            if (ddl.options.length == 0) {
                AddItem("N°Permis Introuvable.", "");
            }
        }

        function AddItem(text, value) {// fonction d'affichage du resultat*******
            var opt = document.createElement("option");
            opt.text = text;
            opt.value = value;
            ddl.options.add(opt);
        } //-------------------------------------------------------------------------------------------fin
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivIdentite" runat="server" style="width: 33.33%; float: left;">
                <fieldset id="fieldset" class="login" style="width: 400px; height: 600px;">
                    <legend>Identité</legend>
                    <table>
                        <tr>
                            <td colspan="2" class="style1">
                                <asp:RadioButtonList runat="server" Font-Overline="false" RepeatDirection="Horizontal"
                                    TextAlign="Left" Width="300px" ID="RbSexe" AutoPostBack="True">
                                    <asp:ListItem Text="Homme" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Femme" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                Nom *
                                <asp:RequiredFieldValidator ValidationGroup="validation" runat="server" ControlToValidate="TbNom"
                                    ErrorMessage="champ obligatoire" Display="None" ID="RequiredFieldValidator3"></asp:RequiredFieldValidator>
                                <asp:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                </asp:ValidatorCalloutExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="TbNom" runat="server" Width="206px" CssClass="text"></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="ftbe3" runat="server" TargetControlID="TbNom"
                                    ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                Prénom *
                                <asp:RequiredFieldValidator ValidationGroup="validation" Display="None" ID="RequiredFieldValidator1"
                                    runat="server" ControlToValidate="TbPrenom" ErrorMessage="champ obligatoire"></asp:RequiredFieldValidator>
                                <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                </asp:ValidatorCalloutExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="TbPrenom" runat="server" Width="206px" CssClass="text"></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="ftbe2" runat="server" TargetControlID="TbPrenom"
                                    ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                            </td>
                            <tr>
                                <td class="style1">
                                    CIN *
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TbMatricule"
                                        Display="None" ErrorMessage="champ obligatoire" ValidationGroup="validation"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender"
                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </asp:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="TbMatricule" runat="server" Width="206px" CssClass="text"></asp:TextBox>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="ftbe4" runat="server" TargetControlID="TbMatricule"
                                        ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    Situation matrimoriale
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLCivilite" runat="server" AutoPostBack="True" Width="210px"
                                        CssClass="text">
                                        <asp:ListItem Value="0">Célibataire</asp:ListItem>
                                        <asp:ListItem Value="1">Marié</asp:ListItem>
                                        <asp:ListItem Value="2">Divorcé</asp:ListItem>
                                        <asp:ListItem Value="3">Veuf</asp:ListItem>
                                        <asp:ListItem Value="4">Pacsé</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    Date naissance
                                </td>
                                <td>
                                    <asp:TextBox ID="TbDNaissance" runat="server" Width="206px" CssClass="text"></asp:TextBox>
                                    <asp:CalendarExtender ID="TbDNaissance_CalendarExtender" runat="server" Enabled="True"
                                        TargetControlID="TbDNaissance">
                                    </asp:CalendarExtender>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="validation"
                                        Type="Date" Operator="DataTypeCheck" ControlToValidate="TbDNaissance" ErrorMessage=" * Date Invalide"
                                        ForeColor="Red">
                                    </asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    Lieu naissance
                                </td>
                                <td>
                                    <asp:TextBox ID="TbLNaissance" runat="server" Width="206px" CssClass="text"></asp:TextBox>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="ftbe5" runat="server" TargetControlID="TbLNaissance"
                                        ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890 " />
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    Adresse
                                </td>
                                <td>
                                    <asp:TextBox ID="TbAdresse" runat="server" Width="205px" CssClass="text" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    Code postale
                                </td>
                                <td>
                                    <asp:TextBox ID="TbCodePostale" runat="server" Width="206px" CssClass="text"></asp:TextBox>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="ftbe6" runat="server" TargetControlID="TbCodePostale"
                                        ValidChars="1234567890" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    Ville
                                </td>
                                <td>
                                    <asp:TextBox ID="TbVille" runat="server" Width="206px" CssClass="text"></asp:TextBox>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="ftbe7" runat="server" TargetControlID="TbVille"
                                        ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    Téléphone
                                </td>
                                <td>
                                    <asp:TextBox ID="TbTel" runat="server" Width="206px" CssClass="text"></asp:TextBox>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="ftbe8" runat="server" TargetControlID="TbTel"
                                        ValidChars="1234567890" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    Email
                                </td>
                                <td>
                                    <asp:TextBox ID="TbEmail" runat="server" Width="206px" CssClass="text"></asp:TextBox>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="ftbe9" runat="server" TargetControlID="TbEmail"
                                        ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890@._-" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    N° SS
                                </td>
                                <td>
                                    <asp:TextBox ID="TbNSS" runat="server" Width="206px" CssClass="text"></asp:TextBox>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="ftbe10" runat="server" TargetControlID="TbNSS"
                                        ValidChars="1234567890" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    Numéro Carte d'identité
                                </td>
                                <td>
                                    <asp:TextBox ID="TbNCI" runat="server" Width="206px" CssClass="text"></asp:TextBox>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="ftbe11" runat="server" TargetControlID="TbNCI"
                                        ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    Date délivrance C.I.N
                                </td>
                                <td>
                                    <asp:TextBox ID="TbDateDelivrance" runat="server" Width="206px" CssClass="text"></asp:TextBox>
                                    <asp:CalendarExtender ID="TbDateDelivrance_CalendarExtender" runat="server" Enabled="True"
                                        TargetControlID="TbDateDelivrance">
                                    </asp:CalendarExtender>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="validation"
                                        Type="Date" Operator="DataTypeCheck" ControlToValidate="TbDateDelivrance" ErrorMessage=" * Date Invalide"
                                        ForeColor="Red">
                                    </asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    N° permis de conduire
                                </td>
                                <td>
                                    <asp:TextBox ID="TbPermis" runat="server" Width="206px" CssClass="text"></asp:TextBox>
                                    <AjaxToolkit:FilteredTextBoxExtender ID="ftbe12" runat="server" TargetControlID="TbPermis"
                                        ValidChars="1234567890/" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    Date délivrance permis
                                </td>
                                <td>
                                    <asp:TextBox ID="TbDateDelivrancePermis" runat="server" Width="206px" CssClass="text"></asp:TextBox>
                                    <asp:CalendarExtender ID="TbDateDelivrancePermis_CalendarExtender" runat="server"
                                        Enabled="True" TargetControlID="TbDateDelivrancePermis">
                                    </asp:CalendarExtender>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="validation"
                                        Type="Date" Operator="DataTypeCheck" ControlToValidate="TbDateDelivrancePermis"
                                        ErrorMessage=" * Date Invalide" ForeColor="Red">
                                    </asp:CompareValidator><
                                </td>
                            </tr>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="RbFlagAutorise" />
            <asp:AsyncPostBackTrigger ControlID="BtnSet" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="PanelProfil" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivProfilPointage" style="width: 33.33%; float: left;" runat="server">
                <fieldset class="login" id="fieldset2" style="width: 400px; height: 600px">
                    <legend>Profil de pointage</legend>
                    <table>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="RbFlagAutorise" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                    TextAlign="Left" Width="278px" OnSelectedIndexChanged="RbFlagAutorise_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="Y">Autorisé</asp:ListItem>
                                    <asp:ListItem Value=" ">Interdit</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="style1">
                                Date début validité
                            </td>
                            <td>
                                <asp:TextBox ID="TbDateDebut" runat="server" Width="206px" Enabled="False" CssClass="text"></asp:TextBox>
                                <asp:CalendarExtender ID="TbDateDebut_CalendarExtender" runat="server" Enabled="True"
                                    TargetControlID="TbDateDebut">
                                </asp:CalendarExtender>
                                <asp:CompareValidator ID="CompareValidator4" runat="server" ValidationGroup="validation"
                                    Type="Date" Operator="DataTypeCheck" ControlToValidate="TbDateDebut" ErrorMessage=" * Date Invalide"
                                    ForeColor="Red">
                                </asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                Date fin validité
                            </td>
                            <td>
                                <asp:TextBox ID="TbDateFin" runat="server" Width="206px" Enabled="False" CssClass="text"></asp:TextBox>
                                <asp:CalendarExtender ID="TbDateFin_CalendarExtender" runat="server" Enabled="True"
                                    TargetControlID="TbDateFin">
                                </asp:CalendarExtender>
                                <asp:CompareValidator ID="CompareValidator5" runat="server" ValidationGroup="validation"
                                    Type="Date" Operator="DataTypeCheck" ControlToValidate="TbDateFin" ErrorMessage=" * Date Invalide"
                                    ForeColor="Red">
                                </asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                Groupe
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLService" runat="server" Width="210px" CssClass="text" AutoPostBack="True"
                                    Enabled="False">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                Droit de pointage
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLDroit" runat="server" Width="210px" CssClass="text" AutoPostBack="True"
                                    Enabled="False">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <asp:CheckBox ID="CbBadge" runat="server" Text="Autoriser badge" AutoPostBack="true"
                                    OnCheckedChanged="CbBadge_CheckedChanged" Enabled="False" Visible="False" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" style="padding-top: 20px;">
                                N° Permis Confiance *
                                <asp:RequiredFieldValidator ValidationGroup="validation" runat="server" ControlToValidate="DDLBadge"
                                    ErrorMessage="champ obligatoire" Display="None" ID="RequiredFieldValidator4"></asp:RequiredFieldValidator>
                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                    TargetControlID="RequiredFieldValidator4">
                                </asp:ValidatorCalloutExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSearch" runat="server" Width="206px" CssClass="text" onkeyup="FilterItems(this.value)"></asp:TextBox><br />
                                <asp:DropDownList ID="DDLBadge" runat="server" Width="210px" Enabled="False" CssClass="text">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Ancien N° Permis
                            </td>
                            <td>
                                <asp:TextBox ID="TbAncienNP" runat="server" Width="206px" Enabled="False" CssClass="text"></asp:TextBox>
                                <AjaxToolkit:FilteredTextBoxExtender ID="ftbe13" runat="server" TargetControlID="TbAncienNP"
                                    ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890/ " />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Observations
                            </td>
                            <td>
                                <asp:TextBox ID="TbObservation" runat="server" Height="100px" Width="205px" CssClass="textplaceholder"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="RbFlagAutorise" />
            <asp:AsyncPostBackTrigger ControlID="BtnSet" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divImage" style="width: 30%; float: left;">
        <div style="text-align: left;">
            <fieldset id="fieldset3" style="width: 90%; text-align: center">
                <legend>Photo du Chauffeur</legend>
                <asp:Image ID="photo" runat="server" Height="370px" ImageAlign="Middle" Width="250px" />
            </fieldset>
        </div>
        <!--endprint-->
        <fieldset id="fieldset4" class="login" style="width: 90%;">
            <legend>Propriétés Fiche Chauffeur</legend>
            <table>
                <tr>
                    <td class="style2">
                        Date de création
                    </td>
                    <td>
                        <asp:TextBox ID="TbDateCreation" runat="server" ReadOnly="True" Width="130px" CssClass="text"
                            Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Date de modification
                    </td>
                    <td>
                        <asp:TextBox ID="TbDateModif" runat="server" ReadOnly="True" Width="130px" CssClass="text"
                            Enabled="False"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
        <asp:UpdatePanel ID="CommandesPanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <fieldset class="login" style="width: 90%;">
                    <legend>Commandes</legend>
                    <table>
                        <tr>
                            <td runat="server" id="TdSet">
                                <asp:Button ID="BtnSet" runat="server" Text="Modifier" CssClass="button" OnClick="BtnSet_Click" />
                                <asp:Button ID="BtnBack" runat="server" Text="Retour" CssClass="button" OnClick="BtnBack_Click" />
                                <asp:Button ID="BtPrint" runat="server" CssClass="button" Text="Imprimer" OnClientClick="doPrint();" /><%--  date: 19/01/2017 par Zouhair LOUALID  Ajouter clique coté client pour impression de la fiche chauffeur--%>
                            </td>
                            <td runat="server" id="TdSave">
                                <asp:Button ID="BtnSave" runat="server" Text="Enregistrer" CssClass="button" ValidationGroup="validation"
                                    OnClick="BtnSave_Click" />
                                <asp:Button ID="BtnCancel" runat="server" CssClass="button" Text="Annuler" OnClick="BtnBack_Click" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="BtnSave" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
