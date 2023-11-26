
<%@ Page Title="" Language="C#" MasterPageFile="~/Interfaces/Shared/MasterPage.master" AutoEventWireup="true" CodeFile="DetailQPU.aspx.cs" Inherits="Interfaces_User_DetailQPU" %>
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

    <div Id="Div1" runat="server" style="  margin-left:450px; " >
        <fieldset class="login" style="width:438px; height:500px;">
        <legend>Autorisation Quiter Périmètre Urbain</legend>

     <table>
            <tr>
            <td class="style1">Date Début Autorisation    
            
                 <asp:RequiredFieldValidator ValidationGroup="validation" Display="None" 
                    ID="RequiredFieldValidator5" runat="server" ControlToValidate="TbDateDebutAQP" 
                    ErrorMessage="champ obligatoire" ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" 
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                </asp:ValidatorCalloutExtender>      
            </td>
             <td>       
             
             
               <asp:TextBox ID="TbDateDebutAQP" runat="server" Width="200px"></asp:TextBox>
                    <asp:CalendarExtender ID="TbDateDebutAQP_CalendarExtender" runat="server" 
                        Enabled="True" TargetControlID="TbDateDebutAQP">
                    </asp:CalendarExtender>
                    
                
               </td>
            </tr>
            <tr>
            <td class="style1">Date Fin Autorisation  
            
             <asp:RequiredFieldValidator ValidationGroup="validation" Display="None" 
                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="TbDateFinAQP" 
                    ErrorMessage="champ obligatoire" ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" 
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                </asp:ValidatorCalloutExtender>       
            </td>
             <td>       
             
             
               <asp:TextBox ID="TbDateFinAQP" runat="server" Width="200px"></asp:TextBox>
                    <asp:CalendarExtender ID="TbDateFinAQP_CalendarExtender" runat="server" 
                        Enabled="True" TargetControlID="TbDateFinAQP">
                    </asp:CalendarExtender>
                    
                
               </td>
            </tr>
            <tr>
             
            <td class="style1">N° Autorisation

               <asp:RequiredFieldValidator ValidationGroup="validation" Display="None" 
                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="TbNumAutorisation" 
                    ErrorMessage="champ obligatoire" ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" 
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                </asp:ValidatorCalloutExtender>
            </td>
            <td >
                <asp:TextBox ID="TbNumAutorisation" runat="server" Width="200px"></asp:TextBox>
                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TbNumAutorisation" ValidChars="1234567890" />
            </td>
            </tr> 
            <tr>
             
            <td class="style1">N° Agrement

                <asp:RequiredFieldValidator ValidationGroup="validation" Display="None" 
                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="TbNumAgrement" 
                    ErrorMessage="champ obligatoire" ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" 
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                </asp:ValidatorCalloutExtender>
                

            </td>
            <td >
                <asp:TextBox ID="TbNumAgrement" runat="server" Width="200px"></asp:TextBox>
                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TbNumAgrement" ValidChars="1234567890" />
            </td>
            </tr>
            <tr>
             
            <td class="style1">Destination
               <asp:RequiredFieldValidator ValidationGroup="validation" Display="None" 
                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="TbDestination" 
                    ErrorMessage="champ obligatoire" ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender" 
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                </asp:ValidatorCalloutExtender>

            </td>
            <td >
                <asp:TextBox ID="TbDestination" runat="server" Width="200px"></asp:TextBox>
                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="TbDestination" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
            </td>
            </tr>   
                             
            <tr>
            <td>
                <asp:RadioButtonList ID="Rbchoix" runat="server" AutoPostBack="true"
                    RepeatDirection="Horizontal" TextAlign="Left" Width="278px"
                    onselectedindexchanged="Rbchoix_SelectedIndexChanged">
                    <asp:ListItem Selected="true" Value="Liste">Liste</asp:ListItem>
                    <asp:ListItem Value="Chauffeur">Chauffeur</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>

            <tr>
            <td class="style1">Listes </td>
            <td>

                <asp:DropDownList ID="DDLListes" runat="server" Width="200px"  AutoPostBack="true" Enabled="true">
                </asp:DropDownList>

            </td>
        </tr>
            <tr>
            <td class="style1">Chauffeur </td>
            <td>
              <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                <asp:DropDownList ID="DDLChauffeur" AutoPostBack="true" Enabled="false" runat="server" Width="206px"  onselectedindexchanged="DDLChauffeur_SelectedIndexChanged">
                </asp:DropDownList>
                </ContentTemplate>
                <Triggers>              
              
                <asp:AsyncPostBackTrigger ControlID="TbMatricule" />              
                </Triggers>
            </asp:UpdatePanel> 
            </td>
        </tr>

            <tr>
                <td class="style1">
                    Matricule chauffeur         
                </td>
                <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <asp:TextBox ID="TbMatricule" runat="server"   Enabled="false" AutoPostBack="true" Width="206px"  OnTextChanged="TbMatricule_TextChanged"></asp:TextBox>               
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="TbMatricule" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890" />
                </ContentTemplate>
                <Triggers>                
               
                <asp:AsyncPostBackTrigger ControlID="DDLChauffeur" />   
                </Triggers>
                </asp:UpdatePanel> 
                </td>
        </tr>
            <tr>
                <td class="style1">
                    Nom Passager 1         
                </td>
                <td>
                    <asp:TextBox ID="TbNomPassager1" runat="server" Width="200px"></asp:TextBox>
                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="TbNomPassager1" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Prénom Passager 1                 
                </td>
                <td>
                    <asp:TextBox ID="TbPrenomPassager1" runat="server" Width="200px"></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="TbPrenomPassager1" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    CIN Passager 1
                </td>
                <td>
                    <asp:TextBox ID="TbCINPassager1" runat="server" Width="200px"></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="TbCINPassager1" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Nom Passager 2                               
               </td>
                <td>
                    <asp:TextBox ID="TbNomPassager2" runat="server" Width="200px"></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="TbNomPassager2" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Prénom Passager 2                                        
                </td>
                <td>
                    <asp:TextBox ID="TbPrenomPassager2" runat="server" Width="200px"></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="TbPrenomPassager2" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    CIN Passager 2                                       
                </td>
                <td>
                    <asp:TextBox ID="TbCINPassager2" runat="server" Width="200px"></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="TbCINPassager2" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890" />
                </td>
            </tr>
            
                          
    </table>
             <table>

        <tr style="height :30px; margin-left:70px; "></tr>
        <tr>
        <td runat="server" id="TdSet">
            <asp:Button ID="BtnSaveAQP" runat="server" Text="Enregistrer"  OnClick="BtnSaveAQPClick" />
            <asp:Button ID="BtnCancel" runat="server" Text="Annuler" OnClick="BtnBack_Click"  />
        </td>
                      
        </tr>
        </table>

        </fieldset>
  </div>








</asp:Content>

