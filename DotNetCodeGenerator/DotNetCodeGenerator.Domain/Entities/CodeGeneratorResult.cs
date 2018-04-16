using DotNetCodeGenerator.Domain.Entities.Enums;
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
        public DatabaseMetadata DatabaseMetadata { get; set; }
        public string ConnectionString { get; set; }
        public string MySqlConnectionString { get; set; }

        [Required]
        public string SelectedTable { get; set; }
        [AllowHtml]
        [Required]
        public string ModifiedTableName { get; set; }
        [AllowHtml]
        public string StringCodePattern { get; set; }

        [AllowHtml]
        public string NameSpace { get; set; }

        public bool IsMethodStatic { get;  set; }
       

   
        [DataType(DataType.MultilineText)]
        public string StoredProcExec { get; set; }
        [Display(Name = "Stored Proc Exec Code")]
        [AllowHtml]
        public string StoredProcExecCode { get; set; }
        [Display(Name = "StoredProcExecModel")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string StoredProcExecModel { get; set; }
        [Display(Name = "StoredProcExecModelDataReader")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string StoredProcExecModelDataReader { get; set; }
 
        [Display(Name = "TableRepository")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string TableRepository { get;  set; }
        public bool IsModelAttributesVisible { get; set; }
        [AllowHtml]
        public string TableClassItem { get; set; }
        [AllowHtml]
        public string TableClassInstance { get; set; }
        [AllowHtml]
        public string SqlDatabaseOperation { get;  set; }
        [AllowHtml]
        public string AspMvcControllerClass { get; set; }


        public string UserMessage { get; set; }
        public UserMessageState UserMessageState { get; set; }

        [AllowHtml]
        public string MySqlDatabaseOperation { get; set; }

    }
}
