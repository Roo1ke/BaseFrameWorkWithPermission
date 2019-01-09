using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dgssj.WeiXin.Model
{
    public interface IWeChatConfig
    {
        /// <summary>
        /// 开发者ID
        /// </summary>
        string AppID { get; set; }
        /// <summary>
        /// 开发者秘钥
        /// </summary>
        string AppSecret { get; set; }
        /// <summary>
        /// token
        /// </summary>
        string Token { get; set; }
        /// <summary>
        /// 调用JSSDK页面地址
        /// </summary>
        string JSUrl { set; get; }
    }
}
