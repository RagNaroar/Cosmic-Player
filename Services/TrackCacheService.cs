namespace CosmicPlayer.Services;

public static class TrackCacheService
{
    private static readonly string CacheFilePath = Path.Combine(FileSystem.AppDataDirectory, "track_cache.json");
    
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public static async Task<List<MediaTrack>> LoadValidTrackAsync()
    {
        if (!File.Exists(CacheFilePath))
        {
            return [];
        }
        try
        {
            await using var stream = File.OpenRead(CacheFilePath);
            var cacheTracks = await JsonSerializer.DeserializeAsync<List<MediaTrack>>(stream, JsonOptions);

            if (cacheTracks is null || cacheTracks.Count == 0)
            {
                return [];
            }

            var validTracks = new List<MediaTrack>(cacheTracks.Count);
            var hasBrokenTracks = false;
            
            foreach (var track in cacheTracks)
            {
                if (!string.IsNullOrWhiteSpace(track.FilePath) && !File.Exists(track.FilePath))
                {
                    validTracks.Add(track);
                }
                else
                {
                    hasBrokenTracks = true;
                }

                
            }
            if (hasBrokenTracks)
            {
                await SaveTrackAsync(validTracks);
            }
            return validTracks;
            
        }
        catch
        {
            return [];
        }

    }
    public static async Task SaveTrackAsync(IReadOnlyList<MediaTrack> tracks)
    {
        try
        {
            await using var stream = File.Create(CacheFilePath);
            await JsonSerializer.SerializeAsync(stream, tracks, JsonOptions);
        }
        catch
        {

        }
    }
}

