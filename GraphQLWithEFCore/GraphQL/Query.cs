using GraphQLWithEFCore.Context;
using GraphQLWithEFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLWithEFCore.GraphQL
{
    public class Query
    {
        private readonly DatabaseContext dbContext;

        public Query(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;

        }
        public IQueryable<Student> Students => dbContext.Students;
    }
}
