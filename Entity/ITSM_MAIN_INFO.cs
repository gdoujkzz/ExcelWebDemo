using SqlSugar;

namespace ExcelWebDemo.Entity
{
    [SugarTable("ITSM_MAIN_INFO")]
    public class ITSM_MAIN_INFO
    {

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string Bill_NO { get; set; }

        /// <summary>
        /// 二级分类
        /// </summary>
        public string SecondCategory { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 事业部
        /// </summary>
        public string BusinessDept { get; set; }

        /// <summary>
        /// 当前解决人
        /// </summary>
        public string CurrentSolveUser { get; set; }

        /// <summary>
        /// 解决人
        /// </summary>
        public string SolveUser { get; set; }

        /// <summary>
        /// 建单时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 响应
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// 满意度
        /// </summary>
        public string Satisfaction { get; set; }

        /// <summary>
        /// 解决
        /// </summary>
        public string Solve { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

    }
}
