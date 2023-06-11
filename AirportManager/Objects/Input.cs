namespace AirportManager.Objects;

/// <summary>
/// Тип поля
/// </summary>
public enum DataType
{
    String = 0,
    Number = 1,
    Datetime = 2,
} 

/// <summary>
/// Поле объекта
/// </summary>
public class Input
{
    public string Text { get; set; }
    public DataType DataType { get; set; }
}