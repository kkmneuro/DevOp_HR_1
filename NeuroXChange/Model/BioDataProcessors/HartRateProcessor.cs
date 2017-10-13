using NeuroXChange.Model.BioData;

namespace NeuroXChange.Model.BioDataProcessors
{
    public class HartRateProcessor : BioDataProcessor
    {
        public HartRateProcessor(MainNeuroXModel mainNeuroXModel) : base(mainNeuroXModel)
        {
        }

        public override void OnNext(Sub_Component_Protocol_Psychophysiological_Session_Data_TPS data)
        {

        }
    }
}
