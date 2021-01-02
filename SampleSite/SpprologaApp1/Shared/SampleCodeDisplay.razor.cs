using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace SpprologaApp1.Shared
{
    public partial class SampleCodeDisplay
    {
        [Inject]
        private IJSRuntime JS { get; set; }

        [Parameter]
        public ComponentBase Component { get; set; }

        [Parameter]
        public string PageName { get; set; }

        private string TypeName = "";

        private string RazorCode = "";

        private string PrologCode = "";

        private bool SampleCodeExpanded = false;

        protected override void OnInitialized()
        {
            this.fact("current_page({0})").as_atom = PageName;

            var targetType = this.Component.GetType();
            this.TypeName = targetType.Name;

            var razorCode = GetCodeText(targetType, ".razor");

            this.RazorCode = Regex.Replace(razorCode, @"[\r\n ]*<SampleCodeDisplay[^>]*/>[\r\n ]*", "");

            this.PrologCode = GetCodeText(targetType, ".razor.prolog");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await this.JS.InvokeVoidAsync("highlightCode");
        }

        private static string GetCodeText(Type targetType, string extension)
        {
            var assembly = targetType.Assembly;
            var resName = targetType.FullName + extension;

            if (!assembly.GetManifestResourceNames().Contains(resName)) return "";
            using var stream = assembly.GetManifestResourceStream(resName);
            if (stream == null) return "";
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        private void ToggleShowHideCode()
        {
            this.SampleCodeExpanded = !this.SampleCodeExpanded;
        }
    }
}

// This sample source codes are distributed under The Unlicense. (https://unlicense.org/)