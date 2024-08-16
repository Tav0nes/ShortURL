using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace ShortURL;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class EndToEnd : PageTest
{
    private IBrowser _browser;
    private IPage _page;

    [SetUp]
    public async Task Setup()
    {
        // Launch the browser (no need for IgnoreHTTPSErrors here)
        _browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true // Set to false if you want to see the browser UI
        });

        // Create a new context with IgnoreHTTPSErrors set to true
        var context = await _browser.NewContextAsync(new BrowserNewContextOptions
        {
            IgnoreHTTPSErrors = true
        });

        // Create a new page in this context
        _page = await context.NewPageAsync();
    }

    [TearDown]
    public async Task Teardown()
    {
        // Clean up by closing the page and browser after each test
        await _page.CloseAsync();
        await _browser.CloseAsync();
    }

    [Test]
    public async Task HasTitle()
    {
        // Navigate to the URL with SSL issues
        await _page.GotoAsync("http://localhost:56608/");

        // Expect a title "to contain" a substring
        await Expect(_page).ToHaveTitleAsync(new Regex("Home"));
    }

    [Test]
    public async Task GetStartedLink()
    {
        // Navigate to the URL with SSL issues
        await _page.GotoAsync("http://localhost:56608/");

        // Click the get started link
        await _page.GetByRole(AriaRole.Link, new() { Name = "List URL" }).ClickAsync();

        // Wait for the MudTable to be present on the page
        await _page.WaitForSelectorAsync("table");

        // Expects page to have a heading with the name "All URLs"
        await Expect(_page.GetByRole(AriaRole.Heading, new() { Name = "All URLs" })).ToBeVisibleAsync();
    }
}
