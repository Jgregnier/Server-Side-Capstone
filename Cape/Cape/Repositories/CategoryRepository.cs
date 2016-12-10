﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cape.Data;
using Cape.Interfaces;
using Cape.Models;
using Microsoft.AspNet.Identity.EntityFramework;

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
            Category selectedCategory = context.Category.Where(category => category.CategoryId == categoryId).Single();

            return selectedCategory;
        }

        public void Create(Category obj)
        {
            context.Entry(obj);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChangesAsync();
        }
    }
}
