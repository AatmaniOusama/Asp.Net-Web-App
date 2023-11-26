<%@ page title="Listes" language="C#" masterpagefile="~/Interfaces/Shared/MasterPage.master" autoeventwireup="true" inherits="Listes_index, App_Web_qjoeb4aj" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    

    <style type="text/css">
        .style1
        {
            width: 294px;
        }
    </style>
   
     
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server" >

            <asp:Table ID="BarreControleListe" runat="server" CellPadding="5" style="margin-left: 1200px"  >
            <asp:TableRow VerticalAlign="Bottom" HorizontalAlign="Right">

             <asp:TableCell>
                <asp:ImageButton ID="BtnAddListe" ImageUrl="~/Icons/Add_Listes.png" CssClass="button" runat="server" ToolTip="Ajouter"   />
                   <asp:ModalPopupExtender ID="MPE" runat="server"  
                TargetControlID="BtnAddListe"  
                PopupControlID="Panel_Ajout"  
                DropShadow="true"
                BackgroundCssClass="ModalPopupBG"
                OnOkScript="CloseScript()"
                CancelControlID="BtCancel">              
                </asp:ModalPopupExtender>
            </asp:TableCell>
             
       
             <asp:TableCell>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  >
                <ContentTemplate>
                <asp:ImageButton ID="BtnSetListe" ImageUrl="~/Icons/Set_Listes.jpg" CssClass="button" runat="server" ToolTip="Modifier"  OnClick="BtnSetListe_Click"/>
                </ContentTemplate>
                <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="GridViewListeListes" />
                </Triggers>
            </asp:UpdatePanel>

            </asp:TableCell>
            
                      
             <asp:TableCell>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional"  >
                <ContentTemplate>
                <asp:ImageButton ID="BtnDeleteListe" ImageUrl="~/Icons/Delete_Listes.png" runat="server" CssClass="button" ToolTip="Supprimer" OnClick="BtnDeleteListe_Click" OnClientClick="return confirm('Etes-vous sûr que vous voulez supprimer cette liste ?');"  />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridViewListeListes" />                  
                </Triggers>
            </asp:UpdatePanel>

            </asp:TableCell>

             

            </asp:TableRow>
            </asp:Table>
      

    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional" >
    <ContentTemplate>
    

  
   

        <asp:GridView ID="GridViewListeListes" 
        CssClass="GridViewStyle" 
        runat="server"
        AutoGenerateColumns="False"  
        ShowFooter="false"   
        FooterStyle-Height="7%" 
        ShowHeaderWhenEmpty="true"
        AllowSorting="True"
        
        AllowPaging="true"  
        PageSize="10" 
        
       
        OnPageIndexChanging="GridViewListeListes_PageIndexChanging" 
        onrowdatabound="GridViewListeListes_RowDataBound" 
        onselectedindexchanging="GridViewListeListes_SelectedIndexChanging">
         
   
         
        <FooterStyle CssClass="GridViewFooterStyle" />
        <RowStyle CssClass="GridViewRowStyle" />    
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <HeaderStyle CssClass="GridViewHeaderStyle" />

 
                <Columns>
                   
                    <asp:ButtonField  Text="" CommandName="Select"  HeaderStyle-Width="0px" HeaderStyle-BackColor="White" HeaderStyle-BorderColor="White" ItemStyle-Width="0px" ItemStyle-BackColor="White" ItemStyle-BorderColor="White"  />
            
                    <asp:TemplateField HeaderText="Abréviation liste  " SortExpression="Abrev" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="20%" HeaderStyle-Width="20%">
                        <ItemTemplate>
                             <asp:Label ID="LblNumList" runat="server" Text='<%#bind("Num_liste") %>' Visible="false"></asp:Label>
                            <asp:Label ID="LblAbrev" runat="server" Text='<%#bind("Abrev") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Libellé liste  "  SortExpression="Libelle" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40%" HeaderStyle-Width="40%">
                        <ItemTemplate>
                            <asp:Label ID="LblLibelle" runat="server"  Text='<%#bind("Libelle") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField  HeaderText="Type liste  " SortExpression="Type" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40%" HeaderStyle-Width="40%" >
                        <ItemTemplate>
                            <asp:Label ID="LblType" runat="server" Text='<%#bind("Type") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
        
                </Columns>
            </asp:GridView>

   


    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="BtSave" />
        <asp:AsyncPostBackTrigger ControlID="BtnDeleteListe" />
        <asp:AsyncPostBackTrigger ControlID="GridViewListeListes"  />    
    </Triggers>
    </asp:UpdatePanel>


    
      <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional"  >
        <ContentTemplate>
     <asp:Panel  id="PanelBarreControleListe" runat="server" Visible="false"  style="width:100%;height:95px;" >
            <asp:Literal ID="Literal_MsgBox" runat="server"></asp:Literal>
 <fieldset  style="width:98%;height:40%;">
    <asp:Table ID="BarreControleListeMembres" runat="server"   CellPadding="5">
           
           
            <asp:TableRow VerticalAlign="Bottom">      
           
            <asp:TableCell>
                <asp:ImageButton ID="BtnAddListeMembres" ImageUrl="~/Icons/AddToList.png" runat="server" CssClass="button" ToolTip="Ajouter membre à  la liste sélectionée"      OnClick="BtnAddUserToList_Click"  />     
            </asp:TableCell>
        

            <asp:TableCell>
             <asp:TextBox ID="TbNumPermis"  runat="server" CssClass="text"  onfocus="if (this.value == 'N° Permis') this.value = '';" onblur="if (this.value == '') this.value = 'N° Permis';" value="N° Permis" style="border-top-left-radius: 20px; border-top-right-radius: 20px;border-bottom-left-radius: 20px;border-bottom-right-radius: 20px; color:Gray; text-align:center;" xmlns:asp="#unknown"></asp:TextBox>
           </asp:TableCell>

            <asp:TableCell>
             <asp:TextBox ID="TbMatricule"  runat="server" CssClass="text"  onfocus="if (this.value == 'Matricule') this.value = '';" onblur="if (this.value == '') this.value = 'Matricule';" value="Matricule" style="border-top-left-radius: 20px; border-top-right-radius: 20px;border-bottom-left-radius: 20px;border-bottom-right-radius: 20px; color:Gray; text-align:center;" xmlns:asp="#unknown"></asp:TextBox>
           </asp:TableCell>

            <asp:TableCell>
              <asp:TextBox ID="TbNom" runat="server" CssClass="text"  onfocus="if (this.value == 'Nom') this.value = '';" onblur="if (this.value == '') this.value = 'Nom';" value="Nom" style="border-top-left-radius: 20px; border-top-right-radius: 20px;border-bottom-left-radius: 20px;border-bottom-right-radius: 20px; color:Gray; text-align:center;" xmlns:asp="#unknown" ></asp:TextBox>
           </asp:TableCell>

            <asp:TableCell>
              <asp:TextBox ID="TbPrenom" runat="server" CssClass="text"  onfocus="if (this.value == 'Prénom') this.value = '';" onblur="if (this.value == '') this.value = 'Prénom';" value="Prénom" style="border-top-left-radius: 20px; border-top-right-radius: 20px;border-bottom-left-radius: 20px;border-bottom-right-radius: 20px; color:Gray; text-align:center;" xmlns:asp="#unknown" ></asp:TextBox>
           </asp:TableCell>

            <asp:TableCell>
                   <asp:TextBox ID="TbMotif" runat="server" CssClass="text"  onfocus="if (this.value == 'Motif demande') this.value = '';" onblur="if (this.value == '') this.value = 'Motif demande';" value="Motif demande" style="border-top-left-radius: 20px; border-top-right-radius: 20px;border-bottom-left-radius: 20px;border-bottom-right-radius: 20px; color:Gray; text-align:center;" xmlns:asp="#unknown" ></asp:TextBox>
           </asp:TableCell>


               <asp:TableCell>
                   <asp:ImageButton   ID="BtnSearch" ImageUrl="~/Icons/Find.png" CssClass="button"  ToolTip="Supprimer" runat="server"  OnClick="BtSearch" ></asp:ImageButton>
           </asp:TableCell>



                <asp:TableCell>

                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional"  >
                <ContentTemplate>
               <asp:TextBox ID="NbrLignes" runat="server" BackColor="Black" Font-Bold="True" AutoPostBack="True" ReadOnly="true"  CssClass="text"                             ForeColor="White" Height="20px" style="margin-left: 108px" Width="50px">
                             </asp:TextBox>&nbsp; 
                             Nb Personnes
             </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridViewListeListes"  />   
                    <asp:AsyncPostBackTrigger ControlID="GridViewListeMembres"  />  
                </Triggers>
            </asp:UpdatePanel>


            </asp:TableCell>
            </asp:TableRow>
                        
            </asp:Table>

 </fieldset>
      </asp:Panel>  
     </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridViewListeListes" EventName="SelectedIndexChanging" />       
                     
                </Triggers>
     </asp:UpdatePanel>

     

    <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional" >
    <ContentTemplate>
     

        <asp:GridView ID="GridViewListeMembres" 
        CssClass="GridViewStyle" 
        runat="server"
        AutoGenerateColumns="False"  
        ShowFooter="false"   
       
        ShowHeaderWhenEmpty="true"
        AllowSorting="True"
        AllowPaging="true"  
        PageSize="15"


       
            OnRowDeleting="GridViewListeMembres_RowDeleting"
            OnPageIndexChanging="GridViewListeMembres_PageIndexChanging"
            
           
         >
        
      
        <FooterStyle CssClass="GridViewFooterStyle" />
        <RowStyle CssClass="GridViewRowStyle" />    
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <HeaderStyle CssClass="GridViewHeaderStyle" />

 
                <Columns>
                   
            <asp:TemplateField  ItemStyle-HorizontalAlign="Center"   ItemStyle-Width="10%">
        
            <ItemTemplate >               
               <asp:ImageButton   ID="ImgDelete" ImageUrl="~/Icons/delete00.jpg"  CssClass="button" ToolTip="Supprimer" runat="server" CausesValidation="false" CommandName="Delete"  CommandArgument='<%#bind("Matricule") %>'   OnClientClick="return confirm('Etes-vous sûr que vous voulez supprimer ce membre de la liste ?');" ></asp:ImageButton>
            </ItemTemplate>
           
            
        </asp:TemplateField>

                    <asp:TemplateField HeaderText="N° Permis  " SortExpression="NumPermis" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="15%" >
                        <ItemTemplate>
                            <asp:Label ID="LblNumPermis" runat="server" Text='<%#bind("NumBadge") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="CIN  " SortExpression="Matricule" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="15%" >
                        <ItemTemplate>
                            <asp:Label ID="LblMatricule" runat="server" Text='<%#bind("Matricule") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Nom "  SortExpression="Nom"   ItemStyle-Width="15%" >
                        <ItemTemplate>
                            <asp:Label ID="LblNom" runat="server"  Text='<%#bind("Nom") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Prénom "  SortExpression="Prenom"  ItemStyle-Width="15%" >
                        <ItemTemplate>
                            <asp:Label ID="LblPrenom" runat="server"  Text='<%#bind("Prenom") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField    HeaderText="Motif demande  " SortExpression="Motif" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="30%" >
                        <ItemTemplate>
                            <asp:Label ID="LblMotif" runat="server" Text='<%#bind("Motif") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
        
                </Columns>
            </asp:GridView>

  

    </ContentTemplate>
    <Triggers>  
     
        <asp:AsyncPostBackTrigger ControlID="GridViewListeListes" EventName="SelectedIndexChanging" />
        <asp:AsyncPostBackTrigger ControlID="GridViewListeMembres"  />    
        <asp:AsyncPostBackTrigger ControlID="BtnSearch"  />   
                     
    </Triggers>
    </asp:UpdatePanel>

    

    <asp:Panel ID="Panel_Ajout" CssClass="ModalPopupBG" runat="server" BackColor="Black" >
    <div class="popup_Container">
        <div class="popup_Titlebar" id="PopupHeader">
            <div class="TitlebarLeft">
                Ajouter une Liste </div>
            <div class="TitlebarRight" onclick="$get('MainContent_BtCancel').click();" >
            </div>
        </div>
        <div class="popup_Body">
        <asp:Table ID="Table_Ajout" runat="server">

        <asp:TableRow>
            <asp:TableCell>
                Abréviation Liste &nbsp;
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="TbAbrevListe" runat="server" Width="200px" CssClass="text"></asp:TextBox>
                  <asp:RequiredFieldValidator ValidationGroup="Ajouter" runat="server" ControlToValidate="TbAbrevListe" 
                    ErrorMessage="champ obligatoire" Display="None" ID="RequiredFieldValidator1" ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1"
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                </asp:ValidatorCalloutExtender>
            </asp:TableCell>
        </asp:TableRow> 
             
        <asp:TableRow >
           <asp:TableCell>
                    Libellé Liste
                </asp:TableCell>
           <asp:TableCell>                   
              <asp:TextBox ID="TbLibelleListe" runat="server" Width="200px"  ReadOnly="false" CssClass="text" ></asp:TextBox>   
               <asp:RequiredFieldValidator ValidationGroup="Ajouter" runat="server" ControlToValidate="TbLibelleListe" 
                    ErrorMessage="champ obligatoire" Display="None" ID="RequiredFieldValidator3" ></asp:RequiredFieldValidator>
                <asp:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender"
                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                </asp:ValidatorCalloutExtender>
        </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
                    <asp:TableCell>
            TypeListe
        </asp:TableCell>
                    <asp:TableCell>
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional" >
                                <ContentTemplate>
                                    <asp:DropDownList ID="DDLTypeListe" runat="server"   Enabled="false" Width="200px" CssClass="text"  >
                                    <asp:ListItem Text="Chauffeurs" Value="1" ></asp:ListItem>
                                   <asp:ListItem Text="Opérateurs" Value="2" ></asp:ListItem>
                                   <asp:ListItem Text="Agents" Value="4" ></asp:ListItem>
                                   <asp:ListItem Text="Chauffeurs demandés" Value="8"  Selected="True" ></asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GridViewListeListes" />
                                </Triggers>
                        </asp:UpdatePanel>    
                    </asp:TableCell>
        </asp:TableRow>
       <asp:TableRow style=" height:10px;"></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                <asp:Button ID="BtSave" runat="server" CssClass="button" Text="Enregistrer" ValidationGroup="Ajouter" OnClick="BtSave_Click" />
                <asp:Button ID="BtCancel" runat="server" CssClass="button"  Text="Annuler" OnClick="BtCancel_Click" />
            </asp:TableCell>
        </asp:TableRow>

        </asp:Table>
        </div>
    </div>
    </asp:Panel>

    
</asp:Content>

