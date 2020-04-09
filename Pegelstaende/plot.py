# -*- coding: utf-8 -*-
"""    

Examples:
        $ python3 plot.py Tagliamento.txt.txt
        $ python3 plot.py -t -0.2 Tagliamento.txt.txt tagliamento_plot


Todo:


"""

import argparse
import sys
import os

import matplotlib
matplotlib.use('Agg')
import matplotlib.pyplot as plt
import numpy as np


def readInput(textinput, separator=';', float_sep=','):
  levels = []
  timestamps = []
  for line in textinput:
    fields = line.split(separator)
    timestamp = fields[0]
    timestamps.append(timestamp)
    level = float(fields[1].replace(float_sep, '.'))
    levels.append(level)
  return timestamps, levels

def parseArguments():
  parser = argparse.ArgumentParser(description='Read text file and plot')
  parser.add_argument('infile', nargs='?', 
                        type=argparse.FileType('r'), default=sys.stdin,
                        help='Input file')
  parser.add_argument('outfile', nargs='?', 
                        type=str, default='plot',
                        help='Filename for plot')
  parser.add_argument('-t', '--threshold', type=float, help='Threshold is shown as horizontal line, format <x.x>')  
  parser.add_argument('-n', dest='nlast', type=int, help='Plots only last n lines of infile')  

  return parser.parse_args()

if __name__ == '__main__':

  args = parseArguments()
  textinput = None
  infname = None
  with args.infile as f:
    textinput = [line.strip() for line in f]
    infname = f.name
    if args.nlast:
      textinput = textinput[-args.nlast:]

  timestamps, levels = readInput(textinput)

  x = np.arange(len(levels))
  fig, ax = plt.subplots()
  ax.plot(x, levels)
  if args.threshold:
    ax.plot(x, len(x)*[args.threshold], '--')
  ax.set_xlim(0, len(levels)-1)
  xticks = ax.get_xticks()
  xticks[-1] = len(levels)-1
  ax.set_xticks(xticks)
  ax.set_xticklabels([timestamps[int(x)] for x in xticks], rotation = 45, ha="right")
  ax.set_ylabel('Pegelstand')
  ax.set_title(os.path.splitext(infname)[0])
  plt.savefig(args.outfile, bbox_inches = 'tight')

