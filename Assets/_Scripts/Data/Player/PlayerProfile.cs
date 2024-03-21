namespace Scripts.Data.Player
{
    public class PlayerProfile
    {
        //==========================
        //=====SINGLETON
        //==========================

        private static PlayerProfile instance;
        public static PlayerProfile GetInstance()
        {
            if (instance == null) 
            { 
                instance = new PlayerProfile(); 
            }
            return instance;
        }

        //==========================
        //=====PROFILE
        //==========================

        private string playerUsername;

        private SettingsData playerSettings;
        public SettingsData Settings { get { return playerSettings; } }

        private ContactsData playerContacts;
        public ContactsData Contacts { get { return playerContacts; } }

        private PicturesData playerPictures;
        public PicturesData Pictures { get { return playerPictures; } }

        public void Initialize(string userKey, string settingsKey, string contactsFile, string contactsPregen, string picturesFile, string picturesPregen, int picturesCount)
        {
            playerUsername = userKey;

            playerSettings = new SettingsData(userKey+settingsKey);
            playerContacts = new ContactsData(userKey+contactsFile, contactsPregen);
            playerPictures = new PicturesData(userKey+picturesFile, picturesPregen, picturesCount);

            playerSettings.LoadPrefs();
        }

        public void SaveData()
        {
            playerSettings.SavePrefs();
            playerContacts.SaveData();
        }

        public void LoadData()
        {
            playerSettings.LoadPrefs();
            playerContacts.LoadData();
            playerPictures.LoadData();
        }

    }
}

