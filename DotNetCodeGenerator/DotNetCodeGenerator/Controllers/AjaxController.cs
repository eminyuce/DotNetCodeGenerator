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
        public ActionResult GetTables(String connectionString = "", string mySqlConnectionString = "")
        {
            if (String.IsNullOrEmpty(connectionString) && String.IsNullOrEmpty(mySqlConnectionString))
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            if (!String.IsNullOrEmpty(connectionString))
            {
                var allTablesMetaData = TableService.GetAllTablesFromCache(connectionString);
                var resultHtml = (from t in allTablesMetaData.Tables
                                 select new
                                 {
                                     TableNameWithSchema = t.TableNameWithSchema,
                                     DatabaseTableName = t.DatabaseTableName + "-" + t.SuggestedEntityName
                                 }).ToList();

                resultHtml.Insert(0, new { TableNameWithSchema = "Select a Table from SqlServer", DatabaseTableName = "" });
                return Json(resultHtml, JsonRequestBehavior.AllowGet);
            }
            else if (!String.IsNullOrEmpty(mySqlConnectionString))
            {
                var allTablesMetaData = TableService.GetAllMySqlTables(mySqlConnectionString);
                var resultHtml = (from t in allTablesMetaData.Tables
                                  select new
                                  {
                                      TableNameWithSchema = t.TableNameWithSchema,
                                      DatabaseTableName = t.DatabaseTableName + "-" + t.SuggestedEntityName
                                  }).ToList();

                resultHtml.Insert(0, new { TableNameWithSchema = "Select a Table From MySql", DatabaseTableName = "" });
                return Json(resultHtml, JsonRequestBehavior.AllowGet);
            }

            return Json("", JsonRequestBehavior.AllowGet);

        }
    }
}