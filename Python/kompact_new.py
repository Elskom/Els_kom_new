# coding=utf-8
# kompact_new.py
"""
Kom File Packer Python Scypt.
"""
import sys
import komformat


def main(argv):
    """
    Main Program Function.
    :param argv: Arguments.
    :return: Nothing.
    """
    komformat.packer_main(argv)


if __name__ == "__main__":
    main(sys.argv[1:])
