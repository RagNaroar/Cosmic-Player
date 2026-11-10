namespace CosmicPlayer.Pages;

public partial class MainPage : ContentPage
{

    public MainPage() => InitializeComponent();

    private bool _isPlaying;

    private void PlayPauseBtnClicked(object? sender, EventArgs e)
    {
        _isPlaying = !_isPlaying;
        PlayPauseBtn.Text = _isPlaying ? "⏸" : "▶";
    }
}
