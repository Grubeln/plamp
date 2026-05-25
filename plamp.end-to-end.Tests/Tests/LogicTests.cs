using plamp.EndToEnd.Tests.Infrastructure;
using Shouldly;

namespace plamp.EndToEnd.Tests.Tests;

/// <summary>
/// Тесты логических операторов и логики
/// </summary>
public class LogicTests
{
    private const string SourceFile = "Logic.plp";

    /// <summary>
    /// Проверяет выражение со скобками, где результат зависит от приоритета группировки вокруг && и ||
    /// </summary>
    [Theory]
    [InlineData(false, false, false, false)]
    [InlineData(false, false, true, true)]
    [InlineData(true, false, true, false)]
    [InlineData(true, true, false, true)]
    public async Task EvaluatesParenthesizedLogic(
        bool first,
        bool second,
        bool third,
        bool expected)
    {
        var program = await PlampE2ERunner.CompileFromCodeForTestsAsync(SourceFile);

        var result = program.Invoke<bool>("parenthesized_logic", first, second, third);

        result.ShouldBe(expected);
    }

    /// <summary>
    /// Проверяет отрицание составного выражения с вложенными скобками
    /// </summary>
    [Theory]
    [InlineData(false, false, false, true)]
    [InlineData(true, false, false, true)]
    [InlineData(true, true, false, false)]
    [InlineData(true, false, true, false)]
    public async Task EvaluatesInvertedParenthesizedLogic(
        bool first,
        bool second,
        bool third,
        bool expected)
    {
        var program = await PlampE2ERunner.CompileFromCodeForTestsAsync(SourceFile);

        var result = program.Invoke<bool>("inverted_parenthesized_logic", first, second, third);

        result.ShouldBe(expected);
    }

    /// <summary>
    /// Проверяет использование составного bool выражения со скобками как условия if
    /// </summary>
    [Theory]
    [InlineData(false, false, false, 0)]
    [InlineData(false, false, true, 42)]
    [InlineData(true, false, false, 0)]
    [InlineData(true, true, false, 42)]
    public async Task UsesLogicExpressionInIfBlock(
        bool first,
        bool second,
        bool third,
        int expected)
    {
        var program = await PlampE2ERunner.CompileFromCodeForTestsAsync(SourceFile);

        var result = program.Invoke<int>("logic_in_if_block", first, second, third);

        result.ShouldBe(expected);
    }

    /// <summary>
    /// Проверяет использование bool переменной и составного bool выражения как условия while.
    /// </summary>
    [Theory]
    [InlineData(0, 0)]
    [InlineData(2, 3)]
    [InlineData(10, 6)]
    public async Task UsesLogicExpressionInWhileBlock(int limit, int expected)
    {
        var program = await PlampE2ERunner.CompileFromCodeForTestsAsync(SourceFile);

        var result = program.Invoke<int>("logic_in_while_block", limit);

        result.ShouldBe(expected);
    }

    /// <summary>
    /// Проверяет, что функция может возвращать составное bool выражение как результат bool типа
    /// </summary>
    [Theory]
    [InlineData(10, false)]
    [InlineData(15, true)]
    [InlineData(20, false)]
    public async Task ReturnsBoolExpression(int value, bool expected)
    {
        var program = await PlampE2ERunner.CompileFromCodeForTestsAsync(SourceFile);

        var result = program.Invoke<bool>("returns_bool_type", value);

        result.ShouldBe(expected);
    }

    /// <summary>
    /// Проверяет запись составного bool выражения в новую переменную и последующий return этой переменной
    /// </summary>
    [Theory]
    [InlineData(0, 1, false)]
    [InlineData(10, 20, true)]
    [InlineData(20, 10, false)]
    public async Task StoresBoolExpressionInVariable(int first, int second, bool expected)
    {
        var program = await PlampE2ERunner.CompileFromCodeForTestsAsync(SourceFile);

        var result = program.Invoke<bool>("stores_bool_expression_in_variable", first, second);

        result.ShouldBe(expected);
    }

    /// <summary>
    /// Проверяет переприсваивание bool переменной результатом нового bool выражения
    /// </summary>
    [Theory]
    [InlineData(10, 20, true)]
    [InlineData(20, 10, false)]
    [InlineData(10, 60, false)]
    public async Task ReassignsBoolExpressionVariable(int first, int second, bool expected)
    {
        var program = await PlampE2ERunner.CompileFromCodeForTestsAsync(SourceFile);

        var result = program.Invoke<bool>("reassigns_bool_expression_variable", first, second);

        result.ShouldBe(expected);
    }
}
