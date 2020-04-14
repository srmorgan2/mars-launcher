# ------------------------
# A sample MARS tool
# This script will be launched by
# the MARS launcher
#
# Johary 6/4/2020
# ------------------------

import sys
import json

if len(sys.argv) <= 1:
    print("Arguments expected.", file=sys.stderr)
    print("The first argument should be the absolute path to MARS", file=sys.stderr)
    exit(101)

MARS_PATH = sys.argv[1]
sys.path.append(MARS_PATH)

import pandas as pd
import time
import Base.DataAccess as Da


def process_my_data(my_data):
    """Simulates a long process to generate a dataframe"""

    print("Running process_my_data()...", file=sys.stderr)
    buffer = []
    for i in range(25):
        print('Step {}'.format(i), file=sys.stderr)
        time.sleep(0.02)
        buffer.append([i, 10 * i, 100 * i])

    result = pd.DataFrame(buffer, columns=["One", "Two", "Three"])
    return result


# ----------------------
#  The main program
# ----------------------

database = Da.DataAccess()
database.test_me()

print("Starting the MARS tool...", file=sys.stderr)

# Get JSON data from stdin and create a DataFrame from it
input_data = json.load(sys.stdin)
curve = input_data['Curve']
input_curve = pd.DataFrame(data=curve['data'], index=curve['index'], columns=curve['columns'])
input_curve = input_curve.dropna()
input_curve['Rate'] = pd.to_numeric(input_curve['Rate'], errors='coerce')

# Process the DataFrame (produce dummy discount factors)
output_data = process_my_data(input_data)
output_dfs = pd.DataFrame(input_curve)
output_dfs.rename(columns={'Rate':'DF'}, inplace=True)
output_dfs['DF'] = 1/ (1 + output_dfs['DF'])

# Example - output Multiple DataFrames to stdout
result = {}
result['text'] = 'value'
result['Input Curve'] = input_curve.to_dict(orient='split')
result['Output Discount Factors'] = output_dfs.to_dict(orient='split')
print(json.dumps(result))
print("Done.", file=sys.stderr)
