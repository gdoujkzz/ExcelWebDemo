using ExcelWebDemo.Provider;
using NPOI.SS.UserModel;
using NPOI.XSSF.Streaming;
using NPOI.XSSF.UserModel;
using System.Runtime.CompilerServices;

namespace ExcelWebDemo.Common.Excel
{
    public class ExcelHelper
    {


        /// <summary>
        /// 导出Excel（使用XLSX)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">导入的数据源</param>
        /// <param name="filename">文件名，不含扩展名</param>
        /// <returns></returns>
        public static byte[] Export<T>(List<T> list, string filename)
        {
            IWorkbook wb = new XSSFWorkbook();
            ISheet sheet = wb.CreateSheet(filename);
            SetColumnTitle<T>(sheet, list[0]);
            int index = 1;
            foreach (var model in list)
            {
                Type type = model.GetType();
                var properties = type.GetProperties();
                IRow row = sheet.CreateRow(index++);
                int j = 0;
                for (int i = 0; i < properties.Length; i++)
                {
                    var p = properties[i];
                    object[] objs = p.GetCustomAttributes(typeof(ExcelAttribute), true);
                    if (objs.Length > 0)
                    {
                        object obj = p.GetValue(model, null);
                        if (obj != null)
                        {
                            row.CreateCell(j).SetCellValue(obj.ToString());
                        }
                        else
                        {
                            row.CreateCell(j).SetCellValue("");
                        }
                        j++;
                    }
                }
            }
            byte[] buffer;
            using (NpoiMemoryStream ms = new NpoiMemoryStream())
            {
                ms.AllowClose = false;
                wb.Write(ms);
                ms.Flush();
                buffer = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(buffer, 0, buffer.Length);
                ms.AllowClose = true;
            }
            wb.Close();

            //强制清空占用内存，因为NPOI占用的内存，不建议手动GC。
            //GcCollectHelper.ClearMemory();

            return buffer;
        }


        /// <summary>
        /// 设置列标题
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sheet"></param>
        /// <param name="t"></param>
        private static void SetColumnTitle<T>(ISheet sheet, T t)
        {
            Type type = t.GetType();
            var properties = type.GetProperties();
            IRow row = sheet.CreateRow(0);
            int j = 0;
            for (int i = 0; i < properties.Length; i++)
            {
                var p = properties[i];
                object[] objs = p.GetCustomAttributes(typeof(ExcelAttribute), true);
                if (objs.Length > 0)
                {
                    ExcelAttribute obj = objs[0] as ExcelAttribute;
                    row.CreateCell(j).SetCellValue(obj.Title);
                    sheet.SetColumnWidth(j, obj.Width);
                    j++;
                }
            }
        }



        /// <summary>
        /// 导出Excel(直接使用SXSSFWorkbook)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static byte[] ExportStream<T>(List<T> list, string filename)
        {
            SXSSFWorkbook wb = new SXSSFWorkbook(1000);
            ISheet sheet = wb.CreateSheet(filename);
            SetColumnTitle<T>(sheet, list[0]);
            int index = 1;
            foreach (var model in list)
            {
                Type type = model.GetType();
                var properties = type.GetProperties();
                IRow row = sheet.CreateRow(index++);
                int j = 0;
                for (int i = 0; i < properties.Length; i++)
                {
                    var p = properties[i];
                    object obj = p.GetValue(model, null);
                    if (obj != null)
                    {
                        row.CreateCell(i).SetCellValue(obj.ToString());
                    }
                }
            }
            byte[] buffer;
            using (NpoiMemoryStream ms = new NpoiMemoryStream())
            {
                ms.AllowClose = false;
                wb.Write(ms);
                ms.Flush();
                buffer = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(buffer, 0, buffer.Length);
                ms.AllowClose = true;
            }
            wb.Dispose();
            return buffer;

        }



        /// <summary>
        /// 导出Excel分页(SXSSFWorkbook)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="action">分页函数的委托</param>
        /// <returns></returns>
        public static byte[] ExportStreamByPage<T>(string filename, int pageSize, Func<int, int, List<T>> action) where T : new()
        {
            IWorkbook wb = new SXSSFWorkbook(pageSize);
            ISheet sheet = wb.CreateSheet(filename);
            //设置标题
            SetColumnTitle<T>(sheet, new T());

            var type = typeof(T);
            int pageIndex = 1;
            Boolean hasNext = true;

            //记录循环次数
            while (hasNext)
            {
                var datas = action.Invoke(pageIndex, pageSize);
                SetRowContent<T>(type, sheet, pageIndex, pageSize, datas);

                //不包含任何数据的时候，就退出
                if (!datas.Any())
                {
                    break;
                }
                //说明已经到了最后一页。
                if (datas.Count() < pageSize)
                {
                    hasNext = false;
                }
                pageIndex++;
            }
            byte[] buffer;
            using (NpoiMemoryStream ms = new NpoiMemoryStream())
            {
                ms.AllowClose = false;
                wb.Write(ms);
                ms.Flush();
                buffer = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(buffer, 0, buffer.Length);
                ms.AllowClose = true;
            }
            wb.Close();
            return buffer;

        }




        public static void SetRowContent<T>(Type type, ISheet sheet, int pageIndex, int pageSize, List<T> list)
        {
           int rowIndex = (pageIndex - 1) * pageSize + 1;
            foreach (var model in list)
            {
                var properties = type.GetProperties();

                IRow row = CreateRow(sheet, rowIndex);// sheet.CreateRow(start);
                int j = 0;
                for (int i = 0; i < properties.Length; i++)
                {
                    var p = properties[i];
                    object obj = p.GetValue(model, null);
                    if (obj != null)
                    {
                        row.CreateCell(i).SetCellValue(obj.ToString());
                    }
                }
                rowIndex++;
            }
        }

     






        /// <summary>
        /// 导出Excel分页+多线程(SXSSFWorkbook)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="action">分页函数的委托</param>
        /// <returns></returns>
        public static byte[] ExportStreamByMultiSheet<T>(string filename,int recordCount, int pageSize, Func<int, int, List<T>> action) where T : new()
        {
            IWorkbook wb = new SXSSFWorkbook(pageSize);
            var type = typeof(T);

            //开启多线程(开启固定线程)方法
            var excelTasks=new List<Task>();
            int fixThreadCount = 10;  //开启10个固定数量
            for(int i=1;i< (recordCount / pageSize)+1; i++)
            {
                ISheet sheet = wb.CreateSheet(filename+i);
                SetColumnTitle<T>(sheet, new T());
                excelTasks.Add(
                    Task.Factory.StartNew(()=>
                         SetExcel(pageSize, action, sheet, type, i)
                    )
                );
                if (excelTasks.Count >= fixThreadCount)
                {
                    Task.WaitAny(excelTasks.ToArray()); //等待任何一个完成
                    excelTasks = excelTasks.Where(d => d.Status != TaskStatus.RanToCompletion).ToList();
                }
            }
            Task.WaitAll(excelTasks.ToArray());
            byte[] buffer;
            using (NpoiMemoryStream ms = new NpoiMemoryStream())
            {
                ms.AllowClose = false;
                wb.Write(ms);
                ms.Flush();
                buffer = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(buffer, 0, buffer.Length);
                ms.AllowClose = true;
            }
            wb.Close();
            return buffer;

        }

        private static void SetExcel<T>(int pageSize, Func<int, int, List<T>> action, ISheet sheet, Type type, int pageIndex) where T : new()
        {
            var datas = action.Invoke(pageIndex, pageSize);
            SetSheetRowContent<T>(type, sheet, pageIndex, pageSize, datas);
        }


        public static IRow CreateRow(ISheet sheet,int num)
        {
            return sheet.CreateRow(num);
        }


        public static void SetSheetRowContent<T>(Type type, ISheet sheet, int pageIndex, int pageSize, List<T> list)
        {
            int rowIndex = 1;
            foreach (var model in list)
            {
                var properties = type.GetProperties();

                IRow row = CreateRow(sheet, rowIndex);// sheet.CreateRow(start);
                int j = 0;
                for (int i = 0; i < properties.Length; i++)
                {
                    var p = properties[i];
                    object obj = p.GetValue(model, null);
                    if (obj != null)
                    {
                        row.CreateCell(i).SetCellValue(obj.ToString());
                    }
                }
                rowIndex++;
            }
        }










    }
}
