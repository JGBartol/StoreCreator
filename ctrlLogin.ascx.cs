using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class ctrlLogin : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

       

    }



    public event LogInEventHandler LogInChanged;

    protected virtual void OnLoggedInChanged(LogInEventArgs e)
    {
        if (LogInChanged != null)
        {
            LogInChanged(this, e);
        }
    }

    protected bool IsValidationGroupValid()
    {
        foreach (BaseValidator validator in this.Page.Validators)
        {
            if (validator.ValidationGroup == "ctrlLogIn")
            {

                bool IsValid = validator.IsValid;

                if (IsValid)
                {
                    validator.Validate();
                    IsValid = validator.IsValid;
                    validator.IsValid = true;
                }
                if (!IsValid)
                {
                    return false;
                }
            }
        }

        return true;
    }
    protected void btnLogIn_Click(object sender, EventArgs e)
    {
        
            string Message;

            int UserId = RegisterAndLogin.AuthenticaUser(txtUserName.Text.Trim(), txtPassWord.Text.Trim(), out Message);

            if (UserId != 0)
            {
                LogInEventArgs args = new LogInEventArgs(txtUserName.Text.Trim(), true, UserId, Message);
                OnLoggedInChanged(args);

            }
            else
            {
                LogInEventArgs args = new LogInEventArgs(txtUserName.Text.Trim(), false, UserId, Message);
                OnLoggedInChanged(args);

            }

            lblStatus.Text = Message;
            lblStatus.ForeColor = System.Drawing.Color.Red;
            lblStatus.Font.Bold = true;
    }
}


public class LogInEventArgs : EventArgs
{
    private string _UserName;
    private bool _IsAuthenticated;
    private int _UserId;
    private string _Message;

    public LogInEventArgs(string UserName, bool IsAuthenticated, int UserId, string Message)
    {
        this._UserName = UserName;
        this._IsAuthenticated = IsAuthenticated;
        this._UserId = UserId;
        this._Message = Message;
    }

    public string userName
    {
        get { return this._UserName; }
    }
    public bool isAuthenticated
    {
        get { return _IsAuthenticated; }
    }
    public int UserId
    {
        get { return _UserId; }
    }
    public string message
    {
        get { return _Message; }
    }
}

public delegate void LogInEventHandler(object sender, LogInEventArgs e);
