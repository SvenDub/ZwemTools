namespace ZwemTools.Data.Sql;

public record Heat(Guid Id)
{
    required public int Number { get; set; }

    required public int LenexId { get; set; }

    public int? Order { get; set; }

    public virtual ICollection<Entry> Entries { get; set; } = new Collection<Entry>();
}
