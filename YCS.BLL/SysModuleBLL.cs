using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using System.Web.Caching;
using YCS.Common;
using YCS.Model;
using YCS.DAL;

namespace YCS.BLL
{
/// <summary>
/// 後台模塊表-业务逻辑类
/// 创建人:楊小明
/// 日期:2017/2/20 11:35:17
/// </summary>

public class  SysModuleBLL:Base.SysModule
{

private readonly SysModuleDAL sysDAL=new SysModuleDAL();

#region 取信息分页列表
/// <summary>
/// 取信息分页列表
/// </summary>
public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
{
return sysDAL.GetInfoPageList(trans, hs, p, out PageStr);
}
#endregion

#region 取DataTable
/// <summary>
/// 取DataTable
/// </summary>
public DataTable GetDataTable(SqlTransaction trans)
{
StringBuilder LeftJoin = new StringBuilder();
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
string FieldShow="a.*";
string FieldOrder="a.SysModuleId asc";
return sysDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
#endregion

#region 取实体集合
/// <summary>
/// 取实体集合
/// </summary>
public List<SysModuleModel> GetModels(SqlTransaction trans)
{
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
string FieldOrder="SysModuleId asc";
return sysDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
#endregion

#region 取实体
/// <summary>
/// 取实体
/// </summary>
public SysModuleModel GetModel(SqlTransaction trans, int SysModuleId)
{
StringBuilder SqlQuery = new StringBuilder();
SqlQuery.Append(" and SysModuleId=@SysModuleId");
List<SqlParameter> listParams = new List<SqlParameter>();
listParams.Add(new SqlParameter("@SysModuleId", SysModuleId));
return sysDAL.GetModel(trans, SqlQuery, listParams);
}
#endregion

#region 取记录总数
/// <summary>
/// 取记录总数
/// </summary>
public int GetAllCount(SqlTransaction trans)
{
StringBuilder LeftJoin = new StringBuilder();
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
return sysDAL.GetAllCount(trans,LeftJoin, SqlQuery, listParams);
}
#endregion

#region 取字段总和
/// <summary>
/// 取字段总和
/// </summary>
public decimal GetAllSum(SqlTransaction trans)
{
StringBuilder LeftJoin = new StringBuilder();
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
return sysDAL.GetAllSum(trans,LeftJoin, SqlQuery, listParams,"a.SysModuleId");
}
#endregion
#region 读取排序号
/// <summary>
/// 读取排序号
/// </summary>
public int GetSeqNo(SqlTransaction trans, int intParentId)
{
    return sysDAL.GetSeqNo(trans, intParentId);
}
#endregion
#region 显示路径
/// <summary>
/// 显示路径
/// </summary>
public StringBuilder ShowPath(SqlTransaction trans, int intSysModuleId)
{
    StringBuilder tempStr = new StringBuilder("根结点");
    SysModuleModel sysMolModel = new SysModuleModel();
    sysMolModel = sysDAL.GetInfo(trans, intSysModuleId);
    if (sysMolModel == null)
    {
        return tempStr;
    }
    else
    {
        string strPath = sysDAL.GetPath(trans, intSysModuleId).ToString();
        string[] arrPath = strPath.Split(',');
        foreach (var item in arrPath)
        {
            SysModuleModel sysMolModel_2 = new SysModuleModel();
            sysMolModel_2 = sysDAL.GetInfo(trans, Convert.ToInt32(item));
            if (sysMolModel_2 != null)
            {
                tempStr.Append(" > " + sysMolModel_2.ModuleName);
            }
        }
        return tempStr;
    }
}
#endregion
#region 检查信息,保持某字段的唯一性
/// <summary>
/// 检查信息,保持某字段的唯一性
/// </summary>
public bool CheckInfo(SqlTransaction trans, int intParentId, string strFieldName, string strFieldValue, int intSysModuleId)
{
    return sysDAL.CheckInfo(trans, intParentId, strFieldName, strFieldValue, intSysModuleId);
}
#endregion
#region 排序信息
/// <summary>
/// 排序信息
/// </summary>
/// <returns></returns>
public void OrderInfo(SqlTransaction trans, int intParentId, int intSeqNo, int intOldSeqNo)
{
    sysDAL.OrderInfo(trans, intParentId, intSeqNo, intOldSeqNo);
}
#endregion
#region 增加子级数
/// <summary>
/// 增加子级数
/// </summary>
public void AddChildNum(SqlTransaction trans, int intSysModuleId)
{
    sysDAL.AddChildNum(trans, intSysModuleId);
}
#endregion
#region 减少子级数
/// <summary>
/// 减少子级数
/// </summary>
public void CutChildNum(SqlTransaction trans, int intSysModuleId)
{
    sysDAL.CutChildNum(trans, intSysModuleId);
}
#endregion
#region 显示下拉树形列表
/// <summary>
/// 显示下拉树形列表
/// </summary>
public List<SelectListItem> GetSelectTreeList(SqlTransaction trans, int intParentId, int intLevel, List<SelectListItem> list, string strSql)
{
    return sysDAL.GetSelectTreeList(trans, intParentId, intLevel, list, strSql);
}
#endregion

#region 读取模块名称
/// <summary>
/// 读取模块名称
/// </summary>
public string GetModuleNames(SqlTransaction trans, string strSysModuleIds)
{
    return sysDAL.GetModuleNames(trans, strSysModuleIds);
}
#endregion
#region 转移时更新子级数
/// <summary>
/// 修改时更新子级数
/// </summary>
public void UpdateChildNum(SqlTransaction trans, int intTargetParentId, int intOldParentId)
{
    if (intTargetParentId != intOldParentId)
    {
        sysDAL.AddChildNum(trans, intTargetParentId);
        sysDAL.CutChildNum(trans, intOldParentId);
    }
}
#endregion
#region 取管理菜单
/// <summary>
/// 取管理菜单
/// </summary>
public List<SysModuleModel> GetMenu(SqlTransaction trans, int intParentId, string strSql)
{
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and IsClose=0 and IsShow=1 and ParentId=@ParentId" + strSql);
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@ParentId", intParentId));
    string FieldOrder = "SeqNo asc";
    return sysDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
#endregion
#region 取列表(复选)
/// <summary>
/// 取列表(复选)
/// </summary>
public string GetModuleList(SqlTransaction trans, string SysModuleIds, int intSysRoleId)
{
    return sysDAL.GetModuleList(trans, SysModuleIds, intSysRoleId);
}
#endregion
#region 检查模块是否存在
/// <summary>
/// 检查模块是否存在
/// </summary>
public bool IsExist(SqlTransaction trans, string strModuleValue)
{
    StringBuilder LeftJoin = new StringBuilder();
    StringBuilder SqlQuery = new StringBuilder();
    SqlQuery.Append(" and IsClose=0");
    SqlQuery.Append(" and ModuleValue=@ModuleValue");
    List<SqlParameter> listParams = new List<SqlParameter>();
    listParams.Add(new SqlParameter("@ModuleValue", strModuleValue));
    return sysDAL.GetAllCount(trans, LeftJoin, SqlQuery, listParams) > 0;
}
#endregion

#region 取列表(有权限)
/// <summary>
/// 取列表(有权限)
/// </summary>
public string GetModuleListByAdmin(SqlTransaction trans, string SysModuleIds, int intSysRoleId, int adminId)
{
    return sysDAL.GetModuleListByAdmin(trans, SysModuleIds, intSysRoleId, adminId);
}
#endregion
}
}
