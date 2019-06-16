<%@ Page Title="" Language="C#" MasterPageFile="~/LeftNavBar.master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="StoreCreator.MyProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container" >


                    <div class="row" style="padding-top:10px;">
                    

                                           <div class="col-sm-2">
                            
                                           </div>

                         <div class="col-sm-1">

                       </div>

                         <div class="col-sm-6">


               <div class="well well-lg" style="color:#669999; border-bottom: 3px #e5e5e5 solid;border-right: 3px #e5e5e5 solid; border-left: 3px #e5e5e5 solid; ">

                               <h3 style="text-align:center;padding-bottom:10px; font-weight:bold;">My Profile</h3>
                   
                                <asp:Panel ID="pnlImageCreationIsSuccessful" runat="server" BorderColor="DarkGreen">

                                <div class="alert alert-success" style="text-align:center;">
                                         <strong>Image Was Uploaded!</strong>                                            
                                </div>
                 
                                        </asp:Panel>  
                                    <asp:Panel ID="pnlUpdateInfo" runat="server" BorderColor="DarkGreen">

                                   <div class="alert alert-success" style="text-align:center;">
                                         <strong>Basic Information Was Updated!</strong> 
                                    
                                          </div>
                                          </asp:Panel>          
                               
               

                              

                    

                         
                              <h4 style="text-align:center;">Basic Information</h4>
                      
                                <hr />


                               First Name:
                               <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                               <br />
                               Last Name:
                               <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                               <br />
                               Address:
                               <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                               <br />
                               City:
                               <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                               <br />
                               State:

                            <asp:DropDownList ID="ddlStates" runat="server" Font-Size="Small" CssClass="form-control">
                                       <asp:ListItem Value="-1">Select State</asp:ListItem>        
                                       <asp:ListItem Value="Alabama">Alabama</asp:ListItem>
                                       <asp:ListItem Value="Alaska">Alaska</asp:ListItem>
                                       <asp:ListItem Value="Arizona">Arizona</asp:ListItem>
                                       <asp:ListItem Value="Arkansas">Arkansas</asp:ListItem>
                                       <asp:ListItem Value="California">California</asp:ListItem>
                                       <asp:ListItem Value="Coloroda">Coloroda</asp:ListItem>
                                       <asp:ListItem Value="Connecticut">Connecticut</asp:ListItem>
                                       <asp:ListItem Value="Deleware">Deleware</asp:ListItem>
                                       <asp:ListItem Value="Florida">Florida</asp:ListItem>
                                       <asp:ListItem Value="Georgia">Georgia</asp:ListItem>
                                       <asp:ListItem Value="Hawaii">Hawaii</asp:ListItem>
                                       <asp:ListItem Value="Idaho">Idaho</asp:ListItem>
                                       <asp:ListItem Value="Illinois">Illinois</asp:ListItem>
                                       <asp:ListItem Value="Indiana">Indiana</asp:ListItem>
                                       <asp:ListItem Value="Iowa">Iowa</asp:ListItem>
                                       <asp:ListItem Value="Kansas">Kansas</asp:ListItem>
                                       <asp:ListItem Value="Kentucky">Kentucky</asp:ListItem>
                                       <asp:ListItem Value="Louisiana">Louisiana</asp:ListItem>
                                       <asp:ListItem Value="Maine">Maine</asp:ListItem>
                                       <asp:ListItem Value="Maryland">Maryland</asp:ListItem>
                                       <asp:ListItem Value="Massachusetts">Massachusetts</asp:ListItem>
                                       <asp:ListItem Value="Michigan">Michigan</asp:ListItem>
                                       <asp:ListItem Value="Minnesota">Minnesota</asp:ListItem>
                                       <asp:ListItem Value="Mississippi">Mississippi</asp:ListItem>
                                       <asp:ListItem Value="Missouri">Missouri</asp:ListItem>
                                       <asp:ListItem Value="Montana">Montana</asp:ListItem>
                                       <asp:ListItem Value="Nebraska">Nebraska</asp:ListItem>
                                       <asp:ListItem Value="Nevada">Nevada</asp:ListItem>
                                       <asp:ListItem Value="New Hampshire">New Hampshire</asp:ListItem>
                                       <asp:ListItem Value="New Jersey">New Jersey</asp:ListItem>
                                       <asp:ListItem Value="New Mexico">New Mexico</asp:ListItem>
                                       <asp:ListItem Value="New York">New York</asp:ListItem>
                                       <asp:ListItem Value="North Carolina">North Carolina</asp:ListItem>
                                       <asp:ListItem Value="North Dakota">North Dakota</asp:ListItem>
                                       <asp:ListItem Value="Ohio">Ohio</asp:ListItem>
                                       <asp:ListItem Value="Oklahoma">Oklahoma</asp:ListItem>
                                       <asp:ListItem Value="Oregon">Oregon</asp:ListItem>
                                       <asp:ListItem Value="Pennsylvania">Pennsylvania</asp:ListItem>
                                       <asp:ListItem Value="Rhode Island">Rhode Island</asp:ListItem>
                                       <asp:ListItem Value="South Carolina">South Carolina</asp:ListItem>
                                       <asp:ListItem Value="South Dakota">South Dakota</asp:ListItem>
                                       <asp:ListItem Value="Tennessee">Tennessee</asp:ListItem>
                                       <asp:ListItem Value="Missouri">Missouri</asp:ListItem>
                                       <asp:ListItem Value="Texas">Texas</asp:ListItem>
                                       <asp:ListItem Value="Utah">Utah</asp:ListItem>
                                       <asp:ListItem Value="Vermont">Vermont</asp:ListItem>
                                       <asp:ListItem Value="Virginia">Virginia</asp:ListItem>
                                       <asp:ListItem Value="Washington">Washington</asp:ListItem>
                                       <asp:ListItem Value="West Virginia">West Virginia</asp:ListItem>
                                       <asp:ListItem Value="Wisconsin">Wisconsin</asp:ListItem>
                                       <asp:ListItem Value="Wyoming">Wyoming</asp:ListItem>
                                   </asp:DropDownList>

                                                  <br />
                               ZipCode:
                               <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control"></asp:TextBox>
                               <br />
                               Email:
                               <asp:TextBox ID="txtUserEmail" runat="server" CssClass="form-control"></asp:TextBox>
                               <br />

                               <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Zip Code Must Be Numbers Only" ControlToValidate="txtZipCode" Type="Integer" Operator="DataTypeCheck" ForeColor="Red"></asp:CompareValidator>
                               <asp:Button ID="btnUpdateInfo" runat="server" Text="Update Basic Information" OnClick="btnUpdateInfo_Click" CssClass="btn-sm"  BorderColor="#669999" BorderWidth="1" Font-Bold="true"  BackColor="#669999" ForeColor="White"  />





                         
                                    <h4 style="text-align:center;">Upload Avatar</h4>
  <hr />

                        
                                                            <asp:Image ID="imgAvatar" runat="server" Width="200" Height="200" />

                                  <br />
                                   <br />
                                  <br />

                               <asp:FileUpload ID="filAvatarUpload" runat="server"  />

                                                                <br />

                               <asp:Button ID="btnUploadAvatar" runat="server" Text="Upload Avatar" OnClick="btnUploadAvatar_Click" />

                               <asp:Label ID="lblUploadStatus" runat="server" ></asp:Label>


                           



                              
    
                   </div>

                                                            
                        </div>

                                                 <div class="col-sm-1">
                            
                                           </div>

                         <div class="col-sm-2">

                       </div>

              </div>
 </div>


</asp:Content>
