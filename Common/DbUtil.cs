using SqlSugar;

namespace ExcelWebDemo.Common
{
    public class DbUtil
    {
        public static SqlSugarClient GetInstance()
        {
            //创建数据库对象
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "server=127.0.0.1;database=mytestdb;USER ID=root;PASSWORD=123456",//连接符字串
                DbType = DbType.MySql,
                IsAutoCloseConnection = true
            });

            //添加Sql打印事件，开发中可以删掉这个代码
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql);
            };
            return db;
        }
    }

}
