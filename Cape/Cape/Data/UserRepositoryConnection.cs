﻿using System;

namespace Cape.Data
{
    public class UserRepositoryConnection : IDisposable
    {
        private readonly ApplicationDbContext context;

        public UserRepositoryConnection(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public ApplicationDbContext AppContext { get { return context; } }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}