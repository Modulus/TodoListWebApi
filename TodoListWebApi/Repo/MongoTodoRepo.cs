﻿using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListWebApi.Mappers;
using TodoListWebApi.Models;

namespace TodoListWebApi.Repo
{
    public class MongoTodoRepo : ITodoRepo
    {
        private readonly IMongoClient _mongoClient;
        private readonly string _databaseName;
        private readonly string _collectionName;

        public MongoTodoRepo(IMongoClient mongoClient, string databaseName = "todoBase", string collectionName = "todos")
        {
           _mongoClient = mongoClient;
           _databaseName = databaseName;
            _collectionName = collectionName;
        }

        public async Task Delete(string todoId)
        {
            var collection = GetCollection();

            await collection.FindOneAndDeleteAsync(todo => todo.Id == todoId);
        }

        public async Task<bool> Exists(string todoId)
        {
            var collection = GetCollection();

            var existingTodo = await collection.Find(todo => todo.Id == todoId).FirstOrDefaultAsync();

            return existingTodo != null;
        }

        public async Task<TodoItem> FindById(string todoId)
        {
            var collection = GetCollection();

            var todo = await collection.Find(item => item.Id == todoId).FirstOrDefaultAsync();

            return TodoMapper.Map(todo);
        }

        public async Task<IList<TodoItem>> GetTodos()
        {
            var collection = GetCollection();

            var todos = await collection.Find<MongoTodoItem>(_ => true).ToListAsync();

            return todos.Select(t => TodoMapper.Map(t)).ToList();
        }

        public async Task<TodoItem> MarkAsDone(string todoId)
        {
            var todo = await UpdateStatus(GetCollection(), todoId, true);

            return TodoMapper.Map(todo);
        }

        public async Task<TodoItem> MaskAsNotDone(string todoId)
        {
            var todo = await UpdateStatus(GetCollection(), todoId, false);

            return TodoMapper.Map(todo);
        }

        private async Task<MongoTodoItem> UpdateStatus(IMongoCollection<MongoTodoItem> collection, string id, bool status)
        {
            var todo = await collection.FindOneAndUpdateAsync(t => t.Id == id, Builders<MongoTodoItem>.Update.Set(t => t.Done, status));

            return todo;
        }

        private IMongoCollection<MongoTodoItem> GetCollection()
        {
            var db = _mongoClient.GetDatabase(_databaseName, null);

            return db.GetCollection<MongoTodoItem>(_collectionName, null);
        }
    }
}