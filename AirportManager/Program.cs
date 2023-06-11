using System.Text;
using AirportManager.Helpers;
using AirportManager.Objects;
using LinqToDB;
using LinqToDB.Data;
using DataType = AirportManager.Objects.DataType;

namespace AirportManager;

internal static class Program
{
    private static DataConnection db;
    private static Form1 form;

    [STAThread]
    static void Main()
    {
        db = new DataConnection(ProviderName.MySql, @"Server=localhost;Database=airportdb;User ID=root;Password=;");

        ApplicationConfiguration.Initialize();
        form = new Form1();
        var tableTames = Helper.GetTableNames();
        // Init selectors
        foreach (var name in tableTames)
            form.View_Select.Items.Add(name);
        foreach (var name in tableTames)
            form.Insert_Select.Items.Add(name);

        // View page init
        Init_InsertPage();
        Init_ViewPage();

        Application.Run(form);
    }

    /// <summary>
    /// Инициализация страницы просмотра объектов
    /// </summary>
    private static void Init_ViewPage()
    {
        form.dataGridView.SortCompare += Helper.SortCompare;
        form.searchTextBox.TextChanged += (_, _) => { LoadTable(); };
        form.dataGridView.CellClick +=
            (_, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0)
                    return;
                var column = form.dataGridView.Columns[e.ColumnIndex];
                var row = form.dataGridView.Rows[e.RowIndex];
                if (row.Cells[0].Value is null)
                    return;
                var id = int.Parse(row.Cells[0].Value.ToString());
                if (column.Name == "deleteBtn")
                    DeleteObject(id);
                else if (column.Name == "editBtn")
                {
                    form.Insert_Select.SelectedItem = form.View_Select.SelectedItem;
                    form.IDUpdate.Value = id;
                    form.tabPage.SelectedIndex = 1;
                }
            };
        form.reloadTableBtn.MouseClick += (_, _) => LoadTable();
        form.View_Select.SelectedIndexChanged += (_, _) =>
        {
            var selectedTable = form.View_Select.SelectedItem.ToString();
            var classType = Helper.GetClassWithTableName(selectedTable);

            LoadViewTable(classType);
        };
    }

    /// <summary>
    /// Метод удаляющий в бд
    /// </summary>
    /// <param name="id">Объекта который нужно удалить</param>
    private static void DeleteObject(int id)
    {
        var selectedTable = form.View_Select.SelectedItem.ToString();
        var classType = Helper.GetClassWithTableName(selectedTable);
        var count = 0;
        if (classType == typeof(Airplane))
            count = db.GetTable<Airplane>()
                .Where(e => e.Id == id)
                .Delete();
        else if (classType == typeof(AirplaneBrand))
            count = db.GetTable<AirplaneBrand>()
                .Where(e => e.Id == id)
                .Delete();
        else if (classType == typeof(Airport))
            count = db.GetTable<Airport>()
                .Where(e => e.Id == id)
                .Delete();
        else if (classType == typeof(Flight))
            count = db.GetTable<Flight>()
                .Where(e => e.Id == id)
                .Delete();
        MessageBox.Show(count == 0 ? $"Объект с id={id} не найден" : "Объект удалён");
        if (count != 0)
            LoadViewTable(classType);
    }

    /// <summary>
    /// Инициализация страницы вставки/сохранения
    /// </summary>
    private static void Init_InsertPage()
    {
        form.Insert_Select.SelectedIndexChanged += (_, _) =>
        {
            var selectedTable = form.Insert_Select.SelectedItem.ToString();
            RenderFields(Helper.GetFields(selectedTable));
        };
        form.IDUpdate.ValueChanged += (_, _) =>
        {
            if (form.Insert_Select.SelectedItem == null)
                return;
            var selectedTable = form.Insert_Select.SelectedItem.ToString();
            RenderFields(Helper.GetFields(selectedTable));
            var classType = Helper.GetClassWithTableName(selectedTable);
            var id = form.IDUpdate.Value;
            var inputs = Helper.GetFields(classType);
            var updatePage = form.tabPage.Controls[1];
            object? obj = null;
            if (typeof(Airplane) == classType)
                obj = db.GetTable<Airplane>().FirstOrDefault(e => e.Id == id);
            if (typeof(AirplaneBrand) == classType)
                obj = db.GetTable<AirplaneBrand>().FirstOrDefault(e => e.Id == id);

            if (typeof(Airport) == classType)
                obj = db.GetTable<Airport>().FirstOrDefault(e => e.Id == id);

            if (typeof(Flight) == classType)
                obj = db.GetTable<Flight>().FirstOrDefault(e => e.Id == id);

            if (obj is null)
                return;
            foreach (var inp in inputs)
            {
                var prop = obj.GetType().GetProperty(inp.Text);
                var value = prop?.GetValue(obj, null);
                if (value is null)
                    continue;

                if (inp.DataType == DataType.Number)
                {
                    updatePage
                        .Controls
                        .OfType<NumericUpDown>()
                        .FirstOrDefault(n => n.Name.Substring(5) == inp.Text)!
                        .Value = (int) (value ?? 0);
                }

                if (inp.DataType == DataType.String)
                {
                    updatePage
                        .Controls
                        .OfType<TextBox>()
                        .FirstOrDefault(n => n.Name.Substring(5) == inp.Text)!
                        .Text = (string) value!;
                }
            }
        };
    }

    /// <summary>
    /// Отрисовка полей объекта для вставки/добавления
    /// </summary>
    /// <param name="inputs">Поля объекта</param>
    private static void RenderFields(IEnumerable<Input?> inputs)
    {
        const int space = 70;
        var i = 0;

        foreach (var item in form.tabPage.Controls[1].Controls.OfType<Label>().ToList())
        {
            if (item.Name == "IDUpdateLabel")
                continue;
            form.tabPage.Controls[1].Controls.Remove(item);
        }

        foreach (var item in form.tabPage.Controls[1].Controls.OfType<TextBox>().ToList())
            form.tabPage.Controls[1].Controls.Remove(item);

        foreach (var item in form.tabPage.Controls[1].Controls.OfType<DateTimePicker>().ToList())
            form.tabPage.Controls[1].Controls.Remove(item);

        foreach (var item in form.tabPage.Controls[1].Controls.OfType<NumericUpDown>().ToList())
        {
            if (item.Name == "IDUpdate")
                continue;
            form.tabPage.Controls[1].Controls.Remove(item);
        }

        form.tabPage.Controls[1].Controls.RemoveByKey("InsertSaveBTN");
        form.tabPage.Controls[1].Controls.RemoveByKey("InsertBTN");

        foreach (var inp in inputs)
        {
            var label = new Label
            {
                Text = inp.Text,
                Location = new Point(4, 60 + i * space),
                Name = $"label{inp.Text}",
                AutoSize = true,
            };
            Control input = new();
            switch (inp.DataType)
            {
                case DataType.String:
                    input = new TextBox
                    {
                        Text = "",
                        Location = new Point(4, 85 + i * space),
                        Name = $"input{inp.Text}",
                        AutoSize = true,
                        Width = 250
                    };
                    break;
                case DataType.Number:
                    input = new NumericUpDown
                    {
                        Location = new Point(4, 85 + i * space),
                        Name = $"input{inp.Text}",
                        AutoSize = true,
                        Width = 250,
                        Minimum = Decimal.MinValue,
                        Maximum = Decimal.MaxValue,
                    };
                    break;
                case DataType.Datetime:
                    input = new DateTimePicker
                    {
                        Location = new Point(4, 85 + i * space),
                        Name = $"input{inp.Text}",
                        AutoSize = true,
                        Width = 250,
                        Format = DateTimePickerFormat.Custom,
                        CustomFormat = "dd.MM.yyyy HH:mm",
                        ShowUpDown = true
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            form.tabPage.Controls[1].Controls.Add(label);
            form.tabPage.Controls[1].Controls.Add(input);
            i++;
        }

        var button = new Button
        {
            AutoSize = true,
            Text = "Добавить",
            Location = new Point(4, 60 + i * space),
            Name = "InsertSaveBTN",
        };
        var editBtn = new Button
        {
            AutoSize = true,
            Text = "Изменить",
            Location = new Point(100, 60 + i * space),
            Name = "InsertBTN",
        };
        button.MouseClick += (_, _) => InsertSaveClicked();
        editBtn.MouseClick += (_, _) => SaveClicked();
        form.tabPage.Controls[1].Controls.Add(button);
        form.tabPage.Controls[1].Controls.Add(editBtn);
    }


    private static void InsertSaveClicked()
    {
        var data = Helper.Merge(new[]
        {
            GetUpdateDataTextBox(),
            GetUpdateDataNumeric(),
            GetUpdateDataDateTime(),
        });
        foreach (var (key, value) in data)
        {
            if (value.Length != 0) continue;
            MessageBox.Show($"Поле \"{key}\" не заполнено");
            return;
        }

        var selectedTable = form.Insert_Select.SelectedItem.ToString();
        var type = Helper.GetClassWithTableName(selectedTable);
        Helper.InsertElement(db, type, data);
        LoadTable();
    }

    /// <summary>
    /// Событие при сохранении формы
    /// </summary>
    private static void SaveClicked()
    {
        var data = Helper.Merge(new[]
        {
            GetUpdateDataTextBox(),
            GetUpdateDataNumeric(),
            GetUpdateDataDateTime(),
        });
        foreach (var (key, value) in data)
        {
            if (value.Length != 0) continue;
            MessageBox.Show($"Поле \"{key}\" не заполнено");
            return;
        }

        var selectedTable = form.Insert_Select.SelectedItem.ToString();
        var type = Helper.GetClassWithTableName(selectedTable);
        Helper.UpdateElement(db, type, data, (int) form.IDUpdate.Value);
        LoadTable();
    }

    /// <summary>
    /// Обновить таблицу просмотра обхектов
    /// </summary>
    private static void LoadTable()
    {
        if (form.View_Select.SelectedItem is null)
            return;
        var selectedTable = form.View_Select.SelectedItem.ToString();
        if (selectedTable is null)
            return;
        var classType = Helper.GetClassWithTableName(selectedTable);
        if (classType is null)
            return;
        LoadViewTable(classType);
    }

    /// <summary>
    /// Получить строковые данные из формы
    /// </summary>
    /// <returns>Словарь key - имя поля, value - значение</returns>
    private static Dictionary<string, string> GetUpdateDataTextBox() =>
        form.tabPage.Controls[1].Controls
            .OfType<TextBox>()
            .ToList()
            .ToDictionary(item => item.Name[5..], item => item.Text);

    /// <summary>
    /// Получить числовые данные из формы
    /// </summary>
    /// <returns>Словарь key - имя поля, value - значение</returns>
    private static Dictionary<string, string> GetUpdateDataNumeric() =>
        form.tabPage.Controls[1].Controls
            .OfType<NumericUpDown>()
            .Where(e => e.Name != "IDUpdate")
            .ToList()
            .ToDictionary(item => item.Name[5..], item => item.Value.ToString());

    /// <summary>
    /// Получить данные из формы формата - дата
    /// </summary>
    /// <returns>Словарь key - имя поля, value - значение</returns>
    private static Dictionary<string, string> GetUpdateDataDateTime() =>
        form.tabPage.Controls[1].Controls
            .OfType<DateTimePicker>()
            .ToList()
            .ToDictionary(item => item.Name[5..], item => (item.Value.Date + item.Value.TimeOfDay).Ticks.ToString());


    /// <summary>
    /// Отрисовать таблицу с объектами
    /// </summary>
    /// <param name="classType">Тип объекта для отрисовки</param>
    private static void LoadViewTable(Type classType)
    {
        form.dataGridView.Columns.Clear();
        var fields = new List<Input?> {new() {Text = "Id", DataType = DataType.Number}};
        fields.AddRange(Helper.GetFields(classType));
        foreach (var inp in fields)
            form.dataGridView.Columns.Add(inp.Text, inp.Text);
        form.dataGridView.Columns.Add(new DataGridViewButtonColumn
        {
            Name = "editBtn",
            HeaderText = "Изменить",
            Text = "Изменить",
            UseColumnTextForButtonValue = true
        });
        form.dataGridView.Columns.Add(new DataGridViewButtonColumn
        {
            Name = "deleteBtn",
            HeaderText = "Удалить",
            Text = "Удалить",
            UseColumnTextForButtonValue = true
        });
        var objects = new List<object>();
        if (classType == typeof(Airplane))
            objects = db.GetTable<Airplane>()
                .Cast<object>()
                .ToList();
        else if (classType == typeof(AirplaneBrand))
            objects = db.GetTable<AirplaneBrand>()
                .ToList()
                .Cast<object>()
                .ToList();
        else if (classType == typeof(Airport))
            objects = db.GetTable<Airport>()
                .ToList()
                .Cast<object>()
                .ToList();
        else if (classType == typeof(Flight))
            objects = db.GetTable<Flight>()
                .ToList()
                .Cast<object>()
                .ToList();

        foreach (var obj in objects)
        {
            var values = new List<object>();
            var str = new StringBuilder();
            foreach (var inp in fields)
            {
                var prop = obj.GetType().GetProperty(inp.Text);
                var value = prop?.GetValue(obj, null);
                if (value is null)
                    continue;
                var data = value.ToString()!;
                values.Add(data);
                str.Append($"{data} ");
            }

            if (form.searchTextBox.Text.Length == 0
                || str.ToString().ToLower().Contains(form.searchTextBox.Text.ToLower()))
                form.dataGridView.Rows.Add(values.ToArray());
        }
    }
}