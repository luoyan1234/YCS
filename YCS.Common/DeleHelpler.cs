using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCS.Common
{
    /// <summary>
    /// 委托类
    /// </summary>
    public class DeleHelpler
    {
        #region 执行事务
        /// <summary>
        /// 定义委托
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="list"></param>
        public delegate void DelegateTrans2(SqlTransaction trans, List<object> listInput);
        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="d2"></param>
        /// <param name="listInput"></param>
        /// <returns></returns>
        public static bool RunTrans(DelegateTrans2 d2, List<object> listInput)
        {
            using (SqlConnection conn = new SqlConnection(Config.SqlConnStr))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //执行委托方法
                        d2(trans, listInput);

                        //提交事务
                        trans.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        Config.Err(ex);
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 定义委托
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="listInput"></param>
        /// <param name="listOut"></param>
        public delegate void DelegateTrans3(SqlTransaction trans, List<object> listInput, out List<object> listOut);
        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="d3"></param>
        /// <param name="listInput"></param>
        /// <param name="listOut"></param>
        /// <returns></returns>
        public static bool RunTrans(DelegateTrans3 d3, List<object> listInput, out List<object> listOut)
        {
            using (SqlConnection conn = new SqlConnection(Config.SqlConnStr))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //执行委托方法
                        d3(trans, listInput, out listOut);

                        //提交事务
                        trans.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        listOut = null;
                        trans.Rollback();
                        Config.Err(ex);
                        return false;
                    }
                }
            }
        }
        #endregion
    }
}
