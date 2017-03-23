#title           :get_urls.py
#description     :Download content from urls and place them in their respected
#                 directories.
#author          :Ali Hamdan
#date            :11/14/2016
#version         :0.1
#usage           :python get_urls.py
#notes           :
#python_version  :2.7.12  
#==============================================================================

import os
import sys
import re
import requests
import argparse
import gzip


class downloader(object):
    def __init__(self, verbose):
        self.urls = []
        self.verbose = verbose
        # Gets directory script lives in.
        self.full_path = os.path.realpath(__file__)
        # Path to the data directory
        self.data_dir = os.path.dirname(self.full_path).replace('bin', 'data')

    # Creates file directory within the data directory and returns new path

    def createDir(self, fileName):
        newDir = os.path.join(self.data_dir, fileName)
        if not os.path.exists(newDir):
            if self.verbose:
                print 'Creating directory at "{}"'.format(newDir)
            os.makedirs(newDir)
        else: 
            if self.verbose:
                print 'Directory already exists at "{}"'.format(newDir)
        return newDir

    # Uses regex to retrieve the source from a url and returns it

    def getSource(self, url):
        #gets shorter source
        pattern = r"https?:\/{2}([^.]+)"
        #gets website source
        pattern2 = r"https?:\/{2}([^\/]+)"
        try:
            result = re.match(pattern, url).group(1)
        except Exception as e:
            raise AttributeError('Incorrect URL format: "{}"'.format(url))
        if self.verbose: print "\nRaw: {}\nExtracted Source: {}".format(url, result)
        return "{}_data".format(result)

    # Gets Urls from file.

    def getUrls(self, url_file):
        try:
            with open(url_file, 'r') as feed_file:
                # split file by lines into list
                self.urls.extend(filter(None, feed_file.read().split("\n")))
        except (OSError, IOError) as e:
            print "Could not find a file containing URLS."

    # Downloads url content and extracts if needed.

    def dlUrl(self, url):
        # Open the url
        try:
            filename = url.split("/")[-1]

            # Get source name from url
            source = getSource(url)
            # Create directory for the data to be stored in.
            outDir = createDir(source)


            # Open the url 
            response = requests.get(url)

            total_length_kb = int(response.headers.get('content-length')) / 1024

            if self.verbose: print "Downloading: {} [{}KB]".format(
                                        filename, total_length_kb)


            if filename.endswith('.gz'): filename = filename[:-3]
            with open(os.path.join(outDir, filename), "wb") as local_file:
                local_file.write(response.content)
                #  for chunk in response.iter_content(chunk_size=1024):
                #      if chunk:
                #          local_file.write(chunk)
                # if its a gz file, unzip it
                #  if filename.endswith('.gz') or filename.endswith('.tgz'):
                #      unzip(filename, outDir)
                
        # handle errors
        except Exception as e:
            print "\nError:", e

    """
    # Extracts contents from archive.
    # Currently only supports .gz
    def unzip(inFileName, directory):
        # just incase
        # if not inFileName.endswith('.gz'): return

        if self.verbose:
            print "Unzipping: {}".format(inFileName)
        # Stripping ".gz" extention from inFileName.
        outFileName = inFileName[:-3]
        with gzip.open(os.path.join(directory, inFileName), "rb") as infile:
            with open(os.path.join(directory, outFileName), "wb") as outfile:
                for line in infile:
                    outfile.write(line)
    """


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('-urls', type=str, default="src_urls.txt",
                        help='A file containing the urls to download on separate lines.')
    parser.add_argument('-v', action='store_true',
                        help='Run the script in Verbose mode.')
    args = parser.parse_args()

    # Check for verbose mode
    verbose = args.v
    dler = downloader(verbose)

    # Get urls from file
    dler.getUrls(args.urls)
    # Download urls supplied
    for url in dler.urls:
        dler.dlUrl(url)


if __name__ == '__main__':
    main()