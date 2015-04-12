using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Reflection;

using Killhope.Plugins.Manager.Domain.Properties;
using System.Runtime.CompilerServices;

namespace Killhope.Plugins.Manager.Presentation
{
    public class PluginLoader
    {

        //TODO: PM.1 -
        // On a load or refresh operation, the specified folder will be searched for compatible plugins, these will be loaded and the GUI of the Plugin manager will be updated to allow these plugins to be run.
        //If a plugin fails to load, then an error will be shown.



        /// <summary>
        /// Returns a  CompositionContainer containing all loaded parts from assemblies specified in the application configuration.
        /// </summary>
        /// <param name="loadCurrentAssembly"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public CompositionContainer ObtainPlugins(bool loadCurrentAssembly = true)
        {
            //Note that NoInlining is required for Assembly.GetCallingAssembly() to return a reliable and correct result.
            var catalog = new AggregateCatalog();

            Action<ComposablePartCatalog> addCatalog = (c) => catalog.Catalogs.Add(c);

            //Add the assembly containing this file.
            addCatalog(new AssemblyCatalog(typeof(PluginLoader).Assembly));

            //Add the calling assembly.
            if (loadCurrentAssembly) 
                addCatalog(new AssemblyCatalog(Assembly.GetCallingAssembly())); 

            //Adds the plugin directory set in the settings.
            try
            {
                addCatalog(new DirectoryCatalog(Settings.Default.PluginLocation)); 
            }
            catch (ArgumentException e)
            {
                //Invalid characters in the path, most likely due to the settings 
            }
            catch (DirectoryNotFoundException e)
            {

            }
            catch (PathTooLongException e)
            {

            }

            return new CompositionContainer(catalog, CompositionOptions.DisableSilentRejection);
        }
    }
}
