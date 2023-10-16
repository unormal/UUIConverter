using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using VYaml;
using VYaml.Serialization;

namespace UUIConvert
{
    public class UnityApplicationDatabase
    {
        public UnityScene LoadScene( string filename )
        {
            return UnityScene.FromFile( Path.Combine(basePath, filename), this );
        }

        string basePath = null;
        Dictionary<string,string> FileToGuid = new();
        Dictionary<string,string> GuidToFile = new();

        public void AddFile( string path, string metaPath )
        {
            //Log.Info($"parsing metadata: {path} {metaPath}");

            try
            {
                var yaml = YamlSerializer.Deserialize<dynamic>(File.ReadAllBytes(metaPath));
                var guid = yaml["guid"];

                FileToGuid.Add(path, guid);
                GuidToFile.Add(guid, path);
            }
            catch( Exception ex )
            {
                Log.Exception($"bad metafile {metaPath}", ex);
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

            newDatabase.basePath = path;
            newDatabase.AddFolder( path );

            return newDatabase;
        }
    }
}
