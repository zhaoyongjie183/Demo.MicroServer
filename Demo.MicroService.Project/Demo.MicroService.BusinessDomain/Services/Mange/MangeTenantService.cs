using Demo.MicroService.BusinessDomain.IServices.IMange;
using Demo.MicroService.BusinessModel.Model.Mange;
using Demo.MicroService.Core.Model;
using Demo.MicroService.Core.Utils;
using Demo.MicroService.Repository.IRepository.IMangeRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/13 13:38:25                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.BusinessDomain.Services.Mange                              
*│　类    名： MangeTenantService                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.BusinessDomain.Services.Mange
{
    public class MangeTenantService : IMangeTenantService
    {
        private IMangeTenantRepository _mangeTenantRepository;
        public MangeTenantService(IMangeTenantRepository mangeTenantRepository) 
        { 
            this._mangeTenantRepository = mangeTenantRepository;
        }

        /// <summary>
        /// 新增客户信息
        /// </summary>
        /// <param name="mangeTenant"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseResult> AddCustomer(MangeTenant mangeTenant)
        {
            mangeTenant.MTenantID= Guid.NewGuid();
            mangeTenant.CreatedUser=Guid.NewGuid();
            mangeTenant.CreatedTime= DateTime.Now;
            var result=await this._mangeTenantRepository.InsertIgnoreNullColumnAsync(mangeTenant, nameof(mangeTenant.Id));
            return new ResponseResult() { IsSuccess = result, Message = result ? "新增成功" : "新增失败" };
        }

        /// <summary>
        /// 查询租户信息
        /// </summary>
        /// <param name="mangeTenant"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseResult> QueryTenantId(string TenantCode)
        {
            var old = await this._mangeTenantRepository.Queryable().FirstAsync(x => x.TenantCode == TenantCode);
            return old.IsNullT() ? new ResponseResult() { IsSuccess = false } : new ResponseResult<Guid>() { IsSuccess = true, DataResult = old.MTenantID };

        }

        /// <summary>
        /// 更新客户
        /// </summary>
        /// <param name="mangeTenant"></param>
        /// <returns></returns>
        public async Task<ResponseResult> UpdateCustomer(MangeTenant mangeTenant)
        {
            var old=await this._mangeTenantRepository.Queryable().FirstAsync(x => x.TenantCode == mangeTenant.TenantCode);
            if (old.IsNullT())
                return new ResponseResult() { IsSuccess = false, Message = "客户不存在" };
            mangeTenant.Id = old.Id;
            mangeTenant.CreatedUser=old.CreatedUser;
            mangeTenant.CreatedTime =old.CreatedTime;
            mangeTenant.MTenantID = old.MTenantID;
            mangeTenant.UpdatedUser= Guid.NewGuid();
            mangeTenant.UpdatedTime= DateTime.Now;
            var result = await this._mangeTenantRepository.UpdateAsync(mangeTenant);
            return new ResponseResult() { IsSuccess = result, Message = result ? "新增成功" : "新增失败" };
        }

        /// <summary>
        /// 获取租户链接
        /// </summary>
        /// <param name="TenantCode"></param>
        /// <returns></returns>
        public async Task<ResponseResult> QueryTenantConneString(Guid tenantId)
        {
            var old = await this._mangeTenantRepository.Queryable().FirstAsync(x => x.MTenantID == tenantId);
            if (old.IsNullT())
                return new ResponseResult() { IsSuccess = false };
            else
            {
                SqlConneModel sqlConneModel = new SqlConneModel();
                var dbType = old.TenantDBType.ToEnum<DbType>();
                switch (dbType)
                {
                    case DbType.MySql:
                        sqlConneModel.DbType = DbType.MySql;
                        sqlConneModel.SqlConnectionString = $"server={old.DBIP};database={old.DBName};port={old.DBPort};uid={old.DBUser};pwd={old.DBPwd};charset='utf8'";
                        break;

                    case DbType.SqlServer:
                        sqlConneModel.DbType = DbType.SqlServer;
                        sqlConneModel.SqlConnectionString = $"server={old.DBIP};user id={old.DBUser};password={old.DBPwd};database={old.DBName}";
                        break;

                    case DbType.Sqlite:
                        sqlConneModel.DbType = DbType.Sqlite;
                        sqlConneModel.SqlConnectionString = $"Data Source={old.DBName};Version=3;Password={old.DBPwd}";
                        break;

                    case DbType.Oracle:
                        sqlConneModel.DbType = DbType.Oracle;
                        sqlConneModel.SqlConnectionString = $"User ID={old.DBUser};Password={old.DBPwd};Data Source=(DESCRIPTION = (ADDRESS_LIST=(ADDRESS=(PROTOCOL = TCP)(HOST = {old.DBIP})(PORT = {old.DBPort})))(CONNECT_DATA = (SERVICE_NAME = {old.DBName})))";
                        break;

                    case DbType.PostgreSQL:
                        sqlConneModel.DbType = DbType.PostgreSQL;
                        sqlConneModel.SqlConnectionString = $"Host={old.DBIP};Port={old.DBPort};Username={old.DBUser};Password={old.DBPwd};Database={old.DBName}";
                        break;

                    default:
                        sqlConneModel.DbType = DbType.MySql;
                        sqlConneModel.SqlConnectionString = $"server={old.DBIP};database={old.DBName};port={old.DBPort};uid={old.DBUser};pwd={old.DBPwd};charset='utf8'";
                        break;
                }
                return  new ResponseResult<SqlConneModel>() { IsSuccess = true, DataResult = sqlConneModel };
            }

        }
    }
}
