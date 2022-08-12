using System.Xml.Serialization;

namespace Schotejil.Clubkampioen.Data.Lenex.Xml;

public enum AgeDateType
{
    [XmlEnum("YEAR")]
    Year,
    [XmlEnum("DATE")]
    Date,
    [XmlEnum("POR")]
    Por,
    [XmlEnum("CAN.FNQ")]
    CanFnq,
    [XmlEnum("LUX")]
    Lux,
}
