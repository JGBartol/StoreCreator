<%@ Page Title="" Language="C#" MasterPageFile="~/LeftNavBar.master"  AutoEventWireup="true" CodeBehind="FrontEndShoppingCart.aspx.cs" Inherits="StoreCreator.FrontEndShoppingCart" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



          <div class="container" >

                  <div class="well well-lg" style="color:#669999; border-bottom: 3px #e5e5e5 solid;border-right: 3px #e5e5e5 solid; border-left: 3px #e5e5e5 solid; ">

   

                                                  <div class="row" style="padding-top:10px;">
                                                 


                                                                <div class="col-sm-12" >

                                                          
                               <div class="well well-lg">
                                                         
                                 <asp:Wizard ID="wizCheckOut" runat="server"  SideBarStyle-Font-Size="Large" SideBarStyle-Width="200px"
                                      OnActiveStepChanged="wizCheckOut_ActiveStepChanged"
                                      OnCancelButtonClick="wizCheckOut_CancelButtonClick"
                                      OnNextButtonClick="wizCheckOut_NextButtonClick"
                                      OnFinishButtonClick="btnSubmitPayment_Click"
                                      OnPreviousButtonClick="wizCheckOut_PreviousButtonClick"
                                      OnSideBarButtonClick="wizCheckOut_SideBarButtonClick"
                                       SideBarStyle-VerticalAlign="Top"
                                      Font-Bold="true" Font-Size="Large" 
                                      
                                       
                                     >
                                       <WizardSteps>
                                         <asp:WizardStep ID="WizardStep1"  runat="server" Title="1) Confirm Products" >



                                              <asp:ListView ID="lvShpppingCart" runat="server"   OnItemCommand="lvShpppingCart_ItemCommand" OnItemDeleted="lvShpppingCart_ItemDeleted" OnItemDeleting="lvShpppingCart_ItemDeleting"   >
                                                 <ItemTemplate>
                                                      

                                                            <div class="col-sm-2" >                          

                                                   <asp:Image ID="Image1" runat="server" Height="90px" Width="90px"
                                                  ImageUrl='<%# "data:Image/png;base64,"
                                                  + Convert.ToBase64String((byte[])Eval("bytImage")) %>' />
                                     
                                                                </div>

                                                         <div class="col-sm-10" >                          


                                        <b style="font-size:large;"> <%# Eval("strProductName") %> </b>
                                                             
                                       <br />

                          

                                         <p style="font-size:small;"><%# Eval("strProductDescription") %></p>


                                               


                                                        Price:   <b style="color:green;"> <%# Eval("decProductPrice", "{0:c}") %></b>
                                        
                                                           <br />

                                                                  Quantity:
                                                                  <asp:Label ID="lblQuantity" Font-Bold="true" runat="server" Text='<%# GetQuantity(Convert.ToInt32(Eval("pkProductId"))).ToString() %>'></asp:Label>
                                                                 


                                                              <asp:Label ID="lblProductId" runat="server" Text='<%# Eval("pkProductId") %>' Visible="false"></asp:Label>           
                                                              <asp:Label ID="lblStoreId" runat="server" Text='<%# Eval("intStoreId") %>' Visible="false"></asp:Label>           

                                                             <br />
                                              
                                                              Final Price:  
                                                 
                                                            
                                                                   <asp:Label ID="lblFinalPrice" runat="server" ForeColor="Green" Font-Bold="true" Text='<%# GetQuantity(Convert.ToInt32(Eval("pkProductId"))) * Convert.ToDecimal(Eval("decProductPrice")) %>'></asp:Label>
                                                          
                                             <span style="float:right;">
                                                  <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Delete" CommandArgument='<%# Eval("pkProductId") %>'>Remove Product</asp:LinkButton>
                                                 </span>

                                                    <br />
                                                   
                                                     <hr />

                                             </div>
                                     
                                   

                                </ItemTemplate>
                           </asp:ListView>

                                            <span style="float:right;font-weight:bold;">
                                       Total Costs:            
                 <asp:Label ID="lblTotalPrice" runat="server"  Font-Bold="true" ForeColor="Green" Font-Size="X-Large"></asp:Label>
                                                </span>

                                         </asp:WizardStep>
                                         <asp:WizardStep ID="WizardStep2" runat="server"  Title="2) Payment Information" >

                                             
                       <b>First Name:</b> <span style="color:red;">*</span> 
                         <asp:TextBox ID="txtFirstName" runat="server"  CssClass="form-control" BorderColor="DarkGray"  MaxLength="50"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="reqFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="First Name Is Required" ForeColor="Red" SetFocusOnError="true" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>

                <br />

                        <b>Last Name:</b><span style="color:red;">*</span>  
                        <asp:TextBox ID="txtLastName" runat="server"  CssClass="form-control" BorderColor="DarkGray" MaxLength="50"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqLastName" runat="server" ControlToValidate="txtLastName" ErrorMessage="Last Name Is Required" ForeColor="Red" SetFocusOnError="true" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>

               <br />

                
                        <b>User Email:</b><span style="color:red;">*</span> 
                        <asp:TextBox ID="txtUserEmail" runat="server" aria-invalid="true" CssClass="form-control" BorderColor="DarkGray" MaxLength="50" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqUserEmail" runat="server" ControlToValidate="txtUserEmail" ErrorMessage="Email Is Required" ForeColor="Red" SetFocusOnError="true" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>
                   
                               <br />

                     
                             <br />

                             <b>Address:</b><span style="color:red;">*</span> 
                        <asp:TextBox ID="txtAddress" runat="server"  CssClass="form-control" BorderColor="DarkGray" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddress" ErrorMessage="Address Is Required" ForeColor="Red" SetFocusOnError="true" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>

               <br />

                       <b>City:</b><span style="color:red;">*</span> 
                        <asp:TextBox ID="txtCity" runat="server"  CssClass="form-control" BorderColor="DarkGray" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCity" ErrorMessage="City Is Required" ForeColor="Red" SetFocusOnError="true" ValidationGroup="PersonalInfo"></asp:RequiredFieldValidator>

               <br />

                       <b>State:</b><span style="color:red;">*</span> 
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

                        <b>Zip Code:</b>
                        <asp:TextBox ID="txtZipCode" runat="server"  CssClass="form-control" BorderColor="DarkGray" MaxLength="50"></asp:TextBox>
                   




                                         </asp:WizardStep>
                                         <asp:WizardStep ID="WizardStep3" runat="server" Title="3) Check Out">

                                             Payment Method

                                                 <hr />

                                             <asp:RadioButtonList ID="rdoPaymentMethod" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdoPaymentMethod_SelectedIndexChanged">
                                                 <asp:ListItem>Cash</asp:ListItem>
                                                 <asp:ListItem>Credit Card</asp:ListItem>
                                                 <asp:ListItem>Bill Me</asp:ListItem>
                                                 <asp:ListItem>Check</asp:ListItem>
                                             </asp:RadioButtonList>

                                             <asp:RequiredFieldValidator ID="reqPaymentMethod" runat="server" ErrorMessage="Payment Method Is Required" ControlToValidate="rdoPaymentMethod"  ForeColor="Red" SetFocusOnError="true" ></asp:RequiredFieldValidator>

                                             <asp:Panel ID="pnlCreditCard" runat="server">

                                                 <br />
                                                                                    
              
                                                First Name
                                                 <asp:TextBox ID="txtNameOnCardFirst" runat="server" ></asp:TextBox> 
                                                <asp:RequiredFieldValidator ID="reqCardFirstName" runat="server" ErrorMessage="First Name Is Required" ControlToValidate="txtNameOnCardFirst"  ForeColor="Red" SetFocusOnError="true"  ValidationGroup="CreditCard" ></asp:RequiredFieldValidator>

                                                  <br />

                                                      Middle Name
                                                 <asp:TextBox ID="txtNameOnCardMiddle" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqCardMiddleName" runat="server" ErrorMessage="Middle Name Is Required" ControlToValidate="txtNameOnCardMiddle"  ForeColor="Red" SetFocusOnError="true"   ValidationGroup="CreditCard"></asp:RequiredFieldValidator>
                                                
                                                 <br />


                                                         Last Name
                                                 <asp:TextBox ID="txtNameOnCardLast" runat="server"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="reqCardLastName" runat="server" ErrorMessage="Last Name Is Required" ControlToValidate="txtNameOnCardLast"  ForeColor="Red" SetFocusOnError="true"   ValidationGroup="CreditCard"></asp:RequiredFieldValidator>
                                                               
                                                       <br />



                                                Type Of Card
                                                 <hr />



                      <asp:RadioButtonList ID="rdoTypeOfCard" runat="server">
                        <asp:ListItem>Mastercard</asp:ListItem>
                        <asp:ListItem>Visa</asp:ListItem>
                        <asp:ListItem>American Express</asp:ListItem>
                        <asp:ListItem>Discover</asp:ListItem>
                    </asp:RadioButtonList>

                 <asp:RequiredFieldValidator ID="reqTypeOfCard" runat="server" ErrorMessage="Type Of Card Is Required" ControlToValidate="rdoTypeOfCard"  ForeColor="Red" SetFocusOnError="true"   ValidationGroup="CreditCard"></asp:RequiredFieldValidator>
                              <br />


                 <div class="form-inline" role="form">
                <div class="form-group">

               Credit Card Number:
                    <br />

                    <asp:TextBox ID="txt" runat="server" CssClass="form-control" Width="80" MaxLength="4"></asp:TextBox> -
                    <asp:TextBox ID="txt2" runat="server" CssClass="form-control" Width="80" MaxLength="4"></asp:TextBox> -
                    <asp:TextBox ID="txt3" runat="server" CssClass="form-control" Width="80" MaxLength="4"></asp:TextBox> -
                    <asp:TextBox ID="txt4" runat="server" CssClass="form-control" Width="80" MaxLength="4"></asp:TextBox>

                   <asp:RequiredFieldValidator ID="reqCardNumber1" runat="server" ErrorMessage="Four Digits Are Required" ControlToValidate="txt"  ForeColor="Red" SetFocusOnError="true"   ValidationGroup="CreditCard"></asp:RequiredFieldValidator>
                   <asp:RequiredFieldValidator ID="reqCardNumber3" runat="server" ErrorMessage="Four Digits Are Required" ControlToValidate="txt2"  ForeColor="Red" SetFocusOnError="true"   ValidationGroup="CreditCard"></asp:RequiredFieldValidator>
                   <asp:RequiredFieldValidator ID="reqCardNumber4" runat="server" ErrorMessage="Four Digits Are Required" ControlToValidate="txt3"  ForeColor="Red" SetFocusOnError="true"   ValidationGroup="CreditCard"></asp:RequiredFieldValidator>
                   <asp:RequiredFieldValidator ID="reqCardNumber5" runat="server" ErrorMessage="Four Digits Are Required" ControlToValidate="txt4"  ForeColor="Red" SetFocusOnError="true"   ValidationGroup="CreditCard"></asp:RequiredFieldValidator>

               </div>
                     </div>
                                                 
                 <div class="form-inline" role="form">
                <div class="form-group">

                    Month:
                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                        <asp:ListItem>Select Month</asp:ListItem>                  
                        <asp:ListItem>Jan</asp:ListItem>
                        <asp:ListItem>Feb</asp:ListItem>
                        <asp:ListItem>Mar</asp:ListItem>
                        <asp:ListItem>Apr</asp:ListItem>
                        <asp:ListItem>May</asp:ListItem>
                        <asp:ListItem>Jun</asp:ListItem>
                        <asp:ListItem>Jul</asp:ListItem>
                        <asp:ListItem>Aug</asp:ListItem>
                        <asp:ListItem>Sep</asp:ListItem>
                        <asp:ListItem>Oct</asp:ListItem>
                        <asp:ListItem>Nov</asp:ListItem>
                        <asp:ListItem>Dec</asp:ListItem>
                    </asp:DropDownList> 
       
                    <asp:RequiredFieldValidator ID="reqMonth" runat="server"
                        ErrorMessage="Month is required" Text="*"
                        ControlToValidate="ddlMonth" ForeColor="Red"
                        InitialValue="Select Month" ValidationGroup="CreditCard">
                    </asp:RequiredFieldValidator>
                                 
                    Year:
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                        <asp:ListItem>Select Year</asp:ListItem>                                        
                        <asp:ListItem>2016</asp:ListItem>
                        <asp:ListItem>2017</asp:ListItem>
                        <asp:ListItem>2018</asp:ListItem>
                        <asp:ListItem>2019</asp:ListItem>
                        <asp:ListItem>2020</asp:ListItem>
                        <asp:ListItem>2021</asp:ListItem>
                        <asp:ListItem>2022</asp:ListItem>
                        <asp:ListItem>2023</asp:ListItem>
                        <asp:ListItem>2024</asp:ListItem>
                        <asp:ListItem>2025</asp:ListItem>
                    </asp:DropDownList>    

                     <asp:RequiredFieldValidator ID="reqYear" runat="server"
                        ErrorMessage="Year is required" Text="*"
                        ControlToValidate="ddlYear" ForeColor="Red"
                        InitialValue="Select Year" ValidationGroup="CreditCard">
                    </asp:RequiredFieldValidator>
                                                                                                         
               </div>
                     </div>

                                             </asp:Panel>




                                         </asp:WizardStep>

                                       </WizardSteps>
                                  </asp:Wizard>
                            




                                                                    </div>

                                                      </div>
                                              

                                                    
                                  </div>
              </div>
</div>

</asp:Content>
