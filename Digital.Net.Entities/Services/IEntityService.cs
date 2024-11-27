﻿using Digital.Net.Core.Messages;
using Digital.Net.Entities.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Digital.Net.Entities.Services;

public interface IEntityService<T, TQuery>
    where T : EntityBase
    where TQuery : Query
{
    /// <summary>
    ///     Get a schema of the entity describing its properties.
    /// </summary>
    /// <typeparam name="T">The model of the entity</typeparam>
    /// <returns>Schema of the entity</returns>
    List<SchemaProperty<T>> GetSchema();

    /// <summary>
    ///     Get entities based on a query. Converts the entities to the provided model.
    /// </summary>
    /// <param name="query">The query to filter entities</param>
    /// <typeparam name="TModel">The model to convert the entities to</typeparam>
    /// <returns>QueryResult of the model</returns>
    QueryResult<TModel> Get<TModel>(TQuery query) where TModel : class;

    /// <summary>
    ///    Get an entity based on its primary key. Converts the entity to the provided model.
    /// </summary>
    /// <param name="id">The entity primary key</param>
    /// <typeparam name="TModel">The model to convert the entities to</typeparam>
    /// <returns>Result of the model</returns>
    Result<TModel> Get<TModel>(Guid? id) where TModel : class;

    /// <summary>
    ///    Get an entity based on its primary key. Converts the entity to the provided model.
    /// </summary>
    /// <param name="id">The entity primary key</param>
    /// <typeparam name="TModel">The model to convert the entities to</typeparam>
    /// <returns>Result of the model</returns>
    Result<TModel> Get<TModel>(int id) where TModel : class;

    /// <summary>
    ///     Patch an entity based on its primary key.
    /// </summary>
    /// <param name="patch">The patch body</param>
    /// <param name="id">The entity primary key</param>
    /// <returns>Result of the model</returns>
    /// <exception cref="InvalidOperationException">
    ///      If the patch is invalid, throws an exceptions.
    /// </exception>
    Task<Result> Patch(JsonPatchDocument<T> patch, Guid? id);

    /// <summary>
    ///     Patch an entity based on its primary key.
    /// </summary>
    /// <param name="patch">The patch body</param>
    /// <param name="id">The entity primary key</param>
    /// <returns>Result of the model</returns>
    /// <exception cref="InvalidOperationException">
    ///      If the patch is invalid, throws an exceptions.
    /// </exception>
    Task<Result> Patch(JsonPatchDocument<T> patch, int id);

    /// <summary>
    ///     Create a new entity.
    /// </summary>
    /// <param name="entity">The entity to create</param>
    /// <returns>Result of the model</returns>
    /// <exception cref="InvalidOperationException">
    ///     If the payload is invalid, throws an exceptions.
    /// </exception>
    Task<Result> Create(T entity);

    /// <summary>
    ///     Delete an entity based on its primary key.
    /// </summary>
    /// <param name="id">The entity primary key</param>
    /// <returns>Result of the model</returns>
    /// <exception cref="InvalidOperationException">
    ///     If the entity is not found, throws an exceptions.
    /// </exception>
    Task<Result> Delete(Guid? id);

    /// <summary>
    ///     Delete an entity based on its primary key.
    /// </summary>
    /// <param name="id">The entity primary key</param>
    /// <returns>Result of the model</returns>
    /// <exception cref="InvalidOperationException">
    ///     If the entity is not found, throws an exceptions.
    /// </exception>
    Task<Result> Delete(int id);
}
