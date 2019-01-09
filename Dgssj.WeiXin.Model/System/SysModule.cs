using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dgssj.WeiXin.Model
{
    public partial class SysModule
    {
        public int Id { get; set; }

        /// <summary>
        /// 父级菜单
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 模块名称 
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 区域 
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 控制器 
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// ACTION 
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Icon 
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Url 
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 是否菜单
        /// </summary>
        public bool IsMenu { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsVisible { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
    /// <summary>
    /// 扩展字段
    /// </summary>
    public partial class SysModule
    {
        /// <summary>
        /// 分类层级
        /// </summary>
        [NotMapped]
        public int Level { get; set; }

        /// <summary>
        /// 子分类
        /// </summary>
        [NotMapped]
        public List<SysModule> Children { get; set; }

        /// <summary>
        /// 是否存在子分类
        /// </summary>
        [NotMapped]
        public bool HasChild
        {
            get
            {
                return this.Children != null && this.Children.Count > 0;
            }
        }

        /// <summary>
        /// 子分类数量
        /// </summary>
        [NotMapped]
        public int ChildNum
        {
            get
            {
                if (this.Children == null)
                    return 0;
                return this.Children.Count;
            }
        }

        /// <summary>
        /// 层级结构
        /// </summary>
        [NotMapped]
        public string Hierarchy { get; set; }
    }
}

