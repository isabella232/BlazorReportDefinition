# Blazor Reporting Component with Layout in Razor Markup

## Introduction
As you may already know, a new idea arises when you look at things from a different point of view. As Blazor developers, we reviewed our report layout definition files and noticed that Reporting for Blazor lacks a designer component that would fit seamlessly into the Blazor ecosystem. Our existing DevExpress Visual Report Designer might be a bit complex, while web development in its core includes layout (html + css) and logic, and visual designers are not that popular in this world. 

This leads to the following question: **How do we apply Web development techniques to report layout creation?**

Consider the following sample diagram. An item by the number one illustrates the way demonstrated in this sample, whereas the number two describes a regular report creation approach with the help of Visual Studio Report Designer:

![MicrosoftTeams-image (79)](https://user-images.githubusercontent.com/27409929/149539967-87170d98-80e6-4766-97d4-f7759318c8fc.png)

You can think of a Blazor report as a Blazor component with the layout defined in Razor markup. When the page is prerendered on the server, the Blazor Report component (XReport) is transformed into an XtraReport instance which, in turn, is wrapped into the [DevExpress Blazor Report Viewer component](https://docs.devexpress.com/XtraReports/DevExpress.Blazor.Reporting.DxReportViewer) automatically, and added to the Blazor render tree. Developers may prefer using hot reload functionality to design a report in markup instead of the DevExpress Visual Studio Report Designer.

Furthermore the ability to create a report in markup allows us to create a report on all platforms, be it MacOS or Linux.

## How to 

To create a report, define it in the Razor markup as follows:

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


A report layout in Razor markup generates a convenient report view that is easy to follow and is located on the same page as the component. The report view supports Intellisense and displays the report layout in a structured view.

Markup tags are named after related bands and reporting controls: the "X" prefix is added to the band type name and the "XControl" prefix replaces the "XR" prefix in the report control type name.

Since Web developers have their own techniques for positioning elements, we offer the most popular types of positioning - **flex** and **absolute**. You can use block positioning for report elements within the scope of a report. This technique works well regardless of the browser type, because the position of the elements will be the same in any browser. 

The entire Visual Studio Report Designer functionality is quite extensive, and we do not attempt to implement all the features with the Razor markup syntax because we do not want to duplicate XML layout format. We'll stick to the simplified version that contains only the most requested features, and if you need something specific, you can use the **Customize** method:

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

If you have a custom control, or if you wish to extend the control properties, you can make a descendant and implement the desired parameters. The following code samples show how to set the KeepTogether property in markup.

Create a `XControlLabel` class descendant:

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

Use the new property in Razor markup:

```razor
<XReport>
    <Bands>
        <XBandDetail>
            <XControlLabelEx KeepTogether="true"></XControlLabelEx>
        </XBandDetail>
    </Bands>
</XReport>
```


## Prerequisites

You should have the following installed in your system:

- .NET 5.0
- Visual Studio 2019 version 16.4+
- DevExpress DXperience Subscription v21.2.4 

## How to Run the Project

Download the project, open in Visual Studio and run. In the navigation pane you'll find two sample reports:  [EmployeeList.razor](https://github.com/e1em3ntoDX/BlazorReportDefinition/blob/master/Reports/EmployeeList.razor) and [TableReport.razor](https://github.com/e1em3ntoDX/BlazorReportDefinition/blob/master/Reports/TableReport.razor). 

Note that the required NuGet packages are automatically loaded from the local DevExpress package source and from the NuGet.org.

## How to Submit Feedback

We encourage you to create a Razor page with your own report to check the capabilities of this proof of concept and share your experience in the [Feedback Discussion](https://github.com/e1em3ntoDX/BlazorReportDefinition/discussions/6) for this repository. 





