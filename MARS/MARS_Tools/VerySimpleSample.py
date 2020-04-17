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
import math
import datetime
import Base.DataAccess as Da


def print_dataframe(df):
    print("<<<<Data")
    print("CSV")
    df.to_csv(sys.stdout)
    print("Data>>>")


def process_my_data(my_data):
    """Simulates a long process to generate a dataframe"""

    print("Running process_my_data()...")
    buffer = []

    current_date = datetime.date(2020, 1, 10)

    for i in range(25):
        print('Step {}'.format(i))
        time.sleep(0.01)
        buffer.append([current_date,
                       'ABC{}'.format(i + 1),
                       math.sqrt(1000 * i),
                       i % 4 + 1])
        current_date = current_date + datetime.timedelta(days=15)

    result = pd.DataFrame(buffer, columns=["Date", "Name", "Amount", "Size"])

    return result


# ----------------------
#  The main program
# ----------------------

database = Da.DataAccess()
database.test_me()

print("Starting the MARS tool...")

input_data = pd.DataFrame()
output_data = process_my_data(input_data)

print_dataframe(output_data)

print("Done")