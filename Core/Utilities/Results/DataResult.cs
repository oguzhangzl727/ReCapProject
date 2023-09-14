using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T Data, bool Success, string Message) : base(Success, Message)
        {
            this.Data = Data;
        }
        public DataResult(T Data, bool Success) : base(Success)
        {
            this.Data = Data;
        }
        public T Data { get; }
    }
}
