// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

/*
 Credits to this file goes to the C# plugin
 loader & interface examples.
*/
namespace Els_kom_Core.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.IO.Compression;
    using System.Reflection;

    /// <summary>
    /// Generic Els_kom plugin loader.
    /// </summary>
    /// <typeparam name="T">The type to look for when loading plugins.</typeparam>
    internal static class GenericPluginLoader<T>
    {
        /// <summary>
        /// Loads plugins with the specified plugin interface type.
        /// </summary>
        /// <param name="path">The path to look for plugins to load.</param>
        /// <returns>A list of plugins loaded that derive from the specified type.</returns>
        internal static ICollection<T> LoadPlugins(string path)
        {
            string[] dllFileNames = null;
            if (Directory.Exists(path))
            {
                dllFileNames = Directory.GetFiles(path, "*.dll");
            }

            // try to load from a zip as well if plugins are installed in both places.
            var zippath = $"{path}.zip";
            ICollection<T> plugins = new List<T>();
            int.TryParse(SettingsFile.Settingsxml?.TryRead("SaveToZip"), out var saveToZip1);
            var saveToZip = Convert.ToBoolean(saveToZip1);

            // handle when path points to a zip file.
            if (Directory.Exists(path) || File.Exists(zippath))
            {
                ICollection<Assembly> assemblies = new List<Assembly>();
                if (dllFileNames != null)
                {
                    foreach (var dllFile in dllFileNames)
                    {
                        SettingsFile.Settingsxml?.ReopenFile();
                        int.TryParse(SettingsFile.Settingsxml?.TryRead("LoadPDB"), out var tempint);
                        var loadPDB = Convert.ToBoolean(tempint) ? Convert.ToBoolean(tempint) : Debugger.IsAttached;
                        try
                        {
                            var assembly = loadPDB ?
                                Assembly.Load(File.ReadAllBytes(dllFile), File.ReadAllBytes(dllFile.Replace("dll", "pdb"))) :
                                Assembly.Load(File.ReadAllBytes(dllFile));
                            assemblies.Add(assembly);
                        }
                        catch (BadImageFormatException)
                        {
                            // ignore the error and load the other files.
                        }
                    }
                }

                if (saveToZip && File.Exists(zippath))
                {
                    var zipFile = ZipFile.OpenRead(zippath);
                    foreach (var entry in zipFile.Entries)
                    {
                        // just lookup the dlls here. The LoadFromZip method will load the pdb’s if they are deemed needed.
                        if (entry.FullName.EndsWith(".dll"))
                        {
                            SettingsFile.Settingsxml?.ReopenFile();
                            int.TryParse(SettingsFile.Settingsxml?.TryRead("LoadPDB"), out var tempint);
                            var assembly = ZipAssembly.LoadFromZip(zippath, entry.FullName, Convert.ToBoolean(tempint));
                            assemblies.Add(assembly);
                        }
                    }

                    zipFile.Dispose();
                }

                var pluginType = typeof(T);
                ICollection<Type> pluginTypes = new List<Type>();
                foreach (var assembly in assemblies)
                {
                    if (assembly != null)
                    {
                        var types = assembly.GetTypes();
                        foreach (var type in types)
                        {
                            if (type.IsInterface || type.IsAbstract)
                            {
                                continue;
                            }
                            else
                            {
                                if (type.GetInterface(pluginType.FullName) != null)
                                {
                                    pluginTypes.Add(type);
                                }
                            }
                        }
                    }
                }

                foreach (var type in pluginTypes)
                {
                    var plugin = (T)Activator.CreateInstance(type);
                    plugins.Add(plugin);
                }

                return plugins;
            }

            return plugins;
        }
    }
}
