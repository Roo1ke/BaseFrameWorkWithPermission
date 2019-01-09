using System;
using System.Collections.Generic;
using System.Text;

namespace Dgssj.WeiXin.Model
{
    //设置权限时的树形结构
    public class MenusTreeModel
    {
        public int id { set; get; }
        public string label { set; get; }
        public List<MenusTreeModel> children { set; get; }
    }
}
