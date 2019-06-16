<%@ Page Title="" Language="C#" MasterPageFile="~/LeftNavBar.master" AutoEventWireup="true" CodeBehind="FrontEndSearchProducts.aspx.cs" Inherits="StoreCreator.FrontEndSearchProducts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    

    

       <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
  <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>

                                                 
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>                   
                                                               
                           <style type="text/css">
        .StarCss { 
            background-image: url(star.png);
            height:24px;
            width:24px;
        }
        .FilledStarCss {
            background-image: url(filledstar.png);
            height:24px;
            width:24px;
        }
        .EmptyStarCss {
            background-image: url(star.png);
            height:24px;
            width:24px;
        }
        .WaitingStarCss {
            background-image: url(waitingstar.png);
            height:24px;
            width:24px;
        }
  .carousel-inner > .item > img,
  .carousel-inner > .item > a > img {
      width: 70%;
      margin: auto;
  }

    </style>                                                 

                                          

         <div class="container" >

                  <div class="well well-lg" style="color:#669999; border-bottom: 3px #e5e5e5 solid;border-right: 3px #e5e5e5 solid; border-left: 3px #e5e5e5 solid; ">

                                                    
                                                             <div class="row">
                                                                        
                                                          
                                                             <div class="col-sm-2"  >
                                                                            
                                                                 <b>Search Criteria</b>

                                                                  <br />

                                                                     Price Between:
                                                                   <asp:TextBox ID="txtPriceBegin" runat="server" CssClass="form-control"></asp:TextBox>
                                                                  <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtPriceBegin" ErrorMessage="Must Be In 12.50 Format" ForeColor="Red" Type="Currency" Operator="DataTypeCheck"></asp:CompareValidator>
                                                                 <br />
                                                                         And 
                                                                 <br />
                                                                          <asp:TextBox ID="txtPriceEnd" runat="server" CssClass="form-control"></asp:TextBox>
                                                                 <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtPriceEnd" ErrorMessage="Must Be In 12.50 Format" ForeColor="Red" Type="Currency" Operator="DataTypeCheck"></asp:CompareValidator>


                                                                 Category:

                                                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                                                                             <asp:ListItem Value="-1">Any Category</asp:ListItem>
                                                                             <asp:ListItem Value="Shirts">Shirts</asp:ListItem>
                                                                             <asp:ListItem Value="Pants">Pants</asp:ListItem>
                                                                             <asp:ListItem Value="Shoes">Shoes</asp:ListItem>                                                                                                                                                            <asp:ListItem>Socks</asp:ListItem>
                                                                             <asp:ListItem Value="Socks">Socks</asp:ListItem>
                                                                             <asp:ListItem Value="Sneakers">Sneakers</asp:ListItem>
                                                                            </asp:DropDownList>





                                                                 Gender:
                                                                 <asp:RadioButtonList ID="RadioButtonList1" runat="server" Font-Bold="false">
                                                                     <asp:ListItem>Male</asp:ListItem>
                                                                     <asp:ListItem>Female</asp:ListItem>
                                                                 </asp:RadioButtonList>
                                                               


                                                                  <asp:TextBox ID="txtInventoryBeg" runat="server" CssClass="form-control"></asp:TextBox>
                                                                 <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtInventoryBeg" ErrorMessage="Must Be A Number" ForeColor="Red" Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>

                                                                  <asp:TextBox ID="txtInventoryEnd" runat="server" CssClass="form-control"></asp:TextBox>
                                                                 <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtInventoryEnd" ErrorMessage="Must Be A Number" ForeColor="Red" Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>


                                                                 <br />



                                                                   <asp:Button ID="btnSearchProducts" runat="server"  OnClick="btnSearchProducts_Click"   Text="Search" CssClass="form-control"  BorderColor="#669999" BorderWidth="1" Font-Bold="true"  BackColor="#669999" ForeColor="White"  />


                                                                </div>
                                                                <div class="col-sm-10" >

                                                            <asp:Panel ID="pnlLoginIsSuccessful" runat="server" BorderColor="DarkGreen">
                                                                            <div class="alert alert-success" style="text-align:center;">
                                                                                     <strong>Login was successful!</strong> 
                                                                            </div>
                                                                        </asp:Panel>
                                                                  
                

                                                              
                
                                                      
       <asp:GridView ID="gvStoreProducts"  runat="server" CellPadding="4" GridLines="None" OnRowCreated="gvStoreProducts_RowCreated" EmptyDataText="No Products" OnSorting="gvStoreProducts_Sorting" AllowSorting="True" Width="100%" OnRowDataBound="gvStoreProducts_RowDataBound" ShowFooter="true"  OnDataBound="gvStoreProducts_DataBound" OnRowCommand="gvStoreProducts_RowCommand"
                                   CurrentSortField="strProductName" CurrentSortDirection="ASC" 
                                   AllowPaging="True" PageSize="6" ForeColor="#333333">
                                  <Columns>
                                   <asp:TemplateField>
                                       <HeaderTemplate>
                                           <asp:CheckBox ID="cbHeadergvStoreProducts" runat="server" OnCheckedChanged="cbHeadergvStoreProducts_CheckedChanged" AutoPostBack="true" />
                                       </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbgvStoreProducts" runat="server" />
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                    <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" Height="40px" Width="40px"
                                            ImageUrl='<%# "data:Image/png;base64,"
                                            + Convert.ToBase64String((byte[])Eval("bytImage")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                      <asp:TemplateField>
                                          <ItemTemplate>
                                              <asp:ImageButton ID="ImageButton1" runat="server"   ImageUrl="~/Shop-512.png" PostBackUrl='<%# "FrontEndProducts.aspx?ProductId=" +  Eval("pkProductId") %>' Width="20" Height="20" />
                                              <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/view-icone-6308-128.png" Width="20" Height="20" CommandName="QuickView" CommandArgument='<%# Eval("pkProductId")  %>' />
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                       <asp:TemplateField>
                                          <ItemTemplate>
                                              <asp:Label ID="lblCBgvStoreProducts" runat="server" Text='<%# Eval("pkProductId") %>' Visible="false" ></asp:Label>        
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                  </Columns>

                                   <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>

                                   <EditRowStyle BackColor="#7C6F57"></EditRowStyle>

                                   <FooterStyle BackColor="#669999" ForeColor="White" Font-Bold="True"></FooterStyle>

                                   <HeaderStyle BackColor="#669999" Font-Bold="True" ForeColor="White"></HeaderStyle>

                                   <PagerStyle HorizontalAlign="Center" BackColor="#666666" ForeColor="White"></PagerStyle>

                                   <RowStyle BackColor="#E3EAEB"></RowStyle>

                                   <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

                                   <SortedAscendingCellStyle BackColor="#F8FAFA"></SortedAscendingCellStyle>

                                   <SortedAscendingHeaderStyle BackColor="#246B61"></SortedAscendingHeaderStyle>

                                   <SortedDescendingCellStyle BackColor="#D4DFE1"></SortedDescendingCellStyle>

                                   <SortedDescendingHeaderStyle BackColor="#15524A"></SortedDescendingHeaderStyle>
                               </asp:GridView>
                           
                            <asp:Label ID="lblCompareStatus" runat="server" ></asp:Label>

                      <asp:Button ID="btnCompareProducts" runat="server" Text="Compare Selected Products" OnClick="btnCompareProducts_Click" CssClass="form-control" Width="300px"  BorderColor="#669999" BorderWidth="1" Font-Bold="true"  BackColor="#669999" ForeColor="White"   />



                            <asp:Repeater ID="rptStoreProducts" runat="server">
                   <ItemTemplate>
                       <ul class="pagination">
                         <li>
                        <asp:LinkButton ID="rptProductRepeater" runat="server" 
                            Text='<%#Eval("Text") %>' 
                            CommandArgument='<%# Eval("Value") %>'
                            Enabled='<%# Eval("Enabled") %>' 
                            OnClick="rptProductRepeater_Click">
                        </asp:LinkButton>
                         </li>
                  </ul>
                    </ItemTemplate>
                                </asp:Repeater>

                                         





        </div>

             <div class="row">
                  <div class="col-sm-3" >
                      </div>

                <div class="col-sm-3" >


     
                  <div id="CarViewed" class="carousel slide" data-ride="carousel">

                    <ol class="carousel-indicators">
                      <li data-target="#CarViewed" data-slide-to="0" class="active"></li>
                      <li data-target="#CarViewed" data-slide-to="1"></li>
                      <li data-target="#CarViewed" data-slide-to="2"></li>
                      <li data-target="#CarViewed" data-slide-to="3"></li>
                      <li data-target="#CarViewed" data-slide-to="4"></li>
                      <li data-target="#CarViewed" data-slide-to="5"></li>
                      <li data-target="#CarViewed" data-slide-to="6"></li>
                      <li data-target="#CarViewed" data-slide-to="7"></li>
                    </ol>
                  
<div class="carousel-inner" role="listbox" style="max-width:300px; height:300px;">
    <div class="item active">
                <h1 style="text-align:center;">Most<br />Viewed<br />Products</h1>

    </div>

                      
      <asp:ListView ID="lvMostViewProducts" runat="server">
          <ItemTemplate>
              <div class="item">
          <div style=" margin-right:auto; max-width:300px; padding-left:40px;">

         <h4> <%# Eval("strProductName") %></h4>
                                

                    <asp:Image ID="Image1" runat="server" Height="100px" Width="100px"
                                            ImageUrl='<%# "data:Image/png;base64,"
                                            + Convert.ToBase64String((byte[])Eval("bytImage")) %>' />



                                                            <p> <%# Eval("strProductDescription") %></p>
                              <p> <%# Eval("AverageRating") %></p>
                            <b style="color:green;"><%# Eval("decProductPrice", "{0:c}") %></b>

                            <ajaxToolkit:Rating ID="Rating1" runat="server" ReadOnly="true"
                                                                        CurrentRating='<%# Convert.ToInt32(Eval("ProductRating")) %>'
                                                                        StarCssClass="StarCss"
                                                                        FilledStarCssClass="FilledStarCss"
                                                                        EmptyStarCssClass="EmptyStarCss"
                                                                        WaitingStarCssClass="WaitingStarCss"
                                                                        AutoPostBack="false"
                               
                                                                        >


                                                                        </ajaxToolkit:Rating>
              <br />
                               <a href= "<%# "FrontEndProducts.aspx?ProductId=" +  Eval("pkProductId") %>"><%# Eval("strProductName") %></a>


              </div>
                  </div>
    </ItemTemplate>
      </asp:ListView>

                    <a class="left carousel-control" href="#CarViewed" role="button" data-slide="prev">
                      <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                      <span class="sr-only">Previous</span>
                    </a>
                    <a class="right carousel-control" href="#CarViewed" role="button" data-slide="next">
                      <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                      <span class="sr-only">Next</span>
                    </a>
                  </div>
                </div>





                    </div>
                      <div class="col-sm-3" >



                  <div id="CarouselFeatured" class="carousel slide" data-ride="carousel">

                    <ol class="carousel-indicators">
                      <li data-target="#CarouselFeatured" data-slide-to="0" class="active"></li>
                      <li data-target="#CarouselFeatured" data-slide-to="1"></li>
                      <li data-target="#CarouselFeatured" data-slide-to="2"></li>
                      <li data-target="#CarouselFeatured" data-slide-to="3"></li>
                        <li data-target="#CarouselFeatured" data-slide-to="4"></li>
                      <li data-target="#CarouselFeatured" data-slide-to="5"></li>
                        <li data-target="#CarouselFeatured" data-slide-to="6"></li>
                      <li data-target="#CarouselFeatured" data-slide-to="7"></li>
                    </ol>
                  
<div class="carousel-inner" role="listbox" style="max-width:300px; height:300px;">
    <div class="item active">
                <h1 style="text-align:center;">Most<br />Bought<br />Products</h1>

    </div>

                      
      <asp:ListView ID="lvPopularProducts" runat="server">
          <ItemTemplate>
              
              <div class="item">
          <div style=" margin-right:auto; max-width:300px; padding-left:40px;">

         <h4> <%# Eval("strProductName") %></h4>
                                

                    <asp:Image ID="Image1" runat="server" Height="100px" Width="100px"
                                            ImageUrl='<%# "data:Image/png;base64,"
                                            + Convert.ToBase64String((byte[])Eval("bytImage")) %>' />



                                                            <p> <%# Eval("strProductDescription") %></p>
                              <p> <%# Eval("AverageRating") %></p>
                            <b style="color:green;"><%# Eval("decProductPrice", "{0:c}") %></b>

                            <ajaxToolkit:Rating ID="Rating1" runat="server" ReadOnly="true"
                                                                        CurrentRating='<%# Convert.ToInt32(Eval("ProductRating")) %>'
                                                                        StarCssClass="StarCss"
                                                                        FilledStarCssClass="FilledStarCss"
                                                                        EmptyStarCssClass="EmptyStarCss"
                                                                        WaitingStarCssClass="WaitingStarCss"
                                                                        AutoPostBack="false"
                               
                                                                        >


                                                                        </ajaxToolkit:Rating>
              <br />
                               <a href= "<%# "FrontEndProducts.aspx?ProductId=" +  Eval("pkProductId") %>"><%# Eval("strProductName") %></a>


              </div>
                  </div>
    </ItemTemplate>
      </asp:ListView>

                    <a class="left carousel-control" href="#CarouselFeatured" role="button" data-slide="prev">
                      <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                      <span class="sr-only">Previous</span>
                    </a>
                    <a class="right carousel-control" href="#CarouselFeatured" role="button" data-slide="next">
                      <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                      <span class="sr-only">Next</span>
                    </a>
                  </div>
                </div>
                                          
     
                                                 
                                                      
                                                                 <asp:ListView ID="lvRecProducts" runat="server">                 
                                                       <ItemTemplate>
              <div class="item">
          <div style=" margin-right:auto; max-width:300px; padding-left:40px;">

                    <h4> <%# Eval("strProductName") %></h4>
  

                    <asp:Image ID="Image1" runat="server" Height="100px" Width="100px"
                                            ImageUrl='<%# "data:Image/png;base64,"
                                            + Convert.ToBase64String((byte[])Eval("bytImage")) %>' />



                                                            <p> <%# Eval("strProductDescription") %></p>

                         
              <br />
                               <a href= "<%# "FrontEndProducts.aspx?ProductId=" +  Eval("pkProductId") %>"><%# Eval("strProductName") %></a>


              </div>
                  </div>
    </ItemTemplate>
            
                                                                 
                                          </asp:ListView>               
     
     
                          </div>


                        <div class="col-sm-3" >
                      </div>



                  </div>
                    
            </div>
    </div>
</div>
 





















        
                        
                                                                 
                                                                 
                                                                 
                                                                 
                                          
                                                                 
                                                                 
                                                                 
                                                                 
                                                                 
                                                                 
                                                                 
                                                                 
                                                                 
                                                                 
                                                                 
                                                                 
                                                                 
                                                                 
                                                                 
                                                                 
                                                                 
                                                                 
                                                                 
                                                                 
                                                                 
                                                                 
                                                                                        
                           
                                    <div id="modalQuickView" class="modal fade" role="dialog">
                                      <div class="modal-dialog">

                                        <div class="modal-content">
                                              <div class="modal-header">
                                   
                                                                 
        <button type="button" class="close" data-dismiss="modal">&times;</button>
           <h2 style="text-align:center;">Quick View Product</h2> 
      </div>

        <div class="modal-body">
    
                     
            <asp:FormView ID="fvQuickViewProduct" runat="server">

                
                                                    <ItemTemplate>

                                                         <div style="text-align:center;">
                                                              <h2> <%# Eval("strProductName") %></h2>

                                                                  <img src='<%# FrontEndProdInfo.ProductImage(Convert.ToInt32(Eval("pkProductId"))) %>' width="120" height="120" />

                                                                  <br />
                                                                            
                                
                                                             
                                                                  <p style="text-align:left;"> <%# Eval("strProductDescription") %></p>


                                                                 <h3 style="color:green; font-weight:bold;"> <%# Eval("decProductPrice", "{0:c}") %></h3>
                                                        
                                                               View <a href= "<%# "FrontEndProducts.aspx?ProductId=" +  Eval("pkProductId") %>"><%# Eval("strProductName") %></a>

                                                       
                                                         </div>

                                                    </ItemTemplate>

            </asp:FormView>
            
           <h3 style="text-align:center;">Reviews</h3> 
            <hr />
                <asp:ListView ID="lvReviews" runat="server" >
                                                        <ItemTemplate>

                                                                    <div class="well well-sm" style="background-color:white;">
                                                      
                         
                                                              <h5 style="font-weight:bold">  <%# Eval("strReviewTitle") %> </h5>
                         


                                                              <p2>  <%# Eval("strReviewBody") %> </p2>

                                                                        <br />

                                                                        Posted On:
                                                                <%# Convert.ToDateTime(Eval("datDateCreated")).ToShortDateString() %> 

                                                                        <br />

                                                                        <ajaxToolkit:Rating ID="Rating2" 
                                                                        
                                                                             ReadOnly="true"
                                                                        runat="server"
                                                                        CurrentRating='<%# Eval("intProductRating") %>'
                                                                        StarCssClass="StarCss"
                                                                        FilledStarCssClass="FilledStarCss"
                                                                        EmptyStarCssClass="EmptyStarCss"
                                                                        WaitingStarCssClass="WaitingStarCss"
                                                                        AutoPostBack="false"
                                                                        >


                                                                        </ajaxToolkit:Rating>

                                                                        <br />
                                                                 
                                                            </div>

                                                        </ItemTemplate>
                                                </asp:ListView>
            
                     
         
            <br />

      </div>

      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
      </div>
    </div>
       </div>

























</div>   
                                      
                                                                 
                                                                 
                                     
                    















    
                                    <div id="modalCompareProducts" class="modal fade" role="dialog" >
                                      <div class="modal-dialog" >

                                        <div class="modal-content">
                                              <div class="modal-header">
                                   
                                                                 
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Compare Products</h4>
      </div>

        <div class="modal-body">
    
            <asp:ListView ID="lvCompareProducts" runat="server" GroupItemCount="3">
                <LayoutTemplate>
                    <table border="1">
                        <tr id="groupPlaceholder" runat="server"  />
                </LayoutTemplate>
                <GroupTemplate>
                    <tr><td id="itemPlaceholder" runat="server" valign="top" />

                </GroupTemplate>
                <ItemTemplate>
                        <td>
                            
                                <h4> <%# Eval("strProductName") %></h4>
                                
                                                            <p> <%# Eval("strProductDescription") %></p>
                              <p> <%# Eval("AverageRating") %></p>
                            <b style="color:green;"><%# Eval("decProductPrice", "{0:c}") %></b>

                            <ajaxToolkit:Rating ID="Rating1" runat="server" ReadOnly="true"
                                                                        CurrentRating='<%# Convert.ToInt32(Eval("ProductRating")) %>'
                                                                        StarCssClass="StarCss"
                                                                        FilledStarCssClass="FilledStarCss"
                                                                        EmptyStarCssClass="EmptyStarCss"
                                                                        WaitingStarCssClass="WaitingStarCss"
                                                                        AutoPostBack="false"
                               
                                                                        >


                                                                        </ajaxToolkit:Rating>

                                View <a href= "<%# "FrontEndProducts.aspx?ProductId=" +  Eval("pkProductId") %>"><%# Eval("strProductName") %></a>

                        </td>
                </ItemTemplate>
            </asp:ListView>    
                </table>
           

      </div>
      <div class="modal-footer">

        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

      </div>
    </div>
       </div>







</div>   
                                             
                                                                 
       
</asp:Content>
