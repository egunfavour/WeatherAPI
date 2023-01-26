﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Weather_API.Core.DTO
{
    public class ResponseDTO<T>
    {
        public int StatusCode { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public static ResponseDTO<T> Fail(string errorMessage, int statusCode = (int)HttpStatusCode.NotFound)
        {
            return new ResponseDTO<T> { Status = false, Message = errorMessage, StatusCode = statusCode };
        }
        public static ResponseDTO<T> Success(string successMessage, T data, int statusCode = (int)HttpStatusCode.OK)
        {
            return new ResponseDTO<T> { Status = true, Message = successMessage, Data = data, StatusCode = statusCode };
        }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
