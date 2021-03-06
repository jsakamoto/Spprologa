﻿using NUnit.Framework;
using Spprologa.CSProlog;

namespace Spprologa.Test
{
    public class FactBinderTest
    {
        [Test]
        public void AsString_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString("name(\"John Titor\").");
            var binder = new FactBinder(runtime, "name({0}).");

            binder.as_string.Is("John Titor");
            runtime.query("name(X).").ToString().Is("John Titor");

            binder.as_string = "John Connor";

            binder.as_string.Is("John Connor");
            runtime.query("name(X).").ToString().Is("John Connor");
        }

        [Test]
        public void AsString_NoPeriod_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString("name(\"John Titor\").");
            var binder = new FactBinder(runtime, "name({0})"); // no period

            binder.as_string.Is("John Titor");
            runtime.query("name(X).").ToString().Is("John Titor");

            binder.as_string = "John Connor";

            binder.as_string.Is("John Connor");
            runtime.query("name(X).").ToString().Is("John Connor");
        }

        [Test]
        public void AsInt_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString("age(\"John Titor\", 34).");
            var binder = new FactBinder(runtime, "age(_, {0}).");

            binder.as_int.Is(34);
            runtime.query("age(_, N).")["N"].Is(34);

            binder.as_int = 35;

            binder.as_int.Is(35);
            runtime.query("age(_, N).")["N"].Is(35);
        }

        [Test]
        public void AsInt_ToNull_Test()
        {
            var runtime = new SpprologaRuntime(new CSPrologEngineAdapter());
            runtime.PrologEngine.ConsultFromString("age(\"John Titor\", null).");
            var binder = new FactBinder(runtime, "age(_, {0}).");

            binder.as_int.IsNull();
            binder.as_int = 36;
            runtime.query("age(_, N).")["N"].Is(36);

            binder.as_int = null;
            runtime.query("age(_, N).")["N"].Is("null");
        }
    }
}
