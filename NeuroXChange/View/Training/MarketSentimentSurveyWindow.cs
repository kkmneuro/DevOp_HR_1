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
            string variantString = null;
            if (sender == btnVariant1)
                variantString = "High";
            else if (sender == btnVariant2)
                variantString = "Low";
            else if (sender == btnVariant3)
                variantString = "Against";
            else if (sender == btnVariant4)
                variantString = "Favor";
            else if (sender == btnVariant5)
                variantString = "Exit";
            else if (sender == btnVariant6)
                variantString = "Enter";
            localDatabaseConnector.WriteUserAction(Model.UserAction.MarketSentimentSurveyPressed, variantString);
        }
    }
}
