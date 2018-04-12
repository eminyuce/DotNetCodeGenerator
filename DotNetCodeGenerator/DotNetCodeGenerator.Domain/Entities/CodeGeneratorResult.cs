using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DotNetCodeGenerator.Domain.Entities
{
    public class CodeGeneratorResult
    {
        public string ConnectionString { get; set; }
        public string SelectedTable { get; set; }
        public string ModifiedTableName { get; set; }
        public string StringCodePattern { get; set; }
        public bool IsMethodStatic { get;  set; }


        [Display(Name = "StoredProcExec")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string StoredProcExec { get; set; }
        [Display(Name = "StoredProcExecModel")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string StoredProcExecModel { get; set; }
        [Display(Name = "StoredProcExecModelDataReader")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string StoredProcExecModelDataReader { get; set; }


        [Display(Name = "StoredProcExec")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string SaveOrUpdateDatabaseUtility { get; set; }

    }
}
