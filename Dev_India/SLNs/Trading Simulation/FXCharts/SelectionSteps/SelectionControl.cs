
namespace Common
{

    public enum SelectionSteps
    {
        SELECTION_securities = 10,
        SELECTION_Live_Data = 20,
        SELECTION_Comands = 30,
        SELECTION_Line_Golden_Upper = 40,
        SELECTION_Line_Golden_Lower = 50,
        SELECTION_Line_Lime = 60,
        SELECTION_2Lines_Aqua = 70,
        SELECTION_2Lines_DodgerBlue = 80,
        SELECTION_4Lines_Blue = 90,
        SELECTION_Multiple_Dashed_Lines_White = 100,
        SELECTION_Multiple_Dashed_Lines_RedWhite = 110,
        REVIEW_All_Lines = 120,
        TRADING = 130
    }

    public class SelectionStep
    {
        public static SelectionSteps stringToSelectionStep(string step) {
            SelectionSteps enumStep;
            switch (step)
            {
                case "REVIEW_All_Lines":                            enumStep = SelectionSteps.REVIEW_All_Lines;                            break;
                case "SELECTION_2Lines_Aqua":                       enumStep = SelectionSteps.SELECTION_2Lines_Aqua;                       break;
                case "SELECTION_4Lines_Blue":                       enumStep = SelectionSteps.SELECTION_4Lines_Blue;                       break;
                case "SELECTION_2Lines_DodgerBlue":                 enumStep = SelectionSteps.SELECTION_2Lines_DodgerBlue;                 break;
                case "SELECTION_Comands":                           enumStep = SelectionSteps.SELECTION_Comands;                           break;
                case "SELECTION_Line_Golden_Upper":                 enumStep = SelectionSteps.SELECTION_Line_Golden_Upper;                 break;
                case "SELECTION_Line_Golden_Lower":                 enumStep = SelectionSteps.SELECTION_Line_Golden_Lower;                 break;
                case "SELECTION_Line_Lime":                         enumStep = SelectionSteps.SELECTION_Line_Lime;                         break;
                case "SELECTION_Live_Data":                         enumStep = SelectionSteps.SELECTION_Live_Data;                         break;
                case "SELECTION_Multiple_Dashed_Lines_RedWhite":    enumStep = SelectionSteps.SELECTION_Multiple_Dashed_Lines_RedWhite;    break;
                case "SELECTION_Multiple_Dashed_Lines_White":       enumStep = SelectionSteps.SELECTION_Multiple_Dashed_Lines_White;       break;
                case "SELECTION_securities":                        enumStep = SelectionSteps.SELECTION_securities;                        break;
                case "TRADING":                                     enumStep = SelectionSteps.TRADING; break;
                default:                                            enumStep = SelectionSteps.SELECTION_securities;                        break;
            }
            return enumStep;
        }
    }
}
