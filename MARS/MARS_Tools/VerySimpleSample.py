# ------------------------
# A sample MARS tool
# This script will be launched by
# the MARS launcher
#
# Johary 6/4/2020
# ------------------------

import sys

if len(sys.argv) <= 1:
    print("Arguments expected.")
    print("The first argument should be the absolute path to MARS")
    exit(101)

MARS_PATH = sys.argv[1]
sys.path.append(MARS_PATH)

import pandas as pd
import time
import Base.DataAccess as Da


def process_my_data(my_data):
    """Simulates a long process to generate a dataframe"""

    print("Running process_my_data()...")
    buffer = []
    for i in range(25):
        print('Step {}'.format(i))
        time.sleep(0.2)
        buffer.append([i, 10 * i, 100 * i])

    result = pd.DataFrame(buffer, columns=["One", "Two", "Three"])

    return result


# ----------------------
#  The main program
# ----------------------

database = Da.DataAccess()
database.test_me()

print("Starting the MARS tool...")

input_data = pd.DataFrame()
output_data = process_my_data(input_data)
print(output_data)

print("Done")
