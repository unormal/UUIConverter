using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using System.IO;
using VYaml.Serialization;

namespace UUIConvert
{
    public class UnityScene
    {
        Dictionary<string, dynamic> membersByFileId = new Dictionary<string, dynamic>();

        public static UnityScene FromFile( string scenePath, UnityApplicationDatabase database )
        {
            if( !File.Exists(scenePath)) throw new FileNotFoundException();

            UnityScene newScene = new UnityScene();

            var yaml = YamlSerializer.DeserializeMultipleDocuments<dynamic>(File.ReadAllBytes(scenePath));

            foreach( var rawObject in yaml )
            {
                var o = rawObject as Dictionary<object,object>;
                Log.Info(o?.Keys.FirstOrDefault()?.ToString() ?? "<null>");
            }

            return newScene;
        }
    }
}
