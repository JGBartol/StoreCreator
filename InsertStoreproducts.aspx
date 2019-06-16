<%@ Page Title="" Language="C#" MasterPageFile="~/LeftNavBar.master" AutoEventWireup="true" CodeBehind="InsertStoreproducts.aspx.cs" Inherits="StoreCreator.InsertStoreproducts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


      <div class="container"" >


               <div class="well well-lg" style="color:#669999; border-bottom: 3px #e5e5e5 solid;border-right: 3px #e5e5e5 solid; border-left: 3px #e5e5e5 solid; ">

                             <h4 style="text-align:center;">Products</h4>

                       <asp:Panel ID="pnlNumberOfImages" runat="server" BorderColor="DarkGreen">

                        <div class="alert alert-success" style="text-align:center;">
                                 <strong><asp:Label ID="lblNumberOfImagesUploaded" runat="server" ></asp:Label> Image/s Uploaded Successfully!</strong> 
                        </div>
                  </asp:Panel>


                   <asp:Panel ID="pnlImageUpload" runat="server">
                        <div class="alert alert-success" style="text-align:center;">
                     <strong>Main Product Image Uploaded Successfully!</strong> 
                               </div>
                   </asp:Panel>

                    <div class="row" style="padding-top:0px;">


    <asp:GridView ID="gvInsertProducts" runat="server" ShowFooter="true" OnRowCommand="gvInsertProducts_RowCommand" AutoGenerateColumns="false" CellPadding="4" GridLines="Horizontal" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbEdit" CommandArgument='<%# Eval("pkProductId") %>' CommandName="EditRow" ForeColor="#8C4510" runat="server" CausesValidation="false">Edit</asp:LinkButton>
                    <asp:LinkButton ID="lbDelete" CommandArgument='<%# Eval("pkProductId") %>' CommandName="DeleteRow" ForeColor="#8C4510" runat="server" CausesValidation="false" OnClientClick="confirm('Are you sure you want to delete this product>');">Delete</asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="lbUpdate" CommandArgument='<%# Eval("pkProductId") %>' CommandName="UpdateRow" ForeColor="#8C4510" runat="server" CausesValidation="true" ValidationGroup="Update">Update</asp:LinkButton>
                    <asp:LinkButton ID="lbCancel" CommandArgument='<%# Eval("pkProductId") %>' CommandName="CancelUpdate" ForeColor="#8C4510" runat="server" CausesValidation="false">Cancel</asp:LinkButton>
                </EditItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField>
                <ItemTemplate>
                     <img src='<%# FrontEndProdInfo.ProductImage(Convert.ToInt32(Eval("pkProductId"))) %>' width="40" height="40" />
                </ItemTemplate>
           </asp:TemplateField>

            <asp:TemplateField  InsertVisible="False"
                SortExpression="pkProductId">
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server"
                        Text='<%# Eval("pkProductId") %>' Visible="false">
                    </asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server"
                        Text='<%# Bind("pkProductId") %>' Visible="false">
                    </asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="lbInsert" ValidationGroup="Insert" CommandName="InsertRow"
                        runat="server">Insert 
                    </asp:LinkButton>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Product Name" SortExpression="Name">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"
                        Text='<%# Bind("strProductName") %>'>
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEditName" runat="server"
                        ErrorMessage="Name is a required field"
                        ControlToValidate="TextBox1"  ForeColor="Red" ValidationGroup="Update">
                    </asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server"
                        Text='<%# Bind("strProductName") %>'>
                    </asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvInsertName" runat="server"
                        ErrorMessage="Name is a required field"
                        ControlToValidate="txtName" Text="*" ForeColor="Red"
                        ValidationGroup="Insert">
                    </asp:RequiredFieldValidator>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ProductCategory" SortExpression="Gender">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server"
                        SelectedValue='<%# Bind("strProductCategory") %>'>
                        <asp:ListItem>Select Category</asp:ListItem>
                        <asp:ListItem>Shirts</asp:ListItem>
                        <asp:ListItem>Pants</asp:ListItem>
                          <asp:ListItem>Shoes</asp:ListItem>
                        <asp:ListItem>Socks</asp:ListItem>
                          <asp:ListItem>Sneakers</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvEditGender" runat="server"
                        ErrorMessage="Gender is a required field" Text="*"
                        ControlToValidate="DropDownList1" ForeColor="Red" ValidationGroup="Update"
                        InitialValue="Select Category">
                    </asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblProductCategory" runat="server"
                        Text='<%# Bind("strProductCategory") %>'>
                    </asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlInsertCategory" runat="server" CssClass="form-control">
                        <asp:ListItem>Select Category</asp:ListItem>
                              <asp:ListItem>Shirts</asp:ListItem>
                        <asp:ListItem>Pants</asp:ListItem>
                          <asp:ListItem>Shoes</asp:ListItem>
                        <asp:ListItem>Socks</asp:ListItem>
                          <asp:ListItem>Sneakers</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvInsertCategory" runat="server"
                        ErrorMessage="Gender is a required field" Text="*"
                        ControlToValidate="ddlInsertCategory" ForeColor="Red"
                        InitialValue="Select Category" ValidationGroup="Insert">
                    </asp:RequiredFieldValidator>
                </FooterTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="ProductDescription" SortExpression="Gender">
                <EditItemTemplate>
                    <br />
                    <asp:TextBox ID="txtDescription" runat="server"
                        Text='<%# Bind("strProductDescription") %>'>
                    </asp:TextBox>

                    <asp:RequiredFieldValidator ID="rfvEditDescription" runat="server"
                        ErrorMessage="Description is a required field" 
                        ControlToValidate="txtDescription" ForeColor="Red" ValidationGroup="Update">
                    </asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server"
                        Text='<%# Bind("strProductDescription") %>'>
                    </asp:Label>
                </ItemTemplate>
                <FooterTemplate>

                    <asp:TextBox ID="txtDescriptionInsert" runat="server" CssClass="form-control">
                    </asp:TextBox>

                    <asp:RequiredFieldValidator ID="rfvInsertDescription" runat="server"
                        ErrorMessage="Description is a required field" Text="*"
                        ControlToValidate="txtDescriptionInsert" ForeColor="Red"
                        ValidationGroup="Insert">
                    </asp:RequiredFieldValidator>
                </FooterTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Price" SortExpression="Price">
                <EditItemTemplate>
                     <br />
                    <asp:TextBox ID="TextBox3" runat="server"
                        Text='<%# Bind("decProductPrice", "{0:C}") %>' ForeColor="Green"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEditCity" runat="server"
                        ErrorMessage="Price is a required field" Text="*"
                        ControlToValidate="TextBox3" ForeColor="Red" ValidationGroup="Update">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cmp" runat="server" ForeColor="Red" ControlToValidate="TextBox3" ErrorMessage="Must Be In Currency Format" Operator="DataTypeCheck" Type="Currency" ValidationGroup="Update">
                    </asp:CompareValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server"
                        Text='<%#  Bind("decProductPrice", "{0:c}") %>' ForeColor="Green"></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvInsertCity" runat="server"
                        ErrorMessage="Price is a required field" Text="*"
                        ControlToValidate="txtPrice" ForeColor="Red"
                        ValidationGroup="Insert">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" 
                        ErrorMessage="Must be currency" ControlToValidate="txtPrice" Type="Currency" 
                        Operator="DataTypeCheck"
                        ForeColor="Red" ValidationGroup="Insert">
                    </asp:CompareValidator>
                </FooterTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="FeaturedProduct" SortExpression="Price">
                <EditItemTemplate>
                    <asp:RadioButtonList ID="rdoFeaturedEdit" runat="server" RepeatDirection="Horizontal" SelectedValue='<%# Bind("blnFeaturedProduct") %>'  >
                        <asp:ListItem Value="False">No</asp:ListItem>
                        <asp:ListItem Value="True">Yes</asp:ListItem>
                    </asp:RadioButtonList>               
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server"
                        Text='<%# Bind("blnFeaturedProduct") %>' ></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                     <asp:RadioButtonList ID="rdoFeaturedInsert" runat="server"  RepeatDirection="Horizontal">
                        <asp:ListItem Value="True">No</asp:ListItem>
                        <asp:ListItem Value="False">Yes</asp:ListItem>
                    </asp:RadioButtonList>        
                </FooterTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Inventory" SortExpression="Price">
                <EditItemTemplate>
                    <asp:TextBox ID="txtInventory" runat="server" Text='<%# Bind("intProductInventory") %>'></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Must Be Whole Numbers" ControlToValidate="txtInventory" ForeColor="Red" Type="Integer" Operator="DataTypeCheck" ValidationGroup="Update"></asp:CompareValidator>        
                    <asp:RequiredFieldValidator ID="reqInventory" runat="server" ErrorMessage="Inventory Is Required" ForeColor="Red" ControlToValidate="txtInventory" ValidationGroup="Update"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                     <br />
                    <asp:Label ID="Label5" runat="server"
                        Text='<%# Bind("intProductInventory") %>' ></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <br />
                    <asp:TextBox ID="txtInventoryInsert" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqInventoryInsert" runat="server" ErrorMessage="Inventory Is Required" ForeColor="Red" ControlToValidate="txtInventoryInsert" ValidationGroup="Insert"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cmpInsertInventory" runat="server" ErrorMessage="Must Be Whole Numbers" ControlToValidate="txtInventoryInsert" ForeColor="Red" Type="Integer" Operator="DataTypeCheck" ValidationGroup="Insert"></asp:CompareValidator>        
                </FooterTemplate>
            </asp:TemplateField>

        </Columns>

        <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>

        <FooterStyle BackColor="White" ForeColor="#333333"></FooterStyle>

        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White"></HeaderStyle>

        <PagerStyle HorizontalAlign="Center" BackColor="#336666" ForeColor="White"></PagerStyle>

        <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>

        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White"></SelectedRowStyle>

        <SortedAscendingCellStyle BackColor="#F7F7F7"></SortedAscendingCellStyle>

        <SortedAscendingHeaderStyle BackColor="#487575"></SortedAscendingHeaderStyle>

        <SortedDescendingCellStyle BackColor="#E5E5E5"></SortedDescendingCellStyle>

        <SortedDescendingHeaderStyle BackColor="#275353"></SortedDescendingHeaderStyle>
    </asp:GridView>


    <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="Insert" 
    ForeColor="Red" runat="server" />
<asp:ValidationSummary ID="ValidationSummary2" ForeColor="Red" 
    runat="server" />

                        <br />

                                            <div class="row" style="padding-top:10px;">
                                                             <div class="col-sm-6"  >

                        <h4>Upload Multiple Images</h4>
                                                <br />


    <asp:DropDownList ID="ddlNumberOfImagesToUpload" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNumberOfImagesToUpload_SelectedIndexChanged" CssClass="form-control">
        <asp:ListItem>Select Number Of Images To Upload</asp:ListItem>
        <asp:ListItem>One</asp:ListItem>
        <asp:ListItem>Two</asp:ListItem>
        <asp:ListItem>Three</asp:ListItem>
        <asp:ListItem>Four</asp:ListItem>
        <asp:ListItem>Five</asp:ListItem>
        <asp:ListItem>Six</asp:ListItem>
        <asp:ListItem>Seven</asp:ListItem>
        <asp:ListItem>Eight</asp:ListItem>
        <asp:ListItem>Nine</asp:ListItem>
        <asp:ListItem>Ten</asp:ListItem>
    </asp:DropDownList>
                                                <br />
                        Enter Product Id

    <asp:TextBox ID="txtProductId" runat="server" CssClass="form-control"></asp:TextBox>
                                                <br />

  
                                                <br />

                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtProductId" ErrorMessage="Must be a whole number" Type="Integer" Operator="DataTypeCheck" ValidationGroup="UploadMultipleImages" ForeColor="Red"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required Field" ControlToValidate="txtProductId" ValidationGroup="UploadMultipleImages" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <br />


    <asp:Button ID="btnUploadImages" runat="server" Text="Upload Multiple Images" OnClick="btnUploadImages_Click"  ValidationGroup="UploadMultipleImages"/>


                                                <br />

   <asp:PlaceHolder ID="phImageUploaders" runat="server">
    </asp:PlaceHolder>

        <br />

                                                <br />


                                                                 </div>
                                     <h4>Upload Primary Image</h4>

                                                    <div class="col-sm-6"  >

                        <asp:FileUpload ID="filUploadImage" runat="server" />
                        <asp:TextBox ID="txtPrimaryImageProductId" runat="server" CssClass="form-control"></asp:TextBox>

                        <br />

                         <asp:Button ID="btnUploadMainImage" runat="server" Text="Upload Images" OnClick="btnUploadMainImage_Click"  ValidationGroup="UploadOneImage"/>

                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPrimaryImageProductId" ErrorMessage="Must be a whole number" Type="Integer" Operator="DataTypeCheck" ValidationGroup="UploadOneImage" ForeColor="Red"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required Field" ControlToValidate="txtPrimaryImageProductId" ValidationGroup="UploadOneImage" ForeColor="Red"></asp:RequiredFieldValidator>
                    
                                                        


                                                            <br />


               

                        </div>
                                                </div>
                        </div>
</div>
</div>






</asp:Content>


