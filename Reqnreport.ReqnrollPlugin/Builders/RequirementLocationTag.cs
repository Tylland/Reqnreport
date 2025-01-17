namespace Trafikverket.Strateg.Reqnreport.ReqnrollPlugin.Builders
{
    public class RequirementLocationTag(string tag)
    {
        public const string TagName = "KravSpecifikation";
        public const string Separator = ":";

        public string Location { get; set; } = Parse(tag);

        public static bool Matches(string tag)
        {
            return tag.StartsWith(TagName + Separator);
        }

        public static string Parse(string tag)
        {
            return tag.Replace(TagName + Separator, string.Empty);
        }
    }
}
