using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleDriveTest.Lib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoogleDriveTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropboxController : ControllerBase
    {
        DropboxLib lib;

        public DropboxController(DropboxLib dlib)
        {
            lib = dlib;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await lib.GetAccountName();
            return Ok(result);
        }

        [HttpGet("CreateFolder")]
        public async Task<IActionResult> CreateFolder()
        {
            return Ok(await lib.CreateFolder());
        }

        [HttpGet("UploadFile")]
        public async Task<IActionResult> UploadFile()
        {
            return Ok(await lib.UploadFile());
        }

        [HttpGet("DownloadFile")]
        public async Task<IActionResult> DownloadFile()
        {
            return Ok(await lib.DownloadFile("NewText.txt"));
        }

        [HttpGet("ListOfFiles")]
        public async Task<IActionResult> GetListOfFiles()
        {
            return Ok(await lib.GetFilesList());
        }
    }
}