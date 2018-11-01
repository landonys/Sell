using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySell.Dal
{
    /// <summary>
    /// 表示调用执行结果反馈
    /// </summary>
    /// <remarks>可以作为执行增删改订单操作响应。</remarks>
    public class Feedback
    {
        #region 公共属性
        /// <summary>
        /// 获取或设置本地调用是否成功执行。
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 获取或设置本次调用返回的响应代码。
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 获取或设置本次调用的结果消息说明。
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 获取或设置本次调用响应的结果数据。
        /// </summary>
        public object Result { get; set; }
        #endregion

        #region 实例方法
        /// <summary>
        /// 初始化执行结果反馈
        /// </summary>
        /// <param name="success">操作是否成功完成</param>
        /// <param name="message">操作反馈消息</param>
        /// <param name="code">操作响应代码</param>
        /// <param name="result">操作反馈结果</param>
        public Feedback(bool success, string message = "", int code = 0, object result = null)
        {
            this.Success = success;
            this.Code = code;
            this.Message = message;
            this.Result = result;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 创建一个调用成功的反馈结果
        /// </summary>
        /// <param name="msg">异常响应消息，为空默认返回“恭喜，执行成功！”</param>
        /// <param name="code">响应代码</param>
        /// <param name="result">响应结果</param>
        /// <returns>新的执行结果返回实例</returns>
        public static Feedback Ok(string msg = null, int code = 0, object result = null)
        {
            if (string.IsNullOrWhiteSpace(msg))
                msg = "恭喜，执行成功！";

            return new Feedback(true, msg, code, result);
        }

        /// <summary>
        /// 创建一个代用失败的反馈结果
        /// </summary>
        /// <param name="msg">异常响应消息，为空默认返回“”</param>
        /// <param name="code">响应代码 </param>
        /// <param name="result">响应结果</param>
        /// <returns>新的执行结果返回实例</returns>
        public static Feedback Failure(string msg = null, int code = 1, object result = null)
        {
            if (string.IsNullOrWhiteSpace(msg))
                msg = "抱歉，执行失败！";

            return new Feedback(false, msg, code, result);
        }

        /// <summary>
        /// 创建一个应用发出异常的反馈结果
        /// </summary>
        /// <param name="msg">异常响应消息，为空默认返回“发生未知错误，请查看错误日志！”</param>
        /// <param name="code">响应代码</param>
        /// <returns>新的执行结果返回实例</returns>
        /// <remarks>一般用于调用捕获了异常的中响应结果</remarks>
        public static Feedback Error(string msg = null, int code = -1)
        {
            if (string.IsNullOrWhiteSpace(msg))
                msg = "发生未知错误，请查看错误日志！";

            return new Feedback(false, msg, code);
        }
        #endregion
    }
}
