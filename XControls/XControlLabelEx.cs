using DevExpress.Blazor.Reporting.Designer.Components;
using DevExpress.XtraReports.UI;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorReportDemos {
    public class XControlLabelEx : XControlLabel {
        [Parameter] public bool KeepTogether { get; set; } = false;
        protected override XRLabel CreateControl() {
            var label = base.CreateControl();
            label.KeepTogether = KeepTogether;
            return label;
        }
    }
}
