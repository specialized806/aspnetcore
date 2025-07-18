// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using System.IO;

namespace Microsoft.Extensions.Validation;

public sealed partial class ValidationsGenerator : IIncrementalGenerator
{
    public static string GeneratedCodeConstructor => $@"global::System.CodeDom.Compiler.GeneratedCodeAttribute(""{typeof(ValidationsGenerator).Assembly.FullName}"", ""{typeof(ValidationsGenerator).Assembly.GetName().Version}"")";
    public static string GeneratedCodeAttribute => $"[{GeneratedCodeConstructor}]";

    internal static void Emit(SourceProductionContext context, (InterceptableLocation? AddValidation, ImmutableArray<ValidatableType> ValidatableTypes) emitInputs)
    {
        if (emitInputs.AddValidation is null)
        {
            // Avoid generating code if no AddValidation call was found.
            return;
        }
        var source = Emit(emitInputs.AddValidation, emitInputs.ValidatableTypes);
        context.AddSource("ValidatableInfoResolver.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private static string Emit(InterceptableLocation addValidation, ImmutableArray<ValidatableType> validatableTypes) => $$"""
#nullable enable annotations
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#nullable enable
#pragma warning disable ASP0029

namespace System.Runtime.CompilerServices
{
    {{GeneratedCodeAttribute}}
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    file sealed class InterceptsLocationAttribute : System.Attribute
    {
        public InterceptsLocationAttribute(int version, string data)
        {
        }
    }
}

namespace Microsoft.Extensions.Validation.Generated
{
    {{GeneratedCodeAttribute}}
    file sealed class GeneratedValidatablePropertyInfo : global::Microsoft.Extensions.Validation.ValidatablePropertyInfo
    {
        public GeneratedValidatablePropertyInfo(
            [param: global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMembers(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.PublicProperties | global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.PublicConstructors)]
            global::System.Type containingType,
            global::System.Type propertyType,
            string name,
            string displayName) : base(containingType, propertyType, name, displayName)
        {
            ContainingType = containingType;
            Name = name;
        }

        [global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMembers(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.PublicProperties | global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.PublicConstructors)]
        internal global::System.Type ContainingType { get; }
        internal string Name { get; }

        protected override global::System.ComponentModel.DataAnnotations.ValidationAttribute[] GetValidationAttributes()
            => ValidationAttributeCache.GetValidationAttributes(ContainingType, Name);
    }

    {{GeneratedCodeAttribute}}
    file sealed class GeneratedValidatableTypeInfo : global::Microsoft.Extensions.Validation.ValidatableTypeInfo
    {
        public GeneratedValidatableTypeInfo(
            [param: global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMembers(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.Interfaces)]
            global::System.Type type,
            ValidatablePropertyInfo[] members) : base(type, members) { }
    }

    {{GeneratedCodeAttribute}}
    file class GeneratedValidatableInfoResolver : global::Microsoft.Extensions.Validation.IValidatableInfoResolver
    {
        public bool TryGetValidatableTypeInfo(global::System.Type type, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out global::Microsoft.Extensions.Validation.IValidatableInfo? validatableInfo)
        {
            validatableInfo = null;
{{EmitTypeChecks(validatableTypes)}}
            return false;
        }

        // No-ops, rely on runtime code for ParameterInfo-based resolution
        public bool TryGetValidatableParameterInfo(global::System.Reflection.ParameterInfo parameterInfo, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out global::Microsoft.Extensions.Validation.IValidatableInfo? validatableInfo)
        {
            validatableInfo = null;
            return false;
        }
    }

    {{GeneratedCodeAttribute}}
    file static class GeneratedServiceCollectionExtensions
    {
        {{addValidation.GetInterceptsLocationAttributeSyntax()}}
        public static global::Microsoft.Extensions.DependencyInjection.IServiceCollection AddValidation(this global::Microsoft.Extensions.DependencyInjection.IServiceCollection services, global::System.Action<global::Microsoft.Extensions.Validation.ValidationOptions>? configureOptions = null)
        {
            // Use non-extension method to avoid infinite recursion.
            return global::Microsoft.Extensions.DependencyInjection.ValidationServiceCollectionExtensions.AddValidation(services, options =>
            {
                options.Resolvers.Insert(0, new GeneratedValidatableInfoResolver());
                if (configureOptions is not null)
                {
                    configureOptions(options);
                }
            });
        }
    }

    {{GeneratedCodeAttribute}}
    file static class ValidationAttributeCache
    {
        private sealed record CacheKey(
            [param: global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMembers(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.PublicProperties | global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.PublicConstructors)]
            [property: global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMembers(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.PublicProperties | global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.PublicConstructors)]
            global::System.Type ContainingType,
            string PropertyName);
        private static readonly global::System.Collections.Concurrent.ConcurrentDictionary<CacheKey, global::System.ComponentModel.DataAnnotations.ValidationAttribute[]> _cache = new();

        public static global::System.ComponentModel.DataAnnotations.ValidationAttribute[] GetValidationAttributes(
            [global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMembers(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.PublicProperties | global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.PublicConstructors)]
            global::System.Type containingType,
            string propertyName)
        {
            var key = new CacheKey(containingType, propertyName);
            return _cache.GetOrAdd(key, static k =>
            {
                var results = new global::System.Collections.Generic.List<global::System.ComponentModel.DataAnnotations.ValidationAttribute>();

                // Get attributes from the property
                var property = k.ContainingType.GetProperty(k.PropertyName);
                if (property != null)
                {
                    var propertyAttributes = global::System.Reflection.CustomAttributeExtensions
                        .GetCustomAttributes<global::System.ComponentModel.DataAnnotations.ValidationAttribute>(property, inherit: true);

                    results.AddRange(propertyAttributes);
                }

                // Check constructors for parameters that match the property name
                // to handle record scenarios
                foreach (var constructor in k.ContainingType.GetConstructors())
                {
                    // Look for parameter with matching name (case insensitive)
                    var parameter = global::System.Linq.Enumerable.FirstOrDefault(
                        constructor.GetParameters(),
                        p => string.Equals(p.Name, k.PropertyName, global::System.StringComparison.OrdinalIgnoreCase));

                    if (parameter != null)
                    {
                        var paramAttributes = global::System.Reflection.CustomAttributeExtensions
                            .GetCustomAttributes<global::System.ComponentModel.DataAnnotations.ValidationAttribute>(parameter, inherit: true);

                        results.AddRange(paramAttributes);

                        break;
                    }
                }

                return results.ToArray();
            });
        }
    }
}
""";

    private static string EmitTypeChecks(ImmutableArray<ValidatableType> validatableTypes)
    {
        var sw = new StringWriter();
        var cw = new CodeWriter(sw, baseIndent: 3);
        foreach (var validatableType in validatableTypes)
        {
            var typeName = validatableType.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            cw.WriteLine($"if (type == typeof({typeName}))");
            cw.StartBlock();
            cw.WriteLine($"validatableInfo = new GeneratedValidatableTypeInfo(");
            cw.Indent++;
            cw.WriteLine($"type: typeof({typeName}),");
            if (validatableType.Members.IsDefaultOrEmpty)
            {
                cw.WriteLine("members: []");
            }
            else
            {
                cw.WriteLine("members: [");
                cw.Indent++;
                foreach (var member in validatableType.Members)
                {
                    EmitValidatableMemberForCreate(member, cw);
                }
                cw.Indent--;
                cw.WriteLine("]");
            }
            cw.Indent--;
            cw.WriteLine(");");
            cw.WriteLine("return true;");
            cw.EndBlock();
        }
        return sw.ToString();
    }

    private static void EmitValidatableMemberForCreate(ValidatableProperty member, CodeWriter cw)
    {
        cw.WriteLine("new GeneratedValidatablePropertyInfo(");
        cw.Indent++;
        cw.WriteLine($"containingType: typeof({member.ContainingType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}),");
        cw.WriteLine($"propertyType: typeof({member.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}),");
        cw.WriteLine($"name: \"{member.Name}\",");
        cw.WriteLine($"displayName: \"{member.DisplayName}\"");
        cw.Indent--;
        cw.WriteLine("),");
    }
}
