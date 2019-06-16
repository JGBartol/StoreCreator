<%@ Page Title="" Language="C#" MasterPageFile="~/LeftNavBar.master" AutoEventWireup="true" CodeBehind="CookiesDisabled.aspx.cs" Inherits="StoreCreator.CookiesDisabled" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="container"  >

                     <div class="row">

                                           <div class="col-sm-4">
                            



                                           </div>
                                        
                                        <div class="col-sm-4">

                                               <div class="well well-sm"  style="color:#669999;  border:3px #e5e5e5 solid; margin-left:auto; margin-right:auto; text-align:LEFT; padding-top:5px; padding-bottom:5px;   ">
       

    
                                                    <asp:Panel ID="pnlCookiesDisabled" runat="server" BorderColor="DarkRed">

                                                               <div class="alert alert-danger" style="text-align:center;">
                                                                  <strong>Cookies Are Disabled. Must Have Cookies Enabled To Use This Site</strong> 
                                                             </div>

                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlBrowser" runat="server" BorderColor="DarkRed">

                                                               <div class="alert alert-danger" style="text-align:center;">
                                                                  <strong><asp:Label ID="lblBrowser" runat="server" ></asp:Label> Does Not Support Cookies. Must Have Cookies Enabled To Use This Site</strong> 
                                                             </div>

                                                    </asp:Panel>

                                                   </div>
                                            </div>
                                              <div class="col-sm-4">
                            



                                           </div>
                                        
                         </div>
         </div>



</asp:Content>
