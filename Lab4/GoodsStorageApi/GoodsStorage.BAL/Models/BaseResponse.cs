using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.BAL.Models
{
    public enum Status
    {
        Ok = 0,
        Error = 1
    }
    public class BaseResponse<T>
    {
        public string Description { get; set; }
        public Status Status { get; set; }
        public T Data { get; set; }
        
        public static BaseResponse<T> ErrorResponse(string message)
        {
            return new BaseResponse<T>()
            {
                Description = message,
                Status = Status.Error
            };
        }
        public static BaseResponse<T> OkResponse(T data)
        {
            return new BaseResponse<T>()
            {
                Data = data,
                Status = Status.Ok
            };
        }
    }
}
