using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Swift_MT799.Helpers;
using Swift_MT799.Models;
using NLog;
using Microsoft.Extensions.Logging;

namespace Swift_MT799.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SwiftMessagesController : ControllerBase
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync([FromForm] IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    logger.Warn("File not selected.");
                    return BadRequest("File not selected");
                }

                using var reader = new StreamReader(file.OpenReadStream());
                string content = await reader.ReadToEndAsync();

                SwiftMessage swiftMessage = SwiftParser.Parse(content);
                if (swiftMessage == null)
                {
                    logger.Warn("Failed to parse Swift message.");
                    return BadRequest("Failed to parse Swift message.");
                }

                var sqliteHelper = new SQLiteHelper();
                sqliteHelper.InsertMessage(swiftMessage);
                logger.Info("Swift message processed and saved successfully.");
                return Ok("Swift message processed and saved successfully.");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error processing Swift message.");
                return StatusCode(500, "Internal server error. Check logs for more details.");
            }
        }

    }
}
