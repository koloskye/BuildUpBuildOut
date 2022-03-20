using System.IO;
using ICities;


namespace BUBO
{
    /// <summary>
    /// Handles savegame data saving and loading.
    /// </summary>
    public class Serializer : SerializableDataExtensionBase
    {
        // Unique data ID.
        private readonly string dataID = "BUBO";
        public const int DataVersion = 0;


        /// <summary>
        /// Serializes data to the savegame.
        /// Called by the game on save.
        /// </summary>
        public override void OnSaveData()
        {
            base.OnSaveData();

            using (MemoryStream stream = new MemoryStream())
            {
                // Serialise savegame settings.
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    ServiceLimits.Serialize(writer);

                    // Write to savegame.
                    serializableDataManager.SaveData(dataID, stream.ToArray());

                    Logging.Message("wrote ", stream.Length);
                }
            }
        }


        /// <summary>
        /// Deserializes data from a savegame (or initialises new data structures when none available).
        /// Called by the game on load (including a new game).
        /// </summary>
        public override void OnLoadData()
        {
            base.OnLoadData();

            // Read data from savegame.
            byte[] data = serializableDataManager.LoadData(dataID);

            // Check to see if anything was read.
            if (data != null && data.Length != 0)
            {
                // Data was read - go ahead and deserialise.
                using (MemoryStream stream = new MemoryStream(data))
                {
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        // Deserialise savegame settings.
                        ServiceLimits.Deserialize(reader);
                        Logging.Message("read ", stream.Length);
                    }
                }
            }
            else
            {
                // No data read.
                Logging.Message("no data read");
            }
        }
    }
}