using Hangmo.Repository.Data.DAO.Interfaces;
using Hangmo.Repository.Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Hangmo.Repository
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBaseDAOImplementations(this IServiceCollection services)
        {
            var baseDaoType = typeof(IBaseDAO<>);

            // Escaneia todos os tipos no assembly atual
            var daoImplementations = Assembly.GetExecutingAssembly().GetTypes()
                // Filtra apenas classes que implementam IBaseDAO<T>
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == baseDaoType))
                // Mapeia cada implementação para seu tipo de interface correspondente
                .Select(t => new { Interface = t.GetInterfaces().First(i => i.GetGenericTypeDefinition() == baseDaoType), Implementation = t });

            // Registra todas as implementações encontradas
            foreach (var daoImpl in daoImplementations)
            {
                services.AddScoped(daoImpl.Interface, daoImpl.Implementation);
            }
        }
    }
}