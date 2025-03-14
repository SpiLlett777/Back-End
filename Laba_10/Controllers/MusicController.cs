using Laba_10.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Laba_10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly List<Track> _tracks;

        public MusicController()
        {
            _tracks = Data.MusicDb.Tracks;
        }

        [HttpGet("html")]
        public IActionResult GetTracksHtml()
        {
            string html = $"<html><head><meta charset=\"utf-8\"></head><body><h1>Music List</h1><ul>";
            foreach (var track in _tracks)
            {
                html += $"<li>{track.Title} - {track.Artist}. <a href='/api/music/download/{track.Id}'>Download</a></li>";
            }

            html += "</ul></body></html>";
            
            return Content(html, "text/html");
        }

        [HttpGet("json")]
        public IActionResult GetTracksJson()
        {
            string jsonData = JsonSerializer.Serialize(_tracks);
            return Content(jsonData, "application/json");
        }

        [HttpGet("download/{id}")]
        public IActionResult DownloadTrack([FromRoute] int id)
        {
            var track = _tracks.FirstOrDefault(t => t.Id == id);

            if (track is null)
                return NotFound();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), track.FilePath);

            return PhysicalFile(filePath, "audio/mpeg", track.Title + ".mp3");
        }
    }
}
