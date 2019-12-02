using NeuroXChange.Model;
using NeuroXChange.Model.Database;

namespace NeuroXChange.View.Training
{
    public partial class MarketSentimentSurveyWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private LocalDatabaseConnector localDatabaseConnector;

        public MarketSentimentSurveyWindow(LocalDatabaseConnector localDatabaseConnector)
        {
            this.localDatabaseConnector = localDatabaseConnector;

            InitializeComponent();
        }

        private void btnVariant_Click(object sender, System.EventArgs e)
        {
            UserActionDetail detail = UserActionDetail.NoDetail;
            if (sender == btnVariant1)
                detail = UserActionDetail.SurveyHigh;
            else if (sender == btnVariant2)
                detail = UserActionDetail.SurveyLow;
            else if (sender == btnVariant3)
                detail = UserActionDetail.SurveyAgainst;
            else if (sender == btnVariant4)
                detail = UserActionDetail.SurveyFavor;
            else if (sender == btnVariant5)
                detail = UserActionDetail.SurveyExit;
            else if (sender == btnVariant6)
                detail = UserActionDetail.SurveyEnter;
            else if (sender == btnVariant7)
                detail = UserActionDetail.LongTrade;
            else if (sender == btnVariant8)
                detail = UserActionDetail.ShortTrade;
            else if (sender == btnVariant9)
                detail = UserActionDetail.NoDirection;
            localDatabaseConnector.WriteUserAction(Model.UserAction.MarketSentimentSurveyPressed, detail);
        }
    }
}
