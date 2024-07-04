namespace Accommodations.API.Configurations
{
    public class SerilogSettings
    {
        public MinimumLevelSettings MinimumLevel { get; set; }
        public WriteToSettings[] WriteTo { get; set; }

        public SerilogSettings(MinimumLevelSettings minimumLevel, WriteToSettings[] writeTo)
        {
            MinimumLevel = minimumLevel ?? throw new ArgumentNullException(nameof(minimumLevel));
            WriteTo = writeTo ?? throw new ArgumentNullException(nameof(writeTo));
        }

        public SerilogSettings() // Parameterless constructor for deserialization
        {
            MinimumLevel = new MinimumLevelSettings();
            WriteTo = Array.Empty<WriteToSettings>();
        }

        public class MinimumLevelSettings
        {
            public Dictionary<string, string> Override { get; set; }

            public MinimumLevelSettings(Dictionary<string, string> overrideDict)
            {
                Override = overrideDict ?? throw new ArgumentNullException(nameof(overrideDict));
            }

            public MinimumLevelSettings() // Parameterless constructor for deserialization
            {
                Override = new Dictionary<string, string>();
            }
        }

        public class WriteToSettings
        {
            public string Name { get; set; }
            public ArgsSettings Args { get; set; }

            public WriteToSettings(string name, ArgsSettings args)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                Args = args ?? throw new ArgumentNullException(nameof(args));
            }

            public WriteToSettings() // Parameterless constructor for deserialization
            {
                Name = string.Empty;
                Args = new ArgsSettings();
            }

            public class ArgsSettings
            {
                public string OutputTemplate { get; set; }
                public string Path { get; set; }
                public string RollingInterval { get; set; }
                public bool RollOnFileSizeLimit { get; set; }
                public string Formatter { get; set; }

                public ArgsSettings(string outputTemplate, string path, string rollingInterval, bool rollOnFileSizeLimit, string formatter)
                {
                    OutputTemplate = outputTemplate ?? throw new ArgumentNullException(nameof(outputTemplate));
                    Path = path ?? throw new ArgumentNullException(nameof(path));
                    RollingInterval = rollingInterval ?? throw new ArgumentNullException(nameof(rollingInterval));
                    RollOnFileSizeLimit = rollOnFileSizeLimit;
                    Formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
                }

                public ArgsSettings() // Parameterless constructor for deserialization
                {
                    OutputTemplate = string.Empty;
                    Path = string.Empty;
                    RollingInterval = string.Empty;
                    RollOnFileSizeLimit = false;
                    Formatter = string.Empty;
                }
            }
        }
    }
}
