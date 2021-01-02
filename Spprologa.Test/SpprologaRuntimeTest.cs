using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Spprologa.CSProlog;

namespace Spprologa.Test
{
    public class SpprologaRuntimeTest
    {
        [Test]
        public void query_atom_First_ToString_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString(
                "human(socrates).\r\n" +
                "human(aristotle).");

            runtime.query("human(X).").ToString().Is("socrates");
            runtime.query("human(X)").ToString().Is("socrates"); // no period version
        }

        [Test]
        public void query_atom_All_ToString_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString(
                "human(socrates).\r\n" +
                "human(aristotle).");

            runtime.query("human(X).").Select(sln => sln.ToString())
                .Is("socrates", "aristotle");
        }

        [Test]
        public void query_atom_First_RefVal_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString(
                "life(human, aristotle).\r\n" +
                "life(human, socrates).");

            runtime.query("life(Type, Name).")["Name"].Is("aristotle");
        }

        [Test]
        public void query_atom_All_RefVal_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString(
                "life(human, aristotle).\r\n" +
                "life(human, socrates).");

            runtime.query("life(Type, Name).")
                .Select(sln => $"{sln["Name"]} - {sln["Type"]}")
                .Is("aristotle - human", "socrates - human");
        }

        [Test]
        public void query_number_First_ToString_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString(
                "age(bob, 22).\r\n" +
                "age(alice, 24).");

            runtime.query("age(_,X).").ToString().Is("22");
        }

        [Test]
        public void query_number_First_RefVal_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString("age(bob, 22).");
            runtime.query("age(_,X).")["X"].Is(22);
        }

        [Test]
        public void query_quotedatom_First_ToString_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString("human('John Titor').");
            runtime.query("human(X).").ToString().Is("'John Titor'");
        }

        [Test]
        public void query_quotedatom_RefVal_ToString_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString("human('John Titor').");
            runtime.query("human(X).")["X"].Is("'John Titor'");
        }

        [Test]
        public void query_string_First_ToString_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString("human(\"John Titor\").");
            runtime.query("human(Name).").ToString().Is("John Titor");
        }

        [Test]
        public void query_string_First_RefVal_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString("human(\"John Titor\").");
            runtime.query("human(Name).")["Name"].Is("John Titor");
        }

        [Test]
        public void query_Cause_Changes_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString(
                "solved(no).\r\n" +
                "resolve :- retract(solved(_)), asserta(solved(yes)).");

            runtime.query("solved(X).")["X"].Is("no");
            runtime.query("resolve.");
            runtime.query("solved(X).")["X"].Is("yes");
        }

        [Test]
        public void query_Cause_Exception_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString("human(socrates).");
            Assert.Throws<PrologException>(() => runtime.query("human(X")); // Parentheses are not closed.
        }

        [Test]
        public void solved_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString("solved(yes).");

            runtime.solved("solved(yes).").IsTrue();
            runtime.solved("solved(X).").IsTrue();
            runtime.solved("solved(no).").IsFalse();

            // no period version
            runtime.solved("solved(yes)").IsTrue();
            runtime.solved("solved(X)").IsTrue();
            runtime.solved("solved(no)").IsFalse();
        }

        [Test]
        public void solved_Cause_Exception_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString("solved(yes).");
            Assert.Throws<PrologException>(() => runtime.solved("solv ed(yes)")); // Unexpected whitespace
        }

        [Test]
        public void unsolved_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString("solved(yes).");

            runtime.unsolved("solved(yes).").IsFalse();
            runtime.unsolved("solved(X).").IsFalse();
            runtime.unsolved("solved(no).").IsTrue();

            // no period version
            runtime.unsolved("solved(yes)").IsFalse();
            runtime.unsolved("solved(X)").IsFalse();
            runtime.unsolved("solved(no)").IsTrue();
        }

        [Test]
        public void unsolved_Cause_Exception_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString("solved(yes).");
            Assert.Throws<PrologException>(() => runtime.unsolved("solved(no)..")); // too many period
        }

        [Test]
        public async Task then_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString(
                "solved(no).\r\n" +
                "resolve :- retract(solved(_)), asserta(solved(yes)).");

            runtime.query("solved(X).").ToString().Is("no");
            await runtime.then("resolve.").Invoke();
            runtime.query("solved(X).").ToString().Is("yes");
        }

        [Test]
        public async Task then_NoPeriod_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString(
                "solved(no).\r\n" +
                "resolve :- retract(solved(_)), asserta(solved(yes)).");

            runtime.query("solved(X).").ToString().Is("no");
            await runtime.then("resolve").Invoke(); // no period query
            runtime.query("solved(X).").ToString().Is("yes");
        }

        [Test]
        public async Task then_Cause_Exception_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString(
                "solved(no).\r\n" +
                "resolve :- retarct(solved(_)), asserta(solved(yes))."); // spell miss of "retract/1"

            var exception = default(PrologException);
            try { await runtime.then("resolve").Invoke(); }
            catch (PrologException e) { exception = e; }
            exception.IsNotNull();
        }
    }
}