using PetaPoco.NetCore;
using System;

namespace Dgssj.WeiXin.Model
{
    public class Sys_Users
    {
        public int ID { set; get; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { set; get; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { set; get; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { set; get; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { set; get; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { set; get; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime EditTime { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { set; get; }
        /// <summary>
        /// 状态 1启用 0禁用 99删除
        /// </summary>
        public int Status { set; get; }

        #region 查询条件
        [Ignore]public DateTime BeginDate { set; get; }
        [Ignore] public DateTime EndDate { set; get; }
        #endregion
    }
}
