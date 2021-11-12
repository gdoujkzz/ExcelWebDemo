namespace ExcelWebDemo.Common.Excel
{
    public class ExcelAttribute : Attribute
    {
        /// <summary>
        /// 列宽,32=1px
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 列的标题，如果资源文件中不存在该值的KEY，则标题直接为该值
        /// </summary>
        public string Title { get; set; }
    }
}
