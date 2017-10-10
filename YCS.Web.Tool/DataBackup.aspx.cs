using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using YCS.Common;

namespace YCS.Web.Tool
{
    public partial class DataBackup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsLocal) Response.End();

            Backup();
        }

        private void Backup()
        {
            string sql_name = "Select Name From Master..SysDataBases Where DbId=(Select Dbid From Master..SysProcesses Where Spid = @@spid)";
            using (SqlDataReader dr = Config.SqlHelper().GetDataReader(CommandType.Text, sql_name, null))
            {
                if (dr.Read())
                {
                    string strBackupDir = Server.MapPath("/App_Data/");
                    string strDatabaseName = dr[0].ToString();
                    IOHelper.FolderCheck(strBackupDir);
                    string strBackupPath = strBackupDir + strDatabaseName + "_mssql_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak";
                    string strBackupSql = "BACKUP DATABASE [" + strDatabaseName + "] TO DISK = N'" + strBackupPath + "' WITH NOFORMAT, NOINIT,  NAME = N'" + strDatabaseName + "', SKIP, REWIND, NOUNLOAD,  STATS = 10 ";
                    try
                    {
                        Config.SqlHelper().ExecuteSql(CommandType.Text, strBackupSql, null);
                        Config.ShowEnd("备份成功!");
                    }
                    catch (Exception ex)
                    {
                        Config.ShowEnd(ex.Message);
                    }
                }
            }
        }
    }
}
