using DotNetCodeGenerator.Domain.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetCodeGenerator.Controllers
{
    public class AjaxController : Controller
    {
        [Inject]
        public TableService TableService { get; set; }
        public ActionResult GetTables(String connectionString)
        {
            var allTablesMetaData = TableService.GetAllTables(connectionString);
            var resultHtml = from t in allTablesMetaData.Tables
                    select new
                    {
                        TableName = t.TableName,
                        DatabaseTableName = t.DatabaseTableName
                    };
            return Json(resultHtml, JsonRequestBehavior.AllowGet);
        }
    }
}