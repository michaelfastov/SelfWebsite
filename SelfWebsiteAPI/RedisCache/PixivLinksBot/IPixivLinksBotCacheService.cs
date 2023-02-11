using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCache.PixivLinksBot;

public interface IPixivLinksBotCacheService
{
    Task CacheRecentPixivLinks(string username, string newLink);
    Task<List<string>> GetCachedLinksByUsername(string username);
}
