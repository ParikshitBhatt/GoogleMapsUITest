namespace GoogleMapsUITest.TestResources
{
    public static class TestData
    {
        // Specific Addresses
        public const string WoogaHQ = "Wooga, Saarbrücker Str. 38, 10405 Berlin, Germany";
        public const string HCLTechHQ = "HCL Technologies Ltd., SEZ Plot No. 3A, Sector 126 | Noida – 201304, India";
        public const string PlaytikaHQ = "Playtika, HaChoslim St 8 Herzliya Pituarch, Israel";
        public const string GoogleHQ = "Googleplex, 1600 Amphitheatre Parkway, Mountain View, CA, USA";
        public const string MetaHQ = "Meta Headquarters, 1 Hacker Wy, Menlo Park, CA 94025, United States";


        // Points of Interest
        public static List<string> PointsOfInterest = new List<string>
        {
            "Brandenburg Gate, Berlin, Germany",
            "Colosseum, Rome, Italy",
            "Statue of Liberty, New York, NY, USA",
            "Taj Mahal, Agra, India"
        };

        public const string QueryWithSpecialChars = "Gaststätte";
        public const string VeryLongAddress = "The incredibly long address of a place that stretches across multiple lines, including additional details and maybe even a small story about its history - Example City, State, Country";
    }
}