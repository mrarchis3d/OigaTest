using Microsoft.Extensions.Primitives;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Utils;

public static class Patterns
{
    public const string FilterOnlyAlphabet = "[a-zA-Z]+";

    private static char[] MarksToReplace = new char[5] { '\'', '.', ',', '-','\"'}; 
    public static string GetFilteredValue(string value, string pattern)
    {
        var normalizedString = value.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }
        return Regex.Replace(stringBuilder.ToString().ToLower(), pattern, x => x.Value.ToLower());
    }

    public static string GenerateReplacesSQL(List<string> filters, string searchWords)
    {
        StringBuilder fulltext = new StringBuilder();
        for (int j=0; j<filters.Count;j++)
        {
            StringBuilder sb = new StringBuilder();
            for (int i=0; i<MarksToReplace.Length; i++)
            {
                sb.Insert(0, "Replace(");
                if (i == 0)
                {
                    sb.Append($"LOWER({filters[j]})");
                }
                if(MarksToReplace[i]=='\'')
                    sb.Append($", '''', '')");
                else
                    sb.Append( $", '{MarksToReplace[i]}', '')"  );
            }
            if (j != filters.Count - 1)
            {
                fulltext.Append(sb.ToString() + $" LIKE '%' + '{searchWords}' + '%' collate SQL_Latin1_General_CP1_CI_AI OR ");
                continue;
            }
            fulltext.Append(sb.ToString() + $" LIKE '%' + '{searchWords}' + '%' collate SQL_Latin1_General_CP1_CI_AI ");
        }
        return fulltext.ToString();
    }


}
