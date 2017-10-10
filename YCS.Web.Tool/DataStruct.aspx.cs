using System;
using System.Data;
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
using System.Data.OleDb;

namespace YCS.Web.Tool
{
    public partial class DataStruct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string act = Config.Request(Request.QueryString["act"],"");
            if (!Request.IsLocal && !act.Equals("s")) Response.End();

            if (!IsPostBack)
            {
                TableSql();
            }

        }

        #region Sql
        //±í
        private void TableSql()
        {
            //string sql = "select * from sysobjects where xtype='U' order by [name] asc";
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT");
            sql.Append(" (CASE WHEN a.colorder = 1 THEN d.name ELSE '' END) as Name,");
            sql.Append(" (CASE WHEN a.colorder = 1 THEN ISNULL(f.value, '') ELSE '' END) as Description");
            sql.Append(" FROM syscolumns a");
            sql.Append(" INNER JOIN sysobjects d");
            sql.Append(" ON a.id = d.id");
            sql.Append(" AND d.xtype = 'U'");
            sql.Append(" AND d.name <> 'sys.extended_properties'");
            sql.Append(" LEFT JOIN sys.extended_properties f");
            sql.Append(" ON a.id = f.major_id");
            sql.Append(" AND f.minor_id = 0");
            sql.Append(" WHERE (CASE");
            sql.Append(" WHEN a.colorder = 1 THEN d.name ELSE ''");
            sql.Append(" END) <> ''");
            sql.Append(" order by d.name asc");
            DataTable dt = Config.SqlHelper().GetDataSet(CommandType.Text, sql.ToString(), null).Tables[0];
            repNavSql.DataSource = dt.DefaultView;
            repNavSql.DataBind();
            repTableSql.DataSource = dt.DefaultView;
            repTableSql.DataBind();
        }
        //ÁÐ
        protected void repTableSql_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater repCol = (Repeater)e.Item.FindControl("repCol");
            DataRowView drv = (DataRowView)e.Item.DataItem;
            string strTableName = drv["Name"].ToString();

            StringBuilder sql = new StringBuilder("SELECT ");
            sql.Append("[TableName] = D.NAME,");
            sql.Append("[ColumnSort] = A.COLORDER,");
            sql.Append("[FieldName] = A.NAME,");
            sql.Append("[IsIdentity] = CASE WHEN COLUMNPROPERTY(A.ID,A.NAME, 'ISIDENTITY ')=1 THEN '¡Ì' ELSE ' ' END,");
            sql.Append("[IsPrimaryKey] = CASE WHEN EXISTS (SELECT 1 FROM SYSOBJECTS WHERE XTYPE = 'PK ' AND  PARENT_OBJ = A.ID AND ");
            sql.Append("NAME IN (SELECT NAME FROM SYSINDEXES WHERE ");
            sql.Append("INDID IN (SELECT INDID FROM SYSINDEXKEYS WHERE ID = A.ID AND  COLID = A.COLID) ");
            sql.Append(")) THEN '¡Ì' ELSE ' ' END,");
            sql.Append("[DataType] = B.NAME,");
            sql.Append("[Length] = A.LENGTH,");
            sql.Append("[DecimalDigit] = ISNULL(COLUMNPROPERTY(A.ID,A.NAME, 'SCALE '),0),");
            sql.Append("[IsNull] = CASE WHEN A.ISNULLABLE=1 THEN '¡Ì' ELSE ' ' END,");
            sql.Append("[DefaultValue] = ISNULL(E.TEXT, ' '),");
            sql.Append("[ColumnDescription] = ISNULL(G.[VALUE], ' ') ");
            sql.Append("FROM SYSCOLUMNS A ");
            sql.Append("LEFT JOIN SYSTYPES B ON A.XUSERTYPE = B.XUSERTYPE ");
            sql.Append("INNER JOIN SYSOBJECTS D ON A.ID = D.ID AND D.XTYPE = 'U ' AND D.NAME = '" + strTableName + "'");
            sql.Append("LEFT JOIN SYSCOMMENTS E ON A.CDEFAULT = E.ID ");
            sql.Append("LEFT JOIN sys.extended_properties G ON A.ID = G.major_id AND A.COLID = G.minor_id ");
            sql.Append("LEFT JOIN sys.extended_properties F ON D.ID = F.major_id AND F.minor_id = 0 ");
            sql.Append("ORDER BY D.NAME,A.COLORDER");
            DataTable dt = Config.SqlHelper().GetDataSet(CommandType.Text, sql.ToString(), null).Tables[0];
            repCol.DataSource = dt.DefaultView;
            repCol.DataBind();
        }
        #endregion
    }
}
