using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySell.Dal
{
    public class Paged
    {
        #region 公共属性
        /// <summary>
        /// 获取或设置当前页码
        /// </summary>
        public int PageIndex { set; get; }
        /// <summary>
        /// 获取或设置页大小
        /// </summary>
        public int PageSize { set; get; }
        /// <summary>
        /// 获取或设置记录总数
        /// </summary>
        public int RowCount { set; get; }

        /// <summary>
        /// 起始序号
        /// </summary>
        public int Start
        {
            get
            {
                return PageSize * PageIndex - PageSize + 1;
            }
        }

        /// <summary>
        /// 获取截止序号
        /// </summary>
        public int End
        {
            get
            {
                return PageSize * PageIndex;
            }
        }

        /// <summary>
        /// 获取总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (RowCount < 1)
                    return 0;

                int p = RowCount / PageSize;
                if (RowCount % PageSize != 0)
                    p++;
                return p;
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 创建分页实例
        /// </summary>
        /// <param name="index">当前页</param>
        /// <param name="size">分页大小</param>
        /// <param name="rowCount">记录总数</param>
        public Paged(int index, int size, int rowCount = 0)
        {
            this.PageIndex = index > 0 ? index : 1;
            this.PageSize = size > 0 ? size : 1;
            this.RowCount = rowCount;
        }
        #endregion
    }
}
