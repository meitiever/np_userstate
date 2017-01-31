using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

namespace Nop.Plugin.Misc.UserStateManagement.Services
{
    public interface IFileDataCacheManager
    {
        T Get<T>(string key, string dependentFilePath, Func<string, object> resolveValue)
            where T : class;
    }

    public class FileDataCacheManager : IFileDataCacheManager
    {
        private readonly Dictionary<string, FileCacheResolver> resolvers = new Dictionary<string, FileCacheResolver>();

        public T Get<T>(string key, string dependentFilePath, Func<string, object> resolveValue)
            where T : class
        {
            FileCacheResolver resolver;
            if (!resolvers.TryGetValue(key, out resolver))
            {
                resolver = new FileCacheResolver
                {
                    Key = key,
                    FileName = dependentFilePath,
                    Resolver = resolveValue
                };

                resolvers[key] = resolver;
            }

            var value = HttpRuntime.Cache[key];
            if (value == null)
            {
                value = resolver.Resolver(resolver.FileName);
                HttpRuntime.Cache.Insert(key, value, new CacheDependency(resolver.FileName));
            }

            return value as T;
        }

        private class FileCacheResolver
        {
            public string Key { get; set; }

            public string FileName { get; set; }

            public Func<string, object> Resolver { get; set; }
        }
    }
}
