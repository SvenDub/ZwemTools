namespace Schotejil.Clubkampioen.Data.Lenex.Xml;

public enum ResultStatus
{
    [XmlEnum("EXH")]
    Exhibition,
    [XmlEnum("DSQ")]
    Disqualified,
    [XmlEnum("DNS")]
    DidNotStart,
    [XmlEnum("DNF")]
    DidNotFinish,
    [XmlEnum("SICK")]
    Sick,
    [XmlEnum("WDR")]
    Withdrawn,
}