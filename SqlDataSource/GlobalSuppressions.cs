// Этот файл используется анализом кода для поддержки атрибутов 
// SuppressMessage, примененных в этом проекте. 
// Подавления на уровне проекта либо не имеют целевого объекта, либо для них задан 
// конкретный объект и область пространства имен, тип, член и т. д.
//
// Чтобы добавить подавление к этому файлу, щелкните правой кнопкой 
// мыши сообщение в списке ошибок, укажите на команду "Подавить сообщения" и выберите вариант 
// "В файле проекта для блокируемых предупреждений".
// Нет необходимости вручную добавлять подавления к этому файлу.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Проверка запросов SQL на уязвимости безопасности", Scope = "member", Target = "SqlDataSource.SqlDataSourcePlug.#SqlSelectTable(System.String,ExtendedTypes.ReportTable&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Проверка запросов SQL на уязвимости безопасности", Scope = "member", Target = "SqlDataSource.SqlDataSourcePlug.#SqlSelectScalar(System.String,System.Object&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Проверка запросов SQL на уязвимости безопасности", Scope = "member", Target = "SqlDataSource.SqlDataSourcePlug.#SqlModifyQuery(System.String,System.Int32&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Scope = "member", Target = "SqlDataSource.IPlug.#SqlSelectTable(System.String,ExtendedTypes.ReportTable&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Scope = "member", Target = "SqlDataSource.IPlug.#SqlModifyQuery(System.String,System.Int32&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Scope = "member", Target = "SqlDataSource.IPlug.#SqlSelectScalar(System.String,System.Object&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1007:UseGenericsWhereAppropriate", Scope = "member", Target = "SqlDataSource.IPlug.#SqlSelectScalar(System.String,System.Object&)")]
