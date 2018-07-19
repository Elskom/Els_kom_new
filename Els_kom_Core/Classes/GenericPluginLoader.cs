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
            // handle when path points to a zip file.
            if (System.IO.Directory.Exists(path) || System.IO.File.Exists(path))
            {
                System.Collections.Generic.ICollection<System.Reflection.Assembly> assemblies = new System.Collections.Generic.List<System.Reflection.Assembly>();
                if (dllFileNames != null)
                {
                    foreach(string dllFile in dllFileNames)
                    {
                        System.Reflection.AssemblyName an = System.Reflection.AssemblyName.GetAssemblyName(dllFile);
                        System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(an);
                        assemblies.Add(assembly);
                    }
                }
                else
                {
                    System.IO.Compression.ZipArchive zipFile = System.IO.Compression.ZipFile.OpenRead(path);
                    foreach (var entry in zipFile.Entries)
                    {
                        // just lookup the dlls here. The LoadFromZip method will load the pdb’s if they are deemed needed.
                        if (entry.FullName.EndsWith(".dll"))
                        {
                            SettingsFile.Settingsxml.ReopenFile();
                            int.TryParse(SettingsFile.Settingsxml.Read("LoadPDB"), out int tempint);
                            System.Reflection.Assembly assembly = AssemblyExtensions.LoadFromZip(path, entry.FullName, System.Convert.ToBoolean(tempint));
                            assemblies.Add(assembly);
                        }
                    }
                    zipFile.Dispose();
                }
                System.Type pluginType = typeof(T);
                System.Collections.Generic.ICollection<System.Type> pluginTypes = new System.Collections.Generic.List<System.Type>();
                foreach(System.Reflection.Assembly assembly in assemblies)
                {
                    if(assembly != null)
                    {
                        System.Type[] types = assembly.GetTypes();
                        foreach(System.Type type in types)
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
                System.Collections.Generic.ICollection<T> plugins = new System.Collections.Generic.List<T>(pluginTypes.Count);
                foreach(System.Type type in pluginTypes)
                {
                    T plugin = (T)System.Activator.CreateInstance(type);
                    plugins.Add(plugin);
                }
                return plugins;
            }
            return null;
        }
    }
}
