namespace Schotejil.Clubkampioen.Data.Sql;

public class Ranking
{
    public int Id { get; set; }

    public int? Order { get; set; }

    public int Place { get; set; }

    public int ResultId { get; set; }

    public virtual Result Result { get; set; } = null!;
}