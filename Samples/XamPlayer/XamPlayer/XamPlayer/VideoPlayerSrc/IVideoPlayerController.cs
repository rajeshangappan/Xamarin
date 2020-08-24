using System;

namespace XamPlayer.VideoPlayerSrc
{
    public interface IVideoPlayerController
    {
        VideoStatus Status { set; get; }

        TimeSpan Duration { set; get; }
    }
}