using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Specialized;

namespace StoreCreator
{
    public partial class FrontEndProducts : System.Web.UI.Page
    {
     
        protected void Page_Load(object sender, EventArgs e)
        {
            CloseReply.ServerClick += new EventHandler(btn_CloseReply);
            Session["UserId"] = 1;

          
            /*
            if (Request.Cookies["ProductVisited"] != null)
            {
                HttpCookie c = HttpContext.Current.Request.Cookies["ProductVisited"];

                if (c.HasKeys)
                {
                    NameValueCollection nv = c.Values;

                    foreach (string val in nv.AllKeys)
                    {
                        Response.Write(val);
                    }
                }
            }
            */

             if (Request.QueryString["ProductId"] != null)
             {
                 if (!this.Page.IsPostBack)
                 {

                     if (Session["UserId"] == null)
                     {
                         Statistics.InsertMemberPageVisit("FrontEndProducts.aspx", 0, Request.QueryString["ProductId"]);
                     }
                     else
                     {
                         Statistics.InsertMemberPageVisit("FrontEndProducts.aspx", (int)Session["UserId"], Request.QueryString["ProductId"]);
                     }
                 }
                 
                 int ProductId = Convert.ToInt32(Request.QueryString["ProductId"]);
               

             

                
                 if (Request.QueryString["BulletinBoardMethod"] != null)
                 {
                     if (Session["UserId"] != null)
                     {
                         if (Request.QueryString["BulletinBoardMethod"] == "LikeParentBoardItem")
                         {
                             FrontEndProdInfo.LikeBulletinBoard(Convert.ToInt32(Request.QueryString["BulletinId"]), true);
                             // pnlUserBulletin.Visible = true;
                             //   pnlBulletinWasLiked.Visible = true;
                             //  litBulletinBoard.Text = UserInformationClass.GetUserBulletinBoard((int)Session["UserId"], true).ToString();


                         }
                         else if (Request.QueryString["BulletinBoardMethod"] == "LikeChildBoardItem")
                         {
                             FrontEndProdInfo.LikeBulletinBoard(Convert.ToInt32(Request.QueryString["BulletinId"]), false);

                         }
                         else if (Request.QueryString["BulletinBoardMethod"] == "Reply")
                         {

                             GetModal("ReplyBBModal");
                         }
                     }
                     else
                     {
                         //pnlUserLogIn.Visible = true;
                     }
                 }
                

                 litBB.Text = FrontEndProdInfo.GetBulletinBoard(Convert.ToInt32(Request.QueryString["ProductId"])).ToString();


                 SqlParameter paramProductId = new SqlParameter("@ProductId", ProductId);
                 List<SqlParameter> parameters = new List<SqlParameter>();
                 parameters.Add(paramProductId);

                 DataTable dTable =    ProductInformation.GetGenericDataTablesUsingParams("spSelectProductByProductId", parameters);
                 lvProduct.DataSource = dTable;

                 if (!this.Page.IsPostBack)
                 {
                     if (dTable.Rows.Count > 0)
                     {
                         Statistics.InsertProductVisitedCookie(dTable.Rows[0]["strProductDescription"].ToString(), dTable.Rows[0]["strGender"].ToString(), Convert.ToDecimal(dTable.Rows[0]["decProductPrice"]));
                     }
                 }


                 List<SqlParameter> parametersReviews = new List<SqlParameter>();
                 parametersReviews.Add(new SqlParameter("@ProductId", ProductId));

                 DataTable dTableReviews = ProductInformation.GetGenericDataTablesUsingParams("spSelectProductReviews", parametersReviews);
                 lvReviews.DataSource = dTableReviews;

                 if (dTableReviews.Rows.Count > 0)
                 {
                     int TotalRows = dTableReviews.Rows.Count;

                     string AverageRating = dTableReviews.Rows[TotalRows - 1]["AverageRating"].ToString();
                     lblAverageRating.Text = AverageRating;
                 }

                 List<string> ProductImages = FrontEndProdInfo.SelectProductImages(ProductId);

                 if (ProductImages.Count > 0)
                 {
                     pnlProductImages.Visible = true;

                     foreach (string ProductImage in ProductImages)
                     {

                         phImages.Controls.Add(new Image() { ImageUrl = ProductImage, Width = Unit.Pixel(100), Height = Unit.Pixel(100) });
                         phImages.Controls.Add(new Literal() { Text = "&nbsp&nbsp" });
                     }
                 }
                 else
                 {
                     pnlProductImages.Visible = false;
                 }

                this.Page.DataBind();

            }

            // ctrlLogin.LogInChanged += new LogInEventHandler(LogInEvent);


        }

        private void btn_CloseReply(object sender, EventArgs e)
        {
            Response.Redirect("FrontEndProducts.aspx?ProductId=" + Request.QueryString["ProductId"]);

        }
       

        protected void btnPutInCart_Click(object sender, EventArgs e)
        {

            if (ddlQuantityToAdd.SelectedIndex != 0)
            {
                int qString;

                if (Request.QueryString["ProductId"] != null && Session["UserId"] != null && int.TryParse(Request.QueryString["ProductId"], out qString))
                {

                    List<int> ProductIds = new List<int>();

                    if (Session["ShoppingCart"] != null)
                    {
                         ProductIds = (List<int>)Session["ShoppingCart"]; 
                    }
                
                    for (int i = 0; i < ddlQuantityToAdd.SelectedIndex; i++)
                    {
                        
                           ProductIds.Add(qString);
                           Session["ShoppingCart"] = ProductIds;                                
                    }
                 
                    try
                    {
                        LoggingUserActions.LogUserProductToCart((int)Session["UserId"], Convert.ToInt32(Request.QueryString["ProductId"]), ddlQuantityToAdd.SelectedIndex);
                        Response.Redirect("FrontEndProducts.aspx?ProductId=" + Request.QueryString["ProductId"]);
                    }
                    catch (Exception error)
                    {
                        throw new ApplicationException(error.Message);
                    }
                }
            }                       
        }

        protected void btnFavoriteProduct_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                int qString;
                if (Request.QueryString["ProductId"] != null && Session["UserId"] != null && int.TryParse(Request.QueryString["ProductId"], out qString))
                {
                    List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@UserId", Value=(int)Session["UserId"] },
                    new SqlParameter(){ParameterName="@ProductId", Value= qString }
                };

                    FrontEndProdInfo.InsertGenericMethod(parameters.ToArray(), "spInsertProductLike");

                }
            }
            else
            {
                Response.Write("Must Be Logged In");
            }
        }

        protected void btnCreateReview_Click(object sender, EventArgs e)
        {
            if(Request.QueryString["ProductId"] != null && Session["UserId"] != null)
            {

                FrontEndProdInfo.CreateReview((int)Session["UserId"], Convert.ToInt32(Request.QueryString["ProductId"]), txtReviewTitle.Text.Trim(), txtReviewBody.Text.Trim(),  3);

                List<SqlParameter> parametersReviews = new List<SqlParameter>();
                parametersReviews.Add(new SqlParameter("@ProductId", Convert.ToInt32(Request.QueryString["ProductId"])));

                DataTable dTableReviews = ProductInformation.GetGenericDataTablesUsingParams("spSelectProductReviews", parametersReviews);
                lvReviews.DataSource = dTableReviews;

                if (dTableReviews.Rows.Count > 0)
                {
                    int TotalRows = dTableReviews.Rows.Count;

                    string AverageRating = dTableReviews.Rows[TotalRows - 1]["AverageRating"].ToString();
                    lblAverageRating.Text = AverageRating;
                }

            }

        }

        protected void btnReviewModal_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                GetModal("ReviewModal");
            }
            else
            {
               // pnlUserLogIn.Visible = true;
            }
        }

        protected void btnPostToBulletin_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                GetModal("BulletinModal");
            }
            else
            {
              //  pnlUserLogIn.Visible = true;
            }
        }

        protected void btnPostBulletin_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                FrontEndProdInfo.PostToProductBulletinBoard((int)Session["UserId"], Convert.ToInt32(Request.QueryString["ProductId"]), txtBulletinTitle.Text, txtBulletinMessage.Text, true, 0);
            }
            else
            {
               // pnlUserLogIn.Visible = true;
            }
        }

        private void GetModal(string ModalId)
        {
            string jquery = "$('#" + ModalId + "').modal('show');";
            ClientScript.RegisterStartupScript(typeof(Page), "a key",
           "<script type=\"text/javascript\">" + jquery + "</script>");
        }

  

        protected void btnSendEmailToFriend_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                GetModal("modalSendEmail");
            }
            else
            {
              //  pnlUserLogIn.Visible = true;
            }
        }

       
    
        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                EmailUsers.SendProductEmailToFriend(txtEmailAddress.Text, txtToName.Text, txtSubject.Text, Convert.ToInt32(Request.QueryString["ProductId"]), txtFirstName.Text, txtLastName.Text);
            }
            else
            {
              //  pnlUserLogIn.Visible = true;
            }
        }

        protected void btnReplyToBulletin_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                FrontEndProdInfo.PostToProductBulletinBoard((int)Session["UserId"], Convert.ToInt32(Request.QueryString["ProductId"]), txtBulletinTitle.Text, txtBulletinMessage.Text, false, Convert.ToInt32(Request.QueryString["BulletinId"]));
                Response.Redirect("FrontEndProducts.aspx?ProductId=" + Request.QueryString["ProductId"]);
            }
            else
            {
              //  pnlUserLogIn.Visible = true;
            }
        }


     

  

       
     
    }
}