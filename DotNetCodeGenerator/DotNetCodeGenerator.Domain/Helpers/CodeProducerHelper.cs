using DotNetCodeGenerator.Domain.Repositories;
using DotNetCodeGenerator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DotNetCodeGenerator.Domain.Services;
using Ninject;

namespace DotNetCodeGenerator.Domain.Helpers
{
    public class CodeProducerHelper
    {
        [Inject]
        public TableService TableService { get; set; }
        public void GenerateSPModel(CodeGeneratorResult codeGeneratorResult,
            DatabaseMetadata databaseMetadata)
        {
            #region Execute SP to get tables so that we can generate code
            string StoredProc_Exec = codeGeneratorResult.StoredProcExec;

            if (String.IsNullOrEmpty(StoredProc_Exec))
            {
                return;
            }
            StoredProc_Exec = StoredProc_Exec.Replace("\r\n", " ").Trim();
            string returnResultClass = "NwmResultItem";
            string storedProcName = "";
            DataSet ds = null;
            String sqlCommand = "";
            List<string> tableNames = new List<string>();
            try
            {
                storedProcName = Regex.Split(StoredProc_Exec, @"\s+").Select(r => r.Trim()).FirstOrDefault();
                String[] storedProcNameParts = Regex.Split(storedProcName, @"_").Select(r => r.Trim()).ToArray();
                storedProcName = storedProcNameParts != null && storedProcNameParts.Any() ? storedProcNameParts[1] : storedProcName;
                storedProcName = GeneralHelper.ToTitleCase(storedProcName);
                //  storedProcName = StoredProc_Exec.Split("-".ToCharArray());
                StoredProc_Exec = StoredProc_Exec.Replace("]", "").Replace("[", "").Trim();
                string[] m = StoredProc_Exec.Split("-".ToCharArray());

                sqlCommand = m.FirstOrDefault();

                ds = TableService.GetDataSet(sqlCommand, databaseMetadata.ConnectionString);

                String tableNamesTxt = m.LastOrDefault();

                #region Entity names are coming from user input
                // If no entity names are defined, we will generate table names
                if (m.Length > 1)
                {
                    if (!String.IsNullOrEmpty(tableNamesTxt))
                    {
                        tableNames = Regex.Split(tableNamesTxt, @"\s+").Select(r => r.Trim()).Where(s => !String.IsNullOrEmpty(s)).ToList();
                    }

                    // we have more than one tables coming from SP
                    if (ds.Tables.Count > 1)
                    {
                        // The last table names is the result of that method.
                        // Table names should be more than number of returned table
                        // 
                        if (ds.Tables.Count + 1 == tableNames.Count)
                        {
                            returnResultClass = tableNames.LastOrDefault();
                        }
                        else if (ds.Tables.Count == tableNames.Count)
                        {

                        }
                        else if (ds.Tables.Count > tableNames.Count)
                        {
                            int diff = ds.Tables.Count - tableNames.Count;
                            for (int i = 0; i < diff; i++)
                            {
                                tableNames.Add("Tablo" + i);
                            }
                        }
                        else if (ds.Tables.Count < tableNames.Count)
                        {
                            // number to remove is the difference between the current length
                            // and the maximum length you want to allow.
                            var count = tableNames.Count - ds.Tables.Count;
                            if (count > 0)
                            {
                                // remove that number of items from the start of the list
                                tableNames.RemoveRange(0, count);
                            }
                        }
                    }

                }
                else
                {
                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        tableNames.Add("Tablo" + i);
                    }

                }
                #endregion

            }
            catch (Exception ex)
            {

                codeGeneratorResult.StoredProcExec = ex.StackTrace;


            }
            if (ds == null)
            {
                return;
            }
            #endregion
            #region Generating ENTITY FROM datable coming from SP
            try
            {

                var built2 = new StringBuilder();
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    DataTable table = ds.Tables[i];

                    var built = new StringBuilder();
                    built.AppendLine(String.Format("public class {0} ", tableNames.Any() ? tableNames[i] : "Tablo" + i) + "{");
                    foreach (DataColumn column in table.Columns)
                    {
                        try
                        {
                            String dataType = "string";
                            DataRow firstRow = table.Rows.Cast<DataRow>().ToArray().Take(1).FirstOrDefault();
                            if (firstRow != null)
                            {

                                dataType = firstRow[column].GetType().Name.ToLower()
                                    .Replace("32", "")
                                    .Replace("boolean", "bool")
                                    .Replace("datetime", "DateTime");
                                if (firstRow[column].GetType().Name.Equals("DBNull"))
                                {
                                    dataType = "string";
                                }
                            }

                            built.AppendLine(String.Format("public {1} {0} ", column.ColumnName, dataType) + "{ get; set;}");
                        }
                        catch (Exception ee)
                        {

                        }

                    }
                    built.AppendLine("}");
                    built2.AppendLine(built.ToString());

                }
                if (ds.Tables.Count == 1)
                {
                    codeGeneratorResult.StoredProcExecModel = built2.ToString();
                }
                else
                {
                    built2.AppendLine("");
                    //Generating the return result class and its related list classess 
                    built2.AppendLine(String.Format("public class {0} ", returnResultClass) + "{");
                    for (int i = 0; i < tableNames.Count; i++)
                    {
                        if (tableNames[i].Equals(returnResultClass, StringComparison.InvariantCultureIgnoreCase))
                            continue;
                        built2.AppendLine(String.Format("public List<{1}> {0}List ", tableNames[i], tableNames[i]) + "{ get; set;}");
                    }
                    built2.AppendLine("}");
                    codeGeneratorResult.StoredProcExecModel = built2.ToString();
                }

            }
            catch (Exception ex)
            {
                codeGeneratorResult.StoredProcExecModel = ex.StackTrace;

            }
            #endregion

            #region  Generating Table to Entity method code
            String staticText = codeGeneratorResult.IsMethodStatic ? "static" : "";
            try
            {
                var built2 = new StringBuilder();
                // generating entities from data row classes 
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    DataTable table = ds.Tables[i];
                    String modelName = String.Format("{0}", tableNames.Any() ? tableNames[i] : "Tablo" + i);
                    var method = new StringBuilder();
                    method.AppendLine("private " + staticText + " " + modelName + " Get" + modelName + "FromDataRow(DataRow dr)");
                    method.AppendLine("{");
                    method.AppendLine("var item = new " + modelName + "();");
                    method.AppendLine("");

                    foreach (DataColumn column in table.Columns)
                    {
                        String dataType = "string";
                        DataRow firstRow = table.Rows.Cast<DataRow>().ToArray().Take(1).FirstOrDefault();
                        if (firstRow != null)
                        {

                            dataType = firstRow[column].GetType().Name.ToLower().Replace("32", "").Replace("boolean", "bool").Replace("datetime", "DateTime");
                            if (firstRow[column].GetType().Name.Equals("DBNull"))
                            {
                                dataType = "string";
                            }
                        }

                        dataType = dataType.ToLower();
                        // method.AppendLine("item." + column.ColumnName + " = dr[\"" + column.ColumnName + "\"].ToStr();");


                        if (dataType.IndexOf("string") > -1)
                        {
                            // method.AppendLine("item." + item.columnName + " = (read[\"" + item.columnName + "\"] is DBNull) ? \"\" : read[\"" + item.columnName + "\"].ToString();");
                            method.AppendLine("item." + column.ColumnName + " = dr[\"" + column.ColumnName + "\"].ToStr();");
                        }
                        else if (dataType.IndexOf("int") > -1)
                        {
                            //method.AppendLine("item." + item.columnName + " = (read[\"" + item.columnName + "\"] is DBNull) ? -1 : Convert.ToInt32(read[\"" + item.columnName + "\"].ToString());");
                            method.AppendLine("item." + column.ColumnName + " = dr[\"" + column.ColumnName + "\"].ToInt();");
                        }
                        else if (dataType.IndexOf("date") > -1)
                        {
                            //method.AppendLine("item." + item.columnName + " = (read[\"" + item.columnName + "\"] is DBNull) ? DateTime.Now : DateTime.Parse(read[\"" + item.columnName + "\"].ToString());");
                            method.AppendLine("item." + column.ColumnName + " = dr[\"" + column.ColumnName + "\"].ToDateTime();");

                        }
                        else if (dataType.IndexOf("bool") > -1)
                        {
                            //method.AppendLine("item." + item.columnName + " = (read[\"" + item.columnName + "\"] is DBNull) ? false : Boolean.Parse(read[\"" + item.columnName + "\"].ToString());");
                            method.AppendLine("item." + column.ColumnName + " = dr[\"" + column.ColumnName + "\"].ToBool();");
                        }
                        else if (dataType.IndexOf("float") > -1)
                        {
                            //method.AppendLine("item." + item.columnName + " = (read[\"" + item.columnName + "\"] is DBNull) ? -1 : float.Parse(read[\"" + item.columnName + "\"].ToString());");
                            method.AppendLine("item." + column.ColumnName + " = dr[\"" + column.ColumnName + "\"].ToFloat();");
                        }

                    }
                    method.AppendLine("return item;");
                    method.AppendLine("}");
                    built2.AppendLine(method.ToString());

                }

                codeGeneratorResult.StoredProcExecModelDataReader = built2.ToString();





            }
            catch (Exception ex)
            {
                codeGeneratorResult.StoredProcExecModelDataReader = ex.StackTrace;

            }
            #endregion
            #region Generationg  calling SP method, main functionality

            try
            {
                String modelName2 = "";
                string returnTypeText = "";
                if (ds.Tables.Count > 1)
                {
                    returnTypeText = "dbSpResult";
                }

                var method = new StringBuilder();
                method.AppendLine("//" + StoredProc_Exec);
                var queryParts = Regex.Split(sqlCommand, @"\s+").Select(r => r.Trim()).Where(s => !String.IsNullOrEmpty(s)).ToList();
                String sp = queryParts.FirstOrDefault();
                sqlCommand = sqlCommand.Replace(sp, "");

                var queryParts2 = Regex.Split(sqlCommand, @",").Select(r => r.Trim()).Where(s => !String.IsNullOrEmpty(s)).ToList();



                String modelName = String.Format("{0}", tableNames.Any() ? tableNames.LastOrDefault() : "Table" + (ds.Tables.Count + 1));
                String returnOfMethodName = tableNames.Any() && tableNames.Count > 1 ? returnResultClass : " List<" + modelName + ">";
                String selectedTable = databaseMetadata.DatabaseName;
                string methodParameterBuiltText = "()";
                if (queryParts2.Any())
                {
                    StringBuilder methodParameterBuilt = new StringBuilder();

                    methodParameterBuilt.Append("(");
                    foreach (var item in queryParts2)
                    {
                        try
                        {
                            var parameterParts = Regex.Split(item, @"=").Select(r => r.Trim()).Where(s => !String.IsNullOrEmpty(s)).ToList();
                            var paraterValue = parameterParts.LastOrDefault();
                            var paramterName = parameterParts.FirstOrDefault().Replace("@", "");
                            var parameterName2 = paramterName.ToLower();
                            if (paramterName.ToLower().Contains("date"))
                            {
                                methodParameterBuilt.Append("DateTime ? " + parameterName2 + " =null,");
                            }
                            else if (paraterValue.Contains("'"))
                            {
                                paraterValue = paraterValue.Replace("'", "\"");
                                methodParameterBuilt.Append("string " + parameterName2 + " = " + paraterValue + ",");
                            }
                            else
                            {
                                methodParameterBuilt.Append("int " + parameterName2 + " = " + paraterValue + ",");
                            }


                        }
                        catch (Exception)
                        {


                        }

                    }
                    methodParameterBuiltText = methodParameterBuilt.ToString().Trim().TrimEnd(",".ToCharArray());
                    methodParameterBuiltText = methodParameterBuiltText + ")";
                }

                method.AppendLine(" public " + staticText + " " + returnOfMethodName + " Get" + storedProcName + methodParameterBuiltText.ToString());
                method.AppendLine(" {");
                String commandText = sp;
                method.AppendLine(" string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;");
                method.AppendLine(String.Format(" String commandText = @\"{0}\";", commandText));
                method.AppendLine(" var parameterList = new List<SqlParameter>();");
                method.AppendLine(" var commandType = CommandType.StoredProcedure;");


                foreach (var item in queryParts2)
                {
                    try
                    {
                        var parameterParts = Regex.Split(item, @"=").Select(r => r.Trim()).Where(s => !String.IsNullOrEmpty(s)).ToList();
                        var paraterValue = parameterParts.LastOrDefault();
                        var paramterName = parameterParts.FirstOrDefault().Replace("@", "");
                        var parameterName2 = paramterName.ToLower();
                        string sqlDbType = "SqlDbType.Int";
                        if (paramterName.ToLower().Contains("date"))
                        {
                            method.AppendLine("if(" + parameterName2 + ".HasValue)");
                            sqlDbType = "SqlDbType.DateTime";
                        }
                        else if (paraterValue.Contains("'"))
                        {
                            sqlDbType = "SqlDbType.NVarChar";
                            parameterName2 = parameterName2 + ".ToStr()";
                        }
                        else
                        {
                            //    parameterName2 = parameterName2;
                        }

                        method.AppendLine(" parameterList.Add(DatabaseUtility.GetSqlParameter(\"" + paramterName + "\", " + parameterName2 + "," + sqlDbType + "));");

                    }
                    catch (Exception)
                    {


                    }

                }
                if (ds.Tables.Count == 1)
                {
                    method.AppendLine(String.Format("[return_type]"));
                }
                else
                {
                    method.AppendLine(String.Format("var dbSpResult=new {0}();", returnResultClass));
                }

                method.AppendLine(" DataSet dataSet = DatabaseUtility.ExecuteDataSet(new SqlConnection(connectionString), commandText, commandType, parameterList.ToArray());");
                method.AppendLine(" if (dataSet.Tables.Count > 0)");
                method.AppendLine(" {");

                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    try
                    {
                        modelName2 = String.Format("{0}", tableNames.Any() ? tableNames[i] : "Tablo" + i);
                        if (ds.Tables.Count != 1)
                        {
                            method.AppendLine(String.Format("var list{0}=new List<{1}>();", i, modelName2));
                        }
                        else
                        {

                        }
                        method.AppendLine(String.Format(" using (DataTable dt = dataSet.Tables[{0}])", i));
                        method.AppendLine(" {");
                        method.AppendLine(" foreach (DataRow dr in dt.Rows)");
                        method.AppendLine(" {");
                        method.AppendLine(String.Format(" var e = Get{0}FromDataRow(dr);", modelName2));
                        method.AppendLine(String.Format(" list{0}.Add(e);", i));
                        method.AppendLine(" }");
                        if (ds.Tables.Count > 1)
                        {
                            method.AppendLine(" dbSpResult." + modelName2 + "List=list" + i + ";");
                        }

                        method.AppendLine(" }");
                        method.AppendLine(" ");
                        method.AppendLine(" ");
                    }
                    catch (Exception)
                    {


                    }

                }
                returnTypeText = String.Format("var list{0}=new List<{1}>();", 0, modelName2);

                method.Replace("[return_type]", returnTypeText);
                method.AppendLine(" }");
                if (ds.Tables.Count > 1)
                {
                    method.AppendLine(" return dbSpResult;");
                }
                else
                {
                    method.AppendLine(" return list0;");
                }



                method.AppendLine(" }");

                codeGeneratorResult.StoredProcExec = method.ToString();
            }
            catch (Exception ex)
            {
                codeGeneratorResult.StoredProcExec = ex.StackTrace;
            }
            #endregion

        }

    }
}
