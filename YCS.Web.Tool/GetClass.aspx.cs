using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using YCS.Common;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace YCS.Web.Tool
{
    /// <summary>
    /// 代码生成工具
    /// create by jimy
    /// </summary>
    public partial class GetClass : System.Web.UI.Page
    {
        #region 参数
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get { return txtProjectName.Text; }
        }
        public string AreaName
        {
            get { return txtAreaName.Text; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return drpTableName.SelectedValue; }
        }

        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName
        {
            get { return txtClassName.Text; }
        }

        /// <summary>
        /// 类说明
        /// </summary>
        public string ClassDesc
        {
            get { return txtClassDesc.Text; }
        }

        /// <summary>
        /// 类对象实例名
        /// </summary>
        public string ObjName
        {
            get { return txtObjName.Text; }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Author
        {
            get { return txtAuthor.Text; }
        }

        /// <summary>
        /// Model保存路径
        /// </summary>
        public string SaveModelPath
        {
            get { return Request.PhysicalApplicationPath.Replace("Web.Tool", "Model"); }
        }

        /// <summary>
        /// DAL保存路径
        /// </summary>
        public string SaveDALPath
        {
            get { return Request.PhysicalApplicationPath.Replace("Web.Tool", "DAL"); }
        }

        /// <summary>
        /// BLL保存路径
        /// </summary>
        public string SaveBLLPath
        {
            get { return Request.PhysicalApplicationPath.Replace("Web.Tool", "BLL"); }
        }

        /// <summary>
        /// ClassFactory保存路径
        /// </summary>
        public string SaveClassFactoryPath
        {
            get { return Request.PhysicalApplicationPath.Replace("Web.Tool", "BLL"); }
        }

        /// <summary>
        /// DbUtility保存路径
        /// </summary>
        public string SaveDbUtilityPath
        {
            get { return Request.PhysicalApplicationPath.Replace("Web.Tool", "Model"); }
        }

        /// <summary>
        /// 控制器/视图保存路径
        /// </summary>
        public string SavePath
        {
            get { return Request.PhysicalApplicationPath; }
        }
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsLocal) Response.End();

            if (!IsPostBack)
            {
                GetDataTable();
            }
        }
        #endregion

        #region 列出数据表
        /// <summary>
        /// 列出数据表
        /// </summary>
        private void GetDataTable()
        {
            #region Sql
            string sql = "select * from sysobjects where xtype='U' order by [name] asc";
            DataTable dt = Config.SqlHelper().GetDataSet(CommandType.Text, sql, null).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                drpTableName.Items.Add(new ListItem(dr["name"].ToString(), dr["name"].ToString()));
            }
            #endregion
        }
        #endregion

        #region 选择表
        /// <summary>
        /// 选择表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //取类名
            string strTable = drpTableName.SelectedValue;
            string strClassName = CreateCode.GetClassName(strTable);
            txtClassName.Text = strClassName;

            //取实例化名
            txtObjName.Text = CreateCode.GetObjName(strClassName);
        }
        #endregion

        #region 生成代码
        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, CommandEventArgs e)
        {
            CreateCode crCode = new CreateCode();
            crCode.ColumnName = "FieldName";
            crCode.DataType = "DataType";
            crCode.Length = "Length";
            crCode.ColumnDesc = "ColumnDescription";
            crCode.TableName = "[" + TableName + "]";
            crCode.ProjectName = ProjectName;
            crCode.AreaName = AreaName;
            crCode.ClassName = ClassName;
            crCode.ClassDesc = ClassDesc;
            crCode.ObjName = ObjName;
            crCode.Author = Author;
            crCode.SaveModelPath = SaveModelPath;
            crCode.SaveDALPath = SaveDALPath;
            crCode.SaveBLLPath = SaveBLLPath;
            crCode.SaveClassFactoryPath = SaveClassFactoryPath;
            crCode.SaveDbUtilityPath = SaveDbUtilityPath;
            crCode.SavePath = SavePath;
            crCode.DataUsing = "System.Data.SqlClient";
            crCode.DataConn = "Config.SqlHelper()";
            crCode.DataPara = "SqlParameter";
            crCode.DataParaNew = "new SqlParameter";
            crCode.DataReader = "SqlDataReader";
            crCode.Transaction = "SqlTransaction";

            switch (e.CommandName)
            {
                case "Save":
                    CreateOne(crCode, TableName);
                    break;
                case "Factory":
                    CreateFactory(crCode);
                    break;
                case "Context":
                    CreateContext(crCode);
                    break;
                case "Model":
                    CreateAllModel(crCode);
                    break;
                case "Controllers":
                    CreateAllControllers(crCode);
                    break;
                default:
                    break;
            }
            errMsg.Text = "生成成功!";
        }

        #region 获取DataTable
        private DataTable GetFdTable(string strTableName)
        {
            //列
            StringBuilder sql = new StringBuilder("SELECT ");
            sql.Append("[TableName] = D.NAME,");
            sql.Append("[ColumnSort] = A.COLORDER,");
            sql.Append("[FieldName] = A.NAME,");
            sql.Append("[IsIdentity] = CASE WHEN COLUMNPROPERTY(A.ID,A.NAME, 'ISIDENTITY ')=1 THEN '√' ELSE ' ' END,");
            sql.Append("[IsPrimaryKey] = CASE WHEN EXISTS (SELECT 1 FROM SYSOBJECTS WHERE XTYPE = 'PK ' AND  PARENT_OBJ = A.ID AND ");
            sql.Append("NAME IN (SELECT NAME FROM SYSINDEXES WHERE ");
            sql.Append("INDID IN (SELECT INDID FROM SYSINDEXKEYS WHERE ID = A.ID AND  COLID = A.COLID) ");
            sql.Append(")) THEN '√' ELSE ' ' END,");
            sql.Append("[DataType] = B.NAME,");
            sql.Append("[Length] = A.LENGTH,");
            sql.Append("[DecimalDigit] = ISNULL(COLUMNPROPERTY(A.ID,A.NAME, 'SCALE '),0),");
            sql.Append("[IsNull] = CASE WHEN A.ISNULLABLE=1 THEN '√' ELSE ' ' END,");
            sql.Append("[DefaultValue] = ISNULL(E.TEXT, ' '),");
            sql.Append("[ColumnDescription] = ISNULL(G.[VALUE], ' ') ");
            sql.Append("FROM SYSCOLUMNS A ");
            sql.Append("LEFT JOIN SYSTYPES B ON A.XUSERTYPE = B.XUSERTYPE ");
            sql.Append("INNER JOIN SYSOBJECTS D ON A.ID = D.ID AND D.XTYPE = 'U ' AND D.NAME = '" + strTableName + "'");
            sql.Append("LEFT JOIN SYSCOMMENTS E ON A.CDEFAULT = E.ID ");
            sql.Append("LEFT JOIN sys.extended_properties G ON A.ID = G.major_id AND A.COLID = G.minor_id ");
            sql.Append("LEFT JOIN sys.extended_properties F ON D.ID = F.major_id AND F.minor_id = 0 ");
            DataSet ds = Config.SqlHelper().GetDataSet(CommandType.Text, sql.ToString(), null);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        private DataTable GetTbTable()
        {
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
            DataSet ds = Config.SqlHelper().GetDataSet(CommandType.Text, sql.ToString(), null);
            DataTable dt = ds.Tables[0];
            return dt;
        }
        #endregion

        #region 生成单个
        private void CreateOne(CreateCode crCode, string strTableName)
        {
            DataTable dt = GetFdTable(strTableName);
            crCode.CreateStart(dt);
        }
        #endregion

        #region 生成工厂类
        private void CreateFactory(CreateCode crCode)
        {
            DataTable dt = GetTbTable();
            crCode.CreateClassFactory(dt);
        }
        #endregion

        #region 生成上下文类
        private void CreateContext(CreateCode crCode)
        {
            DataTable dt = GetTbTable();
            crCode.CreateDbUtility(dt);
        }
        #endregion

        #region 生成所有Model/DAL/BLL
        private void CreateAllModel(CreateCode crCode)
        {
            DataTable dt = GetTbTable();
            foreach (DataRow dr in dt.Rows)
            {
                DataTable dtTemp = GetFdTable(dr["Name"].ToString());

                crCode.TableName = "[" + dr["Name"].ToString() + "]";
                crCode.ClassName = CreateCode.GetClassName(dr["Name"].ToString());
                crCode.ClassDesc = dr["Description"].ToString();
                crCode.ObjName = CreateCode.GetObjName(crCode.ClassName);

                crCode.CreateModel(dtTemp);
                crCode.CreateDAL(dtTemp);
                crCode.CreateBLL(dtTemp);
            }
        }
        #endregion

        #region 生成所有Controllers
        private void CreateAllControllers(CreateCode crCode)
        {
            DataTable dt = GetTbTable();
            foreach (DataRow dr in dt.Rows)
            {
                DataTable dtTemp = GetFdTable(dr["Name"].ToString());

                crCode.TableName = "[" + dr["Name"].ToString() + "]";
                crCode.ClassName = CreateCode.GetClassName(dr["Name"].ToString());
                crCode.ClassDesc = dr["Description"].ToString();
                crCode.ObjName = CreateCode.GetObjName(crCode.ClassName);

                crCode.CreateController(dtTemp);
                crCode.CreateList(dtTemp);
                crCode.CreateForm(dtTemp);
            }
        }
        #endregion

        #endregion
    }

    #region 代码生成类
    public class CreateCode
    {
        #region 属性
        public string DataType { get; set; }
        public string ColumnName { get; set; }
        public string Length { get; set; }
        public string ColumnDesc { get; set; }
        public string TableName { get; set; }
        public string ProjectName { get; set; }
        public string AreaName { get; set; }
        public string ClassName { get; set; }
        public string ClassDesc { get; set; }
        public string ObjName { get; set; }
        public string Author { get; set; }
        public string SaveModelPath { get; set; }
        public string SaveDALPath { get; set; }
        public string SaveBLLPath { get; set; }
        public string SaveClassFactoryPath { get; set; }
        public string SaveDbUtilityPath { get; set; }
        public string SavePath { get; set; }
        public string DataUsing { get; set; }
        public string DataConn { get; set; }
        public string DataPara { get; set; }
        public string DataParaNew { get; set; }
        public string DataReader { get; set; }
        public string Transaction { get; set; }
        #endregion

        #region 根据表名取类名
        /// <summary>
        /// 根据表名取类名
        /// </summary>
        /// <param name="strTable"></param>
        /// <returns></returns>
        public static string GetClassName(string strTable)
        {
            //int intPos = strTable.IndexOf("_");
            //int intTableLen = strTable.Length;
            //int intGetLen = intTableLen;
            //if (intPos > -1)
            //{
            //    intGetLen = intTableLen - (intPos + 1);
            //}
            //string strTableKey = strTable.Substring(intPos + 1, intGetLen);
            return strTable;
        }
        #endregion

        #region 根据表名取实例化名
        /// <summary>
        /// 根据表名取类名
        /// </summary>
        /// <param name="strTable"></param>
        /// <returns></returns>
        public static string GetObjName(string strClassName)
        {
            string strObjName;
            if (strClassName.Length > 3)
            {
                strObjName = strClassName.Substring(0, 3).ToLower();
            }
            else
            {
                strObjName = strClassName.ToLower();
            }
            return strObjName;
        }
        #endregion

        #region 获取数据类型
        private string GetDataType(string strDataType)
        {
            switch (strDataType)
            {
                case "bit": return "bool";
                case "tinyint": return "byte";
                case "smallint": return "short";
                case "int": return "int";
                case "bigint": return "long";
                case "real": return "float";
                case "float": return "double";
                case "money": return "decimal";
                case "decimal": return "decimal";
                case "datetimeoffset": return "DateTimeOffset";
                case "datetime": return "DateTime";
                case "char":
                case "varchar":
                case "nchar":
                case "nvarchar":
                case "text":
                case "ntext": return "string";
                case "image":
                case "binary":
                case "varbinary": return "byte[]";
                case "uniqueidentifier": return "Guid";
                default: return "string";
            }
        }
        #endregion

        #region 开始生成单个表
        public void CreateStart(DataTable dt)
        {
            CreateModel(dt);
            CreateDAL(dt);
            CreateBLL(dt);
            CreateController(dt);
            CreateList(dt);
            CreateForm(dt);
        }
        #endregion

        #region 获取默认值
        private string GetDefaultValue(string strDataType)
        {
            switch (strDataType)
            {
                case "bit": return "false";
                case "tinyint": return "(byte)0";
                case "smallint": return "(short)0";
                case "int": return "0";
                case "bigint": return "(long)0";
                case "real": return "0";
                case "float": return "0";
                case "money": return "0";
                case "decimal": return "0";
                case "datetimeoffset": return "Convert.ToDateTime(\"1900-1-1\")";
                case "datetime": return "Convert.ToDateTime(\"1900-1-1\")";
                case "char":
                case "varchar":
                case "nchar":
                case "nvarchar":
                case "text":
                case "ntext": return "\"\"";
                case "image":
                case "binary":
                case "varbinary": return "new byte[]{}";
                case "uniqueidentifier": return "Guid.NewGuid()";
                default: return "\"\"";
            }
        }
        #endregion
        //================

        #region 生成Model
        /// <summary>
        /// 生成Model
        /// </summary>
        /// <param name="dt"></param>
        public void CreateModel(DataTable dt)
        {
            StringBuilder strModel = new StringBuilder();
            strModel.Append("using System;\n");
            strModel.Append("using System.Text;\n");
            strModel.Append("using System.Runtime.Serialization;\n");
            strModel.Append("using System.Collections.Generic;\n");
            strModel.Append("using System.ComponentModel.DataAnnotations;\n");
            strModel.Append("using System.ComponentModel.DataAnnotations.Schema;\n");
            strModel.Append("\n");
            strModel.Append("namespace " + ProjectName + ".Model\n");
            strModel.Append("{\n");
            strModel.Append("/// <summary>\n");
            strModel.Append("///" + ClassDesc + "-实体类\n");
            strModel.Append("/// 创建人:" + Author + "\n");
            strModel.Append("/// 日期:" + DateTimeOffset.Now.ToString() + "\n");
            strModel.Append("/// </summary>\n");
            //strModel.Append("[DataContract]\n");
            strModel.Append("[Serializable]\n");
            strModel.Append("[Table(\"" + TableName + "\")]\n");
            strModel.Append("public class  " + ClassName + "Model:Base." + ClassName + "\n");
            strModel.Append("{\n");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strModel.Append("/// <summary>\n");
                strModel.Append("/// " + Config.Traditional2Simplified(dt.Rows[i][ColumnDesc].ToString()) + "\n");
                strModel.Append("/// </summary>\n");
                if (i == 0)
                {
                    strModel.Append("[DatabaseGenerated(DatabaseGeneratedOption.Identity)]\n");
                    strModel.Append("[Key]\n");
                }
                strModel.Append("public " + GetDataType(dt.Rows[i][DataType].ToString()) + " " + dt.Rows[i][ColumnName] + "{get;set;}\n");
            }
            strModel.Append("}\n");
            strModel.Append("}\n");
            //------------------------------------------------------------------生成文件
            IOHelper.FolderCheck(SaveModelPath + @"\");
            string strFilePath = SaveModelPath + @"\" + ClassName + "Model.cs";
            IOHelper.FileCreate(strFilePath, strModel.ToString());


            //------------------------------------------------------------------
            //------------------------------------------------------------------生成调用类
            StringBuilder strModel2 = new StringBuilder();
            strModel2.Append("using System;\n");
            strModel2.Append("using System.Text;\n");
            strModel2.Append("using System.Runtime.Serialization;\n");
            strModel2.Append("using System.Collections.Generic;\n");
            strModel2.Append("using System.ComponentModel.DataAnnotations;\n");
            strModel2.Append("using System.ComponentModel.DataAnnotations.Schema;\n");
            strModel2.Append("\n");
            strModel2.Append("namespace " + ProjectName + ".Model.Base\n");
            strModel2.Append("{\n");
            strModel2.Append("/// <summary>\n");
            strModel2.Append("///" + ClassDesc + "-实体类\n");
            strModel2.Append("/// 创建人:" + Author + "\n");
            strModel2.Append("/// 日期:" + DateTimeOffset.Now.ToString() + "\n");
            strModel2.Append("/// </summary>\n");
            strModel2.Append("\n");
            //strModel2.Append("[DataContract]\n");
            strModel2.Append("[Serializable]\n");
            strModel2.Append("public class  " + ClassName + "\n");
            strModel2.Append("{\n");

            strModel2.Append("}\n");
            strModel2.Append("}\n");

            //------------------------------------------------------------------生成文件
            IOHelper.FolderCheck(SaveModelPath + @"\Base\");
            string strFilePath2 = SaveModelPath + @"\Base\" + ClassName + ".cs";
            if (!File.Exists(strFilePath2))
            {
                IOHelper.FileCreate(strFilePath2, strModel2.ToString());
            }
        }
        #endregion

        #region 生成DAL
        /// <summary>
        /// 生成DAL
        /// </summary>
        /// <param name="dt"></param>
        public void CreateDAL(DataTable dt)
        {
            string ColumnID = dt.Rows[0][ColumnName].ToString();
            string ColumnType = GetDataType(dt.Rows[0][DataType].ToString());
            StringBuilder strDAL = new StringBuilder();
            strDAL.Append("using System;\n");
            strDAL.Append("using System.Collections;\n");
            strDAL.Append("using System.Collections.Generic;\n");
            strDAL.Append("using System.Text;\n");
            strDAL.Append("using System.Data;\n");
            strDAL.Append("using " + DataUsing + ";\n");
            strDAL.Append("using System.Web;\n");
            strDAL.Append("using System.Web.Mvc;\n");
            strDAL.Append("using " + ProjectName + ".Model;\n");
            strDAL.Append("using " + ProjectName + ".Common;\n");
            strDAL.Append("\n");
            strDAL.Append("namespace " + ProjectName + ".DAL.Base\n");
            strDAL.Append("{\n");
            strDAL.Append("/// <summary>\n");
            strDAL.Append("/// " + ClassDesc + "-数据访问类\n");
            strDAL.Append("/// 创建人:" + Author + "\n");
            strDAL.Append("/// 日期:" + DateTimeOffset.Now.ToString() + "\n");
            strDAL.Append("/// </summary>\n");
            strDAL.Append("public class  " + ClassName + ":_base\n");
            strDAL.Append("{\n");

            //------------------------------------------------------------------检查信息,保持某字段的唯一性
            strDAL.Append("#region 检查信息,保持某字段的唯一性\n");
            strDAL.Append("/// <summary>\n");
            strDAL.Append("/// 检查信息,保持某字段的唯一性\n");
            strDAL.Append("/// </summary>\n");
            strDAL.Append("public bool CheckInfo(" + Transaction + " trans, string strFieldName, string strFieldValue," + ColumnType + " " + ColumnID.ToString() + ")\n");
            strDAL.Append("{\n");
            strDAL.Append("StringBuilder sql = new StringBuilder();\n");
            strDAL.Append("sql.Append(\"select count(0) from " + TableName + " where \" + strFieldName + \"=@\" + strFieldName + \" and " + ColumnID.ToString() + "<>@" + ColumnID.ToString() + "\");\n");
            strDAL.Append(DataPara + "[] cmdParams = {\n");
            strDAL.Append(DataParaNew + "(\"@\"+strFieldName,strFieldValue),\n");
            strDAL.Append(DataParaNew + "(\"@" + ColumnID.ToString() + "\"," + ColumnID.ToString() + ")};\n");
            strDAL.Append("object obj = GetScalar(trans, CommandType.Text, sql.ToString(), cmdParams);\n");
            strDAL.Append("int count = 0;\n");
            strDAL.Append("try { count = Convert.ToInt32(obj); }\n");
            strDAL.Append("catch { count = 0; }\n");
            strDAL.Append("return count > 0;\n");
            strDAL.Append("}\n");
            strDAL.Append("#endregion\n");
            strDAL.Append("\n");

            //------------------------------------------------------------------取字段值
            strDAL.Append("#region 取字段值\n");
            strDAL.Append("/// <summary>\n");
            strDAL.Append("/// 取字段值\n");
            strDAL.Append("/// </summary>\n");
            strDAL.Append("public string GetValueByField(" + Transaction + " trans,string strFieldName, " + ColumnType + " " + ColumnID + ")\n");
            strDAL.Append("{\n");
            strDAL.Append("StringBuilder sql = new StringBuilder();\n");
            strDAL.Append("sql.Append(\"select \" + strFieldName + \" from " + TableName + " where " + ColumnID + "=@" + ColumnID + "\")" + ";\n");
            strDAL.Append(DataPara + "[] cmdParams = {\n");
            strDAL.Append(DataParaNew + "(\"@" + ColumnID + "\"," + ColumnID + ")};\n");
            strDAL.Append("object obj = GetScalar(trans, CommandType.Text, sql.ToString(), cmdParams);\n");
            strDAL.Append("try { return obj.ToString(); }\n");
            strDAL.Append("catch { return \"\"; }\n");
            strDAL.Append("}\n");
            strDAL.Append("#endregion\n");
            strDAL.Append("\n");

            //------------------------------------------------------------------取实体
            strDAL.Append("#region 取实体\n");
            strDAL.Append("/// <summary>\n");
            strDAL.Append("/// 取实体\n");
            strDAL.Append("/// </summary>\n");
            strDAL.Append("protected " + ClassName + "Model GetModel(DataRow dr)\n");
            strDAL.Append("{\n");
            strDAL.Append(ClassName + "Model " + ObjName + "Model=new " + ClassName + "Model();\n");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strDAL.Append(ObjName + "Model." + dt.Rows[i][ColumnName].ToString() + "=Convert.IsDBNull(dr[\"" + dt.Rows[i][ColumnName].ToString() + "\"]) ? " + GetDefaultValue(dt.Rows[i][DataType].ToString()) + " : (" + GetDataType(dt.Rows[i][DataType].ToString()) + ")dr[\"" + dt.Rows[i][ColumnName].ToString() + "\"];\n");
            }
            strDAL.Append("return " + ObjName + "Model;\n");
            strDAL.Append("}\n");
            strDAL.Append("#endregion\n");
            strDAL.Append("\n");

            //------------------------------------------------------------------读取信息
            strDAL.Append("#region 读取信息\n");
            strDAL.Append("/// <summary>\n");
            strDAL.Append("/// 读取信息\n");
            strDAL.Append("/// </summary>\n");
            strDAL.Append("public " + ClassName + "Model GetInfo(" + Transaction + " trans," + ColumnType + " " + ColumnID.ToString() + ")\n");
            strDAL.Append("{\n");
            strDAL.Append("StringBuilder sql = new StringBuilder();\n");
            strDAL.Append("sql.Append(\"select * from " + TableName + " where " + ColumnID.ToString() + "=@" + ColumnID.ToString() + "\");\n");
            strDAL.Append(DataPara + "[] cmdParams = {\n");
            strDAL.Append(DataParaNew + "(\"@" + ColumnID.ToString() + "\"," + ColumnID.ToString() + ")};\n");
            strDAL.Append("DataTable dt = GetDataSet(trans, CommandType.Text, sql.ToString(), cmdParams).Tables[0];\n");
            strDAL.Append("if (dt != null && dt.Rows.Count > 0)\n");
            strDAL.Append("{\n");
            strDAL.Append("DataRow dr=dt.Rows[0];\n");
            strDAL.Append("return GetModel(dr);\n");
            strDAL.Append("}\n");
            strDAL.Append("else\n");
            strDAL.Append("{\n");
            strDAL.Append("return null;\n");
            strDAL.Append("}\n");
            strDAL.Append("}\n");
            strDAL.Append("#endregion\n");
            strDAL.Append("\n");

            //------------------------------------------------------------------插入信息
            strDAL.Append("#region 插入信息\n");
            strDAL.Append("/// <summary>\n");
            strDAL.Append("/// 插入信息\n");
            strDAL.Append("/// </summary>\n");
            strDAL.Append("public int InsertInfo(" + Transaction + " trans," + ClassName + "Model " + ObjName + "Model)\n");
            strDAL.Append("{\n");
            strDAL.Append("StringBuilder sql = new StringBuilder(\"insert into\");\n");
            strDAL.Append("sql.Append(\" " + TableName + "(");
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][ColumnName].ToString() != "TotalAmount")
                {
                    strDAL.Append(dt.Rows[i][ColumnName].ToString());
                    if (i < dt.Rows.Count - 1)
                    {
                        strDAL.Append(",");
                    }
                    else
                    {
                        strDAL.Append(")\");\n");
                    }
                }
            }
            strDAL.Append("sql.Append(\" values(");
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][ColumnName].ToString() != "TotalAmount")
                {
                    strDAL.Append("@" + dt.Rows[i][ColumnName].ToString());
                    if (i < dt.Rows.Count - 1)
                    {
                        strDAL.Append(",");
                    }
                    else
                    {
                        strDAL.Append(")\");\n");
                    }
                }
            }
            strDAL.Append("sql.Append(\"select @@identity\");\n");
            strDAL.Append(DataPara + "[] cmdParams = {\n");
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][ColumnName].ToString() != "TotalAmount")
                {
                    strDAL.Append(DataParaNew + "(\"@" + dt.Rows[i][ColumnName].ToString() + "\"," + ObjName + "Model." + dt.Rows[i][ColumnName].ToString() + ".DefVal())");
                    if (i < dt.Rows.Count - 1)
                    {
                        strDAL.Append(",\n");
                    }
                    else
                    {
                        strDAL.Append("};\n");
                    }
                }
            }
            strDAL.Append("//return ExecuteSql(trans, CommandType.Text, sql.ToString(), cmdParams);\n");
            strDAL.Append("object obj = GetScalar(trans, CommandType.Text, sql.ToString(), cmdParams);\n");
            strDAL.Append("try { return Convert.ToInt32(obj); }\n");
            strDAL.Append("catch { return 0; }\n");
            strDAL.Append("}\n");
            strDAL.Append("#endregion\n");
            strDAL.Append("\n");

            //------------------------------------------------------------------更新信息
            strDAL.Append("#region 更新信息\n");
            strDAL.Append("/// <summary>\n");
            strDAL.Append("/// 更新信息\n");
            strDAL.Append("/// </summary>\n");
            strDAL.Append("public int UpdateInfo(" + Transaction + " trans," + ClassName + "Model " + ObjName + "Model," + ColumnType + " " + ColumnID.ToString() + ")\n");
            strDAL.Append("{\n");
            strDAL.Append("StringBuilder sql = new StringBuilder(\"update " + TableName + " set\");\n");
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][ColumnName].ToString() != "TotalAmount")
                {
                    strDAL.Append("sql.Append(\" " + dt.Rows[i][ColumnName].ToString() + "=@" + dt.Rows[i][ColumnName].ToString());
                    if (i < dt.Rows.Count - 1)
                    {
                        strDAL.Append(",\");\n");
                    }
                    else
                    {
                        strDAL.Append("\");\n");
                    }
                }
            }
            strDAL.Append("sql.Append(\" where " + ColumnID.ToString() + "=@" + ColumnID.ToString() + "\");\n");
            strDAL.Append(DataPara + "[] cmdParams = {\n");
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][ColumnName].ToString() != "TotalAmount")
                {
                    strDAL.Append(DataParaNew + "(\"@" + dt.Rows[i][ColumnName].ToString() + "\"," + ObjName + "Model." + dt.Rows[i][ColumnName].ToString() + ".DefVal()),\n");
                }
            }
            strDAL.Append(DataParaNew + "(\"@" + ColumnID.ToString() + "\"," + ColumnID.ToString() + ")};\n");
            strDAL.Append("return ExecuteSql(trans, CommandType.Text, sql.ToString(), cmdParams);\n");
            strDAL.Append("}\n");
            strDAL.Append("#endregion\n");
            strDAL.Append("\n");

            //------------------------------------------------------------------删除信息
            strDAL.Append("#region 删除信息\n");
            strDAL.Append("/// <summary>\n");
            strDAL.Append("/// 删除信息\n");
            strDAL.Append("/// </summary>\n");
            strDAL.Append("public int DeleteInfo(" + Transaction + " trans," + ColumnType + " " + ColumnID.ToString() + ")\n");
            strDAL.Append("{\n");
            strDAL.Append("StringBuilder sql = new StringBuilder();\n");
            strDAL.Append("sql.Append(\"delete from " + TableName + " where " + ColumnID.ToString() + "=@" + ColumnID.ToString() + "\");\n");
            strDAL.Append(DataPara + "[] cmdParams = {\n");
            strDAL.Append(DataParaNew + "(\"@" + ColumnID.ToString() + "\"," + ColumnID.ToString() + ")};\n");
            strDAL.Append("return ExecuteSql(trans, CommandType.Text, sql.ToString(), cmdParams);\n");
            strDAL.Append("}\n");
            strDAL.Append("#endregion\n");
            strDAL.Append("\n");

            strDAL.Append("}\n");
            strDAL.Append("}\n");
            //------------------------------------------------------------------生成文件
            IOHelper.FolderCheck(SaveDALPath + @"\Base\");
            string strFilePath = SaveDALPath + @"\Base\" + ClassName + ".cs";
            IOHelper.FileCreate(strFilePath, strDAL.ToString());
            //------------------------------------------------------------------
            //------------------------------------------------------------------生成调用类
            StringBuilder strDAL2 = new StringBuilder();
            strDAL2.Append("using System;\n");
            strDAL2.Append("using System.Collections;\n");
            strDAL2.Append("using System.Collections.Generic;\n");
            strDAL2.Append("using System.Text;\n");
            strDAL2.Append("using System.Data;\n");
            strDAL2.Append("using " + DataUsing + ";\n");
            strDAL2.Append("using System.Web;\n");
            strDAL2.Append("using System.Web.Mvc;\n");
            strDAL2.Append("using " + ProjectName + ".Model;\n");
            strDAL2.Append("using " + ProjectName + ".Common;\n");
            strDAL2.Append("\n");
            strDAL2.Append("namespace " + ProjectName + ".DAL\n");
            strDAL2.Append("{\n");
            strDAL2.Append("/// <summary>\n");
            strDAL2.Append("/// " + ClassDesc + "-数据访问类\n");
            strDAL2.Append("/// 创建人:" + Author + "\n");
            strDAL2.Append("/// 日期:" + DateTimeOffset.Now.ToString() + "\n");
            strDAL2.Append("/// </summary>\n");
            strDAL2.Append("\n");
            strDAL2.Append("public class  " + ClassName + "DAL:Base." + ClassName + "\n");
            strDAL2.Append("{\n");

            //------------------------------------------------------------------取信息分页列表
            strDAL2.Append("#region 取信息分页列表\n");
            strDAL2.Append("/// <summary>\n");
            strDAL2.Append("/// 取信息分页列表\n");
            strDAL2.Append("/// </summary>\n");
            strDAL2.Append("public DataTable GetInfoPageList(" + Transaction + " trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)\n");
            strDAL2.Append("{\n");
            strDAL2.Append("StringBuilder SqlQuery = new StringBuilder();\n");
            strDAL2.Append("List<" + DataPara + "> listParams = new List<" + DataPara + ">();\n");
            strDAL2.Append("StringBuilder UrlPara = new StringBuilder();\n");

            strDAL2.Append("#region 查询条件\n");
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                strDAL2.Append("if (hs.Contains(\"" + dt.Rows[i][ColumnName].ToString() + "\"))\n");
                strDAL2.Append("{\n");
                if (GetDataType(dt.Rows[i][DataType].ToString()) == "string")
                {
                    strDAL2.Append("SqlQuery.Append(\" and a." + dt.Rows[i][ColumnName].ToString() + " like @" + dt.Rows[i][ColumnName].ToString() + "\");\n");
                    strDAL2.Append("listParams.Add(" + DataParaNew + "(\"@" + dt.Rows[i][ColumnName].ToString() + "\", \"%\" +hs[\"" + dt.Rows[i][ColumnName].ToString() + "\"]+ \"%\"));\n");
                }
                else
                {
                    strDAL2.Append("SqlQuery.Append(\" and a." + dt.Rows[i][ColumnName].ToString() + " = @" + dt.Rows[i][ColumnName].ToString() + "\");\n");
                    strDAL2.Append("listParams.Add(" + DataParaNew + "(\"@" + dt.Rows[i][ColumnName].ToString() + "\", hs[\"" + dt.Rows[i][ColumnName].ToString() + "\"]));\n");
                }
                strDAL2.Append("UrlPara.Append(\"" + dt.Rows[i][ColumnName].ToString() + "=\" + HttpContext.Current.Server.UrlEncode(hs[\"" + dt.Rows[i][ColumnName].ToString() + "\"].ToString()) + \"&\");\n");
                strDAL2.Append("}\n");
            }
            strDAL2.Append("#endregion\n");

            strDAL2.Append("string TableName = \"" + TableName + " as a\";\n");
            strDAL2.Append("string FieldKey = \"a." + ColumnID.ToString() + "\";\n");
            strDAL2.Append("string FieldShow = \"a.*\";\n");
            strDAL2.Append("string Where = \"1=1\" + SqlQuery;\n");
            strDAL2.Append("string FieldOrder = p.FieldOrder;\n");
            strDAL2.Append("DataTable dt = GetInfoPageList(trans, TableName, FieldKey, FieldShow, Where, FieldOrder, listParams.ToArray(), p.PageSize, p.CurrentPage, p.PageUrl+UrlPara, p.PageStrType, out PageStr);\n");
            strDAL2.Append("return dt;\n");
            strDAL2.Append("}\n");
            strDAL2.Append("#endregion\n");
            strDAL2.Append("\n");

            //------------------------------------------------------------------取DataTable
            strDAL2.Append("#region 取DataTable\n");
            strDAL2.Append("/// <summary>\n");
            strDAL2.Append("/// 取DataTable\n");
            strDAL2.Append("/// </summary>\n");
            strDAL2.Append("public DataTable GetDataTable(" + Transaction + " trans, StringBuilder LeftJoin, StringBuilder SqlQuery, List<" + DataPara + "> listParams, string FieldShow, string FieldOrder)\n");
            strDAL2.Append("{\n");
            strDAL2.Append("string TableName = \"" + TableName + " as a\" + LeftJoin;\n");
            strDAL2.Append("string FieldKey = \"a." + ColumnID.ToString() + "\";\n");
            strDAL2.Append("string Where = \"1=1\" + SqlQuery;\n");
            strDAL2.Append("DataTable dt = GetDataTable(trans, TableName, FieldKey, FieldShow, Where, FieldOrder, listParams.ToArray());\n");
            strDAL2.Append("return dt;\n");
            strDAL2.Append("}\n");
            strDAL2.Append("#endregion\n");
            strDAL2.Append("\n");

            //------------------------------------------------------------------取实体集合
            strDAL2.Append("#region 取实体集合\n");
            strDAL2.Append("/// <summary>\n");
            strDAL2.Append("/// 取实体集合\n");
            strDAL2.Append("/// </summary>\n");
            strDAL2.Append("public List<" + ClassName + "Model> GetModels(" + Transaction + " trans, StringBuilder SqlQuery, List<" + DataPara + "> listParams, int TopNum, string FieldOrder)\n");
            strDAL2.Append("{\n");
            strDAL2.Append("StringBuilder sql = new StringBuilder();\n");
            strDAL2.Append("sql.AppendFormat(\"select {0} * from " + TableName + " where 1=1\", TopNum > 0 ? \"top \" + TopNum : \"\");\n");
            strDAL2.Append("sql.Append(SqlQuery);\n");
            strDAL2.Append("sql.Append(\" order by \" + FieldOrder);\n");
            strDAL2.Append("DataTable dt = GetDataSet(trans, CommandType.Text, sql.ToString(), listParams.ToArray()).Tables[0];\n");
            strDAL2.Append("List<" + ClassName + "Model> list = new List<" + ClassName + "Model>();\n");
            strDAL2.Append("foreach (DataRow dr in dt.Rows)\n");
            strDAL2.Append("{\n");
            strDAL2.Append("list.Add(GetModel(dr));\n");
            strDAL2.Append("}\n");
            strDAL2.Append("return list;\n");
            strDAL2.Append("}\n");
            strDAL2.Append("#endregion\n");
            strDAL2.Append("\n");

            //------------------------------------------------------------------取实体
            strDAL2.Append("#region 取实体\n");
            strDAL2.Append("/// <summary>\n");
            strDAL2.Append("/// 取实体\n");
            strDAL2.Append("/// </summary>\n");
            strDAL2.Append("public " + ClassName + "Model GetModel(" + Transaction + " trans, StringBuilder SqlQuery, List<" + DataPara + "> listParams)\n");
            strDAL2.Append("{\n");
            strDAL2.Append("StringBuilder sql = new StringBuilder();\n");
            strDAL2.Append("sql.Append(\"select * from " + TableName + " where 1=1\");\n");
            strDAL2.Append("sql.Append(SqlQuery);\n");
            strDAL2.Append("DataTable dt = GetDataSet(trans, CommandType.Text, sql.ToString(), listParams.ToArray()).Tables[0];\n");
            strDAL2.Append("if (dt != null && dt.Rows.Count > 0)\n");
            strDAL2.Append("{\n");
            strDAL2.Append("DataRow dr=dt.Rows[0];\n");
            strDAL2.Append("return GetModel(dr);\n");
            strDAL2.Append("}\n");
            strDAL2.Append("else\n");
            strDAL2.Append("{\n");
            strDAL2.Append("return null;\n");
            strDAL2.Append("}\n");
            strDAL2.Append("}\n");
            strDAL2.Append("#endregion\n");
            strDAL2.Append("\n");

            //------------------------------------------------------------------取记录总数
            strDAL2.Append("#region 取记录总数\n");
            strDAL2.Append("/// <summary>\n");
            strDAL2.Append("/// 取记录总数\n");
            strDAL2.Append("/// </summary>\n");
            strDAL2.Append("public int GetAllCount(" + Transaction + " trans,StringBuilder LeftJoin, StringBuilder SqlQuery, List<" + DataPara + "> listParams)\n");
            strDAL2.Append("{\n");
            strDAL2.Append("string TableName = \"" + TableName + " as a\" + LeftJoin;\n");
            strDAL2.Append("string Where = \"1=1\" + SqlQuery;\n");
            strDAL2.Append("int code = GetAllCount(trans, TableName, Where, listParams.ToArray());\n");
            strDAL2.Append("return code;\n");
            strDAL2.Append("}\n");
            strDAL2.Append("#endregion\n");
            strDAL2.Append("\n");

            //------------------------------------------------------------------取字段总和
            strDAL2.Append("#region 取字段总和\n");
            strDAL2.Append("/// <summary>\n");
            strDAL2.Append("/// 取字段总和\n");
            strDAL2.Append("/// </summary>\n");
            strDAL2.Append("public decimal GetAllSum(" + Transaction + " trans,StringBuilder LeftJoin, StringBuilder SqlQuery, List<" + DataPara + "> listParams,string SumField)\n");
            strDAL2.Append("{\n");
            strDAL2.Append("string TableName = \"" + TableName + " as a\" + LeftJoin;\n");
            strDAL2.Append("string Where = \"1=1\" + SqlQuery;\n");
            strDAL2.Append("decimal sum = GetAllSum(trans, SumField, TableName, Where, listParams.ToArray());\n");
            strDAL2.Append("return sum;\n");
            strDAL2.Append("}\n");
            strDAL2.Append("#endregion\n");
            strDAL2.Append("\n");

            strDAL2.Append("}\n");
            strDAL2.Append("}\n");
            //------------------------------------------------------------------生成文件
            string strFilePath2 = SaveDALPath + @"\" + ClassName + "DAL.cs";
            if (!File.Exists(strFilePath2))
            {
                IOHelper.FileCreate(strFilePath2, strDAL2.ToString());
            }
            else
            {
                IOHelper.FolderCheck(SavePath + @"\_out\" + ClassName + @"\");
                strFilePath2 = SavePath + @"\_out\" + ClassName + @"\" + ClassName + "DAL.cs";
                IOHelper.FileCreate(strFilePath2, strDAL2.ToString());
            }
        }
        #endregion

        #region 生成BLL
        /// <summary>
        /// 生成BLL
        /// </summary>
        /// <param name="dt"></param>
        public void CreateBLL(DataTable dt)
        {
            string ColumnID = dt.Rows[0][ColumnName].ToString();
            string ColumnType = GetDataType(dt.Rows[0][DataType].ToString());
            StringBuilder strBLL = new StringBuilder();
            strBLL.Append("using System;\n");
            strBLL.Append("using System.Collections;\n");
            strBLL.Append("using System.Collections.Generic;\n");
            strBLL.Append("using System.Text;\n");
            strBLL.Append("using System.Data;\n");
            strBLL.Append("using " + DataUsing + ";\n");
            strBLL.Append("using System.Web;\n");
            strBLL.Append("using System.Web.Mvc;\n");
            strBLL.Append("using System.Web.Caching;\n");
            strBLL.Append("using " + ProjectName + ".Common;\n");
            strBLL.Append("using " + ProjectName + ".Model;\n");
            strBLL.Append("using " + ProjectName + ".DAL;\n");
            strBLL.Append("\n");
            strBLL.Append("namespace " + ProjectName + ".BLL.Base\n");
            strBLL.Append("{\n");
            strBLL.Append("/// <summary>\n");
            strBLL.Append("/// " + ClassDesc + "-业务逻辑类\n");
            strBLL.Append("/// 创建人:" + Author + "\n");
            strBLL.Append("/// 日期:" + DateTimeOffset.Now.ToString() + "\n");
            strBLL.Append("/// </summary>\n");
            strBLL.Append("\n");
            strBLL.Append("public class  " + ClassName + "\n");
            strBLL.Append("{\n");
            strBLL.Append("\n");
            strBLL.Append("private readonly " + ClassName + "DAL " + ObjName + "DAL=new " + ClassName + "DAL();\n");
            strBLL.Append("\n");

            //------------------------------------------------------------------检查信息,保持某字段的唯一性
            strBLL.Append("#region 检查信息,保持某字段的唯一性\n");
            strBLL.Append("/// <summary>\n");
            strBLL.Append("/// 检查信息,保持某字段的唯一性\n");
            strBLL.Append("/// </summary>\n");
            strBLL.Append("public bool CheckInfo(" + Transaction + " trans,string strFieldName, string strFieldValue," + ColumnType + " " + ColumnID.ToString() + ")\n");
            strBLL.Append("{\n");
            strBLL.Append("return " + ObjName + "DAL.CheckInfo(trans,strFieldName, strFieldValue," + ColumnID.ToString() + ");\n");
            strBLL.Append("}\n");
            strBLL.Append("#endregion\n");
            strBLL.Append("\n");
            //------------------------------------------------------------------取字段值
            strBLL.Append("#region 取字段值\n");
            strBLL.Append("/// <summary>\n");
            strBLL.Append("/// 取字段值\n");
            strBLL.Append("/// </summary>\n");
            strBLL.Append("public string GetValueByField(" + Transaction + " trans,string strFieldName, " + ColumnType + " " + ColumnID.ToString() + ")\n");
            strBLL.Append("{\n");
            strBLL.Append("return " + ObjName + "DAL.GetValueByField(trans,strFieldName, " + ColumnID.ToString() + ");\n");
            strBLL.Append("}\n");
            strBLL.Append("#endregion\n");
            strBLL.Append("\n");
            //------------------------------------------------------------------读取信息
            strBLL.Append("#region 读取信息\n");
            strBLL.Append("/// <summary>\n");
            strBLL.Append("/// 读取信息\n");
            strBLL.Append("/// </summary>\n");
            strBLL.Append("public " + ClassName + "Model GetInfo(" + Transaction + " trans," + ColumnType + " " + ColumnID.ToString() + ")\n");
            strBLL.Append("{\n");
            strBLL.Append("return " + ObjName + "DAL.GetInfo(trans," + ColumnID.ToString() + ");\n");
            strBLL.Append("}\n");
            strBLL.Append("#endregion\n");
            strBLL.Append("\n");
            //------------------------------------------------------------------从缓存读取信息
            strBLL.Append("#region 从缓存读取信息\n");
            strBLL.Append("/// <summary>\n");
            strBLL.Append("/// 从缓存读取信息\n");
            strBLL.Append("/// </summary>\n");
            strBLL.Append("public " + ClassName + "Model GetCacheInfo(" + Transaction + " trans," + ColumnType + " " + ColumnID.ToString() + ")\n");
            strBLL.Append("{\n");
            strBLL.Append("string key=\"Cache_" + ClassName + "_Model_\"+" + ColumnID.ToString() + ";\n");
            strBLL.Append("object value = CacheHelper.GetCache(key);\n");
            strBLL.Append("if (value != null)\n");
            strBLL.Append("return (" + ClassName + "Model)value;\n");
            strBLL.Append("else\n");
            strBLL.Append("{\n");
            strBLL.Append(ClassName + "Model " + ObjName + "Model = " + ObjName + "DAL.GetInfo(trans," + ColumnID.ToString() + ");\n");
            strBLL.Append("CacheHelper.AddCache(key, " + ObjName + "Model, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);\n");
            strBLL.Append("return " + ObjName + "Model;\n");
            strBLL.Append("}\n");
            strBLL.Append("}\n");
            strBLL.Append("#endregion\n");
            strBLL.Append("\n");

            //------------------------------------------------------------------插入信息
            strBLL.Append("#region 插入信息\n");
            strBLL.Append("/// <summary>\n");
            strBLL.Append("/// 插入信息\n");
            strBLL.Append("/// </summary>\n");
            strBLL.Append("public int InsertInfo(" + Transaction + " trans," + ClassName + "Model " + ObjName + "Model)\n");
            strBLL.Append("{\n");
            strBLL.Append("return " + ObjName + "DAL.InsertInfo(trans," + ObjName + "Model);\n");
            strBLL.Append("}\n");
            strBLL.Append("#endregion\n");
            strBLL.Append("\n");

            //------------------------------------------------------------------更新信息
            strBLL.Append("#region 更新信息\n");
            strBLL.Append("/// <summary>\n");
            strBLL.Append("/// 更新信息\n");
            strBLL.Append("/// </summary>\n");
            strBLL.Append("public int UpdateInfo(" + Transaction + " trans," + ClassName + "Model " + ObjName + "Model," + ColumnType + " " + ColumnID.ToString() + ")\n");
            strBLL.Append("{\n");
            strBLL.Append("string key=\"Cache_" + ClassName + "_Model_\"+" + ColumnID.ToString() + ";\n");
            strBLL.Append("CacheHelper.RemoveCache(key);\n");
            strBLL.Append("return " + ObjName + "DAL.UpdateInfo(trans," + ObjName + "Model," + ColumnID.ToString() + ");\n");
            strBLL.Append("}\n");
            strBLL.Append("#endregion\n");
            strBLL.Append("\n");

            //------------------------------------------------------------------删除信息
            strBLL.Append("#region 删除信息\n");
            strBLL.Append("/// <summary>\n");
            strBLL.Append("/// 删除信息\n");
            strBLL.Append("/// </summary>\n");
            strBLL.Append("public int DeleteInfo(" + Transaction + " trans," + ColumnType + " " + ColumnID.ToString() + ")\n");
            strBLL.Append("{\n");
            strBLL.Append("string key=\"Cache_" + ClassName + "_Model_\"+" + ColumnID.ToString() + ";\n");
            strBLL.Append("CacheHelper.RemoveCache(key);\n");
            strBLL.Append("return " + ObjName + "DAL.DeleteInfo(trans," + ColumnID.ToString() + ");\n");
            strBLL.Append("}\n");
            strBLL.Append("#endregion\n");
            strBLL.Append("\n");

            strBLL.Append("}\n");
            strBLL.Append("}\n");
            //------------------------------------------------------------------生成文件
            IOHelper.FolderCheck(SaveBLLPath + @"\Base\");
            string strFilePath = SaveBLLPath + @"\Base\" + ClassName + ".cs";
            IOHelper.FileCreate(strFilePath, strBLL.ToString());

            //------------------------------------------------------------------
            //------------------------------------------------------------------生成调用类
            StringBuilder strBLL2 = new StringBuilder();
            strBLL2.Append("using System;\n");
            strBLL2.Append("using System.Collections;\n");
            strBLL2.Append("using System.Collections.Generic;\n");
            strBLL2.Append("using System.Linq;\n");
            strBLL2.Append("using System.Text;\n");
            strBLL2.Append("using System.Data;\n");
            strBLL2.Append("using " + DataUsing + ";\n");
            strBLL2.Append("using System.Web;\n");
            strBLL2.Append("using System.Web.Mvc;\n");
            strBLL2.Append("using System.Web.Caching;\n");
            strBLL2.Append("using " + ProjectName + ".Common;\n");
            strBLL2.Append("using " + ProjectName + ".Model;\n");
            strBLL2.Append("using " + ProjectName + ".DAL;\n");
            strBLL2.Append("\n");
            strBLL2.Append("namespace " + ProjectName + ".BLL\n");
            strBLL2.Append("{\n");
            strBLL2.Append("/// <summary>\n");
            strBLL2.Append("/// " + ClassDesc + "-业务逻辑类\n");
            strBLL2.Append("/// 创建人:" + Author + "\n");
            strBLL2.Append("/// 日期:" + DateTimeOffset.Now.ToString() + "\n");
            strBLL2.Append("/// </summary>\n");
            strBLL2.Append("\n");
            strBLL2.Append("public class  " + ClassName + "BLL:Base." + ClassName + "\n");
            strBLL2.Append("{\n");
            strBLL2.Append("\n");
            strBLL2.Append("private readonly " + ClassName + "DAL " + ObjName + "DAL=new " + ClassName + "DAL();\n");
            strBLL2.Append("\n");

            //------------------------------------------------------------------取信息分页列表
            strBLL2.Append("#region 取信息分页列表\n");
            strBLL2.Append("/// <summary>\n");
            strBLL2.Append("/// 取信息分页列表\n");
            strBLL2.Append("/// </summary>\n");
            strBLL2.Append("public DataTable GetInfoPageList(" + Transaction + " trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)\n");
            strBLL2.Append("{\n");
            strBLL2.Append("return " + ObjName + "DAL.GetInfoPageList(trans, hs, p, out PageStr);\n");
            strBLL2.Append("}\n");
            strBLL2.Append("#endregion\n");
            strBLL2.Append("\n");

            //------------------------------------------------------------------取DataTable
            strBLL2.Append("#region 取DataTable\n");
            strBLL2.Append("/// <summary>\n");
            strBLL2.Append("/// 取DataTable\n");
            strBLL2.Append("/// </summary>\n");
            strBLL2.Append("public DataTable GetDataTable(" + Transaction + " trans)\n");
            strBLL2.Append("{\n");
            strBLL2.Append("StringBuilder LeftJoin = new StringBuilder();\n");
            strBLL2.Append("StringBuilder SqlQuery = new StringBuilder();\n");
            strBLL2.Append("List<" + DataPara + "> listParams = new List<" + DataPara + ">();\n");
            strBLL2.Append("string FieldShow=\"a.*\";\n");
            strBLL2.Append("string FieldOrder=\"a." + ColumnID.ToString() + " asc\";\n");
            strBLL2.Append("return " + ObjName + "DAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);\n");
            strBLL2.Append("}\n");
            strBLL2.Append("#endregion\n");
            strBLL2.Append("\n");

            //------------------------------------------------------------------取实体集合
            strBLL2.Append("#region 取实体集合\n");
            strBLL2.Append("/// <summary>\n");
            strBLL2.Append("/// 取实体集合\n");
            strBLL2.Append("/// </summary>\n");
            strBLL2.Append("public List<" + ClassName + "Model> GetModels(" + Transaction + " trans)\n");
            strBLL2.Append("{\n");
            strBLL2.Append("StringBuilder SqlQuery = new StringBuilder();\n");
            strBLL2.Append("List<" + DataPara + "> listParams = new List<" + DataPara + ">();\n");
            strBLL2.Append("string FieldOrder=\"" + ColumnID.ToString() + " asc\";\n");
            strBLL2.Append("return " + ObjName + "DAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);\n");
            strBLL2.Append("}\n");
            strBLL2.Append("#endregion\n");
            strBLL2.Append("\n");

            //------------------------------------------------------------------取实体
            strBLL2.Append("#region 取实体\n");
            strBLL2.Append("/// <summary>\n");
            strBLL2.Append("/// 取实体\n");
            strBLL2.Append("/// </summary>\n");
            strBLL2.Append("public " + ClassName + "Model GetModel(" + Transaction + " trans, " + ColumnType + " " + ColumnID.ToString() + ")\n");
            strBLL2.Append("{\n");
            strBLL2.Append("StringBuilder SqlQuery = new StringBuilder();\n");
            strBLL2.Append("SqlQuery.Append(\" and " + ColumnID.ToString() + "=@" + ColumnID.ToString() + "\");\n");
            strBLL2.Append("List<" + DataPara + "> listParams = new List<" + DataPara + ">();\n");
            strBLL2.Append("listParams.Add(" + DataParaNew + "(\"@" + ColumnID.ToString() + "\", " + ColumnID.ToString() + "));\n");
            strBLL2.Append("return " + ObjName + "DAL.GetModel(trans, SqlQuery, listParams);\n");
            strBLL2.Append("}\n");
            strBLL2.Append("#endregion\n");
            strBLL2.Append("\n");

            //------------------------------------------------------------------取记录总数
            strBLL2.Append("#region 取记录总数\n");
            strBLL2.Append("/// <summary>\n");
            strBLL2.Append("/// 取记录总数\n");
            strBLL2.Append("/// </summary>\n");
            strBLL2.Append("public int GetAllCount(" + Transaction + " trans)\n");
            strBLL2.Append("{\n");
            strBLL2.Append("StringBuilder LeftJoin = new StringBuilder();\n");
            strBLL2.Append("StringBuilder SqlQuery = new StringBuilder();\n");
            strBLL2.Append("List<" + DataPara + "> listParams = new List<" + DataPara + ">();\n");
            strBLL2.Append("return " + ObjName + "DAL.GetAllCount(trans,LeftJoin, SqlQuery, listParams);\n");
            strBLL2.Append("}\n");
            strBLL2.Append("#endregion\n");
            strBLL2.Append("\n");

            //------------------------------------------------------------------取字段总和
            strBLL2.Append("#region 取字段总和\n");
            strBLL2.Append("/// <summary>\n");
            strBLL2.Append("/// 取字段总和\n");
            strBLL2.Append("/// </summary>\n");
            strBLL2.Append("public decimal GetAllSum(" + Transaction + " trans)\n");
            strBLL2.Append("{\n");
            strBLL2.Append("StringBuilder LeftJoin = new StringBuilder();\n");
            strBLL2.Append("StringBuilder SqlQuery = new StringBuilder();\n");
            strBLL2.Append("List<" + DataPara + "> listParams = new List<" + DataPara + ">();\n");
            strBLL2.Append("return " + ObjName + "DAL.GetAllSum(trans,LeftJoin, SqlQuery, listParams,\"a." + ColumnID.ToString() + "\");\n");
            strBLL2.Append("}\n");
            strBLL2.Append("#endregion\n");
            strBLL2.Append("\n");

            strBLL2.Append("}\n");
            strBLL2.Append("}\n");
            //------------------------------------------------------------------生成文件
            string strFilePath2 = SaveBLLPath + @"\" + ClassName + "BLL.cs";
            if (!File.Exists(strFilePath2))
            {
                IOHelper.FileCreate(strFilePath2, strBLL2.ToString());
            }
            else
            {
                IOHelper.FolderCheck(SavePath + @"\_out\" + ClassName + @"\");
                strFilePath2 = SavePath + @"\_out\" + ClassName + @"\" + ClassName + "BLL.cs";
                IOHelper.FileCreate(strFilePath2, strBLL2.ToString());
            }
        }
        #endregion

        #region 生成Controller
        /// <summary>
        /// 生成Controller
        /// </summary>
        /// <param name="dt"></param>
        public void CreateController(DataTable dt)
        {
            string ColumnID = dt.Rows[0][ColumnName].ToString();
            string ColumnType = GetDataType(dt.Rows[0][DataType].ToString());
            StringBuilder strCont = new StringBuilder();
            strCont.Append("using System;\n");
            strCont.Append("using System.Collections;\n");
            strCont.Append("using System.Collections.Generic;\n");
            strCont.Append("using System.Linq;\n");
            strCont.Append("using System.Data;\n");
            strCont.Append("using System.Web;\n");
            strCont.Append("using System.Web.Mvc;\n");
            strCont.Append("using System.Text;\n");
            strCont.Append("using " + ProjectName + ".Common;\n");
            strCont.Append("using " + ProjectName + ".Model;\n");
            strCont.Append("using " + ProjectName + ".BLL;\n");
            strCont.Append("using " + ProjectName + ".Web." + AreaName + ".Filters;\n");
            strCont.Append("\n");
            strCont.Append("namespace " + ProjectName + ".Web." + AreaName + ".Controllers\n");
            strCont.Append("{\n");
            strCont.Append("/// <summary>\n");
            strCont.Append("/// " + ClassDesc + "-控制器\n");
            strCont.Append("/// 创建人:" + Author + "\n");
            strCont.Append("/// 日期:" + DateTimeOffset.Now.ToString() + "\n");
            strCont.Append("/// </summary>\n");
            strCont.Append("\n");
            strCont.Append("public class " + ClassName + "Controller : BaseController\n");
            strCont.Append("{\n");

            strCont.Append("#region 列表\n");
            strCont.Append("/// <summary>\n");
            strCont.Append("/// 列表\n");
            strCont.Append("/// GET: /" + AreaName + "/" + ClassName + "/\n");
            strCont.Append("/// </summary>\n");
            strCont.Append("/// <returns></returns>\n");
            strCont.Append("[Limit(Module = \"MOD_" + ClassName.ToUpper() + "\", Limit = \"LM_LIST\")]\n");
            strCont.Append("public ActionResult Index(int page = 1)\n");
            strCont.Append("{\n");
            strCont.Append("Hashtable hs = new Hashtable();\n");
            strCont.Append("#region 查询条件\n");
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                strCont.Append("string " + dt.Rows[i][ColumnName] + " = Config.Request(Request[\"" + dt.Rows[i][ColumnName] + "\"], \"\");\n");
                strCont.Append("if (!string.IsNullOrEmpty(" + dt.Rows[i][ColumnName] + ")) hs.Add(\"" + dt.Rows[i][ColumnName] + "\", " + dt.Rows[i][ColumnName] + ");\n");
                strCont.Append("\n");
            }
            strCont.Append("#endregion\n");
            strCont.Append("PageHelper p = new PageHelper\n");
            strCont.Append("{\n");
            strCont.Append("FieldOrder = \"a." + ColumnID + " asc\",\n");
            strCont.Append("PageSize = 10,\n");
            strCont.Append("CurrentPage = page,\n");
            strCont.Append("PageUrl = \"?\",\n");
            strCont.Append("PageStrType = EnumList.PageStrType.Ma\n");
            strCont.Append("};\n");
            strCont.Append("StringBuilder PageStr;\n");
            strCont.Append("DataTable dt = Factory." + ClassName + "().GetInfoPageList(null, hs, p, out PageStr);\n");
            strCont.Append("ViewBag.Page = p.CurrentPage;\n");
            strCont.Append("ViewBag.PageSize = p.PageSize;\n");
            strCont.Append("ViewBag.PageStr = PageStr;\n");
            strCont.Append("return View(dt);\n");
            strCont.Append("}\n");
            strCont.Append("#endregion\n");
            strCont.Append("\n");

            strCont.Append("#region 添加(表单)\n");
            strCont.Append("/// <summary>\n");
            strCont.Append("/// 添加(表单)\n");
            strCont.Append("/// GET: /" + AreaName + "/" + ClassName + "/Add\n");
            strCont.Append("/// </summary>\n");
            strCont.Append("/// <returns></returns>\n");
            strCont.Append("[Limit(Module = \"MOD_" + ClassName.ToUpper() + "\", Limit = \"LM_ADD\")]\n");
            strCont.Append("public ActionResult Add()\n");
            strCont.Append("{\n");
            strCont.Append("return View(\"Add\", new " + ClassName + "Model());\n");
            strCont.Append("}\n");
            strCont.Append("#endregion\n");
            strCont.Append("\n");

            strCont.Append("#region 添加(表单提交)\n");
            strCont.Append("/// <summary>\n");
            strCont.Append("/// 添加(表单提交)\n");
            strCont.Append("/// POST: /" + AreaName + "/" + ClassName + "/Add\n");
            strCont.Append("/// </summary>\n");
            strCont.Append("/// <returns></returns>\n");
            strCont.Append("[HttpPost]\n");
            strCont.Append("[ValidateAntiForgeryToken]\n");
            strCont.Append("[Limit(Module = \"MOD_" + ClassName.ToUpper() + "\", Limit = \"LM_ADD\")]\n");
            strCont.Append("public ActionResult Add(" + ClassName + "Model " + ObjName + "Model)\n");
            strCont.Append("{\n");
            strCont.Append("int result = 0;\n");
            strCont.Append("//页面上没有的实体项赋初始值\n");
            strCont.Append("" + ObjName + "Model.AdminId = (int)Session[\"AdminId\"];\n");
            strCont.Append("" + ObjName + "Model.CreationDate = DateTimeOffset.Now;\n");
            strCont.Append("" + ObjName + "Model.LastUpdateDate = DateTimeOffset.Now;\n");
            strCont.Append("result = Factory." + ClassName + "().InsertInfo(null, " + ObjName + "Model);\n");
            strCont.Append("if (result > 0)\n");
            strCont.Append("{\n");
            strCont.Append("Factory.AdminLog().InsertLog(null, EnumList.AdminLogActionType.Add, \"添加ID为\" + result + \"的" + ClassDesc + "!\");\n");
            //strCont.Append("return Content(Config.MsgGotoUrl(\"操作成功!\", Url.Action(\"Index\")));\n");
            strCont.Append("return Content(Config.MsgReload(\"操作成功!\"));\n");
            strCont.Append("}\n");
            strCont.Append("else\n");
            strCont.Append("{\n");
            strCont.Append("ViewBag.ErrMsg = \"操作失败!\";\n");
            strCont.Append("}\n");
            strCont.Append("return View(\"Add\"," + ObjName + "Model);\n");
            strCont.Append("}\n");
            strCont.Append("#endregion\n");
            strCont.Append("\n");

            strCont.Append("#region 修改(表单)\n");
            strCont.Append("/// <summary>\n");
            strCont.Append("/// 修改(表单)\n");
            strCont.Append("/// GET: /" + AreaName + "/" + ClassName + "/Edit\n");
            strCont.Append("/// </summary>\n");
            strCont.Append("/// <returns></returns>\n");
            strCont.Append("[Limit(Module = \"MOD_" + ClassName.ToUpper() + "\", Limit = \"LM_EDIT\")]\n");
            strCont.Append("public ActionResult Edit(" + ColumnType + " " + ColumnID + " = 0,int page = 1)\n");
            strCont.Append("{\n");
            strCont.Append("" + ClassName + "Model " + ObjName + "Model = Factory." + ClassName + "().GetInfo(null, " + ColumnID + ");\n");
            strCont.Append("if (" + ObjName + "Model == null)\n");
            strCont.Append("{\n");
            strCont.Append("" + ObjName + "Model = new " + ClassName + "Model();\n");
            strCont.Append("}\n");
            strCont.Append("return View(\"Add\"," + ObjName + "Model);\n");
            strCont.Append("}\n");
            strCont.Append("#endregion\n");
            strCont.Append("\n");

            strCont.Append("#region 修改(表单提交)\n");
            strCont.Append("/// <summary>\n");
            strCont.Append("/// 修改(表单提交)\n");
            strCont.Append("/// POST: /" + AreaName + "/" + ClassName + "/Edit\n");
            strCont.Append("/// </summary>\n");
            strCont.Append("/// <returns></returns>\n");
            strCont.Append("[HttpPost]\n");
            strCont.Append("[ValidateAntiForgeryToken]\n");
            strCont.Append("[Limit(Module = \"MOD_" + ClassName.ToUpper() + "\", Limit = \"LM_EDIT\")]\n");
            strCont.Append("public ActionResult Edit(" + ClassName + "Model " + ObjName + "Model, " + ColumnType + " " + ColumnID + " = 0,int page = 1)\n");
            strCont.Append("{\n");
            strCont.Append("int result = 0;\n");
            strCont.Append("" + ClassName + "Model tempModel = Factory." + ClassName + "().GetInfo(null, " + ColumnID + ");\n");
            strCont.Append("if (tempModel != null)\n");
            strCont.Append("{\n");
            strCont.Append("//页面上没有的实体项赋原始值\n");
            strCont.Append("" + ObjName + "Model.AdminId = tempModel.AdminId;\n");
            strCont.Append("" + ObjName + "Model.CreationDate = tempModel.CreationDate;\n");
            strCont.Append("" + ObjName + "Model.LastUpdateDate = DateTimeOffset.Now;\n");
            strCont.Append("result = Factory." + ClassName + "().UpdateInfo(null, " + ObjName + "Model, " + ColumnID + ");\n");
            strCont.Append("}\n");
            strCont.Append("if (result > 0)\n");
            strCont.Append("{\n");
            strCont.Append("Factory.AdminLog().InsertLog(null, EnumList.AdminLogActionType.Edit, \"修改ID为\" + " + ColumnID + " + \"的" + ClassDesc + "!\");\n");
            //strCont.Append("return Content(Config.MsgGotoUrl(\"操作成功!\", Url.Action(\"Index\") + \"?page=\" + page));\n");
            strCont.Append("return Content(Config.MsgReload(\"操作成功!\"));\n");
            strCont.Append("}\n");
            strCont.Append("else\n");
            strCont.Append("{\n");
            strCont.Append("ViewBag.ErrMsg = \"操作失败!\";\n");
            strCont.Append("}\n");
            strCont.Append("return View(\"Add\"," + ObjName + "Model);\n");
            strCont.Append("}\n");
            strCont.Append("#endregion\n");
            strCont.Append("\n");

            strCont.Append("#region 删除(Ajax提交)\n");
            strCont.Append("/// <summary>\n");
            strCont.Append("/// 删除(Ajax提交)\n");
            strCont.Append("/// POST: /" + AreaName + "/" + ClassName + "/Del\n");
            strCont.Append("/// </summary>\n");
            strCont.Append("/// <returns></returns>\n");
            strCont.Append("[HttpPost]\n");
            strCont.Append("[ValidateAntiForgeryToken]\n");
            strCont.Append("[Limit(Module = \"MOD_" + ClassName.ToUpper() + "\", Limit = \"LM_DEL\")]\n");
            strCont.Append("public ActionResult Del(string[] " + ColumnID + ")\n");
            strCont.Append("{\n");
            strCont.Append("object json;\n");
            strCont.Append("int i = 0;\n");
            strCont.Append("List<string> list=new List<string>();\n");
            strCont.Append("foreach (string item in " + ColumnID + ")\n");
            strCont.Append("{\n");
            strCont.Append("int int" + ColumnID + " = item.ToInt();\n");
            strCont.Append("" + ClassName + "Model " + ObjName + "Model = Factory." + ClassName + "().GetInfo(null, int" + ColumnID + ");\n");
            strCont.Append("if (" + ObjName + "Model != null)\n");
            strCont.Append("{\n");
            strCont.Append("int result = Factory." + ClassName + "().DeleteInfo(null, int" + ColumnID + ");\n");
            strCont.Append("if (result > 0) { i++; list.Add(item); }\n");
            strCont.Append("}\n");
            strCont.Append("}\n");
            strCont.Append("if (i > 0)\n");
            strCont.Append("{\n");
            strCont.Append("Factory.AdminLog().InsertLog(null, EnumList.AdminLogActionType.Del, \"删除ID为\" + list.ListToStr() + \"的" + ClassDesc + "!\");\n");
            strCont.Append("json = new { code = 1, msg = \"操作成功!\" };\n");
            strCont.Append("}\n");
            strCont.Append("else\n");
            strCont.Append("{\n");
            strCont.Append("json = new { code = 0, msg = \"操作失败!\" };\n");
            strCont.Append("}\n");
            strCont.Append("return Json(json);\n");
            strCont.Append("}\n");
            strCont.Append("#endregion\n");
            strCont.Append("\n");

            strCont.Append("#region 开启(Ajax提交)\n");
            strCont.Append("/// <summary>\n");
            strCont.Append("/// 开启(Ajax提交)\n");
            strCont.Append("/// POST: /" + AreaName + "/" + ClassName + "/Open\n");
            strCont.Append("/// </summary>\n");
            strCont.Append("/// <returns></returns>\n");
            strCont.Append("[HttpPost]\n");
            strCont.Append("[ValidateAntiForgeryToken]\n");
            strCont.Append("[Limit(Module = \"MOD_" + ClassName.ToUpper() + "\", Limit = \"LM_OPEN\")]\n");
            strCont.Append("public ActionResult Open(string[] " + ColumnID + ")\n");
            strCont.Append("{\n");
            strCont.Append("object json;\n");
            strCont.Append("int i = 0;\n");
            strCont.Append("List<string> list=new List<string>();\n");
            strCont.Append("foreach (string item in " + ColumnID + ")\n");
            strCont.Append("{\n");
            strCont.Append("int int" + ColumnID + " = item.ToInt();\n");
            strCont.Append("" + ClassName + "Model " + ObjName + "Model = Factory." + ClassName + "().GetInfo(null, int" + ColumnID + ");\n");
            strCont.Append("if (" + ObjName + "Model != null)\n");
            strCont.Append("{\n");
            strCont.Append("" + ObjName + "Model.IsClose = (int)EnumList.CloseStatus.Open;\n");
            strCont.Append("" + ObjName + "Model.LastUpdateDate = DateTimeOffset.Now;\n");
            strCont.Append("int result = Factory." + ClassName + "().UpdateInfo(null, " + ObjName + "Model, int" + ColumnID + ");\n");
            strCont.Append("if (result > 0) { i++; list.Add(item); }\n");
            strCont.Append("}\n");
            strCont.Append("}\n");
            strCont.Append("if (i > 0)\n");
            strCont.Append("{\n");
            strCont.Append("Factory.AdminLog().InsertLog(null, EnumList.AdminLogActionType.Open, \"开启ID为\" + list.ListToStr() + \"的" + ClassDesc + "!\");\n");
            strCont.Append("json = new { code = 1, msg = \"操作成功!\" };\n");
            strCont.Append("}\n");
            strCont.Append("else\n");
            strCont.Append("{\n");
            strCont.Append("json = new { code = 0, msg = \"操作失败!\" };\n");
            strCont.Append("}\n");
            strCont.Append("return Json(json);\n");
            strCont.Append("}\n");
            strCont.Append("#endregion\n");
            strCont.Append("\n");

            strCont.Append("#region 关闭(Ajax提交)\n");
            strCont.Append("/// <summary>\n");
            strCont.Append("/// 关闭(Ajax提交)\n");
            strCont.Append("/// POST: /" + AreaName + "/" + ClassName + "/Close\n");
            strCont.Append("/// </summary>\n");
            strCont.Append("/// <returns></returns>\n");
            strCont.Append("[HttpPost]\n");
            strCont.Append("[ValidateAntiForgeryToken]\n");
            strCont.Append("[Limit(Module = \"MOD_" + ClassName.ToUpper() + "\", Limit = \"LM_CLOSE\")]\n");
            strCont.Append("public ActionResult Close(string[] " + ColumnID + ")\n");
            strCont.Append("{\n");
            strCont.Append("object json;\n");
            strCont.Append("int i = 0;\n");
            strCont.Append("List<string> list=new List<string>();\n");
            strCont.Append("foreach (string item in " + ColumnID + ")\n");
            strCont.Append("{\n");
            strCont.Append("int int" + ColumnID + " = item.ToInt();\n");
            strCont.Append("" + ClassName + "Model " + ObjName + "Model = Factory." + ClassName + "().GetInfo(null, int" + ColumnID + ");\n");
            strCont.Append("if (" + ObjName + "Model != null)\n");
            strCont.Append("{\n");
            strCont.Append("" + ObjName + "Model.IsClose = (int)EnumList.CloseStatus.Close;\n");
            strCont.Append("" + ObjName + "Model.LastUpdateDate = DateTimeOffset.Now;\n");
            strCont.Append("int result = Factory." + ClassName + "().UpdateInfo(null, " + ObjName + "Model, int" + ColumnID + ");\n");
            strCont.Append("if (result > 0) { i++; list.Add(item); }\n");
            strCont.Append("}\n");
            strCont.Append("}\n");
            strCont.Append("if (i > 0)\n");
            strCont.Append("{\n");
            strCont.Append("Factory.AdminLog().InsertLog(null, EnumList.AdminLogActionType.Close, \"关闭ID为\" + list.ListToStr() + \"的" + ClassDesc + "!\");\n");
            strCont.Append("json = new { code = 1, msg = \"操作成功!\" };\n");
            strCont.Append("}\n");
            strCont.Append("else\n");
            strCont.Append("{\n");
            strCont.Append("json = new { code = 0, msg = \"操作失败!\" };\n");
            strCont.Append("}\n");
            strCont.Append("return Json(json);\n");
            strCont.Append("}\n");
            strCont.Append("#endregion\n");

            strCont.Append("}\n");
            strCont.Append("}\n");
            //------------------------------------------------------------------生成文件
            IOHelper.FolderCheck(SavePath + @"\_out\" + ClassName + @"\");
            string strFilePath = SavePath + @"\_out\" + ClassName + @"\" + ClassName + "Controller.cs";
            IOHelper.FileCreate(strFilePath, strCont.ToString());
        }
        #endregion

        #region 生成列表View
        /// <summary>
        /// 生成列表View
        /// </summary>
        /// <param name="dt"></param>
        public void CreateList(DataTable dt)
        {
            string ColumnID = dt.Rows[0][ColumnName].ToString();
            StringBuilder strList = new StringBuilder();
            strList.Append("@model DataTable\n");
            strList.Append("@{\n");
            strList.Append("ViewBag.Title = \"" + ClassDesc + "列表\";\n");
            strList.Append("}\n");
            strList.Append("<div id=\"queryForms\">\n");
            strList.Append("<form id=\"queryForm\" method=\"get\" action=\"?\">\n");
            strList.Append("<dl>\n");
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                string strDataType = dt.Rows[i][DataType].ToString();
                int intLength = dt.Rows[i][Length].ToInt();
                string strMaxLength = "";
                if ((strDataType == "char" || strDataType == "varchar" || strDataType == "nchar" || strDataType == "nvarchar") && intLength > 0)
                {
                    strMaxLength = ",new{@maxlength=" + intLength / 2 + "}";
                }
                
                strList.Append("<dt>" + Config.Traditional2Simplified(dt.Rows[i][ColumnDesc].ToString()) + "：</dt>\n");
                strList.Append("<dd>@Html.TextBox(\"" + dt.Rows[i][ColumnName] + "\", Config.Request(Request[\"" + dt.Rows[i][ColumnName] + "\"], \"\")" + strMaxLength + ")</dd>\n");
            }
            strList.Append("<dd id=\"buttons\">\n");
            strList.Append("<input type=\"submit\" value=\"查询\" />\n");
            strList.Append("</dd>\n");
            strList.Append("</dl>\n");
            strList.Append("</form>\n");
            strList.Append("</div>\n");
            strList.Append("<div id=\"listForms\">\n");
            strList.Append("<h1>@ViewBag.Title</h1>\n");
            strList.Append("<div id=\"opLink\">\n");
            //strList.Append("操作选项：\n");
            strList.Append("<a href=\"javascript:;\" id=\"btnAdd\"><i></i>添加" + ClassDesc + "</a>\n");
            strList.Append("<a href=\"javascript:;\" id=\"btnEdit\"><i></i>修改" + ClassDesc + "</a>\n");
            strList.Append("<a href=\"javascript:;\" id=\"btnDel\"><i></i>删除" + ClassDesc + "</a>\n");
            strList.Append("<a href=\"javascript:;\" id=\"btnOpen\"><i></i>批量开启</a>\n");
            strList.Append("<a href=\"javascript:;\" id=\"btnClose\"><i></i>批量关闭</a>\n");
            strList.Append("<a href=\"javascript:;\" id=\"btnQuery\">查询" + ClassDesc + "</a>\n");
            strList.Append("</div>\n");
            strList.Append("<form id=\"listForm\">\n");
            strList.Append("@Html.AntiForgeryToken()\n");
            strList.Append("<div id=\"pager\">@Html.Raw(ViewBag.PageStr)</div>\n");
            strList.Append("<table  id=\"tbList\">\n");
            strList.Append("<tr>\n");
            strList.Append("<th><input id=\"checkAll\" type=\"checkbox\" /></th>\n");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strList.Append("<th>" + Config.Traditional2Simplified(dt.Rows[i][ColumnDesc].ToString()) + "</th>\n");
            }
            strList.Append("<th>操作</th>\n");
            strList.Append("</tr>\n");
            strList.Append("@{\n");
            strList.Append("int page = ViewBag.Page;\n");
            strList.Append("int PageSize = ViewBag.PageSize;\n");
            strList.Append("int i = (page - 1) * PageSize+1;\n");
            strList.Append("}\n");
            strList.Append("@foreach (DataRow dr in Model.Rows)\n");
            strList.Append("{\n");
            strList.Append("<tr>\n");
            strList.Append("<td><input name=\"" + ColumnID.ToString() + "\" type=\"checkbox\" value=\"@dr[\"" + ColumnID.ToString() + "\"]\" class=\"ItemID\"/></td>\n");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strList.Append("<td>@dr[\"" + dt.Rows[i][ColumnName] + "\"]</td>\n");
            }
            strList.Append("<td>\n");
            strList.Append("<a href=\"javascript:;\" onclick=\"opdialog('修改', '@Url.Action(\"Edit\", new { " + ColumnID.ToString() + " = dr[\"" + ColumnID.ToString() + "\"] })');\">修改</a>\n");
            strList.Append("<a href=\"javascript:;\" onclick=\"opdel('" + ColumnID.ToString() + "=@dr[\"" + ColumnID.ToString() + "\"]', '@Url.Action(\"Del\")')\">删除</a>\n");
            strList.Append("</td>\n");
            strList.Append("</tr>\n");
            strList.Append("}\n");
            strList.Append("</table>\n");
            strList.Append("<div id=\"pager\">@Html.Raw(ViewBag.PageStr)</div>\n");
            strList.Append("</form>\n");
            strList.Append("</div>\n");
            strList.Append("@section scripts{\n");
            strList.Append("<script type=\"text/javascript\">\n");
            strList.Append("$(function () {\n");
            strList.Append("//添加\n");
            strList.Append("$(\"#btnAdd\").click(function () {\n");
            //strList.Append("lib.gotoUrl('@Url.Action(\"Add\")');\n");
            strList.Append("opdialog('添加','@Url.Action(\"Add\")');\n");
            strList.Append("});\n");
            strList.Append("//修改\n");
            strList.Append("$(\"#btnEdit\").click(function () {\n");
            strList.Append("checkEdit('.ItemID', '@Url.Action(\"Edit\")?page=@ViewBag.Page&" + ColumnID.ToString() + "=');\n");
            strList.Append("});\n");
            strList.Append("//删除\n");
            strList.Append("$(\"#btnDel\").click(function () {\n");
            strList.Append("checkDel('.ItemID', '@Url.Action(\"Del\")');\n");
            strList.Append("});\n");
            strList.Append("//开放\n");
            strList.Append("$(\"#btnOpen\").click(function () {\n");
            strList.Append("checkOperate('.ItemID', '@Url.Action(\"Open\")');\n");
            strList.Append("});\n");
            strList.Append("//关闭\n");
            strList.Append("$(\"#btnClose\").click(function () {\n");
            strList.Append("checkOperate('.ItemID', '@Url.Action(\"Close\")');\n");
            strList.Append("});\n");
            strList.Append("//查询\n");
            strList.Append("$(\"#btnQuery\").click(function () {\n");
            strList.Append("query();\n");
            strList.Append("});\n");
            strList.Append("//全选\n");
            strList.Append("$(\"#checkAll\").click(function () {\n");
            strList.Append("lib.checkAll(this, \".ItemID\");\n");
            strList.Append("});\n");
            strList.Append("//改变全选状态\n");
            strList.Append("$(\".ItemID\").click(function (e) {\n");
            strList.Append("e.stopPropagation();//阻止执行父元素事件\n");
            strList.Append("lib.checkAllStatus(\"#checkAll\", \".ItemID\");\n");
            strList.Append("});\n");
            strList.Append("//单元格点击\n");
            strList.Append("$(\"#tbList tr td\").click(function () {\n");
            strList.Append("tdClick(this);\n");
            strList.Append("});\n");
            strList.Append("});\n");
            strList.Append("</script>\n");
            strList.Append("}\n");
            //------------------------------------------------------------------生成文件
            IOHelper.FolderCheck(SavePath + @"\_out\" + ClassName + @"\");
            string strFilePath = SavePath + @"\_out\" + ClassName + @"\Index.cshtml";
            IOHelper.FileCreate(strFilePath, strList.ToString());
        }
        #endregion

        #region 生成表单View
        /// <summary>
        /// 生成表单View
        /// </summary>
        /// <param name="dt"></param>
        public void CreateForm(DataTable dt)
        {
            StringBuilder strForm = new StringBuilder();
            strForm.Append("@model " + ClassName + "Model\n");
            strForm.Append("@{\n");
            strForm.Append("ViewBag.Title = \"添加/修改" + ClassDesc + "\";\n");
            strForm.Append("}\n");
            strForm.Append("<div id=\"addForms\">\n");
            strForm.Append("<h1>@ViewBag.Title</h1>\n");
            strForm.Append("@using (Html.BeginForm())\n");
            strForm.Append("{\n");
            strForm.Append("@Html.AntiForgeryToken()\n");
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                string strDataType = dt.Rows[i][DataType].ToString();
                int intLength = dt.Rows[i][Length].ToInt();
                string strMaxLength = "";
                if ((strDataType == "char" || strDataType == "varchar" || strDataType == "nchar" || strDataType == "nvarchar") && intLength > 0)
                {
                    strMaxLength = ",new{@maxlength=" + intLength / 2 + "}";
                }

                strForm.Append("<dl>\n");
                strForm.Append("<dt>" + Config.Traditional2Simplified(dt.Rows[i][ColumnDesc].ToString()) + "：</dt>\n");
                strForm.Append("<dd>@Html.TextBoxFor(model => model." + dt.Rows[i][ColumnName] + strMaxLength + ")</dd>\n");
                strForm.Append("</dl>\n");
            }
            strForm.Append("<div id=\"buttons\">\n");
            strForm.Append("<input type=\"submit\" value=\"提交\" />\n");
            strForm.Append("<input type=\"button\" value=\"返回\" onclick=\"javascript:lib.closeFrameDialog();\" />\n");
            strForm.Append("<label id=\"errMsg\">@ViewBag.ErrMsg</label>\n");
            strForm.Append("</div>\n");
            strForm.Append("}\n");
            strForm.Append("</div>\n");
            strForm.Append("@section Scripts{\n");
            strForm.Append("<script type=\"text/javascript\">\n");
            strForm.Append("$(function () {\n");
            strForm.Append("$(\"form:eq(0)\").validate({\n");
            strForm.Append("errorPlacement: function (error, element) {\n");
            strForm.Append("error.appendTo(element.parent());\n");
            strForm.Append("},\n");
            strForm.Append("errorClass: \"error\",\n");
            strForm.Append("errorElement: \"label\",\n");
            strForm.Append("wrapper: \"\",\n");
            strForm.Append("success: function (element) {\n");
            strForm.Append("element.addClass(\"valid\");\n");
            strForm.Append("},\n");
            strForm.Append("rules: {\n");
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                strForm.Append("" + dt.Rows[i][ColumnName] + ": { required: true }");
                if (i < dt.Rows.Count - 1)
                {
                    strForm.Append(",\n");
                }
                else
                {
                    strForm.Append("\n");
                }
            }
            strForm.Append("},\n");
            strForm.Append("messages: {\n");
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                strForm.Append("" + dt.Rows[i][ColumnName] + ": { required: \"" + Config.Traditional2Simplified(dt.Rows[i][ColumnDesc].ToString()) + "不能为空\" }");
                if (i < dt.Rows.Count - 1)
                {
                    strForm.Append(",\n");
                }
                else
                {
                    strForm.Append("\n");
                }
            }
            strForm.Append("},\n");
            strForm.Append("submitHandler: function (form) {\n");
            strForm.Append("form.submit();\n");
            strForm.Append("}\n");
            strForm.Append("});\n");
            strForm.Append("});\n");
            strForm.Append("</script>\n");
            strForm.Append("}\n");
            //------------------------------------------------------------------生成文件
            IOHelper.FolderCheck(SavePath + @"\_out\" + ClassName + @"\");
            string strFilePath = SavePath + @"\_out\" + ClassName + @"\Add.cshtml";
            IOHelper.FileCreate(strFilePath, strForm.ToString());
        }
        #endregion

        //================

        #region 生成ClassFactory
        /// <summary>
        /// 生成ClassFactory
        /// </summary>
        /// <param name="dt"></param>
        public void CreateClassFactory(DataTable dt)
        {
            StringBuilder strClass = new StringBuilder();
            strClass.Append("using System;\n");
            strClass.Append("using System.Collections.Generic;\n");
            strClass.Append("using System.Linq;\n");
            strClass.Append("using System.Text;\n");
            strClass.Append("using System.Threading.Tasks;\n");
            strClass.Append("\n");
            strClass.Append("namespace " + ProjectName + ".BLL\n");
            strClass.Append("{\n");
            strClass.Append("/// <summary>\n");
            strClass.Append("/// 工厂类\n");
            strClass.Append("/// </summary>\n");
            strClass.Append("public class Factory:Base.BaseFactory\n");
            strClass.Append("{\n");
            foreach (DataRow dr in dt.Rows)
            {
                string strClassName = GetClassName(dr["Name"].ToString());
                string strClassDesc = dr["Description"].ToString();
                strClass.Append("#region " + strClassDesc + "\n");
                strClass.Append("private static " + strClassName + "BLL _" + strClassName.ToLower() + "bll;\n");
                strClass.Append("/// <summary>\n");
                strClass.Append("/// " + strClassDesc + "\n");
                strClass.Append("/// </summary>\n");
                strClass.Append("/// <returns></returns>\n");
                strClass.Append("public static " + strClassName + "BLL " + strClassName + "()\n");
                strClass.Append("{\n");
                strClass.Append("if (_" + strClassName.ToLower() + "bll == null)\n");
                strClass.Append("{\n");
                strClass.Append("_" + strClassName.ToLower() + "bll = new " + strClassName + "BLL();\n");
                strClass.Append("}\n");
                strClass.Append("return _" + strClassName.ToLower() + "bll;\n");
                strClass.Append("}\n");
                strClass.Append("#endregion\n");
            }
            strClass.Append("}\n");
            strClass.Append("}\n");
            //------------------------------------------------------------------生成文件
            string strFilePath = SaveClassFactoryPath + @"\_Factory.cs";
            IOHelper.FileCreate(strFilePath, strClass.ToString());
        }
        #endregion

        #region 生成DbUtility
        /// <summary>
        /// 生成DbUtility
        /// </summary>
        /// <param name="dt"></param>
        public void CreateDbUtility(DataTable dt)
        {
            StringBuilder strDb = new StringBuilder();
            strDb.Append("using System;\n");
            strDb.Append("using System.Collections.Generic;\n");
            strDb.Append("using System.Linq;\n");
            strDb.Append("using System.Text;\n");
            strDb.Append("using System.Threading.Tasks;\n");
            strDb.Append("using System.Data.Entity;\n");
            strDb.Append("\n");
            strDb.Append("namespace " + ProjectName + ".Model\n");
            strDb.Append("{\n");
            strDb.Append("public class DbContextConfig : DbContext\n");
            strDb.Append("{\n");
            strDb.Append("public DbContextConfig()\n");
            strDb.Append(": base(\"name=SqlConnStr\")\n");
            strDb.Append("{ }\n");
            strDb.Append("\n");
            foreach (DataRow dr in dt.Rows)
            {
                string strClassName = GetClassName(dr["Name"].ToString());
                string strClassDesc = dr["Description"].ToString();
                strDb.Append("#region " + strClassDesc + "\n");
                strDb.Append("/// <summary>\n");
                strDb.Append("/// " + strClassDesc + "\n");
                strDb.Append("/// </summary>\n");
                strDb.Append("/// <returns></returns>\n");
                strDb.Append("public DbSet<" + strClassName + "Model> " + strClassName + " { get; set; }\n");
                strDb.Append("#endregion\n");
            }
            strDb.Append("}\n");
            strDb.Append("}\n");
            //------------------------------------------------------------------生成文件
            string strFilePath = SaveDbUtilityPath + @"\_DbContextConfig.cs";
            IOHelper.FileCreate(strFilePath, strDb.ToString());
        }
        #endregion
    }
    #endregion
}