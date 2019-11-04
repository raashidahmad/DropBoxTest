using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleDriveTest.Lib
{
    public class DropboxLib
    {
        string token = "40mPFdltTIAAAAAAAAAADEudwU9BL8Zq1VXgdrVeBXBOP-_QFshfwiXpRKX74IFm";
        public DropboxLib()
        {

        }

        public async Task<string> GetAccountName()
        {
            using (var dbx = new DropboxClient(token))
            {
                var full = await dbx.Users.GetCurrentAccountAsync();
                return ("Name: " + full.Name.DisplayName + " Email: " + full.Email);
            }
        }

        public async Task<string> CreateFolder()
        {
            using (var dbx = new DropboxClient(token))
            {
                var folderArg = new CreateFolderArg("/Test");
                var result = await dbx.Files.CreateFolderV2Async(folderArg);
                return result.Metadata.Name;
            }
        }

        public async Task<string> UploadFile()
        {
            using (var dbx = new DropboxClient(token))
            {
                string fileName = "NewText.txt";
                var reader = new StreamReader("E://DropboxTestFiles/Sample.txt");
                var content = reader.ReadToEnd();
                using (var stream = new MemoryStream(System.Text.UTF8Encoding.UTF8.GetBytes(content)))
                {
                    var response = await dbx.Files.UploadAsync("/Test/" + fileName, WriteMode.Overwrite.Instance, body: stream);
                    return fileName;
                }
            }
        }

        public async Task<string> DownloadFile(string fileName)
        {
            using (var dbx = new DropboxClient(token))
            {
                using (var response = await dbx.Files.DownloadAsync("/Test/" + fileName))
                {
                    using (var fileStream = File.Create("E://DropboxTestFiles/Downloaded.txt"))
                    {
                        (await response.GetContentAsStreamAsync()).CopyTo(fileStream);
                    }
                }
            }
            return fileName;
        }

        public async Task<List<string>> GetFilesList()
        {
            List<string> fileNames = new List<string>();
            using (var dbx = new DropboxClient(token))
            {
                var list = await dbx.Files.ListFolderAsync("/Test");
                foreach (var item in list.Entries.Where(i => i.IsFile))
                {
                    var file = item.AsFile;
                    fileNames.Add(item.Name);
                }
                return fileNames;
            }
        }

    }
}
