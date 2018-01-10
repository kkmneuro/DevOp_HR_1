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

        private void btnVariant1_Click(object sender, System.EventArgs e)
        {
            localDatabaseConnector.WriteUserAction(Model.UserAction.MarketSentimentSurveyPressed, "Afraid");
        }

        private void btnVariant2_Click(object sender, System.EventArgs e)
        {
            localDatabaseConnector.WriteUserAction(Model.UserAction.MarketSentimentSurveyPressed, "Excited");
        }
    }
}
