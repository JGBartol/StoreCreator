<%@ Page Title="" Language="C#" MasterPageFile="~/LeftNavBar.master" AutoEventWireup="true" CodeBehind="FrontEndProducts.aspx.cs" Inherits="StoreCreator.FrontEndProducts" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Src="~/ctrlLogin.ascx" TagPrefix="uc1" TagName="ctrlLogin" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     
    <link href="CssStyles/Review.css" rel="stylesheet" />

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            
    
     <div class="container" >

                  <div class="well well-lg" style="color:#669999; border-bottom: 3px #e5e5e5 solid;border-right: 3px #e5e5e5 solid; border-left: 3px #e5e5e5 solid; ">

   

                                                  <div class="row" style="padding-top:10px;">
                                                 

                                                             <div class="col-sm-2" >

                                                                 

                                                       Purchase Item
                                                         <asp:DropDownList ID="ddlQuantityToAdd" runat="server" CssClass="form-control">
                                                                                    <asp:ListItem>Select Quantity To Purchase</asp:ListItem>
                                                                                    <asp:ListItem>One</asp:ListItem>
                                                                                    <asp:ListItem>Two</asp:ListItem>
                                                                                    <asp:ListItem>Three</asp:ListItem>
                                                                                    <asp:ListItem>Four</asp:ListItem>
                                                                                    <asp:ListItem>Five</asp:ListItem>                                                                                                             
                                                         </asp:DropDownList>
                                                   
                                                                 <asp:Label ID="lblQuantityStatus" runat="server" ForeColor="Red"></asp:Label>

                                                         <br />

                                                                           <asp:Button ID="btnPutInCart" runat="server" ValidationGroup="Cart"  OnClick="btnPutInCart_Click"  Text="Add To Cart" CssClass="form-control"  BorderColor="#669999" BorderWidth="1" Font-Bold="true"  BackColor="#669999" ForeColor="White"   /><br />
                                                                           
                                                                   <asp:RequiredFieldValidator ID="rfvInsertProductCart" runat="server"
                                                                        ErrorMessage="Must Select Quantity" Text="Must Select Quantity"
                                                                        ControlToValidate="ddlQuantityToAdd" ForeColor="Red"
                                                                        InitialValue="Select Quantity To Purchase" ValidationGroup="Cart">
                                                                   </asp:RequiredFieldValidator>
                                                         <hr />

                             

                                <asp:Button ID="btnFavoriteProduct" runat="server"  OnClick="btnFavoriteProduct_Click"  Text="Like Product" CssClass="form-control"  BorderColor="#669999" BorderWidth="1" Font-Bold="true"  BackColor="#669999" ForeColor="White"   /><br />
                               <asp:Button ID="btnReviewModal" runat="server" Text="Create Review" OnClick="btnReviewModal_Click"  CssClass="form-control"  BorderColor="#669999" BorderWidth="1" Font-Bold="true"  BackColor="#669999" ForeColor="White"   /><br />
                               <asp:Button ID="btnPostToBulletin" runat="server" Text="Post To Bulletin" OnClick="btnPostToBulletin_Click"  CssClass="form-control"  BorderColor="#669999" BorderWidth="1" Font-Bold="true"  BackColor="#669999" ForeColor="White"   /><br />
                               <asp:Button ID="btnSendEmailToFriend" runat="server" Text="Send Product Email" OnClick="btnSendEmailToFriend_Click"  CssClass="form-control"  BorderColor="#669999" BorderWidth="1" Font-Bold="true"  BackColor="#669999" ForeColor="White"   />



                                                                 </div>

                                                                <div class="col-sm-10" >

                                                  
                                                                  






                   
                                                                          
                                                          
     
                                                         




                                     <div class="row" style="padding-top:10px;">

                                                     

                                       <div class="col-sm-9" >
                                            <div class="well well-sm">

                                                        
                                              
                                                           
                                              <asp:Label ID="lblLogInStatus" runat="server" ForeColor="Green"></asp:Label>

                                         <div style="background-color:white;">
                                             <asp:ListView ID="lvProduct" runat="server">
                                                    <ItemTemplate>
                                                         <div class="row">

                                                                  <div class="col-sm-3" >

                                                              <img src='<%# FrontEndProdInfo.ProductImage(Convert.ToInt32(Eval("pkProductId"))) %>' width="150" height="150" />

                                                                                  </div>
                                                                             <div class="col-sm-6" >

                                                              <h2> <%# Eval("strProductName") %></h2>
                                
                                                              <p> <%# Eval("strProductDescription") %></p>


                                                           
                                                               </div>
                                                             <div class="col-sm-3" >

                                                             <h3 style="color:green; font-weight:bold;"> <%# Eval("decProductPrice", "{0:c}") %></h3>

                                                                 </div>
                                                             </div>

                                                    </ItemTemplate>
                                                </asp:ListView>
                                              </div>
                                       </div>

                                           
                                           <asp:Panel ID="pnlProductImages" runat="server"> 

                                                                             <div class="well well-sm">

                                                                        <h4 style="text-align:center;">Product Images</h4>
                                                                                 <hr />

                                                                                <asp:PlaceHolder ID="phImages" runat="server">
                                                                                </asp:PlaceHolder>                                             


                                                                            </div>

                                           </asp:Panel>  
                                                                            <div class="well well-sm">
                                                                                
                                                                         <h4 style="text-align:center;">Bulletin Board</h4>
                                                                                <hr />
                            
                                                                                <asp:Literal ID="litBB" runat="server"></asp:Literal>


                                                                            </div>

                                   
                                           <div class="well well-sm">

                                              <h4 style="text-align:center;"> Product Reviews</h4>
                                                    <hr />

                                               Average Rating:
                                               <asp:Label ID="lblAverageRating" runat="server" ></asp:Label>

                                                        <asp:ListView ID="lvReviews" runat="server">
                                                                                <ItemTemplate >

                                                                                            <div class="well well-sm"  style="background-color:white;">
                                                      
                         
                                                                                      <h5 style="font-weight:bold">  <%# Eval("strReviewTitle") %> </h5>
                         


                                                                                      <p2>  <%# Eval("strReviewBody") %> </p2>

                                                                                                <br />

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
                           

                                               </div>
                                           </div>
                                    


                                   <div class="col-sm-3" >




                                     </div>


                                                      </div>                          



          



                                                                                
                                                                                
                                                                                
                                                                                
                                                                                
                                                               


                                                      </div>
             

                                              <div class="col-sm-3" >


                                                  </div>




                            </div>
               </div>

</div>









    
                                    <div id="ReviewModal" class="modal fade" role="dialog">
                                      <div class="modal-dialog">

                                        <div class="modal-content">
                                              <div class="modal-header">
                                   
                                               
                                                                 
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Product Review</h4>
      </div>

        <div class="modal-body">
    
                               Review Title:

                <asp:TextBox ID="txtReviewTitle" runat="server" CssClass="form-control"></asp:TextBox>
     
                                                                                <br />

                                                                                Review Message:
                                                                                <asp:TextBox ID="txtReviewBody" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="8"></asp:TextBox>
                                                                             
                                                                                <br />

           
 


            <ajaxToolkit:Rating ID="rat2" runat="server"
                 StarCssClass="StarCss"
                 EmptyStarCssClass="EmptyStarCss"
                 WaitingStarCssClass="WaitingStarCss"
                 FilledStarCssClass="FilledStarCss"
                 AutoPostBack="true"
                ></ajaxToolkit:Rating>


            


                       <asp:Label ID="Label1" runat="server" ></asp:Label>

            <br />
            <br />

      </div>
      <div class="modal-footer">

        <asp:Button ID="btnCreateReview" runat="server" Text="Create Review"  OnClick="btnCreateReview_Click" class="btn btn-default" />
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

      </div>
    </div>
       </div>
</div>   

        




    
                                    <div id="BulletinModal" class="modal fade" role="dialog">
                                      <div class="modal-dialog">

                                        <div class="modal-content">
                                              <div class="modal-header">
                                   
                                                                 
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Post To Bulletin</h4>
      </div>

        <div class="modal-body">
    
                               Bulletin Title:

                <asp:TextBox ID="txtBulletinTitle" runat="server" CssClass="form-control"></asp:TextBox>
     
                               <br />

                                   Bulltin Message:
                                   <asp:TextBox ID="txtBulletinMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="8"></asp:TextBox>
                                                                             
                                  <br />

            <br />
            <br />

      </div>
      <div class="modal-footer">

        <asp:Button ID="btnPostBulletin" runat="server" Text="Post To Bulletin"  OnClick="btnPostBulletin_Click" class="btn btn-default" />
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

      </div>
    </div>
       </div>
</div>   

    

    
                                    <div id="modalSendEmail" class="modal fade" role="dialog">
                                      <div class="modal-dialog">

                                        <div class="modal-content">
                                              <div class="modal-header">
                                   
                                                                 
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Send Product Email</h4>
      </div>

        <div class="modal-body">
    
                               Email Address:

                <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="form-control"></asp:TextBox>
            To:
                     <asp:TextBox ID="txtToName" runat="server" CssClass="form-control"></asp:TextBox>

                     
         
              <br />
                                               Email Subject:

                            <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control"></asp:TextBox>
                                          <br />

            First Name
            <div class="form-group">
                           <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>

                Last Name:

                           <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>

                </div>
                                                                             
                                  <br />

            <br />
            <br />

      </div>
      <div class="modal-footer">

        <asp:Button ID="btnSendEmail" runat="server" Text="Send Email"  OnClick="btnSendEmail_Click" class="btn btn-default" />
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

      </div>
    </div>
       </div>
</div>   




       <div class="modal fade" id="ReplyBBModal" role="dialog">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" id="CloseReply" class="close" data-dismiss="modal" runat="server" >&times;</button>
          <h1 class="modal-title">Reply To Bulletin Board</h1>
        </div>
        <div class="modal-body">
          <p>

              Title: 
              <asp:TextBox ID="txtReplyBBTitle" runat="server"  CssClass="form-control" ></asp:TextBox>

              <br />

              Body:
               <asp:TextBox ID="txtReplyBBBody" runat="server"  CssClass="form-control" TextMode="MultiLine" ></asp:TextBox>


           
         <br />
          </p>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            <asp:Button ID="btnReplyToBulletin" runat="server" Text="Reply To Bulletin" OnClick="btnReplyToBulletin_Click" />
               <span class="glyphicon glyphicon-pencil"></span> 
        </div>
      </div>
      
    </div>
  </div>


        


</asp:Content>
