using MySell.Dal;
using System;
using System.Collections;
using System.Web.Mvc;

namespace MySell.App.Web
{
    public static class ControllerExtended
    {
        #region 私有成员
        private static readonly int _maxPageSize = 200;
        #endregion

        #region 公共方法
        /// <summary>
        /// 验证分页大小不超过限定值，以防止恶意搜索
        /// </summary>
        /// <param name="pageSize">请求的分页大小</param>
        /// <returns>安全校验后的大小</returns>
        public static int SafePageSize(int pageSize)
        {
            return Math.Min(_maxPageSize, pageSize);
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 验证分页大小不超过限定值，以防止恶意搜索
        /// </summary>
        /// <param name="controller">当前实例</param>
        /// <param name="pageSize">接受验证分页参数</param>
        /// <returns></returns>
        public static int SafePageSize(this Controller controller, int pageSize)
        {
            return SafePageSize(pageSize);
        }

        /// <summary>
        /// 创建一个不超过限定值的分页对象
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static Paged SafePaged(this Controller controller, int pageIndex, int pageSize)
        {
            return new Paged(pageIndex, SafePageSize(pageSize));
        }

        /// <summary>
        /// 向客户端响应一个Datatabless组件的JSON数据源
        /// </summary>
        /// <param name="controller">当前实例</param>
        /// <param name="page">分页结果及设定</param>
        /// <param name="result">搜索结果</param>
        /// <returns></returns>
        public static ActionResult DataTableSource(this Controller controller, Paged page, ICollection result)
        {
            var obj = new
            {
                draw = 1,
                pageIndex = page.PageIndex,
                pageSize = page.PageSize,
                recordsTotal = page.RowCount,
                recordsFiltered = page.RowCount,
                data = result
            };

            var view = new ContentResult
            {
                ContentType = "application/json",
                Content = Newtonsoft.Json.JsonConvert.SerializeObject(obj)
            };

            return view;
        }

        #endregion
    }
}