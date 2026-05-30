using System;
using System.IO;
using System.Linq;
using System.Text;
using AutoFixture;
using AutoFixture.Kernel;
using plamp.Alternative.Parsing;
using plamp.Alternative.Tokenization;
using plamp.Alternative.Tokenization.Token;

namespace plamp.Alternative.Tests.Parsing;

public class ParserContextCustomization(string toParse) : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is not Type type || type != typeof(ParsingContext)) return new NoSpecimen();
        var fileName = context.Create<string>();
        using var stream = new MemoryStream(Encoding.Unicode.GetBytes(toParse));
        using var reader = new StreamReader(stream, Encoding.Unicode);
        var tokenizationResult = Tokenizer.TokenizeAsync(reader, fileName).Result;
        var translationTable = new TranslationTable();
        tokenizationResult.Sequence.SkipWhiteSpace();
        var result = new ParsingContext(tokenizationResult.Sequence, tokenizationResult.Exceptions, translationTable);
        return result;
    }
}
