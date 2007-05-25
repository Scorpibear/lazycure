VisualStudio 8.0 and NUnit 2.4 have to be installed in order to perform developing activities. 'bin' folder of NUnit 2.4 have to be added to 'PATH' environment variable.

Rules for developers:
1. Bug. Assign a bug to yourself before introducing any code changes.
2. Red. Write a failing automated unit test before you write any code, which reproduces the bug.
3. Green. Write code in order to make unit test passed.
4. Refactor. Remove code duplication, verifying that all unit tests still passed. You can repeat 2-4 steps by introducing additional tests, related to the bug.
5. Commit. Commit the code changes, specifying bug id and summary and any other details of maden changes.

Notes.

During fixing bug, which require a lot of Red/Green/Refactor steps, you can create branch in /branches/YOURNAMEGOESHERE/ svn folder and commit your changes to here. After fixing bug completely, merge you changes with the latest revision on /trunk svn folder and commit changes to /trunk.

When you could not create automated unit test, add manual test case to LazyCure_TestCases.xls file. Every bug should have appropriate test case.


