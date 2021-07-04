using System;
using System.Reflection.Metadata.Ecma335;
using JiraProject.Business.Abstract;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace JiraProject.Business.Concrete
{
    public class RedisCacheManager : ICacheService
    {
        private readonly IDistributedCache _redisCache;


        public RedisCacheManager(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }




        public string Get(string key)
        {
            return _redisCache.GetString(key);
        }



        public T Get<T>(string key)
        {
            string value = Get(key);
            return JsonConvert.DeserializeObject<T>(value);
        }



        public void Add(string key, object value, int duration)
        {

            var cacheOptions = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(duration)
            };

            _redisCache.SetString(key,JsonConvert.SerializeObject(value),cacheOptions);

        }



        public bool IsKeyExist(string key)
        {
            return _redisCache.GetString(key) == null ? false : true;
        }

        public void Remove(string key)
        {
            _redisCache.Remove(key);
        }


    }
}