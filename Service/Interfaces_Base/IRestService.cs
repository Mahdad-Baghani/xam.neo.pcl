﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neo.pcl
{
    public interface IRestService
    {
        Task<TResult> GetAsync<TResult>(string uri);
        Task<TResult> PostAsync<TResult>(string uri, TResult data);
        Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest data);
        Task<TResult> PutAsync<TResult>(string uri, TResult data);
        Task<TResult> PutAsync<TRequest, TResult>(string uri, TRequest data);
    }
}
