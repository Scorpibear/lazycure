First steps:
1. Verify that the following software is installed: VisualStudio 8.0 or higher, NUnit 2.4 or higher, SVN Client 1.4 or higher.
2. Make svn checkout for https://lazycure.svn.sourceforge.net/svnroot/lazycure/trunk.
3. Build LazyCure project and verify, that all unit tests are passed. 

One development cycle:
1. Bug. Assign a bug to yourself before introducing any code changes.
2. Red. Write a failing automated unit test before you write any code, which reproduces the bug.
3. Green. Write code in order to make unit test passed.
4. Refactor. Remove code duplication, verifying that all unit tests still passed. You can repeat 2-4 steps by introducing additional tests, related to the bug.
5. Commit. Commit the code changes, specifying bug id and summary and any other details of maden changes.

Notes:
When you could not create automated unit test, add manual test case to LazyCure_TestCases.xls file. Every bug should have appropriate test case.
LazyCure requirements could be found at http://lazycure.wiki.sourceforge.net/Requirements.
In case of any questions, please, contact Mikhail Subach (Scorpibear) by emailing at scorpibear@tut.by.



