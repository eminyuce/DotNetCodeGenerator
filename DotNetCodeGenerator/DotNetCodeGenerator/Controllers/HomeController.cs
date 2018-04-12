using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetCodeGenerator.Domain.Entities;
using DotNetCodeGenerator.Domain.Helpers;
using Ninject;
using DotNetCodeGenerator.Domain.Services;
using System.Threading.Tasks;
using NLog;

namespace DotNetCodeGenerator.Controllers
{
    public class HomeController : BaseController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public ActionResult Index()
        {
            var codeGeneratorResult = new CodeGeneratorResult();
            codeGeneratorResult.DatabaseMetadata = null;
            return View(codeGeneratorResult);
        }
        [HttpPost]
        public async Task<ActionResult> Index(CodeGeneratorResult codeGeneratorResult)
        {

            await TableService.GenerateCode(codeGeneratorResult);
            return View(codeGeneratorResult);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}