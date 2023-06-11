using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportManager.Objects;
using LinqToDB.Mapping;

namespace AirportManager
{
    public static class Helper
    {
        public static IEnumerable<string?> GetTableNames() =>
            from assembly in AppDomain.CurrentDomain.GetAssemblies()
            from type in assembly.GetTypes()
            select type.GetCustomAttributes(typeof(TableAttribute), false)
            into attribs
            where attribs.Length > 0
            select attribs[0] as TableAttribute
            into table
            select table.Name;
        public static IEnumerable<Input?> GetFields(string tableName) =>
        GetClassWithTableName(tableName)
            ?.GetProperties()
            .Where(e =>
            {
                var attributes = e.GetCustomAttributes(typeof(ColumnAttribute), true);
                return attributes.Length > 0;
            })
            .Select(e => new Input { Text = e.Name, DataType = GetDataTypeWithType(e.PropertyType) })!;
        public static Type? GetClassWithTableName(string tableName) =>
            (from types in AppDomain.CurrentDomain.GetAssemblies()
             from type in types.GetTypes()
             let attribute = type.GetCustomAttributes(typeof(TableAttribute), true)
             where attribute.Length > 0
             let table = attribute[0] as TableAttribute
             where table.Name == tableName
             select type).FirstOrDefault();
        private static Objects.DataType GetDataTypeWithType(Type type)
        {
            if (type == typeof(int)) return Objects.DataType.Number;
            if (type == typeof(string)) return Objects.DataType.String;
            return Objects.DataType.DateTime;
        }
    }
}
