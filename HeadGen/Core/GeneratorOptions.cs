﻿using JetBrains.Annotations;

namespace HeadGen.Core;

[PublicAPI]
public class GeneratorOptions
{
    public virtual string[] Header { get; init; } =
    [
        "------------------------------------------------------------------------------",
        "This code was generated by a Hypercube.Generators",
        "File: ${file}",
        "Path: ${path}",
        "------------------------------------------------------------------------------",
    ];

    public virtual string[] Usings { get; init; } = [];
    public virtual string Namespace { get; init; } = string.Empty;
    public virtual string Name { get; init; } = string.Empty;
    public virtual string Path { get; init; } = string.Empty;
    public virtual string File { get; init; } = string.Empty;
    public virtual string Modifiers { get; init; } = string.Empty;
    public virtual string Type { get; init; } = string.Empty;

    public string SourcePath => $"{Path}{Name}.g.cs";
}