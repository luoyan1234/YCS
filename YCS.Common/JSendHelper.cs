namespace YCS.Common
{
    public class JSendHelper
    {
        /// <summary>
        /// Reference JSend response format - Success
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns></returns>
        public static object JSendSuccess(object data)
        {
            return new
            {
                status = "success",
                data = data
            };
        }

        /// <summary>
        /// Reference JSend response format - Fail
        /// </summary>
        /// <param name="failTitle">Fail title</param>
        /// <returns></returns>
        public static object JSendFail(string failTitle)
        {
            return new
            {
                status = "fail",
                data = new { title = failTitle }
            };
        }

        /// <summary>
        /// Reference JSend response format - Error
        /// </summary>
        /// <param name="errorMessage">Error message</param>
        /// <returns></returns>
        public static object JSendError(string errorMessage)
        {
            return new
            {
                status = "error",
                message = errorMessage
            };
        }
    }
}