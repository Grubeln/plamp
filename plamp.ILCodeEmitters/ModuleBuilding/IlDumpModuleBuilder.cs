using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using plamp.ILCodeEmitters.EmissionDebug;

namespace plamp.ILCodeEmitters.ModuleBuilding;

/// <summary>
/// Декоратор билдера, который собирает IL дамп сгенерированных функций
/// </summary>
public sealed class IlDumpModuleBuilder(IPlampModuleBuilder inner) : IPlampModuleBuilder
{
    private readonly List<DebugMethodBuilder> _methods = [];

    /// <inheritdoc />
    public ModuleBuilder Module => inner.Module;

    /// <inheritdoc />
    public TypeBuilder DefineType(string name, TypeAttributes attributes, Type? parent)
    {
        return inner.DefineType(name, attributes, parent);
    }

    /// <inheritdoc />
    public MethodBuilder DefineGlobalMethod(
        string name,
        MethodAttributes attributes,
        CallingConventions callingConvention,
        Type? returnType,
        Type[]? parameterTypes)
    {
        return inner.DefineGlobalMethod(name, attributes, callingConvention, returnType, parameterTypes);
    }

    /// <inheritdoc />
    public MethodBuilder CreateBodyEmissionBuilder(MethodBuilder methodBuilder)
    {
        var debugMethodBuilder = new DebugMethodBuilder(methodBuilder);
        _methods.Add(debugMethodBuilder);

        return debugMethodBuilder;
    }

    /// <inheritdoc />
    public void CreateGlobalFunctions()
    {
        inner.CreateGlobalFunctions();
    }

    /// <summary>
    /// Возвращает IL дамп всех функций, сгенерированных через этот декоратор
    /// </summary>
    public string GetIlDump()
    {
        var result = new StringBuilder();
        foreach (var method in _methods)
        {
            result.AppendLine(method.GetIlRepresentation());
        }

        return result.ToString();
    }
}
