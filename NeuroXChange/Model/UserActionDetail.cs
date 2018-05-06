namespace NeuroXChange.Model
{
    public enum UserActionDetail
    {
        NoDetail = 0,

        // survey
        SurveyHigh = 100,
        SurveyLow = 101,
        SurveyAgainst = 102,
        SurveyFavor = 103,
        SurveyExit = 104,
        SurveyEnter = 105,
        LongTrade = 106,
        ShortTrade = 107,
        NoDirection = 108,

        // Training
        TrainingCompDay = 200,

        // Manual position chosen
        ManualPositionMLS1 = 300,
        ManualPositionMLS2 = 301,
        ManualPositionMSL1 = 302,
        ManualPositionMSL2 = 303,
        ManualPositionSingularLong = 304,
        ManualPositionSingularShort = 305
    }
}
