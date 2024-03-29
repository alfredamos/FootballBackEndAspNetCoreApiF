﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballBackEndAspNetCoreApiF.Contracts
{
    public interface IBaseRepository<T> where T: class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> AddEntity(T newEntity);
        Task<T> UpdateEntity(T updateEntity);
        Task<T> DeleteEntity(int id);
        Task<IEnumerable<T>> Search(string searchKey);
    }
}
