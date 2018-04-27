using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetCodeGenerator.Domain.Entities;
using DotNetCodeGenerator.Domain.Helpers;
using DotNetCodeGenerator.Domain.Entities.Enums;
using DotNetCodeGenerator.Domain.Services;
using Ninject;
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
            codeGeneratorResult.UserMessage = "Hi, dude, Generate the code of selected table \"Controller, Service, Repository and the SQL\" :)";
            codeGeneratorResult.UserMessageState = UserMessageState.Welcome;
            return View(codeGeneratorResult);
        }
        [HttpPost]
        public async Task<ActionResult> Index(CodeGeneratorResult codeGeneratorResult, string btnAction="")
        {
            if(!String.IsNullOrEmpty(codeGeneratorResult.ConnectionString.ToStr().Trim()) 
                || !String.IsNullOrEmpty(codeGeneratorResult.MySqlConnectionString.ToStr().Trim()))
            {
                if(String.IsNullOrEmpty(codeGeneratorResult.SelectedTable.ToStr().Trim()))
                {
                    ModelState.AddModelError("SelectedTable", "Selected Table is required.");
                    return View(codeGeneratorResult);
                }
                else if (String.IsNullOrEmpty(codeGeneratorResult.ModifiedTableName.ToStr().Trim()))
                {
                    ModelState.AddModelError("ModifiedTableName", "Entity Name is required.");
                    return View(codeGeneratorResult);
                }
            }
            else if (String.IsNullOrEmpty(codeGeneratorResult.SqlCreateTableStatement.ToStr().Trim()))
            {
                ModelState.AddModelError("SqlCreateTableStatement", "Sql Create Table Statement is required.");
                return View(codeGeneratorResult);
            }

            if (btnAction.Equals("Generate Code", StringComparison.InvariantCultureIgnoreCase))
            {
                await TableService.GenerateCode(codeGeneratorResult);
            }
            else if (btnAction.Equals("Fill GridView", StringComparison.InvariantCultureIgnoreCase))
            {
                await TableService.FillGridView(codeGeneratorResult);
            }
            //Logger.Trace("XmlParserHelper.ToXml codeGeneratorResult " + XmlParserHelper.ToXml(codeGeneratorResult));
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