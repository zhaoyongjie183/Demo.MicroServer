using Demo.MicroService.Core.Model;
using Demo.MicroService.Repository.IRepository;
using SqlSugar;
using System.Data;
using System.Dynamic;
using System.Linq.Expressions;
using Demo.MicroService.Core.Utils;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/8 10:39:06                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Repository.Repository                              
*│　类    名： BaseRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Repository.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        public SqlSugarClient _db;
        //public BaseRepository(ISqlSugarClient context)//注意这里要有默认值等于null
        //{
        //    var configId = typeof(T).GetCustomAttribute<TenantAttribute>().configId;
        //    //根据特性指定具体使用哪个库
        //    base.Context = DbScoped.SugarScope.GetConnection(configId);
        //    itenant = DbScoped.SugarScope;
        //}
        public BaseRepository(SqlSugarClient dbContext)
        {
            _db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            //_db = new SqlSugarClient(new ConnectionConfig()
            //{
            //    ConnectionString = Config.ConnectionString,
            //    DbType = DbType.SqlServer,
            //    IsAutoCloseConnection = true,
            //    InitKeyType = InitKeyType.Attribute,
            //    AopEvents = new AopEvents()
            //    {
            //        OnLogExecuting = (sql, p) =>
            //        {
            //            Console.WriteLine(sql);
            //        }
            //    }
            //});
        }

        //public virtual SqlSugarClient GetSqlSugarClient(string ConnectionString)
        //{
        //    return new SqlSugarClient(new ConnectionConfig()
        //    {
        //        ConnectionString = ConnectionString,
        //        DbType = SqlSugar.DbType.SqlServer,
        //        IsAutoCloseConnection = true,
        //        InitKeyType = InitKeyType.Attribute,
        //    });
        //}

        #region add

        public bool Insert(T t, bool IgnoreNullColumn = true)
        {
            return _db.Insertable(t).IgnoreColumns(IgnoreNullColumn).ExecuteCommand() > 0;
        }
        public async Task<bool> InsertAsync(T t, bool IgnoreNullColumn = true)
        {
            return await _db.Insertable(t).IgnoreColumns(IgnoreNullColumn).ExecuteCommandAsync() > 0;
        }
        public bool InsertIgnoreNullColumn(T t)
        {
            return _db.Insertable(t).IgnoreColumns(true).ExecuteCommand() > 0;
        }

        public async Task<bool> InsertIgnoreNullColumnAsync(T t, params string[] columns)
        {
            return await _db.Insertable(t).IgnoreColumns(columns).ExecuteCommandAsync() > 0;
        }

        public bool InsertIgnoreNullColumn(T t, params string[] columns)
        {
            return _db.Insertable(t).IgnoreColumns(columns).ExecuteCommand() > 0;
        }

        public bool Insert(SqlSugarClient client, T t)
        {
            return client.Insertable(t).ExecuteCommand() > 0;
        }

        public long InsertBigIdentity(T t)
        {
            return _db.Insertable(t).ExecuteReturnBigIdentity();
        }

        public bool Insert(List<T> t)
        {
            return _db.Insertable(t).ExecuteCommand() > 0;
        }

        public bool InsertIgnoreNullColumn(List<T> t)
        {
            return _db.Insertable(t).IgnoreColumns(true).ExecuteCommand() > 0;
        }

        public bool InsertIgnoreNullColumn(List<T> t, params string[] columns)
        {
            return _db.Insertable(t).IgnoreColumns(columns).ExecuteCommand() > 0;
        }

        public DbResult<bool> InsertTran(T t)
        {
            var result = _db.Ado.UseTran(() =>
            {
                _db.Insertable(t).ExecuteCommand();
            });
            return result;
        }

        public DbResult<bool> InsertTran(List<T> t)
        {
            var result = _db.Ado.UseTran(() =>
            {
                _db.Insertable(t).ExecuteCommand();
            });
            return result;
        }

        public T InsertReturnEntity(T t)
        {
            return _db.Insertable(t).ExecuteReturnEntity();
        }

        //public async Task<TKey> InsertEntityReturnTKeyAsync(T t)
        //{
        //    var entity=await _db.Insertable(t).ExecuteReturnEntityAsync();
        //    return (TKey)entity
        //}

        public async Task<T> InsertReturnEntityAsync(T t)
        {
            return await _db.Insertable(t).ExecuteReturnEntityAsync();
        }

        public T InsertReturnEntity(T t, string sqlWith = SqlWith.UpdLock)
        {
            return _db.Insertable(t).With(sqlWith).ExecuteReturnEntity();
        }

        public bool ExecuteCommand(string sql, object parameters)
        {
            return _db.Ado.ExecuteCommand(sql, parameters) > 0;
        }

        public bool ExecuteCommand(string sql, params SugarParameter[] parameters)
        {
            return _db.Ado.ExecuteCommand(sql, parameters) > 0;
        }

        public bool ExecuteCommand(string sql, List<SugarParameter> parameters)
        {
            return _db.Ado.ExecuteCommand(sql, parameters) > 0;
        }

        #endregion add

        #region update

        public bool UpdateEntity(T entity)
        {
            return _db.Updateable(entity).ExecuteCommand() > 0;
        }

        public bool Update(T entity, Expression<Func<T, bool>> expression)
        {
            return _db.Updateable(entity).Where(expression).ExecuteCommand() > 0;
        }

        public bool Update(T entity, Expression<Func<T, object>> expression)
        {
            return _db.Updateable(entity).UpdateColumns(expression).ExecuteCommand() > 0;
        }

        public bool Update(T entity, Expression<Func<T, object>> expression, Expression<Func<T, bool>> where)
        {
            return _db.Updateable(entity).UpdateColumns(expression).Where(where).ExecuteCommand() > 0;
        }

        public bool Update(SqlSugarClient client, T entity, Expression<Func<T, object>> expression, Expression<Func<T, bool>> where)
        {
            return client.Updateable(entity).UpdateColumns(expression).Where(where).ExecuteCommand() > 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <param name="isNull">默认为true</param>
        /// <returns></returns>
        public bool Update(T entity, List<string> list = null, bool isNull = true)
        {
            if (list.IsNullT())
            {
                list = new List<string>()
            {
                "CreateBy",
                "CreateDate"
            };
            }
            //_db.Updateable(entity).IgnoreColumns(c => list.Contains(c)).Where(isNull).ExecuteCommand()
            return _db.Updateable(entity).IgnoreColumns(isNull).IgnoreColumns(list.ToArray()).ExecuteCommand() > 0;
        }

        public bool Update(List<T> entity)
        {
            var result = _db.Ado.UseTran(() =>
            {
                _db.Updateable(entity).ExecuteCommand();
            });
            return result.IsSuccess;
        }

        #endregion update

        #region Tran
        public DbResult<bool> UseTran(Action action)
        {
            var result = _db.Ado.UseTran(() => action());
            return result;
        }

        public DbResult<bool> UseTran(SqlSugarClient client, Action action)
        {
            var result = client.Ado.UseTran(() => action());
            return result;
        }

        public bool UseTran2(Action action)
        {
            var result = _db.Ado.UseTran(() => action());
            return result.IsSuccess;
        }
        #endregion

        #region delete

        public bool Delete(Expression<Func<T, bool>> expression)
        {
            return _db.Deleteable<T>().Where(expression).ExecuteCommand() > 0;
        }

        public bool Delete<PkType>(PkType[] primaryKeyValues)
        {
            return _db.Deleteable<T>().In(primaryKeyValues).ExecuteCommand() > 0;
        }

        public bool Delete(object obj)
        {
            return _db.Deleteable<T>().In(obj).ExecuteCommand() > 0;
        }

        #endregion delete

        #region query

        public bool IsAny(Expression<Func<T, bool>> expression)
        {
            //_db.Queryable<T>().Any();
            return _db.Queryable<T>().Where(expression).Any();
        }

        public ISugarQueryable<T> Queryable()
        {
            return _db.Queryable<T>();
        }

        public ISugarQueryable<ExpandoObject> Queryable(string tableName, string shortName)
        {
            return _db.Queryable(tableName, shortName);
        }

        public List<T> QueryableToList(Expression<Func<T, bool>> expression)
        {
            return _db.Queryable<T>().Where(expression).ToList();
        }

        public Task<List<T>> QueryableToListAsync(Expression<Func<T, bool>> expression)
        {
            return _db.Queryable<T>().Where(expression).ToListAsync();
        }

        public string QueryableToJson(string select, Expression<Func<T, bool>> expressionWhere)
        {
            var query = _db.Queryable<T>().Select(select).Where(expressionWhere).ToList();
            return query.ToJson();
        }

        public T QueryableToEntity(Expression<Func<T, bool>> expression)
        {
            return _db.Queryable<T>().Where(expression).First();
        }

        public List<T> QueryableToList(string tableName)
        {
            return _db.Queryable<T>(tableName).ToList();
        }

        public List<T> QueryableToList(string tableName, Expression<Func<T, bool>> expression)
        {
            return _db.Queryable<T>(tableName).Where(expression).ToList();
        }

        public (List<T>, int) QueryableToPage(Expression<Func<T, bool>> expression, int pageIndex = 0, int pageSize = 10)
        {
            int totalNumber = 0;
            var list = _db.Queryable<T>().Where(expression).ToPageList(pageIndex, pageSize, ref totalNumber);
            return (list, totalNumber);
        }

        public (List<T>, int) QueryableToPage(Expression<Func<T, bool>> expression, string order, int pageIndex = 0, int pageSize = 10)
        {
            int totalNumber = 0;
            var list = _db.Queryable<T>().Where(expression).OrderBy(order).ToPageList(pageIndex, pageSize, ref totalNumber);
            return (list, totalNumber);
        }

        public (List<T>, int) QueryableToPage(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderFiled, string orderBy, int pageIndex = 0, int pageSize = 10)
        {
            int totalNumber = 0;

            if (orderBy.Equals("DESC", StringComparison.OrdinalIgnoreCase))
            {
                var list = _db.Queryable<T>().Where(expression).OrderBy(orderFiled, OrderByType.Desc).ToPageList(pageIndex, pageSize, ref totalNumber);
                return (list, totalNumber);
            }
            else
            {
                var list = _db.Queryable<T>().Where(expression).OrderBy(orderFiled, OrderByType.Asc).ToPageList(pageIndex, pageSize, ref totalNumber);
                return (list, totalNumber);
            }
        }

        public (List<T>, int) QueryableToPage(Expression<Func<T, bool>> expression, PageParams pageparams)
        {
            int totalNumber = 0;
            if (pageparams.offset != 0)
            {
                pageparams.offset = pageparams.offset / pageparams.limit + 1;
            }
            if (pageparams.order.Equals("DESC", StringComparison.OrdinalIgnoreCase))
            {
                var list = _db.Queryable<T>().Where(expression).OrderBy(pageparams.sort).ToPageList(pageparams.offset, pageparams.limit, ref totalNumber);
                return (list, totalNumber);
            }
            else
            {
                var list = _db.Queryable<T>().Where(expression).OrderBy(pageparams.sort).ToPageList(pageparams.offset, pageparams.limit, ref totalNumber);
                return (list, totalNumber);
            }
        }

        public List<T> SqlQueryToList(string sql, object obj = null)
        {
            return _db.Ado.SqlQuery<T>(sql, obj);
        }

        #endregion query

        #region Procedure
        /// <summary>
        /// 此方法不带output返回值
        /// var list = new List<SugarParameter>();
        /// list.Add(new SugarParameter(ParaName, ParaValue)); input
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable UseStoredProcedureToDataTable(string procedureName, List<SugarParameter> parameters)
        {
            return _db.Ado.UseStoredProcedure().GetDataTable(procedureName, parameters);
        }

        /// <summary>
        /// 带output返回值
        /// var list = new List<SugarParameter>();
        /// list.Add(new SugarParameter(ParaName, ParaValue, true));  output
        /// list.Add(new SugarParameter(ParaName, ParaValue)); input
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public (DataTable, List<SugarParameter>) UseStoredProcedureToTuple(string procedureName, List<SugarParameter> parameters)
        {
            var result = (_db.Ado.UseStoredProcedure().GetDataTable(procedureName, parameters), parameters);
            return result;
        }

        #endregion 

        #region Async Method
        public virtual Task<T> GetByIdAsync(dynamic id)
        {
            return _db.Queryable<T>().InSingleAsync(id);
        }
        public virtual Task<List<T>> GetListAsync()
        {
            return _db.Queryable<T>().ToListAsync();
        }

        public virtual Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression)
        {
            return _db.Queryable<T>().Where(whereExpression).ToListAsync();
        }
        public virtual Task<T> GetSingleAsync(Expression<Func<T, bool>> whereExpression)
        {
            return _db.Queryable<T>().SingleAsync(whereExpression);
        }
        public virtual Task<T> GetFirstAsync(Expression<Func<T, bool>> whereExpression)
        {
            return _db.Queryable<T>().FirstAsync(whereExpression);
        }
        public virtual async Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, PageModel page)
        {
            RefAsync<int> count = 0;
            var result = await _db.Queryable<T>().Where(whereExpression).ToPageListAsync(page.PageIndex, page.PageSize, count);
            page.TotalCount = count;
            return result;
        }
        public virtual async Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
        {
            RefAsync<int> count = 0;
            var result = await _db.Queryable<T>().OrderByIF(orderByExpression != null, orderByExpression, orderByType).Where(whereExpression).ToPageListAsync(page.PageIndex, page.PageSize, count);
            page.TotalCount = count;
            return result;
        }
        public virtual async Task<List<T>> GetPageListAsync(List<IConditionalModel> conditionalList, PageModel page)
        {
            RefAsync<int> count = 0;
            var result = await _db.Queryable<T>().Where(conditionalList).ToPageListAsync(page.PageIndex, page.PageSize, count);
            page.TotalCount = count;
            return result;
        }
        public virtual async Task<List<T>> GetPageListAsync(List<IConditionalModel> conditionalList, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
        {
            RefAsync<int> count = 0;
            var result = await _db.Queryable<T>().OrderByIF(orderByExpression != null, orderByExpression, orderByType).Where(conditionalList).ToPageListAsync(page.PageIndex, page.PageSize, count);
            page.TotalCount = count;
            return result;
        }
        public virtual Task<bool> IsAnyAsync(Expression<Func<T, bool>> whereExpression)
        {
            return _db.Queryable<T>().Where(whereExpression).AnyAsync();
        }
        public virtual Task<int> CountAsync(Expression<Func<T, bool>> whereExpression)
        {

            return _db.Queryable<T>().Where(whereExpression).CountAsync();
        }

        public virtual async Task<bool> InsertOrUpdateAsync(T data)
        {
            return await this._db.Storageable(data).ExecuteCommandAsync() > 0;
        }
        public virtual async Task<bool> InsertOrUpdateAsync(List<T> datas)
        {
            return await this._db.Storageable(datas).ExecuteCommandAsync() > 0;
        }
        public virtual async Task<bool> InsertAsync(T insertObj)
        {
            return await this._db.Insertable(insertObj).ExecuteCommandAsync() > 0;
        }
        public virtual Task<int> InsertReturnIdentityAsync(T insertObj)
        {
            return this._db.Insertable(insertObj).ExecuteReturnIdentityAsync();
        }
        public virtual Task<long> InsertReturnBigIdentityAsync(T insertObj)
        {
            return this._db.Insertable(insertObj).ExecuteReturnBigIdentityAsync();
        }
     
        public virtual async Task<bool> InsertRangeAsync(T[] insertObjs)
        {
            return await this._db.Insertable(insertObjs).ExecuteCommandAsync() > 0;
        }
        public virtual async Task<bool> InsertRangeAsync(List<T> insertObjs)
        {
            return await this._db.Insertable(insertObjs).ExecuteCommandAsync() > 0;
        }
        public virtual async Task<bool> UpdateAsync(T updateObj)
        {
            return await this._db.Updateable(updateObj).ExecuteCommandAsync() > 0;
        }
        public virtual async Task<bool> UpdateRangeAsync(T[] updateObjs)
        {
            return await this._db.Updateable(updateObjs).ExecuteCommandAsync() > 0;
        }
        public virtual async Task<bool> UpdateRangeAsync(List<T> updateObjs)
        {
            return await this._db.Updateable(updateObjs).ExecuteCommandAsync() > 0;
        }
        public virtual async Task<bool> UpdateAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression)
        {
            return await this._db.Updateable<T>().SetColumns(columns).Where(whereExpression).ExecuteCommandAsync() > 0;
        }
        public virtual async Task<bool> UpdateSetColumnsTrueAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression)
        {
            return await this._db.Updateable<T>().SetColumns(columns, true).Where(whereExpression).ExecuteCommandAsync() > 0;
        }
        public virtual async Task<bool> DeleteAsync(T deleteObj)
        {
            return await this._db.Deleteable<T>().Where(deleteObj).ExecuteCommandAsync() > 0;
        }
        public virtual async Task<bool> DeleteAsync(List<T> deleteObjs)
        {
            return await this._db.Deleteable<T>().Where(deleteObjs).ExecuteCommandAsync() > 0;
        }
        public virtual async Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await this._db.Deleteable<T>().Where(whereExpression).ExecuteCommandAsync() > 0;
        }
        public virtual async Task<bool> DeleteByIdAsync(dynamic id)
        {
            return await this._db.Deleteable<T>().In(id).ExecuteCommandAsync() > 0;
        }
        public virtual async Task<bool> DeleteByIdsAsync(dynamic[] ids)
        {
            return await this._db.Deleteable<T>().In(ids).ExecuteCommandAsync() > 0;
        }

        public virtual Task<long> InsertReturnSnowflakeIdAsync(T insertObj)
        {
            return this._db.Insertable(insertObj).ExecuteReturnSnowflakeIdAsync();
        }
        public virtual Task<List<long>> InsertReturnSnowflakeIdAsync(List<T> insertObjs)
        {
            return this._db.Insertable(insertObjs).ExecuteReturnSnowflakeIdListAsync();
        }
        #endregion
    }
}
