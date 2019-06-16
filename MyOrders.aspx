<%@ Page Title="" Language="C#" MasterPageFile="~/LeftNavBar.master" AutoEventWireup="true" CodeBehind="MyOrders.aspx.cs" Inherits="StoreCreator.MyOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="container" >

               <div class="well well-lg" style="color:#669999; border-bottom: 3px #e5e5e5 solid;border-right: 3px #e5e5e5 solid; border-left: 3px #e5e5e5 solid; ">

                    <div class="row" style="padding-top:10px;">


                               <h3 style="text-align:center;padding-bottom:10px; font-weight:bold;">My Profile</h3>
                   
                             
                                    <asp:Panel ID="pnlDeleteOrder" runat="server" BorderColor="DarkGreen">

                                   <div class="alert alert-success" style="text-align:center;">
                                         <strong>Order Was Deleted!</strong> 
                                    
                                          </div>
                                          </asp:Panel>          
                               
                                             


                         <div class="row" style="padding-top:10px;">

                              

                        
                 

                          <div class="col-sm-6">
                              <h4 style="text-align:center;">My Bought Orders</h4>
                      
                                <hr />

                                                    
                                              <asp:ListView ID="lvMyOrders" runat="server" OnItemCommand="lvMyOrders_ItemCommand" OnItemDeleting="lvMyOrders_ItemDeleting"   >
                                                 <ItemTemplate>
                                                             <div class="row">

                                                            <div class="col-sm-3" >                          

                                                   <asp:Image ID="Image1" runat="server" Height="80px" Width="80px"
                                                  ImageUrl='<%# "data:Image/png;base64,"
                                                  + Convert.ToBase64String((byte[])Eval("bytImage")) %>' />
                                     
                                                                </div>

                                                                 
                                                         <div class="col-sm-9" >                          

                                                    Product:  <%# Eval("strProductName") %> 
                                                             <br />
                                                              
                                                    Quantity:    <%# Eval("intQuantity") %>  
                                                             <br />

                                                   Price:   <b style="color:green;"> <%# Eval("decTransactionAmount", "{0:c}") %></b>
                                                             <br />

                                                   Final Price:   <b style="color:green;"> <%# Eval("FinalPrice", "{0:c}") %></b>
                                                             <br />

                                                   Shipped To:   <%# Eval("FullAddress") %>


                                                   <br />

                      

                                                      </div>                                      
                                                 </div>

                                        <br />
                                    <hr />  

                                </ItemTemplate>
                           </asp:ListView>


                                                 </div>
                                <div class="col-sm-6">

                             <h4 style="text-align:center;">My Sold Orders</h4>
                      
                                <hr />

                                           
                                              <asp:ListView ID="lvMySoldOrders" runat="server"   >
                                                 <ItemTemplate>
                                                             <div class="row">

                                                            <div class="col-sm-3" >                          

                                                   <asp:Image ID="Image1" runat="server" Height="80px" Width="80px"
                                                  ImageUrl='<%# "data:Image/png;base64,"
                                                  + Convert.ToBase64String((byte[])Eval("bytImage")) %>' />
                                     
                                                                </div>

                                                                 
                                                         <div class="col-sm-9" >                          

                                                    Product:  <%# Eval("strProductName") %> 
                                                             <br />
                                                              
                                                    Quantity:    <%# Eval("intQuantity") %>  
                                                             <br />

                                                   Price:   <b style="color:green;"> <%# Eval("decTransactionAmount", "{0:c}") %></b>
                                                             <br />

                                                   Final Price:   <b style="color:green;"> <%# Eval("FinalPrice", "{0:c}") %></b>
                                                             <br />

                                                   Shipped To:   <%# Eval("FullAddress") %>


                                                   <br />

                      

                                                      </div>                                      
                                                 </div>

                                        <br />
                                    <hr />  

                                </ItemTemplate>
                           </asp:ListView>





                                                    </div>
                                  




                               </div>
                      

                              

                             </div>
                        </div>
                   </div>
            </div>


</asp:Content>
