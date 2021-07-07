using System;
using System.IO;

namespace VersionAutoIncrement
{
    static class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: VersionAutoIncrement.exe %1");
                return 1;
            }
            
            string filename = args[0];
            
            if (!File.Exists(filename))
            {
                Console.WriteLine("File {0} does not exist.", filename);
                return 1;
            }

            try
            {
                string[] lines = File.ReadAllLines(filename);

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("[assembly: AssemblyVersion"))
                    {
                        IncVersion(ref lines[i]);
                    }
                    else if (lines[i].StartsWith("[assembly: AssemblyFileVersion"))
                    {
                        IncVersion(ref lines[i]);
                    }
                }

                File.WriteAllLines(filename, lines);
                
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                return 1;
            }
        }

        static void IncVersion(ref string Line)
        {
            ushort Major = 0;
            ushort Minor = 0;
            ushort Build = 0;
            ushort Revision = 0;

            string[] VersionString = Line.Split('"');

            if (VersionString.Length != 3)
            {
                Console.WriteLine("Problem with VersionString FieldCount @ " + Line);
                return;
            }
            
            string[] Version = VersionString[1].Split('.');

            if (Version.Length > 0) UInt16.TryParse(Version[0], out Major);
            if (Version.Length > 1) UInt16.TryParse(Version[1], out Minor);
            if (Version.Length > 2) UInt16.TryParse(Version[2], out Build);
            if (Version.Length > 3) UInt16.TryParse(Version[3], out Revision);

            if (Build >= ushort.MaxValue - 1) Console.WriteLine("Max build version ({0}) reached.", Build);
            else Build += 1;
            
            Line = String.Format("{0}\"{1}.{2}.{3}.{4}\"{5}", VersionString[0], Major, Minor, Build, Revision, VersionString[2]);
        }
    }
}
