namespace CosmicPlayer.Models;

public sealed record MediaTrack(
    string Id,
    string Title,
    string Artist,
    string FilePath,
    TimeSpan Duration,
    long FileSize
);

