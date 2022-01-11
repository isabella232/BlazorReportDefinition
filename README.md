# Blazor Report Component with Layout in Razor Markup

## Idea
As you may already know, a new idea comes when you look at things from a different point of view. As Blazor developers, we looked at our report layout definition files and noticed that Reporting for Blazor lacks a designer component that would fit seamlessly into the Blazor ecosystem. Our existing visual report designer is quite complex and somewhat unpractical to web development. Web development includes layout (html + css) and logic, and visual designs are not popular in this world. 

With the release of Blazor we get an integration of client and server technologies, allowing us to take advantage of the "layout + logic" concept. With this in mind, we came up with the idea that a non-visual designer would be a great replacement for a visual Blazor component.

Furthermore the ability to create a report in markup allows us to create a report on all platforms, be it MAC or Linux, since development environments often support code completion.

The idea is simple at its core. Let's think of the report as a Blazor component with the layout defined in Razor markup language. When the page is prerendered on the server, the Blazor Report component (XReport) is transformed into an XtraReport instance in the Blazor render tree. We suggest three processing modes for the component - it can return a report instance to the page, preview a report in the Report Viewer, or display a "designer preview" for the report - a preview that does not build a document but visualizes the layout defined in markup. Hot reload functionality can make designing a report in markup even more appealing than working in the visual report designer.

## Implementation

To create a report, define it in the razor markup as follows:

```razor
@using DevExpress.Blazor.Reporting.Designer.Components

<XReport DataSource="CreateDataSource()" DataMember="Employees" Margins="XMargins.From(100, 100, 105, 75)"  ViewMode="ViewMode.Preview">
    <Bands>
        <XBandReportHeader Name="ReportHeader1" Height=50 >
            <XControlLabel Name="Label1" Text="Employees" Width="240" Style="Style1"></XControlLabel>
        </XBandReportHeader>
        <XBandDetail Name="Detail1" Height="30">
            <XControlLabel Name="Label2" X="10" Expression="[FirstName]" Width="120"></XControlLabel>
            <XControlLabel Name="Label3" Expression="[LastName]" X="125" Width="120"></XControlLabel>
        </XBandDetail>
        <XBandBottomMargin Height="100">
            <XControlPageInfo PageInfo="XPageInfo.DateTime" Width="120"  ></XControlPageInfo>
            <XControlPageInfo X="325" FormatString = "Page {0} of {1}"></XControlPageInfo>
        </XBandBottomMargin>
    </Bands>
    <Styles>
        <XStyle Name="Style1" TextAlignment="XTextAlignment.MiddleCenter" BackColor="XColor.From(109, 140, 89)" ForeColor="XColor.White">
            <XFont Name="@XFontNames.Tahoma" Size="20.25f"></XFont>
        </XStyle>
	</Styles>
</XReport>
```

The report is bound to the Employees table of the Northwind database. The resulting page appears as follows:

![image](https://user-images.githubusercontent.com/27409929/148933203-9c8ffb31-0982-4d94-89fb-c06343a93162.png)


Report layout in razor markup offers us a convenient report view that is easy to follow and is located on the same page as the component. You can take advantage of Intellisense and structured view.

Markup tags are named after related bands and reporting controls, i.e. the "X" prefix is added to the band type name, "XControl" prefix substitutes the "XR" prefix in the report control type name. 

The entire Report Designer functionality is quite extensive, and we do not attempt to cover it with the Razor markup because we do not want duplicate our xml layout format. We'll stick to the simplified version that contains only the most requested features, and if you need something specific, you can use the **Customize** method:

```razor
<XReport>
    <Bands>
        <XBandDetail>
            <XControlLabel Customize="x=>CustomizeLabel(x)"></XControlLabel>
        </XBandDetail>
    </Bands>
</XReport>

@code {
    XRLabel CustomizeLabel(XRLabel label) {
        label.KeepTogether = true;
        return label;
    }
}

```

If you have a custom control, or if you wish to extend the control properties,  you can make a descendant and implement the desired parameters. The following code samples shows how to set the KeepTogether property in the markup.

Create a descendant:

```csharp
    public class XControlLabelEx : XControlLabel {
        [Parameter] public bool KeepTogether { get; set; } = false;
        protected override XRLabel CreateControl() {
            var label = base.CreateControl();
            label.KeepTogether = KeepTogether;
            return label;
        }
    }
```

Use new property in razor markup:

```razor
<XReport>
    <Bands>
        <XBandDetail>
            <XControlLabelEx KeepTogether="true"></XControlLabelEx>
        </XBandDetail>
    </Bands>
</XReport>
```

## Render Modes

We suggest three render modes for the report layout component on the page. 

1. Razor markup is rendered and creates a XtraReport instance. 


2. Razor markup is rendered and displayed in the Report Viewer:


3. Razor markup is rendered and displayed in the design preview that enables the developer to edit markup and observe the results on the page with hot reload. The mode works as a report designer.

## What's Next

We can go towards html and css support. However, this solution is tied up with the browser type. The projected algorithm works as follows: we request computedStyles from the client and eventually position the component and apply the style based on the received data. With this approach we face a classic web development problem that the appearance depends on the browser.





