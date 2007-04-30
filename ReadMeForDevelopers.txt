VisualStudio 8.0 and NUnit 2.0 have to be installed in order to perform developing activities. 'bin' folder of NUnit 2.0 have to be added to 'PATH' environment variable.

Rules for developers:
1. Assign a bug to yourself before introducing any code changes.
2. Write a failing automated unit test before you write any code, which reproduces the bug.
3. Write code in order to make unit test passed.
4. Remove code duplication, verifying that all unit tests still passed. You can repeat 2-4 steps by introducing additional tests, related to the bug.
5. Commit the code changes, specifying bug id and summary and any other details of maden changes.