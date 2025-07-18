namespace Neomaster.R7Client.App;

/// <summary>
/// Постоянные значения и справочные данные для работы с API "Р7-Офис. Сервер документов".
/// </summary>
public class R7ClientConsts
{
  /// <summary>
  /// Размеры страниц документа.
  /// </summary>
  public static class DocPageSize
  {
    /// <summary>
    /// Размер страницы формата A4.
    /// </summary>
    public static class A4
    {
      /// <summary>
      /// Ширина страницы формата A4.
      /// </summary>
      public const string Width = "210mm";

      /// <summary>
      /// Высота страницы формата A4.
      /// </summary>
      public const string Height = "297mm";
    }
  }

  /// <summary>
  /// Внешние отступы страниц документа.
  /// </summary>
  public static class DocPageMargins
  {
    /// <summary>
    /// Внешние отступы страницы формата A4.
    /// </summary>
    public static class A4
    {
      /// <summary>
      /// Отступ слева страницы формата A4.
      /// </summary>
      public const string Left = "17.8mm";

      /// <summary>
      /// Отступ справа страницы формата A4.
      /// </summary>
      public const string Right = "17.8mm";

      /// <summary>
      /// Отступ сверху страницы формата A4.
      /// </summary>
      public const string Top = "19.1mm";

      /// <summary>
      /// Отступ снизу страницы формата A4.
      /// </summary>
      public const string Bottom = "19.1mm";
    }
  }
}
