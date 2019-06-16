<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEndMainNav.Master" AutoEventWireup="true" CodeBehind="PassWordReset.aspx.cs" Inherits="StoreCreator.PassWordReset" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
                    <div class="well well-lg">


                        <asp:Panel ID="pnlResetPassWordTextBoxes" runat="server">

                            Temporary PassWord:
                            <asp:TextBox ID="txtTempPass" runat="server"></asp:TextBox>    

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Temporary PassWord Is Required" ControlToValidate="txtTempPass" ForeColor="Red"></asp:RequiredFieldValidator>               
                            
                                                              
                            New PassWord:

                            <asp:TextBox ID="txtNewPass" runat="server"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="New PassWord Is Required" ControlToValidate="txtNewPass" ForeColor="Red"></asp:RequiredFieldValidator>


                            Verify New PassWord:
                            <asp:TextBox ID="txtVerifyNewPass" runat="server"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPass" ControlToValidate="txtVerifyNewPass" ErrorMessage="PassWords Must Match" ForeColor="Red"></asp:CompareValidator>
                        
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required PassWord Is Required" ControlToValidate="txtVerifyNewPass" ForeColor="Red"></asp:RequiredFieldValidator>      
                            
                                          
                                    <asp:Button ID="btnNewPassWord" runat="server" Text="Update PassWord" OnClick="btnNewPassWord_Click" />

                        </asp:Panel>


                         <asp:Panel ID="pnlIsSuccessFul" runat="server">

                        <div class="alert alert-success" style="text-align:center;">
                                 <strong>PassWord Was Updated Successfully!</strong> 
                        </div>
                             </asp:Panel>

                                   </div>

</asp:Content>
