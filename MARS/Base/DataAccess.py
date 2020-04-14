# ---------------------------
#  This is a mock file
# ---------------------------

import sys

class DataAccess:
    def __init__(self):
        self._name = "The mock data access object."

    def test_me(self):
        print("Object successfully created: " + self._name, file=sys.stderr)
