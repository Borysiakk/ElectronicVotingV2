namespace ElectronicVoting.Common
{
    public static class Routes
    {
        public static class PbftConsensusRoutesApi
        {
            public static string Reply = "/api/PbftConsensus/Reply";
            public static string Commit = "/api/PbftConsensus/Commit";
            public static string Preparing = "/api/PbftConsensus/Preparing";
            public static string PrePreparing = "/api/PbftConsensus/PrePreparing";
            
        }
    }
}