namespace Schotejil.Clubkampioen.Data.Sql;

[Owned]
public class AgeDate
{
    public AgeDateType Type { get; set; }

    public DateOnly Value { get; set; }
}
