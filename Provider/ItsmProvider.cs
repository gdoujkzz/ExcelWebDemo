using ExcelWebDemo.Common;
using ExcelWebDemo.Entity;
using ExcelWebDemo.Model.ExcelExportModel;
using SqlSugar;

namespace ExcelWebDemo.Provider
{
    public class ItsmProvider
    {
        public List<MAIN_ITSM_MODEL> GetAllItsms()
        {
            using (var db = DbUtil.GetInstance())
            {

                string sql = @"SELECT * FROM itsm_main_info  tt
            WHERE tt.CreateTime > '2021-03-31 15:25:21'";
    
                var result = db.Ado.SqlQuery<MAIN_ITSM_MODEL>(sql)
                    .ToList();
                return result;
            }
        }


        public  List<MAIN_ITSM_MODEL> GetItsms(int pageIndex, int pageSize)
        {
            using (var db = DbUtil.GetInstance())
            {

                string sql = @"SELECT * FROM itsm_main_info a JOIN (
SELECT tt.id FROM itsm_main_info tt
WHERE tt.CreateTime >'2021-03-31 15:25:21'
LIMIT @offset,@pagesize
) b ON a.ID = b.id";
                var parameters = new List<SugarParameter>();
                parameters.Add(new SugarParameter("pagesize", pageSize));
                parameters.Add(new SugarParameter("offset", (pageIndex - 1) * pageSize));

                var result = db.Ado.SqlQuery<MAIN_ITSM_MODEL>(sql, parameters)
                    .ToList();

                return result;
            }
        }


        public int GetRecordCount()
        {
            using (var db = DbUtil.GetInstance())
            {

                string sql = @"SELECT COUNT(1) FROM itsm_main_info  tt
WHERE tt.CreateTime > '2021-03-31 15:25:21'";

                var count = db.Ado.SqlQuerySingle<int>(sql)
                    ;
                return count;
            }
        }








    }
}
