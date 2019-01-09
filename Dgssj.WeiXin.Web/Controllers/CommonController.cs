using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dgssj.WeiXin.Web.Controllers
{
    public class CommonController : Controller
    {
        private IHostingEnvironment hostingEnvironment;
        string[] pictureFormatArray = { "png", "jpg", "jpeg", "PNG", "JPG", "JPEG"};
        public CommonController(IHostingEnvironment env)
        {
            this.hostingEnvironment = env;
        }
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Upload()
        {
            try
            {
                var files = Request.Form.Files;
                long size = files.Sum(f => f.Length);
                if (size > 104857600)
                {
                    return Json("图片总大小不能超过100M!");
                }

                List<string> filePathResultList = new List<string>();

                foreach (var file in files)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    string filePath = hostingEnvironment.WebRootPath + $@"\Files\Pictures\";

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    string suffix = fileName.Split('.')[1];

                    if (!pictureFormatArray.Contains(suffix))
                    {
                        return Json("仅支持 'png','jpg','jpeg' 类型文件");
                    }

                    fileName = Guid.NewGuid() + "." + suffix;

                    string fileFullName = filePath + fileName;

                    using (FileStream fs = System.IO.File.Create(fileFullName))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    filePathResultList.Add($"/Files/Pictures/{fileName}");
                }
                return Json(filePathResultList[0]);
            }
            catch (Exception ex)
            {
                return Json("上传失败");
            }
            
            
        }
    }
}