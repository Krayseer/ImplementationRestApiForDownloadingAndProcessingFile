using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.RegularExpressions;
using UploadRESTful.Models;

namespace UploadRESTful.Controllers
{
    public class FileUploadController : Controller
    {
        private static string? result;
        private static int id = 0;

        public IActionResult Index() => View();

        [HttpPost]
        [RequestSizeLimit(1048576)] // Ограничение в 1 Мб
        public async Task<IActionResult> Index(IFormFile files)
        {
            result = "";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), files.FileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await files.CopyToAsync(stream);
            var builderText = new StringBuilder();
            var reader = new StreamReader(files.OpenReadStream());
            while (reader.Peek() >= 0) builderText.AppendLine(reader.ReadLine());
            var text = Regex.Split(builderText.ToString(), @"\r\n");
            result = Parser.ParseText(text); // Получить распарсенный файл в виде дерева
            id = new Random().Next(1, 100);
            return Ok("Твой id для получения информации: " + id);
        }

        [HttpGet("FileUpload/{id}")] // Получить текст по заданному id
        public IActionResult GetText() => Ok(result);
    }
}