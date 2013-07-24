using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sample.Repository.Interface;

namespace Sample.Web.WebForm.Category
{
    public partial class Details : System.Web.UI.Page
    {
        private ICategoryRepository _repository;

        public Details()
        {
            this._repository = Global.GetInstance<ICategoryRepository>();
        }

        private int categoryID;
        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }

        private Sample.Domain.Category instance;
        public Sample.Domain.Category Instance
        {
            get
            {
                if (this.instance == null)
                {
                    var category = this._repository.GetOne(this.CategoryID);
                    this.instance = category;
                }
                return instance;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.SetDefault();
            }
        }

        private void SetDefault()
        {
            int id;
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("List.aspx");
            }
            else
            {
                if (!int.TryParse(Request.QueryString["id"].Trim(), out id))
                {
                    Response.Redirect("List.aspx");
                }
                else
                {
                    this.CategoryID = id;
                    if (this.Instance == null)
                    {
                        Response.Redirect("List.aspx");
                    }
                    else
                    {
                        this.Label_CategoryID.Text = this.Instance.CategoryID.ToString();
                        this.TextBox_CategoryName.Text = this.Instance.CategoryName;
                        this.TextBox_Description.Text = this.Instance.Description;
                    }
                }
            }
        }

    }
}