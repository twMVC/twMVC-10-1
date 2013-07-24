using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Repository;

namespace WebForm_MultiLayers_ADONET.Category
{
    public partial class List : System.Web.UI.Page
    {
        private CategoryRepository _repository = new CategoryRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.CategoryDataBind();
            }
        }

        private void CategoryDataBind()
        {
            var categories = this._repository.GetCategories();
            this.GridView1.DataSource = categories;
            this.GridView1.DataKeyNames = new string[] { "CategoryID" };
            this.GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                object primaryKey = GridView1.DataKeys[e.Row.RowIndex]["CategoryID"];
                HyperLink HyperLink_Details = e.Row.FindControl("HyperLink_Details") as HyperLink;

                int categoryID;
                if (int.TryParse(primaryKey.ToString(), out categoryID)
                    && HyperLink_Details != null)
                {
                    HyperLink_Details.NavigateUrl = string.Concat("Details.aspx?id=", categoryID.ToString());
                }
            }
        }
    }
}