using System.Reflection;
using System.Reflection.Emit;

namespace plamp.ILCodeEmitters.ModuleBuilding;

/// <summary>
/// Адаптер обычного <see cref="ModuleBuilder"/> к абстракции эмиттера
/// </summary>
public sealed class ReflectionModuleBuilder(ModuleBuilder module) : IPlampModuleBuilder
{
    /// <inheritdoc />
    public ModuleBuilder Module { get; } = module;

    /// <inheritdoc />
    public TypeBuilder DefineType(string name, TypeAttributes attributes, Type? parent)
    {
        return Module.DefineType(name, attributes, parent);
    }

    /// <inheritdoc />
    public MethodBuilder DefineGlobalMethod(
        string name,
        MethodAttributes attributes,
        CallingConventions callingConvention,
        Type? returnType,
        Type[]? parameterTypes)
    {
        return Module.DefineGlobalMethod(name, attributes, callingConvention, returnType, parameterTypes);
    }

    /// <inheritdoc />
    public MethodBuilder CreateBodyEmissionBuilder(MethodBuilder methodBuilder)
    {
        return methodBuilder;
    }

    /// <inheritdoc />
    public void CreateGlobalFunctions()
    {
        Module.CreateGlobalFunctions();
    }
}
