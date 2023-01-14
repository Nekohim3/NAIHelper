using Newtonsoft.Json;

namespace NAIHelper.Database.UI_Entities.BaseEntities;

[JsonObject]
public abstract class ManyToManyEntity<T1, T2> : Entity where T1 : IdEntity where T2 : IdEntity
{
    public int IdFirst { get; set; }
    public int IdSecond { get; set; }
    public T1? First { get; set; }
    public T2? Second { get; set; }

    protected ManyToManyEntity()
    {

    }

    protected ManyToManyEntity(int idFirst, int idSecond)
    {
        IdFirst = idFirst;
        IdSecond = idSecond;
    }

    protected ManyToManyEntity(T1 first, T2 second)
    {
        First = first;
        Second = second;
        IdFirst = first.Id;
        IdSecond = second.Id;
    }

    public override bool Equals(object? o)
    {
        if (o is not ManyToManyEntity<T1, T2> e)
            return false;
        if (e.IdFirst == 0 || IdFirst == 0 || e.IdSecond == 0 || IdSecond == 0)
            return e.GetHashCode() == GetHashCode();
        return e.IdFirst == IdFirst && e.IdSecond == IdSecond;
    }
}
