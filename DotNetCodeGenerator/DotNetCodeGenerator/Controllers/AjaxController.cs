using DotNetCodeGenerator.Domain.Services;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetCodeGenerator.Controllers
{
    public class AjaxController : BaseController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public ActionResult GetTables(String connectionString="")
        {
            if (String.IsNullOrEmpty(connectionString))
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            var allTablesMetaData = TableService.GetAllTablesFromCache(connectionString);
            var resultHtml = from t in allTablesMetaData.Tables
                    select new
                    {
                        TableNameWithSchema = t.TableNameWithSchema,
                        DatabaseTableName = t.DatabaseTableName
                    };
            return Json(resultHtml, JsonRequestBehavior.AllowGet);
        }
    }
}