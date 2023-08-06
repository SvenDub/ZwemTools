// <copyright file="LenexParser.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

using System.IO.Compression;
using ZwemTools.Data.Lenex.Xml;

namespace ZwemTools.Data.Lenex;

public class LenexParser
{
    public LenexElement Parse(Stream stream)
    {
        using ZipArchive archive = new(stream, ZipArchiveMode.Read);
        ZipArchiveEntry entry = archive.Entries.First(x => x.Name.EndsWith(".lef"));
        using Stream entryStream = entry.Open();
        XmlSerializer serializer = new(typeof(LenexElement));
        return (LenexElement)serializer.Deserialize(entryStream)!;
    }

    public void Write(LenexElement lenexElement, string file, Stream stream)
    {
        using ZipArchive archive = new(stream, ZipArchiveMode.Create);
        ZipArchiveEntry entry = archive.CreateEntry($"{file}.lef");
        using Stream entryStream = entry.Open();
        XmlSerializer serializer = new(typeof(LenexElement));
        serializer.Serialize(entryStream, lenexElement);
    }
}
