<%@ Page Language="C#" MasterPageFile="~/Interfaces/Shared/MasterPage.master" AutoEventWireup="true" CodeFile="DetailCarteGrise.aspx.cs" Inherits="Interfaces_DetailCarteGrise" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  >
    <ContentTemplate>
    <div Id="Div1" runat="server" style="margin-left:450px;">
    <fieldset class="login" style="width:438px; height:297px;">
    <legend>Carte grise Véhicules</legend>
    <table>
       
            <tr>
             
            <td class="style1">N° Immatriculation
            <asp:RequiredFieldValidator ValidationGroup="validation" runat="server" ControlToValidate="TbNumImmatVehicule" 
                    ErrorMessage="champ obligatoire" Display="None" ID="RequiredFieldValidator6" ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3"
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator6">
                </asp:ValidatorCalloutExtender>
            </td>
            <td >
                <asp:TextBox ID="TbNumImmatVehicule" runat="server" Width="200px" CssClass="text"></asp:TextBox>
                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TbNumImmatVehicule" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-" />
            </td>
            </tr> 
               
            <tr>
            <td class="style1">Date Immatriculation         
            </td>
             <td>       
             
             
               <asp:TextBox ID="TbDateImmat" runat="server" Width="200px" CssClass="text" ></asp:TextBox>
                    <asp:CalendarExtender ID="TbDateImmat_CalendarExtender" runat="server" 
                        Enabled="True" TargetControlID="TbDateImmat">
                    </asp:CalendarExtender>
                      <asp:CompareValidator ValidationGroup="validation" runat="server" ControlToValidate="TbDateImmat" Type="Date" Operator="DataTypeCheck"
                                    ErrorMessage="Format incorrect" Display="None" ID="CompareValidator2" ></asp:CompareValidator>
                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" 
                                    runat="server" Enabled="True" TargetControlID="CompareValidator2">
                                </asp:ValidatorCalloutExtender>
                
               </td>
            </tr>
            
            <tr>
            <td class="style1">
            Date de mise en circulation         
            </td>
             <td>  
             <asp:TextBox ID="TbDateMiseEnCirculation" runat="server" Width="200px" CssClass="text" ></asp:TextBox>
                    
                    <asp:CalendarExtender ID="TbDateMiseEnCirculation_CalendarExtender1" runat="server" 
                        Enabled="True" TargetControlID="TbDateMiseEnCirculation">
                    </asp:CalendarExtender>
                                        
                    <asp:CompareValidator ValidationGroup="validation" runat="server" ControlToValidate="TbDateMiseEnCirculation" Type="Date" Operator="DataTypeCheck"
                                    ErrorMessage="Format incorrect" Display="None" ID="CompareValidator1" ></asp:CompareValidator>
                                <asp:ValidatorCalloutExtender ID="CompareValidator1_ValidatorCalloutExtender" 
                                    runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                </asp:ValidatorCalloutExtender>
         
               </td>
            </tr>
               <tr>
                <td class="style1">
                    Marque Véhicule
                   
                </td>
                <td>
                    <asp:TextBox ID="TbMarque" runat="server" Width="200px" CssClass="text" ></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TbMarque" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 " />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Modèle Véhicule
                   
                </td>
                <td>
                    <asp:TextBox ID="TbModele" runat="server" Width="200px" CssClass="text" ></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="TbModele" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 -" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Nom Propriétaire
                  
                </td>
                <td> 
                    <asp:TextBox ID="TbNomProprietaire" runat="server" Width="200px" CssClass="text"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="TbNomProprietaire" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                </td>
            </tr>  
            <tr>
                <td class="style1">
                    Prénom Propriétaire
                   
                </td>
                <td> 
                    <asp:TextBox ID="TbPrenomProprietaire" runat="server" Width="200px" CssClass="text"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="TbPrenomProprietaire" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />

                </td>
            </tr>
            
            <tr>
                <td class="style1">
                    Cin Propriétaire
                  
                </td>
                <td>
                    <asp:TextBox ID="TbCinProprietaire" runat="server" Width="200px" CssClass="text"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="TbCinProprietaire" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 " />

                </td>
            </tr>   

    </table>
     <table style=" height:10px; margin-left:150px;" >

    <tr  style=" height:20px; "></tr>
                <tr >

        
        
        <td runat="server" id="TdSet">
            <asp:Button ID="BtnSaveCarteGrise" runat="server" Text="Enregistrer" ValidationGroup="validation"   CssClass="button"  OnClick="BtnSaveCarteGriseClick" />
            <asp:Button ID="BtnCancel" runat="server" Text="Annuler" CssClass="button" OnClick="BtnBack_Click"  />
        </td>
                      
        </tr>
        </table>


    </fieldset>
     </div>

    
    </ContentTemplate>
    <Triggers>      
        <asp:AsyncPostBackTrigger ControlID="BtnSaveCarteGrise" />
        <asp:PostBackTrigger ControlID="BtnCancel" />
       
    </Triggers>
</asp:UpdatePanel>





</asp:Content>

