using System;
using System.Collections.Generic;
using System.Text;

namespace Dgssj.WeiXin.Model
{
    public class base_dict
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int ID { get; set; }

        /// <summary>
        /// Desc:父级
        /// Default:NULL
        /// Nullable:True
        /// </summary>           
        public int? parentID { get; set; }
        
        /// <summary>
        /// 类型ID
        /// </summary>

        public int typeID { set; get; }

        /// <summary>
        /// Desc:编码
        /// Default:NULL
        /// Nullable:True
        /// </summary>           
        public string Code { get; set; }

        /// <summary>
        /// Desc:名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:拼音
        /// Default:NULL
        /// Nullable:True
        /// </summary>           
        public string Spell { get; set; }

        /// <summary>
        /// Desc:值 
        /// Default:NULL
        /// Nullable:True
        /// </summary>           
        public string Value { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:NULL
        /// Nullable:True
        /// </summary>           
        public string Notes { get; set; }

        /// <summary>
        /// Desc:状态
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int State { get; set; }
    }
}
