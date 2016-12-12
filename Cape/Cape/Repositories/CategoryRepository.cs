﻿using System.Linq;
using Cape.Data;
using Cape.Interfaces;
using Cape.Models;
using System.Data.Entity;

namespace Cape.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext context;

        public CategoryRepository(CategoryRepositoryConnection connection)
        {
            context = connection.AppContext;
        }

        public Category GetById(int categoryId)
        {
            Category selectedCategory = context.Category.Single(category => category.CategoryId == categoryId);

            return selectedCategory;
        }

        public void Create(Category obj)
        {
            context.Category.Add(obj);
            context.SaveChanges();
        }

        public void Update(Category obj)
        {
            context.Entry(obj);

            context.ChangeTracker.DetectChanges();

            context.SaveChanges();
        }
    }
}
