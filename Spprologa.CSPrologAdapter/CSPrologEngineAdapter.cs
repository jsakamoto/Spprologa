using Prolog;

namespace Spprologa.CSProlog
{
    public class CSPrologEngineAdapter : IPrologEngine
    {
        private readonly PrologEngine _PrologEngine = new PrologEngine(persistentCommandHistory: false);

        public void ConsultFromString(string prologCode)
        {
            _PrologEngine.ConsultFromString(prologCode);
        }

        public ISolutionCollection Query(string query)
        {
            _PrologEngine.Query = query;
            return new CSPrologSolutionCollection(_PrologEngine.GetEnumerator());
        }
    }
}
