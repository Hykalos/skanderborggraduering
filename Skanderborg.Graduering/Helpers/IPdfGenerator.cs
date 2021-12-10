namespace Skanderborg.Graduering.Helpers
{
    public interface IPdfGenerator
    {
        Stream Generate(IEnumerable<Member> members, DateTime graduationDate);
    }
}
