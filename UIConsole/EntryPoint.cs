using UUIConvert;

namespace UUIConverter.Console
{
    // https://twitter.com/unormal/status/1706029651871559897
    internal static class EntryPoint
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            
            //Application.Run(new UIConsole());

            var database = UnityApplicationDatabase.FromFolder(@"G:\Workspace\caves-of-qud-2021");
            var scene = database.LoadScene(@"Assets\Game\Game.unity");
        }
    }
}