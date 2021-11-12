using ExcelWebDemo.Common.Excel;
using ExcelWebDemo.Model.ExcelExportModel;
using ExcelWebDemo.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExcelWebDemo.Controller
{
    [Route("api/excel")]
    [ApiController]
    public class ExcelController : ControllerBase
    {

     
        [HttpGet("xlsx")]
        public byte[] ExportItsmXlsx()
        {
            try
            {

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                var itsmInfo = new ItsmProvider().GetAllItsms();
                stopwatch.Stop();
                Console.WriteLine("获取数据耗时" + stopwatch.ElapsedMilliseconds);
                stopwatch.Restart();
                var byteArr = ExcelHelper.Export<MAIN_ITSM_MODEL>(itsmInfo, "test");
                stopwatch.Stop();
                Console.WriteLine("生成excel数据耗时" + stopwatch.ElapsedMilliseconds);
                return byteArr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpGet("sxlsx")]
        public byte[] ExportItsmSXlsx()
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                var itsmInfo = new ItsmProvider().GetAllItsms();
                stopwatch.Stop();
                Console.WriteLine("获取数据耗时" + stopwatch.ElapsedMilliseconds);
                stopwatch.Restart();
                var byteArr = ExcelHelper.ExportStream<MAIN_ITSM_MODEL>(itsmInfo, "test");
                stopwatch.Stop();
                Console.WriteLine("生成excel数据耗时" + stopwatch.ElapsedMilliseconds);
                return byteArr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet("sxlsxpage")]
        public byte[] ExportItsmSXLSXPage()
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                var pageFunc = new Func<int, int, List<MAIN_ITSM_MODEL>>(new ItsmProvider().GetItsms);

                var byteArr = ExcelHelper.ExportStreamByPage<MAIN_ITSM_MODEL>("test",50000, pageFunc);
                stopwatch.Stop();
                Console.WriteLine("生成excel分页数据耗时" + stopwatch.ElapsedMilliseconds);
                return byteArr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet("sxlsxmultipage")]
        public byte[] ExportItsmSXLSXMultiPage()
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                int recordCount=new ItsmProvider().GetRecordCount();
                var pageFunc = new Func<int, int, List<MAIN_ITSM_MODEL>>(new ItsmProvider().GetItsms);

                var byteArr = ExcelHelper.ExportStreamByMultiSheet<MAIN_ITSM_MODEL>("test",recordCount, 50000, pageFunc);
                stopwatch.Stop();
                Console.WriteLine("生成excel多线程分页数据耗时" + stopwatch.ElapsedMilliseconds);
                return byteArr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





    }
}
