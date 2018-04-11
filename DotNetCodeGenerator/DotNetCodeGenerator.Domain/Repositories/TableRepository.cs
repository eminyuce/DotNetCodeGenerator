using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Collections;
using System.Resources;
using System.IO;
using System.Globalization;
using DotNetCodeGenerator.Domain.Entities;
using DotNetCodeGenerator.Domain.Helpers;

namespace DotNetCodeGenerator.Domain.Repositories
{
    public class TableRepository
    {
        public DatabaseMetadata GetAllTables(String connectionString)
        {
            var result = new DatabaseMetadata();
            SqlConnection con =
                          new SqlConnection(connectionString);


            try
            {

                con.Open();



                result.DatabaseName = con.Database;
                DataTable tblDatabases =
                                con.GetSchema(
                                           SqlClientMetaDataCollectionNames.Tables);


                var list = new List<TableMetaData>();
                foreach (DataRow rowDatabase in tblDatabases.Rows)
                {
                    var i = new TableMetaData();
                    i.TableCatalog = rowDatabase["table_catalog"].ToStr();
                    i.TableSchema = rowDatabase["table_schema"].ToStr();
                    i.TableName = rowDatabase["table_name"].ToStr();
                    i.TableType = rowDatabase["table_type"].ToStr();
                    list.Add(i);
                }

                con.Close();

                result.ConnectionString = connectionString;
                result.Tables = list.OrderBy(t=>t.TableName).ToList();
            }
            catch (Exception e)
            {

            }

            return result;
        }
        public void GetSelectedTableMetaData(DatabaseMetadata databaseMetaData, string selectedTable)
        {


            var builder = new SqlConnectionStringBuilder(databaseMetaData.ConnectionString);
            var con = new SqlConnection(builder.ConnectionString);
            con.Open();

            string[] objArrRestrict;
            objArrRestrict = new string[] { null, null, selectedTable, null };
            DataTable tbl = con.GetSchema(SqlClientMetaDataCollectionNames.Columns, objArrRestrict);

            SqlDataAdapter da = new SqlDataAdapter();

            #region Get Primary Key
            String primaryKey = "";
            DataTable ttt = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from " + selectedTable);
            cmd.Connection = con;
            SqlDataAdapter daa = new SqlDataAdapter();
            daa.SelectCommand = cmd;
            //da.Fill(tl);
            daa.FillSchema(ttt, SchemaType.Mapped);
            primaryKey = DataTableHelper.GetPrimaryKeys(ttt);

            #endregion

            List<TableRowMetaData> TableRowMetaDataList = new List<TableRowMetaData>();
            var selectedTableObj = databaseMetaData.Tables.FirstOrDefault(r => r.DatabaseTableName.Equals(selectedTable, StringComparison.InvariantCultureIgnoreCase));

            if (selectedTableObj != null)
            {
                selectedTableObj.TableRowMetaDataList = TableRowMetaDataList;
                databaseMetaData.SelectedTable = selectedTableObj;
                int i = 0;
                
                foreach (DataRow rowTable in tbl.Rows)
                {
                    String TABLE_CATALOG = rowTable["TABLE_CATALOG"].ToStr();
                    String TABLE_SCHEMA = rowTable["TABLE_SCHEMA"].ToStr();
                    String TABLE_NAME = rowTable["TABLE_NAME"].ToStr();
                    String COLUMN_NAME = rowTable["COLUMN_NAME"].ToStr();
                    String ORDINAL_POSITION = rowTable["ORDINAL_POSITION"].ToStr();
                    String COLUMN_DEFAULT = rowTable["COLUMN_DEFAULT"].ToStr();
                    String IS_NULLABLE = rowTable["IS_NULLABLE"].ToStr();
                    String DATA_TYPE = rowTable["DATA_TYPE"].ToStr();
                    String CHARACTER_MAXIMUM_LENGTH = rowTable["CHARACTER_MAXIMUM_LENGTH"].ToStr();
                    String CHARACTER_OCTET_LENGTH = rowTable["CHARACTER_OCTET_LENGTH"].ToStr();
                    String NUMERIC_PRECISION = rowTable["NUMERIC_PRECISION"].ToStr();
                    String NUMERIC_PRECISION_RADIX = rowTable["NUMERIC_PRECISION_RADIX"].ToStr();
                    String NUMERIC_SCALE = rowTable["NUMERIC_SCALE"].ToStr();
                    String DATETIME_PRECISION = rowTable["DATETIME_PRECISION"].ToStr();
                    String CHARACTER_SET_CATALOG = rowTable["CHARACTER_SET_CATALOG"].ToStr();
                    String CHARACTER_SET_SCHEMA = rowTable["CHARACTER_SET_SCHEMA"].ToStr();
                    String CHARACTER_SET_NAME = rowTable["CHARACTER_SET_NAME"].ToStr();
                    String COLLATION_CATALOG = rowTable["COLLATION_CATALOG"].ToStr();
                    String IS_SPARSE = rowTable["IS_SPARSE"].ToStr();
                    String IS_COLUMN_SET = rowTable["IS_COLUMN_SET"].ToStr();
                    String IS_FILESTREAM = rowTable["IS_FILESTREAM"].ToStr();


                    var k = new TableRowMetaData();
                    k.ColumnName = COLUMN_NAME;
                    k.DataType = DATA_TYPE;
                    k.IsNull = IS_NULLABLE;
                    k.MaxChar = CHARACTER_MAXIMUM_LENGTH;
                    k.DataTypeMaxChar = k.DataType;
                    if (k.DataType.Contains("varchar"))
                    {
                        k.MaxChar = CHARACTER_MAXIMUM_LENGTH.Equals("-1") ? "4000" : CHARACTER_MAXIMUM_LENGTH;
                        k.DataTypeMaxChar = k.DataType + "(" + k.MaxChar + ")";
                    }
                    k.Order = ORDINAL_POSITION.ToInt();
                    k.ID = ++i;
                    k.PrimaryKey = COLUMN_NAME == primaryKey;
                    TableRowMetaDataList.Add(k);
                }

            }
            con.Close();



        }
        public DataSet GetDataSet(string sqlCommand, string connectionString)
        {
            DataSet ds = new DataSet();
            if (!String.IsNullOrEmpty(sqlCommand))
            {


                var queryParts = Regex.Split(sqlCommand, @"\s+").Select(r => r.Trim()).Where(s => !String.IsNullOrEmpty(s)).ToList();
                String sp = queryParts.FirstOrDefault();
                sqlCommand = sqlCommand.Replace(sp, "");


                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sp;
                cmd.CommandType = CommandType.StoredProcedure;

                if (!String.IsNullOrEmpty(sqlCommand))
                {
                    var queryParts2 = Regex.Split(sqlCommand, @",").Select(r => r.Trim()).Where(s => !String.IsNullOrEmpty(s)).ToList();
                    foreach (var item in queryParts2)
                    {
                        var parameterParts = Regex.Split(item, @"=").Select(r => r.Trim()).Where(s => !String.IsNullOrEmpty(s)).ToList();
                        var paraterValue = parameterParts.LastOrDefault().Replace("'", "");
                        var paramterName = parameterParts.FirstOrDefault();
                        if (paraterValue.ToLower().Equals("null", StringComparison.InvariantCultureIgnoreCase))
                        {
                            cmd.Parameters.Add(new SqlParameter(paramterName, DBNull.Value));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter(paramterName, paraterValue));
                        }

                    }
                }

                da.SelectCommand = cmd;


                conn.Open();
                da.Fill(ds);
                conn.Close();

            }
            return ds;
        }
    }
}
