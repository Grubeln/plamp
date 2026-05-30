using System.Reflection.Emit;
using plamp.Abstractions.Symbols.SymTableBuilding;

namespace plamp.EndToEnd.Tests.Infrastructure;

/// <summary>
/// Эмиттер модуля для e2e тестов
/// </summary>
internal interface IModuleEmitter
{
    /// <summary>
    /// Выполняет эмиссию модуля и возвращает сгенерированный IL
    /// </summary>
    /// <param name="symTable">Таблица символов</param>
    /// <param name="module">Модуль, в который выполняется эмиссия</param>
    /// <returns>IL дамп, сгенерированный во время эмиссии</returns>
    string EmitModule(ISymTableBuilder symTable, ModuleBuilder module);
}