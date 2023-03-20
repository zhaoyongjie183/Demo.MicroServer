/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：自定义返回实体类                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/7 17:01:28                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Model                              
*│　类    名： ResponseResult                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Model
{
    public class ResponseResult
    {
        /// <summary>
        /// 状态码  无明确意义  前端用Result
        /// </summary>
        public int StatusCode { get; set; } = 200;

        /// <summary>
        ///     操作结果
        ///     true 成功
        ///     flase 失败
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 常规结果都在这里
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 特殊传值
        /// </summary>
        public object OtherValue { get; set; }
        /// <summary>
        /// 提示消息
        /// </summary>
        public string Message { get; set; }
       
    }

    public class ResponseResult<T> : ResponseResult
    {
        /// <summary>
        /// 泛型数据
        /// </summary>
        public T? DataResult { get; set; }
    }

    public class PageResult<T>: ResponseResult
    {
        /// <summary>
        /// 当前页标
        /// </summary>
        public int page { get; set; } = 1;
        /// <summary>
        /// 总页数
        /// </summary>
        public int pageCount => (int)Math.Ceiling((decimal)dataCount / PageSize);
        /// <summary>
        /// 数据总数
        /// </summary>
        public int dataCount { get; set; } = 0;
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { set; get; } = 20;
        /// <summary>
        /// 返回数据
        /// </summary>
        public List<T> DataResult { get; set; }

        public PageResult() { }

        public PageResult(int page, int dataCount, int pageSize, List<T> data)
        {
            this.page = page;
            this.dataCount = dataCount;
            PageSize = pageSize;
            this.DataResult = data;
        }

        public PageResult<TOut> ConvertTo<TOut>()
        {
            return new PageResult<TOut>(page, dataCount, PageSize, default);
        }
    }
}
