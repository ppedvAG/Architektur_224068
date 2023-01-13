﻿using Carshop9000.Model.DomainModel;

namespace Carshop9000.Model.Contracts
{
    public interface IRepository
    {
        T GetById<T>(int id) where T : Entity;

        IQueryable<T> Query<T>() where T : Entity;

        void Add<T>(T entity) where T : Entity;
        void Update<T>(T entity) where T : Entity;
        void Delete<T>(T entity) where T : Entity;



        void SaveAll();
    }
}
