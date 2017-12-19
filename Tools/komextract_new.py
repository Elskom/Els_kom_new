# coding=utf-8
# komextract_new.py
"""
Kom File Extractor Python Scypt.
"""
import sys
import komformat


def main(argv):
    """
    Main Program Function.
    :param argv: Arguments.
    :return: Nothing.
    """
    komformat.unpacker_main(argv)


if __name__ == "__main__":
    main(sys.argv[1:])
