using System.Reflection.Emit;
using plamp.Abstractions.Symbols.SymTableBuilding;
using plamp.ILCodeEmitters;

namespace plamp.EndToEnd.Tests.Infrastructure;

/// <summary>
/// Эмиттер модуля, который перехватывает записи в консоль
/// </summary>
internal sealed class ConsoleCapturingModuleEmitter : IModuleEmitter
{
    /// <summary>
    /// Лок чтобы консоль никто не забрал
    /// </summary>
    private static readonly object EmitLock = new();

    /// <inheritdoc />
    public string EmitModule(ISymTableBuilder symTable, ModuleBuilder module)
    {
        lock (EmitLock)
        {
            var originalOut = Console.Out;
            using var ilWriter = new StringWriter();

            try
            {
                Console.SetOut(ilWriter);

                SymTableEmitter.EmitModule(symTable, module);
                module.CreateGlobalFunctions();

                return ilWriter.ToString();
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }
    }
}