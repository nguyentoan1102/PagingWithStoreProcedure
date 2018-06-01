using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PaginInWebForms
{
    public partial class PagingIndex : System.Web.UI.Page
    {
        private string connectionString = "Data Source=TOANNGUYEN\\SQLEXPRESS;Initial Catalog=NORTHWND;User ID=sa;Password=admin123";
        private DataTable table;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetCustomersPageWise(1);
            }
        }

        private void GetCustomersPageWise(int pageIndex)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetCustomersPageWise", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                        cmd.Parameters.AddWithValue("@PageSize", int.Parse(ddlPageSize.SelectedValue));
                        cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                        cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                        con.Open();
                        IDataReader idr = cmd.ExecuteReader();

                        GridView1.DataSource = idr;
                        GridView1.DataBind();
                        idr.Close();
                        con.Close();

                        int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                        PopulatePager(recordCount, pageIndex);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private void PopulatePager(int recordCount, int currentPage)
        {
            double dblPageCount = (double)(recordCount / decimal.Parse(ddlPageSize.SelectedValue));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            List<ListItem> pages = new List<ListItem>();
            if (pageCount > 0)
            {
                pages.Add(new ListItem("First", "1", currentPage > 1));
                for (int i = 1; i <= pageCount; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
                pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
            }
            rptPager.DataSource = pages;
            rptPager.DataBind();
        }

        protected void PageSize_Changed(object sender, EventArgs e)
        {
            this.GetCustomersPageWise(1);
        }

        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            this.GetCustomersPageWise(pageIndex);
        }
    }
}