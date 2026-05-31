using System.Reflection;
using System.Reflection.Emit;

namespace plamp.ILCodeEmitters.ModuleBuilding;

/// <summary>
/// Абстракция над <see cref="ModuleBuilder"/>, позволяющая добавлять опциональные декораторы вокруг эмиссии модуля
/// </summary>
public interface IPlampModuleBuilder
{
    /// <summary>
    /// Исходный модуль
    /// </summary>
    ModuleBuilder Module { get; }

    /// <summary>
    /// Определяет тип в модуле
    /// </summary>
    /// <param name="name">Имя типа</param>
    /// <param name="attributes">Атрибуты типа</param>
    /// <param name="parent">Базовый тип</param>
    TypeBuilder DefineType(string name, TypeAttributes attributes, Type? parent);

    /// <summary>
    /// Определяет глобальную функцию в модуле
    /// </summary>
    /// <param name="name">Имя функции</param>
    /// <param name="attributes">Атрибуты функции</param>
    /// <param name="callingConvention">Соглашение вызова</param>
    /// <param name="returnType">Возвращаемый тип</param>
    /// <param name="parameterTypes">Типы параметров</param>
    MethodBuilder DefineGlobalMethod(
        string name,
        MethodAttributes attributes,
        CallingConventions callingConvention,
        Type? returnType,
        Type[]? parameterTypes);

    /// <summary>
    /// Создаёт билдер, который будет использован только для эмиссии тела метода
    /// </summary>
    /// <param name="methodBuilder">Реальный билдер метода, сохранённый в таблице символов</param>
    MethodBuilder CreateBodyEmissionBuilder(MethodBuilder methodBuilder);

    /// <summary>
    /// Завершает создание глобальных функций модуля
    /// </summary>
    void CreateGlobalFunctions();
}
