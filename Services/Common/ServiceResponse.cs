using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common
{
    public class ServiceResponse<T>
    {
        public ServiceResponse(T response)
        {
            Response = response;
        }

        public ServiceResponse(string error)
        {
            Error = error;
        }
        public T Response { get; }
        public string Error { get; }
    }
}
