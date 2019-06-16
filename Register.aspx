<%@ Page Title="" Language="C#" MasterPageFile="~/LeftNavBar.master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="StoreCreator.Register" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


        
    
     <div class="container"  >

                     <div class="row">

                                           <div class="col-sm-4">
                                        
                                               </div>

                                        <div class="col-sm-4">

                                               <div class="well well-sm"  style="color:#669999;  border:3px #e5e5e5 solid;"   >
       

       
        
         <h3 style="text-align:center; color:#669999">Register</h3>


        <hr style="color:#669999; border-color:#669999;" />
     


            <div class="panel-body" style="text-align:left";>

                       <asp:Panel ID="pnlRegisterIsSuccessful" runat="server" BorderColor="DarkGreen">

                        <div class="alert alert-success" style="text-align:center;">
                                 <strong>Registration was successful!</strong> 
                        </div>

                  </asp:Panel>


        <asp:Panel ID="pnlUserEmailAlreadyTaken" runat="server" BorderColor="DarkRed">

                       <div class="alert alert-danger" style="text-align:center;">
                          <strong>User Email Already In Use</strong> 
                     </div>

            </asp:Panel>
                

        <asp:Panel ID="pnlUserNameAlreadyTaken" runat="server" BorderColor="DarkRed">

                       <div class="alert alert-danger" style="text-align:center;">
                          <strong>UserName Already Taken</strong> 
                     </div>

            </asp:Panel>
                
                        <b>User Name:</b><span style="color:red;">*</span>      
                                        <asp:TextBox ID="txtUserName" runat="server"  CssClass="form-control" BorderColor="DarkGray" MaxLength="50" ></asp:TextBox>
                                                  <asp:RequiredFieldValidator ID="valReqUserName" runat="server" ErrorMessage="UserName Is Required" ControlToValidate="txtUserName" ForeColor="Red"></asp:RequiredFieldValidator>   
                <br />


                         <b>First Name:</b> <span style="color:red;">*</span> 
                         <asp:TextBox ID="txtFirstName" runat="server"  CssClass="form-control" BorderColor="DarkGray"  MaxLength="50"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqFirstName" runat="server" ErrorMessage="First Name Is Required" ControlToValidate="txtUserName" ForeColor="Red"></asp:RequiredFieldValidator>
                   <br />

                        <b>Last Name:</b><span style="color:red;">*</span>  
                        <asp:TextBox ID="txtLastName" runat="server"  CssClass="form-control" BorderColor="DarkGray" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqLastName" runat="server" ErrorMessage="Last Name Is Required" ControlToValidate="txtLastName" ForeColor="Red"></asp:RequiredFieldValidator>           

                   <br />
                
                        <b>User Email:</b><span style="color:red;">*</span>   
                        <asp:TextBox ID="txtUserEmail" runat="server" aria-invalid="true" CssClass="form-control" BorderColor="DarkGray" MaxLength="50" ></asp:TextBox>                                    
                        <asp:RequiredFieldValidator ID="valreqUserEmail" runat="server" ErrorMessage="Email Is Required" ControlToValidate="txtUserEmail" ForeColor="Red"></asp:RequiredFieldValidator>                                    
                             <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" 
                                            ControlToValidate="txtUserEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ErrorMessage="Invalid Email" ForeColor="Red"></asp:RegularExpressionValidator>
              
                        <br />

                             <b>Address:</b> 

                        <asp:TextBox ID="txtAddress" runat="server"  CssClass="form-control" BorderColor="DarkGray" MaxLength="50"></asp:TextBox>

                          <br />

                       <b>City:</b>
                        <asp:TextBox ID="txtCity" runat="server"  CssClass="form-control" BorderColor="DarkGray" MaxLength="50"></asp:TextBox>
                           <br />

                       <b>State:</b>
                           <asp:DropDownList ID="ddlStates" runat="server" Font-Size="Small" CssClass="form-control">
                                       <asp:ListItem>Select State</asp:ListItem>
                                       <asp:ListItem>Alabama</asp:ListItem>
                                       <asp:ListItem>Alaska</asp:ListItem>
                                       <asp:ListItem>Arizona</asp:ListItem>
                                       <asp:ListItem>Arkansas</asp:ListItem>
                                       <asp:ListItem>California</asp:ListItem>
                                       <asp:ListItem>Coloroda</asp:ListItem>
                                       <asp:ListItem>Connecticut</asp:ListItem>
                                       <asp:ListItem>Deleware</asp:ListItem>
                                       <asp:ListItem>Florida</asp:ListItem>
                                       <asp:ListItem>Georgia</asp:ListItem>
                                       <asp:ListItem>Hawaii</asp:ListItem>
                                       <asp:ListItem>Idaho</asp:ListItem>
                                       <asp:ListItem>Illinois</asp:ListItem>
                                       <asp:ListItem>Indiana</asp:ListItem>
                                       <asp:ListItem>Iowa</asp:ListItem>
                                       <asp:ListItem>Kansas</asp:ListItem>
                                       <asp:ListItem>Kentucky</asp:ListItem>
                                       <asp:ListItem>Louisiana</asp:ListItem>
                                       <asp:ListItem>Maine</asp:ListItem>
                                       <asp:ListItem>Maryland</asp:ListItem>
                                       <asp:ListItem>Massachusetts</asp:ListItem>
                                       <asp:ListItem>Michigan</asp:ListItem>
                                       <asp:ListItem>Minnesota</asp:ListItem>
                                       <asp:ListItem>Mississippi</asp:ListItem>
                                       <asp:ListItem>Missouri</asp:ListItem>
                                       <asp:ListItem>Montana</asp:ListItem>
                                       <asp:ListItem>Nebraska</asp:ListItem>
                                       <asp:ListItem>Nevada</asp:ListItem>
                                       <asp:ListItem>New Hampshire</asp:ListItem>
                                       <asp:ListItem>New Jersey</asp:ListItem>
                                       <asp:ListItem>New Mexico</asp:ListItem>
                                       <asp:ListItem>New York</asp:ListItem>
                                       <asp:ListItem>North Carolina</asp:ListItem>
                                       <asp:ListItem>North Dakota</asp:ListItem>
                                       <asp:ListItem>Ohio</asp:ListItem>
                                       <asp:ListItem>Oklahoma</asp:ListItem>
                                       <asp:ListItem>Oregon</asp:ListItem>
                                       <asp:ListItem>Pennsylvania</asp:ListItem>
                                       <asp:ListItem>Rhode Island</asp:ListItem>
                                       <asp:ListItem>South Carolina</asp:ListItem>
                                       <asp:ListItem>South Dakota</asp:ListItem>
                                       <asp:ListItem>Tennessee</asp:ListItem>
                                       <asp:ListItem>Missouri</asp:ListItem>
                                       <asp:ListItem>Texas</asp:ListItem>
                                       <asp:ListItem>Utah</asp:ListItem>
                                       <asp:ListItem>Vermont</asp:ListItem>
                                       <asp:ListItem>Virginia</asp:ListItem>
                                       <asp:ListItem>Washington</asp:ListItem>
                                       <asp:ListItem>West Virginia</asp:ListItem>
                                       <asp:ListItem>Wisconsin</asp:ListItem>
                                       <asp:ListItem>Wyoming</asp:ListItem>
                                   </asp:DropDownList>
                              <br />

                        <b>Zip Code:</b>
                        <asp:TextBox ID="txtZipCode" runat="server"  CssClass="form-control" BorderColor="DarkGray" MaxLength="50"></asp:TextBox>
                        <asp:CompareValidator ID="valZipCodeInt" runat="server" ErrorMessage="Zip Code Must Be Only Whole Numbers" ControlToValidate="txtZipCode" ForeColor="Red" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>

                   <br />

                     <b>PassWord:</b><span style="color:red;">*</span>         

                        <asp:TextBox ID="txtPassWord" runat="server"  CssClass="form-control" BorderColor="DarkGray" MaxLength="25" TextMode="Password" ></asp:TextBox>
                       <asp:RequiredFieldValidator ID="valReqTxtPassWord" runat="server" ErrorMessage="PassWord Is Required" ControlToValidate="txtPassWord" ForeColor="Red"></asp:RequiredFieldValidator>                 

                        <br />
     
                  
                   <b>Repeat PassWord:</b><span style="color:red;">*</span>      

                        <asp:TextBox ID="txtRepeatPassWord" runat="server"  CssClass="form-control" BorderColor="DarkGray" MaxLength="25" TextMode="Password" ></asp:TextBox>
                        <asp:CompareValidator ID="valCmpTxtRepeatPassWord" runat="server" ErrorMessage="PassWords Must Match" ControlToValidate="txtRepeatPassWord" ControlToCompare="txtPassWord" Operator="Equal"  ForeColor="Red"></asp:CompareValidator>     

                        <br />

     
                       <asp:Button ID="btnRegister" runat="server"  OnClick="btnRegister_Click"  Text="Register" CssClass="btn-sm" Width="100%"  BorderColor="#669999" BorderWidth="1" Font-Bold="true"  BackColor="#669999" ForeColor="White"  />

                



         </div>
                          
                                        </div>

                                                               </div>

                                         <div class="col-sm-4">




                      
     
                     </div>
                </div>
      </div>



</asp:Content>
