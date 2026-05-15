using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Core.Models.Interfaces.Repositories;
using ProjectManager.Data.Context;
using ProjectManager.Data.Repositories;
using ProjectManager.Data.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Data
{
    public static class DInjection
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EfContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<IProjectRepository, ProjectRepository>();
            //services.AddScoped<ITaskRepository, TaskRepository>(); // TODO: доделать
            services.AddScoped<IUserRepository, UserRepository>();
            
            return services;
        }
    }
}
