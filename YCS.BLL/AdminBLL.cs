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
/// 後台帳號表-业务逻辑类
/// 创建人:楊小明
/// 日期:2017/2/20 11:35:12
/// </summary>

public class  AdminBLL:Base.Admin
{

private readonly AdminDAL admDAL=new AdminDAL();
SysModuleBLL sysModBLL = new SysModuleBLL();
SysLimitBLL sysLimBLL = new SysLimitBLL();
SysRoleLimitBLL sysRolLimBLL = new SysRoleLimitBLL();

#region 取信息分页列表
/// <summary>
/// 取信息分页列表
/// </summary>
public DataTable GetInfoPageList(SqlTransaction trans, Hashtable hs, PageHelper p, out StringBuilder PageStr)
{
return admDAL.GetInfoPageList(trans, hs, p, out PageStr);
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
string FieldOrder="a.AdminId asc";
return admDAL.GetDataTable(trans, LeftJoin, SqlQuery, listParams, FieldShow, FieldOrder);
}
#endregion

#region 取实体集合
/// <summary>
/// 取实体集合
/// </summary>
public List<AdminModel> GetModels(SqlTransaction trans)
{
StringBuilder SqlQuery = new StringBuilder();
List<SqlParameter> listParams = new List<SqlParameter>();
string FieldOrder="AdminId asc";
return admDAL.GetModels(trans, SqlQuery, listParams, 0, FieldOrder);
}
#endregion

#region 取实体
/// <summary>
/// 取实体
/// </summary>
public AdminModel GetModel(SqlTransaction trans, int AdminId)
{
StringBuilder SqlQuery = new StringBuilder();
SqlQuery.Append(" and AdminId=@AdminId");
List<SqlParameter> listParams = new List<SqlParameter>();
listParams.Add(new SqlParameter("@AdminId", AdminId));
return admDAL.GetModel(trans, SqlQuery, listParams);
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
return admDAL.GetAllCount(trans,LeftJoin, SqlQuery, listParams);
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
return admDAL.GetAllSum(trans,LeftJoin, SqlQuery, listParams,"a.AdminId");
}
#endregion
#region 登录
/// <summary>
/// 登录
/// </summary>
public bool Login(SqlTransaction trans, string strAdminName, string strAdminPass)
{
    return admDAL.Login(trans, strAdminName, strAdminPass);
}
#endregion
#region 是否登录
/// <summary>
/// 是否登录
/// </summary>
public bool IsLogin(SqlTransaction trans)
{
    if (HttpContext.Current.User.Identity.IsAuthenticated)
    {
        HttpContext.Current.Session["AdminId"] = HttpContext.Current.User.Identity.Name.ToInt();
        return true;
    }
    return false;
    //return admDAL.IsLogin(trans);
}
#endregion
#region 清除管理员权限缓存
/// <summary>
/// 清除管理员权限缓存
/// </summary>
/// <param name="strSysRoleId"></param>
/// <returns></returns>
public void RemoveRoleLimitCache(SqlTransaction trans, int intSysRoleId)
{
    admDAL.RemoveRoleLimitCache(trans, intSysRoleId);
}
public void RemoveRoleLimitCacheBySysRoleGroupId(SqlTransaction trans, int SysRoleGroupId)
{
    admDAL.RemoveRoleLimitCacheBySysRoleGroupId(trans, SysRoleGroupId);
}
#endregion
#region 检查当前登录管理员的权限
/// <summary>
/// 检查当前登录管理员的权限
/// </summary>
/// <param name="strModuleLimit"></param>
/// <returns></returns>
public bool LimitChk(string Module, string Limit)
{
    if (Convert.ToInt32(HttpContext.Current.Session["AdminId"]) == Config.SystemAdminId)//超级管理员不受权限控制
    {
        return true;
    }
    else
    {
        //检查模块和操作权限是否存在
        if (!sysModBLL.IsExist(null, Module) || !sysLimBLL.IsExist(null, Limit))
        {
            return false;
        }
        else
        {
            string strModuleLimits = sysRolLimBLL.GetModuleLimits(null, Convert.ToInt32(HttpContext.Current.Session["AdminId"]));
            return strModuleLimits.Contains("," + Module + ":" + Limit + ",");
        }
    }
}
#endregion
}
}
