﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Chorus.merge.xml.generic;
using LibChorus.TestUtilities;
using NUnit.Framework;

namespace LibChorus.Tests.merge.xml
{
	[TestFixture]
	public sealed class FindByKeyAttributeTests
	{
		// A regression test: http://jira.palaso.org/issues/browse/WS-33895
		// Some lift ids have single quotes in them, so the xpath needs to be escaped correctly.
		[Test]
		public void GetNodeToMerge_WithSingleQuoteInAttribute_NoThrow()
		{
			string xml =
				@"<lift>
					<entry id=""te'st"" />
				</lift>";

			var doc1 = new XmlDocument();
			doc1.LoadXml(xml);

			var finder = new FindByKeyAttribute("id");
			var node = doc1.SelectSingleNode("//entry");
			var result = finder.GetNodeToMerge(node, doc1.DocumentElement);
			Assert.NotNull(result);
			XmlTestHelper.AssertXPathMatchesExactlyOne(result, "entry[@id=\"te'st\"]");
		}

		[Test]
		public void GetNodeToMerge_WithDoubleQuoteInAttribute_FindsIt()
		{
			string xml =
				@"<lift>
					<entry id='she said &quot;Hi!&quot;' />
				</lift>";

			var doc1 = new XmlDocument();
			doc1.LoadXml(xml);

			var finder = new FindByKeyAttribute("id");
			var node = doc1.SelectSingleNode("//entry");
			var result = finder.GetNodeToMerge(node, doc1.DocumentElement);
			Assert.AreEqual(node,result);
		}


		[Test]
		public void GetNodeToMerge_WithDoubleAndSingleQuotesInAttribute_FindsIt()
		{
			string xml =
				@"<lift>
					<entry id='she said &quot;It&apos;s raining!&quot;' />
				</lift>";

			var doc1 = new XmlDocument();
			doc1.LoadXml(xml);

			var finder = new FindByKeyAttribute("id");
			var node = doc1.SelectSingleNode("//entry");
			var result = finder.GetNodeToMerge(node, doc1.DocumentElement);
			Assert.AreEqual(node, result);
		}

		[Test]
		public void GetNodeToMerge_ReturnsNode()
		{
			string xml =
				@"<lift>
					<entry id='test' />
				</lift>";

			var doc1 = new XmlDocument();
			doc1.LoadXml(xml);

			var finder = new FindByKeyAttribute("id");
			var node = doc1.SelectSingleNode("//entry");
			var result = finder.GetNodeToMerge(node, doc1.DocumentElement);
			Assert.NotNull(result);
			XmlTestHelper.AssertXPathMatchesExactlyOne(result, "entry[@id=\"test\"]");
		}
	}

}
