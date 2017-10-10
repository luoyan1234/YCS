using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCS.BLL;

namespace YCS.BLL.Base
{
/// <summary>
/// 工厂类
/// </summary>
    public class BaseFactory
{
        #region 数据帮助类
        private static DataHelper _datahelper;
        /// <summary>
        /// 数据帮助类
        /// </summary>
        /// <returns></returns>
        public static DataHelper DataHelper()
        {
            if (_datahelper == null)
            {
                _datahelper = new DataHelper();
            }
            return _datahelper;
        }
        #endregion
}
}
