# ------------------------
# A sample MARS tool
# This script will be launched by
# the MARS launcher
#
# Johary 6/4/2020
# ------------------------

import sys

if len(sys.argv) <= 1:
    print("Arguments expected.", file=sys.stderr)
    print("The first argument should be the absolute path to MARS", file=sys.stderr)
    #exit(101)

#MARS_PATH = sys.argv[1] # r"C:\Users\steve\Projects\mars-launcher\MARS"
MARS_PATH = r"C:\Users\steve\Projects\mars-launcher\MARS"
sys.path.append(MARS_PATH)

import pandas as pd
import numpy as np
import random as rd
import time
import math
import datetime
import json
import base64
import io
import matplotlib.pyplot as plt
import Base.DataAccess as Da

def print_dataframe_as_csv(df):
    print("<<<<Data")
    print("CSV")
    df.to_csv(sys.stdout)
    print("Data>>>")


def add_data_to_dictionary(dct, key, data):
    type_to_key_map = {pd.DataFrame: 'DataFrames', dict: 'Dictionaries', bytes: 'Figures'}

    data_type = type(data)
    if data_type in type_to_key_map:
        data_type_key = type_to_key_map[data_type]
        if data_type_key not in dct:
            dct[data_type_key] = {}

    if type(data) == pd.DataFrame:
        dct[data_type_key][key] = data.to_dict(orient='split')
    elif type(data) == dict:
        dct[data_type_key][key] = data
    elif type(data) == bytes:
        data_as_str = base64.b64encode(data).decode()
        dct[data_type_key][key] = data_as_str
    else:
        raise Exception("Only DataFrames can be added to the output.")


def to_dataframe(json_dict):
    df = pd.DataFrame(data=json_dict['data'], index=json_dict['index'], columns=json_dict['columns'])
    return df


def json_converter(o):
    if isinstance(o, datetime.datetime):
        return o.__str__()
    elif isinstance(o, datetime.date):
        return o.__str__()


def print_dictionary_as_json(dct):
    print("<<<<Data")
    print("JSON")
    print(json.dumps(dct, default=json_converter))
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
                       math.sqrt(1000 * i) * rd.random(),
                       i % 4 + 1])
        current_date = current_date + datetime.timedelta(days=15)

    result = pd.DataFrame(buffer, columns=["Date", "Name", "Amount", "Size"])

    return result


def get_current_plot_as_binary():
    buf = io.BytesIO()
    plt.savefig(buf, format='png')
    buf.seek(0)
    return buf.read()


def plot_some_data(fig_num):
    data = {'a': np.arange(50),
        'c': np.random.randint(0, 50, 50),
        'd': np.random.randn(50)}
    data['b'] = data['a'] + 10 * np.random.randn(50)
    data['d'] = np.abs(data['d']) * 100

    plt.clf()
    plt.scatter('a', 'b', c='c', s='d', data=data)
    plt.xlabel('Entry a')
    plt.ylabel('Entry b')
    plt.title(f'Figure {fig_num}: Scatter Plot A vs B')


# ----------------------
#  The main program
# ----------------------

database = Da.DataAccess()
database.test_me()

print("Starting the MARS tool...")

input_data = pd.DataFrame()
output_data = {}

for i in range(5):
    df =  process_my_data(input_data)
    add_data_to_dictionary(output_data, f"Table {i+1}", df)
    plot_some_data(i+1)
    binary_plot_data = get_current_plot_as_binary()
    add_data_to_dictionary(output_data, f"Figure {i+1}", binary_plot_data)

print_dictionary_as_json(output_data)

print("Done")
