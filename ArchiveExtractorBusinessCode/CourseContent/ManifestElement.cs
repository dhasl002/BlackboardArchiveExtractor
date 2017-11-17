using System;
using System.Collections.Generic;

namespace ArchiveExtractorBusinessCode
{
    public class ManifestElement
    {
        string ReferenceId;
        string Identifier;
        List<ManifestElement> childElements;
    }
}