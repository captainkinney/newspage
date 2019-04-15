using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.IO;

namespace NewsSite
{
    public partial class index : System.Web.UI.Page
    {
        protected string renderTable()
        {
            //Reads the URLS from the database and returns a string made up of HTML Table rows
            //=============================================
            string SqlConnectionString = ConfigurationManager.ConnectionStrings["myDatabase"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(SqlConnectionString); // create an instance of a connection object
            sqlCon.Open(); // open the connection to the database
                           //=============================================
            string commandString = "SELECT * FROM linkTable";
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader myDataReader = sqlCmd.ExecuteReader();
            //======================================================
            //while there are records - read them
            //use a Stringbuilder object - it makes concatenating strings easier
            StringBuilder myStringBuilder = new StringBuilder("");
            int row = 0;
            myStringBuilder.Append("<table align=\"center\" cellspacing=\"25\"");
            myStringBuilder.Append("<tr><td colspan = \"3\" ><h2> Welcome to my webpage. </h2></td></tr>");
            myStringBuilder.Append("<tr>");
            while (myDataReader.Read()) // loops all the returned records
            {
                myStringBuilder.Append("<td>");
                myStringBuilder.Append("<img src=\"images/");
                myStringBuilder.Append(myDataReader["iconName"]);
                myStringBuilder.Append("\" />");

                myStringBuilder.Append("<a href=\"");
                myStringBuilder.Append(myDataReader["url"].ToString());
                myStringBuilder.Append("\"> " + myDataReader["urlDisplayName"]);
                myStringBuilder.Append(" </a></td>");

                row++;
                if (row == 3)
                {
                    myStringBuilder.Append("</tr> <tr>");
                    row = 0;
                }
            }
            myStringBuilder.Append("</tr>");
            sqlCon.Close(); // close the connection
            return myStringBuilder.ToString();// return the rows as a string
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string name = Request.Form["txtName"].Trim();
            string url = Request.Form["txtUrl"].Trim();

            if (!(name.Equals("") && url.Equals("")))
            {
                uploadImage();
                insertSite(url, name);
            }
        }

        protected void insertSite(string url, string name)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            string SqlConnectionString = ConfigurationManager.ConnectionStrings["myDatabase"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(SqlConnectionString); // create an instance of a connection object
            sqlCon.Open(); // open the connection to the database

            string strFileName = oFile.PostedFile.FileName;
            strFileName = Path.GetFileName(strFileName);

            string sqlCommand = "INSERT INTO linkTable (iconName, urlDisplayName, url) VALUES ("
            + "'" + strFileName + "',"
            + "'" + name + "',"
            + "'" + url + "')";


            adapter.InsertCommand = new SqlCommand(sqlCommand, sqlCon);
            adapter.InsertCommand.ExecuteNonQuery();

            sqlCon.Dispose();
            sqlCon.Close();
        }

        protected void uploadImage()
        {
            string strFileName;
            string strFilePath;
            string strFolder;
            strFolder = Server.MapPath("./images/");

            // Retrieve the name of the file that is posted.

            strFileName = oFile.PostedFile.FileName;
            strFileName = Path.GetFileName(strFileName);
            if (oFile.Value != "")
            {
                // Create the folder if it does not exist.
                if (!Directory.Exists(strFolder))
                {
                    Directory.CreateDirectory(strFolder);
                }

                // Save the uploaded file to the server.
                strFilePath = strFolder + strFileName;

                if (File.Exists(strFilePath))
                {
                    lblUploadResult.Text = strFileName + " already exists on the server!";
                }
                else
                {
                    oFile.PostedFile.SaveAs(strFilePath);
                    lblUploadResult.Text = strFileName + " has been successfully uploaded.";
                }
            }
            else
            {
                lblUploadResult.Text = "Click 'Browse' to select the file to upload.";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}