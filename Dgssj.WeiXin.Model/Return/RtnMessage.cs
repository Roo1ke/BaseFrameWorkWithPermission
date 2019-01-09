using System;
using System.Collections.Generic;
using System.Text;

namespace Dgssj.WeiXin.Model
{
    public class RtnMessage
    {
     
        private int _Code;
        public int Code
        {
            get { return _Code; }
            set
            {
                _Code = value;
                if (value == 0)
                {
                    Message = "操作成功";
                }
                else if (value == 1)
                {
                    Message = "操作失败";
                }
                else {
                    Message = "系统错误";
                }
            }
        }
        private string _Message;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public dynamic Data { set; get; }
    }
}
