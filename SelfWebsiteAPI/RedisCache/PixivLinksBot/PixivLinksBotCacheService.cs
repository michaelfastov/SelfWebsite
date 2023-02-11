using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCache.PixivLinksBot;

public class PixivLinksBotCacheService : IPixivLinksBotCacheService
{
    private const int RedisFirstRank = 1;
    private const int RedisMaxLinks = 3;
    private readonly IConnectionMultiplexer _redis;

    public PixivLinksBotCacheService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task CacheRecentPixivLinks(string username, string newLink)
    {
        var db = _redis.GetDatabase();

        var sortedSet = db.SortedSetRangeByRank(username).ToList();

        if (sortedSet.Count == RedisMaxLinks)
        {
            db.SortedSetRemoveRangeByScore(username, RedisFirstRank, RedisFirstRank);
            sortedSet.RemoveAt(0);

            for (int i = 0; i < sortedSet.Count; i++)
            {
                var link = sortedSet[i].ToString();
                db.SortedSetAdd(username, link, i + RedisFirstRank);
            }
        }

        db.SortedSetAdd(username, newLink, sortedSet.Count + RedisFirstRank);
    }

    public async Task<List<string>> GetCachedLinksByUsername(string username)
    {
        var db = _redis.GetDatabase();

        return (await db.SortedSetRangeByRankAsync(username)).Select(x => x.ToString()).ToList();
    }
}
