using System;
using System.Collections.Generic;
using System.Text;

namespace NotesService.Infrastructure.Configurations
{
    // This class maps MongoDB settings from appsettings.json
    public class MongoSettings
    {
        public string ConnectionString { get; set; } = string.Empty;

        public string DatabaseName { get; set; } = string.Empty;

        public string NotesCollectionName { get; set; } = "Notes";
    }
}
