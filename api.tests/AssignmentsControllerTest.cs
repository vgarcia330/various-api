using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using api.Controllers;

namespace various_api.tests
{
    [TestClass]
    public class AssignmentsControllerTest
    {
/*
Scheduled
630110	1	Baugh, Mary	Bio PAP	A ‑ 1	301
641110	4	Sierra, Wendy	W Geog PAP	A ‑ 2	110
662110	JV Band	Riley, Thomas	Band 1	A ‑ 3	Band Hall
623020	9th gr only	Taylor, Jennifer	Geom PAP	A ‑ 4	221
611110	1	Justice, Jeremiah	Eng 1 PAP	B ‑ 5	204
679020	2	Weyant, Terrance	CT IntroEngDes	B ‑ 6	731
670010	1	Hairston, Lauren	CT Prin AgFdNR	B ‑ 7	721B
694120	1	Pineda‑Gabehart, Yolanda	Span 2 PAP	B ‑ 8	411

 */
        private const string UrlBiology = "http://www.georgetownisd.org/Page/3695";
        private const string UrlWorldgeography = "http://www.georgetownisd.org/Page/2761";
        private const string UrlJvband = "http://www.georgetownisd.org/Page/9583";
        private const string UrlGeometry = "http://www.georgetownisd.org/Page/9583";
        private const string UrlEnglish = "http://www.georgetownisd.org/Page/9676";
        private const string UrlIntroEngDeisgn = "http://www.georgetownisd.org/Page/9583";
        private const string UrlPrinciplesAg = "http://www.georgetownisd.org/Page/9583";
        private const string UrlSpanish = "http://www.georgetownisd.org/Page/9583";

        private const string AssignmentSection = "<!-- Start Centricity Content Area -->";
        //private const string AssignmentSeciontDiv = "<div id=\"sw-content-layout-wrapper\" class=\"ui-sp ui-print\">";
        private const string HtmlNodeAssignmentDiv = "//div[@id='sw-content-layout-wrapper']";

        private readonly MockRepository _mock;
        private AssignmentsController _controller;
        private HttpContextBase _context;

        public AssignmentsControllerTest()
        {
            _mock = new MockRepository();    
        }

        public HttpContextBase MockContext()
        {
            var context = _mock.Stub<HttpContextBase>();
            var request = _mock.Stub<HttpRequestBase>();
            context.Stub(c => c.Request).Return(request);
            request.Expect(x => x.Url).Return(new Uri("http://www.yahoo.com")).Repeat.Any();

            context.Expect(c => c.Request).Return(request).Repeat.Any();
            _mock.ReplayAll();

            return context;
        }

        [TestInitialize]
        public void ShouldSetupTests()
        {
            _context = MockContext();
            _controller = new AssignmentsController();
            _controller.ControllerContext = new ControllerContext(_context, new RouteData(), _controller);
        }

        [TestCleanup]
        public void ShouldCleanupTests()
        {
            
        }

        [TestMethod]
        public async Task ShouldGetWebsiteByUrl()
        {
            if (_context.Request.Url != null)
            {
                var url = "";
                var result = _controller.GetAssignmentHtml(url);
                Assert.IsTrue(result.Result.Data.ToString().Contains("Error. Empty Url."));
            }
        }

        [TestMethod]
        public void ShouldGetWebsiteHtml()
        {
            var url = "http://www.georgetownisd.org/Page/2761";
            var result = _controller.GetWebsiteHtml(url);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShouldGetWebsiteHtmlShouldContainTag()
        {
            var url = "http://www.georgetownisd.org/Page/2761";
            var result = _controller.GetWebsiteHtml(url);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains("<!-- Start Centricity Content Area -->"));
        }







        // 1. class name : Biology
        [TestMethod]
        public void ShouldGetWebsiteHtmlShouldContainTagForBiology()
        {
            var url = UrlBiology;
            var result = _controller.GetWebsiteHtml(url);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains(AssignmentSection));
        }

        // 2. class name : World Geography
        [TestMethod]
        public void ShouldGetWebsiteHtmlShouldContainTagForWorldGeography()
        {
            var url = UrlWorldgeography;
            var result = _controller.GetWebsiteHtml(url);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains(AssignmentSection));
        }

        // 3. class name : JV Band
        [TestMethod]
        public void ShouldGetWebsiteHtmlShouldContainTagForJvBand()
        {
            var url = UrlJvband;
            var result = _controller.GetWebsiteHtml(url);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains(AssignmentSection));
        }

        // 4. class name : Geometry
        [TestMethod]
        public void ShouldGetWebsiteHtmlShouldContainTagForGeometry()
        {
            var url = UrlGeometry;
            var result = _controller.GetWebsiteHtml(url);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains(AssignmentSection));
        }

        // 5. class name : English
        [TestMethod]
        public void ShouldGetWebsiteHtmlShouldContainTagForEnglish()
        {
            var url = UrlEnglish;
            var result = _controller.GetWebsiteHtml(url);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains(AssignmentSection));
        }

        // 6. class name : Intro Engineering Design
        [TestMethod]
        public void ShouldGetWebsiteHtmlShouldContainTagForIntroEngineeringDesign()
        {
            var url = UrlIntroEngDeisgn;
            var result = _controller.GetWebsiteHtml(url);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains(AssignmentSection));
        }

        // 7. class name : Principles Agricultrue
        [TestMethod]
        public void ShouldGetWebsiteHtmlShouldContainTagForPrinciplesArgiculture()
        {
            var url = UrlPrinciplesAg;
            var result = _controller.GetWebsiteHtml(url);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains(AssignmentSection));
        }

        // 8. class name : Spanish
        [TestMethod]
        public void ShouldGetWebsiteHtmlShouldContainTagForSpanish()
        {
            var url = UrlSpanish;
            var result = _controller.GetWebsiteHtml(url);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains(AssignmentSection));
        }




        [TestMethod]
        public async Task ShouldGetWebsiteHtmlShouldContainTagForBiologyAssignmentNode()
        {
            var url = UrlBiology;
            var responseMessage = await _controller.GetWebPageAsync(url);
            Assert.IsNotNull(responseMessage);

            var document = await _controller.GetDocumentAsync(responseMessage);
            Assert.IsNotNull(document);

            var node = _controller.GetDocumentNode(responseMessage, HtmlNodeAssignmentDiv);
            Assert.IsNotNull(node);

            Assert.IsNotNull(node.InnerHtml);
        }
    }
}