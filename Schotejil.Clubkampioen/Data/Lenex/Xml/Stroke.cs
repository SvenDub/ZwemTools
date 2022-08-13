using System.Xml.Serialization;

namespace Schotejil.Clubkampioen.Data.Lenex.Xml;

public enum Stroke
{
    [XmlEnum("APNEA")]
    Apnea,
    [XmlEnum("BACK")]
    Backstroke,
    [XmlEnum("BIFINS")]
    BiFins,
    [XmlEnum("BREAST")]
    Breaststroke,
    [XmlEnum("FLY")]
    Fly,
    [XmlEnum("FREE")]
    Freestyle,
    [XmlEnum("IMMERSION")]
    Immersion,
    [XmlEnum("IMRELAY")]
    IndividualMedleyRelay,
    [XmlEnum("MEDLEY")]
    Medley,
    [XmlEnum("SURFACE")]
    Surface,
    [XmlEnum("UNKNOWN")]
    Unknown,
}