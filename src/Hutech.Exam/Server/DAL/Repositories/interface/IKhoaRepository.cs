﻿using System.Data;

namespace Hutech.Exam.Server.DAL.Repositories
{
    public interface IKhoaRepository
    {
        public Task<IDataReader> GetAll();
    }
}
