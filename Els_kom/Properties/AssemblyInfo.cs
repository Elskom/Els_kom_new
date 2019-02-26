// Copyright (c) 2014-2019, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using Elskom.Generic.Libs;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Els_kom")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("f65ed5f5-0986-4ec1-909a-ba7d20eebc61")]

[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Els_kom org.")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("Copyright © 2014-2019")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [Assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.4.9.8")]
[assembly: AssemblyFileVersion("1.4.9.8")]
[assembly: NeutralResourcesLanguage("en-US")]

// use DumpType = MinidumpTypes.Normal for normal mini-dumps.
[assembly: MiniDump(
    "Please send a copy of {0} to https://github.com/Elskom/Els_kom_new/issues by making an issue and attaching the log(s) and mini-dump(s).",
    DumpType = MinidumpTypes.WithDataSegs | MinidumpTypes.WithFullMemory | MinidumpTypes.WithProcessThreadData | MinidumpTypes.WithFullMemoryInfo | MinidumpTypes.WithThreadInfo | MinidumpTypes.WithCodeSegs | MinidumpTypes.WithUnloadedModules | MinidumpTypes.WithTokenInformation | MinidumpTypes.WithModuleHeaders | MinidumpTypes.FilterTriage | MinidumpTypes.WithIptTrace,
    ExceptionTitle = "Unhandled Exception!",
    ThreadExceptionTitle = "Unhandled Thread Exception!")]
