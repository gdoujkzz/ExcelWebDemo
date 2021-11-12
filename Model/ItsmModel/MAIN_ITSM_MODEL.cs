using ExcelWebDemo.Common.Excel;

namespace ExcelWebDemo.Model.ExcelExportModel
{
    public class MAIN_ITSM_MODEL
    {

        /// <summary>
        /// 标题
        /// </summary>
        [Excel(Title="标题",Width =5000)]
        public string Title { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        [Excel(Title = "单据号", Width = 5000)]
        public string Bill_NO { get; set; }

        /// <summary>
        /// 二级分类
        /// </summary>
        [Excel(Title = "二级分类", Width = 5000)]
        public string SecondCategory { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Excel(Title = "创建人", Width = 5000)]
        public string Creator { get; set; }

        /// <summary>
        /// 事业部
        /// </summary>
        [Excel(Title = "事业部", Width = 5000)]
        public string BusinessDept { get; set; }

        /// <summary>
        /// 当前解决人
        /// </summary>
        [Excel(Title = "当前解决人", Width = 5000)]
        public string CurrentSolveUser { get; set; }

        /// <summary>
        /// 解决人
        /// </summary>
        [Excel(Title = "解决人", Width = 5000)]
        public string SolveUser { get; set; }

        /// <summary>
        /// 建单时间
        /// </summary>
        [Excel(Title = "建单时间", Width = 5000)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Excel(Title = "状态", Width = 5000)]
        public string Status { get; set; }

        /// <summary>
        /// 响应
        /// </summary>
        [Excel(Title = "响应", Width = 5000)]
        public string Response { get; set; }

        /// <summary>
        /// 满意度
        /// </summary>
        [Excel(Title = "满意度", Width = 5000)]
        public string Satisfaction { get; set; }

        /// <summary>
        /// 解决
        /// </summary>
        [Excel(Title = "解决", Width = 5000)]
        public string Solve { get; set; }

    }
}
