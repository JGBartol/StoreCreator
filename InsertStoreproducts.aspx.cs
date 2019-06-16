using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Data.SqlClient;

namespace StoreCreator
{
    public partial class InsertStoreproducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserId"] = 1;

            if(!this.Page.IsPostBack)
            {
                ProductInformation prod = new ProductInformation();
                gvInsertProducts.DataSource = prod.dsGetProductsByStoreId((int)Session["UserId"]);

                this.Page.DataBind();

             

                pnlImageUpload.Visible = false;
                pnlNumberOfImages.Visible = false;
            }

            for (int i = 0; i < ddlNumberOfImagesToUpload.SelectedIndex; i++)
            {
                phImageUploaders.Controls.Add(new FileUpload() { ID = "Image" + i.ToString() });
            }
        }

        protected void gvInsertProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
                switch (e.CommandName)
                {
                    case "EditRow":

                        int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                        gvInsertProducts.EditIndex = rowIndex;

                        GetData();
                        TextBox t = ((TextBox)gvInsertProducts.Rows[rowIndex].FindControl("TextBox3"));
                        t.Text = t.Text.Remove(0, 1);


                        break;

                    case "CancelUpdate":

                        gvInsertProducts.EditIndex = -1;

                        GetData();


                        break;

                    case "UpdateRow":

                        this.Page.Validate("Update");

                        if (this.Page.IsValid)
                        {
                            int RowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;

                            int productId = Convert.ToInt32(e.CommandArgument);
                            string ProductName = ((TextBox)gvInsertProducts.Rows[RowIndex].FindControl("TextBox1")).Text;
                            string category = ((DropDownList)gvInsertProducts.Rows[RowIndex].FindControl("DropDownList1")).SelectedValue.ToString();
                            decimal price = Convert.ToDecimal(((TextBox)gvInsertProducts.Rows[RowIndex].FindControl("TextBox3")).Text);
                            string desc = ((TextBox)gvInsertProducts.Rows[RowIndex].FindControl("txtDescription")).Text;
                            bool IsFeatured = Convert.ToBoolean(((RadioButtonList)gvInsertProducts.Rows[RowIndex].FindControl("rdoFeaturedEdit")).SelectedIndex);
                            int InventoryUpdate = Convert.ToInt32(((TextBox)gvInsertProducts.Rows[RowIndex].FindControl("txtInventory")).Text);


                            ProductInformation.UpdateProduct(productId, category, ProductName, desc, price, IsFeatured, InventoryUpdate);

                            gvInsertProducts.EditIndex = -1;

                            GetData();
                        }

                        break;

                    case "DeleteRow":



                        ProductInformation productsTwo = new ProductInformation();
                        productsTwo.DeleteProduct(Convert.ToInt32(e.CommandArgument));

                        GetData();



                        break;


                    case "InsertRow":

                        this.Page.Validate("Insert");
                        if (this.Page.IsValid)
                        {
                            if (Session["UserId"] != null)
                            {
                                string ProductNameInsert = ((TextBox)gvInsertProducts.FooterRow.FindControl("txtName")).Text;
                                string ProductCategory = ((DropDownList)gvInsertProducts.FooterRow.FindControl("ddlInsertCategory")).SelectedItem.ToString();
                                string ProductDescription = ((TextBox)gvInsertProducts.FooterRow.FindControl("txtDescriptionInsert")).Text;
                                decimal ProductPrice = Convert.ToDecimal(((TextBox)gvInsertProducts.FooterRow.FindControl("txtPrice")).Text);
                                bool IsFeaturedInsert = Convert.ToBoolean(((RadioButtonList)gvInsertProducts.FooterRow.FindControl("rdoFeaturedInsert")).SelectedIndex);
                                int InventoryInsert = Convert.ToInt32(((TextBox)gvInsertProducts.FooterRow.FindControl("txtInventoryInsert")).Text);

                                ProductInformation.InsertProduct(ProductCategory, ProductNameInsert, ProductDescription, ProductPrice, IsFeaturedInsert, InventoryInsert, (int)Session["UserId"]);

                                GetData();
                            }
                        }


                        break;
                }
            
        }

        private void GetData()
        {

            List<SqlParameter> Params = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@StoreId", Value = (int)Session["UserId"]}
            };
            ProductInformation productsThree = new ProductInformation();
            gvInsertProducts.DataSource = productsThree.GetGenericDataSet(Params, "spSelectProductsByStoreId");
            gvInsertProducts.DataBind();

        }

        protected void ddlNumberOfImagesToUpload_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }

        protected void btnUploadImages_Click(object sender, EventArgs e)
        {
            DataTable dtImages = new DataTable();
            dtImages.Columns.Add("ImageName", typeof(string));
            dtImages.Columns.Add("ImageSize", typeof(int));
            dtImages.Columns.Add("ImageBytes", typeof(byte[]));


            for (int i = 0; i < ddlNumberOfImagesToUpload.SelectedIndex ; i++)
            {
                FileUpload f = ((FileUpload)phImageUploaders.FindControl("Image" + i.ToString()));
                HttpPostedFile postedFile = f.PostedFile;
                string FileName = postedFile.FileName;
                int Size = postedFile.ContentLength;

                Stream str = postedFile.InputStream;

                using (str)
                {
                   using (BinaryReader binaryReader = new BinaryReader(str))
                   {
                      
                       Byte[] bytes = binaryReader.ReadBytes((int)str.Length);
                       dtImages.Rows.Add(FileName, Size, bytes);
                    
                   }
               }

                int NumberOfImagesUploaded = ProductInformation.InsertProductImages(dtImages, Convert.ToInt32(txtProductId.Text));
                lblNumberOfImagesUploaded.Text = NumberOfImagesUploaded.ToString();
                pnlNumberOfImages.Visible = true;
            }

        }

        protected void btnUploadMainImage_Click(object sender, EventArgs e)
        {
            bool IsSuccess = false;

            if (filUploadImage.HasFile && filUploadImage.PostedFile.ContentLength > 0)
            {
                HttpPostedFile postedFile = filUploadImage.PostedFile;


                Stream s = postedFile.InputStream;
                using (BinaryReader rdr = new BinaryReader(s))
                {
                    byte[] ImageData = rdr.ReadBytes((int)s.Length);
                    IsSuccess = ProductInformation.InsertMainProductImage(Convert.ToInt32(txtPrimaryImageProductId.Text), ImageData);
                }
            }

            if (IsSuccess)
            {
                pnlImageUpload.Visible = true;
                pnlNumberOfImages.Visible = false;
                GetData();
            }
        }
    }
}