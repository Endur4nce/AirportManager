using System.Reflection;
using AirportManager.Objects;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using DataType = AirportManager.Objects.DataType;

namespace AirportManager.Helpers;

public static class Helper
{
    /// <summary>
    /// Получить все названия объектов - таблиц
    /// </summary>
    /// <returns>Перечисление объектов - таблиц</returns>
    public static IEnumerable<string?> GetTableNames() =>
        from assembly in AppDomain.CurrentDomain.GetAssemblies()
        from type in assembly.GetTypes()
        select type.GetCustomAttributes(typeof(TableAttribute), false)
        into attribs
        where attribs.Length > 0
        select attribs[0] as TableAttribute
        into table
        select table.Name;

    /// <summary>
    /// Получить объект по мени таблицы
    /// </summary>
    /// <param name="tableName">Имя таблицы</param>
    /// <returns>Тип объекта - таблицы</returns>
    public static Type? GetClassWithTableName(string tableName) =>
        (from types in AppDomain.CurrentDomain.GetAssemblies()
            from type in types.GetTypes()
            let attribute = type.GetCustomAttributes(typeof(TableAttribute), true)
            where attribute.Length > 0
            let table = attribute[0] as TableAttribute
            where table.Name == tableName
            select type).FirstOrDefault();

    /// <summary>
    /// Получить редактируемые поля объекта - таблицы
    /// </summary>
    /// <param name="tableName">Имя таблицы</param>
    /// <returns>Перечесление редактируемых полей таблицы</returns>
    public static IEnumerable<Input?> GetFields(string tableName) =>
        GetClassWithTableName(tableName)
            ?.GetProperties()
            .Where(e =>
            {
                var attributes = e.GetCustomAttributes(typeof(ColumnAttribute), true);
                return attributes.Length > 0;
            })
            .Select(e => new Input {Text = e.Name, DataType = GetDataTypeWithType(e.PropertyType)})!;

    /// <summary>
    /// Получить редактируемые поля объекта - таблицы
    /// </summary>
    /// <param name="tableName">Тип объекта-таблицы</param>
    /// <returns>Перечесление редактируемых полей таблицы</returns>
    public static IEnumerable<Input?> GetFields(Type selectedClass) =>
        selectedClass
            .GetProperties()
            .Where(e =>
            {
                var attributes = e.GetCustomAttributes(typeof(ColumnAttribute), true);
                return attributes.Length > 0;
            })
            .Select(e => new Input {Text = e.Name, DataType = GetDataTypeWithType(e.PropertyType)})!;

    /// <summary>
    /// Получить тип поля в DataType
    /// </summary>
    /// <param name="type">Тип поля</param>
    /// <returns>Тип поля в DataType</returns>
    private static Objects.DataType GetDataTypeWithType(Type type)
    {
        if (type == typeof(int))
            return DataType.Number;
        if (type == typeof(string))
            return DataType.String;
        return DataType.Datetime;
    }

    /// <summary>
    /// Добавить элемент в таблицу
    /// </summary>
    /// <param name="db">Соединение с бд</param>
    /// <param name="type">Тип объекта-таблицы</param>
    /// <param name="values">Поля для изменения</param>
    public static void InsertElement(DataConnection db, Type type, Dictionary<string, string> values)
    {
        var instance = Activator.CreateInstance(type);
        foreach (var (key, value) in values)
        {
            var t = instance?.GetType();
            var prop = t.GetProperty(key);
            if (prop?.PropertyType == typeof(int))
                prop.SetValue(instance, int.Parse(value), null);
            else if (prop?.PropertyType == typeof(string))
                prop.SetValue(instance, value, null);
            else
                prop?.SetValue(instance, new DateTime(long.Parse(value)), null);
        }

        if (type == typeof(Airplane))
            db.Insert((Airplane) instance!);
        else if (type == typeof(AirplaneBrand))
            db.Insert((AirplaneBrand) instance!);
        else if (type == typeof(Airport))
            db.Insert((Airport) instance!);
        else if (type == typeof(Flight))
        {
            var flight = (Flight) instance!;
            if (flight.DepartureDate < flight.ArrivalDate)
                db.Insert(flight);
            else
            {
                MessageBox.Show("Incorrect date");
                return;
            }
        }

        MessageBox.Show("Object was created");
    }


    /// <summary>
    /// Обновить элемент в таблице
    /// </summary>
    /// <param name="db">Соединение с бд</param>
    /// <param name="type">Тип объекта-таблицы</param>
    /// <param name="values">Поля для изменения</param>
    /// <param name="id">Id зменяемого элемента</param>
    public static void UpdateElement(DataConnection db, Type type, Dictionary<string, string> values, int id)
    {
        var instance = Activator.CreateInstance(type);
        var t = instance.GetType();
        PropertyInfo? prop;
        foreach (var (key, value) in values)
        {
            prop = t.GetProperty(key);
            if (prop?.PropertyType == typeof(int))
                prop.SetValue(instance, int.Parse(value), null);
            else if (prop?.PropertyType == typeof(string))
                prop.SetValue(instance, value, null);
            else
                prop?.SetValue(instance, new DateTime(long.Parse(value)), null);
        }

        prop = t.GetProperty("Id");
        prop.SetValue(instance, id, null);


        if (type == typeof(Airplane))
            db.Update((Airplane) instance);
        else if (type == typeof(AirplaneBrand))
            db.Update((AirplaneBrand) instance);
        else if (type == typeof(Airport))
            db.Update((Airport) instance);
        else if (type == typeof(Flight))
        {
            var flight = (Flight) instance!;
            if (flight.DepartureDate < flight.ArrivalDate)
                db.Update(flight);
            else
            {
                MessageBox.Show("Incorrect date");
                return;
            }
        }

        MessageBox.Show("Объект сохранён");
    }

    /// <summary>
    /// Метод сортирующий строки и числа правильно
    /// </summary>
    public static void SortCompare(object sender, DataGridViewSortCompareEventArgs e)
    {
        if (!double.TryParse(e.CellValue1.ToString(), out var cell1)
            || !double.TryParse(e.CellValue2.ToString(), out var cell2))
            return;

        if (cell1 > cell2)
            e.SortResult = 1;
        else if (cell1 < cell2)
            e.SortResult = -1;
        else
            e.SortResult = 0;

        e.Handled = true;
    }

    /// <summary>
    /// Слияние двух словарей в один
    /// </summary>
    /// <param name="dictionaries">Перечисление словарей, которые необъодмо объединить</param>
    /// <returns>Объединённый словарь</returns>
    public static Dictionary<TKey, TValue> Merge<TKey, TValue>(IEnumerable<Dictionary<TKey, TValue>> dictionaries) where TKey : notnull
    {
        return dictionaries.SelectMany(x => x)
            .ToDictionary(x => x.Key, y => y.Value);
    }
}