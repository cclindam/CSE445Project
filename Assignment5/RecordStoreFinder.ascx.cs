using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment5
{
	public partial class WebUserControl1 : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				HttpCookie zipcodeCookie = Request.Cookies["LastZipcode"];
				if (zipcodeCookie != null)
				{
					IbIZipcode.Text = zipcodeCookie.Value; 
				}
			}
		}

		protected void btnFindStore_Click(object sender, EventArgs e)
		{
			string zipcode = IbIZipcode.Text;
			string storeName = IbIStorename.Text;

			RecordStoreService1.Service1Client client = new RecordStoreService1.Service1Client(); 

			try
			{
				HttpCookie zipcodeCookie = new HttpCookie("LastZipcode");
				zipcodeCookie.Value = zipcode;
				zipcodeCookie.Expires = DateTime.Now.AddDays(10); 
				string result = client.findNearestRecordStore(zipcode, storeName);
				txtResult.Text = result; 
			} finally
			{
				client.Close(); 
			}
		}
	}
}