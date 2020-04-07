﻿using Microsoft.DotNet.XHarness.iOS.Shared.Logging;

namespace Xharness.TestTasks {
	/// <summary>
	/// Interface to be implemented by those classes that know about common errors that will be reporter to the 
	/// harness runner. This allows to store certain problems that we know are common and that we can skip, helping
	/// those that are monitoring the result.
	/// </summary>
	public interface IErrorKnowledgeBase {
		bool IsKnonwBuildIssue (ILog buildLog, out string knonwFailureMessage);
		bool IsKnonwTestIssue (ILog runLog, out string knonwFailureMessage);
	}
}
