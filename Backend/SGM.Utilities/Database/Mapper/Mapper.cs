using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Orion.Utilities.Database.Mapper {
    /// <summary>
    /// This class is a database utility that assists with mapping data entities to and from an SQL Server database.
    /// Use it to encapsulate database records into a collection of reference-type objects, or write reference-type objects into a database.
    /// </summary>
    public static class Mapper {
        /// <summary>
        /// Asynchronously executes an SQL statement against a specified database.
        /// </summary>
        /// <param name="sql">The SQL statement to be executed.</param>
        /// <param name="connectionString">The connection string to the desired database.</param>
        /// <returns>A Task object representing the asynchronous operation.</returns>
        public static async Task ExecuteSqlAsync(string sql, string connectionString) {
            using (var connection = new SqlConnection()) {
                connection.ConnectionString = connectionString;
                await connection.OpenAsync();

                await new SqlCommand(sql, connection).ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Asynchronously retrieves a collection of strings as a result of an SQL query that returns a single column. 
        /// Note: If the SQL query returns more than one column, only the first one will be returned. For SQL queries that return multiple columns, use method GetResultsAsync instead.
        /// </summary>
        /// <param name="sql">The SQL statement that will retrieve the desired objects.</param>
        /// <param name="connectionString">The connection string to the desired database.</param>
        /// <returns>A collection of strings returned by the specified SQL query. If the query returns no results, an empty list is returned.</returns>
        public static async Task<IEnumerable<string>> GetColumnAsync(string sql, string connectionString) {
            var result = new List<string>();

            using (var connection = new SqlConnection()) {
                connection.ConnectionString = connectionString;
                await connection.OpenAsync();

                var data = await new SqlCommand(sql, connection).ExecuteReaderAsync();

                while (await data.ReadAsync()) {
                    result.Add(data.GetValue(0).ToString());
                }
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves a collection of reference-type objects as a result of an SQL query. Use this method for queries that return multiple columns.
        /// Note: Returning a collection of primitive types is not supported by this method. Use method GetColumn instead.
        /// </summary>
        /// <typeparam name="T">The reference-type that encapsulates the specified SQL query's result set.</typeparam>
        /// <param name="sql">The SQL statement that will retrieve the desired objects.</param>
        /// <param name="connectionString">The connection string to the desired database.</param>
        /// <returns>A collection of reference-type objects returned by the specified SQL query. If the query returns no results, an empty list is returned.</returns>
        public static async Task<IEnumerable<T>> GetResultsAsync<T>(string sql, string connectionString) where T : class, new() {
            using (var connection = new SqlConnection()) {
                connection.ConnectionString = connectionString;
                await connection.OpenAsync();

                var data = await new SqlCommand(sql, connection).ExecuteReaderAsync();

                return await Mapper.MapAsync<T>(data);
            }
        }

        /// <summary>
        /// Asynchronously retrieves a set of records result of an SQL query as a three-dimentional dictionary.
        /// </summary>
        /// <param name="sql">The SQL statement that will retrieve the desired records.</param>
        /// <param name="indexer">The field name by which to index the returning records.</param>
        /// <param name="connectionString">The connection string to the desired database.</param>
        /// <returns>A three-dimentional dictionary result of the defined SQL query. 
        /// The outer dictionary's keys are the record values for the specified indexer field, while the inner dictionary's keys and values are the field name and its corresponding value, respectively, for each returning row.</returns>
        public static async Task<IDictionary<string, IDictionary<string, object>>> GetResultsAsDictionaryAsync(string sql, string indexer, string connectionString) {
            IDictionary<string, IDictionary<string, object>> result = new Dictionary<string, IDictionary<string, object>>(); ;

            using (var connection = new SqlConnection()) {
                connection.ConnectionString = connectionString;
                await connection.OpenAsync();

                var data = await new SqlCommand(sql, connection).ExecuteReaderAsync();

                while (await data.ReadAsync()) {
                    var row = new Dictionary<string, object>();

                    foreach (var column in data.GetColumnSchema()) {
                        var fieldType = column.DataType;

                        if (fieldType == typeof(string))
                            row.Add(column.ColumnName, data[column.ColumnName]);
                        else if (fieldType == typeof(int))
                            row.Add(column.ColumnName, int.Parse(data[column.ColumnName].ToString()));
                        else if (fieldType == typeof(double))
                            row.Add(column.ColumnName, double.Parse(data[column.ColumnName].ToString()));
                        else if (fieldType == typeof(decimal))
                            row.Add(column.ColumnName, decimal.Parse(data[column.ColumnName].ToString()));
                        else if (fieldType == typeof(bool))
                            row.Add(column.ColumnName, bool.Parse(data[column.ColumnName].ToString()));
                        else if (fieldType == typeof(DateTime))
                            row.Add(column.ColumnName, DateTime.Parse(data[column.ColumnName].ToString()));
                        else
                            throw new Exception($"Data Type '{fieldType}' not supported by Mapper.");
                    }

                    result.Add(data[indexer].ToString(), row);
                }
            }

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves a DataTable object containing all the data from a given database table.
        /// </summary>
        /// <param name="sql">The SQL statement that will retrieve the desired data.</param>
        /// <param name="connectionString">The connection string to the database in which the target table lives.</param>
        /// <returns></returns>
        public static async Task<DataTable> GetDataTableFromQueryAsync(string sql, string connectionString) {
            if (string.IsNullOrWhiteSpace(sql))
                throw new Exception("SQL statement cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new Exception("Connection string cannot be null or empty.");

            var result = new DataTable();

            using (SqlConnection connection = new SqlConnection()) {
                connection.ConnectionString = connectionString;
                await connection.OpenAsync();

                var data = await new SqlCommand(sql, connection).ExecuteReaderAsync();

                while (await data.ReadAsync()) {
                    var row = result.NewRow();

                    var fields = data.GetColumnSchema();

                    if (result.Columns == null || result.Columns.Count < 1)
                        result.Columns.AddRange(fields.Select(f => new DataColumn(f.ColumnName)).ToArray());

                    foreach (var field in fields) {
                        row[field.ColumnName] = data[field.ColumnName].ToString();
                    }

                    result.Rows.Add(row);
                }
            }

            return result;
        }

        /// <summary>
        /// Maps the result of a query to the specified custom type, matching the type's properties with returned columns from the result set.
        /// Currently supports only mapping into reference types with primitive property types: string, int, double, float, decimal, bool, DateTime, and its nullables.
        /// </summary>
        /// <typeparam name="T">The type to be mapped into.</typeparam>
        /// <param name="reader">The reader object containing the result set of an executed SQL query.</param>
        /// <returns>A collection of objects of the type defined by T.</returns>
        public static IEnumerable<T> Map<T>(DbDataReader reader) where T : class, new() {
            var result = new List<T>();

            while (reader.Read()) {
                var line = new T();

                foreach (PropertyInfo property in typeof(T).GetProperties()) {
                    try {
                        dynamic newValue;
                        string value = reader[property.Name].ToString();
                        bool isNullable = property.PropertyType.ToString().ToUpper().Contains("NULLABLE");

                        if (property.PropertyType.ToString().ToUpper().Contains("STRING")) {
                            property.SetValue(line, value);
                        }
                        else if (property.PropertyType.ToString().ToUpper().Contains("INT")) {
                            value = string.IsNullOrWhiteSpace(value) ? (isNullable ? null : Activator.CreateInstance(property.PropertyType).ToString()) : value;

                            if (string.IsNullOrWhiteSpace(value) && isNullable)
                                newValue = null;
                            else
                                newValue = int.Parse(value);

                            property.SetValue(line, newValue);
                        }
                        else if (property.PropertyType.ToString().ToUpper().Contains("DOUBLE")) {
                            value = string.IsNullOrWhiteSpace(value) ? (isNullable ? null : Activator.CreateInstance(property.PropertyType).ToString()) : value;

                            if (string.IsNullOrWhiteSpace(value) && isNullable)
                                newValue = null;
                            else
                                newValue = double.Parse(value);

                            property.SetValue(line, newValue);
                        }
                        else if (property.PropertyType.ToString().ToUpper().Contains("FLOAT")) {
                            value = string.IsNullOrWhiteSpace(value) ? (isNullable ? null : Activator.CreateInstance(property.PropertyType).ToString()) : value;

                            if (string.IsNullOrWhiteSpace(value) && isNullable)
                                newValue = null;
                            else
                                newValue = float.Parse(value);

                            property.SetValue(line, newValue);
                        }
                        else if (property.PropertyType.ToString().ToUpper().Contains("DECIMAL")) {
                            value = string.IsNullOrWhiteSpace(value) ? (isNullable ? null : Activator.CreateInstance(property.PropertyType).ToString()) : value;

                            if (string.IsNullOrWhiteSpace(value) && isNullable)
                                newValue = null;
                            else
                                newValue = decimal.Parse(value);

                            property.SetValue(line, newValue);
                        }
                        else if (property.PropertyType.ToString().ToUpper().Contains("BOOL")) {
                            value = string.IsNullOrWhiteSpace(value) ? (isNullable ? null : Activator.CreateInstance(property.PropertyType).ToString()) : value;

                            if (string.IsNullOrWhiteSpace(value) && isNullable)
                                newValue = null;
                            else
                                newValue = bool.Parse(value);

                            property.SetValue(line, newValue);
                        }
                        else if (property.PropertyType.ToString().ToUpper().Contains("DATE")) {
                            value = string.IsNullOrWhiteSpace(value) ? (isNullable ? null : Activator.CreateInstance(property.PropertyType).ToString()) : value;

                            if (string.IsNullOrWhiteSpace(value) && isNullable)
                                newValue = null;
                            else
                                newValue = DateTime.Parse(value);

                            property.SetValue(line, newValue);
                        }
                        else {

                            throw new Exception($"Type {property.PropertyType} not supported by Mapper.");
                        }
                    }
                    catch (Exception e) {
                        throw new Exception($"Property {property.Name} likely does not match the datatype or is not found in query result. \n {e.Message}");
                    }
                }

                result.Add(line);
            }

            return result;
        }

        /// <summary>
        /// Asynchronously maps the result of a query to the specified custom type, matching the type's properties with returned columns from the result set.
        /// Currently supports only mapping into reference types with primitive property types: string, int, double, float, decimal, bool, DateTime, and its nullables.
        /// </summary>
        /// <typeparam name="T">The type to be mapped into.</typeparam>
        /// <param name="reader">The reader object containing the result set of an executed SQL query.</param>
        /// <returns>A collection of objects of the type defined by T.</returns>
        public static async Task<IEnumerable<T>> MapAsync<T>(DbDataReader reader) where T : class, new() {
            var result = new List<T>();

            while (await reader.ReadAsync()) {
                var line = new T();

                foreach (PropertyInfo property in typeof(T).GetProperties()) {
                    try {
                        dynamic newValue;
                        string value = reader[property.Name].ToString();
                        bool isNullable = property.PropertyType.ToString().ToUpper().Contains("NULLABLE");

                        if (property.PropertyType.ToString().ToUpper().Contains("STRING")) {
                            property.SetValue(line, value);
                        }
                        else if (property.PropertyType.ToString().ToUpper().Contains("INT")) {
                            value = string.IsNullOrWhiteSpace(value) ? (isNullable ? null : Activator.CreateInstance(property.PropertyType).ToString()) : value;

                            if (string.IsNullOrWhiteSpace(value) && isNullable)
                                newValue = null;
                            else
                                newValue = int.Parse(value);

                            property.SetValue(line, newValue);
                        }
                        else if (property.PropertyType.ToString().ToUpper().Contains("DOUBLE")) {
                            value = string.IsNullOrWhiteSpace(value) ? (isNullable ? null : Activator.CreateInstance(property.PropertyType).ToString()) : value;

                            if (string.IsNullOrWhiteSpace(value) && isNullable)
                                newValue = null;
                            else
                                newValue = double.Parse(value);

                            property.SetValue(line, newValue);
                        }
                        else if (property.PropertyType.ToString().ToUpper().Contains("FLOAT")) {
                            value = string.IsNullOrWhiteSpace(value) ? (isNullable ? null : Activator.CreateInstance(property.PropertyType).ToString()) : value;

                            if (string.IsNullOrWhiteSpace(value) && isNullable)
                                newValue = null;
                            else
                                newValue = float.Parse(value);

                            property.SetValue(line, newValue);
                        }
                        else if (property.PropertyType.ToString().ToUpper().Contains("DECIMAL")) {
                            value = string.IsNullOrWhiteSpace(value) ? (isNullable ? null : Activator.CreateInstance(property.PropertyType).ToString()) : value;

                            if (string.IsNullOrWhiteSpace(value) && isNullable)
                                newValue = null;
                            else
                                newValue = decimal.Parse(value);

                            property.SetValue(line, newValue);
                        }
                        else if (property.PropertyType.ToString().ToUpper().Contains("BOOL")) {
                            value = string.IsNullOrWhiteSpace(value) ? (isNullable ? null : Activator.CreateInstance(property.PropertyType).ToString()) : value;

                            if (string.IsNullOrWhiteSpace(value) && isNullable)
                                newValue = null;
                            else
                                newValue = bool.Parse(value);

                            property.SetValue(line, newValue);
                        }
                        else if (property.PropertyType.ToString().ToUpper().Contains("DATE")) {
                            value = string.IsNullOrWhiteSpace(value) ? (isNullable ? null : Activator.CreateInstance(property.PropertyType).ToString()) : value;

                            if (string.IsNullOrWhiteSpace(value) && isNullable)
                                newValue = null;
                            else
                                newValue = DateTime.Parse(value);

                            property.SetValue(line, newValue);
                        }
                        else {

                            throw new Exception($"Type {property.PropertyType} not supported by Mapper.");
                        }
                    }
                    catch (Exception e) {
                        throw new Exception($"Property {property.Name} likely does not match the datatype or is not found in query result. \n {e.Message}");
                    }
                }

                result.Add(line);
            }

            return result;
        }

        /// <summary>
        /// Asynchronously verifies whether a table with a given name already exists in the database, and if not, creates one reflecting the specified concrete reference-type's properties.
        /// Note: Currently supports concrete reference-types with primitive-type properties that are: string, int, double, float, decimal, bool and DateTime.
        /// </summary>
        /// <param name="connectionString">The connection string to the desired database.</param>
        /// <param name="tableName">The name of the new table.</param>
        /// <typeparam name="T">The reference-type from which to get properties. The new table will have one field for each property.
        /// If null, the name will be assumed to be the same as the specified type.</typeparam>
        /// <returns>A Task object representing the asynchronous operation.</returns>
        public static async Task CreateTableAsync<T>(string connectionString, string tableName = null) where T: class, new() {
            if (string.IsNullOrWhiteSpace(tableName))
                tableName = typeof(T).Name;

            string fields = string.Empty;

            foreach (var property in typeof(T).GetProperties()) {
                string name = property.Name;
                string type;

                if (property.PropertyType.ToString().ToUpper().Contains("STRING")) {
                    type = "NVARCHAR(MAX)";
                }
                else if (property.PropertyType.ToString().ToUpper().Contains("INT")) {
                    type = "INT";
                }
                else if (property.PropertyType.ToString().ToUpper().Contains("DOUBLE")) {
                    type = "FLOAT";
                }
                else if (property.PropertyType.ToString().ToUpper().Contains("FLOAT")) {
                    type = "FLOAT";
                }
                else if (property.PropertyType.ToString().ToUpper().Contains("DECIMAL")) {
                    type = "DECIMAL";
                }
                else if (property.PropertyType.ToString().ToUpper().Contains("BOOL")) {
                    type = "BIT";
                }
                else if (property.PropertyType.ToString().ToUpper().Contains("DATE")) {
                    type = "DATETIME";
                }
                else {
                    throw new Exception($"Type {property.PropertyType} not supported by Mapper.");
                }

                fields += $" [{name}] {type}, ";
            }

            string sql = $@"IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}')
                            BEGIN
                                CREATE TABLE [{tableName}] (
                                    {fields}
                                )
                            END
                            GO";

            sql = sql.Substring(0, sql.Length - 2);

            using (var connection = new SqlConnection()) {
                connection.ConnectionString = connectionString;
                await connection.OpenAsync();

                await new SqlCommand(sql, connection).ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Asynchronously writes a new record for the specified reference-type object to the database, in the specified table.
        /// Each object property is automatically mapped to the table field of the same name. A property without a matching table field is ignored, and a table field without a matching property is ignored.
        /// </summary>
        /// <typeparam name="T">The reference-type of the object to be written.</typeparam>
        /// <param name="obj">The object to be written.</param>
        /// <param name="connectionString">The connection string to the desired database.</param>
        /// <param name="tableName">The name of the table to write the new record to. If null or empty, the table name is assumed to be the same as the reference-type.</param>
        /// <returns></returns>
        public static async Task WriteAsync<T>(T obj, string connectionString, string tableName = null) {
            if (string.IsNullOrWhiteSpace(tableName))
                tableName = typeof(T).Name;

            using (var connection = new SqlConnection()) {
                var fields = new List<string>();
                var values = new List<string>();
                string fieldsForSQL = string.Empty;
                string valuesForSQL = string.Empty;
                connection.ConnectionString = connectionString;
                await connection.OpenAsync();

                var fieldNames = await new SqlCommand($@"SELECT	COLUMN_NAME
                                                         FROM	INFORMATION_SCHEMA.COLUMNS
                                                         WHERE	TABLE_NAME = '{tableName}'", connection).ExecuteReaderAsync();

                while (await fieldNames.ReadAsync())
                    fields.Add(fieldNames.GetString(0));

                fieldsForSQL = $"[{string.Join("], [", fields)}]";

                foreach (string field in fields) {
                    try {
                        string value = typeof(T).GetProperty(field).GetValue(obj).ToString();

                        if (typeof(T).GetProperty(field).PropertyType == typeof(DateTime)) {
                            var date = DateTime.Parse(value);
                            value = $"(CONVERT(DATETIME, '{date.Year}-{date.Month}-{date.Day} {date.TimeOfDay.Hours}:{date.TimeOfDay.Minutes}:{date.TimeOfDay.Seconds}:{date.TimeOfDay.Milliseconds}'))";
                        }
                        else if (value != null)
                            value = $"'{value}'";

                        values.Add(value);
                    }
                    catch (Exception e) {
                        // LP: Swallow exception. Object does not have a property that matches the field name.
                        values.Add("NULL");
                    }
                }

                valuesForSQL = string.Join(", ", values);

                string sql = $@"INSERT INTO [{tableName}] ({fieldsForSQL}) 
                                VALUES ({valuesForSQL})";

                await new SqlCommand(sql, connection).ExecuteNonQueryAsync();
            }
        }
    }
}
