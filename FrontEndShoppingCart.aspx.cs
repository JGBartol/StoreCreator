using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Security;

namespace StoreCreator
{
    public partial class FrontEndShoppingCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserId"] = 12;

            if (!this.Page.IsPostBack)
            {
                pnlCreditCard.Visible = false;
              
            }

            if (Session["ShoppingCart"] != null)
            {
                PopulateLV();

                if (!this.Page.IsPostBack)
                {
                    PopulateUserInformation((int)Session["UserId"]);
                }

                decimal TotalPrice = 0;

                foreach (ListViewItem Item in lvShpppingCart.Items)
                {
                    int Quantity = Convert.ToInt32(((Label)Item.FindControl("lblQuantity")).Text);
                    decimal FinalPrice = Convert.ToDecimal(((Label)Item.FindControl("lblFinalPrice")).Text);

                    TotalPrice += FinalPrice;
                }

                if (TotalPrice != 0)
                {
                    string strTotalPrice = TotalPrice.ToString();
                    lblTotalPrice.Text = "$ " + strTotalPrice.Remove(strTotalPrice.Length - 2, 2);
                }

            }
        }

        protected int GetQuantity(int ProductId)
        {

            List<int> ProductList = new List<int>();
            List<string> ProductIds = new List<string>();

             int i = 0;
             foreach (int Product in (List<int>)Session["ShoppingCart"])
             {

                 if (Product == ProductId)
                 {
                     i++;
                 }
             }

             return i;
        }



        protected void wizCheckOut_ActiveStepChanged(object sender, EventArgs e)
        {
            if (lvShpppingCart.Items.Count == 0)
            {
                wizCheckOut.Enabled = false;
            }
          

        }

        protected void wizCheckOut_CancelButtonClick(object sender, EventArgs e)
        {

        }

        protected void wizCheckOut_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (lvShpppingCart.Items.Count > 0)
            {
                if ((sender as Wizard).ActiveStepIndex == 1)
                {
                    Page.Validate("PersonalInfo");
                    if (!this.Page.IsValid)
                    {
                        e.Cancel = true;

                    }
                }
            }
            else
            {
                wizCheckOut.Enabled = false;
            }

        }

        protected void wizCheckOut_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {

        }

        protected void wizCheckOut_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
        {

        }

        protected void wizCheckOut_SideBarButtonClick(object sender, WizardNavigationEventArgs e)
        {

        }

        protected void rdoPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButtonList).SelectedIndex == 1)
            {
                pnlCreditCard.Visible = true;
            }
            else
            {
                pnlCreditCard.Visible = false;
            }
        }

        protected void btnSubmitPayment_Click(object sender, EventArgs e)
        {
            List<object> ClassesToIncludeInTransaction = new List<object>();

            if (rdoPaymentMethod.SelectedItem.Text == "Credit Card")
            {
                Page.Validate("CreditCard");
            }

            if (this.Page.IsValid)
            {

                    FrontEndProdInfo.CheckOutInfo p = new FrontEndProdInfo.CheckOutInfo()
                    {
                        UserId = (int)Session["UserId"],
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        UserEmail = txtUserEmail.Text,
                        Address = txtAddress.Text,
                        City = txtCity.Text,
                        State = ddlStates.SelectedItem.ToString(),
                        ZipCode = int.Parse(txtZipCode.Text),
                        TypeOfTransaction = rdoPaymentMethod.SelectedItem.ToString(),
                        
                    };

                    ClassesToIncludeInTransaction.Add(p);


                    foreach (ListViewItem Item in lvShpppingCart.Items)
                    {
                        int Quantity = Convert.ToInt32(((Label)Item.FindControl("lblQuantity")).Text);
                        decimal FinalPrice = Convert.ToDecimal(((Label)Item.FindControl("lblFinalPrice")).Text);
                        int ProductId =  Convert.ToInt32(((Label)Item.FindControl("lblProductId")).Text);
                        int StoreId = Convert.ToInt32(((Label)Item.FindControl("lblStoreId")).Text);
   
                        FrontEndProdInfo.TransactionLineItems LineItem = new FrontEndProdInfo.TransactionLineItems()
                        {
                            ProductId = ProductId,
                            Quantity = Quantity,
                            TransactionAmount = FinalPrice,
                            DateProcessed = DateTime.Now,
                            StoreId = StoreId                            
                        };

                        ClassesToIncludeInTransaction.Add(LineItem);
                    }



                    if (rdoTypeOfCard.SelectedItem != null)
                    {

                        string CardNumber = txt.Text + txt2.Text + txt3.Text + txt4.Text;
                        string encFile = Server.MapPath("~/") + "\\symmetric_key.config";

                        CryptoMethods.SymmetricAlgorithms.ProtectKey = true;
                        CryptoMethods.SymmetricAlgorithms.AlgorithmName = "DES";

                        if (!System.IO.File.Exists(encFile))
                        {
                            CryptoMethods.SymmetricAlgorithms.GenerateKey(encFile);
                        }

                        byte[] encCreditCardNumbers = CryptoMethods.SymmetricAlgorithms.EncryptData(CardNumber, encFile);


                        string CardHolderName = txtNameOnCardFirst.Text.Trim() + txtNameOnCardMiddle.Text.Trim() + txtNameOnCardLast.Text.Trim();

                        FrontEndProdInfo.CreditCardInfo c = new FrontEndProdInfo.CreditCardInfo();
                        c.CreditCardNumber = encCreditCardNumbers;
                        c.NameOnCardName = CardHolderName;
                        c.TypeOfCardName = rdoTypeOfCard.SelectedItem.ToString();
                        c.CreditCardExpiryDate = Convert.ToDateTime(ddlMonth.SelectedIndex.ToString() + "-01-" + ddlYear.SelectedValue.ToString());

                        ClassesToIncludeInTransaction.Add(c);
                    }

                    FrontEndProdInfo.InsertTransaction(ClassesToIncludeInTransaction);
                
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

            lvShpppingCart.DeleteItem(0);
            PopulateLV();
        }

        private void PopulateLV()
        {

            List<string> ProductIds = new List<string>();

            foreach (int Product in (List<int>)Session["ShoppingCart"])
            {
                if (!ProductIds.Contains(Product.ToString()))
                {
                    ProductIds.Add(Product.ToString());
                }
            }

           

            string t;
            DataSet ds = FrontEndProdInfo.SelectShoppingCart(ProductIds, out t);

            if (ds.Tables[0].Rows.Count > 0)
            {
                decimal dec = 0;

                foreach (DataRow d in ds.Tables[0].Rows)
                {
                     dec += Convert.ToDecimal(d["decProductPrice"]) * GetQuantity(Convert.ToInt32(d["pkProductId"]));
                }
            }

            lvShpppingCart.DataSource = ds;
            this.Page.DataBind();

        }

  
        protected void lvShpppingCart_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {

                List<int> ProductIds = new List<int>();

                foreach (int Product in (List<int>)Session["ShoppingCart"])
                {
                    ProductIds.Add(Product);
                    ProductIds.Remove(Convert.ToInt32(e.CommandArgument));
                    Session["ShoppingCart"] = ProductIds;
                }

                Response.Redirect("FrontEndShoppingCart.aspx");
            }
        }

        protected void lvShpppingCart_ItemDeleted(object sender, ListViewDeletedEventArgs e)
        {
           

        }

        protected void lvShpppingCart_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }

        private void PopulateUserInformation(int UserId)
        {
            DataSet dt = UserProfileItems.GetUserBasicInformation(UserId);

            if (dt.Tables[0].Rows.Count > 0)
            {
                txtFirstName.Text = dt.Tables[0].Rows[0]["strFirstName"].ToString();
                txtLastName.Text = dt.Tables[0].Rows[0]["strLastName"].ToString();
                txtAddress.Text = dt.Tables[0].Rows[0]["strAddress"].ToString();
                txtCity.Text = dt.Tables[0].Rows[0]["strCity"].ToString();
                ddlStates.SelectedValue = dt.Tables[0].Rows[0]["strState"].ToString();
                txtZipCode.Text = dt.Tables[0].Rows[0]["intZipCode"].ToString();
                txtUserEmail.Text = dt.Tables[0].Rows[0]["strEmail"].ToString();
            }

        }

    }

}