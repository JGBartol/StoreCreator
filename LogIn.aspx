<%@ Page Title="" Language="C#" MasterPageFile="~/LeftNavBar.master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="StoreCreator.LogIn" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    

     <div class="container"  >

                  <div class="row">

                                           <div class="col-sm-4">
                            
                                           </div>

                    <div class="col-sm-4">

                         <div class="well well-sm"  style="color:#669999;  border:3px #e5e5e5 solid; margin-left:auto; margin-right:auto; text-align:center; padding-top:5px; padding-bottom:5px;   ">
       

        
                             <h3 style="text-align:center; color:#669999">Sign In</h3>

                             
                                  <asp:Panel ID="pnlLoginIsSuccessful" runat="server" BorderColor="DarkGreen">

                                        <div class="alert alert-success" style="text-align:center;">
                                                 <strong>Login was successful!</strong> 
                                        </div>

                                  </asp:Panel>
                                     <asp:Panel ID="pnlForgotPassWordSend" runat="server" BorderColor="DarkGreen">

                                        <div class="alert alert-success" style="text-align:center;">
                                                 <strong>Temporary PassWord Was Sent To <asp:Label ID="lblUserEmail" runat="server" ></asp:Label></strong> 
                                        </div>

                                  </asp:Panel>

                                <asp:Panel ID="pnlLoginIsNotSuccessful" runat="server" BorderColor="DarkRed">

                                               <div class="alert alert-danger" style="text-align:center;">
                                                  <strong>
                                                      <asp:Label ID="lblStatus" runat="server"  ></asp:Label>

                                                  </strong> 
                                             </div>

                                   </asp:Panel>


                            <hr style="color:#669999; border-color:#669999;" />
     

                        <b style="float:left;">User Name:<span style="color:red;">*</span> </b>
                        <asp:TextBox ID="txtUserName" runat="server"  CssClass="form-control" MaxLength="50" BorderWidth="1" Font-Bold="true" ValidationGroup="ForgetPassword" ></asp:TextBox>
                                                
                                                        <asp:RequiredFieldValidator ID="valReqUserName" runat="server" ErrorMessage="UserName Is Required" ControlToValidate="txtUserName" ForeColor="Red" SetFocusOnError="true"  ></asp:RequiredFieldValidator>  
                 <br />

                     <b style="float:left;">PassWord:<span style="color:red;">*</span></b>
                        <asp:TextBox ID="txtPassWord" runat="server"   TextMode="Password"  CssClass="form-control"  MaxLength="25" BorderWidth="1"  Font-Bold="true"   ></asp:TextBox>
              
                                                        <asp:RequiredFieldValidator ID="valReqTxtPassWord" runat="server" ErrorMessage="PassWord Is Required" ControlToValidate="txtPassWord" ForeColor="Red"></asp:RequiredFieldValidator>    <br />                                                                               
                                                        <asp:RequiredFieldValidator ID="reqUserNameForPassWordReset" runat="server" ErrorMessage="UserName Is Required For PassWord Reset" ControlToValidate="txtUserName" ForeColor="Red" SetFocusOnError="true"  ValidationGroup="ForgetPassword"></asp:RequiredFieldValidator>  
                 <br />

                        

                
                 
              

               <div style="text-align:center;">

                    <asp:Button ID="btnLogIn" runat="server"  OnClick="btnLogIn_Click"  Text="Log In" CssClass="btn-sm"  BorderColor="Black" BorderWidth="1" Font-Bold="true"  BackColor="#669999" ForeColor="White"  Width="100%" />

                   <br />

                   </div>

                Remember Me
                   <asp:CheckBox ID="chkRememberMe" runat="server"  />
                   <br />


                   <asp:LinkButton ID="lbForgetPassWord" runat="server"   PostBackUrl="~/ForgotPassWord.aspx" CausesValidation="false">Forget Password?</asp:LinkButton>
                  
             
                                   

          
       


</div>
        </div>
         </div>
    

                                            

                         <div class="col-sm-4">

      </div>

         </div>


</asp:Content>
