using KindleManager.Models;
using KindleManager.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Web;

namespace DotNetSelfHost.WinForms.Controllers
{
    [Route("/convert")]
    public class ConvertController
    {

        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> PostAsync([FromBody] RequestModel data)
        {
            return await HandleRequestData.ProcessContent(data);


        }
    }
}