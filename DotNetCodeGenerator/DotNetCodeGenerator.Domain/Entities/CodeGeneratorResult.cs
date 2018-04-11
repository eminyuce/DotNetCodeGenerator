using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCodeGenerator.Domain.Entities
{
    public class CodeGeneratorResult
    {
        public bool IsMethodStatic { get;  set; }
        public string StoredProcExec { get; set; }
        public string StoredProcExecModel { get; set; }
        public string StoredProcExecModelDataReader { get; set; }
    }
}
