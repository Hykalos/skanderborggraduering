namespace Skanderborg.Graduering.Profiles;

public class MemberProfile : Profile
{
    public MemberProfile()
    {
        CreateMap<CsvMember, Member>();
    }
}
