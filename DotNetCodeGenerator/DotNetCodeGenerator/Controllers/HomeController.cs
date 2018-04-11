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

namespace DotNetCodeGenerator.Controllers
{
    public class HomeController : Controller
    {
        [Inject]
        public TableService TableService { get; set; }
        public ActionResult Index()
        {
            var codeGeneratorResult = new CodeGeneratorResult();
            return View(codeGeneratorResult);
        }
        [HttpPost]
        public ActionResult Index(CodeGeneratorResult codeGeneratorResult)
        {
            TableService.GenerateCode(codeGeneratorResult);
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