namespace ElectronicVoting.API
{
    public static class Routes
    {
        public static class ValidatorApi
        {
            public static string PrePreparing = "/api/PbftConsensus/PrePreparing";
            public static string Preparing = "/api/PbftConsensus/Preparing";
            public static string Commit = "/api/PbftConsensus/Commit";
            public static string Reply = "/api/PbftConsensus/Reply";
        }
    }
}