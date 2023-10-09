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
using System.IO;

namespace UUIConvert
{
    public class UnityScene
    {
        public static UnityScene FromFile( string scenePath, UnityApplicationDatabase database )
        {
            if( !File.Exists(scenePath)) throw new FileNotFoundException();

            UnityScene newScene = new UnityScene();

            var input = new StringReader( YAMLFixup.FixBlankKeys( File.ReadAllText(scenePath) ));
            var yaml = new YamlStream();
            try
            {
                yaml.Load(input);

                var sceneRoot = yaml.Documents[0].RootNode;
                ;
            }
            catch( Exception ex )
            {
                Log.Exception($"bad scene {scenePath}", ex);
            }

            return newScene;
        }
    }
}
