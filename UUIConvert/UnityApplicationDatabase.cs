using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using YamlDotNet.RepresentationModel;

namespace UUIConvert
{
    public class UnityApplicationDatabase
    {
        Dictionary<string,string> FileToGuid = new();
        Dictionary<string,string> GuidToFile = new();

        public void AddFile( string path, string metaPath )
        {
            //Log.Info($"parsing metadata: {path} {metaPath}");
            var input = new StringReader(File.ReadAllText(metaPath));
            var yaml = new YamlStream();
            try
            {
                yaml.Load(input);

                var guid = ((YamlMappingNode)yaml.Documents[0].RootNode).Children["guid"].ToString().Trim();
                FileToGuid.Add(path, guid);
                GuidToFile.Add(guid, path);
            }
            catch( Exception ex )
            {
                Log.Exception( $"bad metafile {metaPath}", ex );
            }
        }

        public void AddFolder( string path )
        {
            //Log.Info( $"parsing folder: {path}" );
            foreach( var fileName in Directory.GetFiles(path) )
            {
                if( !fileName.EndsWith(".meta", StringComparison.InvariantCultureIgnoreCase) && File.Exists( fileName + ".meta") )
                {
                    AddFile( fileName, fileName+".meta");
                }
            }

            foreach( var folder in Directory.GetDirectories(path))
            {
                AddFolder(folder);
            }
        }

        public static UnityApplicationDatabase FromFolder( string path )
        {
            UnityApplicationDatabase newDatabase = new UnityApplicationDatabase();

            newDatabase.AddFolder( path );

            return newDatabase;
        }
    }
}
