# The structure of the project is as follows:

+------------------+
|       API        |  <-- depends on Application (and Infrastructure indirectly via DI)
+------------------+
          |
          v
+------------------+
|  Infrastructure  |  <-- depends on Application & Domain
+------------------+
          |
          v
+------------------+
|   Application    |  <-- depends on Domain
+------------------+
          |
          v
+------------------+
|      Domain      |  <-- depends on nothing
+------------------+
