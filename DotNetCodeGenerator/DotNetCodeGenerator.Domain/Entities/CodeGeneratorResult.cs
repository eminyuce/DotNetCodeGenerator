﻿using DotNetCodeGenerator.Domain.Entities.Enums;
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
        [Display(Name = "SqlServer Connection String")]
        public string ConnectionString { get; set; }
        [Display(Name = "MySql Connection String")]
        public string MySqlConnectionString { get; set; }
        public string SqlCreateTableStatement { get; set; }
        [Display(Name = "Table Name to Generate Code")]
        public string SelectedTable { get; set; }
        [AllowHtml]
        [Required]
        [Display(Name = "Entity Name")]
        public string ModifiedTableName { get; set; }
        [AllowHtml]
        public string StringCodePattern { get; set; }

        [AllowHtml]
        [Display(Name = "NameSpace")]
        public string NameSpace { get; set; }

        public bool IsMethodStatic { get;  set; }
        public bool IsModelAttributesVisible { get; set; }


        [DataType(DataType.MultilineText)]
        public string StoredProcExec { get; set; }
        [Display(Name = "Stored Proc Exec Code")]
        [AllowHtml]
        public string StoredProcExecCode { get; set; }
        [Display(Name = "Stored Proc Model")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string StoredProcExecModel { get; set; }
        [Display(Name = "Stored Proc Model Reader")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string StoredProcExecModelDataReader { get; set; }
 
        [Display(Name = "Entity Repository")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string TableRepository { get;  set; }
 
        [AllowHtml]
        public string TableClassItem { get; set; }
        [AllowHtml]
        public string TableClassInstance { get; set; }
        [AllowHtml]
        public string SqlDatabaseOperation { get;  set; }
        [AllowHtml]
        public string AspMvcControllerClass { get; set; }
        [AllowHtml]
        public string SqlSaveOrUpdateStoredProc { get; set; }

        public string UserMessage { get; set; }
        public UserMessageState UserMessageState { get; set; }

        [AllowHtml]
        public string MySqlDatabaseOperation { get; set; }
        [AllowHtml]
        public string MySqlSaveOrUpdateStoredProc { get; set; }
        [AllowHtml]
        public string WebApiController { get; set; }

    }
}
