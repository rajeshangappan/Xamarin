using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamPlayer.VideoPlayerSrc
{
    public interface IVideoPicker
    {
        Task<string> GetVideoFileAsync();
    }
}
