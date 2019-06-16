<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlLogin.ascx.cs" Inherits="ctrlLogin" %>

 
    
        <h3 style="text-align:center; color:red;">Must Be Logged In</h3>
            
<div class="form-inline" role="form">
  <div class="form-group">

            UserName:
            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
            
              PassWord:
              <asp:TextBox ID="txtPassWord" runat="server" CssClass="form-control"></asp:TextBox>
                  <asp:Button ID="btnLogIn" runat="server" Text="Log In" ValidationGroup="ctrlLogIn" OnClick="btnLogIn_Click" CssClass="form-control"  BorderColor="#669999" BorderWidth="1" Font-Bold="true"  BackColor="#669999" ForeColor="White" />
    
 </div>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="PassWord Is Required" ForeColor="Red" ControlToValidate="txtPassWord" SetFocusOnError="true" ValidationGroup="ctrlLogIn"></asp:RequiredFieldValidator>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="UserName Is Required" ForeColor="Red" ControlToValidate="txtUserName" SetFocusOnError="true" ValidationGroup="ctrlLogIn"></asp:RequiredFieldValidator>
                 
            
                <asp:Label ID="lblStatus" runat="server" ></asp:Label>

    </div>