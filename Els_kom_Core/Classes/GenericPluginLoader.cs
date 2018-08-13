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
    /// <summary>
    /// Generic Els_kom plugin loader.
    /// </summary>
    internal static class GenericPluginLoader<T>
    {
        /// <summary>
        /// Loads plugins with the specified plugin interface type.
        /// </summary>
        internal static System.Collections.Generic.ICollection<T> LoadPlugins(string path)
        {
            string[] dllFileNames = null;
            if (System.IO.Directory.Exists(path))
            {
                dllFileNames = System.IO.Directory.GetFiles(path, "*.dll");
            }
            else
            {
                // try to load from a zip instead then.
                path += ".zip";
            }
            System.Collections.Generic.ICollection<T> plugins = new System.Collections.Generic.List<T>();
            // handle when path points to a zip file.
            if (System.IO.Directory.Exists(path) || System.IO.File.Exists(path))
            {
                System.Collections.Generic.ICollection<System.Reflection.Assembly> assemblies = new System.Collections.Generic.List<System.Reflection.Assembly>();
                if (dllFileNames != null)
                {
                    foreach(var dllFile in dllFileNames)
                    {
                        var an = System.Reflection.AssemblyName.GetAssemblyName(dllFile);
                        var assembly = System.Reflection.Assembly.Load(an);
                        assemblies.Add(assembly);
                    }
                }
                else
                {
                    var zipFile = System.IO.Compression.ZipFile.OpenRead(path);
                    foreach (var entry in zipFile.Entries)
                    {
                        // just lookup the dlls here. The LoadFromZip method will load the pdb’s if they are deemed needed.
                        if (entry.FullName.EndsWith(".dll"))
                        {
                            SettingsFile.Settingsxml?.ReopenFile();
                            int.TryParse(SettingsFile.Settingsxml?.Read("LoadPDB"), out var tempint);
                            var assembly = AssemblyExtensions.LoadFromZip(path, entry.FullName, System.Convert.ToBoolean(tempint));
                            assemblies.Add(assembly);
                        }
                    }
                    zipFile.Dispose();
                }
                var pluginType = typeof(T);
                System.Collections.Generic.ICollection<System.Type> pluginTypes = new System.Collections.Generic.List<System.Type>();
                foreach(var assembly in assemblies)
                {
                    if(assembly != null)
                    {
                        var types = assembly.GetTypes();
                        foreach(var type in types)
                        {
                            if(type.IsInterface || type.IsAbstract)
                            {
                                continue;
                            }
                            else
                            {
                                if(type.GetInterface(pluginType.FullName) != null)
                                {
                                    pluginTypes.Add(type);
                                }
                            }
                        }
                    }
                }
                foreach(var type in pluginTypes)
                {
                    var plugin = (T)System.Activator.CreateInstance(type);
                    plugins.Add(plugin);
                }
                return plugins;
            }
            return plugins;
        }
    }
}
