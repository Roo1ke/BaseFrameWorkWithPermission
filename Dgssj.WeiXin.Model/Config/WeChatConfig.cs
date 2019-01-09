using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dgssj.WeiXin.Model
{
    public class WeChatConfig:IWeChatConfig
    {
        /// <summary>
        /// 开发者ID
        /// </summary>
        public string AppID { get; set; }
        /// <summary>
        /// 开发者秘钥
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 调用JSSDK页面地址
        /// </summary>
        public string JSUrl { set; get; }
    }
}
