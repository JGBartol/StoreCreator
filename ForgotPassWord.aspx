<%@ Page Title="" Language="C#" MasterPageFile="~/LeftNavBar.master" AutoEventWireup="true" CodeBehind="ForgotPassWord.aspx.cs" Inherits="StoreCreator.ForgotPassWord" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container"  >

                     <div class="row">

                                           <div class="col-sm-4">
                            



                                           </div>
                                        
                                        <div class="col-sm-4">

                                               <div class="well well-sm"  style="color:#669999;  border:3px #e5e5e5 solid; margin-left:auto; margin-right:auto; text-align:LEFT; padding-top:5px; padding-bottom:5px;   ">
       
        
                                             <h3 style="text-align:center; color:#669999">Forgot PassWord</h3>
                                                      <hr style="color:#669999; border-color:#669999;" />

                                                    <asp:Panel ID="pnlUserNameNotFound" runat="server" BorderColor="DarkRed">

                                                               <div class="alert alert-danger" style="text-align:center;">
                                                                  <strong>UserName Not Found</strong> 
                                                             </div>

                                                    </asp:Panel>
                                                          <asp:Panel ID="pnlRegisterIsSuccessful" runat="server" BorderColor="DarkGreen">

                                                                <div class="alert alert-success" style="text-align:center;">
                                                                         <strong>Password sent to <asp:Label ID="lblUserEmail" runat="server" ></asp:Label>!</strong> 
                                                                </div>

                                                          </asp:Panel>

                                                   Enter UserName:
                                              <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>  
                                           <asp:RequiredFieldValidator ID="reqUserName" runat="server" ErrorMessage="User Name Is Required" ControlToValidate="txtUserName" ForeColor="Red"></asp:RequiredFieldValidator>
  

                                        <asp:Button ID="btnSendEmailLink" runat="server"  OnClick="btnSendResetLink_Click"  Text="Send Password Reset Link" CssClass="btn-sm" Width="100%"  BorderColor="#669999" BorderWidth="1" Font-Bold="true"  BackColor="#669999" ForeColor="White"  />
                                                                                           


                                         </div>
                                  
                                            </div>

                                         <div class="col-sm-4">




                                         </div>
                          </div>
        </div>

</asp:Content>
