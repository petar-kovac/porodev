﻿namespace PoroDev.Common.Models.UnitOfWorkResponse
{
    public class UnitOfWorkResponseModel<T> where T : class, new()
    {
        public T? Entity { get; set; }

        public string? ExceptionName { get; set; }

        public string? HumanReadableMessage { get; set; }

        public UnitOfWorkResponseModel()
        {

        }

        public UnitOfWorkResponseModel(T entity)
        {
            Entity = entity;
            ExceptionName = null;
            HumanReadableMessage = null;
        }
    }
}